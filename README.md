# AllTheClouds
A store front that interacts with AllTheClouds API

## How To Run
1. Insert API secret in `secrets.json` as `AllTheClouds:ApiKey` more details on how can be found here (https://docs.microsoft.com/en-us/aspnet/core/security/app-secrets)

## Assumptions
1. Authentication for customers is not implemented.
2. `/api/Products` returns unit prices in AUD by default.
3. *sourceCurrency* and *targetCurrency* by `/api/fx-rates` are in the ISO4217 code format (https://en.wikipedia.org/wiki/ISO_4217)

