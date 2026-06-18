# Comprehensive `CancellationToken` Support — Design

**Date:** 2026-06-18
**Branch:** `worktree-cancellation-token-support`
**Fork:** `hughesjs/octokit.net` (`origin`); upstream `octokit/octokit.net` is effectively unmaintained (no merged PR or human maintainer comment since Jan 2025).

## Goal

Surface `CancellationToken` across the entire public client API so callers can cancel any request. Today only ~77 of ~2000 public client methods accept a token (OAuth, Releases, Statistics, ActionsSelfHostedRunners). The low-level `Connection`/`IConnection` HTTP layer already supports cancellation; the gap is the middle layer and the client surface.

## Pattern (decided)

Follow the existing precedent set by PR #2988 (OAuth client): add

```csharp
CancellationToken cancellationToken = default
```

as a **trailing optional parameter, in-place** on existing methods (both interface and implementation). This is source-compatible for callers. No separate overloads. We do not change the established posture the codebase already adopted.

## Layering (verified)

- `IConnection`/`Connection` (bottom HTTP layer): **already** accepts `CancellationToken`. No change needed beyond confirming pass-through.
- `IApiConnection`/`ApiConnection` (middle layer, called by every client): **does not** accept `CancellationToken` on its generic `Get`/`Post`/`Put`/`Patch`/`Delete` methods. **This is the shared prerequisite.**
- Clients (public surface, hand-written): add the parameter and thread it to `ApiConnection`.
- `Octokit.Reactive` Observable clients: mirror the sync clients, accept the token, pass to the underlying client.
- `Octokit.AsyncPaginationExtension`: **generated** by `Octokit.Generators` — regenerate, do not hand-edit.

### Convention constraints

- `CheckObservableClients` enforces method-**name** parity between sync and Observable interfaces. We add **parameters**, not methods, so parity is preserved at all times — agent ordering is unconstrained by this test.
- Existing route/model conventions are unaffected (no methods or models added).

## Execution — three phases

### Phase 1 — Foundation (sequential, single commit, done before fan-out)

1. Add `CancellationToken cancellationToken = default` to `IApiConnection`/`ApiConnection` generic verbs, threading down to `Connection`.
2. Fix the `concellationToken` typo bug in `IOAuthClient.cs` (3 occurrences).
3. Build green (netstandard2.0 + net6.0).

Every client depends on this; it must compile before any client work begins.

### Phase 2 — Client fan-out (parallel agents, mixed model tier)

- **Work unit = one client** = its 4 disjoint files: `IXClient.cs`, `XClient.cs`, `IObservableXClient.cs`, `ObservableXClient.cs`.
- Files never overlap between units → all agents operate in **the single shared worktree** safely.
- **Model tier:** Haiku for simple clients (single/few methods, plain Get/Post); Sonnet for large/complex clients (Repositories, Issues, PullRequests, Actions, Git*). Triage by method count and call-site complexity first.
- Each agent, per assigned client: add the parameter to every public method, thread it into `ApiConnection`/underlying-client calls, mirror into the Observable client.
- **Agent exit gate:** whole clients only (never leave interface/impl mismatched); build + scoped convention tests green before reporting.
- ~184 clients batched (~10–15 per agent) across waves.

### Phase 3 — Integration (sequential)

1. Regenerate `Octokit.AsyncPaginationExtension` via `Octokit.Generators`.
2. Full build (netstandard2.0 + net6.0).
3. Run `Octokit.Tests` (baseline: 4546 passing) and `Octokit.Tests.Conventions`.
4. Add targeted unit tests asserting the token is forwarded to `ApiConnection`/`Connection` for a representative sample.
5. Commit.

## Verification

Baseline captured at branch start: build 0 errors; `Octokit.Tests` 4546 passed / 0 failed / 2 skipped (run with `DOTNET_ROLL_FORWARD=Major` because only .NET 8/10 runtimes are installed, not net6.0). Every phase must return to this green state.

## Out of scope

- GraphQL (separate library).
- Migrating to Kiota-generated SDK (evaluated and deferred; the fork retains octokit's ergonomic surface, typed exceptions, pagination, and caching).
- New endpoints / API coverage changes.

## Upstream

Open a PR to `octokit/octokit.net` after Phase 3 to "see if they bite". Acceptance is unlikely given the repo's dormancy; the fork is the primary beneficiary.
