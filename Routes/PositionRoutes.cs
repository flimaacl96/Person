using Microsoft.EntityFrameworkCore;
using Person.Data;
using Person.Models;
using Position.Models;

namespace Position.Routes;

public static class PositionRoutes
{
    public static void PositionsRoutes(this WebApplication app)
    {
        var route = app.MapGroup("/positions");

        route.MapGet("", async (PersonContext context) =>
        {
            var positions = await context.Position.ToListAsync();
            return Results.Ok(positions);
        });

        route.MapPost("/", async (PositionRequest req, PersonContext context) =>
        {
            var position = new PositionModel
            {
                Description = req.Description
            };

            await context.Position.AddAsync(position);
            await context.SaveChangesAsync();

            return Results.Created($"/positions/{position.Id}", position);
        });

    }
}