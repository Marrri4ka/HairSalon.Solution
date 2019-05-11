using System.Collections.Generic;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HairSalon.Models;
using MySql.Data.MySqlClient;

namespace HairSalon.Tests
{
[TestClass]
public class ClientTest : IDisposable
{
public void Dispose()
{
	Client.ClearAll();
}

public ClientTest()
{
	DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=first_last_test;";
}

[TestMethod]
public void ClientConstructor_CreatesInstanceOfClient_Client()
{
	Client newClient = new Client("test", new DateTime(), 1);
	Assert.AreEqual(typeof(Client), newClient.GetType());
}
[TestMethod]
public void GetName_ReturnName()
{
	Client newClient = new Client("Lisa", new DateTime(), 1);
	string result = "Lisa";
	Assert.AreEqual(result,  newClient.GetName());
}

[TestMethod]
public void FindId_ReturnId()
{
	Stylist stylist = new Stylist("Roma");
	stylist.Save();

	Client newClient = new Client("Lisa", new DateTime(), stylist.GetId());
	newClient.Save();
	int searchId = newClient.GetId();
	Client foundClient = Client.Find(searchId);
	Assert.AreEqual(newClient,foundClient);
}


}
}
