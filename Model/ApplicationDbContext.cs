﻿using Microsoft.EntityFrameworkCore;
using Model.EntityConfiguration;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Model
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {

        }
        public DbSet<ExpenseHeader> ExpenseHeaders { get; set; }
        public DbSet<ExpenseLine> ExpenseLines { get; set; }
        public DbSet<User> Users { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            builder.Entity<ExpenseLine>()
                .Property(e => e.TotalCost)
                .HasComputedColumnSql("[Quantity] * [UnitCost]");

            builder.Entity<ExpenseHeader>()
                .Property(e => e.UsdExchangeRate)
                .HasColumnType($"decimal(13,4)")
                .IsRequired(true);

            //builder.ApplyConfiguration(new UserConfiguration());
            //builder.Entity<User>()
            //    .Property(e => e.FullName)
            //    .HasComputedColumnSql("[FirstName] + ' ' + [LastName]");

            builder.Entity<ExpenseHeader>()
                .HasOne(e => e.Requester)
                .WithMany(e => e.RequesterExpenseHeaders)
                .HasForeignKey(e => e.RequesterId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired(true);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {

        }
    }
}