FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["NewsletterApp/NewsletterApp.csproj", "NewsletterApp/"]
COPY ["NewsletterApp.Shared/NewsletterApp.Shared.csproj", "NewsletterApp.Shared/"]
RUN dotnet restore "NewsletterApp/NewsletterApp.csproj"
COPY . .
WORKDIR "/src/NewsletterApp"
RUN dotnet build "NewsletterApp.csproj" -c Release -o /app/build
RUN dotnet publish "NewsletterApp.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:7.0
EXPOSE 80
EXPOSE 443
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "NewsletterApp.dll"]
