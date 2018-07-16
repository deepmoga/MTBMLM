using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Transactions;
public partial class User_Join_us : System.Web.UI.Page
{
    public static string pin = "", sponser = "", pintype = "", newregno = "", pass = "", lastdata;
    SQLHelper objsql = new SQLHelper();
    string constring = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
    public static Boolean proposalstatus = false;
    public static int lefdirect = 0, rightdirect = 0, left = 0, right = 0, cappingleft = 0, cappingright = 0;
    int checkcapping = 0;
    public static string date = "";
    public static TimeZoneInfo INDIAN_ZONE;
    public DateTime indianTime = new DateTime();
    Common cm = new Common();
    protected void Page_Load(object sender, EventArgs e)
    {
        INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        indianTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);
        date = indianTime.ToString("yyyy-MM-dd");
        if (!IsPostBack)
        {
            if (Request.QueryString["pin"] != null || Request.QueryString["sponser"] != null)
            {


                txtpin.Text = Request.QueryString["pin"].ToString();
                //sponser = Request.QueryString["sponser"].ToString();
                //txtsponserid.Text = sponser;
                //lblsponsername.Text = Common.Get(objsql.GetSingleValue("select fname from usersnew where regno='" + sponser + "'"));
                lblleafnode.Text = lastnode(sponser);
                //pintype = Common.Get(objsql.GetSingleValue("select pintype from pins where pin='" + pin + "'"));
                lastdata = lastnode(sponser);
            }
        }
    }

    #region Check Valid Upline
    protected void txtproposerid_TextChanged(object sender, EventArgs e)
    {

        using (SqlConnection con = new SqlConnection(constring))
        {
            using (SqlCommand cmd = new SqlCommand("VAL_DOWNLINE", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@PID", txtproposerid.Text.Trim());         // proposer id
                cmd.Parameters.AddWithValue("@ID", txtsponserid.Text.Trim());                             // sponser id
                cmd.Parameters.Add("@Down", SqlDbType.VarChar, 30);
                cmd.Parameters["@Down"].Direction = ParameterDirection.Output;  // outpur parameter
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                string check = cmd.Parameters["@Down"].Value.ToString();
                if (check == "")
                {
                    lblproposername.Text = "Proposer is Not Vaid in up line";
                }
                else
                {
                    lblproposername.Text = Common.Get(objsql.GetSingleValue("select fname from usersnew where regno='" + txtproposerid.Text + "'"));
                    proposalstatus = true;
                    lblleafnode.Text = lastnode(sponser);
                }
                //  lblFruitName.Text = "Last Node: " + cmd.Parameters["@printvalue"].Value.ToString();
            }
        }
    }
    #endregion

    protected string lastnode(string sponser)
    {
        using (SqlConnection con = new SqlConnection(constring))
        {

            using (SqlCommand cmd = new SqlCommand("LastNode", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", txtsponserid.Text.Trim());           //sponser id
                cmd.Parameters.AddWithValue("@node", rdonode.SelectedItem.Value);                            //node
                cmd.Parameters.Add("@printvalue", SqlDbType.NVarChar, 30);
                cmd.Parameters["@printvalue"].Direction = ParameterDirection.Output;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                return cmd.Parameters["@printvalue"].Value.ToString();

            }

        }
    }

    protected void rdonode_TextChanged(object sender, EventArgs e)
    {
        lblleafnode.Text = lastnode(sponser);
        // lastdata = lastnode(sponser);
    }

    protected void btnjoin_Click(object sender, EventArgs e)
    {
        using (TransactionScope ts = new TransactionScope())
        {
            try
            {
                if (lblsponsername.Text != "")
                {

                    // insert in usersnew table
                    // sregno= lastnode
                    //spillsregno = sponser
                    newregno = cm.GenerateRegno();
                    string now = "MM" + newregno;
                    pass = cm.Generatepass();
                    #region Check capping
                    if (sponser != "")
                    {
                        string countcapping = "";
                        if (txtpin.Text != "")
                        {
                            objsql.ExecuteNonQuery("insert into usersnew(regno,pass,fname,lname,dob,add1,city,pin,state,country,mobile,nomirel,sregno,node,status,joined,grace,spillsregno,updated,updatepin,pintypeid,aadharcard,proposerregno,relation,active,capping,rid) values('" + now + "','" + pass + "','" + txtname.Text + "','" + txtrelation.Text + "','" + dob() + "','" + txtadd.Text + "','" + txtcity.Text + "','" + txtpincode.Text + "','" + txtstate.Text + "','" + txtcountry.Text + "','" + txtphn.Text + "','" + txtnominee.Text + "','" + lastnode(sponser) + "','" + rdonode.SelectedItem.Value + "','y','" + indianTime.ToString("yyyy-MM-dd HH:mm:ss") + "','10','" + sponser + "','n','" + txtpin.Text + "','" + pintype + "','" + txtaadhar.Text + "','" + txtproposerid.Text + "','" + ddlrelation.SelectedItem.Text + "','1','0','"+newregno+"')");
                            #region insert in leg table


                            objsql.ExecuteNonQuery("insert into legs(regno,leftleg,rightleg,leftdirect,rightdirect,status,cappingleft,cappingright)values('" + now + "','0','0','0','0','0','0','0')");

                            #endregion
                            using (SqlConnection con = new SqlConnection(constring))
                            {

                                using (SqlCommand cmd = new SqlCommand("EveryNodeNo", con))
                                {
                                    string check = lastdata;
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.Parameters.AddWithValue("@id", lblleafnode.Text);           // sponser id
                                    cmd.Parameters.AddWithValue("@node", rdonode.SelectedItem.Value);                            // node
                                    cmd.Parameters.AddWithValue("@checkid", lblleafnode.Text);
                                    cmd.Parameters.Add("@printvalue", SqlDbType.VarChar, 30);
                                    cmd.Parameters["@printvalue"].Direction = ParameterDirection.Output;
                                    con.Open();
                                    cmd.ExecuteNonQuery();
                                    con.Close();
                                    string a = cmd.Parameters["@printvalue"].Value.ToString();

                                }

                            }
                            objsql.ExecuteNonQuery("update pins set status='y', subregno='" + newregno + "' where pin='" + txtpin.Text + "'");
                        }
                        else
                        {
                            objsql.ExecuteNonQuery("insert into usersnew(regno,pass,fname,lname,dob,add1,city,pin,state,country,mobile,nomirel,sregno,node,status,joined,grace,spillsregno,updated,updatepin,pintypeid,aadharcard,proposerregno,relation,active,capping,rid) values('" + now + "','" + pass + "','" + txtname.Text + "','" + txtrelation.Text + "','" + dob() + "','" + txtadd.Text + "','" + txtcity.Text + "','" + txtpincode.Text + "','" + txtstate.Text + "','" + txtcountry.Text + "','" + txtphn.Text + "','" + txtnominee.Text + "','" + lastnode(sponser) + "','" + rdonode.SelectedItem.Value + "','y','" + indianTime.ToString("yyyy-MM-dd HH:mm:ss") + "','10','" + sponser + "','n','" + txtpin.Text + "','" + pintype + "','" + txtaadhar.Text + "','" + txtproposerid.Text + "','" + ddlrelation.SelectedItem.Text + "','0','0','"+newregno+"')");
                            #region insert in leg table


                            objsql.ExecuteNonQuery("insert into legs(regno,leftleg,rightleg,leftdirect,rightdirect,status,cappingleft,cappingright)values('" + now + "','0','0','0','0','0','0','0')");






                            #endregion

                        }

                        #region capping roghf


                        //countcapping = Common.Get(objsql.GetSingleValue("select count(*) from usersnew where spillsregno='" + sponser + "' and joined='" + date + "'"));
                        //if (Convert.ToInt32(countcapping) >= 30)
                        //{
                        //    objsql.ExecuteNonQuery("insert into usersnew(regno,pass,fname,lname,dob,add1,city,pin,state,country,mobile,nomirel,sregno,node,status,joined,grace,spillsregno,updated,updatepin,pintypeid,aadharcard,proposerregno,relation,active,capping) values('" + newregno + "','" + pass + "','" + txtname.Text + "','" + txtrelation.Text + "','" + dob() + "','" + txtadd.Text + "','" + txtcity.Text + "','" + txtpincode.Text + "','" + txtstate.Text + "','" + txtcountry.Text + "','" + txtphn.Text + "','" + txtnominee.Text + "','" + lastnode(sponser) + "','" + rdonode.SelectedItem.Value + "','y','" + indianTime.ToString("yyyy-MM-dd HH:mm:ss") + "','10','" + sponser + "','n','" + txtpin.Text + "','" + pintype + "','" + txtaadhar.Text + "','" + txtproposerid.Text + "','" + ddlrelation.SelectedItem.Text + "','0','1')");
                        //    #region insert in leg table
                        //    DataTable capp = new DataTable();
                        //    capp = objsql.GetTable("select * from legs where regno='" + sponser + "'");
                        //    if (capp != null)
                        //    {
                        //        if (rdonode.SelectedItem.Value == "one")
                        //        {

                        //            cappingleft = (Convert.ToInt32(capp.Rows[0]["cappingleft"]) + 1);
                        //            objsql.ExecuteNonQuery("update legs set cappingleft='" + cappingleft + "' where regno='" + sponser + "'");
                        //        }
                        //        else
                        //        {
                        //            cappingright = (Convert.ToInt32(capp.Rows[0]["cappingright"]) + 1);
                        //            objsql.ExecuteNonQuery("update legs set cappingright='" + cappingright + "' where regno='" + sponser + "'");
                        //        }
                        //    }
                        //    else
                        //    {
                        //        if (rdonode.SelectedItem.Value == "one")
                        //        {

                        //            cappingleft = 1;
                        //            objsql.ExecuteNonQuery("update legs set cappingleft='" + cappingleft + "' where regno='" + sponser + "'");

                        //        }
                        //        else
                        //        {
                        //            cappingright = 1;
                        //            objsql.ExecuteNonQuery("update legs set cappingright='" + cappingright + "' where regno='" + sponser + "'");

                        //        }
                        //    }

                        //    objsql.ExecuteNonQuery("insert into legs(regno,leftleg,rightleg,leftdirect,rightdirect,status,cappingleft,cappingright)values('" + newregno + "','0','0','0','0','0','0','0')");






                        //    #endregion
                        //}
                        //else
                        //{
                        //    objsql.ExecuteNonQuery("insert into usersnew(regno,pass,fname,lname,dob,add1,city,pin,state,country,mobile,nomirel,sregno,node,status,joined,grace,spillsregno,updated,updatepin,pintypeid,aadharcard,proposerregno,relation,active,capping) values('" + newregno + "','" + pass + "','" + txtname.Text + "','" + txtrelation.Text + "','" + dob() + "','" + txtadd.Text + "','" + txtcity.Text + "','" + txtpincode.Text + "','" + txtstate.Text + "','" + txtcountry.Text + "','" + txtphn.Text + "','" + txtnominee.Text + "','" + lastnode(sponser) + "','" + rdonode.SelectedItem.Value + "','y','" + indianTime.ToString("yyyy-MM-dd HH:mm:ss") + "','10','" + sponser + "','n','" + txtpin.Text + "','" + pintype + "','" + txtaadhar.Text + "','" + txtproposerid.Text + "','" + ddlrelation.SelectedItem.Text + "','0','0')");
                        //    #region insert in leg table


                        //    objsql.ExecuteNonQuery("insert into legs(regno,leftleg,rightleg,leftdirect,rightdirect,status,cappingleft,cappingright)values('" + newregno + "','0','0','0','0','0','0','0')");






                        //    #endregion
                        //}


                        #endregion




                    }
                    #endregion

                    

                    #region Update Legs Direct
                    DataTable dt = new DataTable();
                    dt = objsql.GetTable("select * from legs where regno='" + txtsponserid.Text + "'");
                    if (dt.Rows.Count > 0)
                    {
                        if (txtsponserid.Text != "")        // direct 
                        {
                            if (rdonode.SelectedItem.Value == "one")
                            {
                                int getdirect = int.Parse(Common.Get(objsql.GetSingleValue("select leftdirect from legs where regno='" + txtsponserid.Text + "'")));

                                getdirect += 1;
                                objsql.ExecuteNonQuery("update legs set leftdirect='" + getdirect + "' where regno='" + txtsponserid.Text + "'");


                            }
                            else
                            {
                                rightdirect = int.Parse(Common.Get(objsql.GetSingleValue("select rightdirect from legs where regno='" + txtsponserid.Text + "'")));
                                rightdirect += 1;
                                objsql.ExecuteNonQuery("update legs set rightdirect='" + rightdirect + "' where regno='" + txtsponserid.Text + "'");

                            }

                        }
                        
                    }
                    #endregion
                    
                    


                    ts.Complete();
                    ts.Dispose();
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Thank You For Registration ')", true);
                    string newd = "MM" + newregno;
                    Response.Redirect("welcome.aspx?id=" + newd + "&pass=" + pass + "&name=" + txtname.Text);

                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Sponser Invalid')", true);

                }
            }
            catch (Exception a)
            {

                string msz = a.Message;
                throw;
            }
        }
    }
    protected string dob()
    {
        return txtdob.Text;
      //  return ddlmonth.SelectedItem.Text + "/" + ddlday.SelectedItem.Text + "/" + ddlyear.SelectedItem.Text;
    }

    protected void txtsponserid_TextChanged(object sender, EventArgs e)
    {
        lblsponsername.Text = Common.Get(objsql.GetSingleValue("select fname from usersnew where regno='" + txtsponserid.Text + "'"));
        if (lblsponsername.Text != "")
        {
            sponser = txtsponserid.Text;
            lastdata = lastnode(sponser);
            lblleafnode.Text = lastnode(sponser);
            capping(txtsponserid.Text);
        }
        else
        {
            btnjoin.Enabled = false;
        }

    }

    protected void Button1_Click(object sender, EventArgs e)
    {

    }

    protected void txtpin_TextChanged(object sender, EventArgs e)
    {
        string pin = Common.Get(objsql.GetSingleValue("select pin from pins where pin='" + txtpin.Text + "' and status='n'"));
        if (pin != "")
        {
            lblstatus.Text = "Pin Available";
        }
        else
        {
            lblstatus.Text = "Pin Not Available";
            btnjoin.Enabled = false;
        }
    }
    public void capping(string sponser)
    {

    }

}