using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Person.Data;
using Person.Models;

namespace Employee.Routes;

public static class EmployeeRoutes
{
    public static void EmployeesRoutes(this WebApplication app)
    {
        var route = app.MapGroup("/employees");

        route.MapGet("/", async (PersonContext context) =>
        {
            var result = await context.Employee
                .Select(e => new EmployeeRequest(
                    e.PersonId,
                    e.Person.Name,
                    e.PositionId,
                    e.Position.Description
                ))
                .ToListAsync();

            return Results.Ok(result);
        });


        route.MapPost("/", async (EmployeeRequest req, PersonContext context) =>
        {
            // 1️⃣ Verifica se já existe (PK composta)
            var exists = await context.Employee
                .AnyAsync(e =>
                    e.PersonId == req.PersonId &&
                    e.PositionId == req.PositionId);

            if (exists)
                return Results.Conflict("Esse vínculo já existe.");

            // 2️⃣ (Opcional) valida se Person existe
            var personExists = await context.People
                .AnyAsync(p => p.Id == req.PersonId);

            if (!personExists)
                return Results.NotFound("Pessoa não encontrada.");

            // 3️⃣ (Opcional) valida se Position existe
            var positionExists = await context.Position
                .AnyAsync(p => p.Id == req.PositionId);

            if (!positionExists)
                return Results.NotFound("Cargo não encontrado.");

            // 4️⃣ Cria vínculo
            var employee = new EmployeeModel
            {
                PersonId = req.PersonId,
                PositionId = req.PositionId
            };

            context.Employee.Add(employee);
            await context.SaveChangesAsync();

            return Results.Created(
                $"/employees/{req.PersonId}/{req.PositionId}",
                employee
            );
        });

    }
}