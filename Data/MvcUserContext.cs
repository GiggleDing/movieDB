using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MvcMovie.Models;

    public class MvcUserContext : DbContext
    {
        public MvcUserContext (DbContextOptions<MvcUserContext> options)
            : base(options)
        {
        }

        public DbSet<MvcMovie.Models.User> User { get; set; }
    }
