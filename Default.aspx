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
        <div id="dasma" class="map-style" style="height: 100%;"></div>
        <script>
            var map;
            function loadMap() {
                map = new google.maps.Map(document.getElementById('dasma'), {
                    center: { lat: 14.329039, lng: 120.941083 },
                    zoom: 8
                });
            }
        </script>
        <asp:ScriptManager runat="server" />
        <asp:UpdatePanel ID="upLocations" runat="server">
            <ContentTemplate>
                <div class="col-lg-3 col-xs-4">

                    <div class="list-group">
                        <div class="list-group-item" style="padding: 0; border: 0;">
                            <div class="input-group">
                                <asp:TextBox ID="txtKeyword" runat="server" CssClass="form-control" placeholder="Keyword..." MaxLength="20"
                                    AutoPostBack="true" OnTextChanged="txtKeyword_TextChanged" autocomplete="off" />
                                <span class="input-group-btn">
                                    <button class="btn btn-default" type="button"><i class="fa fa-search"></i></button>
                                </span>
                            </div>
                            <!-- /input-group -->
                        </div>
                        <asp:LinkButton ID="region_All" runat="server" class="list-group-item active" OnClick="region_All_Click">
                                <%--<span class="badge">
                                    <asp:Literal ID="ltTotal" runat="server" /></span>--%>
                                ALL LOCATIONS
                        </asp:LinkButton>
                        <asp:LinkButton ID="region_NL" runat="server" CssClass="list-group-item active" OnClick="region_NL_Click">
                                <%--<span class="badge">
                                    <asp:Literal ID="ltTotal_NL" runat="server" /></span>--%>
                                NORTH LUZON
                        </asp:LinkButton>
                        <asp:ListView ID="lvNL" runat="server" OnItemCommand="GetLocation">
                            <ItemTemplate>
                                <asp:LinkButton ID="btnSelect" runat="server" CssClass="list-group-item" CommandName="getlocation">
                                    <asp:Literal ID="ltDistrict" runat="server" Text='<%# Eval("district") %>' />
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:ListView>
                        <asp:LinkButton ID="region_CSL" runat="server" CssClass="list-group-item active" OnClick="region_CSL_Click">
                                <%--<span class="badge">
                                    <asp:Literal ID="ltTotal_CSL" runat="server" /></span>--%>
                                CENTRAL NORTH LUZON
                        </asp:LinkButton>
                        <asp:ListView ID="lvCSL" runat="server" OnItemCommand="GetLocation">
                            <ItemTemplate>
                                <asp:LinkButton ID="btnSelect" runat="server" CssClass="list-group-item" CommandName="getlocation">
                                    <asp:Literal ID="ltDistrict" runat="server" Text='<%# Eval("district") %>' />
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:ListView>
                        <asp:LinkButton ID="region_MM" runat="server" CssClass="list-group-item active" OnClick="region_MM_Click">
                                <%--<span class="badge">
                                    <asp:Literal ID="ltTotal_MM" runat="server" /></span>--%>
                                METRO MANILA
                        </asp:LinkButton>
                        <asp:ListView ID="lvMM" runat="server" OnItemCommand="GetLocation">
                            <ItemTemplate>
                                <asp:LinkButton ID="btnSelect" runat="server" CssClass="list-group-item" CommandName="getlocation">
                                    <asp:Literal ID="ltDistrict" runat="server" Text='<%# Eval("district") %>' />
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:ListView>
                        <asp:LinkButton ID="region_V" runat="server" CssClass="list-group-item active" OnClick="region_V_Click">
                                <%--<span class="badge">
                                    <asp:Literal ID="ltTotal_V" runat="server" /></span>--%>
                                VISAYAS
                        </asp:LinkButton>
                        <asp:ListView ID="lvV" runat="server" OnItemCommand="GetLocation">
                            <ItemTemplate>
                                <asp:LinkButton ID="btnSelect" runat="server" CssClass="list-group-item" CommandName="getlocation">
                                    <asp:Literal ID="ltDistrict" runat="server" Text='<%# Eval("district") %>' />
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:ListView>
                        <asp:LinkButton ID="region_M" runat="server" CssClass="list-group-item active" OnClick="region_M_Click">
                                <%--<span class="badge">
                                    <asp:Literal ID="ltTotal_M" runat="server" /></span>--%>
                                MINDANAO
                        </asp:LinkButton>
                        <asp:ListView ID="lvM" runat="server" OnItemCommand="GetLocation">
                            <ItemTemplate>
                                <asp:LinkButton ID="btnSelect" runat="server" CssClass="list-group-item" CommandName="getlocation">
                                    <asp:Literal ID="ltDistrict" runat="server" Text='<%# Eval("district") %>' />
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:ListView>
                    </div>
                </div>
                <div class="col-lg-9 col-xs-8">
                    <div class="panel-group" id="accordion">
                        <asp:Image ID="imgRegion" runat="server" CssClass="img-responsive center-block" />
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
                                                <li><i class="fa fa-map-marker fa-fw" title="District"></i>
                                                    <asp:Literal ID="ltDistrict" runat="server" Text='<%# Eval("district") %>' /></li>
                                                <li><i class="fa fa-home fa-fw" title="Address"></i><%# Eval("address") %></li>
                                                <li><i class="fa fa-user fa-fw" title="Head Pastor"></i><%# Eval("head_pastor") %></li>
                                                <li><i class="fa fa-mobile fa-fw" title="Contact Number/s"></i><%# Eval("mobile_no") %></li>
                                                <li><i class="fa fa-envelope fa-fw" title="Email Address"></i><a href='mailto:<%# Eval("email") %>'><%# Eval("email") %></a></li>
                                            </ul>
                                            <asp:Panel ID="pnlMap" runat="server" Visible='<%# Eval("latitude").ToString() == "" ? false : true %>'>
                                                <div id="map-<%# Eval("slug") %>" class="map-style"></div>
                                                <script>
                                                    var map;
                                                    function initMap() {
                                                        map = new google.maps.Map(document.getElementById('map-<%# Eval("slug") %>'), {
                                                    center: { lat: <%# Eval("latitude") %>, lng: <%# Eval("longitude") %>},
                                                            zoom: 8
                                                        });
                                                    }
                                                </script>
                                            </asp:Panel>

                                        </div>
                                    </div>
                                </div>
                            </ItemTemplate>
                            <EmptyDataTemplate>
                                <div class="panel panel-default well">
                                    <div class="help-block"></div>
                                        <h3 class="text-center">No records found. Please try again.</h3>
                                    <div class="help-block"></div>
                                </div>
                            </EmptyDataTemplate>
                        </asp:ListView>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:UpdateProgress runat="server" ID="update_progress">
            <ProgressTemplate>
                <img src="images/loading.gif" />
            </ProgressTemplate>
        </asp:UpdateProgress>
    </form>
    <script src="https://maps.googleapis.com/maps/api/js?key=AIzaSyCyXB2yNj-FKBLVi4gZxEDZArg8V7VKdWE&callback=loadMap"
        async defer></script>
</body>
</html>
