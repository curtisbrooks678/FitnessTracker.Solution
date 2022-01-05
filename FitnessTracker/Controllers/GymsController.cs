using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using FitnessTracker.Models;

namespace FitnessTracker.Controllers
{
  public class GymsController : Controllers
  {
    private readonly FitnessTrackerContext _db;
    public GymsController(FitnessTrackerContext db)
    {
      _db = db;
    }

    public ActionResult Index()
    {
      return View(_db.Gyms.ToList());
    }

    public ActionResult Details(int id)
    {
      Gym thisGym = _db.Gyms
        .Include(gym => gym.Routines)
        .Include(gym => gym.Members)
        .FirstOrDefault(gym => gym.GymId == id);
      return View(thisGym);
    }
    
    public ActionResult Create()
    {
      return View();
    }
    
    [HttpPost]
    public ActionResult Create(Gym gym)
    {
      _db.Gyms.Add(gym);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Edit (int id)
    {
      Gym thisGym = _db.Gyms.FirstOrDefault(gym => gym.GymId ==id);
      return View(thisGym);
    }

    [HttpPost]
    public ActionResult Edit(Gym gym)
    {
      _db.Entry(gym).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Delete(int id)
    {
      Gym thisGym = _db.Gyms.FirstOrDefault(gym => gym.GymId == id);
      return View(thisGym);
    }



    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      Gym thisGym = _db.Gyms.FirstOrDefault(gym => gym.GymId == id);
      _db.Gyms.Remove(thisGym);
      _db.SaveChanges();
      return RedirectToAction("Index"); 
    }
  }
}