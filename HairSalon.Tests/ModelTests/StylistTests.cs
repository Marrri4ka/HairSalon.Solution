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
	DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=first_last_test;";
}
public void Dispose()
{

	Stylist.GetAll();
}

[TestMethod]
public void StylistConstructor_CreatesInstanceOfStylist_Stylist()
{
	string name = "Rita";
	Stylist newStylist = new Stylist(name);
	Assert.AreEqual(typeof(Stylist), newStylist.GetType());
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



}
}
