﻿

namespace CarService.JobScheduler.Data
{
    using Microsoft.EntityFrameworkCore;
    using CarServices.JobScheduler.Data.Models;

    public class JobSchedulerDbContext : DbContext
    {
        public JobSchedulerDbContext(DbContextOptions<JobSchedulerDbContext> options)
        : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            builder.Entity<Job>()
                .Property(nameof(Job.DepartmentId))
                .IsRequired();

            builder.Entity<Job>()
             .Property(nameof(Job.CreatedByEmployeeId))
             .IsRequired();

            builder.Entity<Job>()
           .Property(nameof(Job.AssignedEmployeeId))
           .IsRequired();

            builder.Entity<JobService>()
                .Property(js => js.Price)
                .HasColumnType("decimal(18,2)");


            builder.Entity<Job>()
                .HasMany(sc => sc.PurchasedServices)
                    .WithOne()
                .HasForeignKey(sc => sc.JobId);


            base.OnModelCreating(builder);
        }
    }
}
