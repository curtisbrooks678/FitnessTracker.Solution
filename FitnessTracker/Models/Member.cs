using System.Collections.Generic;
using System;
using System.ComponentModel.DataAnnotations;

namespace FitnessTracker.Models
{
  public class Member
  {
    public int MemberId { get; set; }
    public string Name { get; set; }
    [DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}", ApplyFormatInEditMode = true)]
    // [Required ( ErrorMessage = "Start Date field cannot be blank. Please enter member start date.")]
    public DateTime StartDate { get; set; }

    public int RoutinesCompleted { get; set; }
    public virtual ICollection<MemberRoutine> JoinEntities { get; set; }
    public virtual Gym Gym { get; set; }
    public int GymId { get; set; }
    public Member()
    {
      this.JoinEntities = new HashSet<MemberRoutine>();
    }

    public int RoutinesCompletedCounter() 
    {
      var count = 0;
      foreach(MemberRoutine memberroutine in JoinEntities) 
      {
        if(memberroutine.Complete == true) 
        {
          count++;
        }
      }
      return count;
    }    
  }
}