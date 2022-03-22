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
            context.Organisers.Add(new Organiser()
            {
                Id = 1,
                Name = "Testaniser",
                Description = "Test organiser.",
                Address = new Address()
                {
                    Street = "Test street 1",
                    City = "test City 1",
                    PostalCode = "TEst postal",
                    Number = "2",
                    Appartment = "st tv"
                }

            });
            context.SaveChanges();
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
            context.MarketTemplates.Add(new MarketTemplate()
            {
                Id = 1,
                OrganiserId = 1,
                Name = "Test Market 1",
                Description = "Test Description 1"
            });
            context.MarketInstances.Add(new MarketInstance()
            {
                Id=1,
                StartDate=DateTime.Now,
                EndDate=DateTime.Now.AddDays(1),
                MarketTemplateId = 1
            });
            context.MarketTemplates.Add(new MarketTemplate()
            {
                Id = 2,
                OrganiserId = 1,
                Name = "Test Market 2",
                Description = "Test Description 2"
            });
            context.MarketInstances.Add(new MarketInstance()
            {
                Id = 2,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(1),
                MarketTemplateId = 2
            });
            context.MarketInstances.Add(new MarketInstance()
            {
                Id = 3,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(1),
                MarketTemplateId = 2
            });
            context.MarketInstances.Add(new MarketInstance()
            {
                Id = 4,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(1),
                MarketTemplateId = 2
            });
            context.SaveChanges();
        }
    }
}
