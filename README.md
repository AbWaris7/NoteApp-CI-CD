# Notes App – GitHub Actions CI

A small ASP.NET Core .NET 10 Minimal API for creating, viewing, updating, and deleting notes.

## Run locally

```bash
dotnet restore NotesApi.Web/NotesApi.Web.csproj
dotnet build NotesApi.Web/NotesApi.Web.csproj --no-restore
dotnet run --project NotesApi.Web/NotesApi.Web.csproj
```

Open the URL printed by `dotnet run`, then try:

- `GET /api/notes`
- `GET /api/notes/1`
- `POST /api/notes`
- `PUT /api/notes/1`
- `DELETE /api/notes/1`

Example JSON for POST or PUT:

```json
{
  "title": "Study CI/CD",
  "content": "Practise GitHub Actions."
}
```

## CI workflow

The workflow in `.github/workflows/ci.yml` runs when code is pushed to `main` or manually started from the GitHub Actions tab. It checks out the repository, sets up .NET 10, restores dependencies, and builds the API.

> Notes are stored in memory and reset when the application restarts. This is a learning project, not production storage.
