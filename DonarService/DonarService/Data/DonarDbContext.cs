using DonarService.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DonarService.Data
{
    public class DonarDbContext:DbContext
    {
        public DonarDbContext(DbContextOptions<DonarDbContext> options)
 : base(options)
        {
        }

        public virtual DbSet<Donar> DonarDetails { get; set; }

    }
}
