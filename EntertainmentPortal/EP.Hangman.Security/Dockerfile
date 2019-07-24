FROM mcr.microsoft.com/dotnet/core/aspnet:2.1-alpine

EXPOSE 80

WORKDIR /app

COPY ./bin/publish ./

ENTRYPOINT ["dotnet","EP.Hangman.Security.dll"]