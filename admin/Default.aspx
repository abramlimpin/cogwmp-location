<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="admin_Default" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Administration - Church of God World Missions Philippines - Locations</title>
    <link href="https://bootswatch.com/3/spacelab/bootstrap.min.css" rel="stylesheet" />
    <link href="https://stackpath.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css" rel="stylesheet" />
    <link href="css/style.css" rel="stylesheet" />
    <script src="https://code.jquery.com/jquery-3.3.1.min.js"></script>
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
    <link href="//cdn.datatables.net/1.10.16/css/jquery.dataTables.min.css" rel="stylesheet" />
    <script src="//cdn.datatables.net/1.10.16/js/jquery.dataTables.min.js"></script>
</head>
<body>
    <form id="frmLocations" class="form-horizontal" runat="server">
        <div class="col-lg-3">
            <h2 class="text-center">Manage Locations</h2>
            <div class="well clearfix">
                <asp:ScriptManager runat="server" />
                <div id="status" runat="server" class="form-group" visible="false">
                    <label class="control-label col-lg-4">Status</label>
                    <div class="col-lg-8">
                        <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control">
                            <asp:ListItem>Active</asp:ListItem>
                            <asp:ListItem>Inactive</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <asp:UpdatePanel ID="upDistricts" runat="server">
                    <ContentTemplate>
                        <div class="form-group">
                            <label class="control-label col-lg-4">Region</label>
                            <asp:Literal ID="ltID" runat="server" Visible="false" />
                            <div class="col-lg-8">
                                <asp:DropDownList ID="ddlRegions" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlRegions_SelectedIndexChanged" required>
                                    <asp:ListItem Value="">Select one...</asp:ListItem>
                                    <asp:ListItem>North Luzon</asp:ListItem>
                                    <asp:ListItem>Metro Manila</asp:ListItem>
                                    <asp:ListItem>Central South Luzon</asp:ListItem>
                                    <asp:ListItem>Visayas</asp:ListItem>
                                    <asp:ListItem>Mindanao</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="control-label col-lg-4">District</label>
                            <div class="col-lg-8">
                                <asp:DropDownList ID="ddlDistrict" runat="server" CssClass="form-control" required>
                                    <asp:ListItem Value="">Select a region...</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="control-label col-lg-4">Church Branch</label>
                            <div class="col-lg-8">
                                <div class="input-group">
                                    <span class="input-group-addon">COG</span>
                                    <asp:TextBox ID="txtBranch" runat="server" MaxLength="50" CssClass="form-control text-uppercase" AutoPostBack="true" OnTextChanged="txtBranch_TextChanged" required />
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="control-label col-lg-4">Slug</label>
                            <div class="col-lg-8">
                                <asp:TextBox ID="txtSlug" runat="server" MaxLength="50" CssClass="form-control" disabled />
                                <asp:Literal ID="ltSlug" runat="server" Visible="false" />
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <div class="form-group">
                    <label class="control-label col-lg-4">Head Pastor</label>
                    <div class="col-lg-8">
                        <asp:TextBox ID="txtPastor" runat="server" MaxLength="100" CssClass="form-control" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="control-label col-lg-4">Mobile No.</label>
                    <div class="col-lg-8">
                        <asp:TextBox ID="txtMobile" runat="server" MaxLength="100" TextMode="MultiLine" CssClass="form-control" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="control-label col-lg-4">Email Address</label>
                    <div class="col-lg-8">
                        <asp:TextBox ID="txtEmail" runat="server" MaxLength="100" type="email" CssClass="form-control" />
                    </div>
                </div>
                <div class="form-group">
                    <label class="control-label col-lg-4">Address</label>
                    <div class="col-lg-8">
                        <asp:TextBox ID="txtAddress" runat="server" MaxLength="300" TextMode="MultiLine" CssClass="form-control" required />
                    </div>
                </div>
                <div class="form-group">
                    <div class="col-lg-offset-4 col-lg-8">
                        <asp:Button ID="btnAdd" runat="server" CssClass="btn btn-success" Text="Add" OnClick="btnAdd_Click" OnClientClick='return confirm("Are you sure?");' />
                        <asp:Button ID="btnUpdate" runat="server" CssClass="btn btn-success" Text="Update" Visible="false" OnClick="btnUpdate_Click" OnClientClick='return confirm("Are you sure?");' />
                        <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-danger" Text="Cancel" OnClick="btnCancel_Click" formnovalidate />
                    </div>
                </div>
            </div>
        </div>
        <div class="col-lg-9">
            <div id="update" runat="server" class="alert alert-success" visible="false">
                Record updated.
            </div>
            <table id="tblLocations" class="table">
                <thead>
                    <th>Church Branch</th>
                    <th>Region</th>
                    <th>Address</th>
                    <th>Head Pastor</th>
                    <th>Status</th>
                    <th>Date Added</th>
                    <th>Date Modified</th>
                    <th>Actions</th>
                </thead>
                <tbody>
                    <asp:ListView ID="locations" runat="server" OnItemCommand="locations_ItemCommand">
                        <ItemTemplate>
                            <tr>
                                <td>
                                    <asp:Literal ID="id" runat="server" Text='<%# Eval("id") %>' Visible="false" />
                                    <%# Eval("church_branch") %></td>
                                <td><%# Eval("region") %></td>
                                <td><%# Eval("address") %></td>
                                <td><%# Eval("head_pastor") %></td>
                                <td><%# Eval("status") %></td>
                                <td title='<%# Eval("date_added", "{0: MMMM dd, yyyy HH:mm tt}") %>'><%# Eval("date_added", "{0: MM/dd/yy}") %></td>
                                <td title='<%# Eval("date_modified", "{0: MMMM dd, yyyy HH:mm tt}") %>'><%# Eval("date_modified", "{0: MM/dd/yy}") %></td>
                                <td>
                                    <asp:LinkButton ID="btnUpdate" runat="server" CssClass="btn btn-xs btn-info" CommandName="selectitem">
                                            <i class="fa fa-edit"></i>
                                    </asp:LinkButton>
                                    <asp:LinkButton ID="btnDelete" runat="server" CssClass="btn btn-xs btn-danger" CommandName="removeitem" OnClientClick='return confirm("Archive record?");'>
                                            <i class="fa fa-trash"></i>
                                    </asp:LinkButton>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:ListView>
                </tbody>
            </table>
        </div>
    </form>
    <script>
        $(document).ready(function () {
            $('#tblLocations').DataTable(
                {
                    "iDisplayLength": 20
                }
            );
        });
    </script>
</body>
</html>
