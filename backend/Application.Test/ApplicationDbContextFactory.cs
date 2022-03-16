using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Data.Sqlite;
using System.Collections.Generic;
using Domain.Entities;

namespace Application.Test
{
    public static class ApplicationDbContextFactory
    {
        public static ApplicationDbContext Create()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning))
                .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

            var context = new ApplicationDbContext(options);

            if (context != null)
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
                SeedSampleData(context);
            }

            return context;
        }

        internal static void Destroy(ApplicationDbContext context)
        {
            context.Database.EnsureDeleted();
            context.Dispose();
        }

        /*Add Tets Data here.*/
        public static void SeedSampleData(ApplicationDbContext context)
        {
            List<Organiser> organisers = new List<Organiser>()
            {
                new Organiser()
                {
                    Address = new Address()
                    {
                        Appartment = "st tv",
                        City = "City 1",
                        Street = "Street 1",
                        Number = "1",
                        PostalCode = "1234"
                    },
                    Name = "Organiser 1",
                    Description = "Description 1"
                },
                new Organiser()
                {
                    Address = new Address()
                    {
                        Appartment = "st tv",
                        City = "City 1",
                        Street = "Street 1",
                        Number = "1",
                        PostalCode = "1234"
                    },
                    Name = "Organiser 2",
                    Description = "Description 1"
                },
                new Organiser()
                {
                    Address = new Address()
                    {
                        Appartment = "st tv",
                        City = "City 1",
                        Street = "Street 1",
                        Number = "1",
                        PostalCode = "1234"
                    },
                    Name = "Organiser 3",
                    Description = "Description 1"
                },
                new Organiser()
                {
                    Address = new Address()
                    {
                        Appartment = "st tv",
                        City = "City 1",
                        Street = "Street 1",
                        Number = "1",
                        PostalCode = "1234"
                    },
                    Name = "Organiser 4",
                    Description = "Description 1"
                },
                new Organiser()
                {
                    Address = new Address()
                    {
                        Appartment = "st tv",
                        City = "City 1",
                        Street = "Street 1",
                        Number = "1",
                        PostalCode = "1234"
                    },
                    Name = "Organiser 5",
                    Description = "Description 1"
                },
                new Organiser()
                {
                    Address = new Address()
                    {
                        Appartment = "st tv",
                        City = "City 1",
                        Street = "Street 1",
                        Number = "1",
                        PostalCode = "1234"
                    },
                    Name = "Organiser 6",
                    Description = "Description 1"
                },
                new Organiser()
                {
                    Address = new Address()
                    {
                        Appartment = "st tv",
                        City = "City 1",
                        Street = "Street 1",
                        Number = "1",
                        PostalCode = "1234"
                    },
                    Name = "Organiser 7",
                    Description = "Description 1"
                },
                new Organiser()
                {
                    Address = new Address()
                    {
                        Appartment = "st tv",
                        City = "City 1",
                        Street = "Street 1",
                        Number = "1",
                        PostalCode = "1234"
                    },
                    Name = "Organiser 8",
                    Description = "Description 1"
                },
                new Organiser()
                {
                    Address = new Address()
                    {
                        Appartment = "st tv",
                        City = "City 1",
                        Street = "Street 1",
                        Number = "1",
                        PostalCode = "1234"
                    },
                    Name = "Organiser 9",
                    Description = "Description 1"
                }
            };
            context.Organisers.AddRange(organisers);
            context.SaveChanges();
        }
    }
}
