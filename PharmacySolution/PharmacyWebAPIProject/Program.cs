using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PharmacyWebAPIProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PharmacyWebAPIProject
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();

            using (var db = new MedicamentContext())
            {
                MedicamentModel med1 = new MedicamentModel { Title = "Супрадин", Price = 546 };
                MedicamentModel med2 = new MedicamentModel { Title = "Комбилипен", Price = 253 };
                MedicamentModel med3 = new MedicamentModel { Title = "Аквадетрим", Price = 206 };

                db.Medicaments.Add(med1);
                db.Medicaments.Add(med2);
                db.Medicaments.Add(med3);

                Console.WriteLine("Объекты успешно сохранены");

                var medicaments = db.Medicaments;
                Console.WriteLine("Список объектов:");
                foreach (MedicamentModel m in medicaments)
                {
                    Console.WriteLine("{0}.{1} - {2}", m.Id, m.Title, m.Price);
                }
            }
            Console.ReadKey();

            

        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
