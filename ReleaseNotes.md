### New in 0.29.0 (released 19/02/2018)

## Advisories and Breaking Changes

- On February 22, 2018 19:00 UTC, GitHub will [disable permanently the use of weak cryptogrpahic standards](https://developer.github.com/changes/2018-02-01-weak-crypto-removal-notice/).  Applications targeting .NET Framework 4.5.x will be affected, as that framework does not enable the now required protocol (TLS1.2) by default.  This octokit.net release will automatically enable this protocol, when the `GitHubClient` is constructed.  Note that applications targeting .NET Framework 4.6+ or .NET Core should already have TLS1.2 enabled by default, and this release does nothing with enabled protocols on those platforms.
- Affected clients that are unable to update octokit.net, can include their own [code change](https://github.com/octokit/octokit.net/blob/41b4059c110a60aee86a912dda2987fecc5a3fcb/Octokit/Http/HttpClientAdapter.cs#L31) to enable TLS1.2 as an alternative to updating to this release.

## Release Notes

### Milestone: Missing Pagination Support

**Features/Enhancements**

- Add pagination support to `ReferencesClient` - [#1694](https://github.com/octokit/octokit.net/pull/1694) via [@gdziadkiewicz](https://github.com/gdziadkiewicz)
- Add pagination support to `RepositoryInvitationsClient` - [#1692](https://github.com/octokit/octokit.net/pull/1692) via [@gdziadkiewicz](https://github.com/gdziadkiewicz)


### Milestone: None

**Features/Enhancements**

- Add `InReplyToId` property to `PullRequestReviewComment` response model, to indicate when a comment is in reply to another comment - [#1715](https://github.com/octokit/octokit.net/pull/1715) via [@thedillonb](https://github.com/thedillonb)
- Ensure the `netstandard1.1` targeted package is compatible with AWS Lambda `netcoreapp1.0` environment, by explicitly specifying the `NetStandard.Library` meta-package version - [#1713](https://github.com/octokit/octokit.net/pull/1713) via [@ryangribble](https://github.com/ryangribble)
- Add `UpdatedAt` property to `Milestone` response model, to indicate when it was last updated - [#1722](https://github.com/octokit/octokit.net/pull/1722) via [@shaggygi](https://github.com/shaggygi), [@ryangribble](https://github.com/ryangribble)
- Support `StatusEvent` payloads, using new response model `StatusEventPayload` - [#1732](https://github.com/octokit/octokit.net/pull/1732) via [@itaibh](https://github.com/itaibh)
- Octokit now handles `DateTime` and `DateTimeOffset` response fields whose API response is in an unexpected Unix epoch time format - [#1735](https://github.com/octokit/octokit.net/pull/1735) via [@itaibh](https://github.com/itaibh)
- Add `PullRequestReviewId ` property to `PullRequestReviewComment` response model, to indicate which `PullRequestReview` the comment is related to - [#1739](https://github.com/octokit/octokit.net/pull/1739) via [@mirsaeedi](https://github.com/mirsaeedi)
- Implement support for Repository Licenses, including adding `License` property to `Repository` response model, adding `SpxId` field to `LicenseMetadata` response model and a new `IRepositoriesClient.GetLicenseContents()` call - [#1630](https://github.com/octokit/octokit.net/pull/1630) via [@jozefizso](https://github.com/jozefizso), [@M-Zuber](https://github.com/M-Zuber), [@ryangribble](https://github.com/ryangribble)
- Add `MergeableState` property to `PullRequest` response model, to indicate additional information about why a pull request can't be merged - [#1764](https://github.com/octokit/octokit.net/pull/1764) via [@ryangribble](https://github.com/ryangribble)
- Add `Visibility` property to `EmailAddress` response model, to indicate whether a primary email address is `Public` or `Private` - [#1757](https://github.com/octokit/octokit.net/pull/1757) via [@asapferg](https://github.com/asapferg), [@ryangribble](https://github.com/ryangribble)

**Fixes**

- `OAuthClient` now handles GitHub Enterprise instances correctly in `CreateAccessToken()` and `GetGitHubLoginUrl()` methods - [#1726](https://github.com/octokit/octokit.net/pull/1726) via [@ryangribble](https://github.com/ryangribble)
- Using the same `GitHubClient` instance from multiple threads in parallel will no longer throw occasional exceptions, after making the `GitHubSerializerStrategy` internals thread-safe - [#1748](https://github.com/octokit/octokit.net/pull/1748) via [@daveaglick](https://github.com/daveaglick)
- Remove deserializer enum cache miss by correcting the case of `AccountType` parameter values - [#1759](https://github.com/octokit/octokit.net/pull/1759) via [@ryangribble](https://github.com/ryangribble)
- Add TLS1.2 to enabled security protocols (.NET Framework 4.5 only) to avoid SSL connectivity errors when [GitHub deprecates weak algorithms](https://developer.github.com/changes/2018-02-01-weak-crypto-removal-notice/) on February 22 - [#1758](https://github.com/octokit/octokit.net/pull/1758) via [@ryangribble](https://github.com/ryangribble)
- Deserializer now handles nullable `StringEnum<T>` members - [#1760](https://github.com/octokit/octokit.net/pull/1760) via [@ryangribble](https://github.com/ryangribble)

**Documentation Updates**

- Updated Rate Limits documentation and samples - [#1742](https://github.com/octokit/octokit.net/pull/1742) via [@mirsaeedi](https://github.com/mirsaeedi)
- Update supported platforms in README.md to include `.NET Standard 1.1` - [#1744](https://github.com/octokit/octokit.net/pull/1744) via [@ShalokShalom](https://github.com/ShalokShalom), [@ryangribble](https://github.com/ryangribble)
- Clarified `ProductHeaderValue` usage to align with GitHub API Docs - [#1751](https://github.com/octokit/octokit.net/pull/1751) via [@IAmHughes](https://github.com/IAmHughes), [@ryangribble](https://github.com/ryangribble)

### New in 0.28.0 (released 6/11/2017)

## Advisories and Breaking Changes

- This release has been pushed out in response to `CommitStatus.Id` on GitHub exceeding `Int32.MaxValue`.  We've made this field a `long` now... sorry about that!

## Release Notes

**Features/Enhancements**

- You can now use `IGitHubClient.SetRequestTimeout(TimeSpan timeout)` to set a custom timeout, particularly useful for lengthy operations such as uploading release assets. - [#1693](https://github.com/octokit/octokit.net/pull/1693) via [@pmiossec](https://github.com/pmiossec), [@ryangribble](https://github.com/ryangribble)

**Fixes**

- Update `CommitStatus.Id` field from `int` to `long` to prevent overflow exceptions - [#1703](https://github.com/octokit/octokit.net/pull/1703) via [@kzu](https://github.com/kzu)

**Housekeeping**

- Correct rendering of `HttpClient` documentation page - [#1699](https://github.com/octokit/octokit.net/pull/1699) via [@scovetta](https://github.com/scovetta)

### New in 0.27.0 (released 7/10/2017)

## Advisories and Breaking Changes

- `NewTeam.Permission` has been changed to a nullable type `Permission?` and will no longer be sent unless explicitly set

## Release Notes

**Features/Enhancements**

- Implement additional fields in `Team` response and `NewTeam` and `UpdateTeam` requests, for `Privacy`, `Maintainers` and `Description` where they were missing - [#1669](https://github.com/octokit/octokit.net/pull/1669) via [@ryangribble](https://github.com/ryangribble)
- Implement `Organization` filter in `ISearchClient.SearchCode()` - [#1672](https://github.com/octokit/octokit.net/pull/1672) via [@sepharg](https://github.com/sepharg), [@ryangribble](https://github.com/ryangribble)
- Implement team membership enhancements for role (Maintainer or Member) and state (Active or Pending) including new methods `TeamsClient.AddOrEditMembership()` and `TeamsClient.GetMembershipDetails()` and updating `TeamsClient.GetAllMembers()` to allow filtering by role. - [#1670](https://github.com/octokit/octokit.net/pull/1670) via [@ryangribble](https://github.com/ryangribble)
- Implement [Nested Teams API (Preview)](https://developer.github.com/changes/2017-08-30-preview-nested-teams/) - [#1682](https://github.com/octokit/octokit.net/pull/1682) via [@ryangribble](https://github.com/ryangribble)

**Fixes**

- Assembly versioning, NuGet package metadata and inter-package version dependencies should now be correct, after automating them via the build process - [#1660](https://github.com/octokit/octokit.net/pull/1660) via [@ryangribble](https://github.com/ryangribble), [@mderriey](https://github.com/mderriey)
- Octokit can now run in environments where `PlatformNotSupported` exception was encountered when initializing the API connection (eg AWS Lambda) - [#1660](https://github.com/octokit/octokit.net/pull/1660) via [@ryangribble](https://github.com/ryangribble), [@mderriey](https://github.com/mderriey)
- Intellisense should once again be available for Octokit libraries - sorry about that! - [#1674](https://github.com/octokit/octokit.net/pull/1674) via [@mderriey](https://github.com/mderriey)
- `ISearchClient.SearchRepo()` now uses the correct values for the `Forks` search qualifiers (`Include` or `Only`) - [#1680](https://github.com/octokit/octokit.net/pull/1680) via [@ryangribble](https://github.com/ryangribble)

**Housekeeping**

- Add convention tests to enforce API Pagination support and naming conventions - [#1659](https://github.com/octokit/octokit.net/pull/1659) via [@ryangribble](https://github.com/ryangribble)
- BranchProtection response class `EnforceAdmins` now provides a standard `ctor` allowing it to be mocked if required - [#1679](https://github.com/octokit/octokit.net/pull/1679) via [@ryangribble](https://github.com/ryangribble)


### New in 0.26.0 (released 31/8/2017)

## Advisories and Breaking Changes

- This release contains the necessary Octokit changes to specify the `required_pull_request_reviews` field on Branch Protection updates, which becomes mandatory when the Protected Branches API [graduates from preview mode](https://developer.github.com/changes/2017-06-16-loki-preview-ending-soon/) on the 1st September

## Release Notes

**Features/Enhancements**

- Implement `RequiredPullRequestReviews` support in `IRepositoryBranchesClient.UpdateBranchProtection` and additional granular methods to `GetReviewEnforcement`, `UpdateReviewEnforcement` and `RemoveReviewEnforcement` via [@M-Zuber](https://github.com/M-Zuber), [@ryangribble](https://github.com/ryangribble)

### New in 0.25.0 (released 23/8/2017)

## Advisories and Breaking Changes

- Octokit.net has been ported to dotnetcore :tada: providing libraries targetting `netstandard1.1` and `net45` frameworks

- `Enum` fields in Octokit response classes are now wrapped in an `StringEnum<TEnum>` helper class, to provide more robustness in dealing with unknown API values for these fields.  Whilst the changes are backwards compatible, please consult the guidance on [working with Enums](https://github.com/octokit/octokit.net/blob/master/docs/working-with-enums.md) for more information

- `IncludeAdmins` field is no longer present in `BranchProtectionRequiredStatusChecks` and `BranchProtectionRequiredStatusChecksUpdate` classes, instead use the new `EnforceAdmins` field on `BranchProtectionSettingsUpdate` or the [new explicit methods](https://github.com/octokit/octokit.net/blob/master/Octokit/Clients/IRepositoryBranchesClient.cs#L304-L365) for configuring Admin Enforcement on protected branches.  This was an [upstream API breaking change](https://developer.github.com/changes/2017-05-02-adoption-of-admin-enforced/) so we couldn't follow our normal deprecation schedule

## Release Notes

### Milestone: CAKE Builds

**Features/Enhancements**

- Add a build task to validate LINQPad samples - [#1551](https://github.com/octokit/octokit.net/pull/1551) via [@mderriey](https://github.com/mderriey)
- Add a code formatting task to CAKE - [#1550](https://github.com/octokit/octokit.net/pull/1550) via [@mderriey](https://github.com/mderriey)
- Add GitVersion configuration file - [#1555](https://github.com/octokit/octokit.net/pull/1555) via [@mderriey](https://github.com/mderriey)


### Milestone: dotnetcore Support

**Features/Enhancements**

- Port to .NET Core - [#1503](https://github.com/octokit/octokit.net/pull/1503) via [@mderriey](https://github.com/mderriey), [@ryangribble](https://github.com/ryangribble)
- Remove unneeded files for .NET Core - [#1549](https://github.com/octokit/octokit.net/pull/1549) via [@mderriey](https://github.com/mderriey)
- Migrate dotnetcore to vs2017 tooling - [#1567](https://github.com/octokit/octokit.net/pull/1567) via [@ryangribble](https://github.com/ryangribble), [@mderriey](https://github.com/mderriey)
- Provide [SourceLink](https://github.com/ctaggart/SourceLink) capability for Octokit and Octokit.Reactive assemblies - [#1574](https://github.com/octokit/octokit.net/pull/1574) via [@ryangribble](https://github.com/ryangribble), [@mderriey](https://github.com/mderriey)
- Deliver the dotnetcore port and CAKE build framework changes - [#1581](https://github.com/octokit/octokit.net/pull/1581) via [@ryangribble](https://github.com/ryangribble), [@mderriey](https://github.com/mderriey)

**Fixes**

- Fix broken JSON deserialization in .NET 4.5 after VS2017 project update - [#1647](https://github.com/octokit/octokit.net/pull/1647) via [@mderriey](https://github.com/mderriey)


### Milestone: None

**Features/Enhancements**

- Add support for the newly resurrected `PullRequest.MergeCommitSha` property - [#1562](https://github.com/octokit/octokit.net/pull/1562) via [@alexperovich](https://github.com/alexperovich)
- Enhance `RepositoryBranchesClient` to support Admin Enforcement changes - [#1598](https://github.com/octokit/octokit.net/pull/1598) via [@M-Zuber](https://github.com/M-Zuber)
- Implement [Pull Request Review Requests API (Preview)](https://developer.github.com/v3/pulls/review_requests/) - [#1588](https://github.com/octokit/octokit.net/pull/1588) via [@gdziadkiewicz](https://github.com/gdziadkiewicz), [@ryangribble](https://github.com/ryangribble)
- Provide a robust way to handle unknown enum values returned by GitHub API, to prevent deserialization errors until the enum values can be added to octokit - [#1595](https://github.com/octokit/octokit.net/pull/1595) via [@khellang](https://github.com/khellang), [@ryangribble](https://github.com/ryangribble)
- Implement [Projects API (Preview)](https://developer.github.com/v3/projects/) - [#1480](https://github.com/octokit/octokit.net/pull/1480) via [@maddin2016](https://github.com/maddin2016), [@ryangribble](https://github.com/ryangribble)
- Implement `ReviewPermission()` functionality for `OrganizationMembersClient` (Preview API) - [#1633](https://github.com/octokit/octokit.net/pull/1633) via [@alfhenrik](https://github.com/alfhenrik)
- Implement [Organization OutsideCollaborators API (Preview)](https://developer.github.com/v3/orgs/outside_collaborators/) - [#1639](https://github.com/octokit/octokit.net/pull/1639) via [@alfhenrik](https://github.com/alfhenrik), [@ryangribble](https://github.com/ryangribble)
- Implement pagination support for `OrganizationOutsideCollaboratorsClient.GetAll()` method - [#1650](https://github.com/octokit/octokit.net/pull/1650) via [@ryangribble](https://github.com/ryangribble)
- Implement `GetAllPendingInvitations()` functionality for `OrganizationMembersClient` and `TeamsClient` (Preview API) - [#1640](https://github.com/octokit/octokit.net/pull/1640) via [@alfhenrik](https://github.com/alfhenrik), [@ryangribble](https://github.com/ryangribble)
- Implement [Pull Request Reviews API](https://developer.github.com/v3/pulls/reviews/) - [#1648](https://github.com/octokit/octokit.net/pull/1648) via [@hartra344](https://github.com/hartra344), [@ryangribble](https://github.com/ryangribble)

**Fixes**

- Fix `RepositoryTrafficClient` to handle upstream API change in timestamps from Unix epoch time to ISO8601 - [#1560](https://github.com/octokit/octokit.net/pull/1560) via [@mderriey](https://github.com/mderriey), [@ryangribble](https://github.com/ryangribble)
- Fix more `IssueTimelineClient` deserialization exceptions by adding more new `EventInfoState` values - [#1563](https://github.com/octokit/octokit.net/pull/1563) via [@ryangribble](https://github.com/ryangribble)
- Fix `NotificationsClient.MarkAsRead()` exception by specifying a payload body in the `PUT` request - [#1579](https://github.com/octokit/octokit.net/pull/1579) via [@shiftkey](https://github.com/shiftkey), [@ryangribble](https://github.com/ryangribble)
- Fix `connection.GetLastApiInfo()` was returning `null` in some situations - [#1580](https://github.com/octokit/octokit.net/pull/1580) via [@ryangribble](https://github.com/ryangribble)
- Fix even more `IssueTimelineClient` deserialization exceptions by adding even more new `EventInfoState` values (this is getting old!) - [#1591](https://github.com/octokit/octokit.net/pull/1591) via [@lynnfaraday](https://github.com/lynnfaraday), [@ryangribble](https://github.com/ryangribble)
- `NewRepositoryWebHook.ToRequest()` no longer discards existing fields if they are set - [#1623](https://github.com/octokit/octokit.net/pull/1623) via [@ctolkien](https://github.com/ctolkien)
- Fix pagination on API calls that use `Uri` parameters (typically for requests that include some form of filtering) - [#1649](https://github.com/octokit/octokit.net/pull/1649) via [@ryangribble](https://github.com/ryangribble)
- Fixed `RepositoryCommitsClient.GetSha1()` to correctly obtain the sha1 of the specified commit, after the API went from preview to official - [#1654](https://github.com/octokit/octokit.net/pull/1654) via [@ryangribble](https://github.com/ryangribble)

**Housekeeping**

- Remove obsolete constructor of `RepositoryUpdate` request class - [#1569](https://github.com/octokit/octokit.net/pull/1569) via [@eriawan](https://github.com/eriawan)
- Remove unused Rx-Main dependency from LINQPad samples - [#1593](https://github.com/octokit/octokit.net/pull/1593) via [@NickCraver](https://github.com/NickCraver)
- Change response models 'Url' properties from `Uri` to `string` - [#1585](https://github.com/octokit/octokit.net/pull/1585) via [@mderriey](https://github.com/mderriey)
- Remove obsolete branch protection methods/classes - [#1620](https://github.com/octokit/octokit.net/pull/1620) via [@ryangribble](https://github.com/ryangribble)
- Remove methods and members that were marked `[Obsolete]` in 0.23 or earlier - [#1622](https://github.com/octokit/octokit.net/pull/1622) via [@ryangribble](https://github.com/ryangribble)

**Documentation Updates**

- Fix `Issue` documentation samples (`GetForRepository()` should be `GetForRepository()`) - [#1602](https://github.com/octokit/octokit.net/pull/1602) via [@tnaoto](https://github.com/tnaoto)
- Fix `Release` documentation samples (`ReleaseUpdate` should be `NewRelease`) - [#1611](https://github.com/octokit/octokit.net/pull/1611) via [@watsonlu](https://github.com/watsonlu)

### New in 0.24.0 (released 17/1/2017)

**Features/Enhancements**

 - Add `GetAll` method to `OrganizationsClient` - [#1469](https://github.com/octokit/octokit.net/pull/1469) via [malamour-work](https://github.com/malamour-work)
 - Add missing fields to `Repository` class - `HasPages`, `SubscribersCount`, `Size` - [#1473](https://github.com/octokit/octokit.net/pull/1473) via [ryangribble](https://github.com/ryangribble)
 - Allow base64 content for create/update file - [#1488](https://github.com/octokit/octokit.net/pull/1488) via [laedit](https://github.com/laedit)
 - Add `HtmlUrl` field to `Milestone` class - [#1489](https://github.com/octokit/octokit.net/pull/1489) via [StanleyGoldman](https://github.com/StanleyGoldman)
 - Add support for passing sort options to `IssueCommentsClient.GetAllForRepository()` - [#1501](https://github.com/octokit/octokit.net/pull/1501) via [pjc0247](https://github.com/pjc0247)
 - Rename `PullRequest.Comment` to `PullRequest.ReviewComment` for better accuracy - [#1520](https://github.com/octokit/octokit.net/pull/1520) via [bmeverett](https://github.com/bmeverett)
 - Introduce `AbuseException` - [#1528](https://github.com/octokit/octokit.net/pull/1528) via [SeanKilleen](https://github.com/SeanKilleen)
 - Add `Id` field to `PullRequest` class - [#1537](https://github.com/octokit/octokit.net/pull/1537) via [YunLi1988](https://github.com/YunLi1988)
 - Unparseable `ApiErrors` should now fall back to better default error messages - [#1540](https://github.com/octokit/octokit.net/pull/1540) via [SeanKilleen](https://github.com/SeanKilleen)

**Fixes**

 - Fix errors in `ObservableEventsClient` caused by incorrect return types - [#1490](https://github.com/octokit/octokit.net/pull/1490) via [StanleyGoldman](https://github.com/StanleyGoldman)
 - Add missing `SecurityCritical` attribute on `GetObjectData()` overrides - [#1493](https://github.com/octokit/octokit.net/pull/1493) via [M-Zuber](https://github.com/M-Zuber)
 - Fix exceptions in Events API by adding missing event types to `EventInfo` enumeration - [#1536](https://github.com/octokit/octokit.net/pull/1536) via [lynnfaraday](https://github.com/lynnfaraday)
 - Add new AccountType "Bot" to prevent deserialization errors - [#1541](https://github.com/octokit/octokit.net/pull/1541) via [ryangribble](https://github.com/ryangribble)

**Documentation Updates**

 - Clarify `ApiInfo` rate limiting usage in docs - [#1524](https://github.com/octokit/octokit.net/pull/1524) via [SeanKilleen](https://github.com/SeanKilleen)
 - Clarify label coloring usage in docs - [#1530](https://github.com/octokit/octokit.net/pull/1530) via [SeanKilleen](https://github.com/SeanKilleen)

**Breaking Changes**

 - Creating and Editing Issues (and PullRequests) using `NewIssue` and `IssueUpdate` requests 
should now use the `Assignees` collection rather than the now deprecated 'Assignee` field. 
Both fields can't be specified on the same request, so any code still using `Assignee` will 
need to explicitly set `Assignees` to `null` to avoid Api validation errors.

 - `OrganizationsClient.GetAll(string user)` has been marked obsolete in favour of 
`OrganizationsClient.GetAllForUser(string user)`

 - `PullRequest.Comment` has been marked obsolete in favour of `PullRequest.ReviewComment`

- Several `EventsClient` methods previously returned the incorrect `Activity` response class. 
This has been corrected to `IssueEvent` which although is now correct could break calling 
code that was written assuming this previous incorrect return type.

### New in 0.23.0 (released 07/10/2016)

**Features**

 - Added support to test whether a URL points to a GitHub Enterprise instance - #1404 via @haacked
 - Added granular methods for Protected Branches preview API - #1443 via @maddin2016
 - Repository Traffic preview API support - #1457 via @maddin2016
 - Preview API for merge/squash/rebase in repository settings - #1477 via @ryangribble
 - Added support for performing a rebase and merge through the API- #1479 via @ryangribble

**Fixes**

 - Repository identifiers now use `long` instead of `int` - #1445 via @shana, #1485 via @ryangribble
 - Searching for C# through the GitHub API now uses the correct alias - #1463 via @dampir
 - Resolved deadlocking scenario in async/await usage - #1486 via @zzzprojects

**Other**

 - LINQPad samples are now verified at build time - #1456 via @mderriey
 - More obsolete APIs removed - #1458 via @ryangribble
 - .NET Core support has been started - #1462 via @mderriey

**Breaking Changes**

Repository identifiers returned from the GitHub API will exceed `Int32.MaxValue` in
around 12 months, based on current growth. We've decided to update everywhere we
require (or return) a repository identifier from `int` to `long` so that these will
continue to work in the future, and the implicit conversion from `int` to `long`
means the impact should be manageable now.

`MergePullRequest.Squash` has been marked as obsolete in favour of the `MergeMethod`
property - use `PullRequestMergeMethod.Squash` or `PullRequestMergeMethod.Rebase` if
you want to change the merge behaviour when merging a pull request.

### New in 0.22.0 (released 2016/09/01)

**Features**

 - Timeline preview API support - #1435 via @alfhenrik
 - Initial groundwork for Branches API - #1437 via @ryangribble
 - Base branch can now be updated when updating a pull request - #1450 via @ryangribble
 - Enhancements to Protected Branches preview API - #1441 via @ryangribble

**Fixes**

 - Redirect timeout when repository renamed - #1411 via @maddin2016

**Breaking Changes**

The new Branches client added in #1437 means that existing methods on 
I(Observable)RepositoryClient are now marked as obsolete. Please update your
usages to the new endpoints as these will be removed in a future release:

 - `client.Repository.GetBranch()` => `client.Repository.Branch.Get()`
 - `client.Repository.GetAllBranches()` => `client.Repository.Branch.GetAll()`
 - `client.Repository.EditBranch()` => `client.Repository.Branch.Edit()`

There is also a change in how branch protection works with the API, due to 
upstream changes. The existing methods have been marked as obsolete, but for
the sake of brevity here are the details about what you should be doing today.

The process for inspecting branch protection is now two steps:

 - first, check the branch returned by `client.Repository.Branch.Get()` or 
   `client.Repository.Branch.GetAll()` has it's `Protected` property set to `true`.

 - then, a call to `client.Repository.Branch.GetBranchProtection()` will return
   the details about the protection settings for the given branch. If no protection
   is set for this branch, you will received a `HTTP 404` response.

### New in 0.21.1 (released 2016/07/29)

**Features**

Due to a programming error in the tool to generate these release notes, additional
features were not properly documented for the previous release:

 - Reactions preview API support for issues, issue comments, commit comments and PR comments - #1335, #1341, #1405 via @maddin2016, @alfhenrik 
 - Repository Invitation preview API support - #1410 via @maddin2016
 - Added new fields for signature verification to Git Data Commit API - #1398 via @Sarmad93

No additional code changes have been made to this release.

### New in 0.21.0 (released 2016/07/29)

**Features**

This release adds support across Octokit.net for providing the repository Id
rather than a name/owner pair. The repository Id does not change when transferring
ownership of a repository, and is more robust for API callers. This work
was lead by @dampir as part of Google Summer of Code 2016.

 - Added new fields for Deployment and DeploymentStatus preview API - #1365 via @ErikSchierboom
 - Added new fields for signature verification to Git Data Tag API - #1420 via @Sarmad93
 - Added new fields for GitHub Pages preview API - #1421 via @dampir

**Fixes**

 - Fix serialization of enum value attributes - #1402 via @maddin2016
 - Fix searching for repositories with underscore in name - #1418 via @dsplaisted, @shiftkey

**Other**

 - Clarified obsolete warnings for Protected Branch preview API - #1428 via @ryangribble
 - Remove Obsolete items - #1422 via @ryangribble

**Breaking Changes**

After a long grace period, #1422 has removed these obsoleted members. These features
exist in other parts of the API surface:

 - `I(Observable)GitHubClient.Release`
 - `I(Observable)GitHubClient.Notification`
 - `I(Observable)GitHubClient.GitDatabase`
 - `I(Observable)GitHubClient.SshKey`
 - `I(Observable)GitHubClient.Repository.RepositoryComments`
 - `I(Observable)GitHubClient.Repository.CommitStatus`
 - `I(Observable)GitHubClient.Repository.RepoCollaborators`
 - `I(Observable)GitHubClient.Repository.Commits`

This method is no longer supported through the API and has been removed from Octokit.net.: 

 - `I(Observable)GitHubClient.Authorization.RevokeAllApplicationAuthentications()`

### New in 0.20.0 (released 2016/06/15)

**Features**

The big focus for this release is pagination support. This lets the caller
control how much data to retrieve for `GetAll*` endpoints throughout Octokit.
This was a team effort to apply this across the entire codebase, with
contributions from @dampir, @devkhan, @prayankmathur, @SamTheDev and @shiftkey.

For more information about how to use pagination in your projects refer to the
documentation: http://octokitnet.readthedocs.io/en/latest/extensibility/#pagination

 - Add Migrations preview API - #1141 via @devkhan
 - Add Issue Lock/Unlock functionality - #1185 via @prayankmathur
 - Added Commit Reference SHA-1 API - #1195 via @ryangribble
 - Add additional parameters to `SearchIssuesRequest` -  #1228 via @ryangribble
 - Add `Importer` property to Meta endpoint - #1235 via @ryangribble
 - Raise HTTP 451 exception when repository has DMCA notice - #1239 via @devkhan
 - Add Merge and Squash preview API - #1245 via @Sarmad93
 - Add additional methods to `IEventsClient` - #1288 via @drasticactions
 - Add Organization Permissions preview API - #1342 via @ryangribble
 - Add GPG Keys preview API - #1343 via @alfhenrik
 
**Fixes**

 - Renamed `IUserKeysClient.GetAll()` to `IUserKeysClient.GetAllForCurrent()` - #1139 via @M-Zuber
 - Add `ItemStateFilter` enum to differentiate between search and list endpoints - #1140 via @prayankmathur
 - `RepositoriesClient.GetAllPublic()` fails for Enterprise instanes due to URI structure - #1204 via @ryangribble 
 - `ConfigureAwait(false)` usages added, eliminating deadlocks - #1248 via @shiftkey
 - Renamed `CompareResult.MergedBaseCommit` to fix serialization issue - #1265 via @kivancmuslu
 - Activity Feed now returns issues and repository events - #1288 via @drasticactions
 - Add `Repository` property to `Issue` response - #1292 via @M-Zuber
 - `SearchCodeRequest` now supports searching without specifying a term - #1338 via @dsplaisted
 - Add required Permission parameter to team management APIs - #1347 via @ryangribble
 - Add `ClosedBy` property to `Issue` - #1353 via @maddin2016

**Other**
 
 - Deleting now-obsolete code - #1224 via @M-Zuber
 - Centralize and cleanup the `Uri`s created in Octokit - #1287, #1290 via @dampir
 - Updated documentation links - #1289 via @radu-matei, #1250 via @SamTheDev 
 
**Breaking Changes**

 - `IUserKeysClient.GetAll()` was named incorrectly when it was originally implemented
   and only works for the current user's keys. Update all usages to `GetAllForCurrent()`.

 - `CompareResult.MergedBaseCommit` was never deserialized correctly, and has
   been marked as obsolete. You should use `CompareResult.MergeBaseCommit`
   instead (note the lack of a `d`).

 - `IEventsClient.GetAllForRepository` was incorrectly retrieving issue
    events before this release. Use the new `IEventsClient.GetAllIssuesForRepository`
    method if you still require issues, or continue to use `IEventsClient.GetAllForRepository`
    if you require all repository events.

 - `IUsersClient` has a property named `Keys` which has been renamed in the
   GitHub API documentation - Octokit has added the name `GitSshKey` to
   reflect this change, and `Keys` will be removed in a later release.

### New in 0.19.0 (released 2016/03/11)

**Features**

 - Add `GetLatest` endpoint for Releases API - #975 via @chenjiaming93
 - Add Enterprise License and Organization APIs - #1073 via @ryangribble
 - Add Locked property to `PullRequest` - #1089 via @M-Zuber
 - Add Enterprise Search Indexing API - #1095 via @ryangribble
 - Add support for `Visibility` and `Affiliation` to repository search - #1096, #1132 via  @Sarmad93, @AlexP11223
 - Add Enterprise LDAP API - #1099 via @ryangribble
 - Add `CreateBranch` extension methods to IReferencesClient - #1103 via @M-Zuber
 - Additional Enterprise methods on User Administration Client - #1108  via @ryangribble
 - Complete `UserKeysClient` API - #1112 via @ryangribble
 - `RepositoryContentsClient` create, update and delete actions now specify branch - #1093 via @M-Zuber

**Fixes**

 - `StatisticsClient` should not clobber /api/v3/ in path - #1085 via @shiftkey
 - Fix JSON deserialization of string containing hyphens to List<string> property - #1094 via @ryangribble
 - Incorrect reference passed to `RepositoryContentsClient.GetArchive` - #1113 via @michael-kokorin

**Other**

 - Add failing integration test for Issue Search API - #1083 via @hahmed
 - Add integration tests for `IReleasesClient.GetLatest` - #1090 via @M-Zuber
 - Remove extraneous Bcl .targets reference - #1100 via @shana
 - Add proper syntax highlighting to exploring-pull-requests.md -  #1117 via @tiesmaster
 - Fix issue with optional parameters in .\script\configure-integration-tests - #1118 via @Anubhav10
 - Update Issue creation sample code - #1131 via @AlexP11223
 - `IJsonSerializer` not used inside `Connection` - #1133 via @devkhan

**Breaking Changes**

`ISshKeysClient` has a number of methods which at the time should have been
implemented in `IUserKeysClient` - these methods are marked as obsolete and will
be removed in a future release:

 - `ISshKeysClient.Get(int id)`
 - `ISshKeysClient.GetAll(string user)`
 - `ISshKeysClient.GetAllForCurrent()`
 - `ISshKeysClient.Create(SshKeyUpdate key)`
 - `ISshKeysClient.Update(int id, SshKeyUpdate key)`
 - `ISshKeysClient.Delete(int id)`

### New in 0.18.0 (released 2016/02/03)

* New: support for User Administration API (GitHub Enterprise) - #1068 via @paladique
* New: support for Admin Stats API (GitHub Enterprise) - - #1049 via @ryangribble
* New: support for Repository Pages API - #1061 via @M-Zuber
* New: get stargazer creation timestamps - #1060 via @daveaglick
* New: support for Protected Branches API - #996 via @ryangribble
* New: support for creating Personal Access Tokens - #990 via @alfhenrik
* Fixed: `Milestone` property added to `PullRequest` response - #1075 via @Eilon
* Fixed: Add member role filter to `OrganizationMembersClient.GetAll()` - #1072 via @ryangribble
* Fixed: `Repository.Content.GetAllContents` now support the root of the repository - #1064 via @naveensrinivasan, @shiftkey
* Fixed: added `Id` and `Locked` to `Issue`, added `CommitUrl` to `IssueEvent` - #1039 via @gabrielweyer
* Fixed: additional fields on `Release` and `ReleaseAsset` - #1009 via @gabrielweyer
* Fixed: `ApiException` now includes JSON payload when `.ToString()`- #974 via @asizikov

**Breaking Changes:**

As part of reaching 1.0 we went through to audit the current implementation
and identify areas that didn't align with our conventions. For this release,
we're marking the endpoints as `[Obsolete]` and indicating the new location.
These will be cleaned up in the next release:

 - `IGitHubClient.Notifications` -> `IGitHubClient.Activity.Notifications` - #1019 via @M-Zuber
 - `IGitHubClient.Repository.CommitStatus` -> `IGitHubClient.Repository.Status` - #1043 via @RobPethick
 - `IGitHubClient.Repository.Commits` -> `IGitHubClient.Repository.Commit` - #1057 via @M-Zuber
 - `IGitHubClient.Repository.RepoCollaborators` -> `IGitHubClient.Repository.Collaborator` - #1040 via @M-Zuber
 - `IGitHubClient.Repository.RepositoryComments` -> `IGitHubClient.Repository.Comment` - #1044 via @M-Zuber
 - `IGitHubClient.Release` -> `IGitHubClient.Repository.Release` - #1058 via @RobPethick
 - `IGitHubClient.GitDatabase` -> `IGitHubClient.Git` - #1048 via @RobPethick

Other breaking changes:

 - a public `ApiExtensions.Get<T>` extension method was causing a bunch of
   tests to be written in a confusing way. This has been ported to an interface
   method on `IApiConnection` but hopefully you're not referencing this method
   externally - see #1063 for more information.

 - `IRepositoryContentsClient.GetArchiveLink` is no longer correct, as the HTTP
   behaviour in Octokit was updated to follow redirects received from the server.
   See #986 for the last bits of cleanup.

 - `IRepositoryContentsClient.GetAllContents(string owner, string name, string path, string reference)`
   has been renamed to `GetAllContentsByRef(string owner, string name, string path, string reference)`
   to prevent overlap with methods on `IRepositoryContentsClient` which do not
   specify a path - and thus look at the root of the repository.

 - `IssueEventPayload` has two fields which are never populated from the API -
   `Assignee` and `Label` - these are now removed. You should use
   `Issue.Assignee` and `Issue.Labels` instead. See #1039 for more details.

 - `PullRequest.MergeCommitSha` is marked as obsolete by the GitHub API - we
    are cleaning up the behaviour for determining whether a PR has been
    merged in #997 - see the PR for more information.

 - `IAuthorizationsClient.RevokeAllApplicationAuthentications` is no longer
   available through the GitHub API - this will be removed in the next
   release.

**Shout outs**

A lot of extra work went into this release, and I wanted to thank those people
who helped out - without their efforts we wouldn't be at this point:

 - @naveensrinivasan - for helping set up our Travis CI builds to test this on
   Mono - see #995 for the details
 - @hahmed - for contributing a bunch of documentation around the Octokit search
   APIs - see #955, #954 and #951
 - @JakesCode - for clarifying some documentation after he reported an issue - #1054
 - @ryangribble - for helping get our GitHub Enterprise testing off the ground - #987
 - @naveensrinivasan - for catching and addressing an issue with our LINQPad snippets - #987

### New in 0.17.0 (released 2015/12/07)

* New: `NewRepositoryWebHook` helper class useful for creating web hooks - #917 via @alfhenrik
* New: Overloads to the `GetArchive` method of `RepositoryContentsClient` that accept a timeout - #918 via @willsb
* Improved: Added `EventsUrl` to `Issue` - #901 via @alfhenrik
* Improved: Added `Committer` and `Author` to the `GitHubCommit` object - #903 via @willsb
* Improved: Made `EncodedContent` property of `RepositoryContent` public - #861 via @naveensrinivasan
* Improved: Added ability to create deploy keys that are read only and can only be used to read repository contents and not write to them - #915 via @haacked
* Improved: Added `Content` property to `NewTreeItem` to allow specifying content for a tree - #915 via @haacked
* Improved: Added `Description` property to `NewTeam` to allow specifying a description for a team - #915 via @haacked
* Improved: Added `Description` property to `OrganizationUpdate` to allow specifying a description for an organization - #915 via @haacked
* Improved: Added `Before` property to `NotificationsRequest` to find notifications updated before a specific time - #915 via @haacked
* Improved: Renamed `SignatureResponse` to `Committer` and replaced `CommitEntity` with `Committer` - #916 via @haacked
* Improved: Added URLs with more information to the `PrivateRepositoryQuotaExceededException` - #929 via @elbaloo
* Improved: The `Merge` method of `PullRequestsClient` now throws more specific exceptions when pull request is not mergeable - #976 via @elbaloo and @shiftkey
* Fixed: Bug that prevented specifying a commit message for pull request merges - #915 via @haacked
* Fixed: Added `System` to required framework assemblies for the `net45` NuGet package - #919 via @adamralph
* Fixed: Change the `HasIssues` property of `NewRepository` to be a nullable boolean because it's optional - #942 via @alfhenrik
* Fixed: Bug that caused downloading release assets to fail because it didn't handle the `application/octet-stream` content type properly - #943 via @naveensrinivasan
* Fixed: JSON serialization bug with unicode characters - #972 via @naveensrinivasan

**Breaking Changes:**
 - `NewDeployment` constructor requires a ref as this is required for the API. It no longer has a default constructor.
 - `NewDeploymentStatus` constructor requires a `DeploymentState` as this is required for the API. It no longer has a default constructor.
 - The `Name` property of `NewTeam` is now read only. It is specified via the constructor.
 - Renamed `SignatureResponse` to `Committer` and removes `CommitEntity`, replacing it with `Committer`.
 - Changed the type of `HasIssues` property of `NewRepository` to be nullable.

### New in 0.16.0 (released 2015/09/17)

* New: Implemented `GetMetadata` method of `IMiscellaneousClient` to retrieve information from the Meta endpoint -#892 via @haacked
* Improved: Add missing `ClosedAt` property to `Milestone` response - #890 via @geek0r
* Fixed: `NullReferenceException` when retrieving contributors for an empty repository - #897 via @adamralph
* Fixed: Bug that prevented release uploads and will unblock the entire F# ecosystem - #895 via @naveensrinivasan

### New in 0.15.0 (released 2015/09/11)
* New: `IRepositoryContentsClient.GetAllContents` now has an overload to support specifying a reference - #730 via @goalie7960
* New: Support for retrieving rate limit information from `IMiscellaneousClient` - #848 via @Red-Folder
* New: Use `GitHubClient.GetLastApiInfo()` to get API information for previous request - #855 via @Red-Folder, @khellang
* New: `PreviousFileName` returned to show renamed files in commit - #871 via @CorinaCiocanea
* Improved: `CommentUrl` returned on `Issue` response - #884 via @naveensrinivasan
* Improved: Issue and Code Search now accepts multiple repositories - #835 via @shiftkey
* Improved: Search now accepts a range of dates - #857 via @ChrisMissal
* Improved: Documentation on `Issue` response - #876 via @Eilon
* Improved: Code Search now accepts `FileName` parameter - #864 via @fffej
* Fixed: `GetQueuedContent` should return empty response for `204 No Content`, instead of throwing - #862 via @haacked
* Fixed: `TeamClient.AddMembership` sends correct parameter to server - #856 via @davidalpert
* Obsolete: `Authorization` endpoint which does not require fingerprint - #878 via @niik

**Breaking Changes:**
 - #835 has changed the `Repos` property for `SearchIssuesRequest` and `SearchCodeRequest`
   are now of type `RepositoryCollection` so that multiple repositories can be searched.
 - The workarounds removed in #878 were added initially to support transitioning, but now
   we enforce the use of a fingerprint. See https://developer.github.com/v3/oauth_authorizations/
   for more details.


### New in 0.14.0 (released 2015/07/21)
* New: Repository redirects are supported natively - #808 via @darrelmiller, @shiftkey
* Fixed: Support for searching repositories without a search term - #828 via @alexandrugyori

### New in 0.13.0 (released 2015/06/17)
* Fixed: Added some missing Organization Teams methods - #795 via @phantomtypist, @shiftkey

### New in 0.12.0 (released 2015/05/19)
* New: Added support for repository hooks and forks - #776 via @kristianhald, @johnduhart and @AndyCross
* Fixed: Merging a PR should permit specifying a SHA - #805 via @alfhenrik

### New in 0.11.0 (released 2015/05/10)
* New: Added overload to `IRepositoryClient.GetAllPublic` specifying a `since` parameter - #774 via @alfhenrik
* New: Added `IGistsClient.GetAllCommits` and `IGistsClient.GetAllForks` implementations - #542 via @haagenson, #794 via @shiftkey
* New: Added `IRepositoryContentsClient.GetArchiveLink` for getting archived code - #765 via @alfhenrik
* Fixed: `PullRequestFile` properties were not serialized correctly - #789 via @thedillonb
* Fixed: Allow to download zip-attachments - #792 via @csware

### New in 0.10.0 (released 2015/04/22)
* Fixed: renamed methods to follow `GetAll` convention - #771 via @alfhenrik
* Fixed: helper functions and cleanup to make using Authorization API easier to consume - #786 via @haacked

**Breaking Changes:**
 - As part of #771 there were many method which were returning collections
   but the method name made it unclear. You might think that it wasn't much, but
   you'd be wrong. So if you have a method that no longer compile,
   it is likely that you need to set the prefix to `GetAll` to re-disocver that API.
 - `CommitComment.Position` is now a nullable `int` to prevent serialization issues.

### New in 0.9.0 (released 2015/04/04)
* New: added `PullRequest.Files` APIs - #752 via @alfhenrik
* Fixed: `PullRequestRequest` now supports `SortDirection` and `SortProperty` - #752 via @alfhenrik
* Fixed: `Repository.Create` now enforces a repository name - #763 via @haacked
* Fixed: corrected naming conventions for endpoints which return a list of results - #766 via @alfhenrik
* Deprecated: `Repository.GetReadme` and `Repository.GetReadmeHtml` - #759 via @khellang

**Breaking Changes:**
 - `NewRepository` constructor requires a `name` parameter
 - `IRepositoriesClient.GetReadme` -> `IRepositoriesClient.Content.GetReadme`
 - `IRepositoriesClient.GetReadmeHtml` -> `IRepositoriesClient.Content.GetReadmeHtml`
 - `IFollowersClient.GetFollowingForCurrent` -> `IFollowersClient.GetAllFollowingForCurrent`
 - `IFollowersClient.GetFollowing` -> `IFollowersClient.GetAllFollowing`

### New in 0.8.0 (released 2015/03/20)
* New: added `MiscellaneousClient.GetGitIgnoreTemplates` and `MiscellaneousClient.GetGitIgnoreTemplates` APIs - #753 via @haacked
* New: added `MiscellaneousClient.GetLicenses` and `MiscellaneousClient.GetLicense` preview APIs - #754 via @haacked
* New: enhancements to `AuthorizationClient`- #731 via @alfhenrik
* Fixed: handled `unsubscribe` type for Issue events - #751 via @darrencamp
* Fixes: ensure response models define readonly interfaces - #755 via @khellang

### New in 0.7.3 (released 2015/03/06)
* New: added `Repository.GetAllPublic` for searching public repositories - #691 via @rms81
* New: added filters to `Repository.GetAllForCurrent()` - #742 via @shiftkey
* Fixed: deserializing `EventInfoType` value with underscore now works - #727 via @janovesk
* Deprecated: `Repository.SubscriberCount` has no data - #739 via @basildk
* Deprecated: `Repository.Organization` has no data - #726 via @alfhenrik

### New in 0.7.2 (released 2015/03/01)
* Fixed: unshipped Orgs Permissions preview API changes due to excessive paging in some situations.

### New in 0.7.1 (released 2015/02/26)
* New: `SearchCodeRequest` has overloads for owner and repository name - #705 via @kfrancis
* New - support for preview Authorization API changes - #647 via @shiftkey
* New - `Account.Type` to identify user or organization account - #714 via @shiftkey
* Fixed: `EventTypeInfo` did not parse `head_ref_deleted` and `head_ref_restored` - #711 via @janovesk
* Fixed: `IssueUpdate.Labels` did not support "no change" updates - #718 via @shiftkey
* Fixed: `ReleaseUploadAsset` does not require protected setters - #720 via @shiftkey
* Deprecated: `Repository.WatchedCount` has no data - #701 via @DaveWM

### New in 0.7.0 (Released 2015/02/24)
* New: Response models now use read-only properties - #658, #662 via @haacked, #663 via @khellang, #679 via @Zoltu
* New: Added `Truncated` property to `TreeResponse` - #674 via @Zoltu
* New: Added `GetRecursive` method to `ITreesClient` - #673 via @Zoltu
* New: Added `Merging` client to `Repository` API: - #603 via @tabro
* New: API internals are now read-only - #662 via @haacked
* Fixed: Commit Status API now supports combined status- #618 via @khellang
* Fixed: Changed `IGistCommentsClient` identifiers to `string` instead of `int` - #681 via @thedillonb
* Fixed: Improved error message when repository creation fails - #667 via @gabrielweyer
* Fixed: Team membership API was incorrect - #695 via @aneville

**Breaking Changes**
- Response models are all read only. It is recommended that you subclass the
  model class if you need to contructor responses (e.g. for testing)
- `IResponse` is now a `readonly` interface.
- `ApiResponse<T>` accepts the strongly typed body as an argument.
- `IResponse<T>` changed to `IApiResponse<T>`.

### New in 0.6.2 (Released 2015/01/06)
* New: Added `Assignee` and `Label` to `EventInfo` and `IssueEvent` repsonses - #644 via @thedillonb
* New: Added `BrowserDownloadUrl` to `ReleaseAsset` response - #648 via @erangeljr
* New: Added `Stats` to `GitHubCommit` and `Patch` to `GitHubCommitFile` - #646 via @thedillonb
* New: Support for retrieving and manipulating repository contents using `GitClient.Repository.Content` - #649 via @haacked and @khellang
* Fixed: updated enum values returned from `EventInfo.Event` - #644 via @thedillonb
* Fixed: serialization issue with `Head` and `Base` in pull request - #606 via @mge
* Fixed: `SignatureResponse.Date` is now a `DateTimeOffset` - #646 via @thedillonb

**Breaking Changes:**
 - `EventInfo.InfoState` is now `EventInfo.Event`
 - `IssueEvent.InfoState` is now `IssueEvent.Event`
 - `SignatureResponse.Date` has changed from `Date` to `DateTimeOffset`

### New in 0.6.1 (Released 2014/12/23)
* New: `IOrganizationMembersClient.GetAll` now has enum to filter 2FA - #626 via @gbaychev
* Fixed: `User.GravatarId` and `Author.GravatarId` are marked as obsolete - #622 via @gbaychev
* Fixed: Use `DateTimeOffset.MinValue` as default parameter for `NotificationRequest.Since` - #641 via @thedillonb

### New in 0.6.0 (Released 2014/12/15)
* Fixed: Typo in guard clause for `ApiInfo` - #588 via @karlbohlmark
* Fixed: Documentation typos in `NewRepository` - #590 via @karlbohlmark
* Fixed: `Files` array now included when fetching a commit - #608 via @kzu
* Fixed: `GetAllContributors` return `Contributions` count - #614 via @SimonCropp

### New in 0.5.3 (Released 2014/12/05)
* New: Uploading release assets now supports an optional timeout value - #587 via @shiftkey

### New in 0.5.2 (Released 2014/10/13)
* New: Method to add repository to team - #546 via @kevfromireland
* Fixed: PATCH parameters for releases, issues and pull requests are now nullable - #561 via @thedillonb

**Breaking Changes:**

 - `PullRequestUpdate` removed unused fields: `Number`, `State`, `Base`, and `Head`
 - `ReleaseClient.Create` now accepts a `NewRelease` parameter (was `ReleaseUpdate`)
 - `ReleaseUpdate` no longer requires a `TagName` in the constructor (see `NewRelease`)
 - `ReleaseUpdate` now has nullable `Draft` and `Prerelease` properties - only
     set these if you want to apply changes to the API
 - `IssueUpdate.State` is now a nullable `ItemState`
 - `MilestoneUpdate.Number` is now removed
 - `MilestoneUpdate.State` is now a nullable `ItemState`

### New in 0.5.1 (Released 2014/10/08)
* New: added XML docs to NuGet package for Maximum Intellisense - #586 via @shiftkey

### New in 0.5.0 (Released 2014/10/07)
* New: added more methods for users and orgs - #553 via @andrerod
* New: added support for Universal Apps - #575 via @hippiehunter
* New: added missing fields to `Repository` - #560 via @thedillonb
* New: upgraded Octokit.Reactive to Rx 2.2.5 - #564 via @haacked
* Fixed: added `ItemState.All` enum value so issue filtering can be bypassed - #550 via @MitjaBezensek
* Fixed: remove trailing slash in `ApiUrl` that causes /team/{id}/repos to fail - #555 via @matt-gibbs
* Fixed: `PullRequest.Mergeable` was misspelt, causing serialization issue - #576 via @jrowies
* Fixed: serialization issue when parsing `OAuthToken.Scope` list - @shiftkey

**Breaking Change:** `Readme.GetHtmlContent()` would return a 404, due to `Readme.HtmlUrl` not accepting custom Accepts header. This method now uses `Readme.Url` internally, which will return a slightly different DOM.

### New in 0.4.1 (Released 2014/07/22)
* New: Added a public method for turning pages of requests into a flat observable - #544 via @haacked

### New in 0.4.0 (Released 2014/07/14)
* New: added Commit.CommentCount property - #494 via @gabrielweyer
* New: added initial support for User Keys - #525 via @shiftkey
* New: support for listing commits on a repository - #529 via @haagenson
* New: support for Pull Request Comments - #531 via @gabrielweyer
* Fixed: unassign milestone from issue - #526 via @shiftkey
* Fixed: organization deserialization bug - #522 via @shiftkey
* Fixed: Repository.MasterBranch -> Repository.DefaultBranch -  #523 via @shiftkey
* Improved: refinements to Releases API - #519 via @shiftkey
* Improved: can delete registered emails for the authenticated user - #524 via @shiftkey

### New in 0.3.5 (Released 2014/06/30)
* Fix issue search filtering bug - #481 via @shiftkey
* Fix methods to edit a release - #507 via @distantcam
* Fix methods to edit a release assset - #514 via @haacked

### New in 0.3.4 (Released 2014/05/01)
* Improvements to "repository exists" exception result - #473 via @shiftkey
* Encoding query parameters impacts search clients - #467 via @shiftkey

### New in 0.3.3 (Released 2014/04/22)
* Add methods to retrieve a team's members and to check if a user is a member of a team - #449 via @kzu
* Add OAuth web flow methods - #462 via @haacked

### New in 0.3.2 (Released 2014/04/16)
* Allow passing a parameter to the Patch method - #440 via @nigel-sampson
* Remove the redundant Team suffix from ITeamsClient - #451 via @kzu
* Remove Immutable Collections dependency to support .NET 4 builds - #453 via @paulcbetts
* Add method to retrieve raw bytes from a request - #457 via @haacked
* Fix readonly deserialization bug in NetCore45 and related projects - #455 via @nigel-sampson

### New in 0.3.1 (Released 2014/03/31)
* Add support for comparing two commits - #428 via @shiftkey
* Fix regression in throwing proper 2FA exception - #437 via @Haacked

### New in 0.3.0 (Released 2014/03/19)
* Add Portable Class Library support for Octokit package - #401 via @trsneed
* Filter repository issues by users - #427 via @shiftkey

### New in 0.2.2 (Released 2014/03/06)
* Task-based and Observable-based APIs are now consistent - #361 #376 #378 via @shiftkey and @ammeep
* "_links" JSON field serialization convention fix - #387 via @haacked
* Added Feeds client - #386 via @sgwill
* Added support for creating Gists - #391 via @Therzok and @rgmills
* Make readonly collections truly readonly - #394 #399 via @Haacked
* Internalize ProductHeaderValue - #406 via @trsneed
* HttpClient.Send without cancellation token - #410 via @ammeep
* Implement Repository Comments API - #413 via @Haacked @wfroese
* Corrected Search response to match API - #412 #417 #419 #420 via @shiftkey

### New in 0.2.1 (Released 2014/02/19)
* Reverted the dependency on Reactive Extensions 2.2.2 which introduced breaking changes

### New in 0.2.0 (Released 2014/02/19)
* Fixed an issue where some new observable clients were not accessible - #376 via @shiftkey

### New in 0.1.9 (Released 2014/02/19)
* New client for searching users - #289 via @hahmed
* New client for the statistics API - #296 via @ammeep
* New client for managing deployments and deployment status - #298 via @pmacn
* Added methods to repositories client for getting metadata such as contributors, languages, tags, etc. - #319 via @pmacn
* Added GetAll and Add methods to the user emails client - #323 via @pmacn
* New client for managing user followers - #343 via @alfhenrik
* Add more methods for managing releases - #344 via @alfhenrik
* New client for the activity watching API - #350 via @nigel-sampson
* ObservableStarredClient can now be accessed via a property - #351 via @nigel-sampson
* Emoji api now has Emoji type rather than dictionary - #354 via @haacked
* New client for the Pull Requests API - #360 via @jpsullivan and @shiftkey
* Now throws RepositoryExistsException when repository already exists - #377 via @haacked
* Now throws PrivateRepositoryQuotaExceededException when private repository quota would be exceeded by a new repository - #379 via @haacked

### New in 0.1.8 (Released 2014/01/22)
* Added IRepositoryClient.GetAllBranches - #270 via @goalie7960
* Install-Package should add reference to System.Net.Http - #286 via @alfhenrik
* Return a more helpful error if invalid refs path provided - #288 via @alfhenrik
* Refactor SearchIssuesRequest to match the API - #290 via @alfhenrik
* Deprecate custom Releases Accept header - #291 via @shiftkey
* Fix date format used in DateRange - #293 via @alfhenrik
* Add base class for search requests - #301 via @hahmed
* Deprecate IGitHubClient.Blob - #305 via @shiftkey
* Improving Proxy Server support - #306 via @shiftkey
* Add integration tests for IRepositoryClient.GetAllBranches - #309 via @lbargaoanu
* Refactor SearchCodeRequest to match the API - #311 from @alfhenrik
* Implemented Delete for IssueCommentsClient - #315 from @pmacn
* Implemented missing methods for IssueLabels - #316 from @pmacn

### New in 0.1.7 (Released 2013/12/27)
* New client for repository search - #226 and @273 via @hahmed
* Bugfix for creating/updating issue comments - #262 via @tpeczek
* Bugfix for retrieving events - #264 via @shiftkey

### New in 0.1.6 (Released 2013/12/18)
* New client for managing Gists -  #225 via @SimonCropp
* New client for managing Git references - #238 via @khellang
* Added missing Observable versions for Git objects client - #251 by @khellang
* New client for Gist comments - #252 by @khellang
* Lots of documentation - #253 by @pmacn
* New client for managing issue labels - #256 by @andrerod

### New in 0.1.5 (Released 2013/11/19)
* New client for starring repositories
* New client for retrieving commits
* New client for managing an organization's teams and members
* New client for managing blobs
* New client for retrieving and creating trees
* New client for managing collaborators of a repository

### New in 0.1.4 (Released 2013/11/6)
* New client for retrieving activity events
* Fixed bug where concealing an org's member actually shows the member

### New in 0.1.3 (Released 2013/11/5)
* New Xamarin Component store versions of Octokit.net
* New clients for managing assignees, milestones, and tags
* New clients for managing issues, issue events, and issue comments
* New client for managing organization members
* Fixed bug in applying query parameters that could cause paging to continually request the same page

### New in 0.1.2 (Released 2013/10/31)
* New default constructors in Octokit.Reactive
* New IObservableAssigneesClient in Octokit.Reactive

### New in 0.1.1 (Released 2013/10/30)
* Fixed problems with Microsoft.Threading.Tasks

### New in 0.1.0 (Released 2013/10/30)
* Initial release
