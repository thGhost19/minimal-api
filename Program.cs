using Microsoft.AspNetCore.Http.HttpResults;
using MinimalApi.DTOs;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapPost("/login", (LoginDTO loginDTO) =>
{
    if (loginDTO.UserName == "adm@teste.com" && loginDTO.Password == "Senha123")
        return Results.Ok("Login com Sucesso!");
    else
        return Results.Unauthorized();

});

app.Run();

