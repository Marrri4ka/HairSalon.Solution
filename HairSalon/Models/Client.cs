using System.Collections.Generic;
using System;
using MySql.Data.MySqlClient;

namespace HairSalon.Models
{
public class Client
{
private string _name;
private DateTime _appointment;
private int _id;
private int _stylistId;


public Client (string name, DateTime appointment, int stylistId, int id=0)
{
	_name = name;
	_appointment = appointment;
	_stylistId = stylistId;
	_id = id;
}
public int GetStylistId()
{
	return _stylistId;
}

public string GetName()
{
	return _name;
}

public void SetName (string newName)
{
	_name = newName;
}

public DateTime GetAppointment()
{
	return _appointment;
}

public void SetAppointment(DateTime newAppointment)
{
	_appointment = newAppointment;
}

public static List<Client> GetAll()
{
	List<Client> allClients = new List<Client> {
	};
	MySqlConnection conn = DB.Connection();
	conn.Open();
	MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
	cmd.CommandText = @"SELECT * FROM clients;";
	MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;

	while (rdr.Read())
	{
		int clientId = rdr.GetInt32(0);
		string clientName = rdr.GetString(1);
		DateTime clientAppointment = rdr.GetDateTime(2);
		int stylistId = rdr.GetInt32(3);
		Client newClient = new Client(clientName,clientAppointment,stylistId, clientId);
		allClients.Add(newClient);
	}

	conn.Close();

	if (conn != null)
	{
		conn.Dispose();
	}

	return allClients;

}

public void Save()
{
	MySqlConnection conn = DB.Connection();
	conn.Open();
	var cmd = conn.CreateCommand() as MySqlCommand;

	cmd.CommandText = @"INSERT INTO clients (name, appointment, stylist_id) VALUES (@ClientName, @ClientAppointment, @ClientStylistId);";

	MySqlParameter nameParameter = new MySqlParameter();
	nameParameter.ParameterName = "@ClientName";
	nameParameter.Value = this._name;
	cmd.Parameters.Add(nameParameter);

	MySqlParameter appointmentParameter = new MySqlParameter();
	appointmentParameter.ParameterName = "@ClientAppointment";
	appointmentParameter.Value = this._appointment;
	cmd.Parameters.Add(appointmentParameter);

	MySqlParameter stylistParameter = new MySqlParameter();
	stylistParameter.ParameterName = "@ClientStylistId";
	stylistParameter.Value = this._stylistId;
	cmd.Parameters.Add(stylistParameter);

	cmd.ExecuteNonQuery();

	_id = (int) cmd.LastInsertedId;

	conn.Close();
	if (conn != null)
	{
		conn.Dispose();
	}
}
}
}
