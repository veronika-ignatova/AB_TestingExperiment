REST API application with 2 endpoints for realization A/B testing.

The web application (Client) generates some unique client ID when requesting an API, which persists between sessions, and requests the experiment by adding the device-token GET parameter. To answer the server gives an experiment.

For each experiment, the client receives:

  • Key: experiment name (X-name). It is assumed that there is code in the client that will change some behavior depending on the value of this key.
  
  • Value: string, one of the possible options.
  
It is important that the device falls into one group and always remains in it based on the device token.


HOW TO RUN AN APPLICATION:
1. Run SQL scripts located in DataBase/Sql (it will help you create a DB and Stored Procedure).
2. Change a connection string in appsetting.json.

When you run an application it will fill your DB with 2000 data rows. Then you will see the result of statistics in JSON format with a list of experiments, the total number of devices, participating in the experiment, and their distribution among options.

DB structure:

<img width="540" alt="image" src="https://github.com/veronika-ignatova/BackendTZ/assets/122632495/efab5a77-0c0d-4234-b6be-690a5f284eb7">


Example of customer request:

<img width="633" alt="image" src="https://github.com/veronika-ignatova/BackendTZ/assets/122632495/ade1b989-4b7c-46e6-84d4-e2d948362dd0">


Example of start page with statistics:

<img width="960" alt="image" src="https://github.com/veronika-ignatova/BackendTZ/assets/122632495/1915da71-3f63-4853-acd4-99e7a2194040">
