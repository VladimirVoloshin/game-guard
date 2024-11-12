
# GameGuard Solution

GameGuard is a comprehensive solution for detecting malicious activity and bots in gaming environments. This solution consists of three main components:

1.  **GameGuard.API**: A dashboard API for malicious and bot detection.
2.  **GameGuard.Client**: A React-based dashboard UI for visualizing and managing detection data.
3.  **GameGuard.PlayerActivityEmulator**: A console application that emulates random player activity for testing purposes.

## Demo Video: 
### [Video](https://drive.google.com/file/d/1Rcll3g73ZrgsIyXpG85PC23Qfv-2lc4d/view?usp=sharing)

## Setup and Running Instructions

### GameGuard.API

1.  Navigate to the root  `game-guard`  folder.
    
2.  Update the connection string and CORS settings in  `appsettings.json`  or  `appsettings.Development.json`:
    

json

```
{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=GameGuard.db"
  },
  "CorsOrigins": "http://localhost:3000"
}
```

3.  Run the API:

bash

```
dotnet run --project ./src/GameGuard.API
```

You should see a message:  `Now listening on: http://localhost:5000`

### GameGuard.Client

1.  Create a  `.env`  file in the root of the  `GameGuard.Client`  directory and add:

```
REACT_APP_GAME_GUARD_API_URL=http://localhost:5000
```

2.  Navigate to the client application folder:

bash

```
cd ./src/GameGuard.Client
```

3.  Install dependencies and start the application:

bash

```
npm install && npm start
```

You should see a message:  `Local: http://localhost:3000`

### GameGuard.PlayerActivityEmulator

1.  Add the API URL to  `launchSettings.json`  in the  `GameGuard.PlayerActivityEmulator`  project:

json

```
{
  "profiles": {
    "GameGuard.PlayerActivityEmulator": {
      "commandName": "Project",
      "environmentVariables": {
        "API_URL": "http://localhost:5000"
      }
    }
  }
}
```

2.  Run the emulator:

bash

```
dotnet run --project ./src/Other/GameGuard.PlayerActivityEmulator
```

## Usage

After starting all components, you can access the dashboard at  `http://localhost:3000`. The emulator will automatically send random player activity data to the API, which can be visualized on the dashboard.