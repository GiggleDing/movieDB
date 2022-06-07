using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MvcMovie.Models;

    public class MvcCollectionContext : DbContext
    {
        public MvcCollectionContext (DbContextOptions<MvcCollectionContext> options)
            : base(options)
        {
        }

        public DbSet<MvcMovie.Models.Collection>? Collection { get; set; }
    }
