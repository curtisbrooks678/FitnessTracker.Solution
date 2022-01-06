using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using FitnessTracker.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FitnessTracker.Controllers
{
  public class MembersController : Controller
  {
    private readonly FitnessTrackerContext _db;

    public MembersController(FitnessTrackerContext db)
    {
      _db = db;
    }

    public ActionResult Index()
    {
      List<Gym> gyms = _db.Gyms.ToList();
      ViewData.Add("gyms", gyms);
      List<Member> model = _db.Members.Include(member => member.Gym).ToList();
      return View(model);
    }

    public ActionResult Details(int id)
    {
      var thisMember = _db.Members
        .Include(member => member.JoinEntities)
        .ThenInclude(join => join.Routine)
        .FirstOrDefault(Member => Member.MemberId == id);
      return View(thisMember);
    }

    public ActionResult Create()
    {
      ViewBag.GymId = new SelectList(_db.Gyms, "GymId", "Location");
      return View();
    }

    [HttpPost]
    public ActionResult Create(Member member)
    {
      _db.Members.Add(member);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Edit(int id)
    {
      ViewBag.GymId = new SelectList(_db.Gyms, "GymId", "Location");
      Member thisMember = _db.Members.FirstOrDefault(member => member.MemberId == id);
      return View(thisMember);
    }

    [HttpPost]
    public ActionResult Edit(Member member)
    {
      _db.Entry(member).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult AddRoutine(int id)
    {
      var thisMember = _db.Members.FirstOrDefault(member => member.MemberId == id);
      ViewBag.RoutineId = new SelectList(_db.Routines, "RoutineId", "Name");
      return View(thisMember);
    }

    [HttpPost]
    public ActionResult AddRoutine(Member member, int RoutineId)
    {
      bool alreadyExists = _db.MemberRoutine.Any(memberRoutine => memberRoutine.MemberId == member.MemberId && memberRoutine.RoutineId == RoutineId);
      if (RoutineId != 0 && !alreadyExists)
      {
        _db.MemberRoutine.Add(new MemberRoutine() { RoutineId = RoutineId, MemberId = member.MemberId });
      }
      _db.SaveChanges();
      if (alreadyExists)
      {
        return RedirectToAction("AddRoutineError");
      }
      return RedirectToAction("Index");
    }

    public ActionResult AddRoutineError()
    {
      return View();
    }

    [HttpPost]
    public ActionResult SwitchCompleted(int joinId)
    {
      var joinEntry = _db.MemberRoutine.FirstOrDefault(entry => entry.MemberRoutineId == joinId);
      joinEntry.Complete = !joinEntry.Complete;
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult CompletedDetails(int id)
    {
      var thisMember = _db.Members
        .Include(member => member.JoinEntities)
        .ThenInclude(join => join.Routine)
        .FirstOrDefault(Member => Member.MemberId == id);
      return View(thisMember);
    }

    [HttpPost]
    public ActionResult DeleteRoutine(int joinId)
    {
      var joinEntry = _db.MemberRoutine.FirstOrDefault(entry => entry.MemberRoutineId == joinId);
      _db.MemberRoutine.Remove(joinEntry);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Delete(int id)
    {
      Member thisMember = _db.Members.FirstOrDefault(member => member.MemberId == id);
      return View(thisMember);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      Member thisMember = _db.Members.FirstOrDefault(member => member.MemberId == id);
      _db.Members.Remove(thisMember);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
  }
}