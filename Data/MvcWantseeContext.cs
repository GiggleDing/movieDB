using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using movieDB.Models;

    public class MvcWantseeContext : DbContext
    {
        public MvcWantseeContext (DbContextOptions<MvcWantseeContext> options)
            : base(options)
        {
        }

        public DbSet<movieDB.Models.wantsee>? wantsee { get; set; }
    }
