# Working with Issue Labels

Labels are appended using the method `NewIssue.Labels.Add(x)`.

Example:
    var myNewIssue = new NewIssue("Issue with dropdown menu");
    myNewIssue.Labels.Add("bug");
    
The default labels that come with every repository are:
- bug
- duplicate
- enhancement
- help wanted
- invalid
- question
- wontfix
