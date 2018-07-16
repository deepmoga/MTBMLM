<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="Registration.aspx.cs" Inherits="Registration" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cpbread" Runat="Server">
        <style>
        .form-control {
    border-radius: 0px; 
    height: auto;
    line-height: 1.33333;
    padding: 15px 18px 15px 30px;
    background: transparent;
    color: #aaaaaa;
    border: 1px solid #00000091;
    /* box-shadow: 0px 4px 18px rgba(0, 0, 0, 0.1); */
}
        .titles {
                background: #006b2d;
    color: white;
    text-align:center;
    overflow:hidden;
        }
        .text-danger {
    color: #ff0500 !important;
}
    </style>
    <h3 class="text-white">Registration</h3>
                            <ul class="breadcrumb mt-10">
                                <li class="breadcrumb-item"><a href="#">Home</a></li>
                             
                                <li class="breadcrumb-item active">Available Pins</li>
                            </ul>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpmain" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class=" container">
        <div class="col-md-9">
            <h4 class="mb-30 titles">Sponser Details</h4>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div class="form-group col-sm-12" style="display:none">
                        <label for="First Name">Pin <span class=" text-danger">* </span></label>

                        <asp:TextBox ID="txtpin" OnTextChanged="txtpin_TextChanged"   AutoPostBack="true" class="form-control" placeholder="Pin" runat="server"></asp:TextBox>
                        <asp:Label ID="lblstatus" ForeColor="Red" runat="server" Text=""></asp:Label>
                    </div>
            <div class="form-group col-sm-12">
                <label for="First Name">SponserId <span class=" text-danger"> * </span></label>
               
                <asp:TextBox ID="txtsponserid" OnTextChanged="txtsponserid_TextChanged" required AutoPostBack="true" class="form-control" placeholder="Sponserid" runat="server"></asp:TextBox>
                <asp:Label ID="lblsponsername" runat="server" Text="" ForeColor="Red"></asp:Label>
            </div>
            <div class="form-group col-sm-12 hidden">
                <label for="First Name">ProposerId <span class=" text-danger"> * </span></label>
                <asp:TextBox ID="txtproposerid" AutoPostBack="true" class="form-control" OnTextChanged="txtproposerid_TextChanged" placeholder="ProposerId" runat="server"></asp:TextBox>
                <asp:Label ID="lblproposername" runat="server" Text="" ForeColor="Red"></asp:Label>
            </div>
            <div class="form-group col-sm-12">
                <label for="First Name">Placement</label>
                <asp:RadioButtonList ID="rdonode" runat="server" AutoPostBack="true" OnTextChanged="rdonode_TextChanged">
                    <asp:ListItem Value="one" Selected="True">Left</asp:ListItem>
                    <asp:ListItem Value="two">Right</asp:ListItem>
                </asp:RadioButtonList>
                <asp:Label runat="server" ID="lblleafnode" Text=""></asp:Label>
            </div>
                    </ContentTemplate>
            </asp:UpdatePanel>
             <h4 class="mb-30 col-md-12 titles">Personal Details</h4>
              <div class="form-group col-sm-12">
                <label for="First Name">Applicant Name <span class=" text-danger"> * </span></label>
               
                <asp:TextBox ID="txtname" class="form-control" placeholder="Applicant Name" runat="server" required></asp:TextBox>
            </div>
            <div class="form-group col-sm-12">
               
                <asp:RadioButtonList ID="rdotype" runat="server">
                    <asp:ListItem>S/o</asp:ListItem>
                    <asp:ListItem>D/o</asp:ListItem>
                    <asp:ListItem>W/o</asp:ListItem>
                </asp:RadioButtonList>
                <asp:TextBox ID="txtrelation"  class="form-control" placeholder="Relation" runat="server"></asp:TextBox>
            </div>
                        <div class="col-sm-12">
                <div class="row">
                    <label class="col-sm-12" for="phone">Your birthday </label>
                    <div class="form-group col-sm-12">
                        <div class="selected-box auto-hight">
                            <asp:TextBox ID="txtdob" runat="server" CssClass="form-control" placeholder="DD/MM/YYYY"></asp:TextBox>

                        </div>
                    </div>
                    
                    
                </div>
            </div>
            <div class="form-group col-sm-12">
                <label for="First Name">Phone <span class=" text-danger"> * </span></label>
               
                <asp:TextBox ID="txtphn" type="number" class="form-control" placeholder="Phone" runat="server" ></asp:TextBox>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server"
                    ControlToValidate="txtphn"
                    ErrorMessage="Only numeric allowed." ForeColor="Red"
                    ValidationExpression="^[0-9]*$" ValidationGroup="a">*
                </asp:RegularExpressionValidator>
            </div>
            <div class="form-group col-sm-12">
                <label for="First Name">Country <span class=" text-danger"> * </span></label>
               
                <asp:TextBox ID="txtcountry" class="form-control" Text="India"  placeholder="Country" runat="server"></asp:TextBox>
            </div>
            <div class="form-group col-sm-12">
                <label for="First Name">State <span class=" text-danger"> * </span></label>
               
                <asp:TextBox ID="txtstate" class="form-control"  placeholder="State" runat="server" ></asp:TextBox>
            </div>
            <div class="form-group col-sm-12">
                <label for="First Name">City <span class=" text-danger"> * </span></label>
               
                <asp:TextBox ID="txtcity" class="form-control"  placeholder="City" runat="server" ></asp:TextBox>
            </div>
            <div class="form-group col-sm-12">
                <label for="First Name">Address <span class=" text-danger"> * </span></label>
               
                <asp:TextBox ID="txtadd" TextMode="MultiLine" class="form-control" placeholder="Address" runat="server" ></asp:TextBox>
            </div>
            <div class="form-group col-sm-12">
                <label for="First Name">PinCode </label>
               
                <asp:TextBox ID="txtpincode" type="number" class="form-control"  placeholder="Country" runat="server"></asp:TextBox>
            </div>
            <div class="form-group col-sm-12">
                <label for="First Name">Aadhar Card No <span class=" text-danger"> * </span></label>
               
                <asp:TextBox ID="txtaadhar" type="number" class="form-control"  placeholder="Aadhar Card" runat="server" ></asp:TextBox>
               <%-- <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
                    ControlToValidate="txtaadhar"
                    ErrorMessage="Only numeric allowed." ForeColor="Red"
                    ValidationExpression="^[0-9]*$" ValidationGroup="a">*
                </asp:RegularExpressionValidator>--%>
            </div>
            <div class="form-group col-sm-12">
                <label for="First Name">PAN Card No </label>
               
                <asp:TextBox ID="txtpan" class="form-control"  placeholder="Pan Card" runat="server"></asp:TextBox>
            </div>
            <%--Bank Details--%>
              <h4 class="mb-30 titles col-md-12">Bank Details</h4>
            <div class="form-group col-sm-12">
                <label for="First Name">Bank Account No</label>
               
                <asp:TextBox ID="txtbankno" type="number" class="form-control" placeholder="Bank Account No" runat="server"></asp:TextBox>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server"
                    ControlToValidate="txtbankno"
                    ErrorMessage="Only numeric allowed." ForeColor="Red"
                    ValidationExpression="^[0-9]*$" ValidationGroup="a">*
                </asp:RegularExpressionValidator>
            </div>
            <div class="form-group col-sm-12">
                <label for="First Name">Bank Name </label>
               
                <asp:TextBox ID="txtbankname" class="form-control"  placeholder="Bank Name" runat="server"></asp:TextBox>
            </div>
            <div class="form-group col-sm-12">
                <label for="First Name">IFSC Code </label>
               
                <asp:TextBox ID="txtifsc" class="form-control"  placeholder="IFSC Code" runat="server"></asp:TextBox>
            </div>
             <%--Nominee Details--%>
           
             <h4 class="mb-30 titles col-md-12">NOMINEE Details</h4>
            <div class="form-group col-sm-12">
                <label for="First Name">Nominee Name <span class=" text-danger"> * </span></label>
               
                <asp:TextBox ID="txtnominee"  class="form-control"  placeholder="Nominee Name" runat="server"></asp:TextBox>
            </div>
            <div class="form-group col-sm-12">
                <label for="First Name">Relation </label>

                <asp:DropDownList ID="ddlrelation" runat="server">

                    <asp:ListItem Value="" Selected="True">Select Nominee Relation</asp:ListItem>
                    <asp:ListItem Value="Husband">Husband</asp:ListItem>
                    <asp:ListItem Value="Wife">Wife</asp:ListItem>
                    <asp:ListItem Value="Mother">Mother</asp:ListItem>
                    <asp:ListItem Value="Father">Father</asp:ListItem>
                    <asp:ListItem Value="Brother">Brother</asp:ListItem>
                    <asp:ListItem Value="Sister">Sister</asp:ListItem>
                    <asp:ListItem Value="Son">Son</asp:ListItem>
                    <asp:ListItem Value="Daughter">Daughter</asp:ListItem>
                    <asp:ListItem Value="Mother-in-Law">Mother-in-Law</asp:ListItem>
                    <asp:ListItem Value="Father-in-Law">Father-in-Law</asp:ListItem>
                    <asp:ListItem Value="Grandson">Grand Son</asp:ListItem>
                    <asp:ListItem Value="Granddaughter">Grand Daughter</asp:ListItem>
                    <asp:ListItem Value="Daughter-in-Law">Daughter-in-Law</asp:ListItem>
                    <asp:ListItem Value="Son-in-Law">Son-in-Law</asp:ListItem>
                    <asp:ListItem Value="Nephew">Nephew</asp:ListItem>
                    <asp:ListItem Value="Friend">Friend</asp:ListItem>
                </asp:DropDownList>
            </div>
               <div class="form-group col-sm-12">
                   <asp:CheckBox ID="chkterm" runat="server" Text="" />
                   <a href="#">I agree to terms and conditions</a>
                </div>
            <div class="form-group col-sm-12">
                <asp:Button ID="btnjoin" ValidationGroup="a" CausesValidation="true" class="button mt-30" OnClick="btnjoin_Click" runat="server" Text="Join Now" />
                <asp:Button ID="Button1" OnClick="Button1_Click" runat="server" Text="Button" Visible="false" />
                </div>
            <asp:ValidationSummary
                HeaderText="You must enter a value in the following fields:"
                DisplayMode="BulletList"
                EnableClientScript="true" ValidationGroup="a"
                runat="server" />
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cpfooter" Runat="Server">
     <style>
        table#ctl00_cpmain_rdonode tr {
    display: inline;
}
        table#ctl00_cpmain_rdotype tr {
    display: -webkit-inline-box;
}
    </style>    
</asp:Content>

