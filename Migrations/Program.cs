using System;
using System.Reflection;
using FluentMigrator;
using FluentMigrator.Runner;

using Microsoft.Extensions.DependencyInjection;

namespace Migrations
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = CreateServices();

            var test = Assembly.GetAssembly(typeof(InsertDataCatalogLevel));

            // Put the database update into a scope to ensure
            // that all resources will be disposed.
            using (var scope = serviceProvider.CreateScope())
            {
                UpdateDatabase(scope.ServiceProvider);
            }

            Console.ReadKey();
        }

        /// <summary>
        /// Configure the dependency injection services
        /// </summary>
        private static IServiceProvider CreateServices()
        {
            return new ServiceCollection()
                // Add common FluentMigrator services
                .AddFluentMigratorCore()
                .ConfigureRunner(rb => rb
                    
                    // Add Postgres
                    .AddPostgres()
                    // Set the connection string
                    .WithGlobalConnectionString("Server=localhost;Port=5432;User Id=postgres;Password=StormSpirit;Database=TestExercise")

                    // Add SQLite support to FluentMigrator
                    //.AddSQLite()
                    // Set the connection string
                    //.WithGlobalConnectionString("Server=localhost;Port=5432;User Id=postgres;Password=StormSpirit;Database=TestExercise")

                    // Define the assembly containing the migrations
                    .ScanIn(Assembly.GetAssembly(typeof(InsertDataCatalogLevel))).For.All())
                // Enable logging to console in the FluentMigrator way
                .AddLogging(lb => lb.AddFluentMigratorConsole())
                // Build the service provider
                .BuildServiceProvider(false);


            //.WithMigrationsIn(Assembly.GetAssembly(typeof(InsertDataCatalogLevel)))
        }

        /// <summary>
        /// Update the database
        /// </summary>
        private static void UpdateDatabase(IServiceProvider serviceProvider)
        {
            // Instantiate the runner
            var runner = serviceProvider.GetRequiredService<IMigrationRunner>();
            
            try
            {
                // Execute the migrations
                runner.MigrateUp();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }
    }
}
