FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80/tcp

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Shoplister.csproj", "./"]
RUN dotnet restore "Shoplister.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "Shoplister.csproj" -c Release -o /app/build
FROM build AS publish
RUN dotnet publish "Shoplister.csproj" -c Release -o /app/publish
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Shoplister.dll"]