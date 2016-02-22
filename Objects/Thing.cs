using System.Collections.Generic;
using System.Data.SqlClient;
using System;

namespace InventoryList
{
  public class Thing
  {
    private int _id;
    private string _description;

    public Thing(string Description, int Id = 0)
    {
      _id = Id;
      _description = Description;
    }

    public int GetId()
    {
      return _id;
    }
    public string GetDescription()
    {
      return _description;
    }
    public void SetDescription(string newDescription)
    {
      _description = newDescription;
    }
    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("DELETE FROM things;", conn);
      cmd.ExecuteNonQuery();
    }
    public static List<Thing> GetAll()
    {
      List<Thing> allThings = new List<Thing>{};

      SqlConnection conn = DB.Connection();
      SqlDataReader rdr = null;
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM things;", conn);
      rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        int thingId = rdr.GetInt32(0);
        string thingDescription = rdr.GetString(1);
        Thing newThing = new Thing(thingDescription, thingId);
        allThings.Add(newThing);
      }

      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }

      return allThings;
    }

    public override bool Equals(System.Object otherThing)
    {
      if (!(otherThing is Thing))
      {
        return false;
      }
      else
      {
        Thing newThing = (Thing) otherThing;
        bool idEquality = (this.GetId() == newThing.GetId());
        bool descriptionEquality = (this.GetDescription() == newThing.GetDescription());
        return (idEquality && descriptionEquality);
      }
    }
    public override int GetHashCode()
    {
      return 0;
    }
    public void Save()
    {
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr;
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO things (description) OUTPUT INSERTED.id VALUES (@ThingDescription);", conn);

      SqlParameter descriptionParameter = new SqlParameter();
      descriptionParameter.ParameterName = "@ThingDescription";
      descriptionParameter.Value = this.GetDescription();
      cmd.Parameters.Add(descriptionParameter);
      rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        this._id = rdr.GetInt32(0);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
    }
    public static Thing Find(int id)
    {
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr = null;
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM things WHERE id = @ThingId;", conn);
      SqlParameter thingIdParameter = new SqlParameter();
      thingIdParameter.ParameterName = "@ThingId";
      thingIdParameter.Value = id.ToString();
      cmd.Parameters.Add(thingIdParameter);
      rdr = cmd.ExecuteReader();

      int foundThingId = 0;
      string foundThingDescription = null;
      while(rdr.Read())
      {
        foundThingId = rdr.GetInt32(0);
        foundThingDescription = rdr.GetString(1);
      }
      Thing foundThing = new Thing(foundThingDescription, foundThingId);

      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }

      return foundThing;
    }
  }
}
