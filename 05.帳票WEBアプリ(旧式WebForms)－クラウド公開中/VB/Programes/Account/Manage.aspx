<%@ Page Title="アカウントの管理" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Manage.aspx.vb" Inherits="Reports.net.Sample.WebSite.Manage" %>

<%@ Import Namespace="Reports.net.Sample.WebSite" %>
<%@ Import Namespace="Microsoft.AspNet.Identity" %>
<%@ Register Src="~/Account/OpenAuthProviders.ascx" TagPrefix="uc" TagName="OpenAuthProviders" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %>.</h2>

    <div>
        <asp:PlaceHolder runat="server" ID="SuccessMessagePlaceHolder" Visible="false" ViewStateMode="Disabled">
            <p class="text-success"><%: SuccessMessage %></p>
        </asp:PlaceHolder>
    </div>

    <div class="row">
        <div class="col-md-12">
            <div class="form-horizontal">
                <h4>アカウント設定を変更してください</h4>
                <hr />
                <dl class="dl-horizontal">
                    <dt>パスワード:</dt>
                    <dd>
                        <asp:HyperLink NavigateUrl="/Account/ManagePassword" Text="[変更]" Visible="false" ID="ChangePassword" runat="server" />
                        <asp:HyperLink NavigateUrl="/Account/ManagePassword" Text="[作成]" Visible="false" ID="CreatePassword" runat="server" />
                    </dd>
                    <dt>外部ログイン:</dt>
                    <dd><%:LoginsCount %>
                        <asp:HyperLink NavigateUrl="/Account/ManageLogins" Text="[管理]" runat="server" />

                    </dd>
                    <%--
                        電話番号を 2 要素認証システムにおける確認のための第 2 の要素として使用できます。
                        SMS を使用した 2 要素認証をサポートするようこの ASP.NET アプリケーションを設定する方法の詳細については、
                        <a href="http://go.microsoft.com/fwlink/?LinkId=403804">こちらの記事</a>をご覧ください。
                        2 要素認証を設定したら以下のブロックのコメントを解除してください
                    --%>
                    <%--
                    <dt>電話番号:</dt>
                     --%>
                    <% If HasPhoneNumber Then %>
                    <%--
                    <dd>
                        <asp:HyperLink NavigateUrl="/Account/AddPhoneNumber" runat="server" Text="[追加]" />
                    </dd>
                    --%>
                    <% Else %>
                    <%--
                    <dd>
                    
                        <asp:Label Text="" ID="PhoneNumber" runat="server" />
                        <asp:HyperLink NavigateUrl="/Account/AddPhoneNumber" runat="server" Text="[変更]" /> &nbsp;|&nbsp;
                        <asp:LinkButton Text="[削除]" OnClick="RemovePhone_Click" runat="server" />
                    </dd>
                    --%>
                    <% End If %>
                    <dt>2 要素認証:</dt>
                    <dd>
                        <p>
                            構成されている 2 要素認証プロバイダーがありません。2 要素認証をサポートするようこの ASP.NET アプリケーションを設定する方法の詳細については、
                            <a href="http://go.microsoft.com/fwlink/?LinkId=403804">こちらの記事</a>をご覧ください。
                        </p>                        
                        <% If TwoFactorEnabled Then %>
                        <%--
                        有効
                        <asp:LinkButton Text="[無効化]" runat="server" CommandArgument="false" OnClick="TwoFactorDisable_Click" />
                        --%>
                        <% Else %>
                       	<%--
                       	無効
                        <asp:LinkButton Text="[有効化]" CommandArgument="true" OnClick="TwoFactorEnable_Click" runat="server" />
                        --%>
                        <% End If %>
                    </dd>
                </dl>
            </div>
        </div>
    </div>
</asp:Content>

