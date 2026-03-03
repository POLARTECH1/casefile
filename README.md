# casefile
Application native pour la gestion de clients, la creation automatique de dossiers, l'envoi d'emails, etc.

## Prerequis
- Docker + Docker Compose
- .NET 10 SDK installe

## Initialiser la base de donnees (Docker)
Depuis le dossier `CaseFile` :

```bash
docker compose up -d
```

Cette commande demarre PostgreSQL sur le port `55432` (via le fichier `.env`).

## Variables d'environnement requises
- `MIGRATION_CONNECTION_STRING="Host=localhost;Port=55432;Database=casefile;Username=casefile;Password=casefile_dev_pw"`
- `DOTNET_ENVIRONMENT=Development`

Ces variables doivent etre definies dans le meme environnement (terminal/session) que celui utilise pour lancer l'application.

## Fichiers de configuration (desktop)
Le projet desktop charge:
- `casefile.desktop/appsettings.json` (base, Sqlite)
- `casefile.desktop/appsettings.Development.json` (surcharge en mode Development, Postgres)

Contenu actuel:

`appsettings.json`
```json
{
  "Database": {
    "Provider": "Sqlite",
    "ConnectionString": "Data Source=casefile.db"
  }
}
```

`appsettings.Development.json`
```json
{
  "Database": {
    "Provider": "Postgres",
    "ConnectionString": "Host=localhost;Port=55432;Database=casefile;Username=casefile;Password=casefile_dev_pw"
  }
}
```

## Lancer l'application (Windows)
Depuis `CaseFile` (PowerShell) :

```powershell
$env:MIGRATION_CONNECTION_STRING="Host=localhost;Port=55432;Database=casefile;Username=casefile;Password=casefile_dev_pw"
$env:DOTNET_ENVIRONMENT="Development"
dotnet ef database update --project casefile.data --startup-project .\casefile.desktop\
dotnet run --project .\casefile.desktop\casefile.desktop.csproj
```

## Lancer l'application (Linux)
Depuis `CaseFile` (bash) :

```bash
export MIGRATION_CONNECTION_STRING="Host=localhost;Port=55432;Database=casefile;Username=casefile;Password=casefile_dev_pw"
export DOTNET_ENVIRONMENT=Development
dotnet ef database update --project casefile.data --startup-project ./casefile.desktop/
dotnet run --project ./casefile.desktop/casefile.desktop.csproj
```
