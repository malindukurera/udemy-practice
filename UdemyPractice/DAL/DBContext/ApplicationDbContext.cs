﻿using System;
using System.Collections.Generic;
using System.Text;
using DAL.Model;
using Microsoft.EntityFrameworkCore;

namespace DAL.DBContext
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Department> Departments { get; set; }
        public DbSet<Student> Students { get; set; }    
    }
}
