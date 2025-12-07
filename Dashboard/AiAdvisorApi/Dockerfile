FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["AiAdvisorApi.csproj", "./"]
RUN dotnet restore "AiAdvisorApi.csproj"
COPY . .
RUN dotnet build "AiAdvisorApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AiAdvisorApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AiAdvisorApi.dll"]

