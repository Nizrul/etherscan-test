# Etherscan Test

- IndexService is used for interfacing with the Alchemy API
- BlockReward is currently set to 2, not calculating based on https://docs.alchemy.com/docs/how-to-calculate-ethereum-miner-rewards

## Setup

- While it is a hack, you have to uncomment the paramless ApplicationDbContext and the onConfiguring to add and apply migrations
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
