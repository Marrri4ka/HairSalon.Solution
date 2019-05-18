using Microsoft.VisualStudio.TestTools.UnitTesting;
using HairSalon.Models;
using System.Collections.Generic;
using System;
using HairSalon;




namespace ToDoList.Tests
{
[TestClass]
public class StylistTest : IDisposable
{



public StylistTest()
{
	DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=mariia_stashuk_test;";
}
public void Dispose()
{

	Stylist.ClearAll();
	Client.ClearAll();
}


[TestMethod]
public void GetName_ReturnsName_StylistName()
{
	Stylist newStylist = new Stylist("Rita");
	string result = "Rita";
	Assert.AreEqual(result, newStylist.GetName());
}

[TestMethod]
public void GetId_ReturnsId_Id()
{
	Stylist newStylist = new Stylist("Rita", 1);
	int result = 1;
	Assert.AreEqual(result, newStylist.GetId());
}

[TestMethod]
public void GetAllStylists_ReturnAllStylists()
{
	Stylist newStylist = new Stylist("Roma",3);
	newStylist.Save();
	Stylist newStylist1 = new Stylist("Andi",3);
	newStylist1.Save();
	List<Stylist> newList = new List<Stylist> {
		newStylist,newStylist1
	};
	List<Stylist> result = Stylist.GetAll();
	CollectionAssert.AreEqual (result, newList);
}
[TestMethod]

public void ClearAll_ReturnEmptyList()
{
	Stylist newStylist = new Stylist("Roma",3);
	newStylist.Save();
	Stylist newStylist1 = new Stylist("Andi",3);
	newStylist1.Save();
	Stylist.ClearAll();
	List<Stylist> allStylists = Stylist.GetAll();
	List<Stylist> result = new List<Stylist> {
	};
	CollectionAssert.AreEqual(allStylists,result);

}
[TestMethod]
public void Edit_ReturnEditedStylistInfo()
{
	Stylist newStylist = new Stylist("Sorianna");
	newStylist.Save();
	newStylist.Edit("Marianna");
	Stylist result = new Stylist ("Marianna", newStylist.GetId());
	Assert.AreEqual (newStylist,result);

}
[TestMethod]
public void GetClients_ReturnStylistWithAllClients()
{
	Stylist newStylist = new Stylist("Sorianna");
	newStylist.Save();
	Client newClient = new Client("Lisa", new DateTime(2002,2,4));
	Client newClient1 = new Client("Lisa", new DateTime(2002,2,19));
	newClient.Save();
	newClient1.Save();
	newStylist.AddClient(newClient);
	newStylist.AddClient(newClient1);

	List<Client> allStylistsClients = new List<Client> {
		newClient,newClient1
	};
	List<Client> result = newStylist.GetClients();
	CollectionAssert.AreEqual(allStylistsClients,result);


}

[TestMethod]
public void GetSpecialities_ReturnStylistWithAllClients()
{
	Stylist newStylist = new Stylist("Sorianna");
	newStylist.Save();
	Speciality newSpeciality = new Speciality("Hairmaster");
	Speciality newSpeciality1 = new Speciality("Nailmaster");
	newSpeciality.Save();
	newSpeciality1.Save();
	newStylist.AddSpeciality(newSpeciality);
	newStylist.AddSpeciality(newSpeciality1);

	List<Speciality> allStylistsSpecialities = new List<Speciality> {
		newSpeciality,newSpeciality1
	};
	List<Speciality> result = newStylist.GetSpecialities();
	CollectionAssert.AreEqual(allStylistsSpecialities,result);


}



}
}
