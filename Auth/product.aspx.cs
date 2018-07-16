using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Auth_product : System.Web.UI.Page
{
    SQLHelper objsql = new SQLHelper();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["id"] != null)
            {
                bind(Request.QueryString["id"].ToString());
            }
        }
    }

    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["id"] != null)
        {
            objsql.ExecuteNonQuery("update tblproducts set name='" + txtname.Text + "',dp='" + txtdp.Text + "',pv='"+txtpv.Text+"' where id='" + Request.QueryString["id"] + "'");
        }
        else
        {
            objsql.ExecuteNonQuery("insert into tblproducts(name,dp,pv) values('" + txtname.Text + "','" + txtdp.Text + "','"+txtpv.Text+"')");

        }
        Response.Redirect("view-Products.aspx");
    }
    protected void bind(string id)
    {
        DataTable dt = new DataTable();
        dt = objsql.GetTable("select * from tblproducts where id='" + id + "'");
        if (dt.Rows.Count > 0)
        {
            txtname.Text = dt.Rows[0]["name"].ToString();
            txtdp.Text = dt.Rows[0]["dp"].ToString();
            txtpv.Text = dt.Rows[0]["pv"].ToString();
        }
    }
}