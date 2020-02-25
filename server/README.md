# Docker

Build Container
```
docker build -t classroom-service .
```

Tag and Push to Repo
```
docker tag classroom-service quay.io/<repo>/classroom-service
docker push quay.io/<repo>/classroom-service  
```

Run Container
```
docker run -d --name classroom classroom
```

View Logs
```
docker logs classroom
```

## Sonar Scanning

Install sonar scanner tool
```
dotnet tool install --global dotnet-sonarscanner
```

Begin Sonar Scanning
```
dotnet sonarscanner begin /k:"reference-dotnetcore-webapi" /d:"sonar.host.url=<host>"
dotnet build Classroom.sln
dotnet sonarscanner end /d:"sonar.login=<token>"
```



