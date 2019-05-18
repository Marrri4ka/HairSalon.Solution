using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace HairSalon.Models
{
public class Speciality
{

private string _name;
private int _id;


public Speciality (string name, int id=0)
{
	_name = name;
	_id =id;
}


public int GetId()
{
	return _id;
}

public string GetName()
{
	return _name;
}


public override bool Equals (System.Object obj)
{
	if (!(obj is Speciality))
	{
		return false;

	}
	else
	{
		Speciality newSpeciality = (Speciality) obj;
		bool idEquality = this.GetId().Equals(newSpeciality.GetId());
		bool nameEquality = this.GetName().Equals(newSpeciality.GetName());
		return (idEquality && nameEquality);
	}
}


public void Save()
{
	MySqlConnection conn = DB.Connection();
	conn.Open();
	MySqlCommand cmd = conn.CreateCommand();
	cmd.CommandText = "INSERT INTO specialities  (name) VALUE(@Name);";
	MySqlParameter nameParameter = new MySqlParameter("@Name", this._name);
	cmd.Parameters.Add(nameParameter);

	cmd.ExecuteNonQuery();
	conn.Close();
	_id = (int)cmd.LastInsertedId;
	if(conn != null) conn.Dispose();
}




public static Speciality Find(int id)
{
	MySqlConnection conn = DB.Connection();
	conn.Open();
	var cmd = conn.CreateCommand() as MySqlCommand;
	cmd.CommandText = @"SELECT * FROM specialities WHERE id = (@searchId);";

	MySqlParameter searchId = new MySqlParameter();
	searchId.ParameterName = "@searchId";
	searchId.Value = id;
	cmd.Parameters.Add(searchId);
	MySqlDataReader rdr = cmd.ExecuteReader();
	int SpecialityId = 0;
	string SpecialityName = "";

	while(rdr.Read())
	{
		SpecialityId = rdr.GetInt32(0);
		SpecialityName = rdr.GetString(1);
	}
	Speciality newSpeciality = new Speciality(SpecialityName, SpecialityId);
	conn.Close();
	if (conn != null) conn.Dispose();

	return newSpeciality;
}



public static List<Speciality> GetAll()
{
	List<Speciality> allSpecialities = new List<Speciality> {
	};
	MySqlConnection conn = DB.Connection();
	conn.Open();
	MySqlCommand cmd = conn.CreateCommand();
	cmd.CommandText = @"SELECT * FROM specialities;";

	MySqlDataReader rdr = cmd.ExecuteReader();
	while(rdr.Read())
	{
		int specialityId = rdr.GetInt32(0);
		string specialityName = rdr.GetString(1);
		Speciality newSpeciality = new Speciality(specialityName,specialityId);
		allSpecialities.Add(newSpeciality);
	}

	conn.Close();
	if(conn != null) conn.Dispose();
	return allSpecialities;
}


public List<Stylist> GetStylists()
{
	List<Stylist> allSpecialityStylists = new List<Stylist> {
	};
	MySqlConnection conn = DB.Connection();
	conn.Open();
	MySqlCommand cmd = conn.CreateCommand();
	cmd.CommandText = @"SELECT stylists. *
										FROM specialities
										JOIN specialities_stylists ON (specialities.id = specialities_stylists.speciality_id)
										JOIN stylists ON (stylists.id = specialities_stylists.stylist_id)
										WHERE specialities.id = @SpecialityId;";
	MySqlParameter specialityIdParameter = new MySqlParameter("@SpecialityId", this._id);
	cmd.Parameters.Add(specialityIdParameter);
	MySqlDataReader rdr = cmd.ExecuteReader();
	while(rdr.Read())
	{
		int stylistId = rdr.GetInt32(0);
		string stylistName = rdr.GetString(1);


		Stylist newStylist = new Stylist(stylistName, stylistId);
		allSpecialityStylists.Add(newStylist);
	}
	conn.Close();
	if (conn != null) conn.Dispose();

	return allSpecialityStylists;
}

public static void ClearAll()
{
	MySqlConnection conn = DB.Connection();
	conn.Open();
	MySqlCommand cmd = conn.CreateCommand();
	cmd.CommandText = @"DELETE FROM specialities;";
	cmd.ExecuteNonQuery();

	conn.Close();
	if(conn != null) conn.Dispose();
}

public void Delete()
{
	MySqlConnection conn = DB.Connection();
	conn.Open();
	MySqlCommand cmd = conn.CreateCommand();
	cmd.CommandText = @"DELETE FROM specialities WHERE id=@SpecialityId; DELETE FROM specialities_stylists WHERE speciality_id = @SpecialityId;";
	MySqlParameter specialityParameter = new MySqlParameter("@SpecialityId",this.GetId());
	cmd.Parameters.Add(specialityParameter);
	cmd.ExecuteNonQuery();
	conn.Close();
	if (conn != null) conn.Dispose();

}


public void AddStylist (Stylist stylist)
{
	MySqlConnection conn = DB.Connection();
	conn.Open();
	MySqlCommand cmd = conn.CreateCommand();
	cmd.CommandText = @"INSERT INTO specialities_stylists (stylist_id, speciality_id) VALUES (@StylistId, @SpecialityId);";
	MySqlParameter stylistId = new MySqlParameter("@StylistId", stylist.GetId());
	MySqlParameter specialitytId = new MySqlParameter("@SpecialityId",this._id);
	cmd.Parameters.Add(stylistId);
	cmd.Parameters.Add(specialitytId);

	cmd.ExecuteNonQuery();

	conn.Close();
	if (conn != null) conn.Dispose();
}



}

}
