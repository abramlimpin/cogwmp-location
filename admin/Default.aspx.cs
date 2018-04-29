using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string fullOrigionalpath = Request.Url.ToString();

        if (Session["update"] != null)
        {
            update.Visible = true;
            Session.Remove("update");
        }

        if (fullOrigionalpath.Contains("Default.aspx"))
        {
            Response.Redirect("~/admin");
        }

        if (!IsPostBack)
        {
            GetLocations();
        }
    }

    void GetLocations()
    {
        using (SqlConnection con = new SqlConnection(Helper.GetCon()))
        {
            con.Open();
            string query = @"SELECT id, church_branch, slug, 
                region, district, address,
                head_pastor, mobile_no, email,
                status, date_added, date_modified
                FROM cog_location
                WHERE status!=@status
                ORDER BY church_branch, district";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@status", "Archived");
                using (SqlDataReader data = cmd.ExecuteReader())
                {
                    locations.DataSource = data;
                    locations.DataBind();
                }
            }
        }
    }

    protected void locations_ItemCommand(object sender, ListViewCommandEventArgs e)
    {
        Literal id = (Literal)e.Item.FindControl("id");

        if (e.CommandName == "selectitem")
        {
            btnAdd.Visible = false;
            btnUpdate.Visible = true;
            status.Visible = true;
            
            using (SqlConnection con = new SqlConnection(Helper.GetCon()))
            {
                con.Open();
                string query = @"SELECT status, region, district, church_branch,
                    slug, head_pastor, mobile_no, email, address
                    FROM cog_location
                    WHERE id=@id AND status!=@status";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@id", id.Text);
                    cmd.Parameters.AddWithValue("@status", "Archived");
                    using (SqlDataReader data = cmd.ExecuteReader())
                    {
                        while (data.Read())
                        {
                            ltID.Text = id.Text;
                            ddlStatus.SelectedValue = data["status"].ToString();
                            ddlRegions.SelectedValue = data["region"].ToString();
                            ddlRegions_SelectedIndexChanged(null, null);
                            ddlDistrict.SelectedValue = data["district"].ToString();
                            txtBranch.Text = data["church_branch"].ToString().Replace("COG ", "");
                            txtSlug.Text = data["slug"].ToString();
                            txtPastor.Text = data["head_pastor"].ToString();
                            txtMobile.Text = data["mobile_no"].ToString();
                            txtEmail.Text = data["email"].ToString();
                            txtAddress.Text = data["address"].ToString();
                        }
                    }
                }
            }
        }
        else if (e.CommandName == "removeitem")
        {
            using (SqlConnection con = new SqlConnection(Helper.GetCon()))
            {
                con.Open();
                string query = @"UPDATE cog_location SET status=@status
                    WHERE id=@id";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@status", "Archived");
                    cmd.Parameters.AddWithValue("@id", id.Text);
                    cmd.ExecuteNonQuery();
                    Session["update"] = "yes";
                    Response.Redirect("~/admin/");
                }
            }
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/admin/");
    }

    protected void ddlRegions_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlRegions.SelectedIndex == 0)
        {
            ddlDistrict.Items.Clear();
            ddlDistrict.Items.Insert(0, new ListItem("Select a region..."));
        }
        else if (ddlRegions.SelectedIndex == 1)
        {
            ddlDistrict.Items.Clear();
            ddlDistrict.Items.Insert(0, new ListItem("Select a region..."));
            ddlDistrict.Items.Add("Central Pangasinan");
            ddlDistrict.Items.Add("Eastern Pangasinan");
            ddlDistrict.Items.Add("Western Pangasinan");
            ddlDistrict.Items.Add("Urdaneta Pangasinan");
            ddlDistrict.Items.Add("Tarlac");
            ddlDistrict.Items.Add("La Union / Benguet");
            ddlDistrict.Items.Add("Baguio City");
            ddlDistrict.Items.Add("Ilocos Sur");
            ddlDistrict.Items.Add("Cagayan Apayao");
            ddlDistrict.Items.Add("Ilocos Norte");
            ddlDistrict.Items.Add("South Ilocos Norte");
            ddlDistrict.Items.Add("Lasam");
            ddlDistrict.Items.Add("Nueva Viscaya");
            ddlDistrict.Items.Add("Aglipay Saguda");
            ddlDistrict.Items.Add("Madella Nagtipunan");
            ddlDistrict.Items.Add("North Isabela");
            ddlDistrict.Items.Add("North Eastern Isabela");
            ddlDistrict.Items.Add("Eastern Isabela");
            ddlDistrict.Items.Add("Central Isabela");
            ddlDistrict.Items.Add("North San Agustin");
            ddlDistrict.Items.Add("South San Agustin");
            ddlDistrict.Items.Add("West Jones");
            ddlDistrict.Items.Add("East Jones");
            ddlDistrict.Items.Add("Southern Isabela");
        }
        else if (ddlRegions.SelectedIndex == 2)
        {
            ddlDistrict.Items.Clear();
            ddlDistrict.Items.Insert(0, new ListItem("Select a region..."));
            ddlDistrict.Items.Add("Manila");
            ddlDistrict.Items.Add("Cavite");
            ddlDistrict.Items.Add("Laguna");
        }
        else if (ddlRegions.SelectedIndex == 3)
        {
            ddlDistrict.Items.Clear();
            ddlDistrict.Items.Insert(0, new ListItem("Select a region..."));
            ddlDistrict.Items.Add("Central Luzon");
            ddlDistrict.Items.Add("Tagalog");
            ddlDistrict.Items.Add("North Bicol");
            ddlDistrict.Items.Add("South Bicol");
            ddlDistrict.Items.Add("Oriental Mindoro");
            ddlDistrict.Items.Add("Occidental Mindoro");
        }
        else if (ddlRegions.SelectedIndex == 4)
        {
            ddlDistrict.Items.Clear();
            ddlDistrict.Items.Insert(0, new ListItem("Select a region..."));
            ddlDistrict.Items.Add("Central");
            ddlDistrict.Items.Add("Iloilo");
            ddlDistrict.Items.Add("Negros Occidental");
            ddlDistrict.Items.Add("Negros Oriental");
            ddlDistrict.Items.Add("Samar-Leyte");
            ddlDistrict.Items.Add("Capiz & Antique");
        }
        else
        {
            ddlDistrict.Items.Clear();
            ddlDistrict.Items.Insert(0, new ListItem("Select a region..."));
            ddlDistrict.Items.Add("Davao City");
            ddlDistrict.Items.Add("Asean");
            ddlDistrict.Items.Add("Bukidnon North");
            ddlDistrict.Items.Add("Bukidnon South");
            ddlDistrict.Items.Add("Cagayan Misamis");
            ddlDistrict.Items.Add("Cotabato 1");
            ddlDistrict.Items.Add("Cotabato 2");
            ddlDistrict.Items.Add("Jasdonmar");
            ddlDistrict.Items.Add("Koronadal Valley");
            ddlDistrict.Items.Add("Compostella Valley");
            ddlDistrict.Items.Add("Kaleb");
            ddlDistrict.Items.Add("Kulaman");
            ddlDistrict.Items.Add("Lambayong");
            ddlDistrict.Items.Add("Maribato");
            ddlDistrict.Items.Add("Macedonian");
            ddlDistrict.Items.Add("Makarios");
            ddlDistrict.Items.Add("Mt. District");
            ddlDistrict.Items.Add("Ozamiz");
            ddlDistrict.Items.Add("Socsargen");
            ddlDistrict.Items.Add("South Agusan");
            ddlDistrict.Items.Add("Sto. Tomas");
            ddlDistrict.Items.Add("Quitaisu");
            ddlDistrict.Items.Add("Tantangan");
            ddlDistrict.Items.Add("Upper Valley");
        }
    }

    protected void txtBranch_TextChanged(object sender, EventArgs e)
    {
        if (txtBranch.Text != "" && btnAdd.Visible)
        {
            string slug_new = "cog-" + Helper.GenerateSlug(txtBranch.Text);
            string slug_existing = IsExisting(slug_new);

            if (slug_existing != "")
            {
                string lastChar = slug_existing.Substring(slug_existing.Length - 1, 1);
                string ext = "";
                int count = 0;
                bool validNumber = int.TryParse(lastChar, out count);
                if (validNumber)
                {
                    ext = count++.ToString();
                    slug_new = slug_existing.Substring(0, slug_existing.Length - 1) + ext;
                }
                else
                {
                    slug_new += "-2";
                }
            }
            txtSlug.Text = slug_new;
            ltSlug.Text = slug_new;
        }
    }

    string IsExisting(string slug)
    {
        using (SqlConnection con = new SqlConnection(Helper.GetCon()))
        {
            con.Open();
            string query = @"SELECT TOP 1 slug FROM cog_location 
                WHERE slug LIKE @slug
                ORDER BY id DESC";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@slug", "%" + slug + "%");
                return cmd.ExecuteScalar() == null ? "" : (string)cmd.ExecuteScalar();
            }
        }
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        using (SqlConnection con = new SqlConnection(Helper.GetCon()))
        {
            con.Open();
            string query = @"UPDATE cog_location 
                SET status=@status, region=@region,
                district=@district, church_branch=@church_branch,
                head_pastor=@head_pastor, mobile_no=@mobile_no,
                email=@email, address=@address, 
                date_modified=@date_modified
                WHERE id=@id";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@status", ddlStatus.SelectedValue);
                cmd.Parameters.AddWithValue("@region", ddlRegions.SelectedValue);
                cmd.Parameters.AddWithValue("@district", ddlDistrict.SelectedValue);
                cmd.Parameters.AddWithValue("@church_branch", "COG " + txtBranch.Text.ToUpper());
                cmd.Parameters.AddWithValue("@head_pastor", txtPastor.Text);
                cmd.Parameters.AddWithValue("@mobile_no", txtMobile.Text);
                cmd.Parameters.AddWithValue("@email", txtEmail.Text);
                cmd.Parameters.AddWithValue("@address", txtAddress.Text);
                cmd.Parameters.AddWithValue("@date_modified", DateTime.Now);
                cmd.Parameters.AddWithValue("@id", ltID.Text);
                cmd.ExecuteNonQuery();
                Session["update"] = "yes";
                Response.Redirect("~/admin/");
            }
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        using (SqlConnection con = new SqlConnection(Helper.GetCon()))
        {
            con.Open();
            string query = @"INSERT INTO cog_location VALUES
                (@church_branch, @slug, @country, @region,
                @district, @head_pastor, @service_schedule,
                @about, @mobile_no, @email, @address,
                @latitude, @longitude, @status,
                @date_added, @date_modified)";
            using (SqlCommand cmd = new SqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@church_branch", "COG " + txtBranch.Text.ToUpper());
                cmd.Parameters.AddWithValue("@slug", ltSlug.Text);
                cmd.Parameters.AddWithValue("@country", "Philippines");
                cmd.Parameters.AddWithValue("@region", ddlRegions.SelectedValue);
                cmd.Parameters.AddWithValue("@district", ddlDistrict.SelectedValue);
                cmd.Parameters.AddWithValue("@head_pastor", txtPastor.Text);
                cmd.Parameters.AddWithValue("@service_schedule", "");
                cmd.Parameters.AddWithValue("@about", "");
                cmd.Parameters.AddWithValue("@mobile_no", txtMobile.Text);
                cmd.Parameters.AddWithValue("@email", txtEmail.Text);
                cmd.Parameters.AddWithValue("@address", txtAddress.Text);
                cmd.Parameters.AddWithValue("@latitude", "");
                cmd.Parameters.AddWithValue("@longitude", "");
                cmd.Parameters.AddWithValue("@status", "Active");
                cmd.Parameters.AddWithValue("@date_added", DateTime.Now);
                cmd.Parameters.AddWithValue("@date_modified", DBNull.Value);
                cmd.ExecuteNonQuery();
                Session["update"] = "yes";
                Response.Redirect("~/admin/");
            }
        }
    }
}