# HomeLINK Frontier Client C# Starter

This repo has two intended purposes.

1. A code based demostration of how to retrieve data from the HomeLINK Frontier API.
2. A starting point for integration projects targeting the HomeLINK Frontier API.

## Frontier API Documentation

https://help.live.homelync.io/hc/en-us/articles/6930918454929-Frontier-Data-Retrieval-API-

_You'll need a HomeLINk account to view the above!_

## Getting Started

Get the code and it's packages:

- `$ git clone https://github.com/homelync/frontier-client-starter-csharp`
- `$ cd frontier-client-starter-csharp`
- `$ dotnet restore`

Configure:

- Open [`appsettings.Test.json`](./Frontier/appsettings.Test.json) and specifiy your API clientId and secret. You can also specify a `TestPropertyReference` which will be used by the tests include.


## Using this repository as a demonstration

### Implementation

A really simple, single file (mostly) based demo of authentication and retrieving data can be found in [`SimpleFrontierService.cs`](./Frontier/Services/SimpleFrontierService.cs).

This service provides example of how to get:

- `Properties`
- `Devices`
- `Readings`

Take a look at the public methods in this class. They both follow the same simple pattern:

1. Get an access token
2. Perform a `GET` request.

That's all there is to it!

### Tests

To see this class in action you can run the tests that have been included in [`SimpleFrontierService.cs`](./Tests.Integration/Integration/SimpleFrontierService.cs).
These tests have been included simply to show the `SimpleFrontierService` being used, they are not intended to be thorough test cases. To run these tests:

- `$ cd Tests.Integration`
- `$ dotnet test --filter Simple`


**Please note, this code is not intended for production purposes and includes some bad practices that have been included to make the example simple.**

## Using this repository as a starter

### Implementation

A more "production-ready" implementation showing IoC, DI and SoC can be seen in [`AdvancedFrontierService.cs`](./Frontier/Services/AdvancedFrontierService.cs).

### Tests

To see this class in action you can run the tests that have been included in [`AdvancedFrontierService.cs`](./Tests.Integration/Integration/AdvancedFrontierService.cs).
These tests have been included simply to show the `AdvancedFrontierService` being used, they are not intended to be thorough test cases. To run these tests:

- `$ cd Tests.Integration`
- `$ dotnet test --filter Advanced`

### Contributing

If you would like to see other examples included in this repo, please raise an issue. Alternatively, we are accepting PRs. Go on, do it!

