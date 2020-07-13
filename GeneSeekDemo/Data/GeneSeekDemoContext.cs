using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using GeneSeekDemo.Models.DataHubCore;

namespace GeneSeekDemo.Models
{
    public class GeneSeekDemoContext : DbContext
    {
        public GeneSeekDemoContext (DbContextOptions<GeneSeekDemoContext> options)
            : base(options)
        {
        }

        public DbSet<GeneSeekDemo.Models.DataHubCore.Customer> Customer { get; set; }
    }
}
