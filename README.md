# bad-each-way-finder-api
This is the back end API. The front end can be found at: https://github.com/traynosm/bad-each-way-finder

Update the connection string settings in appsettings.json. They are currently configured to the developers machine. 


```c#
"ConnectionStrings": {
    "BadEachWayFinderApi": "Data Source=[Placeholder]"}
```

Prior to running the application run the following scripts:

```console
dotnet ef migrations add initial
```

```console
dotnet ef database update
```
Commands will also need to run to set up user secrets

```console
dotnet user-secrets init
```
```console
dotnet user-secrets set "LoginSettings:BETFAIRUSERNAME" "<secret_username>"
dotnet user-secrets set "LoginSettings:PASSWORD" "<secret_password>"
dotnet user-secrets set "LoginSettings:APP_KEY" "<secret_app_key>"
```

Now run the bad-each-way-finder-api.

Swagger will load on start up.

In order to use swagger, you will need to login and generate a token. You will need to register a user to this. This can register a user by running the front end. 

Once a user is registered, use the Identity/Login method 
![Swagger image of login](.\src\bad-each-way-finder-api\bad-each-way-finder-api\wwwroot\images\Identity_Login.jpg)

Enter the relevant parameters as demostrated below

```c#
{
  "username": "example@email.com",
  "password": "Example123!",
  "email": "example@email.com",
  "userRoles": [
    "user"
  ]
}
```
Copy the token in the token string in the response body and use this to access other methods.