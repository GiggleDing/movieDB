using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MvcMovie.Models;

    public class MvcAttentionContext : DbContext
    {
        public MvcAttentionContext (DbContextOptions<MvcAttentionContext> options)
            : base(options)
        {
        }

        public DbSet<MvcMovie.Models.Attention>? Attention { get; set; }

        public DbSet<MvcMovie.Models.UserInfo> UserInfo { get; set; }
    }
