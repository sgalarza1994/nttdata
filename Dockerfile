FROM mcr.microsoft.com/dotnet/aspnet:5.0 as base 
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["NttDataApi.csproj", "./"]
COPY ["nuget.config", "./"]
RUN dotnet restore "NttDataApi.csproj"
COPY . .
WORKDIR "/src"
RUN dotnet build "NttDataApi.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "NttDataApi.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet" , "/app/NttDataApi.dll"]


