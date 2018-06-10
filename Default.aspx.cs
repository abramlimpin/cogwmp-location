using System;
using System.Collections.Generic;
using System.Data;
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
            //ltTotal.Text = DB.CountData().ToString();
            //ltTotal_NL.Text = DB.CountData("North Luzon").ToString();
            //ltTotal_MM.Text = DB.CountData("Metro Manila").ToString();
            //ltTotal_CSL.Text = DB.CountData("Central South Luzon").ToString();
            //ltTotal_V.Text = DB.CountData("Visayas").ToString();
            //ltTotal_M.Text = DB.CountData("Mindanao").ToString();
            GetLocations();
        }
    }

    void GetLocations()
    {
        using (SqlConnection con = new SqlConnection(Helper.GetCon()))
        {
            con.Open();
            string query = @"SELECT id, church_branch, slug, 
                district, address, latitude, longitude,
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

    void GetLocations_Keyword(string keyword)
    {
        using (SqlConnection con = new SqlConnection(Helper.GetCon()))
        {
            con.Open();
            string query = @"SELECT id, church_branch, slug, 
                district, address, latitude, longitude,
                head_pastor, mobile_no, email  
                FROM cog_location
                WHERE status=@status AND
                (church_branch LIKE @keyword OR
                address LIKE @keyword)
                ORDER BY district";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@status", "Active");
                cmd.Parameters.AddWithValue("@keyword", "%" + keyword + "%");
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
                district, address, latitude, longitude,
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

        txtKeyword.Text = "";
    }

    void GetLocations(string region, string district)
    {
        using (SqlConnection con = new SqlConnection(Helper.GetCon()))
        {
            con.Open();
            string query = @"SELECT id, church_branch, slug, 
                district, address, latitude, longitude,
                head_pastor, mobile_no, email  
                FROM cog_location
                WHERE status=@status AND district=@district
                
                ORDER BY district";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@district", district);
                cmd.Parameters.AddWithValue("@status", "Active");
                using (SqlDataReader data = cmd.ExecuteReader())
                {
                    locations.DataSource = data;
                    locations.DataBind();
                }
            }
        }
    }

    DataTable GetDistrict(string region)
    {
        using (SqlConnection con = new SqlConnection(Helper.GetCon()))
        {
            con.Open();
            string query = @"SELECT DISTINCT district
                FROM cog_location WHERE region=@region";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@region", region);
                using (SqlDataReader data = cmd.ExecuteReader())
                {
                    using (DataTable dt = new DataTable())
                    {
                        dt.Load(data);
                        return dt;
                    }
                }
            }
        }
    }
    
    static string currentRegion = "";

    protected void region_All_Click(object sender, EventArgs e)
    {
        Session["region"] = "All";
        GetLocations();
        imgRegion.Visible = false;

        lvNL.Visible = false;
        lvMM.Visible = false;
        lvCSL.Visible = false;
        lvV.Visible = false;
        lvM.Visible = false;
        currentRegion = "";
    }

    protected void region_NL_Click(object sender, EventArgs e)
    {
        Session["region"] = "NL";
        GetLocations("North Luzon");
        imgRegion.ImageUrl = "~/images/banner-nl.png";
        imgRegion.Visible = true;

        lvNL.Visible = true;
        lvMM.Visible = false;
        lvCSL.Visible = false;
        lvV.Visible = false;
        lvM.Visible = false;

        lvNL.DataSource = GetDistrict("North Luzon");
        lvNL.DataBind();

        if (currentRegion == "NL")
        {
            lvNL.Visible = false;
            currentRegion = "";
        }
        else
            currentRegion = "NL";
    }

    protected void region_MM_Click(object sender, EventArgs e)
    {
        Session["region"] = "MM";
        GetLocations("Metro Manila");
        imgRegion.ImageUrl = "~/images/banner-mm.png";
        imgRegion.Visible = true;

        lvNL.Visible = false;
        lvMM.Visible = true;
        lvCSL.Visible = false;
        lvV.Visible = false;
        lvM.Visible = false;

        lvMM.DataSource = GetDistrict("Metro Manila");
        lvMM.DataBind();

        if (currentRegion == "MM")
        { 
            lvMM.Visible = false;
            currentRegion = "";
        }
        else
            currentRegion = "MM";
    }

    protected void region_CSL_Click(object sender, EventArgs e)
    {
        Session["region"] = "CSL";
        GetLocations("Central South Luzon");
        imgRegion.ImageUrl = "~/images/banner-csl.png";
        imgRegion.Visible = true;

        lvNL.Visible = false;
        lvMM.Visible = false;
        lvCSL.Visible = true;
        lvV.Visible = false;
        lvM.Visible = false;

        lvCSL.DataSource = GetDistrict("Central South Luzon");
        lvCSL.DataBind();

        if (currentRegion == "CSL")
        { 
            lvCSL.Visible = false;
            currentRegion = "";
        }
        else
            currentRegion = "CSL";
    }

    protected void region_V_Click(object sender, EventArgs e)
    {
        Session["region"] = "V";
        GetLocations("Visayas");
        imgRegion.ImageUrl = "~/images/banner-v.png";
        imgRegion.Visible = true;

        lvNL.Visible = false;
        lvMM.Visible = false;
        lvCSL.Visible = false;
        lvV.Visible = true;
        lvM.Visible = false;

        lvV.DataSource = GetDistrict("Visayas");
        lvV.DataBind();

        if (currentRegion == "V")
        { 
            lvV.Visible = false;
            currentRegion = "";
        }
        else
            currentRegion = "V";
    }

    protected void region_M_Click(object sender, EventArgs e)
    {
        Session["region"] = "M";
        GetLocations("Mindanao");
        imgRegion.ImageUrl = "~/images/banner-m.png";
        imgRegion.Visible = true;

        lvNL.Visible = false;
        lvMM.Visible = false;
        lvCSL.Visible = false;
        lvV.Visible = false;
        lvM.Visible = true;

        lvM.DataSource = GetDistrict("Mindanao");
        lvM.DataBind();

        if (currentRegion == "M")
        { 
            lvM.Visible = false;
            currentRegion = "";
        }
        else
            currentRegion = "M";
    }

    protected void GetLocation(object sender, ListViewCommandEventArgs e)
    {
        if (e.CommandName == "getlocation")
        {
            Literal ltDistrict = (Literal)e.Item.FindControl("ltDistrict");
            GetLocations("", ltDistrict.Text);
        }
    }

    protected void txtKeyword_TextChanged(object sender, EventArgs e)
    {
        if (txtKeyword.Text == "")
        {
            Session["region"] = "All";
            GetLocations();
            imgRegion.Visible = false;
        }
        else {
            Session["region"] = "All";
            GetLocations_Keyword(txtKeyword.Text);
            imgRegion.Visible = false;
        }
    }
}