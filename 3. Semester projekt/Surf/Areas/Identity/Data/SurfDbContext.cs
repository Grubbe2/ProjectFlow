using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Surf.Areas.Identity.Data;
using Surf.Models;

namespace Surf.Data;

public class SurfDbContext : IdentityDbContext<ApplicationUser>
{
    public SurfDbContext(DbContextOptions<SurfDbContext> options)
        : base(options)
    {
    }
    public DbSet<Surfboard> Surfboard { get; set; } = default!;

    public DbSet <Rental> Rental { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }
}
