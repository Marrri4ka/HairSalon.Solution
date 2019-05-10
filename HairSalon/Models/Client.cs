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

public int GetId()
{
	return _id;
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

public override bool Equals(System.Object otherClient)
{
	if (!(otherClient is Client))
	{
		return false;
	}
	else
	{
		Client newClient = (Client) otherClient;

		bool idEquality = this.GetId() == newClient.GetId();
		bool nameEquality = this.GetName() == newClient.GetName();
		bool stylistEquality = this.GetStylistId() == newClient.GetStylistId();
		return (idEquality && nameEquality && stylistEquality);
	}
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
public static Client Find(int id)
{
	MySqlConnection conn = DB.Connection();
	conn.Open();
	var cmd = conn.CreateCommand() as MySqlCommand;
	cmd.CommandText = @"SELECT * FROM clients WHERE id = (@searchId);";
	MySqlParameter idParameter = new MySqlParameter();
	idParameter.ParameterName = "@searchId";
	idParameter.Value = id;
	cmd.Parameters.Add(idParameter);
	var rdr = cmd.ExecuteReader() as MySqlDataReader;
	int clientId=0;
	string clientName ="";
	DateTime clientAppointment = new DateTime();
	int clientStylistIdyId = 0;

	while(rdr.Read())
	{
		clientId = rdr.GetInt32(0);
		clientName = rdr.GetString(1);
		clientAppointment = rdr.GetDateTime(2);
		clientStylistIdyId = rdr.GetInt32(3);
	}
	Client foundClient = new Client (clientName,clientAppointment,clientStylistIdyId, clientId);

	conn.Close();
	if(conn != null)
	{
		conn.Dispose();
	}
	return foundClient;
}

public void Delete()
{
	MySqlConnection conn = DB.Connection();
	conn.Open();
	var cmd = conn.CreateCommand() as MySqlCommand;
	cmd.CommandText = @"DELETE FROM clients WHERE id=@client_id;";
	MySqlParameter clientId = new MySqlParameter();
	clientId.ParameterName = "@client_id";
	clientId.Value = this._id;
	cmd.Parameters.Add(clientId);
	cmd.ExecuteNonQuery();
	conn.Close();
	if (conn != null)
	{
		conn.Dispose();
	}
}
}
}
