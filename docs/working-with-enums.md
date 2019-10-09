# Working with Enums

In order to provide a consistent and familiar dotnet experience, Octokit maps appropriate GitHub API fields to c# `Enum`'s.

For example an `Issue`'s status API values are `"open"` and `"closed"` which in Octokit are represented as `ItemState.Open` and `ItemState.Closed`

## Introducing the `StringEnum<TEnum>` Wrapper

Since the upstream GitHub API can move fast, we want to avoid throwing exceptions when deserializing responses that contain field values that are not yet present in our Octokit `Enum` values.

Therefore Octokit now uses a wrapper class `StringEnum<TEnum>` to represent these values in all response models.

### `StringEnum<TEnum>` Usage

`StringEnum` is able to be implicitly converted to/from `Enum` and `string` values and provides the convenience of dealing with explicit enumeration members the majority of cases, with the added resillience to fall back to `string`s when it needs to.

Whilst existing code will continue to function due to the implicit conversions, users should update their code to the "safe" usage patterns shown below in order to guard against future API additions causing deserialization failures.

#### Implicit conversions:
``` csharp
public enum EncodingType
{
    Ascii, // api value "ascii"
    Utf8   // api value "utf-8"
}

// Implicit conversion from Enum
StringEnum<EncodingType> encoding = EncodingType.Utf8;

// Implicit conversion from API string
StringEnum<EncodingType> encoding = "utf-8";
```

#### When dealing with a known API value
Accessing the string value (safe):
``` csharp
StringEnum<EncodingType> encoding = "utf-8";
Console.WriteLine(encoding.StringValue);

// "utf-8"
```

Accessing the Enum value (not safe, but works as the value is known):
``` csharp
StringEnum<EncodingType> encoding = "utf-8";
Console.WriteLine(encoding.Value);

// EncodingType.Utf8
```

#### When dealing with an unknown API value
Accessing the string value (safe):
``` csharp
StringEnum<EncodingType> encoding = "new_hoopy_format";
Console.WriteLine(encoding.StringValue);

// "new_hoopy_format"
```

Accessing the Enum value (not safe, throws exceptionn as the value is not known)
``` csharp
StringEnum<EncodingType> encoding = "new_hoopy_format";
encoding.Value;

// ArgumentException: "Value 'new_hoopy_format' is not a valid 'EncodingType' enum value.
```

Evaluating the value safely, using `TryParse()`
``` csharp
StringEnum<EncodingType> encoding = "new_hoopy_format";
if (encoding.TryParse(out EncodingType type))
{
    Console.WriteLine("{0} was a known enum member!", type);
}
else
{
    Console.WriteLine("{0} was not a known enum member!", encoding.StringValue);
}

// "new_hoopy_format was not a known enum member!"
```

## Activity/Timeline APIs and EventInfoState

Of particular importance are the `Enum`'s used in activity/event stream APIs, such as `EventInfoState`.  Since new functionality introduced on GitHub.com frequently leads to additional event types being received, Octokit was constantly playing catchup with these `Enum` values.  Now, with `StringEnum<TEnum>` we have a solution to keep your code functioning, without needing to wait for a new Octokit release!

### Safe Issue Timeline EventInfoState Example
``` csharp
// Get the event timeline for a PullRequest
var timelineEventInfos = await client.Issue.Timeline.GetAllForIssue("octokit", "octokit.net", 1595);

foreach (var issueEvent in timelineEventInfos)
{
    // Safely check whether the EventInfoType was a known Enum value
    if (issueEvent.Event.TryParse(out EventInfoState eventState))
    {
        // Enum value known
        switch (eventState)
        {
            case EventInfoState.Commented:
            case EventInfoState.CommitCommented:
            case EventInfoState.LineCommented:
                {
                    Console.WriteLine("Comment activity found!");
                    break;
                }
            case EventInfoState.ReviewDismissed:
            case EventInfoState.Reviewed:
            case EventInfoState.ReviewRequested:
            case EventInfoState.ReviewRequestRemoved:
                {
                    Console.WriteLine("Review activity found!");
                    break;
                }
        }
    }
    else
    {
        // Enum value not known, use StringValue
        Console.WriteLine("Unknown EventInfoState encountered: {0}",
            issueEvent.Event.StringValue);
    }
}
```