using Microsoft.EntityFrameworkCore;
using CineMoodAI.Domain.Entities;
using CineMoodAI.Infrastructure.Data;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;
namespace CineMoodAI.WebAPI.Controllers;

public static class MovieEndpoints
{
    public static void MapMovieEndpoints (this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Movie").WithTags(nameof(Movie));

        group.MapGet("/", async (CineMoodAIDbContext db) =>
        {
            return await db.Movies.ToListAsync();
        })
        .WithName("GetAllMovies")
        .WithOpenApi();

        group.MapGet("/{id}", async Task<Results<Ok<Movie>, NotFound>> (int id, CineMoodAIDbContext db) =>
        {
            return await db.Movies.AsNoTracking()
                .FirstOrDefaultAsync(model => model.Id == id)
                is Movie model
                    ? TypedResults.Ok(model)
                    : TypedResults.NotFound();
        })
        .WithName("GetMovieById")
        .WithOpenApi();

        group.MapPut("/{id}", async Task<Results<Ok, NotFound>> (int id, Movie movie, CineMoodAIDbContext db) =>
        {
            var affected = await db.Movies
                .Where(model => model.Id == id)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(m => m.Id, movie.Id)
                    .SetProperty(m => m.Title, movie.Title)
                    .SetProperty(m => m.Genre, movie.Genre)
                    .SetProperty(m => m.Description, movie.Description)
                    );
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("UpdateMovie")
        .WithOpenApi();

        group.MapPost("/", async (Movie movie, CineMoodAIDbContext db) =>
        {
            db.Movies.Add(movie);
            await db.SaveChangesAsync();
            return TypedResults.Created($"/api/Movie/{movie.Id}",movie);
        })
        .WithName("CreateMovie")
        .WithOpenApi();

        group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (int id, CineMoodAIDbContext db) =>
        {
            var affected = await db.Movies
                .Where(model => model.Id == id)
                .ExecuteDeleteAsync();
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("DeleteMovie")
        .WithOpenApi();
    }
}
