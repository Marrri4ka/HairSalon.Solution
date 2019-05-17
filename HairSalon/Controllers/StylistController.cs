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

	Stylist newStylist = Stylist.Find(id);
	ViewBag.Clients = Client.GetAll();
	ViewBag.Stylist = Stylist.Find(id);
	ViewBag.Clients1 = newStylist.GetClients();
	return View();
}

[HttpPost("/stylists/{id}/addclient")]
public ActionResult AddClient(int id, int clientId)
{
	Stylist newStylist = Stylist.Find(id);
	newStylist.AddClient(Client.Find(clientId));
	ViewBag.Clients = Client.GetAll();
	ViewBag.Stylist = Stylist.Find(id);
	ViewBag.Clients1 = newStylist.GetClients();

	return View("Show");
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

[HttpPost("/stylists/{stylistId}/clients/{clientId}/editclient")]
public ActionResult Update(int stylistId, int clientId, string newName, DateTime newAppointment)
{
	Client client = Client.Find(clientId);
	client.Edit(newName, newAppointment);
	Dictionary<string, object> model = new Dictionary<string, object>();
	Stylist stylist = Stylist.Find(stylistId);
	List<Client> stylistClients = stylist.GetClients();
	model.Add("stylist", stylist);
	model.Add("clients", stylistClients);
	return View("Show", model);
}

[HttpGet("/stylists/{stylistId}/edit")]
public ActionResult Edit(int stylistId)

{
	Stylist foundStylist = Stylist.Find(stylistId);
	return View("Edit",foundStylist );
}

[HttpPost("/stylists/{id}/editedStylist")]
public ActionResult Edit(int id, string stylistName)
{
	Stylist newStylist = Stylist.Find(id);
	newStylist.Edit(stylistName);
	List<Stylist> allStylists = Stylist.GetAll();
	return View("Index", allStylists);
}
[HttpGet("/stylists/clientsort")]
public ActionResult Sort(string clientName, DateTime appoinment)
{
	Dictionary<string, object> model = new Dictionary<string, object>();

	List<Client> sortedClients = Client.Sort();
	model.Add("clients", sortedClients);

	return View("Show", model);
}



}
}
