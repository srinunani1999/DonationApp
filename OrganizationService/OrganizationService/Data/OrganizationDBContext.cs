using Microsoft.EntityFrameworkCore;
using OrganizationService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrganizationService.Data
{
    public class OrganizationDBContext:DbContext
    {
        public OrganizationDBContext(DbContextOptions<OrganizationDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Organization> Organization { get; set; }
    }
}
