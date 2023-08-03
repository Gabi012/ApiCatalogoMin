using APICatalogoMin.Context;
using APICatalogoMin.Models;
using Microsoft.EntityFrameworkCore;

namespace APICatalogoMin.ApiEndpoints
{
    public static class CategoriasEndpoints
    {
        public static void MapCategoriasEndpoints(this WebApplication app)
        {

            app.MapPost("/categorias", async (Categoria categoria, AppDbContext db) =>
            {
                db.Categorias.Add(categoria);
                await db.SaveChangesAsync();

                return Results.Created($"/categorias/{categoria.CategoriaId}", categoria);

            });

            app.MapGet("/categorias", async (AppDbContext db) =>
            {
                return await db.Categorias.ToListAsync();
            }).WithTags("Categorias").RequireAuthorization();

            app.MapGet("/categorias/{id:int}", async (int id, AppDbContext db) =>
            {
                //return await db.Categorias.FindAsync(id)
                //is Categoria categoria ? Results.Ok() : Results.NotFound();
                var cat = await db.Categorias.FindAsync(id);

                if (cat is null)
                {
                    return Results.NotFound();
                }
                return Results.Ok(cat);
            });

            app.MapPut("/categoria{id:int}", async (int id, Categoria cat, AppDbContext db) => {
                if (cat.CategoriaId != id)
                {
                    return Results.BadRequest();
                }
                var obj = await db.Categorias.FindAsync(id);
                if (obj is null) Results.NotFound();

                obj.Nome = cat.Nome;
                obj.Descricao = cat.Descricao;

                db.Categorias.Update(obj);
                await db.SaveChangesAsync();

                return Results.Ok(cat.CategoriaId);
            });

            app.MapDelete("/categorias/{id:int}", async (int id, AppDbContext db) =>
            {
                var obj = await db.Categorias.FindAsync(id);

                if (obj is null) Results.NotFound();

                db.Categorias.Remove(obj);
                await db.SaveChangesAsync();

                return Results.NoContent();
            });
        }
    }
}
