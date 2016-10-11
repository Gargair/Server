<%@ Page Title="News" Language="C#" MasterPageFile="~/LoggedIn.Master" AutoEventWireup="true" CodeBehind="News.aspx.cs" Inherits="Server.News" Culture="en-us" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="form-horizontal">
        <asp:GridView ID="NewsGrid" AutoGenerateColumns="false" runat="server" Width="100%" ShowHeaderWhenEmpty="true" DataKeyNames="NewsId" AllowPaging="true" PageSize="5">
            <Columns>
                <asp:CommandField ButtonType="Link" ShowSelectButton="true" SelectText="Select" />
                <asp:BoundField DataField="NewsId" HeaderText="Id" ReadOnly="true" Visible="false" />
                <asp:BoundField DataField="Owner.NickName" HeaderText="Author" ReadOnly="true"/>
                <asp:BoundField DataField="Text" HeaderText="Text"/>
                <asp:CommandField DeleteText="Delete" ShowDeleteButton="true" />
            </Columns>
        </asp:GridView>
        <asp:DetailsView runat="server" ID="InsertView" AutoGenerateInsertButton="True" AutoGenerateEditButton="True" DataKeyNames="NewsId" Width="100%" Visible="false" AutoGenerateRows="false" EmptyDataText="Empty" EnablePagingCallbacks="true">
            <Fields>
                <asp:BoundField DataField="NewsId" HeaderText="Id" ReadOnly="true" Visible="false" InsertVisible="false" />
                <asp:BoundField DataField="Owner.NickName" HeaderText="Author" ReadOnly="true" InsertVisible="false" />
                <asp:BoundField DataField="Text" HeaderText="Text" ApplyFormatInEditMode="true" HtmlEncode="true" NullDisplayText="Empty" />
            </Fields>
        </asp:DetailsView>
    </div>
</asp:Content>
