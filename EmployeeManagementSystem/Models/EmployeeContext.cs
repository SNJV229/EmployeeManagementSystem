using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Collections.Generic;
using System.Reflection.PortableExecutable;
using System;

namespace EmployeeManagementSystem.Models
{

    //Database Context is the main class that coordinates between domain classes and database
    public class EmployeeContext : DbContext
    {
        public EmployeeContext(DbContextOptions<EmployeeContext> options) : base(options)
        {

        }

        //DbSet exposes properties which represent the collection of specified entity.Once you have DbContext
        //ready we can make a query to the database, change tracking, persisting data, caching of data, manage entity relationships.
        public DbSet<Employee> Employees { get; set; }
    }
}
