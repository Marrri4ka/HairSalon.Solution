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
	Client.GetAll();
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

// [TestMethod]
// public void GetName_ReturnName_String()
// {
//      string name = "Rita";
//      DateTime appointment = 2/12/12;
//      Client newClient = new Client(name,appointment,stylistId, clientId);
//      string result = newClient.GetName();
//      Assert.AreEqual(name,result);
// }
}
}
