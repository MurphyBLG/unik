using BulletinBoard.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace BulletinBoard.Server.Persistance;

public class MainContext(DbContextOptions<MainContext> options) : DbContext(options)
{
    public DbSet<Advertisement> Ads { get; set; } = null!;
}
