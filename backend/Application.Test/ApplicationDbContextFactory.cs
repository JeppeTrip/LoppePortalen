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
            SeedGetOrganiserTestData(context);
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

        /**
         *   Organiser Ids starting at 1000
         *   Market Ids starting at 1000
         */
        private static void SeedGetOrganiserTestData(ApplicationDbContext context)
        {
            //User to relate to the organisers.
            context.Users.Add(new Domain.Common.ApplicationUser()
            {
                Id = "GetOrganiserUser",
                Email = "GetOrganiserUser@test"
            });
            context.SaveChanges();
            //Organiser with no market
            Organiser OrganiserNoMarkets = new Organiser()
            {
                Id = 1000,
                Name = "Organiser No Markets",
                Description = "Organiser Without Markets",
                UserId = "GetOrganiserUser",
                Address = new Address()
                {
                    Street = "Organiser No Markets Street",
                    Number = "1",
                    City = "No Markets",
                    Appartment = "",
                    PostalCode = "1234"
                }
            };
            context.Organisers.Add(OrganiserNoMarkets);
            context.SaveChanges();
            
            //Organisers with one market
            //one market but no stall types or stalls.
            Organiser OrganiserOneMarketNoStalls = new Organiser()
            {
                Id = 1001,
                Name = "Organiser One Market No Stalls",
                Description = "Organiser With One Market and No Stalls",
                UserId = "GetOrganiserUser",
                Address = new Address()
                {
                    Street = "Organiser One Market No Stalls Street",
                    Number = "1",
                    City = "One Market",
                    Appartment = "",
                    PostalCode = "1234"
                }
            };
            context.Organisers.Add(OrganiserOneMarketNoStalls);
            MarketTemplate MarketTemplateNoStalls = new MarketTemplate()
            {
                Id = 1000,
                Name = "Market With No Stalls",
                Description = "Market With No Stalls",
                Organiser = OrganiserOneMarketNoStalls,
            };
            context.MarketTemplates.Add(MarketTemplateNoStalls);
            MarketInstance MarketInstanceNoStalls = new MarketInstance()
            {
                Id = 1000,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(1),
                IsCancelled = false,
                MarketTemplate = MarketTemplateNoStalls
            };
            context.MarketInstances.Add(MarketInstanceNoStalls);
            context.SaveChanges();

            //one market, stall types, but no stalls.
            Organiser OrganiserOneMarketStallTypesButNoStalls = new Organiser()
            {
                Id = 1002,
                Name = "Organiser One Market",
                Description = "With stalltypes but no actual stalls.",
                UserId = "GetOrganiserUser",
                Address = new Address()
                {
                    Street = "Organiser One Market Street",
                    Number = "1",
                    City = "One Market",
                    Appartment = "",
                    PostalCode = "1234"
                }
            };
            context.Organisers.Add(OrganiserOneMarketStallTypesButNoStalls);
            MarketTemplate MarketTemplateStallTypesButNoStalls = new MarketTemplate()
            {
                Id = 1001,
                Name = "Market With StallTypes But no stalls",
                Description = "Market With StallTypes But no stalls",
                Organiser = OrganiserOneMarketStallTypesButNoStalls,
            };
            context.MarketTemplates.Add(MarketTemplateStallTypesButNoStalls);
            MarketInstance MarketInstanceStallTypesButNoStalls = new MarketInstance()
            {
                Id = 1001,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(1),
                IsCancelled = false,
                MarketTemplate = MarketTemplateStallTypesButNoStalls
            };
            context.MarketInstances.Add(MarketInstanceStallTypesButNoStalls);
            List<StallType> stallTypesButNoStalls = new List<StallType>()
            {
                new StallType()
                {
                    Id = 1000,
                    Name = "A stall type",
                    Description = "A stall type description",
                    MarketTemplate = MarketTemplateStallTypesButNoStalls
                },
                new StallType()
                {
                    Id = 1001,
                    Name = "Another stall type",
                    Description = "Another stall type description",
                    MarketTemplate = MarketTemplateStallTypesButNoStalls
                }
            };
            context.StallTypes.AddRange(stallTypesButNoStalls);
            context.SaveChanges();
            //One market with stalls no booths.
            Organiser OrganiserOneMarketStallsNoBooths = new Organiser()
            {
                Id = 1003,
                Name = "Organiser One Market",
                Description = "With stalls but no booths.",
                UserId = "GetOrganiserUser",
                Address = new Address()
                {
                    Street = "Organiser One Market Street",
                    Number = "1",
                    City = "One Market",
                    Appartment = "",
                    PostalCode = "1234"
                }
            };
            context.Organisers.Add(OrganiserOneMarketStallsNoBooths);
            MarketTemplate MarketTemplateStallsNoBooths = new MarketTemplate()
            {
                Id = 1002,
                Name = "Market stalls no booths",
                Description = "Market stalls no booths description",
                Organiser = OrganiserOneMarketStallsNoBooths,
            };
            context.MarketTemplates.Add(MarketTemplateStallsNoBooths);
            MarketInstance MarketInstanceStallsNoBooths = new MarketInstance()
            {
                Id = 1002,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(1),
                IsCancelled = false,
                MarketTemplate = MarketTemplateStallsNoBooths
            };
            context.MarketInstances.Add(MarketInstanceStallsNoBooths);
            List<StallType> stallTypesStallsNoBooths = new List<StallType>()
            {
                new StallType()
                {
                    Id = 1002,
                    Name = "stall type 1002",
                    Description = "stall types 1002 description",
                    MarketTemplate = MarketTemplateStallsNoBooths
                },
                new StallType()
                {
                    Id = 1003,
                    Name = "stall type 1003",
                    Description = "stall types 1003 description",
                    MarketTemplate = MarketTemplateStallsNoBooths
                }
            };
            context.StallTypes.AddRange(stallTypesStallsNoBooths);
            List<Stall> stallsNoBooths = new List<Stall>()
            {
                new Stall()
                {
                    Id = 1000,
                    StallTypeId = 1002,
                    MarketInstance = MarketInstanceStallsNoBooths
                },
                new Stall()
                {
                    Id = 1001,
                    StallTypeId = 1002,
                    MarketInstance = MarketInstanceStallsNoBooths
                },
                new Stall()
                {
                    Id = 1002,
                    StallTypeId = 1003,
                    MarketInstance = MarketInstanceStallsNoBooths
                },
                new Stall()
                {
                    Id = 1003,
                    StallTypeId = 1003,
                    MarketInstance = MarketInstanceStallsNoBooths
                },
            };
            context.Stalls.AddRange(stallsNoBooths);
            //Organiser one market stalls and some booths
            Organiser OrganiserOneMarketStallsAndBooths = new Organiser()
            {
                Id = 1004,
                Name = "Organiser One Market",
                Description = "With stalls and booths.",
                UserId = "GetOrganiserUser",
                Address = new Address()
                {
                    Street = "Organiser One Market Street",
                    Number = "1",
                    City = "One Market",
                    Appartment = "",
                    PostalCode = "1234"
                }
            };
            context.Organisers.Add(OrganiserOneMarketStallsAndBooths);
            MarketTemplate MarketTemplateStallsAndBooths = new MarketTemplate()
            {
                Id = 1003,
                Name = "Market stalls and booths",
                Description = "Market stalls and booths description",
                Organiser = OrganiserOneMarketStallsAndBooths,
            };
            context.MarketTemplates.Add(MarketTemplateStallsAndBooths);
            MarketInstance MarketInstanceStallsAndBooths = new MarketInstance()
            {
                Id = 1003,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(1),
                IsCancelled = false,
                MarketTemplate = MarketTemplateStallsAndBooths
            };
            context.MarketInstances.Add(MarketInstanceStallsAndBooths);
            List<StallType> stallTypesStallsAndBooths = new List<StallType>()
            {
                new StallType()
                {
                    Id = 1004,
                    Name = "stall type 1004",
                    Description = "stall types 1004 description",
                    MarketTemplate = MarketTemplateStallsAndBooths
                },
                new StallType()
                {
                    Id = 1005,
                    Name = "stall type 1005",
                    Description = "stall types 1005 description",
                    MarketTemplate = MarketTemplateStallsAndBooths
                }
            };
            context.StallTypes.AddRange(stallTypesStallsAndBooths);
            List<Stall> stallsAndBooths = new List<Stall>()
            {
                new Stall()
                {
                    Id = 1004,
                    StallTypeId = 1004,
                    MarketInstance = MarketInstanceStallsAndBooths
                },
                new Stall()
                {
                    Id = 1005,
                    StallTypeId = 1004,
                    MarketInstance = MarketInstanceStallsAndBooths
                },
                new Stall()
                {
                    Id = 1006,
                    StallTypeId = 1005,
                    MarketInstance = MarketInstanceStallsAndBooths
                },
                new Stall()
                {
                    Id = 1007,
                    StallTypeId = 1005,
                    MarketInstance = MarketInstanceStallsAndBooths
                },
            };
            context.Stalls.AddRange(stallsAndBooths);
            Merchant getOrganiserMerchant = new Merchant()
            {
                UserId = "GetOrganiserUser",
                Name = "Get Organiser Merchant",
                Id = 1000,
                Description = "Get Organiser Merchant Description"
            };
            context.Merchants.Add(getOrganiserMerchant);
            List<Booking> bookingStallsAndBooths = new List<Booking>()
            {
                new Booking()
                {
                    Id = "Booking1000",
                    BoothName = "Booking 1000",
                    BoothDescription = "Booking 1000 description",
                    StallId = 1004,
                    Merchant = getOrganiserMerchant
                },
                new Booking()
                {
                    Id = "Booking1001",
                    BoothName = "Booking 1001",
                    BoothDescription = "Booking 1001 description",
                    StallId = 1005,
                    Merchant = getOrganiserMerchant
                }
            };
            context.Bookings.AddRange(bookingStallsAndBooths);
            context.SaveChanges();
            //Booths with item categories.
            Organiser OrganiserOneMarketItemCategories = new Organiser()
            {
                Id = 1005,
                Name = "Organiser One Market",
                Description = "With booths and item categories.",
                UserId = "GetOrganiserUser",
                Address = new Address()
                {
                    Street = "Organiser One Market Street",
                    Number = "1",
                    City = "One Market",
                    Appartment = "",
                    PostalCode = "1234"
                }
            };
            context.Organisers.Add(OrganiserOneMarketItemCategories);
            MarketTemplate MarketTemplateItemCategories = new MarketTemplate()
            {
                Id = 1004,
                Name = "Market stalls and booths and item categories",
                Description = "Market stalls and booths and item categories description",
                Organiser = OrganiserOneMarketItemCategories,
            };
            context.MarketTemplates.Add(MarketTemplateItemCategories);
            MarketInstance MarketInstanceBoothsAndCategories = new MarketInstance()
            {
                Id = 1004,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(1),
                IsCancelled = false,
                MarketTemplate = MarketTemplateItemCategories
            };
            context.MarketInstances.Add(MarketInstanceBoothsAndCategories);
            List<StallType> stallTypesItemCategories = new List<StallType>()
            {
                new StallType()
                {
                    Id = 1006,
                    Name = "stall type 1004",
                    Description = "stall types 1004 description",
                    MarketTemplate = MarketTemplateItemCategories
                },
                new StallType()
                {
                    Id = 1007,
                    Name = "stall type 1005",
                    Description = "stall types 1005 description",
                    MarketTemplate = MarketTemplateItemCategories
                }
            };
            context.StallTypes.AddRange(stallTypesItemCategories);
            List<Stall> stallsBoothsCategories = new List<Stall>()
            {
                new Stall()
                {
                    Id = 1008,
                    StallTypeId = 1006,
                    MarketInstance = MarketInstanceBoothsAndCategories
                },
                new Stall()
                {
                    Id = 1009,
                    StallTypeId = 1006,
                    MarketInstance = MarketInstanceBoothsAndCategories
                },
                new Stall()
                {
                    Id = 1010,
                    StallTypeId = 1007,
                    MarketInstance = MarketInstanceBoothsAndCategories
                },
                new Stall()
                {
                    Id = 1011,
                    StallTypeId = 1007,
                    MarketInstance = MarketInstanceBoothsAndCategories
                },
            };
            context.Stalls.AddRange(stallsBoothsCategories);
            Category cat1 = new Category()
            {
                Name = "Category 1"
            };
            Category cat2 = new Category()
            {
                Name = "Category 2"
            };
            Category cat3 = new Category()
            {
                Name = "Category 3"
            };
            Category cat4 = new Category()
            {
                Name = "Category 4"
            };
            context.ItemCategories.AddRange(new List<Category>() { 
                cat1, cat2, cat3, cat4
            });
            List<Booking> bookingItemCategories = new List<Booking>()
            {
                new Booking()
                {
                    Id = "Booking1002",
                    BoothName = "Booking 1002",
                    BoothDescription = "Booking 1002 description",
                    StallId = 1010,
                    Merchant = getOrganiserMerchant,
                    ItemCategories = new List<Category>()
                    {
                        cat1, cat2
                    }
                },
                new Booking()
                {
                    Id = "Booking1003",
                    BoothName = "Booking 1003",
                    BoothDescription = "Booking 1003 description",
                    StallId = 1011,
                    Merchant = getOrganiserMerchant,
                    ItemCategories = new List<Category>()
                    {
                        cat1, cat2, cat3, cat4
                    }
                }
            };
            context.Bookings.AddRange(bookingItemCategories);
            context.SaveChanges();
            //Multiple markets (nothing extra)
            Organiser OrganiserMultipleMarkets = new Organiser()
            {
                Id = 1006,
                Name = "Organiser Multiple markets",
                Description = "Organiser With multiple markets",
                UserId = "GetOrganiserUser",
                Address = new Address()
                {
                    Street = "Organiser multiplemarkets street",
                    Number = "1",
                    City = "Multiple Markets",
                    Appartment = "",
                    PostalCode = "1234"
                }
            };
            context.Organisers.Add(OrganiserMultipleMarkets);
            MarketTemplate MarketTemplateOne = new MarketTemplate()
            {
                Id = 1005,
                Name = "Market template 1005",
                Description = "Market template 1005 description",
                Organiser = OrganiserMultipleMarkets,
            };
            context.MarketTemplates.Add(MarketTemplateOne);
            MarketInstance MarketInstanceOne = new MarketInstance()
            {
                Id = 1005,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(1),
                IsCancelled = false,
                MarketTemplate = MarketTemplateOne
            };
            context.MarketInstances.Add(MarketInstanceOne);
            MarketTemplate MarketTemplateTwo = new MarketTemplate()
            {
                Id = 1006,
                Name = "Market template 1006",
                Description = "Market template 1006 description",
                Organiser = OrganiserMultipleMarkets,
            };
            context.MarketTemplates.Add(MarketTemplateTwo);
            MarketInstance MarketInstanceTwo = new MarketInstance()
            {
                Id = 1006,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(1),
                IsCancelled = false,
                MarketTemplate = MarketTemplateTwo
            };
            context.MarketInstances.Add(MarketInstanceTwo);
            context.SaveChanges();
        }
    }
}
