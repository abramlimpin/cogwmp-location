using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for DB
/// </summary>
public class DB
{
    public DB()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public static int CountData()
    {
        using (SqlConnection con = new SqlConnection(Helper.GetCon()))
        {
            con.Open();
            string query = @"SELECT COUNT(id) FROM cog_location WHERE status=@status";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@status", "Active");
                return (int)cmd.ExecuteScalar();
            }
        }
    }

    public static int CountData(string region)
    {
        using (SqlConnection con = new SqlConnection(Helper.GetCon()))
        {
            con.Open();
            string query = @"SELECT COUNT(id) FROM cog_location 
                WHERE status=@status AND region=@region";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@status", "Active");
                cmd.Parameters.AddWithValue("@region", region);
                return (int)cmd.ExecuteScalar();
            }
        }
    }
}