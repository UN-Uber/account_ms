#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.
FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build-env
WORKDIR /app

EXPOSE 5005

COPY *.csproj ./
RUN dotnet restore

COPY . ./
RUN dotnet publish -c Release -o out


FROM mcr.microsoft.com/dotnet/aspnet:5.0 
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "account_ms.dll"]

CMD ASPNETCORE_URLS=http://+:5005



