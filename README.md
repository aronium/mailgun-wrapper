# Aronium Mailgun Wrapper

Mailgun wrapper written in C#. 

Wrapper is using [Builder pattern](https://en.wikipedia.org/wiki/Builder_pattern) for easy chaining and creating [Mailgun](https://www.mailgun.com/) requests.

## Getting Started

Before you start, make sure you read Mailgun documentation at [Mailgun API Reference](https://documentation.mailgun.com/en/latest/api_reference.html)

### Prerequisites

Project is using .NET Framework 4.5

The following dependencies are managed using NuGet packages:

* RestSharp
* Newtonsoft.Json

### Installing

Clone repo or download a ZIP file with source files and open *MailGunWrapper.sln* in Visual Studio. 

First project build will update NuGet dependencies automatically.

## Usage examples

Below is the list of commonly used operations.

### Sending email address

```csharp
// Method will send a simple email to specified recipient
var request = MailgunResourceRequest.Builder
                .ForSendMessage()
                .From("Aronium <me@aronium.com>")
                .To("mail@example.com")
                .Subject("Hi")
                .Text("It is I, Leclerc!")
                .Build();

var response = new MailgunService().GetResponse(request);
        
```

### Read events

```csharp
// Method will get all Unsubscribed and Failed (e.g. Bounced) events from day ago
var request = MailgunResourceRequest.Builder
                .ForEvents(MailGunEventType.Unsubscribed | MailGunEventType.Failed)
                .Begin(DateTime.Now.AddDays(-1))
                .Limit(10)
                .Build();

var response = new MailgunService().GetResponse<MailgunEventCollection>(request);        
```

### List all unsubscriptions
```csharp
var request = MailgunResourceRequest.Builder
                .ForUnsubscribes()
                .Build();

var response = new MailgunService().GetResponse<MailgunEmailAddressCollection>(request);
```

### Check unsubscribe emails
```csharp
var request = MailgunResourceRequest.Builder
                .ForUnsubscribes("unsubsribed@example.com")
                .Build();

var response = new MailgunService().GetResponse<MailgunEmailAddress>(request);
```

## Authors

See the list of [contributors](https://github.com/aronium/mailgun-wrapper/graphs/contributors) who participated in this project.

## License

This project is licensed under the MIT License
