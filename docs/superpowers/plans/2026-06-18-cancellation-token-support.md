# Comprehensive CancellationToken Support — Implementation Plan

> **For agentic workers:** REQUIRED SUB-SKILL: Use superpowers:subagent-driven-development (recommended) or superpowers:executing-plans to implement this plan task-by-task. Steps use checkbox (`- [ ]`) syntax for tracking.

**Goal:** Add an optional `CancellationToken` to every public client method across `Octokit` and `Octokit.Reactive`, threaded all the way to the HTTP layer.

**Architecture:** Three phases. (1) Foundation — thread `CancellationToken` through the two shared layers (`Connection`/`ApiConnection` in core; `GetAndFlattenAllPages` helpers in Reactive), done sequentially by the orchestrator. (2) Fan-out — parallel cheap agents convert one client at a time (4 disjoint files per client), edit-only. (3) Integration — regenerate pagination extensions, full build + tests, add forwarding tests.

**Tech Stack:** C# / .NET (netstandard2.0 core, net6.0+net462 tests), xUnit, NSubstitute, Cake build, `Octokit.Generators` (Roslyn-less string generator).

## Global Constraints

- **Pattern:** add `CancellationToken cancellationToken = default` as the **trailing** parameter, **in-place** on existing methods (interface + implementation). No new overloads. This mirrors the existing `OAuthClient`/`ReleasesClient` precedent (PR #2988).
- **Spelling:** the parameter is `cancellationToken` (fix any `concellationToken` typos encountered).
- **Threading rule:** every method must pass its `cancellationToken` onward — through any sibling-overload delegation, and into the final `ApiConnection`/`Connection`/underlying-client call. Never drop it and never pass `CancellationToken.None`/`default` where a real token is in scope.
- **Whole clients only:** never leave an interface and its implementation with mismatched signatures.
- **Line length:** project `.editorconfig` allows wide lines; do not wrap signatures artificially.
- **Build/test runtime:** only .NET 8/10 runtimes are installed, not net6.0 — prefix test commands with `DOTNET_ROLL_FORWARD=Major`.
- **Baseline (must stay green):** build 0 errors; `Octokit.Tests` = 4546 passed / 0 failed / 2 skipped.
- **Convention note:** `CheckObservableClients` enforces method-**name** parity (not signatures), so adding parameters never breaks it regardless of ordering between sync and observable edits.

---

### Task 1: Foundation — core `Connection` + `ApiConnection` (orchestrator, sequential)

**Files:**
- Modify: `Octokit/Http/IConnection.cs`, `Octokit/Http/Connection.cs`
- Modify: `Octokit/Http/IApiConnection.cs`, `Octokit/Http/ApiConnection.cs`
- Test: `Octokit.Tests/Http/ApiConnectionTests.cs`, `Octokit.Tests/Http/ConnectionTests.cs`

**Interfaces — Produces** (later tasks rely on these exact signatures existing with `= default`):
- `IConnection`: add `CancellationToken cancellationToken = default` to the `Get`/`GetRaw`/`GetHtml`/`Put`/`Patch`/`Delete` overloads that currently lack it (the `Post` family already has it).
- `IApiConnection`: add `CancellationToken cancellationToken = default` to `Get<T>` (3), `GetHtml`, `GetRaw`, `GetRawStream`, `GetAll<T>` (9), `Put`/`Put<T>` (all), `Patch`/`Patch<T>` (all), `Delete`/`Delete<T>` (all). `Post*` and `GetQueuedOperation` already have it.

**Threading:** `Connection` verb methods currently call `SendData<T>(..., CancellationToken.None)` — replace `CancellationToken.None` with the incoming `cancellationToken`. `ApiConnection` methods currently call `Connection.X(...)` — pass `cancellationToken` through. `SendData` and `HttpClientAdapter` already accept a token; no change needed below `Connection`.

- [ ] **Step 1: Write failing tests** asserting the token reaches `Connection`. In `ApiConnectionTests.cs`, for a representative verb of each shape (`Get<T>`, `GetAll<T>`, `Put<T>`, `Patch<T>`, `Delete<T>`):

```csharp
[Fact]
public async Task GetPassesCancellationTokenToConnection()
{
    var cts = new CancellationTokenSource();
    var connection = Substitute.For<IConnection>();
    var apiConnection = new ApiConnection(connection);

    await apiConnection.Get<object>(new Uri("anything", UriKind.Relative), null, null, cts.Token);

    connection.Received().Get<object>(Arg.Any<Uri>(), Arg.Any<IDictionary<string, string>>(), Arg.Any<string>(), cts.Token);
}
```

- [ ] **Step 2: Run, verify they fail to compile/assert**
  Run: `DOTNET_ROLL_FORWARD=Major dotnet test Octokit.Tests/Octokit.Tests.csproj -f net6.0 --filter "FullyQualifiedName~ApiConnectionTests"`
  Expected: compile failure (overload lacks the parameter) or assertion failure.

- [ ] **Step 3: Add the parameter + threading** to `IConnection`/`Connection`, then `IApiConnection`/`ApiConnection`, per the Produces block and Threading rule above. Update XML doc comments to include `<param name="cancellationToken">An optional token to monitor for cancellation requests</param>`.

- [ ] **Step 4: Run the new tests — expect PASS**
  Run: same command as Step 2.

- [ ] **Step 5: Full build + baseline tests**
  Run: `dotnet build Octokit.sln` then `DOTNET_ROLL_FORWARD=Major dotnet test Octokit.Tests/Octokit.Tests.csproj -f net6.0`
  Expected: 0 errors; 4546+ passed.

- [ ] **Step 6: Commit**
  `git add Octokit/Http Octokit.Tests/Http && git commit -m "feat: thread CancellationToken through Connection and ApiConnection"`

---

### Task 2: Foundation — Reactive pagination helpers (orchestrator, sequential)

**Files:**
- Modify: `Octokit.Reactive/Internal/*` — the `GetAndFlattenAllPages` / `GetAndFlattenAllPagesFromUrl` extension family on `IConnection`.
- Test: `Octokit.Tests/Reactive/` (representative)

**Interfaces — Produces:** every `GetAndFlattenAllPages*` overload gains a trailing `CancellationToken cancellationToken = default`, passed into the underlying `connection.Get<List<T>>(..., cancellationToken)` calls (now available from Task 1).

- [ ] **Step 1:** Locate the helpers: `grep -rl "GetAndFlattenAllPages" Octokit.Reactive/Internal`
- [ ] **Step 2: Write a failing test** asserting the token flows to `IConnection.Get` from a flattened-pages call (mirror the Task 1 test shape, using a substituted `IConnection`).
- [ ] **Step 3: Run — expect fail.** `DOTNET_ROLL_FORWARD=Major dotnet test Octokit.Tests/Octokit.Tests.csproj -f net6.0 --filter "FullyQualifiedName~GetAndFlattenAllPages"`
- [ ] **Step 4: Add the parameter + threading** to each `GetAndFlattenAllPages*` overload.
- [ ] **Step 5: Build Reactive + run test — expect pass.** `dotnet build Octokit.Reactive/Octokit.Reactive.csproj`
- [ ] **Step 6: Commit.** `git add Octokit.Reactive/Internal Octokit.Tests/Reactive && git commit -m "feat: thread CancellationToken through Reactive pagination helpers"`

---

### Task 3: Per-client conversion (parallel cheap agents, edit-only)

This task is applied **once per client** by a dispatched agent. Agents are **edit-only** (no build — avoids `bin/obj` races between parallel agents in the shared worktree). The orchestrator builds per wave (Task 4 cadence).

**Work unit = one client = up to 4 files:**
- `Octokit/Clients/I{Name}Client.cs`
- `Octokit/Clients/{Name}Client.cs`
- `Octokit.Reactive/Clients/IObservable{Name}Client.cs` (if it exists)
- `Octokit.Reactive/Clients/Observable{Name}Client.cs` (if it exists)

**Tiering (by interface method count):**
- **Sonnet** (≥20 methods): RepositoryBranches, Repositories, Teams, Releases, IssuesLabels, Gists, Starred, OrganizationMembers, Issues, RepositoryCommits, PullRequests, IssueComments, Events, Notifications, RepoCollaborators, PullRequestReviewComments, CheckRuns, ActionsWorkflowRuns, Packages, Statistics.
- **Haiku** (<20 methods): all remaining clients (~94).

**Batching:** ~10–15 clients per agent, grouped within a tier. ~12–18 agents across waves.

**THE PER-CLIENT BRIEF (paste into each agent, substituting the client list):**

> You are converting Octokit.net client(s) to accept an optional `CancellationToken`. Work ONLY in `/home/james/repos/octokit.net/.claude/worktrees/cancellation-token-support`. DO NOT run any build or test command. Edit files only.
>
> For EACH client `{Name}` in your assigned list, edit these files if they exist: `Octokit/Clients/I{Name}Client.cs`, `Octokit/Clients/{Name}Client.cs`, `Octokit.Reactive/Clients/IObservable{Name}Client.cs`, `Octokit.Reactive/Clients/Observable{Name}Client.cs`.
>
> Rules:
> 1. Add `CancellationToken cancellationToken = default` as the **last** parameter of **every public `Task`/`Task<T>`-returning method** in the core interface + implementation, and **every public `IObservable<T>`-returning method** in the observable interface + implementation.
> 2. Add `using System.Threading;` to any edited file that does not already import it.
> 3. **Thread the token:**
>    - When a method body delegates to a sibling overload (e.g. `return GetAll(owner, name, ApiOptions.None);`), append `cancellationToken`: `return GetAll(owner, name, ApiOptions.None, cancellationToken);`.
>    - When a method calls `ApiConnection.X(...)`, append `cancellationToken` as the last argument.
>    - In **observable** clients: methods that wrap `_client.X(...).ToObservable()` become `_client.X(..., cancellationToken).ToObservable()`; methods that call `_connection.GetAndFlattenAllPages<T>(uri, options)` become `_connection.GetAndFlattenAllPages<T>(uri, options, cancellationToken)`.
> 4. Add a `<param name="cancellationToken">An optional token to monitor for cancellation requests</param>` line to each method's XML doc comment in the **interface** files (implementations usually have `<inheritdoc/>` or no doc — leave those).
> 5. Do NOT change `[ManualRoute]`/`[GeneratedRoute]` attributes, method names, return types, or existing parameters/their order. Do NOT add overloads.
> 6. If you see the typo `concellationToken`, correct it to `cancellationToken`.
> 7. After editing, re-read each method you changed and confirm: signature ends with the token; every internal call forwards it; interface and implementation signatures match exactly.
>
> Report: list each file edited and the count of methods changed per file. Flag any method where the call site was ambiguous and you were unsure how to thread the token.

- [ ] Dispatch agents per the batching/tiering above (orchestrator, via superpowers:dispatching-parallel-agents).

---

### Task 4: Integration & verification (orchestrator, sequential)

**Files:**
- Regenerate: `Octokit.AsyncPaginationExtension/*` via `Octokit.Generators`
- Test: `Octokit.Tests`, `Octokit.Tests.Conventions`

- [ ] **Step 1: After each wave of agents, build** `dotnet build Octokit.sln`. Collect compile errors; dispatch a focused cleanup agent (Sonnet) per failing file with the error text. Repeat until the wave builds clean.

- [ ] **Step 2: Regenerate AsyncPagination extensions.** Inspect `Octokit.Generators/AsyncPaginationExtensionsGenerator.cs` to learn its run command, then run it so the generated extensions pick up the new `cancellationToken` parameter (the generator reads client method signatures). If the generator does not propagate the token, hand-thread it in `Octokit.AsyncPaginationExtension/Extensions.cs` following the same rule.
  Run: `dotnet run --project Octokit.Generators` (confirm exact args from the generator source).

- [ ] **Step 3: Full build.** `dotnet build Octokit.sln` — expect 0 errors across all TFMs.

- [ ] **Step 4: Unit + convention tests.**
  Run: `DOTNET_ROLL_FORWARD=Major dotnet test Octokit.Tests/Octokit.Tests.csproj -f net6.0`
  Run: `DOTNET_ROLL_FORWARD=Major dotnet test Octokit.Tests.Conventions/Octokit.Tests.Conventions.csproj -f net6.0`
  Expected: ≥4546 passed, 0 failed; conventions pass.

- [ ] **Step 5: Add forwarding tests** for a representative sample (one client per shape: paginated GET, single GET, POST, PUT, PATCH, DELETE) asserting the client forwards the token to a substituted `IApiConnection`. Example:

```csharp
[Fact]
public async Task GetAllWatchersForwardsCancellationToken()
{
    var cts = new CancellationTokenSource();
    var apiConnection = Substitute.For<IApiConnection>();
    var client = new WatchedClient(apiConnection);

    await client.GetAllWatchers("owner", "repo", ApiOptions.None, cts.Token);

    apiConnection.Received().GetAll<User>(Arg.Any<Uri>(), Arg.Any<ApiOptions>(), cts.Token);
}
```

- [ ] **Step 6: Final commit.** `git add -A && git commit -m "feat: add CancellationToken support across all clients"`

- [ ] **Step 7: Push fork + open upstream PR.** `git push -u origin worktree-cancellation-token-support`, then open a PR to `octokit/octokit.net` summarising the change (note acceptance is unlikely given repo dormancy; the fork is the primary target).
