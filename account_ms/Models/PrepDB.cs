using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using account_ms.Data;

namespace account_ms.Models
{
    public static class PrepDB
    {
        public static void PrepPopulation(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CretateScope())
            {
                SeedData(serviceScope.ServiceProvider.GetService<DataContext>())
            }

        }

        public static void SeedData(DataContext context)
        {
            System.Console.WriteLine("Se esta haciedo la migración");
            context.Database.Migrate();

            if(!context.Clients.Any())
            {
                System.Console.WriteLine("Se agrega nueva informacion a la base vacia");
                context.Clients.AddRange(
                    new Client{
                        fName = "Alejnadro",
                        sName = "Oscar",
                        sureName = "Gomez",
                        telNumber = 3156240783,
                        active = 0,
                        email = "ogomezs@correo.com",
                        password = "pass"
                    }
                );           
            }
        }
    }
}