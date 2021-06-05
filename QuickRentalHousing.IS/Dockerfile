#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 433

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["QuickRentalHousing.IS/QuickRentalHousing.IS.csproj", "QuickRentalHousing.IS/"]
RUN dotnet restore "QuickRentalHousing.IS/QuickRentalHousing.IS.csproj"
COPY . .
WORKDIR "/src/QuickRentalHousing.IS"
RUN dotnet build "QuickRentalHousing.IS.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "QuickRentalHousing.IS.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "QuickRentalHousing.IS.dll"]
