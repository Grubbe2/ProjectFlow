using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SurfApi.Models;

namespace SurfApi.Data
{
    public class SurfApiContext : DbContext
    {
        public SurfApiContext (DbContextOptions<SurfApiContext> options)
            : base(options)
        {
        }

        public DbSet<SurfApi.Models.Rental> Rental { get; set; } = default!;
        public DbSet<SurfApi.Models.Surfboard> Surfboard { get; set; } = default!;
    }
}
