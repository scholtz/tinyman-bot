#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["TinyManCronJob/TinyManCronJob.csproj", "TinyManCronJob/"]
COPY ["dotnet-tinyman-sdk/src/Tinyman/Tinyman.csproj", "dotnet-tinyman-sdk/src/Tinyman/"]
RUN dotnet restore "TinyManCronJob/TinyManCronJob.csproj"
COPY . .
WORKDIR "/src/TinyManCronJob"
RUN dotnet build "TinyManCronJob.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TinyManCronJob.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TinyManCronJob.dll"]