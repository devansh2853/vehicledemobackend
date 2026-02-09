FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src/VehicleProject
COPY VehicleProject/ .
RUN dotnet publish "VehicleProject.csproj" -c Release -o /published /p:UseAppHost=false


FROM base AS final
WORKDIR /app
COPY --from=build /published .
ENTRYPOINT [ "dotnet", "VehicleProject.dll" ]