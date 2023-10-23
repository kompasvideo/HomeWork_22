using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace HomeWork_22.Models
{
    public static class SeedData
    {
        public static void EnsurePopulated(IApplicationBuilder app)
        {
            ApplicationDBContext context = app.ApplicationServices.GetRequiredService<ApplicationDBContext>();
            context.Database.Migrate();
            if (!context.PhoneBooks.Any())
            {
                context.PhoneBooks.AddRange(
                new PhoneBook
                {
                    LastName = "Петров",
                    FirstName = "Алексей",
                    ThreeName = "Алексеевич",
                    NumberPhone = "8-927-45-12-34",
                    Address = "ул. Забелина 3с1 - 15",
                    Description = "Директор"
                }, new PhoneBook
                {
                    LastName = "Иванов",
                    FirstName = "Сергей",
                    ThreeName = "Сергеевич",
                    NumberPhone = "8-917-456-78-45",
                    Address = "Колпачный пер. 6с4 - 21",
                    Description = "Заместитель директора"
                },
                new PhoneBook
                {
                    LastName = "Крылов",
                    FirstName = "Никита",
                    ThreeName = "Никитович",
                    NumberPhone = "8-903-456-96-32",
                    Address = "ул. Покровка 7/9-11к1 - 23",
                    Description = "Начальник отдела"
                },
                new PhoneBook
                {
                    LastName = "Юдина",
                    FirstName = "Татьяна",
                    ThreeName = "Ивановна",
                    NumberPhone = "8-962-123-45-67",
                    Address = "Покровский бульвар 4/17с1 - 54",
                    Description = "Секретарь"
                });
                context.SaveChanges();
            }
        }
    }
}
