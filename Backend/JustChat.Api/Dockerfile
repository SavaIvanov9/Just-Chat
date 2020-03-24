#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["JustChat.Api/JustChat.Api.csproj", "JustChat.Api/"]
COPY ["JustChat.Persistence/JustChat.Persistence.csproj", "JustChat.Persistence/"]
COPY ["JustChat.Application/JustChat.Application.csproj", "JustChat.Application/"]
COPY ["JustChat.Domain/JustChat.Domain.csproj", "JustChat.Domain/"]
COPY ["JustChat.Persistence.Commands/JustChat.Persistence.Commands.csproj", "JustChat.Persistence.Commands/"]
COPY ["JustChat.Mediator/JustChat.Mediator.csproj", "JustChat.Mediator/"]
RUN dotnet restore "JustChat.Api/JustChat.Api.csproj"
COPY . .
WORKDIR "/src/JustChat.Api"
RUN dotnet build "JustChat.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "JustChat.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "JustChat.Api.dll"]