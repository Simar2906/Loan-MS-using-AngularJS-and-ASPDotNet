using BCrypt.Net;
using System.Collections.Generic;
using Dapper;
using Npgsql;

namespace Password_Hasher
{
    internal class Program
    {
        static void Main(string[] args)
        {


            // Fetch existing passwords from the database
            // For demonstration, let's assume you have a method to retrieve passwords
            string connectionString = "Host=localhost;Port=5432;Database=LMS2;Username=postgres;Password=simarjit;";

            using (var connection = new NpgsqlConnection(connectionString))
            {
                List<string> existingPasswords = connection.Query<string>("SELECT \"Password\" FROM \"Users\"").ToList();

                foreach (string existingPassword in existingPasswords)
                {
                    // Generate a new salt for each password
                    string salt = BCrypt.Net.BCrypt.GenerateSalt();

                    // Hash the existing password with the generated salt
                    string hashedPassword = BCrypt.Net.BCrypt.HashPassword(existingPassword, salt);

                    // Update the database with the new hashed password and salt
                    connection.Execute("UPDATE \"Users\" SET \"Password\" = @HashedPassword, \"Salt\" = @Salt WHERE \"Password\" = @ExistingPassword",
                    new { HashedPassword = hashedPassword, Salt = salt, ExistingPassword = existingPassword });
                }
                //List<string> existingPasswords = connection.Query<string>("SELECT \"Password\" FROM \"Users\"").ToList();
                //List<string> existingSalt = connection.Query<string>("SELECT \"Salt\" FROM \"Users\"").ToList();
                //for (int i = 0; i<existingPasswords.Count; i++)
                //{ 
                //    string normal = BCrypt.Net.BCrypt.HashPassword("john12345", existingSalt[i]);
                //    if(normal == existingPasswords[i])
                //    {
                //        Console.WriteLine(true);
                //    }
                //    else
                //    {
                //        Console.WriteLine(false);
                //    }
                //}
            }
        }
    }
}
