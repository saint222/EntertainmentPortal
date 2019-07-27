FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-alpine

EXPOSE 80

WORKDIR /app

COPY ./bin/publish ./

ENTRYPOINT ["dotnet","EP.Balda.Web.dll"]