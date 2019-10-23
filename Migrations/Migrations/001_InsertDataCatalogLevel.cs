using System;
using FluentMigrator;
using Npgsql;

namespace Migrations.Migrations
{
    [Migration(1)]
    public class InsertDataCatalogLevel : Migration
    {
        public override void Up()
        {
            Execute.WithConnection((connection, transaction) =>
            {
                var pgConnection = (NpgsqlConnection) connection;

                try
                {
                    using (var command = pgConnection.CreateCommand())
                    {
                        command.CommandText = @"
INSERT INTO app.catalog_level(name, description)
SELECT name, description
FROM app.catalog;
";
                        command.ExecuteNonQuery();

                        command.CommandText = @"
INSERT INTO app.catalog_level(parent_id, name, description)
SELECT cl.id, ca.name, ca.description
FROM app.catalog_aggregate as ca
INNER JOIN app.catalog as c ON ca.catalog_id = c.id
INNER JOIN app.catalog_level as cl ON c.name = cl.name;
";

                        command.ExecuteNonQuery();

                        command.CommandText = @"
INSERT INTO app.catalog_level(parent_id, name, description)
SELECT cl.id, cm.name, cm.description
FROM app.catalog_model as cm
INNER JOIN app.catalog_aggregate as ca ON cm.catalog_aggregate_id = ca.id
INNER JOIN app.catalog as c ON ca.catalog_id = c.id 
INNER JOIN (SELECT * FROM app.catalog_level WHERE parent_id IS NOT NULL) as cl ON cl.name = ca.name
	AND cl.parent_id = (SELECT id FROM app.catalog_level WHERE name = c.name);
";
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            });
        }

        public override void Down()
        {
            Execute.Sql($"TRUNCATE app.catalog_level RESTART IDENTITY");
        }
    }
}
