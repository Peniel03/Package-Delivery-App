FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["ShipmentService.Api/ShipmentService.Api.csproj", "ShipmentService.Api/"]
RUN dotnet restore "ShipmentService.Api/ShipmentService.Api.csproj"
COPY . .
WORKDIR "/src/ShipmentService.Api"
RUN dotnet build "ShipmentService.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ShipmentService.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ShipmentService.Api.dll"]
