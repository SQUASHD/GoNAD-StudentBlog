FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["StudentBlogAPI/StudentBlogAPI.csproj", "StudentBlogAPI/"]
RUN dotnet restore "StudentBlogAPI/StudentBlogAPI.csproj"
COPY . .
WORKDIR "/src/StudentBlogAPI"
RUN dotnet build "StudentBlogAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "StudentBlogAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "StudentBlogAPI.dll"]
