using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class User_ewallet : System.Web.UI.Page
{
    SQLHelper objsql = new SQLHelper();
    public int leftpair = 0, rightpair = 0;
    DataTable dt = new DataTable();
    public int total = 0, bal = 0;
    public string value1 = "", value2 = "";
    public int cl = 0, cr = 0, Cl = 0, Cr = 0, Activeid = 0, se = 0, ActiveidL = 0, c = 0, leftthismont = 0, thismonthinst = 0, thismonthinstleft = 0,pair=0;
    public int left = 0, right = 0;
    public static string date = "";
    public static TimeZoneInfo INDIAN_ZONE;
    public DateTime indianTime = new DateTime();

    protected void Page_Load(object sender, EventArgs e)
    {
        INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        indianTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);
        date = indianTime.ToString("yyyy-MM-dd");
       // date = "2018-07-15";
       
        if (!IsPostBack)
        {
            txtdate.Text = date;

            pNodeL(Session["user"].ToString(), "one");
            pNodeR(Session["user"].ToString(), "two");
           
            if (left > 0)
            {
                left = left - 1;
                
            }
            if (right > 0)
            {
               right=    right - 1;
            }
            bind(Session["user"].ToString());
        }
    }
    protected void bind(string reg)
    {
        lblregno.Text = reg.ToString();
        lblname.Text = Common.Get(objsql.GetSingleValue("select fname from usersnew where regno='" + reg + "'"));
        leftpair = int.Parse(Common.Get(objsql.GetSingleValue("select leftleg from legs where regno='" + reg + "'")));
        rightpair = int.Parse(Common.Get(objsql.GetSingleValue("select rightleg from legs where regno='" + reg + "'")));
        check();
        string active = Common.Get(objsql.GetSingleValue("select active from usersnew where regno='" + reg + "'"));
        if (active == "False")
        {
            lbltotal.Text = "0";
            lblincome.Text = "0";
        }
        else
        {
            lbltotal.Text = lblincome.Text;
        }

        //  lblptotal.Text = (Convert.ToInt32(lblpincome.Text) * Convert.ToInt32(proposer)).ToString();
        //lbltotal.Text = (Convert.ToInt32(lblstotal.Text) + Convert.ToInt32(lblptotal.Text)).ToString();
        lbltds.Text = ((Convert.ToInt32(lbltotal.Text) * Convert.ToInt32(5)) / Convert.ToInt32(100)).ToString();
        lbladmin.Text = ((Convert.ToInt32(lbltotal.Text) * Convert.ToInt32(5)) / Convert.ToInt32(100)).ToString();
        lbldeduction.Text = ((Convert.ToInt32(lbltotal.Text) * Convert.ToInt32(20)) / Convert.ToInt32(100)).ToString();
        lblnet.Text = (Convert.ToInt32(lbltotal.Text) - (Convert.ToInt32(lbltds.Text) + Convert.ToInt32(lbladmin.Text))).ToString();
        dt = objsql.GetTable("select * from payout where regno='" + reg + "'");
        if (dt.Rows.Count > 0)
        {
            ListView1.DataSource = dt;
            ListView1.DataBind();
        }
        else
        {
            bal = Convert.ToInt32(lblnet.Text);
        }
    }
    protected void income()
    {
        DataTable dt2 = new DataTable();
        dt2 = objsql.GetTable("select * from legs where regno='" + Session["user"] + "'");
        if (dt2.Rows.Count > 0)
        {
            int left = Convert.ToInt32(dt2.Rows[0]["leftleg"]);
            int cappingleft = Convert.ToInt32(dt2.Rows[0]["cappingleft"]);
            if (cappingleft > left)
            {
                left = left;
            }
            else
            {
                left = left - cappingleft;
            }
            int right = Convert.ToInt32(dt2.Rows[0]["rightleg"]);
            int cappingright = Convert.ToInt32(dt2.Rows[0]["cappingright"]);
            if (cappingright > right)
            {
                //  right = right - cappingright;
            }
            else
            {
                right = right - cappingright;
            }

            if ((left >= 2 && right >= 1) || (left >= 1 && right >= 2))
            {
                if (left < right)
                {
                    lblincome.Text = (left * 200).ToString();
                }
                else if (left == right)
                {
                    left = left - 1;
                    lblincome.Text = (left * 200).ToString();
                }
                else
                {
                    lblincome.Text = (right * 200).ToString();
                }

                //if (left == right)
                //{
                //    int minus = left - 1;
                //    lblincome.Text = (300 * minus).ToString();
                //}
                //if (left < right)
                //{
                //    int minus = left - 1;
                //    lblincome.Text = (300 * minus).ToString();
                //}
                //else
                //{
                //    int minus = right - 1;
                //    lblincome.Text = (300 * minus).ToString();
                //}

            }
            else
            {
                if (left == right)
                {
                    int minus = left - 1;
                    lblincome.Text = (200 * minus).ToString();
                }
                else if (left < right)
                {
                    int minus = left - 1;
                    lblincome.Text = (200 * minus).ToString();
                }
                else
                {
                    int minus = right - 1;
                    lblincome.Text = (200 * minus).ToString();
                }
            }

            capping();
        }
    }
    protected void capping()
    {
        int left = Convert.ToInt32(Common.Get(objsql.GetSingleValue("select cappingleft from legs where regno='" + Session["user"] + "' ")));
        int right = Convert.ToInt32(Common.Get(objsql.GetSingleValue("select cappingright from legs where regno='" + Session["user"] + "' ")));
        lblcapping.Text = (left + right).ToString();

    }

    

   




    protected void ListView1_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        if (e.Item.ItemType == ListViewItemType.DataItem)
        {
            Label amt = (Label)e.Item.FindControl("lblamt");
            total += int.Parse(amt.Text);

        }
        bal = Convert.ToInt32(lblnet.Text) - total;
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
       
        DateTime date2 =Convert.ToDateTime(Common.Get(objsql.GetSingleValue("select joined from usersnew where regno='" + id + "'")));
        string datelive = date2.ToString("yyyy-MM-dd");
        if (datelive == date)
        {
            left += 1;
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


        DateTime date2 = Convert.ToDateTime(Common.Get(objsql.GetSingleValue("select joined from usersnew where regno='" + id + "'")));
        string datelive = date2.ToString("yyyy-MM-dd");
        if (datelive == date)
        {
            right += 1;
        }
    }


    protected void check()
    {
        if (left >= 2 && right >= 1 || left >= 1 && right >= 2)
        {
            if (left <= right)
            {
                pair = left;
            }
            else
            {
                pair = right;
            }
            if (pair > 15)
            {
                lblincome.Text = (15 * 200).ToString();
            }
            else
            {
                lblincome.Text = (pair * 200).ToString();
            }
        }



        //if (left <= 15 || right <= 15)
        //{



        //    if ((left >= 2 && right >= 1) || (left >= 1 && right >= 2))
        //    {
        //        if (left < right)
        //        {
        //            lblincome.Text = (left * 200).ToString();
        //        }
        //        else if (left == right)
        //        {
        //            left = left - 1;
        //            lblincome.Text = (left * 200).ToString();
        //        }
        //        else
        //        {
        //            lblincome.Text = (right * 200).ToString();
        //        }

        //        //if (left == right)
        //        //{
        //        //    int minus = left - 1;
        //        //    lblincome.Text = (300 * minus).ToString();
        //        //}
        //        //if (left < right)
        //        //{
        //        //    int minus = left - 1;
        //        //    lblincome.Text = (300 * minus).ToString();
        //        //}
        //        //else
        //        //{
        //        //    int minus = right - 1;
        //        //    lblincome.Text = (300 * minus).ToString();
        //        //}

        //    }
        //    else
        //    {
        //        if (left == right)
        //        {
        //            int minus = left - 1;
        //            if (minus < 0)
        //            {
        //                lblincome.Text = "0";


        //            }
        //            else
        //            {
        //                lblincome.Text = (200 * minus).ToString();
        //            }


        //        }
        //        else if (left < right)
        //        {
        //            int minus = left - 1;
        //            lblincome.Text = (200 * minus).ToString();
        //        }
        //        else
        //        {
        //            int minus = right - 1;
        //            if (minus < 0)
        //            {
        //                lblincome.Text = "0";


        //            }
        //            else
        //            {
        //                lblincome.Text = (200 * minus).ToString();
        //            }
        //        }

        //    }
        //}
    }


    protected void btndate_Click(object sender, EventArgs e)
    {
        date = txtdate.Text;
        pNodeL(Session["user"].ToString(), "one");
        pNodeR(Session["user"].ToString(), "two");

        if (left > 0)
        {
            left = left - 1;

        }
        if (right > 0)
        {
            right = right - 1;
        }
        bind(Session["user"].ToString());
    }
}