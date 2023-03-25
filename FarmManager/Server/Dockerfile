#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
ENV ASPNETCORE_ENVIRONMENT=Development
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ENV ASPNETCORE_ENVIRONMENT=Development
WORKDIR /src
COPY ["FarmManager/Server/FarmManager.Server.csproj", "FarmManager/Server/"]
RUN dotnet restore "FarmManager/Server/FarmManager.Server.csproj"
COPY . ./
WORKDIR "FarmManager/Server"
RUN dotnet build "FarmManager.Server.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FarmManager.Server.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FarmManager.Server.dll"]
