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



public Client (string name, DateTime appointment, int id=0)
{
	_name = name;
	_appointment = appointment;
	_id = id;
}

public string GetName()
{
	return _name;
}


public int GetId()
{
	return _id;
}

public DateTime GetAppointment()
{
	return _appointment;
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
		Client newClient = new Client(clientName,clientAppointment,clientId);
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
		return (idEquality && nameEquality);
	}
}

public void Save()
{
	MySqlConnection conn = DB.Connection();
	conn.Open();
	var cmd = conn.CreateCommand() as MySqlCommand;

	cmd.CommandText = @"INSERT INTO clients (name, appointment) VALUES (@ClientName, @ClientAppointment);";

	MySqlParameter nameParameter = new MySqlParameter();
	nameParameter.ParameterName = "@ClientName";
	nameParameter.Value = this._name;
	cmd.Parameters.Add(nameParameter);

	MySqlParameter appointmentParameter = new MySqlParameter();
	appointmentParameter.ParameterName = "@ClientAppointment";
	appointmentParameter.Value = this._appointment;
	cmd.Parameters.Add(appointmentParameter);


	cmd.ExecuteNonQuery();

	_id = (int) cmd.LastInsertedId;

	conn.Close();
	if( conn != null) conn.Dispose();
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
	Client foundClient = new Client (clientName,clientAppointment,clientId);

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



public void Edit(string newName, DateTime newAppointment)
{
	MySqlConnection conn = DB.Connection();
	conn.Open();
	var cmd = conn.CreateCommand() as MySqlCommand;
	cmd.CommandText = @"UPDATE clients SET name = @newName, appointment = @newAppointment WHERE id = @searchId;";

	MySqlParameter searchId = new MySqlParameter();
	searchId.ParameterName = "@searchId";
	searchId.Value = _id;
	cmd.Parameters.Add(searchId);

	MySqlParameter name = new MySqlParameter();
	name.ParameterName = "@newName";
	name.Value = newName;
	cmd.Parameters.Add(name);

	MySqlParameter appointment = new MySqlParameter();
	appointment.ParameterName = "@newAppointment";
	appointment.Value = newAppointment;
	cmd.Parameters.Add(appointment);
	cmd.ExecuteNonQuery();

	_name = newName;
	_appointment = newAppointment;

	conn.Close();
	if (conn != null)
	{
		conn.Dispose();
	}
}
public static List<Client> Sort(int stylistId)
{
	List<Client> allClients = new List<Client> {
	};

	MySqlConnection conn = DB.Connection();
	conn.Open();
	MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
	cmd.CommandText = @"SELECT * FROM clients WHERE stylist_id = @stylistId ORDER BY appointment DESC;";
	MySqlParameter stylistIdParameter = new MySqlParameter();
	stylistIdParameter.ParameterName = "@stylistId";
	stylistIdParameter.Value = stylistId;
	cmd.Parameters.Add(stylistIdParameter);
	MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;

	while (rdr.Read())
	{
		int clientId = rdr.GetInt32(0);
		string clientName = rdr.GetString(1);
		DateTime appoinment = rdr.GetDateTime(2);
		Client newClient = new Client (clientName,appoinment,clientId);
		allClients.Add(newClient);
	}

	conn.Close();

	if (conn != null)
	{
		conn.Dispose();
	}

	return allClients;

}

public List<Stylist> GetStylists()
{
	List<Stylist> allStylists = new List<Stylist> {
	};
	MySqlConnection conn = DB.Connection();
	conn.Open();
	MySqlCommand cmd = conn.CreateCommand();
	cmd.CommandText = @"SELECT stylists. *
											FROM clients
											JOIN stylists_clients ON (clients.id = stylists_clients.client_id)
											JOIN stylists ON (stylists.id = stylists_clients.stylist_id)
											WHERE clients.id = @ClientId;";
	MySqlParameter clientParameter = new MySqlParameter ("@ClientId", this._id);
	cmd.Parameters.Add(clientParameter);
	MySqlDataReader rdr = cmd.ExecuteReader();
	while(rdr.Read())
	{
		int id = rdr.GetInt32(0);
		string name =rdr.GetString(1);
		allStylists.Add(new Stylist(name,id));
	}

	conn.Close();
	if (conn != null) conn.Dispose();
	return allStylists;
}




}
}
