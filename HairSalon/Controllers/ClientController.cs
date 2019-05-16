using Microsoft.AspNetCore.Mvc;
using HairSalon.Models;
using System.Collections.Generic;

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
[HttpGet("/clients/new")]
public ActionResult New()
{

	return View();
}

}
}
