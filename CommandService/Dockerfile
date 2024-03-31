FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /App

#build app
COPY . ./
RUN dotnet restore
RUN dotnet publish -c Release -o out

#build runtime image\
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /App
COPY --from=build-env /App/out ./
ENTRYPOINT ["dotnet", "CommandService.dll"]