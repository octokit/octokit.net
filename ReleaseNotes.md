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
