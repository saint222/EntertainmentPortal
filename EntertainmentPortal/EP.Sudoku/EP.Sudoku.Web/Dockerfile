FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY EP.Sudoku.Web/EP.Sudoku.Web.csproj EP.Sudoku.Web/
COPY EP.Sudoku.Logic/EP.Sudoku.Logic.csproj EP.Sudoku.Logic/
COPY EP.Sudoku.Data/EP.Sudoku.Data.csproj EP.Sudoku.Data/
RUN dotnet restore EP.Sudoku.Web/EP.Sudoku.Web.csproj
COPY . .
WORKDIR /src/EP.Sudoku.Web
RUN dotnet build EP.Sudoku.Web.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish EP.Sudoku.Web.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "EP.Sudoku.Web.dll"]
