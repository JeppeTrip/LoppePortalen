using Domain.Common;
using Domain.Entities;
using Domain.Enums;
using IdentityServer4.EntityFramework.Options;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;

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
            context.Users.Add(new ApplicationUser()
            {
                Id = Guid.Empty.ToString(),
                Email = "test@test"
            });
            context.Users.Add(new ApplicationUser()
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

            //TODO: Make seed functions for all the things that needs testing, it is much easier to keep track of!
            SeedGetOrganiserTestData(context);
            SeedEditMerchantTestData(context);
            SeedGetMerchantTestData(context);
            SeedGetUsersMerchantsQuery(context);
            SeedCreateStallTypeCommandTest(context);
            SeedEditStallTypeCommandTest(context);
            SeedGetStallType(context);
            SeedGetMarketStallTypesTest(context);
            SeedMarketInstanceEntityExtensionTestData(context);
            SeedAddStallToMarketTestData(context);
            SeedDeleteStallTestData(context);
            SeedGetMarketStallsTestData(context);
            SeedGetStallTestData(context);
            SeedGetFilteredBoothsQuery(context);
            SeedUpdateBoothCommandTestData(context);
            SeedGetBoothTestData(context);
            SeedGetUsersBoothsTestData(context);
            SeedAddOrganiserContactsTestData(context);
            SeedRemoveOrganiserContactTestData(context);
            SeedCreateMarketTestData(context);
            SeedEditMarketTestData(context);
            SeedCancelMarketTestData(context);
            SeedBookStallsTestData(context);
            SeedGetMarketInstanceQueryTestData(context);
            SeedGetUsersMarketsTestData(context);
            SeedGetFilteredMarketInstances(context);
            SeedAddMerchantContactInformationTest(context);
        }

        /**
         *   All ids within this starting at 1000.
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

            context.ContactInformations.Add(new ContactInfo()
            {
                OrganiserId = 1000,
                ContactType = 0,
                Value = "contact"
            });
            context.SaveChanges();
        }

        /** All ids within start at 2000 */
        private static void SeedEditMerchantTestData(ApplicationDbContext context)
        {
            context.Users.Add(new ApplicationUser()
            {
                Id = "EditMerchantUser",
                Email = "edit@merchant"
            });
            context.Users.Add(new ApplicationUser()
            {
                Id = "EditMerchantFakeUser",
                Email = "fake@merchant"
            });
            context.SaveChanges();

            List<Merchant> merchantList = new List<Merchant>() { 
                new Merchant()
                {
                    Id = 2000,
                    Name = "Merchant 2000",
                    Description = "Merchant 2000 description",
                    UserId = "EditMerchantUser"
                }
            };

            context.Merchants.AddRange(merchantList);
            context.SaveChanges();
        }

        /** All ids within start at 3000 */
        private static void SeedGetMerchantTestData(ApplicationDbContext context)
        {
            context.Users.Add(new ApplicationUser()
            {
                Id = "User3000",
                Email = "User3000@mail",
                UserName = "User3000@mail"
            });
            context.UserInfo.Add(new Domain.Entities.User()
            {
                IdentityId = "User3000",
                Email = "User3000@mail",
                Country = "test",
                DateOfBirth = new DateTime(1990, 1, 1),
                FirstName = "Fristname User3000",
                LastName = "Lastname User3000",
                Phone = "12345678"
            });

            List<Merchant> merchantList = new List<Merchant>() {
                new Merchant()
                {
                    Id = 3000,
                    Name = "Merchant 3000",
                    Description = "Merchant 3000 description",
                    UserId = "User3000"
                },
                new Merchant()
                {
                    Id = 3001,
                    Name = "Merchant 3001",
                    Description = "Merchant 3001 description",
                    UserId = "User3000"
                },
                new Merchant()
                {
                    Id = 3002,
                    Name = "Merchant 3002",
                    Description = "Merchant 3002 description",
                    UserId = "User3000"
                },
                new Merchant()
                {
                    Id = 3003,
                    Name = "Merchant 3003",
                    Description = "Merchant 3003 description",
                    UserId = "User3000"
                },
                new Merchant()
                {
                    Id = 3004,
                    Name = "Merchant 3004",
                    Description = "Merchant 3004 description",
                    UserId = "User3000"
                }
            };
            context.Merchants.AddRange(merchantList);

            context.Organisers.Add(new Organiser()
            {
                Id = 3000,
                Name = "Organiser 3000",
                Description = "Organiser 3000 Description",
                UserId = "User3000",
                Address = new Address()
                {
                    Id = 3000,
                    Street = "street",
                    Number = "number",
                    Appartment = "apt",
                    City = "city",
                    PostalCode = "postal"
                }
            });
            context.MarketTemplates.Add(new MarketTemplate()
            {
                Id = 3000,
                Name = "Market 3000",
                Description = "Market 3000 Description",
                OrganiserId = 3000
            });
            context.MarketInstances.Add(new MarketInstance()
            {
                Id = 3000,
                MarketTemplateId = 3000,
                StartDate = DateTimeOffset.Now.AddDays(-1),
                EndDate = DateTimeOffset.Now.AddDays(1),
                IsCancelled = false,
            });

            context.MarketTemplates.Add(new MarketTemplate()
            {
                Id = 3001,
                Name = "Market 3001",
                Description = "Market 3001 Description",
                OrganiserId = 3000
            });
            context.MarketInstances.Add(new MarketInstance()
            {
                Id = 3001,
                MarketTemplateId = 3001,
                StartDate = DateTimeOffset.Now.AddDays(-1),
                EndDate = DateTimeOffset.Now.AddDays(1),
                IsCancelled = false,
            });

            context.MarketTemplates.Add(new MarketTemplate()
            {
                Id = 3002,
                Name = "Market 3002",
                Description = "Market 3002 Description",
                OrganiserId = 3000
            });
            context.MarketInstances.Add(new MarketInstance()
            {
                Id = 3002,
                MarketTemplateId = 3002,
                StartDate = DateTimeOffset.Now.AddDays(-1),
                EndDate = DateTimeOffset.Now.AddDays(1),
                IsCancelled = false,
            });

            context.MarketTemplates.Add(new MarketTemplate()
            {
                Id = 3003,
                Name = "Market 3003",
                Description = "Market 3003 Description",
                OrganiserId = 3000
            });
            context.MarketInstances.Add(new MarketInstance()
            {
                Id = 3003,
                MarketTemplateId = 3003,
                StartDate = DateTimeOffset.Now.AddDays(-1),
                EndDate = DateTimeOffset.Now.AddDays(1),
                IsCancelled = false,
            });

            context.MarketTemplates.Add(new MarketTemplate()
            {
                Id = 3004,
                Name = "Market 3004",
                Description = "Market 3004 Description",
                OrganiserId = 3000
            });
            context.MarketInstances.Add(new MarketInstance()
            {
                Id = 3004,
                MarketTemplateId = 3004,
                StartDate = DateTimeOffset.Now.AddDays(-1),
                EndDate = DateTimeOffset.Now.AddDays(1),
                IsCancelled = true,
            });

            context.MarketTemplates.Add(new MarketTemplate()
            {
                Id = 3005,
                Name = "Market 3005",
                Description = "Market 3005 Description",
                OrganiserId = 3000
            });
            context.MarketInstances.Add(new MarketInstance()
            {
                Id = 3005,
                MarketTemplateId = 3005,
                StartDate = DateTimeOffset.Now.AddDays(-2),
                EndDate = DateTimeOffset.Now.AddDays(-1),
                IsCancelled = false,
            });

            context.StallTypes.AddRange(new StallType()
            {
                Id = 3000,
                Name = "Stalltype 3000",
                Description = "Stalltype 3000 description",
                MarketTemplateId = 3000
            },
            new StallType()
            {
                Id = 3001,
                Name = "Stalltype 3001",
                Description = "Stalltype 3001 description",
                MarketTemplateId = 3001
            },
            new StallType()
            {
                Id = 3002,
                Name = "Stalltype 3002",
                Description = "Stalltype 3002 description",
                MarketTemplateId = 3002
            },
            new StallType()
            {
                Id = 3003,
                Name = "Stalltype 3003",
                Description = "Stalltype 3003 description",
                MarketTemplateId = 3003
            },
            new StallType()
            {
                Id = 3004,
                Name = "Stalltype 3004",
                Description = "Stalltype 3004 description",
                MarketTemplateId = 3004
            },
            new StallType()
            {
                Id = 3005,
                Name = "Stalltype 3005",
                Description = "Stalltype 3005 description",
                MarketTemplateId = 3005
            });

            context.Stalls.AddRange(
                new Stall()
                {
                    Id = 3000,
                    MarketInstanceId = 3000,
                    StallTypeId = 3000
                },
                new Stall()
                {
                    Id = 3001,
                    MarketInstanceId = 3001,
                    StallTypeId = 3001
                },
                new Stall()
                {
                    Id = 3002,
                    MarketInstanceId = 3002,
                    StallTypeId = 3002
                },
                new Stall()
                {
                    Id = 3003,
                    MarketInstanceId = 3003,
                    StallTypeId = 3003
                }
                ,
                new Stall()
                {
                    Id = 3004,
                    MarketInstanceId = 3004,
                    StallTypeId = 3004
                },
                new Stall()
                {
                    Id = 3005,
                    MarketInstanceId = 3004,
                    StallTypeId = 3004
                },
                new Stall()
                {
                    Id = 3006,
                    MarketInstanceId = 3004,
                    StallTypeId = 3004
                },
                new Stall()
                {
                    Id = 3007,
                    MarketInstanceId = 3005,
                    StallTypeId = 3005
                },
                new Stall()
                {
                    Id = 3008,
                    MarketInstanceId = 3005,
                    StallTypeId = 3005
                },
                new Stall()
                {
                    Id = 3009,
                    MarketInstanceId = 3005,
                    StallTypeId = 3005
                }
            );

            context.Bookings.AddRange(new Booking()
            {
                Id = "Booth3000",
                BoothName = "Booth 3000",
                BoothDescription = "Booth 3000 Description",
                MerchantId = 3001,
                StallId = 3000,
                ItemCategories = new List<Category>()
                {
                    new Category()
                    {
                        Name = "Category 3000",
                    },
                    new Category()
                    {
                        Name = "Category 3001",
                    },
                    new Category()
                    {
                        Name = "Category 3002",
                    }
                }
            },
            new Booking()
            {
                Id = "Booth3001",
                BoothName = "Booth 3001",
                BoothDescription = "Booth 3001 Description",
                MerchantId = 3002,
                StallId = 3001
            },
            new Booking()
            {
                Id = "Booth3002",
                BoothName = "Booth 3002",
                BoothDescription = "Booth 3002 Description",
                MerchantId = 3002,
                StallId = 3002
            },
            new Booking()
            {
                Id = "Booth3003",
                BoothName = "Booth 3003",
                BoothDescription = "Booth 3003 Description",
                MerchantId = 3002,
                StallId = 3003
            },
            new Booking()
            {
                Id = "Booth3004",
                BoothName = "Booth 3004",
                BoothDescription = "Booth 3004 Description",
                MerchantId = 3003,
                StallId = 3004
            },
            new Booking()
            {
                Id = "Booth3005",
                BoothName = "Booth 3005",
                BoothDescription = "Booth 3005 Description",
                MerchantId = 3003,
                StallId = 3005
            },
            new Booking()
            {
                Id = "Booth3006",
                BoothName = "Booth 3006",
                BoothDescription = "Booth 3006 Description",
                MerchantId = 3003,
                StallId = 3006
            },
            new Booking()
            {
                Id = "Booth3007",
                BoothName = "Booth 3007",
                BoothDescription = "Booth 3007 Description",
                MerchantId = 3004,
                StallId = 3007
            },
            new Booking()
            {
                Id = "Booth3008",
                BoothName = "Booth 3008",
                BoothDescription = "Booth 3008 Description",
                MerchantId = 3004,
                StallId = 3008
            },
            new Booking()
            {
                Id = "Booth3009",
                BoothName = "Booth 3009",
                BoothDescription = "Booth 3009 Description",
                MerchantId = 3004,
                StallId = 3009
            });


            context.SaveChanges();
        }

        /** All ids within start at 4000 */
        private static void SeedGetUsersMerchantsQuery(ApplicationDbContext context)
        {
            context.Users.AddRange(new List<ApplicationUser>() {
            new ApplicationUser()
            {
                Id = "UsersMerchantsNoMerchant",
                Email = "no@merchant"
            },
            new ApplicationUser()
            {
                Id = "UsersMerchantsOneMerchant",
                Email = "one@merchant"
            },
            new ApplicationUser()
            {
                Id = "UsersMerchantsMultipleMerchant",
                Email = "mulitiple@merchant"
            }
            });
            context.SaveChanges();

            List<Merchant> merchantList = new List<Merchant>() {
                new Merchant()
                {
                    Id = 4000,
                    Name = "Merchant 4000",
                    Description = "Merchant 4000 description",
                    UserId = "UsersMerchantsOneMerchant"
                },
                new Merchant()
                {
                    Id = 4001,
                    Name = "Merchant 4001",
                    Description = "Merchant 4001 description",
                    UserId = "UsersMerchantsMultipleMerchant"
                },
                new Merchant()
                {
                    Id = 4002,
                    Name = "Merchant 4002",
                    Description = "Merchant 4002 description",
                    UserId = "UsersMerchantsMultipleMerchant"
                },
                new Merchant()
                {
                    Id = 4003,
                    Name = "Merchant 4003",
                    Description = "Merchant 4003 description",
                    UserId = "UsersMerchantsMultipleMerchant"
                }
            };
            context.Merchants.AddRange(merchantList);
            context.SaveChanges();
        }

        /** All ids within start at 5000 */
        private static void SeedCreateStallTypeCommandTest(ApplicationDbContext context)
        {
            context.Users.Add(new ApplicationUser()
            {
                Id = "CreateStallTypeUser",
                Email = "create@stalltype"
            });

            context.Users.Add(new ApplicationUser()
            {
                Id = "FakeCreateStallTypeUser",
                Email = "create@stalltype"
            });

            context.Organisers.Add(new Organiser()
            {
                Id = 5000,
                Name = "Create stalltype organiser",
                Description = "Create stalltype organiser description",
                UserId = "CreateStallTypeUser",
                Address = new Address()
                {
                    Id = 5000,
                    Appartment = "apt",
                    City = "city",
                    Street = "street",
                    Number = "number",
                    PostalCode = "postal"
                }
            });

            context.MarketTemplates.Add(new MarketTemplate()
            {
                Id = 5000,
                Name = "Create Stalltype Template",
                Description = "Create Stalltype template description",
                OrganiserId = 5000
            });

            context.MarketInstances.Add(new MarketInstance()
            {
                Id = 5000,
                MarketTemplateId = 5000,
                IsCancelled = false,
                StartDate = DateTimeOffset.Now,
                EndDate = DateTimeOffset.Now.AddDays(1)
            });

            context.StallTypes.Add(new StallType()
            {
                Id = 5000,
                Name = "Stalltype Exists",
                Description = "Stalltype Exists description",
                MarketTemplateId = 5000
            });
            context.SaveChanges();
        }

        /** All ids within start at 6000 */
        private static void SeedEditStallTypeCommandTest(ApplicationDbContext context)
        {
            context.Users.Add(new ApplicationUser()
            {
                Id = "EditStallTypeUser1",
                Email = "EditStalltype@User1"
            });


            context.Organisers.Add(new Organiser()
            {
                Id = 6000,
                Name = "Edit stalltype organiser 1",
                Description = "Edit stalltype organiser 1 description",
                UserId = "EditStallTypeUser1",
                Address = new Address()
                {
                    Id = 6000,
                    Appartment = "apt",
                    City = "city",
                    Street = "street",
                    Number = "number",
                    PostalCode = "postal"
                }
            });

            context.MarketTemplates.Add(new MarketTemplate()
            {
                Id = 6000,
                Name = "Create Stalltype Template 1",
                Description = "Create Stalltype template 1 description",
                OrganiserId = 6000
            });

            context.MarketTemplates.Add(new MarketTemplate()
            {
                Id = 6001,
                Name = "Create Stalltype Template 2",
                Description = "Create Stalltype template 2 description",
                OrganiserId = 6000
            });

            context.MarketInstances.Add(new MarketInstance()
            {
                Id = 6000,
                MarketTemplateId = 6000,
                IsCancelled = false,
                StartDate = DateTimeOffset.Now,
                EndDate = DateTimeOffset.Now.AddDays(1)
            });

            context.MarketInstances.Add(new MarketInstance()
            {
                Id = 6001,
                MarketTemplateId = 6001,
                IsCancelled = false,
                StartDate = DateTimeOffset.Now,
                EndDate = DateTimeOffset.Now.AddDays(1)
            });

            context.StallTypes.Add(new StallType()
            {
                Id = 6000,
                Name = "Edit Stalltype 1",
                Description = "Edit Stalltype 1 description",
                MarketTemplateId = 6000
            });

            context.StallTypes.Add(new StallType()
            {
                Id = 6001,
                Name = "Edit Stalltype 2",
                Description = "Edit Stalltype 2 description",
                MarketTemplateId = 6000
            });

            context.StallTypes.Add(new StallType()
            {
                Id = 6002,
                Name = "Edit Stalltype 3",
                Description = "Edit Stalltype 3 description",
                MarketTemplateId = 6001
            });

            context.Users.Add(new ApplicationUser()
            {
                Id = "EditStallTypeUser2",
                Email = "EditStalltype@User2"
            });

            context.Organisers.Add(new Organiser()
            {
                Id = 6001,
                Name = "Edit stalltype organiser 2",
                Description = "Edit stalltype organiser 2 description",
                UserId = "EditStallTypeUser2",
                Address = new Address()
                {
                    Id = 6001,
                    Appartment = "apt",
                    City = "city",
                    Street = "street",
                    Number = "number",
                    PostalCode = "postal"
                }
            });

            context.MarketTemplates.Add(new MarketTemplate()
            {
                Id = 6002,
                Name = "Create Stalltype Template 1",
                Description = "Create Stalltype template 1 description",
                OrganiserId = 6001
            });

            context.MarketInstances.Add(new MarketInstance()
            {
                Id = 6002,
                MarketTemplateId = 6002,
                IsCancelled = false,
                StartDate = DateTimeOffset.Now,
                EndDate = DateTimeOffset.Now.AddDays(1)
            });

            context.StallTypes.Add(new StallType()
            {
                Id = 6003,
                Name = "Edit Stalltype 4",
                Description = "Edit Stalltype 4 description",
                MarketTemplateId = 6002
            });

            context.SaveChanges();
        }

        /** All ids within start at 7000 */
        private static void SeedGetStallType(ApplicationDbContext context)
        {
            context.Users.Add(new ApplicationUser()
            {
                Id = "GetStallTypeYser",
                Email = "get@stalltype"
            });

            context.Organisers.Add(new Organiser()
            {
                Id = 7000,
                Name = "Get stalltype organiser",
                Description = "Get stalltype organiser description",
                UserId = "GetStallTypeYser",
                Address = new Address()
                {
                    Id = 7000,
                    Appartment = "apt",
                    City = "city",
                    Street = "street",
                    Number = "number",
                    PostalCode = "postal"
                }
            });

            context.MarketTemplates.Add(new MarketTemplate()
            {
                Id = 7000,
                Name = "Get Stalltype Template",
                Description = "Create Stalltype template description",
                OrganiserId = 7000
            });

            context.MarketInstances.Add(new MarketInstance()
            {
                Id = 7000,
                MarketTemplateId = 7000,
                IsCancelled = false,
                StartDate = DateTimeOffset.Now,
                EndDate = DateTimeOffset.Now.AddDays(1)
            });

            context.StallTypes.Add(new StallType()
            {
                Id = 7000,
                Name = "Get Stalltype",
                Description = "Get Stalltype description",
                MarketTemplateId = 7000
            });
            context.SaveChanges();
        }

        /** All ids within start at 8000 */
        private static void SeedGetMarketStallTypesTest(ApplicationDbContext context)
        {
            context.Users.Add(new ApplicationUser()
            {
                Id = "GetMarketStallTypesUser",
                Email = "getmarkets@stalltype"
            });

            context.Organisers.Add(new Organiser()
            {
                Id = 8000,
                Name = "Get markets stalltype organiser",
                Description = "Get markets stalltype organiser description",
                UserId = "GetMarketStallTypesUser",
                Address = new Address()
                {
                    Id = 8000,
                    Appartment = "apt",
                    City = "city",
                    Street = "street",
                    Number = "number",
                    PostalCode = "postal"
                }
            });

            //no types
            context.MarketTemplates.Add(new MarketTemplate()
            {
                Id = 8000,
                Name = "Get markets stalltype Template",
                Description = "Get markets stalltype template description",
                OrganiserId = 8000
            });

            context.MarketInstances.Add(new MarketInstance()
            {
                Id = 8000,
                MarketTemplateId = 8000,
                IsCancelled = false,
                StartDate = DateTimeOffset.Now,
                EndDate = DateTimeOffset.Now.AddDays(1)
            });

            //one type
            context.MarketTemplates.Add(new MarketTemplate()
            {
                Id = 8001,
                Name = "Get markets stalltype Template",
                Description = "Get markets stalltype template description",
                OrganiserId = 8000
            });

            context.MarketInstances.Add(new MarketInstance()
            {
                Id = 8001,
                MarketTemplateId = 8001,
                IsCancelled = false,
                StartDate = DateTimeOffset.Now,
                EndDate = DateTimeOffset.Now.AddDays(1)
            });

            context.StallTypes.Add(new StallType()
            {
                Id = 8000,
                Name = "Get markets Stalltype 1",
                Description = "Get markets Stalltype description",
                MarketTemplateId = 8001
            });

            //multiple types
            context.MarketTemplates.Add(new MarketTemplate()
            {
                Id = 8002,
                Name = "Get markets stalltype Template",
                Description = "Get markets stalltype template description",
                OrganiserId = 8000
            });

            context.MarketInstances.Add(new MarketInstance()
            {
                Id = 8002,
                MarketTemplateId = 8002,
                IsCancelled = false,
                StartDate = DateTimeOffset.Now,
                EndDate = DateTimeOffset.Now.AddDays(1)
            });

            context.StallTypes.Add(new StallType()
            {
                Id = 8001,
                Name = "Get markets Stalltype 2",
                Description = "Get markets Stalltype description",
                MarketTemplateId = 8002
            });

            context.StallTypes.Add(new StallType()
            {
                Id = 8002,
                Name = "Get markets Stalltype 3",
                Description = "Get markets Stalltype description",
                MarketTemplateId = 8002
            });

            context.StallTypes.Add(new StallType()
            {
                Id = 8003,
                Name = "Get markets Stalltype 4",
                Description = "Get markets Stalltype description",
                MarketTemplateId = 8002
            });

            context.Stalls.AddRange(new List<Stall>()
            {
                new Stall()
                {
                    Id = 8000,
                    StallTypeId = 8002,
                    MarketInstanceId = 8002
                },
                new Stall()
                {
                    Id = 8001,
                    StallTypeId = 8003,
                    MarketInstanceId = 8002
                },
                new Stall()
                {
                    Id = 8002,
                    StallTypeId = 8003,
                    MarketInstanceId = 8002
                },
                new Stall()
                {
                    Id = 8003,
                    StallTypeId = 8003,
                    MarketInstanceId = 8002
                },

            });
            context.SaveChanges();
        }

        /** All ids within start at 9000 */
        private static void SeedMarketInstanceEntityExtensionTestData(ApplicationDbContext context)
        {
            //seed the relevant user
            context.Users.Add(new ApplicationUser()
            {
                Id = "MarketEntityExtensionUser",
                Email = "user@mentityextension"
            });
            context.UserInfo.Add(new Domain.Entities.User()
            {
                FirstName = "user",
                LastName = "market entity extension",
                Email = "user@mentityextension",
                IdentityId = "MarketEntityExtensionUser",
                Phone = "12345678",
                Country = "Denmark"
            });
            //Seed organiser
            context.Organisers.Add(new Organiser()
            {
                Id = 9000,
                Address = new Address()
                {
                    Id = 9000,
                    City = "test",
                    Appartment = "test",
                    Number = "test",
                    PostalCode = "test",
                    Street = "test"
                },
                Name = "Organiser m. e. e.",
                Description = "Organiser m. e. e. description",
                UserId = "MarketEntityExtensionUser"
            });
            //Seed merchant
            context.Merchants.Add(new Merchant()
            {
                Id = 9000,
                Description = "Merchant description",
                Name = "Merchant name",
                UserId = "MarketEntityExtensionUser"
            });
            Category category1 = new Category()
            {
                Name = "Market Entity Extension Category 1",
            };
            Category category2 = new Category()
            {
                Name = "Market Entity Extension Category 2",
            };
            Category category3 = new Category()
            {
                Name = "Market Entity Extension Category 3",
            };
            // seed item categories
            context.ItemCategories.AddRange(new List<Category>()
            {
                category1, category2, category3
            });
            context.SaveChanges();

            //seed market with no stalls.
            context.MarketTemplates.Add(new MarketTemplate()
            {
                Id = 9000,
                Description = "Marke template no stalls description.",
                Name = "Market template no stalls",
                OrganiserId = 9000
            });
            context.MarketInstances.Add(new MarketInstance()
            {
                Id = 9000,
                MarketTemplateId = 9000,
                StartDate = DateTimeOffset.Now,
                EndDate = DateTimeOffset.Now.AddDays(1),
                IsCancelled = false
            });
            //Market one stall no bookings
            context.MarketTemplates.Add(new MarketTemplate()
            {
                Id = 9001,
                Description = "Marke template no stalls description.",
                Name = "Market template no stalls",
                OrganiserId = 9000
            });
            context.MarketInstances.Add(new MarketInstance()
            {
                Id = 9001,
                MarketTemplateId = 9001,
                StartDate = DateTimeOffset.Now,
                EndDate = DateTimeOffset.Now.AddDays(1),
                IsCancelled = false
            });
            context.StallTypes.Add(new StallType()
            {
                Id = 9000,
                Name = "StallType 1",
                Description = "Stalltype 1 description",
                MarketTemplateId = 9001
            });
            context.Stalls.Add(new Stall()
            {
                Id = 9000,
                StallTypeId = 9000,
                MarketInstanceId = 9001
            });
            //Market one stall one booking
            context.MarketTemplates.Add(new MarketTemplate()
            {
                Id = 9002,
                Description = "Marke template no stalls description.",
                Name = "Market template no stalls",
                OrganiserId = 9000
            });
            context.MarketInstances.Add(new MarketInstance()
            {
                Id = 9002,
                MarketTemplateId = 9002,
                StartDate = DateTimeOffset.Now,
                EndDate = DateTimeOffset.Now.AddDays(1),
                IsCancelled = false
            });
            context.StallTypes.Add(new StallType()
            {
                Id = 9001,
                Name = "StallType 2",
                Description = "Stalltype 2 description",
                MarketTemplateId = 9002
            });
            context.Stalls.Add(new Stall()
            {
                Id = 9001,
                StallTypeId = 9001,
                MarketInstanceId = 9002
            });
            context.Bookings.Add(new Booking()
            {
                Id = "Booking9000",
                BoothName = "booth 9000",
                BoothDescription = "booth 9000 description",
                MerchantId = 9000,
                StallId = 9001

            });
            //Market multiple stalls not booked
            context.MarketTemplates.Add(new MarketTemplate()
            {
                Id = 9003,
                Description = "Marke template no stalls description.",
                Name = "Market template no stalls",
                OrganiserId = 9000
            });
            context.MarketInstances.Add(new MarketInstance()
            {
                Id = 9003,
                MarketTemplateId = 9003,
                StartDate = DateTimeOffset.Now,
                EndDate = DateTimeOffset.Now.AddDays(1),
                IsCancelled = false
            });
            context.StallTypes.Add(new StallType()
            {
                Id = 9002,
                Name = "StallType 3",
                Description = "Stalltype 3 description",
                MarketTemplateId = 9003
            });
            context.Stalls.AddRange(new List<Stall>() {
                new Stall()
                {
                    Id = 9002,
                    StallTypeId = 9002,
                    MarketInstanceId = 9003
                },
                new Stall()
                {
                    Id = 9003,
                    StallTypeId = 9002,
                    MarketInstanceId = 9003
                },
                new Stall()
                {
                    Id = 9004,
                    StallTypeId = 9002,
                    MarketInstanceId = 9003
                }
            });
            //Market multiple stalls all booked
            context.MarketTemplates.Add(new MarketTemplate()
            {
                Id = 9004,
                Description = "Marke template no stalls description.",
                Name = "Market template no stalls",
                OrganiserId = 9000
            });
            context.MarketInstances.Add(new MarketInstance()
            {
                Id = 9004,
                MarketTemplateId = 9004,
                StartDate = DateTimeOffset.Now,
                EndDate = DateTimeOffset.Now.AddDays(1),
                IsCancelled = false
            });
            context.StallTypes.Add(new StallType()
            {
                Id = 9003,
                Name = "StallType 4",
                Description = "Stalltype 4 description",
                MarketTemplateId = 9004
            });
            context.Stalls.AddRange(new List<Stall>() {
                new Stall()
                {
                    Id = 9005,
                    StallTypeId = 9003,
                    MarketInstanceId = 9004
                },
                new Stall()
                {
                    Id = 9006,
                    StallTypeId = 9003,
                    MarketInstanceId = 9004
                },
                new Stall()
                {
                    Id = 9007,
                    StallTypeId = 9003,
                    MarketInstanceId = 9004
                }
            });
            context.Bookings.AddRange(new List<Booking>()
            {
                new Booking()
                {
                    Id = "Booking9001",
                    BoothName = "Booking9001 name",
                    BoothDescription = "Booking9001 Description",
                    MerchantId = 9000,
                    StallId = 9005
                },
                new Booking()
                {
                    Id = "Booking9002",
                    BoothName = "Booking9002 name",
                    BoothDescription = "Booking9002 Description",
                    MerchantId = 9000,
                    StallId = 9006
                },
                new Booking()
                {
                    Id = "Booking9003",
                    BoothName = "Booking9003 name",
                    BoothDescription = "Booking9003 Description",
                    MerchantId = 9000,
                    StallId = 9007
                }
            });
            //market multiple stalls some booked
            context.MarketTemplates.Add(new MarketTemplate()
            {
                Id = 9005,
                Description = "Marke template no stalls description.",
                Name = "Market template no stalls",
                OrganiserId = 9000
            });
            context.MarketInstances.Add(new MarketInstance()
            {
                Id = 9005,
                MarketTemplateId = 9005,
                StartDate = DateTimeOffset.Now,
                EndDate = DateTimeOffset.Now.AddDays(1),
                IsCancelled = false
            });
            context.StallTypes.Add(new StallType()
            {
                Id = 9004,
                Name = "StallType 5",
                Description = "Stalltype 5 description",
                MarketTemplateId = 9005
            });
            context.Stalls.AddRange(new List<Stall>() {
                new Stall()
                {
                    Id = 9008,
                    StallTypeId = 9004,
                    MarketInstanceId = 9005
                },
                new Stall()
                {
                    Id = 9009,
                    StallTypeId = 9004,
                    MarketInstanceId = 9005
                },
                new Stall()
                {
                    Id = 9010,
                    StallTypeId = 9004,
                    MarketInstanceId = 9005
                }
            });
            context.Bookings.AddRange(new List<Booking>()
            {
                new Booking()
                {
                    Id = "Booking9004",
                    BoothName = "Booking9004 name",
                    BoothDescription = "Booking9004 Description",
                    MerchantId = 9000,
                    StallId = 9008
                },
                new Booking()
                {
                    Id = "Booking9005",
                    BoothName = "Booking9005 name",
                    BoothDescription = "Booking9005 Description",
                    MerchantId = 9000,
                    StallId = 9009
                }
            });
            //One booth one category
            context.MarketTemplates.Add(new MarketTemplate()
            {
                Id = 9006,
                Description = "Marke template no stalls description.",
                Name = "Market template no stalls",
                OrganiserId = 9000
            });
            context.MarketInstances.Add(new MarketInstance()
            {
                Id = 9006,
                MarketTemplateId = 9006,
                StartDate = DateTimeOffset.Now,
                EndDate = DateTimeOffset.Now.AddDays(1),
                IsCancelled = false
            });
            context.StallTypes.Add(new StallType()
            {
                Id = 9005,
                Name = "StallType 6",
                Description = "Stalltype 6 description",
                MarketTemplateId = 9006
            });
            context.Stalls.Add(new Stall()
            {
                Id = 9011,
                StallTypeId = 9005,
                MarketInstanceId = 9006
            });
            var booking9006 = new Booking()
            {
                Id = "Booking9006",
                BoothName = "Booking9006 name",
                BoothDescription = "Booking9006 description",
                MerchantId = 9000,
                StallId = 9011,
                ItemCategories = new List<Category>() { category1}
            };
            context.Bookings.Add(booking9006);
            //One booth multiple categories
            context.MarketTemplates.Add(new MarketTemplate()
            {
                Id = 9007,
                Description = "Marke template no stalls description.",
                Name = "Market template no stalls",
                OrganiserId = 9000
            });
            context.MarketInstances.Add(new MarketInstance()
            {
                Id = 9007,
                MarketTemplateId = 9007,
                StartDate = DateTimeOffset.Now,
                EndDate = DateTimeOffset.Now.AddDays(1),
                IsCancelled = false
            });
            context.StallTypes.Add(new StallType()
            {
                Id = 9006,
                Name = "StallType 7",
                Description = "Stalltype 7 description",
                MarketTemplateId = 9007
            });
            context.Stalls.Add(new Stall()
            {
                Id = 9012,
                StallTypeId = 9006,
                MarketInstanceId = 9007
            });
            context.Bookings.Add(new Booking()
            {
                Id = "Booking9007",
                BoothName = "Booking9007 name",
                BoothDescription = "Booking9007 description",
                MerchantId = 9000,
                StallId = 9012,
                ItemCategories = new List<Category>() { category1, category2}
            });

            //Multiple booths unique categories
            context.MarketTemplates.Add(new MarketTemplate()
            {
                Id = 9008,
                Description = "Marke template no stalls description.",
                Name = "Market template no stalls",
                OrganiserId = 9000
            });
            context.MarketInstances.Add(new MarketInstance()
            {
                Id = 9008,
                MarketTemplateId = 9008,
                StartDate = DateTimeOffset.Now,
                EndDate = DateTimeOffset.Now.AddDays(1),
                IsCancelled = false
            });
            context.StallTypes.Add(new StallType()
            {
                Id = 9007,
                Name = "StallType 8",
                Description = "Stalltype 8 description",
                MarketTemplateId = 9008
            });
            context.Stalls.AddRange(new List<Stall>() {
                new Stall()
                {
                    Id = 9013,
                    StallTypeId = 9007,
                    MarketInstanceId = 9008
                },
                new Stall()
                {
                    Id = 9014,
                    StallTypeId = 9007,
                    MarketInstanceId = 9008
                },
                new Stall()
                {
                    Id = 9015,
                    StallTypeId = 9007,
                    MarketInstanceId = 9008
                }
            });
            context.Bookings.AddRange(new List<Booking>()
            {
                new Booking()
                {
                    Id = "Booking9008",
                    BoothName = "Booking9008 name",
                    BoothDescription = "Booking9008 Description",
                    MerchantId = 9000,
                    StallId = 9013,
                    ItemCategories = new List<Category>(){category1}
                },
                new Booking()
                {
                    Id = "Booking9009",
                    BoothName = "Booking9009 name",
                    BoothDescription = "Booking9009 Description",
                    MerchantId = 9000,
                    StallId = 9014,
                    ItemCategories = new List<Category>(){category2}
                },
                new Booking()
                {
                    Id = "Booking9010",
                    BoothName = "Booking9010 name",
                    BoothDescription = "Booking9010 Description",
                    MerchantId = 9000,
                    StallId = 9015,
                    ItemCategories = new List<Category>(){category3}
                }
            });

            //Multiple booths Overlapping categories
            context.MarketTemplates.Add(new MarketTemplate()
            {
                Id = 9009,
                Description = "Marke template no stalls description.",
                Name = "Market template no stalls",
                OrganiserId = 9000
            });
            context.MarketInstances.Add(new MarketInstance()
            {
                Id = 9009,
                MarketTemplateId = 9009,
                StartDate = DateTimeOffset.Now,
                EndDate = DateTimeOffset.Now.AddDays(1),
                IsCancelled = false
            });
            context.StallTypes.Add(new StallType()
            {
                Id = 9008,
                Name = "StallType 9",
                Description = "Stalltype 9 description",
                MarketTemplateId = 9009
            });
            context.Stalls.AddRange(new List<Stall>() {
                new Stall()
                {
                    Id = 9016,
                    StallTypeId = 9008,
                    MarketInstanceId = 9009
                },
                new Stall()
                {
                    Id = 9017,
                    StallTypeId = 9008,
                    MarketInstanceId = 9009
                },
                new Stall()
                {
                    Id = 9018,
                    StallTypeId = 9008,
                    MarketInstanceId = 9009
                }
            });
            context.Bookings.AddRange(new List<Booking>()
            {
                new Booking()
                {
                    Id = "Booking9011",
                    BoothName = "Booking9011 name",
                    BoothDescription = "Booking9011 Description",
                    MerchantId = 9000,
                    StallId = 9016,
                    ItemCategories = new List<Category>(){category1, category2, category3}
                },
                new Booking()
                {
                    Id = "Booking9012",
                    BoothName = "Booking9012 name",
                    BoothDescription = "Booking9012 Description",
                    MerchantId = 9000,
                    StallId = 9017,
                    ItemCategories = new List<Category>(){category1, category2, category3}
                },
                new Booking()
                {
                    Id = "Booking9013",
                    BoothName = "Booking9013 name",
                    BoothDescription = "Booking9013 Description",
                    MerchantId = 9000,
                    StallId = 9018,
                    ItemCategories = new List<Category>(){category1, category2, category3}
                }
            });


            context.SaveChanges();
        }

        /** All ids within start at 1100*/
        private static void SeedAddStallToMarketTestData(ApplicationDbContext context)
        {
            //users
            context.Users.AddRange(
                new ApplicationUser()
                {
                    Id = "User1100",
                    Email = "User1100@mail",
                    UserName = "User1100@mail"
                },
                new ApplicationUser()
                {
                    Id = "User1101",
                    Email = "User1101@mail",
                    UserName = "User1101@mail"
                }
            );
            context.UserInfo.AddRange(
                new Domain.Entities.User()
                {
                    Email = "User1100@mail",
                    FirstName = "FirstNameUser1100",
                    LastName = "LastNameUser1100",
                    DateOfBirth = new DateTimeOffset(new DateTime(1990, 1, 1)),
                    Country = "Denmark",
                    Phone = "12345678",
                    IdentityId = "User1100"
                },
                new Domain.Entities.User()
                {
                    Email = "User1101@mail",
                    FirstName = "FirstNameUser1101",
                    LastName = "LastNameUser1101",
                    DateOfBirth = new DateTimeOffset(new DateTime(1990, 1, 1)),
                    Country = "Denmark",
                    Phone = "12345678",
                    IdentityId = "User1101"
                }
            );
            context.Organisers.AddRange(
                new Organiser()
                {
                    Id = 1100,
                    Name = "Organiser1100",
                    Description = "Organiser1100 Description",
                    UserId = "User1100",
                    Address = new Address()
                    {
                        Id = 1100,
                        Street = "Street",
                        City = "City",
                        Appartment = "apt",
                        Number = "1",
                        PostalCode = "1234"
                    }
                },
                new Organiser()
                {
                    Id = 1101,
                    Name = "Organiser1101",
                    Description = "Organiser1101 Description",
                    UserId = "User1101",
                    Address = new Address()
                    {
                        Id = 1101,
                        Street = "Street",
                        City = "City",
                        Appartment = "apt",
                        Number = "1",
                        PostalCode = "1234"
                    }
                }
            );
            context.MarketTemplates.AddRange(
                new MarketTemplate()
                {
                    Id = 1100,
                    Name = "Template1100",
                    Description = "Template1100 description",
                    OrganiserId = 1100
                },
                new MarketTemplate()
                {
                    Id = 1101,
                    Name = "Template1101",
                    Description = "Template1101 description",
                    OrganiserId = 1100
                },
                new MarketTemplate()
                {
                    Id = 1102,
                    Name = "Template1102",
                    Description = "Template1102 description",
                    OrganiserId = 1101
                }
            );
            context.MarketInstances.AddRange(
                new MarketInstance()
                {
                    Id = 1100,
                    MarketTemplateId = 1100,
                    StartDate = DateTimeOffset.Now,
                    EndDate = DateTimeOffset.Now.AddDays(1),
                    IsCancelled = false
                },
                new MarketInstance()
                {
                    Id = 1101,
                    MarketTemplateId = 1102,
                    StartDate = DateTimeOffset.Now,
                    EndDate = DateTimeOffset.Now.AddDays(1),
                    IsCancelled = false
                }
            );
            context.StallTypes.AddRange(
                new StallType()
                {
                    Id = 1100,
                    Name = "Stalltype1100",
                    Description = "Stalltype1100 Description",
                    MarketTemplateId = 1100
                },
                new StallType()
                {
                    Id = 1101,
                    Name = "Stalltype1101",
                    Description = "Stalltype1101 Description",
                    MarketTemplateId = 1101
                },
                new StallType()
                {
                    Id = 1102,
                    Name = "Stalltype1102",
                    Description = "Stalltype1102 Description",
                    MarketTemplateId = 1101
                }
            );
            context.SaveChanges();
        }
    
        /** All ids within start at 1200 */
        private static void SeedDeleteStallTestData(ApplicationDbContext context)
        {
            context.Users.AddRange(
                new ApplicationUser()
                {
                    Id = "User1200",
                    Email = "User1200@mail",
                    UserName = "User1200@mail"
                },
                new ApplicationUser()
                {
                    Id = "User1201",
                    Email = "User1201@mail",
                    UserName = "User1201@mail"
                }
            );
            context.UserInfo.AddRange(
                new Domain.Entities.User()
                {
                    Email = "User1200@mail",
                    FirstName = "FirstNameUser1200",
                    LastName = "LastNameUser1200",
                    DateOfBirth = new DateTimeOffset(new DateTime(1990, 1, 1)),
                    Country = "Denmark",
                    Phone = "12345678",
                    IdentityId = "User1200"
                },
                new Domain.Entities.User()
                {
                    Email = "User1201@mail",
                    FirstName = "FirstNameUser1201",
                    LastName = "LastNameUser1201",
                    DateOfBirth = new DateTimeOffset(new DateTime(1990, 1, 1)),
                    Country = "Denmark",
                    Phone = "12345678",
                    IdentityId = "User1201"
                }
            );
            context.Organisers.AddRange(
                new Organiser()
                {
                    Id = 1200,
                    Name = "Organiser1200",
                    Description = "Organiser1200 Description",
                    UserId = "User1200",
                    Address = new Address()
                    {
                        Id = 1200,
                        Street = "Street",
                        City = "City",
                        Appartment = "apt",
                        Number = "1",
                        PostalCode = "1234"
                    }
                },
                new Organiser()
                {
                    Id = 1201,
                    Name = "Organiser1201",
                    Description = "Organiser1201 Description",
                    UserId = "User1201",
                    Address = new Address()
                    {
                        Id = 1201,
                        Street = "Street",
                        City = "City",
                        Appartment = "apt",
                        Number = "1",
                        PostalCode = "1234"
                    }
                }
            );
            context.Merchants.AddRange(
                new Merchant()
                {
                    Id = 1200, 
                    Name = "Merchant1200",
                    Description = "Merchant1200 Description",
                    UserId = "User1201"
                }
            );
            context.MarketTemplates.AddRange(
                new MarketTemplate()
                {
                    Id = 1200,
                    Name = "Template1200",
                    Description = "Template1200 description",
                    OrganiserId = 1200
                },
                new MarketTemplate()
                {
                    Id = 1201,
                    Name = "Template1201",
                    Description = "Template1201 description",
                    OrganiserId = 1200
                },
                new MarketTemplate()
                {
                    Id = 1202,
                    Name = "Template1202",
                    Description = "Template1202 description",
                    OrganiserId = 1201
                },
                new MarketTemplate()
                {
                    Id = 1203,
                    Name = "Template1203",
                    Description = "Template1203 description",
                    OrganiserId = 1200
                },
                new MarketTemplate()
                {
                    Id = 1204,
                    Name = "Template1204",
                    Description = "Template1204 description",
                    OrganiserId = 1200
                },
                new MarketTemplate()
                {
                    Id = 1205,
                    Name = "Template1205",
                    Description = "Template1205 description",
                    OrganiserId = 1200
                },
                new MarketTemplate()
                {
                    Id = 1206,
                    Name = "Template1206",
                    Description = "Template1206 description",
                    OrganiserId = 1200
                }
            );
            context.MarketInstances.AddRange(
                new MarketInstance()
                {
                    Id = 1200,
                    MarketTemplateId = 1200,
                    StartDate = DateTimeOffset.Now.AddDays(1),
                    EndDate = DateTimeOffset.Now.AddDays(2),
                    IsCancelled = false
                },
                new MarketInstance()
                {
                    Id = 1201,
                    MarketTemplateId = 1202,
                    StartDate = DateTimeOffset.Now.AddDays(1),
                    EndDate = DateTimeOffset.Now.AddDays(2),
                    IsCancelled = false
                },
                new MarketInstance()
                {
                    Id = 1202,
                    MarketTemplateId = 1202,
                    StartDate = DateTimeOffset.Now.AddDays(1),
                    EndDate = DateTimeOffset.Now.AddDays(2),
                    IsCancelled = false
                },
                new MarketInstance()
                {
                    Id = 1203,
                    MarketTemplateId = 1203,
                    StartDate = DateTimeOffset.Now.AddDays(-2),
                    EndDate = DateTimeOffset.Now.AddDays(-1),
                    IsCancelled = false
                },
                new MarketInstance()
                {
                    Id = 1204,
                    MarketTemplateId = 1204,
                    StartDate = DateTimeOffset.Now.AddDays(-1),
                    EndDate = DateTimeOffset.Now.AddDays(3),
                    IsCancelled = false
                },
                new MarketInstance()
                {
                    Id = 1205,
                    MarketTemplateId = 1205,
                    StartDate = DateTimeOffset.Now.AddDays(1),
                    EndDate = DateTimeOffset.Now.AddDays(2),
                    IsCancelled = false
                },
                new MarketInstance()
                {
                    Id = 1206,
                    MarketTemplateId = 1206,
                    StartDate = DateTimeOffset.Now.AddDays(1),
                    EndDate = DateTimeOffset.Now.AddDays(2),
                    IsCancelled = true
                }
            );
            context.StallTypes.AddRange(
                new StallType()
                {
                    Id = 1200,
                    Name = "Stalltype1200",
                    Description = "Stalltype1200 Description",
                    MarketTemplateId = 1200
                },
                new StallType()
                {
                    Id = 1201,
                    Name = "Stalltype1201",
                    Description = "Stalltype1201 Description",
                    MarketTemplateId = 1201
                },
                new StallType()
                {
                    Id = 1202,
                    Name = "Stalltype1202",
                    Description = "Stalltype1202 Description",
                    MarketTemplateId = 1202
                },
                new StallType()
                {
                    Id = 1203,
                    Name = "Stalltype1203",
                    Description = "Stalltype1203 Description",
                    MarketTemplateId = 1203
                },
                new StallType()
                {
                    Id = 1204,
                    Name = "Stalltype1204",
                    Description = "Stalltype1204 Description",
                    MarketTemplateId = 1204
                },
                new StallType()
                {
                    Id = 1205,
                    Name = "Stalltype1205",
                    Description = "Stalltype1205 Description",
                    MarketTemplateId = 1205
                },
                new StallType()
                {
                    Id = 1206,
                    Name = "Stalltype1206",
                    Description = "Stalltype1206 Description",
                    MarketTemplateId = 1206
                }
            );
            context.Stalls.AddRange(
                new Stall()
                {
                    Id = 1200,
                    StallTypeId = 1200,
                    MarketInstanceId = 1200
                },
                new Stall()
                {
                    Id = 1201,
                    StallTypeId = 1201,
                    MarketInstanceId = 1201
                },
                new Stall()
                {
                    Id = 1202,
                    StallTypeId = 1202,
                    MarketInstanceId = 1202
                },
                new Stall()
                {
                    Id = 1203,
                    StallTypeId = 1203,
                    MarketInstanceId = 1203
                },
                new Stall()
                {
                    Id = 1204,
                    StallTypeId = 1204,
                    MarketInstanceId = 1204
                },
                new Stall()
                {
                    Id = 1205,
                    StallTypeId = 1205,
                    MarketInstanceId = 1205
                },
                new Stall()
                {
                    Id = 1206,
                    StallTypeId = 1206,
                    MarketInstanceId = 1206
                }
            );
            context.Bookings.AddRange(
                new Booking()
                {
                    Id = "Booking1200",
                    BoothName = "Booth1200",
                    BoothDescription = "Booth1200 Description",
                    MerchantId = 1200,
                    StallId = 1205
                }
            );
            context.SaveChanges();
        }
    
        /** All ids within start at 1300 */
        private static void SeedGetMarketStallsTestData(ApplicationDbContext context)
        {
            context.Users.Add(new ApplicationUser()
            {
                Id = "User1300",
                Email = "User1300@mail",
                UserName = "User1300@mail",
                PhoneNumber = "12345678"
            });
            context.UserInfo.Add(new Domain.Entities.User()
            {
                IdentityId = "User1300",
                Email = "User1300@mail",
                Country = "Denmark",
                DateOfBirth = new DateTime(1990, 1, 1),
                LastName = "LastnameUser1300",
                FirstName = "FirstnameUser1300",
                Phone = "12345678"
            });
            context.Organisers.Add(new Organiser()
            {
                UserId = "User1300",
                Id = 1300,
                Name = "Organiser1300",
                Description = "Organiser1300 Description",
                Address = new Address()
                {
                    Id = 1300,
                    Appartment = "apt",
                    City = "city",
                    Number = "1234",
                    Street = "street",
                    PostalCode = "postalcode"
                }
            });
            context.MarketTemplates.AddRange(
                new MarketTemplate()
                {
                    Id = 1300,
                    Name = "Market1300",
                    Description = "Market1300 Description",
                    OrganiserId = 1300
                },
                new MarketTemplate()
                {
                    Id = 1301,
                    Name = "Market1301",
                    Description = "Market1301 Description",
                    OrganiserId = 1300
                },
                new MarketTemplate()
                {
                    Id = 1302,
                    Name = "Market1302",
                    Description = "Market1302 Description",
                    OrganiserId = 1300
                },
                new MarketTemplate()
                {
                    Id = 1303,
                    Name = "Market1303",
                    Description = "Market1303 Description",
                    OrganiserId = 1300
                }
            );
            context.MarketInstances.AddRange(
                new MarketInstance()
                {
                    Id = 1300,
                    MarketTemplateId = 1300,
                    IsCancelled = false,
                    StartDate = DateTimeOffset.Now,
                    EndDate = DateTimeOffset.Now.AddDays(1)
                },
                new MarketInstance()
                {
                    Id = 1301,
                    MarketTemplateId = 1301,
                    IsCancelled = false,
                    StartDate = DateTimeOffset.Now,
                    EndDate = DateTimeOffset.Now.AddDays(1)
                },
                new MarketInstance()
                {
                    Id = 1302,
                    MarketTemplateId = 1302,
                    IsCancelled = false,
                    StartDate = DateTimeOffset.Now,
                    EndDate = DateTimeOffset.Now.AddDays(1)
                },
                new MarketInstance()
                {
                    Id = 1303,
                    MarketTemplateId = 1303,
                    IsCancelled = false,
                    StartDate = DateTimeOffset.Now,
                    EndDate = DateTimeOffset.Now.AddDays(1)
                }
            );
            context.StallTypes.AddRange(
                new StallType()
                {
                    Id = 1300, 
                    Name = "stalltype1300",
                    Description = "stalltype1300 description",
                    MarketTemplateId = 1300
                },
                new StallType()
                {
                    Id = 1301,
                    Name = "stalltype1301",
                    Description = "stalltype1301 description",
                    MarketTemplateId = 1301
                },
                new StallType()
                {
                    Id = 1302,
                    Name = "stalltype1302",
                    Description = "stalltype1302 description",
                    MarketTemplateId = 1302
                },
                new StallType()
                {
                    Id = 1303,
                    Name = "stalltype1303",
                    Description = "stalltype1303 description",
                    MarketTemplateId = 1303
                },
                new StallType()
                {
                    Id = 1304,
                    Name = "stalltype1304",
                    Description = "stalltype1304 description",
                    MarketTemplateId = 1303
                }
            );
            context.Stalls.AddRange(
                new Stall()
                {
                    Id = 1300,
                    StallTypeId = 1301,
                    MarketInstanceId = 1301
                },
                new Stall()
                {
                    Id = 1301,
                    StallTypeId = 1302,
                    MarketInstanceId = 1302
                },
                new Stall()
                {
                    Id = 1302,
                    StallTypeId = 1302,
                    MarketInstanceId = 1302
                },
                new Stall()
                {
                    Id = 1303,
                    StallTypeId = 1302,
                    MarketInstanceId = 1302
                },
                new Stall()
                {
                    Id = 1304,
                    StallTypeId = 1303,
                    MarketInstanceId = 1303
                },
                new Stall()
                {
                    Id = 1305,
                    StallTypeId = 1303,
                    MarketInstanceId = 1303
                },
                new Stall()
                {
                    Id = 1306,
                    StallTypeId = 1304,
                    MarketInstanceId = 1303
                }
            );
            context.SaveChanges();
        }

        /** All ids within start at 1400 */
        private static void SeedGetStallTestData(ApplicationDbContext context)
        {
            context.Users.Add(new ApplicationUser()
            {
                Id = "User1400",
                Email = "User1400@mail",
                UserName = "User1400@mail"
            });
            context.UserInfo.Add(new Domain.Entities.User()
            {
                IdentityId = "User1400",
                Email = "User1400@mail",
                Country = "Test",
                DateOfBirth = new DateTime(1990, 1, 1),
                FirstName = "Firstname User1400",
                LastName = "Lastname User1400",
                Phone = "12345678"
            });
            context.Organisers.Add(new Organiser()
            {
                Id = 1400,
                Name = "Organiser 1400",
                Description = "Organiser 1400 Description",
                UserId = "User1400",
                Address = new Address()
                {
                    Id = 1400,
                    Street = "street",
                    Number = "number",
                    Appartment = "apt",
                    City = "city",
                    PostalCode = "postal"
                }
            });
            context.MarketTemplates.Add(new MarketTemplate()
            {
                Id = 1400,
                Name = "Market 1400",
                Description = "Market 1400 Description",
                OrganiserId = 1400
            });
            context.MarketInstances.Add(new MarketInstance()
            {
                Id = 1400,
                MarketTemplateId = 1400,
                StartDate = new DateTime(1990, 1, 1),
                EndDate = new DateTime(1990, 1, 2),
                IsCancelled = false,
            });
            context.StallTypes.Add(new StallType()
            {
                Id = 1400,
                Name = "Stalltype 1400",
                Description = "Stalltype 1400 description",
                MarketTemplateId = 1400
            });
            context.Stalls.AddRange(
                new Stall()
                {
                    Id = 1400,
                    MarketInstanceId = 1400,
                    StallTypeId = 1400
                },
                new Stall()
                {
                    Id = 1401,
                    MarketInstanceId = 1400,
                    StallTypeId = 1400
                },
                new Stall()
                {
                    Id = 1402,
                    MarketInstanceId = 1400,
                    StallTypeId = 1400
                }
            );
            context.Merchants.Add(new Merchant()
            {
                Id = 1400,
                Name = "Merchant 1400",
                Description = "Merchant 1400 Description",
                UserId = "User1400"
            });
            context.Bookings.Add(new Booking()
            {
                Id = "Booth1400",
                BoothName = "Booth 1400",
                BoothDescription = "Booth 1400 Description",
                MerchantId = 1400,
                StallId = 1400,
                ItemCategories = new List<Category>()
                {
                    new Category()
                    {
                        Name = "Category 1400",
                    },
                    new Category()
                    {
                        Name = "Category 1401",
                    },
                    new Category()
                    {
                        Name = "Category 1402",
                    }
                }
            });
            context.SaveChanges();
        }

        /** All ids within start at 1500 
            Basically just seeding a bunch of different booths within to make sure that they exist.
            But the test here is not really running in isolation from any of the other test data, so 
            it will just pull everything it can get.
            also the year 1500  is used here as period with controlled test data within, so plz don't user elsewhere.
         */
        private static void SeedGetFilteredBoothsQuery(ApplicationDbContext context)
        {
            context.Users.Add(new ApplicationUser()
            {
                Id = "User1500",
                Email = "User1500@mail",
                UserName = "User1500@mail"
            });
            context.UserInfo.Add(new Domain.Entities.User()
            {
                IdentityId = "User1500",
                Email = "User1500@mail",
                Country = "test",
                DateOfBirth = new DateTime(1990, 1, 1),
                FirstName = "Fristname User1500",
                LastName = "Lastname User1500",
                Phone = "12345678"
            });
            context.Organisers.Add(new Organiser()
            {
                Id = 1500,
                Name = "Organiser 1500",
                Description = "Organiser 1500 Description",
                UserId = "User1500",
                Address = new Address()
                {
                    Id = 1500,
                    Street = "street",
                    Number = "number",
                    City = "city",
                    Appartment = "apt",
                    PostalCode = "postal"
                }
            });

            context.MarketTemplates.AddRange(new MarketTemplate()
            {
                Id = 1500,
                Name = "Template 1500",
                Description = "Template 1500 Description",
                OrganiserId = 1500
            });
            context.MarketInstances.AddRange(new MarketInstance()
            {
                Id = 1500,
                IsCancelled = false,
                StartDate = new DateTime(1500, 1, 1),
                EndDate = new DateTime(1500, 2, 1),
                MarketTemplateId = 1500
            });
            context.StallTypes.AddRange(new StallType()
            {
                Id = 1500,
                Name = "Stalltype 1500",
                Description = "Stalltype 1500 description",
                MarketTemplateId = 1500
            });
            context.Stalls.AddRange(new Stall()
            {
                Id = 1500,
                StallTypeId = 1500,
                MarketInstanceId = 1500
            },
            new Stall()
            {
                Id = 1501,
                StallTypeId = 1500,
                MarketInstanceId = 1500
            });

            context.MarketTemplates.AddRange(new MarketTemplate()
            {
                Id = 1501,
                Name = "Template 1501",
                Description = "Template 1501 Description",
                OrganiserId = 1500
            });
            context.MarketInstances.AddRange(new MarketInstance()
            {
                Id = 1501,
                IsCancelled = false,
                StartDate = new DateTime(1500, 3, 1),
                EndDate = new DateTime(1500, 4, 1),
                MarketTemplateId = 1501
            });
            context.StallTypes.AddRange(new StallType()
            {
                Id = 1501,
                Name = "Stalltype 1501",
                Description = "Stalltype 1501 description",
                MarketTemplateId = 1501
            });
            context.Stalls.AddRange(
            new Stall()
            {
                Id = 1502,
                StallTypeId = 1500,
                MarketInstanceId = 1501
            },
            new Stall()
            {
                Id = 1503,
                StallTypeId = 1500,
                MarketInstanceId = 1501
            },
            new Stall()
            {
                Id = 1504,
                StallTypeId = 1500,
                MarketInstanceId = 1501
            },
            new Stall()
            {
                Id = 1505,
                StallTypeId = 1500,
                MarketInstanceId = 1501
            });
            context.Merchants.Add(new Merchant()
            {

                Id = 1500,
                Name = "Merchant 1500",
                Description = "Merchant 1500 Description",
                UserId = "User1500"
            });
            context.Merchants.Add(new Merchant()
            {

                Id = 1501,
                Name = "Merchant 1501",
                Description = "Merchant 1501 Description",
                UserId = "User1500"
            });
            context.Bookings.AddRange(
                new Booking()
            {
                Id = "Booking1500",
                BoothName = "Booth 1500",
                BoothDescription = "Booth 1500 description",
                MerchantId = 1500,
                StallId = 1502
            },
            new Booking()
            {
                Id = "Booking1501",
                BoothName = "Booth 1501",
                BoothDescription = "Booth 1501 description",
                MerchantId = 1500,
                StallId = 1503,
                ItemCategories = new List<Category>() { new Category() { Name = "Category 1500" } }
            },
             new Booking()
             {
                 Id = "Booking1502",
                 BoothName = "Booth 1502",
                 BoothDescription = "Booth 1502 description",
                 MerchantId = 1500,
                 StallId = 1504,
                 ItemCategories = new List<Category>() { new Category() { Name = "Category 1501" } }
             },
             new Booking()
             {
                 Id = "Booking1503",
                 BoothName = "Booth 1503",
                 BoothDescription = "Booth 1503 description",
                 MerchantId = 1501,
                 StallId = 1505
             });
            context.SaveChanges();
        }

        /** All ids within start at 1600 */
        private static void SeedUpdateBoothCommandTestData(ApplicationDbContext context)
        {
            context.Users.Add(new ApplicationUser()
            {
                Id = "User1600",
                Email = "User1600@mail",
                UserName = "User1600@mail"
            });
            context.Users.Add(new ApplicationUser()
            {
                Id = "User1601",
                Email = "User1601@mail",
                UserName = "User1601@mail"
            });
            context.UserInfo.Add(new Domain.Entities.User()
            {
                IdentityId = "User1600",
                Email = "User1600@mail",
                Country = "Test",
                DateOfBirth = new DateTime(1990, 1, 1),
                FirstName = "Firstname User1600",
                LastName = "Lastname User1600",
                Phone = "12345678"
            });
            context.UserInfo.Add(new Domain.Entities.User()
            {
                IdentityId = "User1601",
                Email = "User1601@mail",
                Country = "Test",
                DateOfBirth = new DateTime(1990, 1, 1),
                FirstName = "Firstname User1600",
                LastName = "Lastname User1600",
                Phone = "12345678"
            });
            context.Organisers.Add(new Organiser()
            {
                Id = 1600,
                Name = "Organiser 1600",
                Description = "Organiser 1600 Description",
                UserId = "User1600",
                Address = new Address()
                {
                    Id = 1600,
                    Street = "street",
                    Number = "number",
                    Appartment = "apt",
                    City = "city",
                    PostalCode = "postal"
                }
            });
            context.MarketTemplates.Add(new MarketTemplate()
            {
                Id = 1600,
                Name = "Market 1600",
                Description = "Market 1600 Description",
                OrganiserId = 1600
            });
            context.MarketInstances.Add(new MarketInstance()
            {
                Id = 1600,
                MarketTemplateId = 1600,
                StartDate = DateTimeOffset.Now,
                EndDate = DateTimeOffset.Now.AddDays(1),
                IsCancelled = false,
            });
            context.StallTypes.Add(new StallType()
            {
                Id = 1600,
                Name = "Stalltype 1600",
                Description = "Stalltype 1600 description",
                MarketTemplateId = 1600
            });
            context.Stalls.AddRange(
                new Stall()
                {
                    Id = 1600,
                    MarketInstanceId = 1600,
                    StallTypeId = 1600
                },
                new Stall()
                {
                    Id = 1601,
                    MarketInstanceId = 1600,
                    StallTypeId = 1600
                }
            );
            context.Merchants.AddRange(new Merchant()
            {
                Id = 1600,
                Name = "Merchant 1600",
                Description = "Merchant 1600 Description",
                UserId = "User1600"
            },
            new Merchant()
            {
                Id = 1601,
                Name = "Merchant 1601",
                Description = "Merchant 1601 Description",
                UserId = "User1601"
            });
            var cat1600 = new Category()
            {
                Name = "Category 1600"
            };
            context.ItemCategories.AddRange(
                cat1600,
                new Category()
                {
                    Name = "Category 1601"
                });

            context.Bookings.AddRange(new Booking()
            {
                Id = "Booth1600",
                BoothName = "Booth 1600",
                BoothDescription = "Booth 1600 Description",
                MerchantId = 1600,
                StallId = 1600,
                ItemCategories = new List<Category>()
                {
                    cat1600
                }
            },
            new Booking()
            {
                Id = "Booth1601",
                BoothName = "Booth 1601",
                BoothDescription = "Booth 1601 Description",
                MerchantId = 1601,
                StallId = 1601,
            });

            context.SaveChanges();
        }

        /** All ids within start at 1700 */
        private static void SeedGetBoothTestData(ApplicationDbContext context)
        {
            context.Users.Add(new ApplicationUser()
            {
                Id = "User1700",
                Email = "User1700@mail",
                UserName = "User1700@mail"
            });
            context.UserInfo.Add(new Domain.Entities.User()
            {
                IdentityId = "User1700",
                Email = "User1700@mail",
                Country = "Test",
                DateOfBirth = new DateTime(1990, 1, 1),
                FirstName = "Firstname User1700",
                LastName = "Lastname User1700",
                Phone = "12345678"
            });
            context.Organisers.Add(new Organiser()
            {
                Id = 1700,
                Name = "Organiser 1700",
                Description = "Organiser 1700 Description",
                UserId = "User1700",
                Address = new Address()
                {
                    Id = 1700,
                    Street = "street",
                    Number = "number",
                    Appartment = "apt",
                    City = "city",
                    PostalCode = "postal"
                }
            });
            context.MarketTemplates.Add(new MarketTemplate()
            {
                Id = 1700,
                Name = "Market 1700",
                Description = "Market 1700 Description",
                OrganiserId = 1700
            });
            context.MarketInstances.Add(new MarketInstance()
            {
                Id = 1700,
                MarketTemplateId = 1700,
                StartDate = new DateTime(1990, 1, 1),
                EndDate = new DateTime(1990, 1, 2),
                IsCancelled = false,
            });
            context.StallTypes.Add(new StallType()
            {
                Id = 1700,
                Name = "Stalltype 1700",
                Description = "Stalltype 1700 description",
                MarketTemplateId = 1700
            });
            context.Stalls.AddRange(
                new Stall()
                {
                    Id = 1700,
                    MarketInstanceId = 1700,
                    StallTypeId = 1700
                }
            );
            context.Merchants.Add(new Merchant()
            {
                Id = 1700,
                Name = "Merchant 1700",
                Description = "Merchant 1700 Description",
                UserId = "User1700"
            });
            context.Bookings.Add(new Booking()
            {
                Id = "Booth1700",
                BoothName = "Booth 1700",
                BoothDescription = "Booth 1700 Description",
                MerchantId = 1700,
                StallId = 1700,
                ItemCategories = new List<Category>()
                {
                    new Category()
                    {
                        Name = "Category 1700",
                    },
                    new Category()
                    {
                        Name = "Category 1701",
                    },
                    new Category()
                    {
                        Name = "Category 1702",
                    }
                }
            });
            context.SaveChanges();
        }

        /** All ids within start at 1800 */
        private static void SeedGetUsersBoothsTestData(ApplicationDbContext context)
        {
            context.Users.AddRange(new ApplicationUser()
            {
                Id = "User1800",
                Email = "User1800@mail",
                UserName = "User1800@mail"
            },
            new ApplicationUser()
            {
                Id = "User1801",
                Email = "User1801@mail",
                UserName = "User1801@mail"
            },
            new ApplicationUser()
            {
                Id = "User1802",
                Email = "User1802@mail",
                UserName = "User1802@mail"
            });
            context.UserInfo.AddRange(new Domain.Entities.User()
            {
                IdentityId = "User1800",
                Email = "User1800@mail",
                Country = "Test",
                DateOfBirth = new DateTime(1990, 1, 1),
                FirstName = "Firstname User1800",
                LastName = "Lastname User1800",
                Phone = "12345678"
            },
            new Domain.Entities.User()
            {
                IdentityId = "User1801",
                Email = "User1801@mail",
                Country = "Test",
                DateOfBirth = new DateTime(1990, 1, 1),
                FirstName = "Firstname User1801",
                LastName = "Lastname User1801",
                Phone = "12345678"
            },
            new Domain.Entities.User()
            {
                IdentityId = "User1802",
                Email = "User1802@mail",
                Country = "Test",
                DateOfBirth = new DateTime(1990, 1, 1),
                FirstName = "Firstname User1802",
                LastName = "Lastname User1802",
                Phone = "12345678"
            });
            context.Organisers.Add(new Organiser()
            {
                Id = 1800,
                Name = "Organiser 1800",
                Description = "Organiser 1800 Description",
                UserId = "User1800",
                Address = new Address()
                {
                    Id = 1800,
                    Street = "street",
                    Number = "number",
                    Appartment = "apt",
                    City = "city",
                    PostalCode = "postal"
                }
            });
            context.MarketTemplates.Add(new MarketTemplate()
            {
                Id = 1800,
                Name = "Market 1800",
                Description = "Market 1800 Description",
                OrganiserId = 1800
            });
            context.MarketInstances.Add(new MarketInstance()
            {
                Id = 1800,
                MarketTemplateId = 1800,
                StartDate = new DateTime(1990, 1, 1),
                EndDate = new DateTime(1990, 1, 2),
                IsCancelled = false,
            });
            context.StallTypes.Add(new StallType()
            {
                Id = 1800,
                Name = "Stalltype 1800",
                Description = "Stalltype 1800 description",
                MarketTemplateId = 1800
            });
            context.Stalls.AddRange(
                new Stall()
                {
                    Id = 1800,
                    MarketInstanceId = 1800,
                    StallTypeId = 1800
                },
                new Stall()
                {
                    Id = 1801,
                    MarketInstanceId = 1800,
                    StallTypeId = 1800
                },
                new Stall()
                {
                    Id = 1802,
                    MarketInstanceId = 1800,
                    StallTypeId = 1800
                },
                new Stall()
                {
                    Id = 1803,
                    MarketInstanceId = 1800,
                    StallTypeId = 1800
                }

            );
            context.Merchants.AddRange(new Merchant()
            {
                Id = 1800,
                Name = "Merchant 1800",
                Description = "Merchant 1800 Description",
                UserId = "User1801"
            },
            new Merchant()
            {
                Id = 1801,
                Name = "Merchant 1801",
                Description = "Merchant 1801 Description",
                UserId = "User1802"
            },
            new Merchant()
            {
                Id = 1802,
                Name = "Merchant 1802",
                Description = "Merchant 1802 Description",
                UserId = "User1802"
            });
            context.Bookings.AddRange(new Booking()
            {
                Id = "Booth1800",
                BoothName = "Booth 1800",
                BoothDescription = "Booth 1800 Description",
                MerchantId = 1800,
                StallId = 1800,
                ItemCategories = new List<Category>()
                {
                    new Category()
                    {
                        Name = "Category 1800",
                    },
                    new Category()
                    {
                        Name = "Category 1801",
                    },
                    new Category()
                    {
                        Name = "Category 1802",
                    }
                }
            },
            new Booking()
            {
                Id = "Booth1801",
                BoothName = "Booth 1801",
                BoothDescription = "Booth 1801 Description",
                MerchantId = 1801,
                StallId = 1800
            },
            new Booking()
            {
                Id = "Booth1802",
                BoothName = "Booth 1802",
                BoothDescription = "Booth 1802 Description",
                MerchantId = 1801,
                StallId = 1800
            },
            new Booking()
            {
                Id = "Booth1803",
                BoothName = "Booth 1803",
                BoothDescription = "Booth 1803 Description",
                MerchantId = 1802,
                StallId = 1800
            });
            context.SaveChanges();
        }
    
        /** All ids within start at 1900 */
        private static void SeedAddOrganiserContactsTestData(ApplicationDbContext context)
        {
            context.Users.AddRange(new ApplicationUser()
            {
                Id = "User1900",
                Email = "User1900@mail",
                UserName = "User1900@mail"
            },
            new ApplicationUser()
            {
                Id = "User1901",
                Email = "User1901@mail",
                UserName = "User1901@mail"
            });
            context.UserInfo.AddRange(new Domain.Entities.User()
            {
                IdentityId = "User1900",
                Email = "User1900@mail",
                Country = "Test",
                DateOfBirth = new DateTime(1990, 1, 1),
                FirstName = "Firstname User1900",
                LastName = "Lastname User1900",
                Phone = "12345678"
            },
            new Domain.Entities.User()
            {
                IdentityId = "User1901",
                Email = "User1901@mail",
                Country = "Test",
                DateOfBirth = new DateTime(1990, 1, 1),
                FirstName = "Firstname User1901",
                LastName = "Lastname User1901",
                Phone = "12345678"
            });
            context.Organisers.AddRange(new Organiser()
            {
                Id = 1900,
                Name = "Organiser 1900",
                Description = "Organiser 1900 Description",
                UserId = "User1900",
                Address = new Address()
                {
                    Id = 1900,
                    Street = "street",
                    Number = "number",
                    Appartment = "apt",
                    City = "city",
                    PostalCode = "postal"
                }
            },
            new Organiser()
            {
                Id = 1901,
                Name = "Organiser 1901",
                Description = "Organiser 1901 Description",
                UserId = "User1901",
                Address = new Address()
                {
                    Id = 1901,
                    Street = "street",
                    Number = "number",
                    Appartment = "apt",
                    City = "city",
                    PostalCode = "postal"
                }
            });
            context.ContactInformations.Add(new ContactInfo()
            {
                OrganiserId = 1901,
                Value = "value",
                ContactType = ContactInfoType.PHONE_NUMER
            });
            context.SaveChanges();
        }

        /** All ids within start at 2100 */
        private static void SeedRemoveOrganiserContactTestData(ApplicationDbContext context)
        {
            context.Users.AddRange(new ApplicationUser()
            {
                Id = "User2100",
                Email = "User2100@mail",
                UserName = "User2100@mail"
            },
           new ApplicationUser()
           {
               Id = "User2101",
               Email = "User2101@mail",
               UserName = "User2101@mail"
           });
            context.UserInfo.AddRange(new Domain.Entities.User()
            {
                IdentityId = "User2100",
                Email = "User2100@mail",
                Country = "Test",
                DateOfBirth = new DateTime(1990, 1, 1),
                FirstName = "Firstname User2100",
                LastName = "Lastname User2100",
                Phone = "12345678"
            },
            new Domain.Entities.User()
            {
                IdentityId = "User2101",
                Email = "User2101@mail",
                Country = "Test",
                DateOfBirth = new DateTime(1990, 1, 1),
                FirstName = "Firstname User2101",
                LastName = "Lastname User2101",
                Phone = "12345678"
            });
            context.Organisers.AddRange(new Organiser()
            {
                Id = 2100,
                Name = "Organiser 2100",
                Description = "Organiser 2100 Description",
                UserId = "User2100",
                Address = new Address()
                {
                    Id = 2100,
                    Street = "street",
                    Number = "number",
                    Appartment = "apt",
                    City = "city",
                    PostalCode = "postal"
                }
            },
            new Organiser()
            {
                Id = 2101,
                Name = "Organiser 2101",
                Description = "Organiser 2101 Description",
                UserId = "User2101",
                Address = new Address()
                {
                    Id = 2101,
                    Street = "street",
                    Number = "number",
                    Appartment = "apt",
                    City = "city",
                    PostalCode = "postal"
                }
            });
            context.ContactInformations.AddRange(new ContactInfo()
            {
                OrganiserId = 2100,
                Value = "info 1",
                ContactType = ContactInfoType.PHONE_NUMER
            },
            new ContactInfo()
            {
                OrganiserId = 2101,
                Value = "info 2",
                ContactType = ContactInfoType.PHONE_NUMER
            });
            context.SaveChanges();
        }

        /** All ids within start at 2200 */
        private static void SeedCreateMarketTestData(ApplicationDbContext context)
        {
            context.Users.AddRange(new ApplicationUser()
            {
                Id = "User2200",
                Email = "User2200@mail",
                UserName = "User2200@mail"
            },
            new ApplicationUser()
            {
                Id = "User2201",
                Email = "User2201@mail",
                UserName = "User2201@mail"
            });
            context.UserInfo.AddRange(new Domain.Entities.User()
            {
                IdentityId = "User2200",
                Email = "User2200@mail",
                Country = "Test",
                DateOfBirth = new DateTime(1990, 1, 1),
                FirstName = "Firstname User2200",
                LastName = "Lastname User2200",
                Phone = "12345678"
            },
            new Domain.Entities.User()
            {
                IdentityId = "User2201",
                Email = "User2201@mail",
                Country = "Test",
                DateOfBirth = new DateTime(1990, 1, 1),
                FirstName = "Firstname User2201",
                LastName = "Lastname User2201",
                Phone = "12345678"
            });
            context.Organisers.AddRange(new Organiser()
            {
                Id = 2200,
                Name = "Organiser 2200",
                Description = "Organiser 2200 Description",
                UserId = "User2200",
                Address = new Address()
                {
                    Id = 2200,
                    Street = "street",
                    Number = "number",
                    Appartment = "apt",
                    City = "city",
                    PostalCode = "postal"
                }
            },
            new Organiser()
            {
                Id = 2201,
                Name = "Organiser 2201",
                Description = "Organiser 2201 Description",
                UserId = "User2201",
                Address = new Address()
                {
                    Id = 2201,
                    Street = "street",
                    Number = "number",
                    Appartment = "apt",
                    City = "city",
                    PostalCode = "postal"
                }
            });

            context.SaveChanges();
        }

        /** All ids within start at 2300 */
        private static void SeedEditMarketTestData(ApplicationDbContext context)
        {
            context.Users.AddRange(new ApplicationUser()
            {
                Id = "User2300",
                Email = "User2300@mail",
                UserName = "User2300@mail"
            },
            new ApplicationUser()
            {
                Id = "User2301",
                Email = "User2301@mail",
                UserName = "User2301@mail"
            });

            context.UserInfo.AddRange(new Domain.Entities.User()
            {
                IdentityId = "User2300",
                Email = "User2300@mail",
                Country = "Test",
                DateOfBirth = new DateTime(1990, 1, 1),
                FirstName = "Firstname User2300",
                LastName = "Lastname User2300",
                Phone = "12345678"
            },
            new Domain.Entities.User()
            {
                IdentityId = "User2301",
                Email = "User2301@mail",
                Country = "Test",
                DateOfBirth = new DateTime(1990, 1, 1),
                FirstName = "Firstname User2301",
                LastName = "Lastname User2301",
                Phone = "12345678"
            });
            context.Organisers.AddRange(new Organiser()
            {
                Id = 2300,
                Name = "Organiser 2300",
                Description = "Organiser 2300 Description",
                UserId = "User2300",
                Address = new Address()
                {
                    Id = 2300,
                    Street = "street",
                    Number = "number",
                    Appartment = "apt",
                    City = "city",
                    PostalCode = "postal"
                }
            },
            new Organiser()
            {
                Id = 2301,
                Name = "Organiser 2301",
                Description = "Organiser 2301 Description",
                UserId = "User2300",
                Address = new Address()
                {
                    Id = 2301,
                    Street = "street",
                    Number = "number",
                    Appartment = "apt",
                    City = "city",
                    PostalCode = "postal"
                }
            },
            new Organiser()
            {
                Id = 2302,
                Name = "Organiser 2302",
                Description = "Organiser 2302 Description",
                UserId = "User2301",
                Address = new Address()
                {
                    Id = 2302,
                    Street = "street",
                    Number = "number",
                    Appartment = "apt",
                    City = "city",
                    PostalCode = "postal"
                }
            });

            context.MarketTemplates.Add(new MarketTemplate()
            {
                Id = 2300,
                Name = "Market 2300 name",
                Description = "Market 2300 description",
                OrganiserId = 2300
            });
            context.MarketInstances.Add(new MarketInstance()
            {
                MarketTemplateId = 2300,
                Id = 2300,
                IsCancelled = false,
                StartDate = DateTimeOffset.Now,
                EndDate = DateTimeOffset.Now.AddDays(1)
            });
            context.MarketTemplates.Add(new MarketTemplate()
            {
                Id = 2301,
                Name = "Market 2301 name",
                Description = "Market 2301 description",
                OrganiserId = 2301
            });
            context.MarketInstances.Add(new MarketInstance()
            {
                MarketTemplateId = 2301,
                Id = 2301,
                IsCancelled = false,
                StartDate = DateTimeOffset.Now,
                EndDate = DateTimeOffset.Now.AddDays(1)
            });
            context.MarketTemplates.Add(new MarketTemplate()
            {
                Id = 2302,
                Name = "Market 2302 name",
                Description = "Market 2302 description",
                OrganiserId = 2302
            });
            context.MarketInstances.Add(new MarketInstance()
            {
                MarketTemplateId = 2302,
                Id = 2302,
                IsCancelled = false,
                StartDate = DateTimeOffset.Now,
                EndDate = DateTimeOffset.Now.AddDays(1)
            });

            context.SaveChanges();
        }

        /** All ids within start at 2400 */
        private static async void SeedCancelMarketTestData(ApplicationDbContext context)
        {
            context.Users.AddRange(new ApplicationUser()
            {
                Id = "User2400",
                Email = "User2400@mail",
                UserName = "User2400@mail"
            },
            new ApplicationUser()
            {
                Id = "User2401",
                Email = "User2401@mail",
                UserName = "User2401@mail"
            });

            context.UserInfo.AddRange(new Domain.Entities.User()
            {
                IdentityId = "User2400",
                Email = "User2400@mail",
                Country = "Test",
                DateOfBirth = new DateTime(1990, 1, 1),
                FirstName = "Firstname User2400",
                LastName = "Lastname User2400",
                Phone = "12345678"
            },
            new Domain.Entities.User()
            {
                IdentityId = "User2401",
                Email = "User2401@mail",
                Country = "Test",
                DateOfBirth = new DateTime(1990, 1, 1),
                FirstName = "Firstname User2401",
                LastName = "Lastname User2401",
                Phone = "12345678"
            });
            context.Organisers.AddRange(new Organiser()
            {
                Id = 2400,
                Name = "Organiser 2400",
                Description = "Organiser 2400 Description",
                UserId = "User2400",
                Address = new Address()
                {
                    Id = 2400,
                    Street = "street",
                    Number = "number",
                    Appartment = "apt",
                    City = "city",
                    PostalCode = "postal"
                }
            },
            new Organiser()
            {
                Id = 2401,
                Name = "Organiser 2401",
                Description = "Organiser 2401 Description",
                UserId = "User2401",
                Address = new Address()
                {
                    Id = 2401,
                    Street = "street",
                    Number = "number",
                    Appartment = "apt",
                    City = "city",
                    PostalCode = "postal"
                }
            });

            context.MarketTemplates.Add(new MarketTemplate()
            {
                Id = 2400,
                Name = "Market 2400 name",
                Description = "Market 2400 description",
                OrganiserId = 2400
            });
            context.MarketInstances.Add(new MarketInstance()
            {
                MarketTemplateId = 2400,
                Id = 2400,
                IsCancelled = false,
                StartDate = DateTimeOffset.Now,
                EndDate = DateTimeOffset.Now.AddDays(1)
            });
            context.MarketTemplates.Add(new MarketTemplate()
            {
                Id = 2401,
                Name = "Market 2401 name",
                Description = "Market 2401 description",
                OrganiserId = 2401
            });
            context.MarketInstances.Add(new MarketInstance()
            {
                MarketTemplateId = 2401,
                Id = 2401,
                IsCancelled = false,
                StartDate = DateTimeOffset.Now,
                EndDate = DateTimeOffset.Now.AddDays(1)
            });
            context.SaveChanges();
        }

        /** All ids within start at 2500 */
        private static void SeedBookStallsTestData(ApplicationDbContext context)
        {
            context.Users.AddRange(new ApplicationUser()
            {
                Id = "User2500",
                Email = "User2500@mail",
                UserName = "User2500@mail"
            },
            new ApplicationUser()
            {
                Id = "User2501",
                Email = "User2501@mail",
                UserName = "User2501@mail"
            });

            context.UserInfo.AddRange(new Domain.Entities.User()
            {
                IdentityId = "User2500",
                Email = "User2500@mail",
                Country = "Test",
                DateOfBirth = new DateTime(1990, 1, 1),
                FirstName = "Firstname User2500",
                LastName = "Lastname User2500",
                Phone = "12345678"
            },
            new Domain.Entities.User()
            {
                IdentityId = "User2501",
                Email = "User2501@mail",
                Country = "Test",
                DateOfBirth = new DateTime(1990, 1, 1),
                FirstName = "Firstname User2501",
                LastName = "Lastname User2501",
                Phone = "12345678"
            });
            context.Organisers.AddRange(new Organiser()
            {
                Id = 2500,
                Name = "Organiser 2500",
                Description = "Organiser 2500 Description",
                UserId = "User2500",
                Address = new Address()
                {
                    Id = 2500,
                    Street = "street",
                    Number = "number",
                    Appartment = "apt",
                    City = "city",
                    PostalCode = "postal"
                }
            });
            context.Merchants.AddRange(new Merchant()
            {
                Id = 2500,
                UserId = "User2500",
                Name = "Merchant 2500",
                Description = "Merchant 2500 description"
            },
            new Merchant()
            {
                Id = 2501,
                UserId = "User2501",
                Name = "Merchant 2501",
                Description = "Merchant 2501 description"
            });

            context.MarketTemplates.Add(new MarketTemplate()
            {
                Id = 2500,
                Name = "Market 2500 name",
                Description = "Market 2500 description",
                OrganiserId = 2500
            });
            context.MarketInstances.Add(new MarketInstance()
            {
                MarketTemplateId = 2500,
                Id = 2500,
                IsCancelled = false,
                StartDate = DateTimeOffset.Now,
                EndDate = DateTimeOffset.Now.AddDays(1)
            });
            context.MarketTemplates.Add(new MarketTemplate()
            {
                Id = 2501,
                Name = "Market 2501 name",
                Description = "Market 2501 description",
                OrganiserId = 2500
            });
            context.MarketInstances.Add(new MarketInstance()
            {
                MarketTemplateId = 2501,
                Id = 2501,
                IsCancelled = true,
                StartDate = DateTimeOffset.Now,
                EndDate = DateTimeOffset.Now.AddDays(1)
            });
            context.StallTypes.AddRange(new StallType()
            {
                Id = 2500,
                Name = "Stalltype 2500",
                Description = "Stalltype 2500 description",
                MarketTemplateId = 2500
            },
            new StallType()
            {
                Id = 2501,
                Name = "Stalltype 2501",
                Description = "Stalltype 2501 description",
                MarketTemplateId = 2500
            },
            new StallType()
            {
                Id = 2502,
                Name = "Stalltype 2502",
                Description = "Stalltype 2502 description",
                MarketTemplateId = 2501
            });
            context.Stalls.AddRange(
            new Stall()
            {
                Id = 2500,
                StallTypeId = 2500,
                MarketInstanceId = 2500
            },
            new Stall()
            {
                Id = 2501,
                StallTypeId = 2500,
                MarketInstanceId = 2500
            },
            new Stall()
            {
                Id = 2502,
                StallTypeId = 2501,
                MarketInstanceId = 2500
            },
            new Stall()
            {
                Id = 2503,
                StallTypeId = 2502,
                MarketInstanceId = 2501
            });
            context.SaveChanges();
        }

        /** All ids within start at 2600 */
        private static void SeedGetMarketInstanceQueryTestData(ApplicationDbContext context)
        {
            context.Users.AddRange(new ApplicationUser()
            {
                Id = "User2600",
                Email = "User2600@mail",
                UserName = "User2600@mail"
            });

            context.UserInfo.AddRange(new Domain.Entities.User()
            {
                IdentityId = "User2600",
                Email = "User2600@mail",
                Country = "Test",
                DateOfBirth = new DateTime(1990, 1, 1),
                FirstName = "Firstname User2600",
                LastName = "Lastname User2600",
                Phone = "12345678"
            });
            context.Organisers.AddRange(new Organiser()
            {
                Id = 2600,
                Name = "Organiser 2600",
                Description = "Organiser 2600 Description",
                UserId = "User2600",
                Address = new Address()
                {
                    Id = 2600,
                    Street = "street",
                    Number = "number",
                    Appartment = "apt",
                    City = "city",
                    PostalCode = "postal"
                }
            });
            context.Merchants.Add(new Merchant()
            {
                Id = 2600,
                Name = "Merchant 2600",
                Description = "Merchant 2600",
                UserId = "User2600"
            });


            context.MarketTemplates.Add(new MarketTemplate()
            {
                Id = 2600,
                Name = "Market 2600 name",
                Description = "Market 2600 description",
                OrganiserId = 2600
            });
            context.MarketInstances.Add(new MarketInstance()
            {
                MarketTemplateId = 2600,
                Id = 2600,
                IsCancelled = false,
                StartDate = DateTimeOffset.Now,
                EndDate = DateTimeOffset.Now.AddDays(1)
            });

            context.StallTypes.AddRange(new StallType()
            {
                Id = 2600,
                Name = "Stalltype 2600",
                Description = "Stalltype 2600 description",
                MarketTemplateId = 2600
            },
            new StallType()
            {
                Id = 2601,
                Name = "Stalltype 2601",
                Description = "Stalltype 2601 description",
                MarketTemplateId = 2600
            });

            context.Stalls.AddRange(new Stall()
            {
                Id = 2600,
                StallTypeId = 2600,
                MarketInstanceId = 2600
            },
            new Stall()
            {
                Id = 2601,
                StallTypeId = 2600,
                MarketInstanceId = 2600
            },
            new Stall()
            {
                Id = 2602,
                StallTypeId = 2601,
                MarketInstanceId = 2600
            });

            var itemCategory1 = new Category()
            {
                Name = "category 2600"
            };
            var itemCategory2 = new Category()
            {
                Name = "category 2601"
            };
            var itemCategory3 = new Category()
            {
                Name = "category 2602"
            };
            context.ItemCategories.AddRange(itemCategory1, itemCategory2, itemCategory3);

            context.Bookings.AddRange(new Booking()
            {
                Id = "Booking2600",
                BoothName = "booking 2600",
                BoothDescription = "booking 2600 description",
                MerchantId = 2600,
                StallId = 2602,
                ItemCategories = new List<Category>() { itemCategory1, itemCategory2}
            },
            new Booking()
            {
                Id = "Booking2601",
                BoothName = "booking 2601",
                BoothDescription = "booking 2601 description",
                MerchantId = 2600,
                StallId = 2601,
                ItemCategories = new List<Category>() { itemCategory2, itemCategory3 }
            });

            context.SaveChanges();
        }

        /** Ids within start at 2700 */
        private static void SeedGetUsersMarketsTestData(ApplicationDbContext context)
        {
            context.Users.AddRange(new ApplicationUser()
            {
                Id = "User2700",
                Email = "User2700@mail",
                UserName = "User2700@mail"
            });

            context.UserInfo.AddRange(new Domain.Entities.User()
            {
                IdentityId = "User2700",
                Email = "User2700@mail",
                Country = "Test",
                DateOfBirth = new DateTime(1990, 1, 1),
                FirstName = "Firstname User2700",
                LastName = "Lastname User2700",
                Phone = "12345678"
            });
            context.Organisers.AddRange(new Organiser()
            {
                Id = 2700,
                Name = "Organiser 2700",
                Description = "Organiser 2700 Description",
                UserId = "User2700",
                Address = new Address()
                {
                    Id = 2700,
                    Street = "street",
                    Number = "number",
                    Appartment = "apt",
                    City = "city",
                    PostalCode = "postal"
                }
            },
            new Organiser()
            {
                Id = 2701,
                Name = "Organiser 2701",
                Description = "Organiser 2701 Description",
                UserId = "User2700",
                Address = new Address()
                {
                    Id = 2701,
                    Street = "street",
                    Number = "number",
                    Appartment = "apt",
                    City = "city",
                    PostalCode = "postal"
                }
            });
            context.Merchants.Add(new Merchant()
            {
                Id = 2700,
                Name = "Merchant 2700",
                Description = "Merchant 2700",
                UserId = "User2700"
            });


            context.MarketTemplates.Add(new MarketTemplate()
            {
                Id = 2700,
                Name = "Market 2700 name",
                Description = "Market 2700 description",
                OrganiserId = 2700
            });
            context.MarketInstances.Add(new MarketInstance()
            {
                MarketTemplateId = 2700,
                Id = 2700,
                IsCancelled = false,
                StartDate = DateTimeOffset.Now,
                EndDate = DateTimeOffset.Now.AddDays(1)
            });

            context.MarketTemplates.Add(new MarketTemplate()
            {
                Id = 2701,
                Name = "Market 2701 name",
                Description = "Market 2701 description",
                OrganiserId = 2701
            });
            context.MarketInstances.Add(new MarketInstance()
            {
                MarketTemplateId = 2701,
                Id = 2701,
                IsCancelled = false,
                StartDate = DateTimeOffset.Now,
                EndDate = DateTimeOffset.Now.AddDays(1)
            });


            context.StallTypes.AddRange(new StallType()
            {
                Id = 2700,
                Name = "Stalltype 2700",
                Description = "Stalltype 2700 description",
                MarketTemplateId = 2700
            },
            new StallType()
            {
                Id = 2701,
                Name = "Stalltype 2701",
                Description = "Stalltype 2701 description",
                MarketTemplateId = 2701
            });

            context.Stalls.AddRange(new Stall()
            {
                Id = 2700,
                StallTypeId = 2700,
                MarketInstanceId = 2700
            },
            new Stall()
            {
                Id = 2701,
                StallTypeId = 2700,
                MarketInstanceId = 2700
            },
            new Stall()
            {
                Id = 2702,
                StallTypeId = 2701,
                MarketInstanceId = 2701
            },
            new Stall()
            {
                Id = 2703,
                StallTypeId = 2701,
                MarketInstanceId = 2701
            });

            var itemCategory1 = new Category()
            {
                Name = "category 2700"
            };
            var itemCategory2 = new Category()
            {
                Name = "category 2701"
            };
            var itemCategory3 = new Category()
            {
                Name = "category 2702"
            };
            context.ItemCategories.AddRange(itemCategory1, itemCategory2, itemCategory3);

            context.Bookings.AddRange(new Booking()
            {
                Id = "Booking2700",
                BoothName = "booking 2700",
                BoothDescription = "booking 2700 description",
                MerchantId = 2700,
                StallId = 2700,
                ItemCategories = new List<Category>() { itemCategory1, itemCategory2 }
            },
            new Booking()
            {
                Id = "Booking2701",
                BoothName = "booking 2701",
                BoothDescription = "booking 2701 description",
                MerchantId = 2700,
                StallId = 2702,
                ItemCategories = new List<Category>() { itemCategory2, itemCategory3 }
            });

            context.SaveChanges();
        }
    
        /** All ids within start at 2800 
            The date ranges in year 2800 are also used within so please don't use elsewhere
         */
        private static void SeedGetFilteredMarketInstances(ApplicationDbContext context)
        {
            context.Users.AddRange(new ApplicationUser()
            {
                Id = "User2800",
                Email = "User2800@mail",
                UserName = "User2800@mail"
            });

            context.UserInfo.AddRange(new Domain.Entities.User()
            {
                IdentityId = "User2800",
                Email = "User2800@mail",
                Country = "Test",
                DateOfBirth = new DateTime(1990, 1, 1),
                FirstName = "Firstname User2800",
                LastName = "Lastname User2800",
                Phone = "12345678"
            });
            context.Organisers.AddRange(new Organiser()
            {
                Id = 2800,
                Name = "Organiser 2800",
                Description = "Organiser 2800 Description",
                UserId = "User2800",
                Address = new Address()
                {
                    Id = 2800,
                    Street = "street",
                    Number = "number",
                    Appartment = "apt",
                    City = "city",
                    PostalCode = "postal"
                }
            },
            new Organiser()
            {
                Id = 2801,
                Name = "Organiser 2801",
                Description = "Organiser 2801 Description",
                UserId = "User2800",
                Address = new Address()
                {
                    Id = 2801,
                    Street = "street",
                    Number = "number",
                    Appartment = "apt",
                    City = "city",
                    PostalCode = "postal"
                }
            });
            context.Merchants.Add(new Merchant()
            {
                Id = 2800,
                Name = "Merchant 2800",
                Description = "Merchant 2800",
                UserId = "User2800"
            });


            context.MarketTemplates.Add(new MarketTemplate()
            {
                Id = 2800,
                Name = "Market 2800 name",
                Description = "Market 2800 description",
                OrganiserId = 2800
            });
            context.MarketInstances.Add(new MarketInstance()
            {
                MarketTemplateId = 2800,
                Id = 2800,
                IsCancelled = false,
                StartDate = new DateTimeOffset(new DateTime(2800, 1, 1)),
                EndDate = new DateTimeOffset(new DateTime(2800, 1, 16))
            });

            context.MarketTemplates.Add(new MarketTemplate()
            {
                Id = 2801,
                Name = "Market 2801 name",
                Description = "Market 2801 description",
                OrganiserId = 2801
            });
            context.MarketInstances.Add(new MarketInstance()
            {
                MarketTemplateId = 2801,
                Id = 2801,
                IsCancelled = false,
                StartDate = new DateTimeOffset(new DateTime(2800, 2, 1)),
                EndDate = new DateTimeOffset(new DateTime(2800, 2, 16))
            });


            context.StallTypes.AddRange(new StallType()
            {
                Id = 2800,
                Name = "Stalltype 2800",
                Description = "Stalltype 2800 description",
                MarketTemplateId = 2800
            },
            new StallType()
            {
                Id = 2801,
                Name = "Stalltype 2801",
                Description = "Stalltype 2801 description",
                MarketTemplateId = 2801
            });

            context.Stalls.AddRange(new Stall()
            {
                Id = 2800,
                StallTypeId = 2800,
                MarketInstanceId = 2800
            },
            new Stall()
            {
                Id = 2801,
                StallTypeId = 2800,
                MarketInstanceId = 2800
            },
            new Stall()
            {
                Id = 2802,
                StallTypeId = 2801,
                MarketInstanceId = 2801
            },
            new Stall()
            {
                Id = 2803,
                StallTypeId = 2801,
                MarketInstanceId = 2801
            });

            var itemCategory1 = new Category()
            {
                Name = "category 2800"
            };
            var itemCategory2 = new Category()
            {
                Name = "category 2801"
            };
            var itemCategory3 = new Category()
            {
                Name = "category 2802"
            };
            context.ItemCategories.AddRange(itemCategory1, itemCategory2, itemCategory3);

            context.Bookings.AddRange(new Booking()
            {
                Id = "Booking2800",
                BoothName = "booking 2800",
                BoothDescription = "booking 2800 description",
                MerchantId = 2800,
                StallId = 2800,
                ItemCategories = new List<Category>() { itemCategory1, itemCategory2 }
            },
            new Booking()
            {
                Id = "Booking2801",
                BoothName = "booking 2801",
                BoothDescription = "booking 2801 description",
                MerchantId = 2800,
                StallId = 2802,
                ItemCategories = new List<Category>() { itemCategory2, itemCategory3 }
            });

            context.SaveChanges();
        }

        /** All ids within start at 2900 */
        private static void SeedAddMerchantContactInformationTest(ApplicationDbContext context)
        {
            context.Users.AddRange(new ApplicationUser()
            {
                Id = "User2900",
                Email = "User2900@mail",
                UserName = "User2900@mail"
            },
            new ApplicationUser()
            {
                Id = "User2901",
                Email = "User2901@mail",
                UserName = "User2901@mail"
            });
            context.UserInfo.AddRange(new Domain.Entities.User()
            {
                IdentityId = "User2900",
                Email = "User2900@mail",
                Country = "Test",
                DateOfBirth = new DateTime(1990, 1, 1),
                FirstName = "Firstname User2900",
                LastName = "Lastname User2900",
                Phone = "12345678"
            },
            new Domain.Entities.User()
            {
                IdentityId = "User2901",
                Email = "User2901@mail",
                Country = "Test",
                DateOfBirth = new DateTime(1990, 1, 1),
                FirstName = "Firstname User2901",
                LastName = "Lastname User2901",
                Phone = "12345678"
            });
            context.Merchants.AddRange(new Merchant()
            {
                Id = 2900,
                Name = "Merchant 2900",
                Description = "Merchant 2900 description",
                UserId = "User2900"
            },
            new Merchant()
            {
                Id = 2901,
                Name = "Merchant 2901",
                Description = "Merchant 2901 description",
                UserId = "User2901"
            });
            context.MerchantContactInfos.Add(new MerchantContactInfo()
            {
                MerchantId = 2901,
                Value = "value",
                ContactType = ContactInfoType.PHONE_NUMER
            });
            context.SaveChanges();
        }
    }
}
