<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Reports.net.Sample.WebSite._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <h1>Reports.net Sample - Azure WEBSITE ASP.NET</h1>
        <p class="lead">サンプル処理を選択後、PDF表示・PDFダウンロードをクリックしてください。<br />
            単純なサンプル・10の倍数 以外のサンプルはAzueのSQLデータベースを使用しています。</p>
            <table>
                <tr>
                    <td style="width: 192px; height: 46px">
                        <asp:RadioButton ID="radSampleSimple" GroupName="grpSample" runat="server" Text="単純なサンプル" Checked="True" />
                    </td>
                    <td style="height: 46px; width: 155px;">
                        <asp:RadioButton ID="radSample10の倍数" GroupName="grpSample" runat="server" Text="10の倍数" />
                    </td>
                    <td style="height: 46px; width: 161px;">
                        <asp:RadioButton ID="radSample郵便番号" GroupName="grpSample" runat="server" Text="郵便番号" />
                    </td>
                    <td style="height: 46px; width: 129px;">
                        <asp:RadioButton ID="radSample見積書" GroupName="grpSample" runat="server" Text="見積書" />
                    </td>
                    <td style="height: 46px; width: 107px;">
                        <asp:RadioButton ID="radSample広告" GroupName="grpSample" runat="server" Text="広告" />
                    </td>
                    <td style="height: 46px; width: 129px;">
                        <asp:RadioButton ID="radSample請求書" GroupName="grpSample" runat="server" Text="請求書" />
                    </td>
                    <td style="height: 46px; width: 129px;">
                        <asp:RadioButton ID="radSample商品一覧" GroupName="grpSample" runat="server" Text="商品一覧" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Button ID="btnOutputPDF" runat="server" Text="PDF表示" OnClick="btnOutputPDF_Click" 
                                    OnClientClick="document.forms[0].target='_blank'; return true;" />
                    </td>
                    <td colspan="3">
                        <asp:Button ID="btnDownloadPDF" runat="server" Text="PDFダウンロード" OnClick="btnOutputPDF_Click" 
                                    OnClientClick="document.forms[0].target='_self'; return true;" />
                    </td>
                </tr>
            </table>
            
    </div>


</asp:Content>
