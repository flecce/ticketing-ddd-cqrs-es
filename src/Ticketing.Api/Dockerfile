FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Ticketing.Api/Ticketing.Api.csproj", "Ticketing.Api/"]
RUN dotnet restore "Ticketing.Api/Ticketing.Api.csproj"
COPY . .
WORKDIR "/src/Ticketing.Api"
RUN dotnet build "Ticketing.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Ticketing.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Ticketing.Api.dll"]