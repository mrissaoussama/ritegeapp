using Microsoft.EntityFrameworkCore;
using RitegeDomain.Model;
using SQLite;
using System;
using System.Diagnostics;
using System.IO;
using Xamarin.Essentials;

namespace ritegeapp.Services
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<NotificationToken> Token { get; set; }
        private const string DatabaseFilename = "ritegeparking.db3";

        public const SQLite.SQLiteOpenFlags Flags =
          // open the database in read/write mode
          SQLite.SQLiteOpenFlags.ReadWrite |
          // create the database if it doesn't exist
          SQLite.SQLiteOpenFlags.Create |
          // enable multi-threaded database access
          SQLite.SQLiteOpenFlags.SharedCache;

        public static string DatabasePath
        {
            get
            {
                var basePath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                return Path.Combine(basePath, DatabaseFilename);
            }
        }
        public ApplicationDbContext()
        {
            SQLitePCL.Batteries_V2.Init(); 
            this.Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string dbPath = Path.Combine(FileSystem.AppDataDirectory, DatabaseFilename);
            if(!File.Exists(dbPath))
            File.Create(dbPath);
            optionsBuilder
                .UseSqlite($"Filename={dbPath}");
        }
    }
}
