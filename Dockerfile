FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ProyAPI_802/*.csproj ./ProyAPI_802/
RUN dotnet restore ./ProyAPI_802/ProyAPI_802.csproj
COPY . .
WORKDIR /src/ProyAPI_802
RUN dotnet publish -c Release -o /app

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app .
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080
ENTRYPOINT ["dotnet", "ProyAPI_802.dll"]
