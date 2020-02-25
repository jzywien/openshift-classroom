FROM registry.access.redhat.com/ubi8/dotnet-31-runtime AS base
EXPOSE 8080
EXPOSE 8443

FROM registry.access.redhat.com/ubi8/dotnet-31 AS build
USER 0
WORKDIR /opt/app-root/src
COPY . .
RUN dotnet restore "Classroom.WebApi/Classroom.WebApi.csproj"
RUN dotnet build "Classroom.WebApi/Classroom.WebApi.csproj" -c Release -o /opt/app-root/build

FROM build AS publish
RUN dotnet publish "Classroom.WebApi/Classroom.WebApi.csproj" -c Release -o /opt/app-root/publish

FROM base AS final
RUN chown -R 1001:0 /opt/app-root 
USER 1001
WORKDIR /opt/app-root
COPY --from=publish /opt/app-root/publish .
ENTRYPOINT ["dotnet", "Classroom.WebApi.dll"]