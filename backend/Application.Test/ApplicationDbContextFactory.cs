using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Data.Sqlite;
using System.Collections.Generic;
using Domain.Entities;
using Microsoft.Extensions.Options;
using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.Identity;

namespace Application.Test
{
    public static class ApplicationDbContextFactory
    {
        public static ApplicationDbContext Create()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning))
                .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

            var someOptions = Options.Create(new OperationalStoreOptions());

            var context = new ApplicationDbContext(options, someOptions, null); //TODO this doesn't work.

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
            context.Users.Add(new Domain.Common.ApplicationUser()
            {
                Id = Guid.Empty.ToString(),
                Email = "test@test"
            });
            context.Users.Add(new Domain.Common.ApplicationUser()
            {
                Id = "user2",
                Email = "user2@test"
            });
            context.SaveChanges();

            var User1 = new Domain.Entities.User()
            {
                IdentityId = Guid.Empty.ToString(),
                Email = "test@test",
                Country = "Test Country",
                DateOfBirth = new DateTimeOffset(2000, 1, 1, 0, 0, 0, TimeSpan.Zero),
                LastName = "Test_Lastname",
                FirstName = "Test_Firstname",
                Phone = "12345678"
            };
            var User2 = new Domain.Entities.User()
            {
                IdentityId = "user2",
                Email = "user2@test",
                Country = "Test Country",
                DateOfBirth = new DateTimeOffset(2000, 1, 1, 0, 0, 0, TimeSpan.Zero),
                LastName = "Test_Lastname",
                FirstName = "Test_Firstname",
                Phone = "12345678"
            };
            context.UserInfo.Add(User1);
            context.UserInfo.Add(User2);
            context.SaveChanges();

            context.Organisers.Add(new Organiser()
            {
                Id = 1,
                Name = "Testaniser",
                Description = "Test organiser.",
                User = User1,
                UserId = User1.IdentityId,
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
                    Description = "Description 1",
                    User = User2
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
                    Description = "Description 1",
                    User = User2
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
                    Description = "Description 1",
                    User = User2
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
                    Description = "Description 1",
                    User = User2
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
                    Description = "Description 1",
                    User = User2
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
                    Description = "Description 1",
                    User = User2
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
                    Description = "Description 1",
                    User = User2
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
                    Description = "Description 1",
                    User = User2
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
                    Description = "Description 1",
                    User = User2
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
            context.SaveChanges();

            context.MarketTemplates.Add(new MarketTemplate()
            {
                Id = 2,
                OrganiserId = 1,
                Name = "Test Market 2",
                Description = "Test Description 2"
            });
            List<MarketInstance> instances = new List<MarketInstance>() {
                new MarketInstance(){
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now.AddDays(1),
                    MarketTemplateId = 2,
                    IsCancelled = true
                },
                new MarketInstance(){
                    StartDate = DateTime.Now.AddDays(10),
                    EndDate = DateTime.Now.AddDays(11),
                    MarketTemplateId = 2,
                    IsCancelled = true
                },
                new MarketInstance(){
                    StartDate = DateTime.Now.AddDays(12),
                    EndDate = DateTime.Now.AddDays(17),
                    MarketTemplateId = 2,
                    IsCancelled = true
                },
            };
            context.MarketInstances.AddRange(instances);
            context.SaveChanges();
            //Test date range
            context.MarketTemplates.Add(new MarketTemplate()
            {
                Id = 3,
                OrganiserId = 1,
                Name = "Test Market 3",
                Description = "Test Description 3"
            });
            List<MarketInstance> dateRangeFilterInstances = new List<MarketInstance>() {
                new MarketInstance(){
                    StartDate = new DateTimeOffset(new DateTime(1980, 1, 1)),
                    EndDate = new DateTimeOffset(new DateTime(1980, 1, 3)),
                    MarketTemplateId = 3,
                    IsCancelled = false
                },
                new MarketInstance(){
                    StartDate = new DateTimeOffset(new DateTime(1980, 2, 10)),
                    EndDate = new DateTimeOffset(new DateTime(1980, 2, 13)),
                    MarketTemplateId = 3,
                    IsCancelled = false
                },
                new MarketInstance(){
                    StartDate = new DateTimeOffset(new DateTime(1980, 3, 11)),
                    EndDate = new DateTimeOffset(new DateTime(1980, 3, 15)),
                    MarketTemplateId = 3,
                    IsCancelled = false
                },
                    new MarketInstance(){
                    StartDate = new DateTimeOffset(new DateTime(1980, 4, 11)),
                    EndDate = new DateTimeOffset(new DateTime(1980, 4, 15)),
                    MarketTemplateId = 3,
                    IsCancelled = false
                },
                                                new MarketInstance(){
                    StartDate = new DateTimeOffset(new DateTime(1980, 5, 11)),
                    EndDate = new DateTimeOffset(new DateTime(1980, 5, 15)),
                    MarketTemplateId = 3,
                    IsCancelled = false
                },
                    new MarketInstance(){
                    StartDate = new DateTimeOffset(new DateTime(1980, 6, 11)),
                    EndDate = new DateTimeOffset(new DateTime(1980, 6, 15)),
                    MarketTemplateId = 3,
                    IsCancelled = false
                },
            };
            context.MarketInstances.AddRange(dateRangeFilterInstances);
            context.SaveChanges();

        }
    }
}
