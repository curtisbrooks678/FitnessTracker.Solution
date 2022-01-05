using System.Collections.Generic;
using System;

namespace FitnessTracker.Models
{
  public class Routine
  {
    public Routine()
    {
      this.JoinEntities = new HashSet<MemberRoutine>();
    }
    public int RoutineId { get; set; }
    public string Name { get; set; }
    public string RoutineDescription { get; set; }
    public virtual ICollection<MemberRoutine> JoinEntities { get; set; }
    public virtual Gym Gym { get; set; }
    public int GymId { get; set; }
  }  
}