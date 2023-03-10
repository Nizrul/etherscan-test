# Etherscan Test

- IndexService is used for interfacing with the Alchemy API
- BlockReward is currently set to 2, not calculating based on https://docs.alchemy.com/docs/how-to-calculate-ethereum-miner-rewards

## Setup

- While it is a hack, you have to uncomment the paramless ApplicationDbContext and the onConfiguring to add and apply migrations
  - In addition to that, while using dotnet ef, the call to `indexService:indexBlocks` would need to be disabled in Program.cs as well
- The saving for most transactions will still fail since some of the values are greater than 20 (UINT64 only goes up to 19, and the DB only holds int(20)), and saving partial transctions is not a good alternative
  - It might be better to just increase the space for both the DB and the handling of the number potentially by just converting it to a decimal string instead of a number, but it's kept as is for now
- Create a user secret with the following json:

```
{
    "ConnectionStrings": {
        "Default": ""
    },
    "Alchemy": {
        "ApiKey": ""
    }
}
```
