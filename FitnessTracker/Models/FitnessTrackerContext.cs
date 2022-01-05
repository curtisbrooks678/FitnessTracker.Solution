using Microsoft.EntityFrameworkCore;

namespace FitnessTracker.Models
{
  public class FitnessTrackerContext : DbContext
  {
    public DbSet<Member> Members { get; set; }
    public DbSet<Routine> Routines { get; set; }
    public DbSet<MemberRoutine> MemberRoutine { get; set; }
    public DbSet<Gym> Gyms { get; set; }

    public FitnessTrackerContext(DbContextOptions options) : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      optionsBuilder.UseLazyLoadingProxies();
    }
  }
}