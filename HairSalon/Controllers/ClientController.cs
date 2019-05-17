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

[HttpGet("/stylists/{stylistId}/client/new")]
public ActionResult New(int stylistId)
{
	Stylist stylist = Stylist.Find(stylistId);
	return View(stylist);
}

[HttpGet("/stylists/{stylistId}/clients/{clientId}/edit")]
public ActionResult Edit(int stylistId, int clientId)
{

	Dictionary<string, object> model = new Dictionary<string, object>();
	Stylist stylist = Stylist.Find(stylistId);
	model.Add("stylist", stylist);
	Client client = Client.Find(clientId);
	model.Add("client", client);
	return View(model);
}


}
}
