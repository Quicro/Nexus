using Microsoft.EntityFrameworkCore;
using NexusEF.Models.Nexus;
using NexusEF.Models.Insurance;
using NexusEF.Models.Insurance.Context;
using NexusEF.Models.Mevino.Context;
using NexusEF.Models.Mevino;

namespace NexusEF.DatabaseManager
{
#if DEBUG
    public class AppDatabaseManager : GenericDatabaseManager<MevinoContextInMemory>
    {
        private static readonly GenericDatabaseManager<MevinoContextInMemory> appDatabaseManager;
#endif
#if !DEBUG
    public class DatabaseManager : GenericDatabaseManager<NexusContext> {
        static GenericDatabaseManager<NexusDatabaseManager> databaseManager;
#endif
    }

    public class GenericDatabaseManager<T> where T : DbContext, new()
    {
#if DEBUG
        public static MevinoContextInMemory context = new();
#endif
#if !DEBUG
        public static MevinoContext context = new();
#endif
        public static List<NexusTask> tasks = new();

        static GenericDatabaseManager()
        {
            if (context is not null)
            {
                setupInMemoryDatabase();
            }
        }

        private static void setupInMemoryDatabase()
        {
            // Adding Sellers
            context.Sellers.Add(new Seller { Name = "Hilde" });
            context.Sellers.Add(new Seller { Name = "Bart" });

            // Adding Products
            var products = new List<Product>
            {
                // Wines
                new Product { Name = "Merlot Bottle", Price = 15.99m, WineProduct = new WineProduct { Category = "Red", Region = "Bordeaux", Grape = "Merlot" }},
                new Product { Name = "Chardonnay Bottle", Price = 12.50m, WineProduct = new WineProduct { Category = "White", Region = "Napa Valley", Grape = "Chardonnay" }},
                new Product { Name = "Cabernet Sauvignon Bottle", Price = 18.20m, WineProduct = new WineProduct { Category = "Red", Region = "Sonoma", Grape = "Cabernet Sauvignon" }},
                new Product { Name = "Pinot Noir Bottle", Price = 22.99m, WineProduct = new WineProduct { Category = "Red", Region = "Oregon", Grape = "Pinot Noir" }},
                new Product { Name = "Sauvignon Blanc Bottle", Price = 10.00m, WineProduct = new WineProduct { Category = "White", Region = "Marlborough", Grape = "Sauvignon Blanc" }},
                new Product { Name = "Riesling Bottle", Price = 13.99m, WineProduct = new WineProduct { Category = "White", Region = "Mosel", Grape = "Riesling" }},
                new Product { Name = "Zinfandel Bottle", Price = 16.95m, WineProduct = new WineProduct { Category = "Red", Region = "California", Grape = "Zinfandel" }},
                new Product { Name = "Syrah Bottle", Price = 21.00m, WineProduct = new WineProduct { Category = "Red", Region = "Rhone Valley", Grape = "Syrah" }},
                new Product { Name = "Malbec Bottle", Price = 19.50m, WineProduct = new WineProduct { Category = "Red", Region = "Mendoza", Grape = "Malbec" }},
                new Product { Name = "Gewürztraminer Bottle", Price = 14.25m, WineProduct = new WineProduct { Category = "White", Region = "Alsace", Grape = "Gewürztraminer" }},

                // Beers
                new Product { Name = "Craft Lager", Price = 2.99m, BeerProduct = new BeerProduct { Brewery = "Local Craft", Type = "Lager" }},
                new Product { Name = "IPA Craft Beer", Price = 3.50m, BeerProduct = new BeerProduct { Brewery = "Craft IPA Co.", Type = "IPA" }},

                // Glasses
                new Product { Name = "Standard Wine Glass", Price = 5.00m, GlassProduct = new GlassProduct { Material = "Glass", Size = "Standard" }},
                new Product { Name = "Premium Wine Glass", Price = 12.00m, GlassProduct = new GlassProduct { Material = "Crystal", Size = "Premium" }},
                new Product { Name = "Crystal Wine Glass", Price = 15.00m, GlassProduct = new GlassProduct { Material = "Crystal", Size = "Deluxe" }},
                new Product { Name = "Decorative Wine Glass", Price = 9.50m, GlassProduct = new GlassProduct { Material = "Decorated Glass", Size = "Standard" }},
                new Product { Name = "Bulk Wine Glasses (Set of 6)", Price = 40.00m, GlassProduct = new GlassProduct { Material = "Glass", Size = "Bulk Pack" }},

                // Merchandise
                new Product { Name = "Wine Lover's T-shirt", Price = 25.00m, MerchProduct = new MerchProduct { Material = "Cotton", Type = "T-shirt" }}
            };


            context.Products.AddRange(products);

            var orders = new List<Order>
            {
                new Order {
                    OrderLines = new List<OrderLine>
                    {
                        new OrderLine { Product = products[0], Quantity = 2 }, // Merlot Bottle
                        new OrderLine { Product = products[4], Quantity = 1 }  // Sauvignon Blanc Bottle
                    }
                },
                new Order {
                    OrderLines = new List<OrderLine>
                    {
                        new OrderLine { Product = products[1], Quantity = 3 } // Chardonnay Bottle
                    }
                },
                new Order {
                    OrderLines = new List<OrderLine>
                    {
                        new OrderLine { Product = products[2], Quantity = 1 }, // Cabernet Sauvignon Bottle
                        new OrderLine { Product = products[3], Quantity = 1 }  // Pinot Noir Bottle
                    }
                },
                new Order {
                    OrderLines = new List<OrderLine>
                    {
                        new OrderLine { Product = products[5], Quantity = 2 }, // Riesling Bottle
                        new OrderLine { Product = products[7], Quantity = 1 }, // Syrah Bottle
                        new OrderLine { Product = products[9], Quantity = 1 }  // Gewürztraminer Bottle
                    }
                },
                new Order {
                    OrderLines = new List<OrderLine>
                    {
                        new OrderLine { Product = products[6], Quantity = 1 }, // Zinfandel Bottle
                        new OrderLine { Product = products[8], Quantity = 1 }  // Malbec Bottle
                    }
                }
            };

            context.Orders.AddRange(orders);

            // Save changes to the in-memory database
            context.SaveChanges();
        }

        public static Task Load<T>(IQueryable<T> queryable, Action<List<T>?> actionWhenDone)
        {
            List<T>? list = null;
            CancellationTokenSource source = new();

            Task task = new TaskFactory().StartNew(() =>
            {
                Task<List<T>> task = queryable.ToListAsync(source.Token);

                try
                {
                    task.Wait();
                    list = task.Result;
                }
                catch (AggregateException)
                {
                    //LoggerBla.DatabaseManagerError(aggexp.Message);
                }
            }, source.Token);

            task.ContinueWith(
                t => actionWhenDone(list),
                TaskContinuationOptions.OnlyOnRanToCompletion
            );

            tasks.Add(
                new NexusTask(
                    source,
                    task
                )
            );

            return task;
        }
    }
}
