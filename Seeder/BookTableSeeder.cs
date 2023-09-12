using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using orderService.domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace orderService.domain.Seeder
{
    public class BookTableSeeder : IEntityTypeConfiguration<Book>
    {
     
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.HasData(
                   new Book
                   {
                       Id=1,
                       Description = "Extensively revised to accommodate the latest features that come with C# 11 and .NET 7," +
                       " this latest edition of our guide will get you coding in C# with confidence." +
                       "\r\n\r\nYou'll learn object-oriented programming, writing, testing, and debugging functions, " +
                       "implementing interfaces, and inheriting classes. Next, you'll take on .NET APIs for performing tasks like managing and querying data," +
                       " working with the filesystem, and serialization. As you progress, you'll also explore examples of cross-platform projects you can build and " +
                       "deploy, such as websites and services using ASP.NET Core",
                       IsbnNumber = "978-1803237800",
                       Price = 200,
                       Title = "C# 11 and .NET 7",

                   },
                   new Book
                   {
                       Id = 2,
                       Description = "Extensively revised to accommodate the latest features that come with C# 11 and .NET 7",
                       IsbnNumber = "978-1803237800",
                       Price = 400,
                       Title = ".NET 7 book",

                   },
                     new Book
                     {
                         Id = 3,
                         Description = "Extensively revised to accommodate the latest features that come with C# 11 and .NET 7," +
                               "this latest edition of our guide will get you coding in C# with confidence.",
                         IsbnNumber = "978-1803237800",
                         Price = 200,
                         Title = "C# 11 book",

                     },
                     new Book
                     {
                         Id = 4,
                         Description = "Extensively revised to accommodate the latest features that come with C# 11 and .NET 7," +
                       " this latest edition of our guide will get you coding in C# with confidence." +
                       "\r\n\r\nYou'll learn object-oriented programming, writing, testing, and debugging functions, ",

                         IsbnNumber = "978-1803237800",
                         Price = 200,
                         Title = "C# book",

                     },

                   new Book
                   {
                       Id = 5,
                       Description = "Extensively revised to accommodate the latest features that come with C# 11 and .NET 7," +
                       " this latest edition of our guide will get you coding in C# with confidence." +
                       "\r\n\r\nYou'll learn object-oriented programming, writing, testing, and debugging functions, " +
                       "implementing interfaces, and inheriting classes.",
                       IsbnNumber = "978-1803237800",
                       Price = 400,
                       Title = ".Net Book ",

                   }

               );
        }
    }
}
