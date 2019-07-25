FROM mcr.microsoft.com/dotnet/core/aspnet:2.2

EXPOSE 80

WORKDIR /app

COPY ./bin/publish ./

Entrypoint ["dotnet", "EP.15Puzzle.Web.dll"]