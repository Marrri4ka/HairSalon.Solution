using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Mvc;
using HairSalon.Models;

namespace HairSalon.Controllers
{
public class StylistsController : Controller
{
[HttpGet("/stylists")]
public ActionResult Index()
{
	List<Stylist> allStylists = Stylist.GetAll();
	return View(allStylists);
}

[HttpPost("/stylists")]
public ActionResult Create(string stylistName)
{
	Stylist newStylist = new Stylist(stylistName);
	newStylist.Save();
	List<Stylist> allStylists = Stylist.GetAll();
	return View("Index", allStylists);
}

[HttpGet("/stylists/new")]
public ActionResult New()
{
	return View();
}


[HttpGet("/stylists/{id}")]
public ActionResult Show(int id)
{
	Dictionary<string, object> model = new Dictionary<string, object>();
	Stylist selectedStylist = Stylist.Find(id);
	List<Client> stylistClient = selectedStylist.GetClients();
	model.Add("stylist", selectedStylist);
	model.Add("clients", stylistClient);
	return View(model);
}

[HttpPost("/stylists/{stylistId}/clients")]
public ActionResult Create(int stylistId, string clientName, DateTime clientAppointment)
{
	Dictionary<string, object> model = new Dictionary<string, object>();
	Stylist foundStylist = Stylist.Find(stylistId);
	Client newClient = new Client(clientName, clientAppointment, stylistId);
	newClient.Save();
	List<Client> stylistClients = foundStylist.GetClients();
	model.Add("clients", stylistClients);
	model.Add("stylist", foundStylist);
	return View("Show", model);
}

[HttpPost("/stylists/{stylistId}/clients/{clientId}/deleteclient")]
public ActionResult DeleteClient(int stylistId, int clientId)
{
	Client client = Client.Find(clientId);
	client.Delete();
	Dictionary<string, object> model = new Dictionary<string, object>();
	Stylist stylist = Stylist.Find(stylistId);
	List<Client> stylistClients = stylist.GetClients();
	model.Add("stylist", stylist);
	model.Add("clients", stylistClients);
	return View("Show", model);
}
[HttpGet("/stylists/{stylistId}/delete")]
public ActionResult Delete(int stylistId)

{
	Stylist foundStylist = Stylist.Find(stylistId);
	foundStylist.Delete();
	return RedirectToAction("Index");
}


}
}
