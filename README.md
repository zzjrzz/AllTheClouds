![.NET Core](https://github.com/zzjrzz/AllTheClouds/workflows/.NET%20Core/badge.svg)

# AllTheClouds
A store front that interacts with AllTheClouds API

## How To Run
1. Insert API secret in `secrets.json` as `AllTheClouds:ApiKey` more details on how can be found here (https://docs.microsoft.com/en-us/aspnet/core/security/app-secrets)

## Assumptions
1. Authentication for customers is not implemented.
2. `/api/Products` returns unit prices in AUD by default.
3. *sourceCurrency* and *targetCurrency* by `/api/fx-rates` are in the ISO4217 code format (https://en.wikipedia.org/wiki/ISO_4217)

## Technical Decisions
- Github Actions for pipeline automation
  - Seeing as the repository is hosted on Github and it is free.
- Moq for unit test mocking
  - Widely used and popular, why change if it is not broken?
- The Angular Convention for commit messages (https://github.com/angular/angular/blob/22b96b9/CONTRIBUTING.md#-commit-message-guidelines)
  - Conveys more meaning in less words, following explicit rules in commits make it easy to write automated tools in the future, such as automated semantic versioning.
- WireMock.NET for over the wire test doubles
  - Easy to use, well maintained and responsive contributors
