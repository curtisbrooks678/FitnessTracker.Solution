using System.Collections.Generic;
using System;

namespace FitnessTracker.Models
{
  public class Member
  {
    public int MemberId { get; set; }
    public string Name { get; set; }
    public DateTime StartDate { get; set; }
    public virtual ICollection<MemberRoutine> JoinEntities { get; set; }
    public virtual Gym Gym { get; set; }
    public int GymId { get; set; }
    public Member()
    {
      this.JoinEntities = new HashSet<MemberRoutine>();
    }    
  }
}