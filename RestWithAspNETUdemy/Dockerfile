FROM mcr.microsoft.com/dotnet/core/aspnet:2.2 AS base
WORKDIR /app
ENV ASPNETCORE_URLS http://+:5000;https://+:5001
EXPOSE 5000
EXPOSE 5001

FROM mcr.microsoft.com/dotnet/core/sdk:2.2 AS build
WORKDIR /src
COPY ["RestWithAspNETUdemy.csproj", "./"]
RUN dotnet restore "./RestWithAspNETUdemy.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "RestWithAspNETUdemy.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "RestWithAspNETUdemy.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "RestWithAspNETUdemy.dll"]
