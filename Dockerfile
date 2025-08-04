FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# 1. Copiar solution e projetos (cache de restore)
COPY ["src/ImoveisConnect.sln", "."]
COPY ["src/ImoveisConnect.API/ImoveisConnect.API.csproj", "ImoveisConnect.API/"]
COPY ["src/ImoveisConnect.Application/ImoveisConnect.Application.csproj", "ImoveisConnect.Application/"]
COPY ["src/ImoveisConnect.Domain/ImoveisConnect.Domain.csproj", "ImoveisConnect.Domain/"]
COPY ["src/ImoveisConnect.Infra/ImoveisConnect.Infra.csproj", "ImoveisConnect.Infra/"]

# 2. Restaurar pacotes
RUN dotnet restore "ImoveisConnect.sln"

# 3. Copiar todo o código fonte
COPY ["src", "src"]

# 4. Build explícito para diagnóstico
WORKDIR "/src/ImoveisConnect.API"
RUN dotnet build "ImoveisConnect.API.csproj" -c Release --no-restore -v diag

# 5. Publicar com parâmetros adicionais
RUN dotnet publish "ImoveisConnect.API.csproj" \
    -c Release \
    -o /app/publish \
    --no-build \
    --verbosity diag \
    /p:GenerateProgramFile=false

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "ImoveisConnect.API.dll"]