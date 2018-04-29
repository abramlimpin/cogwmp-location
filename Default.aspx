<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Church of God World Missions Philippines - Locations</title>
    <link href="https://bootswatch.com/3/spacelab/bootstrap.min.css" rel="stylesheet" />
    <link href="https://stackpath.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css" rel="stylesheet" />
    <link href="css/style.css" rel="stylesheet" />
    <script src="https://code.jquery.com/jquery-3.3.1.min.js"></script>
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
</head>
<body>
    <form id="frmLocations" runat="server">
        <asp:ScriptManager runat="server" />
        <asp:UpdatePanel ID="upLocations" runat="server">
            <ContentTemplate>
                    <div class="col-lg-3 col-xs-4">
                        <div class="list-group">
                            <asp:LinkButton ID="region_All" runat="server" class="list-group-item" OnClick="region_All_Click">
                                <span class="badge">
                                    <asp:Literal ID="ltTotal" runat="server" /></span>
                                ALL LOCATIONS
                            </asp:LinkButton>
                            <asp:LinkButton ID="region_NL" runat="server" CssClass="list-group-item" OnClick="region_NL_Click">
                                <span class="badge">
                                    <asp:Literal ID="ltTotal_NL" runat="server" /></span>
                                NORTH LUZON
                            </asp:LinkButton>
                            <asp:LinkButton ID="region_MM" runat="server" CssClass="list-group-item" OnClick="region_MM_Click">
                                <span class="badge">
                                    <asp:Literal ID="ltTotal_MM" runat="server" /></span>
                                METRO MANILA
                            </asp:LinkButton>
                            <asp:LinkButton ID="region_CSL" runat="server" CssClass="list-group-item" OnClick="region_CSL_Click">
                                <span class="badge">
                                    <asp:Literal ID="ltTotal_CSL" runat="server" /></span>
                                CENTRAL NORTH LUZON
                            </asp:LinkButton>
                            <asp:LinkButton ID="region_V" runat="server" CssClass="list-group-item" OnClick="region_V_Click">
                                <span class="badge">
                                    <asp:Literal ID="ltTotal_V" runat="server" /></span>
                                VISAYAS
                            </asp:LinkButton>
                            <asp:LinkButton ID="region_M" runat="server" CssClass="list-group-item" OnClick="region_M_Click">
                                <span class="badge">
                                    <asp:Literal ID="ltTotal_M" runat="server" /></span>
                                MINDANAO
                            </asp:LinkButton>
                        </div>
                    </div>
                    <div class="col-lg-9 col-xs-8">
                        <div class="panel-group" id="accordion">
                            <h3 class="text-center"><asp:Literal ID="ltHeader" runat="server" Text="ALL LOCATIONS" /></h3>
                            <asp:ListView ID="locations" runat="server">
                                <ItemTemplate>
                                    <div class="panel panel-default">
                                        <a data-toggle="collapse" data-parent="#accordion" href="#<%# Eval("slug") %>">
                                            <div class="panel-heading">
                                                <h4 class="panel-title">
                                                    <%# Eval("church_branch") %>
                                                </h4>
                                            </div>
                                        </a>
                                        <div id='<%# Eval("slug") %>' class="panel-collapse collapse">
                                            <div class="panel-body well">
                                                <ul class="fa-ul">
                                                    <li><i class="fa fa-map-marker fa-fw" title="District"></i><%# Eval("district") %></li>
                                                    <li><i class="fa fa-home fa-fw" title="Address"></i><%# Eval("address") %></li>
                                                    <li><i class="fa fa-user fa-fw" title="Head Pastor"></i><%# Eval("head_pastor") %></li>
                                                    <li><i class="fa fa-mobile fa-fw" title="Contact Number/s"></i><%# Eval("mobile_no") %></li>
                                                    <li><i class="fa fa-envelope fa-fw" title="Email Address"></i><a href='mailto:<%# Eval("email") %>'><%# Eval("email") %></a></li>
                                                </ul>
                                            </div>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:ListView>
                        </div>
                    </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:UpdateProgress runat="server" id="update_progress">
            <ProgressTemplate>
                <img src="images/loading.gif" />
            </ProgressTemplate>
        </asp:UpdateProgress>
    </form>
</body>
</html>
