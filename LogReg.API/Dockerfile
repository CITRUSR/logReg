FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app

USER app
FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG configuration=Release
WORKDIR /src
COPY ["LogReg.API/LogReg.API.csproj", "LogReg.API/"]
RUN dotnet restore "LogReg.API/LogReg.API.csproj"
COPY . .
WORKDIR "/src/LogReg.API"
RUN dotnet build "LogReg.API.csproj" -c $configuration -o /app/build

FROM build AS publish
ARG configuration=Release
RUN dotnet publish "LogReg.API.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LogReg.API.dll"]
