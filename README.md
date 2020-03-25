# Just-Chat

### Description:
  Just a simple chat with rooms. Technologies used: ASP.NET Core Web Api 3.1, MsSQl Server, Angular 8.1.3, Docker,
  Entity Framework Core 3.1.2, SignalR, xUnit, FluentValidation, Mediatr, Angular Material
  
### Startup:
  -> To start the API run in Backend folder:
```
docker-compose up
```

 -> If there are any errors run in powershell:
```
docker stop $(docker ps -a -q)
docker rm $(docker ps -a -q)
```

  -> To start the Client run in WebClient folder:
```
ng serve -o
```
