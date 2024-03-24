using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public class ProjectDbContext : DbContext
{
    public ProjectDbContext(DbContextOptions<ProjectDbContext> options) : base(options) { }
}