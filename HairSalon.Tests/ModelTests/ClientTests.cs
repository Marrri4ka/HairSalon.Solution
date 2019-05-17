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
	Stylist.ClearAll();
}

public ClientTest()
{
	DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=mariia_stashuk_test;";
}


[TestMethod]
public void GetName_ReturnName()
{
	Client newClient = new Client("Lisa", new DateTime(), 1);
	string result = "Lisa";
	Assert.AreEqual(result, newClient.GetName());
}

[TestMethod]
public void FindId_ReturnId()
{


	Client newClient = new Client("Lisa", new DateTime(),1);
	newClient.Save();
	int searchId = newClient.GetId();
	Client foundClient = Client.Find(searchId);
	Assert.AreEqual(newClient,foundClient);
}

[TestMethod]
public void GetAll_ReturnAllClients()
{
	Client newClient = new Client("Lisa", new DateTime());
	Client newClient1 = new Client("Mona", new DateTime());

	newClient.Save();
	newClient1.Save();

	List<Client> allClients = new List<Client> {
		newClient,newClient1
	};
	List<Client> result = Client.GetAll();
	CollectionAssert.AreEqual(result,allClients);

}

[TestMethod]
public void Equals_EvaluateEqualsBasedOnIdAndName()
{
	Client newClient = new Client("Lisa", new DateTime(),1);
	Client newClient1 = new Client("Lisa", new DateTime(),1);

	Assert.AreEqual (newClient,newClient1);
}

[TestMethod]
public void Equals_EvaluateEqualsBasedOnName()
{
	Client newClient = new Client("Lisa", new DateTime(),-1);
	Client newClient1 = new Client("Lisa", new DateTime(),1);

	Assert.AreNotEqual (newClient,newClient1);
}

[TestMethod]
public void Equals_EvaluateEqualsBasedOnIs()
{
	Client newClient = new Client("Lisa", new DateTime(),1);
	Client newClient1 = new Client("Mono", new DateTime(),1);

	Assert.AreNotEqual (newClient,newClient1);
}

[TestMethod]
public void Save_SaveToDataBase()
{
	Client newClient = new Client("Lisa", new DateTime());
	newClient.Save();
	List<Client> allClients = new List <Client> {
		newClient
	};
	List<Client> result = Client.GetAll();
	CollectionAssert.AreEqual (allClients,result);

}

[TestMethod]
public void Find_ReturnFoundClient()
{
	Client newClient = new Client("Lisa", new DateTime());
	newClient.Save();
	Client newClient1 = Client.Find(newClient.GetId());
	Assert.AreEqual (newClient,newClient1);
}

[TestMethod]
public void DeleteClient_ReturnEmptyList()
{

	Client newClient = new Client("Lisa", new DateTime());
	newClient.Save();
	newClient.Delete();
	List<Client> allClients = new List<Client> {
	};
	List<Client> result = Client.GetAll();
	CollectionAssert.AreEqual(allClients,result);

}

[TestMethod]
public void ClearAll_ReturnEmptyList()
{
	Client newClient = new Client("Lisa", new DateTime());
	newClient.Save();
	Client newClient1 = new Client("Mona", new DateTime());
	newClient1.Save();

	Client.ClearAll();
	List<Client> result = Client.GetAll();
	List<Client> emptyList = new List<Client> {
	};
	CollectionAssert.AreEqual(emptyList,result);

}

[TestMethod]
public void Edit_ReturnEditedClientInfo()
{
	Client newClient = new Client("Lisa", new DateTime());

	newClient.Save();
	newClient.Edit("Mona",new DateTime(2002,1,2));
	Client result = new Client("Mona", new DateTime(2002,1,2),newClient.GetId());
	Assert.AreEqual (newClient,result);
}

[TestMethod]
public void Sort_ReturnClientsSortedByApp()
{
	Client newClient = new Client("Lisa", new DateTime(2002,2,4));
	Client newClient1 = new Client("Lisa", new DateTime(2002,2,19));
	Client newClient2 = new Client("Lisa", new DateTime(2002,2,7));
	newClient.Save();
	newClient1.Save();
	newClient2.Save();

	List<Client> allClients = Client.Sort();
	List<Client> result = new List<Client> {
		newClient,newClient2,newClient1
	};
	CollectionAssert.AreEqual (allClients,result);
	;


}

[TestMethod]
public void GetStylists_ReturnClientWithAllStylists()
{
	Client newClient = new Client("Lisa", new DateTime(2002,2,4));
	Stylist stylist1 = new Stylist("Olja");
	Stylist stylist2 = new Stylist("Petja");
	newClient.Save();
	stylist1.Save();
	stylist2.Save();

	newClient.AddStylist(stylist1);
	newClient.AddStylist(stylist2);

	List<Stylist> result = newClient.GetStylists();
	List<Stylist> stylists = new List<Stylist> {
		stylist1,stylist2
	};

	CollectionAssert.AreEqual(stylists,result);
}

}
}
