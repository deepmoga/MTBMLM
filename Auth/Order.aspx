<%@ Page Title="" Language="C#" MasterPageFile="~/Auth/main.master" AutoEventWireup="true" CodeFile="Order.aspx.cs" Inherits="Auth_Order" %>

<%@ Register TagPrefix="ajaxToolkit" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cptitle" runat="Server">
    <h3>Purchase Product</h3>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cpmain" runat="Server">
    <div class="col-md-8">


        <div class="col-md-12">
            <div class="form-group col-md-4" style="display:none">
           
                <asp:DropDownList ID="ddllevel" runat="server" CssClass="form-control"></asp:DropDownList>

        </div>
        <div class="form-group col-md-4">
           
            <asp:TextBox ID="txtname" runat="server" class="form-control" placeholder="Search Product Name"></asp:TextBox>

            <ajaxToolkit:AutoCompleteExtender ServiceMethod="SearchCustomers"
                MinimumPrefixLength="2"
                CompletionInterval="100" EnableCaching="false" CompletionSetCount="10"
                TargetControlID="txtname"
                ID="AutoCompleteExtender1" runat="server" FirstRowSelected="false">
            </ajaxToolkit:AutoCompleteExtender>
        </div>
        <div class="form-group col-md-4">
           
            <asp:TextBox ID="txtqty" placeholder="Quantity" runat="server" class="form-control"></asp:TextBox>

        </div>

        <div class="form-group">
            <asp:Button CssClass="btn-success btn" ID="btnsubmit" runat="server"
                Text="Submit" OnClick="btnsubmit_Click" />
        </div>
        </div>
        <div class="col-md-12">
            <asp:GridView ID="GridView1" CssClass="table table-bordered" HeaderStyle-BackColor="#3AC0F2" HeaderStyle-ForeColor="White"
                runat="server" AutoGenerateColumns="false">
                <Columns>
                    <asp:BoundField DataField="Id" HeaderText="Id" ItemStyle-Width="30" />
                    <asp:BoundField DataField="Name" HeaderText="Name" ItemStyle-Width="150" />
                    <asp:BoundField DataField="quantity" HeaderText="Quantity" ItemStyle-Width="150" />
                    <asp:BoundField DataField="price" HeaderText="Price/Pic." ItemStyle-Width="150" />
                     <asp:TemplateField HeaderText="Total">
                         <ItemTemplate>
                             <asp:Label ID="total" runat="server" Text='<%#Eval("total") %>'></asp:Label>
                             <asp:HiddenField ID="hfid" Value='<%#Eval("pid") %>' runat="server" />
                             <asp:HiddenField ID="qty" Value='<%#Eval("quantity") %>' runat="server" />
                         </ItemTemplate>
                     </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <asp:Button ID="Button1" Visible="false" runat="server" OnClick="Button1_Click"  Text="Create Invoice" CssClass="btn btn-danger" />
        </div>
    </div>
    <div class="col-md-4">
       
        <table style="width: 100%;">
            <tr>
                <td>Total</td>
                <td><asp:Label ID="lbltotal" runat="server" Text="0"></asp:Label></td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>Direct Income</td>
                <td>
                    <asp:Label ID="lblpair" runat="server" Text="0"></asp:Label> | <asp:Label ID="lbldirectincome" runat="server" Text="0"></asp:Label></td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>Pay</td>
                <td>
                    <asp:Label ID="lblpay" runat="server" Text="0"></asp:Label></td>
                <td>&nbsp;</td>
            </tr>
        </table>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cpfotter" runat="Server">
</asp:Content>

