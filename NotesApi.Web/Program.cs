using NotesApi.Web.Models;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

var notes = new List<Note>
{
    new() { Id = 1, Title = "Learn GitHub Actions", Content = "Create my first CI workflow." },
    new() { Id = 2, Title = "Practise .NET", Content = "Build a small Web API." }
};
var nextId = 3;

app.MapGet("/", () => "Notes API is running!");

app.MapGet("/api/notes", () => Results.Ok(notes));

app.MapGet("/api/notes/{id:int}", (int id) =>
{
    var note = notes.FirstOrDefault(n => n.Id == id);
    return note is null ? Results.NotFound() : Results.Ok(note);
});

app.MapPost("/api/notes", (Note input) =>
{
    if (string.IsNullOrWhiteSpace(input.Title))
        return Results.BadRequest("Title is required.");

    var note = new Note
    {
        Id = nextId++,
        Title = input.Title,
        Content = input.Content
    };

    notes.Add(note);
    return Results.Created($"/api/notes/{note.Id}", note);
});

app.MapPut("/api/notes/{id:int}", (int id, Note input) =>
{
    var note = notes.FirstOrDefault(n => n.Id == id);
    if (note is null) return Results.NotFound();
    if (string.IsNullOrWhiteSpace(input.Title))
        return Results.BadRequest("Title is required.");

    note.Title = input.Title;
    note.Content = input.Content;
    return Results.Ok(note);
});

app.MapDelete("/api/notes/{id:int}", (int id) =>
{
    var note = notes.FirstOrDefault(n => n.Id == id);
    if (note is null) return Results.NotFound();

    notes.Remove(note);
    return Results.NoContent();
});

app.Run();
