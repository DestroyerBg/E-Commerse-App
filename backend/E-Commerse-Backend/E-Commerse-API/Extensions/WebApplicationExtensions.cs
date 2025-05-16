using System.Text.Json;
using E_Commerse_Data;
using E_Commerse_Data.DTO;
using E_Commerse_Data.Models;
using Microsoft.EntityFrameworkCore;

namespace E_Commerse_API.Extensions
{
    public static class WebApplicationExtensions
    {
        public static IApplicationBuilder ApplyMigrations(this IApplicationBuilder app)
        {
            using (IServiceScope scope = app.ApplicationServices.CreateScope())
            {
                ApplicationDbContext context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                context.Database.Migrate();
            }

            return app;
        }

        public async static Task<IApplicationBuilder> SeedProducts(this IApplicationBuilder app)
        {
            string filePath =
                Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "../../../", "data.json"));

            string json = File.ReadAllText(filePath);

            List<ProductDTO>? productsDtos = JsonSerializer.Deserialize<List<ProductDTO>>(json, new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true
            });

            List<Product> products = productsDtos.Select(p => new Product()
            {
                Id = p.Id,
                Title = p.Title,
                Price = p.Price,
                Description = p.Description,
                Category = p.Category,
                ImageUrl = p.Image,
                RatingRate = p.Rating.Rate,
                RatingCount = p.Rating.Count
            }).ToList();

            using (IServiceScope scope = app.ApplicationServices.CreateScope())
            {
                ApplicationDbContext context = scope
                    .ServiceProvider.
                    GetRequiredService<ApplicationDbContext>();

                if (!await context.Products.AnyAsync())
                {
                    await context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT Products ON");
                    await context.Products.AddRangeAsync(products);
                    await context.SaveChangesAsync();
                }
            }

            return app;
        }
    }
}
