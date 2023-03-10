# Etherscan Test

- IndexService is used for interfacing with the Alchemy API
- BlockReward is currently set to 2, not calculating based on https://docs.alchemy.com/docs/how-to-calculate-ethereum-miner-rewards

## Setup

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
