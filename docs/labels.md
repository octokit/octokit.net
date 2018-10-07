# Working with Issue Labels

Labels are appended using the method `NewIssue.Labels.Add(x)`.

Example:

```csharp
var myNewIssue = new NewIssue("Issue with dropdown menu");
myNewIssue.Labels.Add("bug");
```

The default labels that come with every repository are:
- bug
- duplicate
- enhancement
- help wanted
- invalid
- question
- wontfix

## A Note on Label Colors
The official API returns colors without the leading `#` that you may expect when working with hex colors -- for example, white would return `FFFFFF`, not `#FFFFFF`. 

If you're displaying the colors, you may need to add the `#` in order to display them properly.
