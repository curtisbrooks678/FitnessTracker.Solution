using System.Collections.Generic;

namespace FitnessTracker.Models
{
  public class Gym
  {
    public int GymId { get; set; }
    public string Location { get; set; }
    public virtual ICollection<Routine> Routines { get; set; }
    public virtual ICollection<Member> Members { get; set; }

    public Gym()
    {
      this.Routines = new HashSet<Routine>();
      this.Members = new HashSet<Member>();
    }
  }
}