FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["NewsletterApp/NewsletterApp.csproj", "NewsletterApp/"]
RUN dotnet restore "NewsletterApp/NewsletterApp.csproj"
COPY . .
WORKDIR "/src/NewsletterApp"
RUN dotnet build "NewsletterApp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "NewsletterApp.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NewsletterApp.dll"]
