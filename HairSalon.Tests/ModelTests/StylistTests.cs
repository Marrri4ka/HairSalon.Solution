using Microsoft.VisualStudio.TestTools.UnitTesting;
using HairSalon.Models;
using System.Collections.Generic;
using System;

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

	Stylist.ClearAll();
}

}
}
