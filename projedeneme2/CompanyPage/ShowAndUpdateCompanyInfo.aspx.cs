﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Web.Security;
using System.Web.UI.HtmlControls;
using System.Configuration;
using System.Drawing;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;



namespace projedeneme2.CompanyPage
{
    public partial class ShowAndUpdateCompanyInfo : System.Web.UI.Page
    {
        //The ShowAndUpdateCompanyInfo class derives from 5m-macro, which manages the database connection and changes in the data.
        SqlConnection con = databaseConnect.connectToSQL();     
        protected void Page_Load(object sender, EventArgs e)
        {
            //To restore the page so that data is not lost and overlaid 
            if (!IsPostBack)
            {
                ShowData();
            }
        }

        //ShowData method for Displaying Data in Gridview  
        protected void ShowData()
        {
            DataTable dt = new DataTable();
            con.Open();
            SqlDataAdapter adapt = new SqlDataAdapter("Select Name,SiteURL,PhoneContryCode,PhoneNumber,IBAN,Company_Description,Tax_Admin,Tax_number from CompanyInfo", con);
            adapt.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                GridView1.DataSource = dt;
                GridView1.DataBind();
            }
            con.Close();
        }
        
        protected void GridView1_RowEditing(object sender, System.Web.UI.WebControls.GridViewEditEventArgs e)
        {
            //NewEditIndex property used to determine the index of the row being edited.  
            GridView1.EditIndex = e.NewEditIndex;
            ShowData();
        }
        //The RowUpdating event is raised when a row's Update button is clicked, 
        //but before the GridView control updates the row. 
        //This enables you to provide an event-handling 
        //method that performs a custom routine, such as canceling the update operation, whenever this event occurs.
        protected void GridView1_RowUpdating(object sender, System.Web.UI.WebControls.GridViewUpdateEventArgs e)
        {

            //Simdilik burda kalsin, ileride lazim olabilir..@mertbesirli
            //    Label id = GridView1.Rows[e.RowIndex].FindControl("lbl_ID") as Label;
            //Finding the controls from Gridview for the row which is going to update  
            TextBox name = GridView1.Rows[e.RowIndex].FindControl("nameTextBox") as TextBox;
            TextBox url = GridView1.Rows[e.RowIndex].FindControl("urlTextBox") as TextBox;
            TextBox phonecode = GridView1.Rows[e.RowIndex].FindControl("phoneCodeTextBox") as TextBox;
            TextBox phonenumber = GridView1.Rows[e.RowIndex].FindControl("phoneNumberTextBox") as TextBox;
            TextBox iban = GridView1.Rows[e.RowIndex].FindControl("ibanTextBox") as TextBox;
            TextBox desc = GridView1.Rows[e.RowIndex].FindControl("descriptionTextBox") as TextBox;
            TextBox taxadmin = GridView1.Rows[e.RowIndex].FindControl("taxAdminTextBox") as TextBox;
            TextBox taxnumber = GridView1.Rows[e.RowIndex].FindControl("taxNumberText") as TextBox;
            con.Open();
            //Update to databese CompanyInfo  
            SqlCommand cmd = new SqlCommand("Update CompanyInfo set Name='" + name.Text + "',SiteURL='" + url.Text + "',PhoneContryCode='" + phonecode.Text + "',PhoneNumber='" + phonenumber.Text + "',IBAN='" + iban.Text + "',Company_Description='" + desc.Text + "',Tax_Admin='" + taxadmin.Text + "',Tax_number='" + taxnumber.Text + "'", con);
            cmd.ExecuteNonQuery();
            con.Close();
            //Setting the EditIndex property to -1 to cancel the Edit mode in Gridview  
            GridView1.EditIndex = -1;
            //Call ShowData method for displaying updated data  
            ShowData();
        }
        protected void GridView1_RowCancelingEdit(object sender, System.Web.UI.WebControls.GridViewCancelEditEventArgs e)
        {
            //Setting the EditIndex property to -1 to cancel the Edit mode in Gridview  
            GridView1.EditIndex = -1;
            ShowData();
        }
    }
}
