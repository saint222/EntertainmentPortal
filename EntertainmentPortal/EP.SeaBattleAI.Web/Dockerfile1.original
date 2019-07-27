FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["EP.SeaBattle.Web/EP.SeaBattle.Web.csproj", "EP.SeaBattle.Web/"]
COPY ["EP.SeaBattle.Logic/EP.SeaBattle.Logic.csproj", "EP.SeaBattle.Logic/"]
COPY ["EP.SeaBattle.Common/EP.SeaBattle.Common.csproj", "EP.SeaBattle.Common/"]
COPY ["EP.SeaBattle.Data/EP.SeaBattle.Data.csproj", "EP.SeaBattle.Data/"]
RUN dotnet restore "EP.SeaBattle.Web/EP.SeaBattle.Web.csproj"
COPY . .
WORKDIR /src/EP.SeaBattle.Web
RUN dotnet build EP.SeaBattle.Web.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish "EP.SeaBattle.Web.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "EP.SeaBattle.Web.dll"]
