using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Surf.Areas.Identity.Data;
using SurfApi.Models;
using System;
using System.Linq;
using static System.Net.WebRequestMethods;

namespace SurfApi.Data
{
    public static class SeedData
    {
        public static async Task InitializeAsync(IServiceProvider serviceProvider)
        {
            using (var context = new SurfApiContext(serviceProvider.GetRequiredService<DbContextOptions<SurfApiContext>>()))
            {
                // Look for any movies.
                if (!context.Surfboard.Any())
                {
                    //context.Surfboard.RemoveRange(context.Surfboard); // Remove all records from the table
                    //context.SaveChanges();
                    //return;
                    context.Surfboard.AddRange(

                    new Surfboard
                    {
                        Name = "The Minilog",
                        Length = 6,
                        Width = 21,
                        Thickness = 2.75,
                        Volume = 38.8,
                        Price = 565,
                        Type = "Shortboard",
                        Equipment = " ",
                        Image = "/Images/surfboard1.jpg"
                    },

                new Surfboard
                {
                    Name = "The Wide Glider",
                    Length = 7.1,
                    Width = 21.75,
                    Thickness = 2.75,
                    Volume = 44.16,
                    Price = 685,
                    Type = "Funboard",
                    Equipment = " ",
                    Image = "/Images/surfboard2.jpg"
                },

                new Surfboard
                {
                    Name = "The Golden Ratio",
                    Length = 6.3,
                    Width = 21.85,
                    Thickness = 2.9,
                    Volume = 43.22,
                    Price = 695,
                    Type = "Funboard",
                    Equipment = " ",
                    Image = "/Images/surfboard3.jpg"
                },

                new Surfboard
                {
                    Name = "Mahi Mahi",
                    Length = 5.4,
                    Width = 20.75,
                    Thickness = 2.3,
                    Volume = 29.39,
                    Price = 645,
                    Type = "Fish",
                    Equipment = " ",
                    Image = "/Images/surfboard4.jpg"
                },

                new Surfboard
                {
                    Name = "The Emerald",
                    Length = 9.2,
                    Width = 22.8,
                    Thickness = 2.8,
                    Volume = 65.4,
                    Price = 895,
                    Type = "Longboard",
                    Equipment = " ",
                    Image = "/Images/surfboard5.jpg"
                },

                new Surfboard
                {
                    Name = "The Bomb",
                    Length = 5.5,
                    Width = 21,
                    Thickness = 2.5,
                    Volume = 33.7,
                    Price = 645,
                    Type = "Shortboard",
                    Equipment = " ",
                    Image = "/Images/surfboard6.jpeg"
                },

                new Surfboard
                {
                    Name = "Walden Magic",
                    Length = 9.6,
                    Width = 19.4,
                    Thickness = 3,
                    Volume = 80,
                    Price = 1025,
                    Type = "Longboard",
                    Equipment = " ",
                    Image = "/Images/surfboard7.jpg"
                },

                new Surfboard
                {
                    Name = "Naish One",
                    Length = 12.6,
                    Width = 30,
                    Thickness = 6,
                    Volume = 301,
                    Price = 854,
                    Type = "SUP",
                    Equipment = "Paddle",
                    Image = "/Images/surfboard8.jpg"
                },

                new Surfboard
                {
                    Name = "Six Tourer",
                    Length = 11.6,
                    Width = 32,
                    Thickness = 6,
                    Volume = 270,
                    Price = 611,
                    Type = "SUP",
                    Equipment = "Fin, Paddle, Pump, Leash",
                    Image = "/Images/surfboard9.jpeg"
                },

                new Surfboard
                {
                    Name = "Naish Maliko",
                    Length = 14,
                    Width = 25,
                    Thickness = 6,
                    Volume = 330,
                    Price = 1304,
                    Type = "SUP",
                    Equipment = "Fin, Paddle, Pump, Leash",
                    Image = "/Images/surfboard10.jpeg"
                });

                    context.SaveChanges();
                }
            }



        }
    }
}

