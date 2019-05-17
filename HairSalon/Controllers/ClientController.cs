using Microsoft.AspNetCore.Mvc;
using HairSalon.Models;
using System.Collections.Generic;
using System;

namespace HairSalon.Controllers
{
public class ClientController : Controller
{
[HttpGet("/clients")]
public ActionResult Index()
{
	List<Client> allClients = Client.GetAll();
	return View(allClients);
}

[HttpPost("/deleteall")]
public ActionResult DeleteAll()

{
	Client.ClearAll();
	List<Client> allClients = Client.GetAll();
	return View("Index",allClients);
}

[HttpPost("/clients")]
public ActionResult Create(string clientName, DateTime clientAppointment)
{
	Client newClient = new Client(clientName,clientAppointment);
	newClient.Save();
	List<Client> allClients = Client.GetAll();
	return View("Index",allClients);
}

[HttpGet("clients/new")]
public ActionResult New()
{
	return View();
}

[HttpGet("/clients/{id}")]
public ActionResult Show(int id)
{
	Client newClient = Client.Find(id);
	ViewBag.Stylists = Stylist.GetAll();
	ViewBag.Client = Client.Find(id);
	ViewBag.Stylists1 = newClient.GetStylists();
	return View();
}

[HttpPost("/clients/{id}/addstylist")]
public ActionResult AddStylist(int id, int stylistId)
{
	Client newClient = Client.Find(id);
	newClient.AddStylist(Stylist.Find(stylistId));
	ViewBag.Stylists = Stylist.GetAll();
	ViewBag.Client = Client.Find(id);
	ViewBag.Stylists1 = newClient.GetStylists();
	return View("Show");
}
[HttpGet("/stylists/{stylistId}/client/new")]
public ActionResult New(int stylistId)
{
	Stylist stylist = Stylist.Find(stylistId);
	return View(stylist);
}

[HttpGet("/clients/{clientId}/edit")]
public ActionResult Edit( int clientId)
{


	Client client = Client.Find(clientId);

	return View(client);
}
[HttpPost("/clients/{clientId}/editclient")]
public ActionResult Edit( int clientId, string newName, DateTime newAppointment)
{


	Client client = Client.Find(clientId);
	client.Edit(newName,newAppointment);
	List<Client> allClients = Client.GetAll();

	return View("Index", allClients);
}
[HttpGet("/clients/{clientId}/delete")]
public ActionResult Delete(int clientId)
{


	Client client = Client.Find(clientId);
	client.Delete();
	List<Client> allClients = Client.GetAll();

	return View("Index", allClients);
}

}
}
