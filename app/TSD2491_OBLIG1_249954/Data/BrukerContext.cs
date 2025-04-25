using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TSD2491_OBLIG1_249954.Models;

namespace TSD2491_OBLIG1_249954.Data
{
    public class BrukerContext : DbContext
    {
        public BrukerContext (DbContextOptions<BrukerContext> options)
            : base(options)
        {
        }

        public DbSet<TSD2491_OBLIG1_249954.Models.Bruker> Bruker { get; set; } = default!;
    }
}
