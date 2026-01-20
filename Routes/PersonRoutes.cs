using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Person.Data;
using Person.Models;

namespace Person.Routes;

public static class PersonRoute
{
    public static void PersonRoutes(this WebApplication app)
    {
        var route = app.MapGroup("/person");

        route.MapPost("/", async (PersonRequest req, PersonContext context) =>
        {
           var person = new PersonModel(req.Name, req.Cpf);
           context.People.Add(person); 
           await context.AddAsync(person);
           await context.SaveChangesAsync();
        });

        route.MapGet("", async (PersonContext context) =>
        {
            var people = await context.People.ToListAsync();
            return Results.Ok(people);
        });

        route.MapPut("/{id:guid}", async (Guid id, PersonRequest req, PersonContext context) =>
        {
            var person =  await context.People.FirstOrDefaultAsync(p => p.Id == id);
            if (person is null)            
                return Results.NotFound();
            person.ChangeName(req.Name);
            await context.SaveChangesAsync();
            return Results.Ok(person);
        });

        route.MapDelete("/{id:guid}", async (Guid id, PersonContext context) =>
        {
            var person =  await context.People.FirstOrDefaultAsync(p => p.Id == id);
            if (person is null)            
                return Results.NotFound();
            person.Deactivate();
            await context.SaveChangesAsync();
            return Results.Ok(person);
        });
        
    }
}