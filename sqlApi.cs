using System;
using System.Data;
using System.Windows.Forms;
using Mono.Data.Sqlite;

//TODO: Decide on set db file vs dynamic db file.
public class sqlApi
{
    public static void InitDB(string filename)
    {
        string cs = "URI=file:" + filename;

        using(SqliteConnection con = new SqliteConnection(cs))
        {
            con.Open();
            using(SqliteCommand cmd = new SqliteCommand(con))
            {
                cmd.CommandText = "DROP TABLE IF EXISTS Cable";
                cmd.ExecuteNonQuery();
                cmd.CommandText = @"CREATE TABLE Cable(
                    Id INTEGER PRIMARY KEY,
                       Name TEXT)";
                cmd.ExecuteNonQuery();
            }
            con.Close();
        }
    }

    public static void AddCable(string item)
    {
        string cs = "URI=file:test.db";

        using(SqliteConnection con = new SqliteConnection(cs))
        {
            con.Open();
            using(SqliteCommand cmd = new SqliteCommand(con))
            {
                cmd.CommandText = "INSERT INTO Cable" 
                    + " VALUES(NULL, '" + item + "')";
                cmd.ExecuteNonQuery();
            }
            con.Close();
        }
    }

    public static DataSet LookupCable(string item)
    {
        string cs = "URI=file:test.db";
        string stm = "SELECT * FROM Cable";
        DataSet ds;

        using(SqliteConnection con = new SqliteConnection(cs))
        {
            con.Open();
            ds = new DataSet();
            using(SqliteDataAdapter da 
                    = new SqliteDataAdapter(stm, con))
            {
                da.Fill(ds, "Cable");
            }
            con.Close();
        }
        return ds;
    }
}
