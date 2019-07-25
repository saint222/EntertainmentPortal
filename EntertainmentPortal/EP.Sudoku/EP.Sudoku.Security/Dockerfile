FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY EP.Sudoku.Security/EP.Sudoku.Security.csproj EP.Sudoku.Security/
RUN dotnet restore EP.Sudoku.Security/EP.Sudoku.Security.csproj
COPY . .
WORKDIR /src/EP.Sudoku.Security
RUN dotnet build EP.Sudoku.Security.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish EP.Sudoku.Security.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "EP.Sudoku.Security.dll"]
