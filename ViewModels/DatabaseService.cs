using System;
using System.IO;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Meal_Tracker.ViewModels;
using Microsoft.Data.Sqlite;
using Windows.Storage;


namespace Meal_Tracker
{
    public static class DatabaseHelper
    {

        private const string dbName = "mealtracker.db";

        public static async Task InitializeAsync()
        {
            var localFolder = ApplicationData.Current.LocalFolder;
            var dbFile = await localFolder.TryGetItemAsync(dbName);

            if (dbFile == null)
            {
                var seed = await StorageFile.GetFileFromApplicationUriAsync(
                    new Uri("ms-appx:///Assets/seed.db")
                    );

                await seed.CopyAsync(localFolder, dbName);
            }
        }

        public static SqliteConnection GetConnection()
        {
            var path = Path.Combine(
                    ApplicationData.Current.LocalFolder.Path,
                    dbName
                );

            return new SqliteConnection($"Data Source={path}");

        }

        public static List<Ingredient> GetIngredients()
        {
            var ingredients = new List<Ingredient>();


            try
            {
                using var connection = GetConnection();
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = "SELECT id, name, calories, protein, fat, carb From Ingredients";



                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ingredients.Add(new Ingredient
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Calories = reader.GetInt32(2),
                            Fat = reader.GetInt32(3),
                            Protein = reader.GetInt32(4),
                            Carb = reader.GetInt32(5)
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                // Log or handle the error
                System.Diagnostics.Debug.WriteLine($"Database error: {ex.Message}");
                throw;
            }

            return ingredients;
        }
    

        public static int AddIngredient(Ingredient ingredient)
        {
            try
            {
                using var connection = GetConnection();
                connection.Open();

                // First, let's check what columns actually exist
                var schemaCommand = connection.CreateCommand();
                schemaCommand.CommandText = "PRAGMA table_info(Ingredients)";
                System.Diagnostics.Debug.WriteLine("=== Ingredients Table Schema ===");
                using (var reader = schemaCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        System.Diagnostics.Debug.WriteLine($"Column: {reader.GetString(1)}, Type: {reader.GetString(2)}");
                    }
                }

                var command = connection.CreateCommand();
                command.CommandText = @"
                    INSERT INTO Ingredients (name, calories, protein, fat, carb) 
                    VALUES ($name, $calories, $protein, $fat, $carb)";

                command.Parameters.AddWithValue("$name", ingredient.Name);
                command.Parameters.AddWithValue("$calories", ingredient.Calories);
                command.Parameters.AddWithValue("$protein", ingredient.Protein);
                command.Parameters.AddWithValue("$fat", ingredient.Fat);
                command.Parameters.AddWithValue("$carb", ingredient.Carb);

                command.ExecuteNonQuery();

                // Get the ID of the newly inserted row
                command.CommandText = "SELECT last_insert_rowid()";
                var newId = (long)command.ExecuteScalar();

                return (int)newId;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"AddIngredient error: {ex.Message}");
                throw;
            }
        }

        public static async Task ResetDatabaseAsync()
        {
            try
            {
                var localFolder = ApplicationData.Current.LocalFolder;
                var dbFile = await localFolder.TryGetItemAsync(dbName);

                // Delete existing database if it exists
                if (dbFile != null)
                {
                    await ((StorageFile)dbFile).DeleteAsync();
                    System.Diagnostics.Debug.WriteLine("Old database deleted");
                }

                // Copy fresh seed file
                var seed = await StorageFile.GetFileFromApplicationUriAsync(
                    new Uri("ms-appx:///Assets/seed.db")
                );
                await seed.CopyAsync(localFolder, dbName);
                System.Diagnostics.Debug.WriteLine("Fresh database copied");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"ResetDatabaseAsync error: {ex.Message}");
                throw;
            }
        }
    }
}
