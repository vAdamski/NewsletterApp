FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["NewsletterApp.MailSender/NewsletterApp.MailSender.csproj", "NewsletterApp.MailSender/"]
COPY ["NewsletterApp.Shared/NewsletterApp.Shared.csproj", "NewsletterApp.Shared/"]
RUN dotnet restore "NewsletterApp.MailSender/NewsletterApp.MailSender.csproj"
COPY . .
WORKDIR "/src/NewsletterApp.MailSender"
RUN dotnet build "NewsletterApp.MailSender.csproj" -c Release -o /app/build
RUN dotnet publish "NewsletterApp.MailSender.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:7.0
EXPOSE 80
EXPOSE 443
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "NewsletterApp.MailSender.dll"]
