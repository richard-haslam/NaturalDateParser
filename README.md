![Test Status](https://github.com/richard-haslam/NaturalDateParser/actions/workflows/test.yml/badge.svg)


# NaturalDateParser

A simple and extensible **natural language date/time parser** for .NET that converts expressions like `"next Friday at 5pm"`, `"2 days ago"`, `"17:00"`, or custom date formats (e.g. `"25/05/2025"`) into `DateTime` objects.

**Main aim:** To help with **test automation** scenarios by easily converting human-readable date/time expressions into usable `DateTime` objects in test scripts and automation workflows.

---

## Day of the week parser
To keep your parser predictable and testable (especially for automation), the following rules are defined:

### ✅ Definitions Used
| Expression      | Rule                                                                            |
| --------------- | ------------------------------------------------------------------------------- |
| **This \[day]** | The closest upcoming \[day] **in the current week**, or today if it's that day. |
| **Next \[day]** | The \[day] **in the following week**, even if today is that day.                |
| **Last \[day]** | The \[day] **in the previous week**, even if today is that day.                 |
| No modifier     | Treated like `"next"` if the day has passed, or `"this"` if it hasn't.          |

### 🗓️ Example Scenarios (Assume today is Monday, May 27, 2025)
| Input            | Result            | Explanation                                      |
| ---------------- | ----------------- | ------------------------------------------------ |
| `this Monday`    | Mon, May 27, 2025 | Today                                            |
| `next Monday`    | Mon, June 2, 2025 | Always moves to next week                        |
| `last Monday`    | Mon, May 19, 2025 | Previous week’s Monday                           |
| `Friday`         | Fri, May 30, 2025 | This coming Friday (same week)                   |
| `Monday` (again) | Mon, June 2, 2025 | Treated like `next Monday` since today is Monday |
| `last Friday`    | Fri, May 23, 2025 | Previous Friday                                  |
| `next Friday`    | Fri, June 6, 2025 | Friday *after* this week's Friday                |

### 📌 Why This Approach?
- It avoids ambiguity: "next Friday" always skips this week, even if today is Monday.
- It makes "this Friday" refer to the nearest upcoming Friday.
- It makes "last Monday" always go back 1 week.
- It ensures consistency in automation/test scripts, which need predictable outputs.

## Other Features

- Parse relative dates and times:  
  - `"now"`, `"today"`, `"tomorrow"`, `"yesterday"`  
  - `"2 days ago"`, `"3 weeks from now"`, `"1 month later"`   
- Parse exact date/time formats with built-in support for many common patterns and ability to add custom formats:  
  - `"dd/MM/yyyy"`, `"MM/dd/yyyy"`, `"yyyy-MM-dd"`, `"dd-MM-yy"`, etc.  
- Basic timezone abbreviation handling for offsets like `PST`, `EST`, `UTC`, etc.

---

## Usage

Add the package to your project (if published as NuGet) or include the source files inside the `NaturalDateParsing` namespace.

```csharp
using NaturalDateParsing;

class Program
{
    static void Main()
    {
        string input = "next Friday at 5pm";
        if (NaturalDateParser.TryParse(input, out DateTime parsed))
        {
            Console.WriteLine($"Parsed date/time: {parsed}");
        }
        else
        {
            Console.WriteLine("Could not parse the input.");
        }
    }
}
```

## Supported Expressions
| Expression Type    | Examples                           | Notes                                                        |
| ------------------ | ---------------------------------- | ------------------------------------------------------------ |
| Relative Dates     | `2 days ago`, `1 week from now`    | Supports seconds, minutes, hours, days, weeks, months, years |
| Keywords           | `now`, `today`, `tomorrow`         | —                                                            |
| Day of Week        | `next Monday`, `last Friday`       | Optional time like `"at 5pm"` supported                      |
| Time               | `5pm`, `17:00`, `noon`, `midnight` | 12-hour and 24-hour formats supported                        |
| Exact Formats      | `25/05/2025`, `2025-05-25`         | Built-in common formats and custom formats can be added      |
| Time Zones (Basic) | `PST`, `EST`, `UTC`                | Applies basic timezone offsets to parsed date                |

## Extending the Parser
### Adding Custom Date/Time Formats

You can add custom date/time formats to support additional patterns not included by default:
```csharp
NaturalDateParser.AddCustomFormat("dd MMM yyyy");
NaturalDateParser.AddCustomFormat("yyyyMMdd");
```
These formats will be considered during parsing.

### Adding New Parsing Rules
The parser is designed with modular internal classes for each kind of pattern (relative dates, days of week, times, exact formats, time zones). To add new parsing rules:

1. Create a new internal static parser class that implements a similar TryParse(string input, out DateTime result) method.

2. Add your parsing logic inside that class.

3. Update the main NaturalDateParser.TryParse method to call your new parser before or after existing ones as appropriate.

This modular design keeps parsing logic isolated and easy to maintain.

## Contributing

Feel free to open issues or submit pull requests to improve parsing capabilities or add support for additional languages and regional formats.