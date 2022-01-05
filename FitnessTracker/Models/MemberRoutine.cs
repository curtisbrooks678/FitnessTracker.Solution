namespace FitnessTracker.Models
{
  public class MemberRoutine
  {
    public MemberRoutine()
    {
      Complete = false;
    }
    public int MemberRoutineId { get; set; }
    public int MemberId { get; set; }
    public int RoutineId { get; set; }
    public bool Complete { get; set; }
    public virtual Member Member { get; set; }
    public virtual Routine Routine { get; set; }
  }
}