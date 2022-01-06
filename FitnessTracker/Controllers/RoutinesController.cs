using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using FitnessTracker.Models;

namespace FitnessTracker.Controllers
{
  public class RoutinesController : Controller
  {
    private readonly FitnessTrackerContext _db;
    public RoutinesController(FitnessTrackerContext db)
    {
      _db = db;
    }
    
    public ActionResult Index()
    {
      List<Gym> gyms = _db.Gyms.ToList();
      ViewData.Add("gyms", gyms);
      List<Routine> model = _db.Routines.Include(routine => routine.Gym).ToList();
      return View(_db.Routines.ToList());
    }

    public ActionResult Details(int id)
    {
      Routine thisRoutine = _db.Routines
        .Include(routine => routine.JoinEntities)
        .ThenInclude(join => join.Member)
        .FirstOrDefault(Routine => Routine.RoutineId == id);
      return View(thisRoutine);
    }

    public ActionResult Create()
    {
      ViewBag.GymId = new SelectList(_db.Gyms, "GymId", "Location");
      return View();
    }

    [HttpPost]
    public ActionResult Create(Routine Routine)
    {
      _db.Routines.Add(Routine);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Edit(int id)
    {
      ViewBag.GymId = new SelectList(_db.Gyms, "GymId", "Location");
      Routine thisRoutine = _db.Routines.FirstOrDefault(Routine => Routine.RoutineId == id);
      return View(thisRoutine);
    }

    [HttpPost]
    public ActionResult Edit(Routine Routine)
    {
      _db.Entry(Routine).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Delete(int id)
    {
      Routine thisRoutine = _db.Routines.FirstOrDefault(Routine => Routine.RoutineId == id);
      return View(thisRoutine);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      Routine thisRoutine = _db.Routines.FirstOrDefault(Routine => Routine.RoutineId == id);
      _db.Routines.Remove(thisRoutine);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
  }
}