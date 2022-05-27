FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build-env
WORKDIR /app

# Copy everything and build
COPY . ./
RUN dotnet publish CommandService -c Release -o CommandService/out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /app
COPY --from=build-env /app/CommandService/out .

# Run dotnet cli with parameter
ENTRYPOINT [ "dotnet", "CommandService.dll" ]
