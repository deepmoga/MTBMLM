﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Auth/main.master" AutoEventWireup="true" CodeFile="product.aspx.cs" Inherits="Auth_product" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphead" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cptitle" Runat="Server">
    Product
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cpmain" Runat="Server">
     <div class="col-md-8">
       
             
            
           <div class="form-group">
               <label> Name</label>
               <asp:TextBox ID="txtname" runat="server" class="form-control" ></asp:TextBox>
              
          </div>
        <div class="form-group">
               <label> DP </label>
               <asp:TextBox ID="txtdp" runat="server" class="form-control" ></asp:TextBox>
              
          </div>
         <div class="form-group">
               <label> Pv </label>
               <asp:TextBox ID="txtpv" runat="server" class="form-control" ></asp:TextBox>
              
          </div>
          <div class="form-group">
                   <asp:Button CssClass="btn-success" ID="btnsubmit" runat="server" 
                       Text="Submit" onclick="btnsubmit_Click" />
        </div>
    </div>
    <div class="col-md-4">
       
                    
    </div>
           
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cpfotter" Runat="Server">
</asp:Content>

