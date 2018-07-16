using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Default2 : System.Web.UI.Page
{
    string constring = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
    public int cl = 0, cr = 0, Cl = 0, Cr = 0, Activeid = 0, se = 0, ActiveidL = 0, c = 0, leftthismont = 0, thismonthinst = 0, thismonthinstleft = 0;
    SQLHelper objsql = new SQLHelper();
    public static string date = "";
    public static TimeZoneInfo INDIAN_ZONE;
    public DateTime indianTime = new DateTime();
    protected void Page_Load(object sender, EventArgs e)
    {
        INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        indianTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);
        date = indianTime.ToString("yyyy-MM-dd");
        if (!IsPostBack)
        {
            pNodeL("1001", "one");
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        using (SqlConnection con = new SqlConnection(constring))
        {

            using (SqlCommand cmd = new SqlCommand("VAL_DOWNLINE", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@PID", 1053);           // sponser id
                cmd.Parameters.AddWithValue("@ID", 1054);                            // node
                cmd.Parameters.Add("@Down", SqlDbType.VarChar, 30);
                cmd.Parameters["@Down"].Direction = ParameterDirection.Output;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                string a = cmd.Parameters["@Down"].Value.ToString();

            }

        }
    }
    private void pNodeL(string node, string place)
    {
        string sql = "select * from usersnew where sregno='" + node + "' and node='" + place + "'";


        DataTable dt = new DataTable();
        dt = objsql.GetTable(sql);

        if (dt.Rows.Count == 1)
        {
            ActiveL(node);
            Cl = Cl + 1;
            pNodeLL(dt.Rows[0]["regno"].ToString(), "");

        }
        else if (dt.Rows.Count > 1)
        {
            ActiveL(node);
            Cl = Cl + 2;

            pNodeLL(dt.Select("node='one'")[0].ItemArray[0].ToString(), "One");
            pNodeLL(dt.Select("node='two'")[0].ItemArray[0].ToString(), "Two");

        }
        if (dt.Rows.Count == 0)
        {
            ActiveL(node);

        }
    }
    // Calculate after 1 count left
    private void pNodeLL(string node, string place)
    {
        string sql = "select * from usersnew where sregno='" + node + "' ";
        DataTable dt = new DataTable();
        dt = objsql.GetTable(sql);

        if (dt.Rows.Count == 1)
        {
            ActiveL(node);
            Cl = Cl + 1;
          
            pNodeLL(dt.Rows[0]["regno"].ToString(), "");
        }
        else if (dt.Rows.Count > 1 || dt.Rows.Count < 0)
        {
            ActiveL(node);
            Cl = Cl + 2;

            pNodeLL(dt.Select("node='one'")[0].ItemArray[0].ToString(), "One");


            pNodeLL(dt.Select("node='two'")[0].ItemArray[0].ToString(), "Two");

            //   ActiveL(node);


        }
        if (dt.Rows.Count == 0)
        {

            ActiveL(node);
         
        }

    }
    protected void ActiveL(string id)
    {
        string date = "2018-07-09";
        string date2 = Common.Get(objsql.GetSingleValue("select joined from usersnew where joined='" + date + "'"));
        if (date2 != "")
        {
            thismonthinst += 1;
        }

    }


    private void pNodeR(string node, string place)
    {
        string sql = "select * from usersnew where sregno='" + node + "' and node='" + place + "'";


        DataTable dt = new DataTable();
        dt = objsql.GetTable(sql);

        if (dt.Rows.Count == 1)
        {
            ActiveR(node);
            Cl = Cl + 1;
            pNodeRR(dt.Rows[0]["regno"].ToString(), "");

        }
        else if (dt.Rows.Count > 1)
        {
            ActiveR(node);
            Cl = Cl + 2;

            pNodeRR(dt.Select("node='one'")[0].ItemArray[0].ToString(), "One");
            pNodeRR(dt.Select("node='two'")[0].ItemArray[0].ToString(), "Two");

        }
        if (dt.Rows.Count == 0)
        {
            ActiveR(node);
           
        }
    }
    // Calculate after 1 count left
    private void pNodeRR(string node, string place)
    {
        string sql = "select * from usersnew where sregno='" + node + "' ";
        DataTable dt = new DataTable();
        dt = objsql.GetTable(sql);

        if (dt.Rows.Count == 1)
        {
            ActiveR(node);
            Cl = Cl + 1;
      
            pNodeRR(dt.Rows[0]["regno"].ToString(), "");
        }
        else if (dt.Rows.Count > 1 || dt.Rows.Count < 0)
        {
            ActiveR(node);
            Cl = Cl + 2;
        
            pNodeRR(dt.Select("node='one'")[0].ItemArray[0].ToString(), "One");


            pNodeRR(dt.Select("node='two'")[0].ItemArray[0].ToString(), "Two");

            //   ActiveL(node);


        }
        if (dt.Rows.Count == 0)
        {

            ActiveR(node);
         
        }

    }
    protected void ActiveR(string id)
    {
        
    }
}