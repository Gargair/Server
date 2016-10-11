<%@ Page Title="Baselist" Language="C#" MasterPageFile="~/LoggedIn.Master" AutoEventWireup="true" CodeBehind="List.aspx.cs" Inherits="Server.Base.List" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
 <h2><%: Title %></h2>
    <p class="text-danger">
        <asp:Literal runat="server" ID="ErrorMessage" />
    </p>
    <div class="form-horizontal">
        <asp:GridView Width="100%" ShowHeaderWhenEmpty="True" PageSize="3" AllowPaging="true" AutoGenerateColumns="False" RowHeaderColumn="true" runat="server" ID="BaseGrid" DataKeyNames="baseId">
            <Columns>
                <asp:CommandField ButtonType="Link" ShowSelectButton="true" SelectText="Select" />
                <asp:BoundField AccessibleHeaderText="Name" DataField="name" HeaderText="Name" />
                <asp:BoundField DataField="Owner.NickName" HeaderText="Owner" />
            </Columns>
        </asp:GridView>
    </div>
    <br />
    <br />
    <div class="form-horizontal">
        <asp:DetailsView DataKeyNames="baseId" Width="50%" AutoGenerateRows="false" runat="server" ID="DetailsView" Visible = "false">
            <Fields>
                <asp:BoundField DataField="name" HeaderText="Name" />
            </Fields>
        </asp:DetailsView>
    </div>
</asp:Content>