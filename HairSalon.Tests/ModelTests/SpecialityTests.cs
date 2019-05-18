using Microsoft.VisualStudio.TestTools.UnitTesting;
using HairSalon.Models;
using System.Collections.Generic;
using System;
using HairSalon;




namespace HairSalon.Tests
{
[TestClass]
public class SpecialityTest : IDisposable
{



public SpecialityTest()
{
	DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=mariia_stashuk_test;";
}
public void Dispose()
{

	Stylist.ClearAll();
	Client.ClearAll();
	Speciality.ClearAll();
}


[TestMethod]
public void GetName_ReturnsName_SpecialityName()
{
	Speciality newSpeciality = new Speciality("Hairmaster");
	string result = "Hairmaster";
	Assert.AreEqual(result, newSpeciality.GetName());
}

[TestMethod]
public void GetId_ReturnsId_Id()
{
	Speciality newSpeciality = new Speciality("Hairmaster",1);
	int result = 1;
	Assert.AreEqual(result, newSpeciality.GetId());
}

[TestMethod]
public void GetAllSpecialities_ReturnAllSpecialities()
{
	Speciality newSpeciality = new Speciality("Hairmaster");
	newSpeciality.Save();
	Speciality newSpeciality1 = new Speciality("Nailmaster");
	newSpeciality1.Save();
	List<Speciality> allSpecialities = new List<Speciality> {
		newSpeciality,newSpeciality1
	};
	List<Speciality> result = Speciality.GetAll();
	CollectionAssert.AreEqual (result, allSpecialities);
}
[TestMethod]

public void ClearAll_ReturnEmptyList()
{
	Speciality newSpeciality = new Speciality("Hairmaster");
	newSpeciality.Save();
	Speciality newSpeciality1 = new Speciality("Nailmaster");
	newSpeciality1.Save();
	Speciality.ClearAll();
	List<Speciality> allSpecialities = Speciality.GetAll();
	List<Speciality> result = new List<Speciality> {
	};
	CollectionAssert.AreEqual(allSpecialities,result);

}

[TestMethod]
public void GetStylists_ReturnSpecialityWithAllStylists()
{
	Speciality newSpeciality = new Speciality("Hairmaster");
	newSpeciality.Save();
	Stylist newStylist = new Stylist("Roma",3);
	newStylist.Save();
	Stylist newStylist1 = new Stylist("Andi",3);
	newStylist1.Save();
	newSpeciality.AddStylist(newStylist);
	newSpeciality.AddStylist(newStylist1);

	List<Stylist> allStylists = new List<Stylist> {
		newStylist,newStylist1
	};
	List<Stylist> result = newSpeciality.GetStylists();
	CollectionAssert.AreEqual(allStylists,result);


}




}
}
