using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string fullOrigionalpath = Request.Url.ToString();

        if (fullOrigionalpath.Contains("Default.aspx"))
        {
            Response.Redirect("~/");
        }
        
        if (!IsPostBack)
        {
            ltTotal.Text = DB.CountData().ToString();
            ltTotal_NL.Text = DB.CountData("North Luzon").ToString();
            ltTotal_MM.Text = DB.CountData("Metro Manila").ToString();
            ltTotal_CSL.Text = DB.CountData("Central South Luzon").ToString();
            ltTotal_V.Text = DB.CountData("Visayas").ToString();
            ltTotal_M.Text = DB.CountData("Mindanao").ToString();
            GetLocations();
        }
    }

    void GetLocations()
    {
        using (SqlConnection con = new SqlConnection(Helper.GetCon()))
        {
            con.Open();
            string query = @"SELECT id, church_branch, slug, 
                district, address, 
                head_pastor, mobile_no, email  
                FROM cog_location
                WHERE status=@status
                ORDER BY district";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@status", "Active");
                using (SqlDataReader data = cmd.ExecuteReader())
                {
                    locations.DataSource = data;
                    locations.DataBind();
                }
            }
        }
    }

    void GetLocations(string region)
    {
        using (SqlConnection con = new SqlConnection(Helper.GetCon()))
        {
            con.Open();
            string query = @"SELECT id, church_branch, slug, 
                district, address,
                head_pastor, mobile_no, email
                FROM cog_location
                WHERE status=@status AND region=@region
                ORDER BY district";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@status", "Active");
                cmd.Parameters.AddWithValue("@region", region);
                using (SqlDataReader data = cmd.ExecuteReader())
                {
                    locations.DataSource = data;
                    locations.DataBind();
                }
            }
        }
    }



    protected void region_All_Click(object sender, EventArgs e)
    {
        Session["region"] = "All";
        GetLocations();
        ltHeader.Text = "ALL LOCATIONS";
    }

    protected void region_NL_Click(object sender, EventArgs e)
    {
        Session["region"] = "NL";
        GetLocations("North Luzon");
        ltHeader.Text = "NORTH LUZON";
    }

    protected void region_MM_Click(object sender, EventArgs e)
    {
        Session["region"] = "MM";
        GetLocations("Metro Manila");
        ltHeader.Text = "METRO MANILA";
    }

    protected void region_CSL_Click(object sender, EventArgs e)
    {
        Session["region"] = "CSL";
        GetLocations("Central South Luzon");
        ltHeader.Text = "CENTRAL SOUTH LUZON";
    }

    protected void region_V_Click(object sender, EventArgs e)
    {
        Session["region"] = "V";
        GetLocations("Visayas");
        ltHeader.Text = "VISAYAS";
    }

    protected void region_M_Click(object sender, EventArgs e)
    {
        Session["region"] = "M";
        GetLocations("Mindanao");
        ltHeader.Text = "MINDANAO";
    }
}