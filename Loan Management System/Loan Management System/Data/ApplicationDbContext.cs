﻿using Loan_Management_System.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Loan_Management_System.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .Property(u => u.Salary)
                .HasColumnType("decimal(18, 2)");
            modelBuilder.Entity<User>()
            .Property(u => u.Email).HasColumnType("text");
            base.OnModelCreating(modelBuilder);
        }
    }
}