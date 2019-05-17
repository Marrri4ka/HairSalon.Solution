using Microsoft.AspNetCore.Mvc;
using HairSalon.Models;
using System.Collections.Generic;
using System;

namespace HairSalon.Controllers
{
public class SpecialityController : Controller
{
[HttpGet("/specialities")]
public ActionResult Index()
{
	List<Speciality> allSpecialities = Speciality.GetAll();
	return View(allSpecialities);
}

[HttpGet("/specialities/new")]
public ActionResult New()
{

	return View();
}

[HttpPost("/specialities")]
public ActionResult Create(string specialityName)
{
	Speciality newSpeciality = new Speciality(specialityName);
	newSpeciality.Save();
	List<Speciality> allSpecialities= Speciality.GetAll();
	return View("Index", allSpecialities);
}

[HttpGet("/specialities/{id}")]
public ActionResult Show(int id)

{
	Speciality newSpeciality = Speciality.Find(id);
	ViewBag.Stylists = Stylist.GetAll();
	ViewBag.Speciality = Speciality.Find(id);
	ViewBag.Stylists1 = newSpeciality.GetStylists();
	return View();
}

// [HttpPost("/specialities/delete")]
// public ActionResult DeleteAll()
// {
//      Speciality.DeleteAll();
//      return View();
// }
//


[HttpPost("/specialities/{id}/addstylist")]
public ActionResult AddStylist(int id, int stylistId)
{
	Speciality newSpeciality = Speciality.Find(id);
	newSpeciality.AddStylist(Stylist.Find(stylistId));
	ViewBag.Stylists = Stylist.GetAll();
	ViewBag.Speciality = Speciality.Find(id);
	ViewBag.Stylists1 = newSpeciality.GetStylists();

	return View("Show");
}


}
}
