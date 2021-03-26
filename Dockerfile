FROM mcr.microsoft.com/dotnet/aspnet:5.0
RUN adduser --disabled-password --gecos '' user
USER user
COPY --chown=user:users bin/Release/net5.0/publish/ App/
ENV ASPNETCORE_URLS=http://+:5000
WORKDIR /App
ENTRYPOINT ["dotnet", "PictureManagement.dll"]
