<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Alerts.aspx.cs" Inherits="Ecan.WaterUseAlerts.Web.Alerts"%>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">

    <link href="~/../Content/jquery-ui-1.8.2-redmond.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">

        $(document).ready(function() 
        {
            $("#gvAllAlerts").tablesorter({
                widgets: ['zebra'],
                sortList: [[0, 1]],
                headers: {
                    4: { sorter: false },
                    5: { sorter: false }
                         }
            })
            $("#txtRmoaStartDate").datepicker({ dateFormat: "dd/M/yy" }).val();
            $("#txtStatsStartDate").datepicker({ dateFormat: "dd/M/yy" }).val();
            $("#txtStartDate").datepicker({ dateFormat: "dd/M/yy" }).val();
            $("#txtEndDate").datepicker({ dateFormat: "dd/M/yy" }).val();

            // show 'Please wait...' spinner during button postbacks
            function ShowProgress() {
                setTimeout(function () {
                    var modal = $('<div />');
                    modal.addClass("modal");
                    $('body').append(modal);
                    var loading = $(".loading");
                    loading.show();
                    var top = Math.max($(window).height() / 2 - loading[0].offsetHeight / 2, 0);
                    var left = Math.max($(window).width() / 2 - loading[0].offsetWidth / 2, 0);
                    loading.css({ top: top, left: left });
                }, 200);
            }

            $('form').live("submit", function () {
                ShowProgress();
            });

            // do this last. if it's placed first, the VR grids don't get zerba'ed
            $("#tabs").tabs();

            //re-show the tab they were on when they did the post-back
            var tabNo = $("#currTabNo").val();
            $("#tabs").tabs('select', parseInt(tabNo));
        });

    </script>

     <style type="text/css">
        .modal
        {
            position: fixed;
            top: 0;
            left: 0;
            background-color: White;
            z-index: 99;
            opacity: 0.3;
            filter: alpha(opacity=30);
            -moz-opacity: 0.3;
            min-height: 100%;
            width: 100%;
        }
        .loading
        {
            font-family: Arial;
            font-size: 10pt;
            border: 3px solid #67CFF5;
            width: 300px;
            height: 120px;
            display: none;
            position: fixed;
            background-color: White;
            z-index: 999;
            text-align:center;
            margin-top:0px;
            margin-bottom:0px;
            padding:0px;
        }

    </style>

    <h2>Water Use Alerts Manager</h2>

    <div id="tabs">
          <fieldset>
            <legend>Processing Info</legend>
                <asp:Label ID="infoMessages" runat="server" EnableViewState="false"></asp:Label>
        </fieldset>

        <fieldset>
            <legend>Processing Errors</legend>
                <asp:Label ID="errorMessages" runat="server" EnableViewState="false"> </asp:Label>
        </fieldset>

        <%--used to maintain the curent tab during a postback--%>
        <div style="display: none;">
            <asp:TextBox ID="currTabNo" runat="server" ClientIDMode="Static" EnableViewState="false" />
        </div>

        <ul>
            <li><a href="#divRmoaAlerts">RMOA Alerts</a></li>
            <li><a href="#divAllAlerts">All Alerts</a></li>
            <li><a href="#divStatistics">Stats</a></li>
        </ul>

        <div class="loading">
            <p>
                Loading.  Please wait.....
            </p>
            <img src="~/../Images/loader.gif" alt="" />
        </div>

        <div id="divRmoaAlerts">

            <fieldset id="fsRmoaAlerts">
                <legend>Water Use Alerts</legend>

                Start Date&nbsp; &nbsp;
                <asp:TextBox ID="txtRmoaStartDate" runat="server" ClientIDMode="Static" Width="100" EnableViewState="false" OnTextChanged="txtRmoaStartDate_OnTextChanged"></asp:TextBox> &nbsp; &nbsp;

                <asp:CheckBox ID="cbxMyAlerts" runat="server" CausesValidation="false" OnCheckedChanged="cbxMyAlerts_OnCheckedChanged"/> 
                My Alerts&nbsp; &nbsp;
                <asp:TextBox ID="txtID" runat="server" Visible="false"/>

                <asp:Button ID="btnRefreshRmoa" runat="server" CausesValidation="False" CommandName="RefreshRmoaAlerts" Text="Refresh" OnClick="btnRefreshRmoa_Click"/>
                <asp:GridView ID="gvRmoaAlerts" runat="server" AutoGenerateColumns="false" ClientIDMode="Static"
                    OnRowDataBound="gvRmoaAlerts_RowDataBound" OnRowCommand="gvRmoaAlerts_RowCommand" OnSelectedIndexChanged="gvRmoaAlerts_SelectedIndexChanged"
                    OnRowCancelingEdit="gvRmoaAlerts_RowCancelingEdit" OnRowEditing="gvRmoaAlerts_RowEditing" OnRowUpdating="gvRmoaAlerts_RowUpdating">

                    <Columns>
                        <asp:BoundField DataField="RunDate" HeaderText="Run Date" DataFormatString="{0:dd/MMM/yyyy}" ReadOnly="true"/>
                        <asp:TemplateField ItemStyle-Width="80" HeaderText="Consent No Water Group">
                            <ItemTemplate><%# Eval("ConsentID")%></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-Width="200" HeaderText="Meters" ItemStyle-Wrap="true">
                            <ItemTemplate><%# Eval("MetersTruncated")%> </ItemTemplate>
                        </asp:TemplateField>                        
                        <asp:BoundField DataField="DisplayableType" HeaderText="Type" ReadOnly="true"/>
                        <asp:BoundField DataField="Limit" HeaderText="Limit" ReadOnly="true"/>
                        <asp:BoundField DataField="MaxUse" HeaderText="Max Use" ReadOnly="true"/>
                        <asp:TemplateField ItemStyle-Width="100" HeaderText="Status">
                            <ItemTemplate><%# Eval("Status")%></ItemTemplate>
                            <EditItemTemplate> 
                                <asp:DropDownList width="100" ID="ddlRmoaStatus" runat="server" CssClass="dropdownlist" />
                            </EditItemTemplate> 
                        </asp:TemplateField>                        
                        <asp:TemplateField ItemStyle-Width="200" HeaderText="Comments">
                            <ItemTemplate><%# Eval("Comment")%></ItemTemplate>
                            <EditItemTemplate> 
                                <asp:TextBox width="200" ID="txtRmoaComments" runat="server" Text='<%# Bind("Comment") %>' />
                            </EditItemTemplate> 
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Edit" ShowHeader="False" ControlStyle-CssClass="tableButton">  
                            <ItemTemplate>  
                                <asp:LinkButton ID="btn_RmoaEdit" runat="server" Text="Edit" CommandName="Edit" />  
                            </ItemTemplate>  
                            <EditItemTemplate>  
                                <asp:LinkButton ID="btn_RmoaSave" runat="server" Text="Save" CommandName="Update"/>  
                                <asp:LinkButton ID="btn_RmoaCancel" runat="server" Text="Cancel" CommandName="Cancel"/>  
                            </EditItemTemplate>  
                        </asp:TemplateField>  
                        <asp:BoundField DataField="Username" HeaderText="User" ReadOnly="true"/>
                        <asp:BoundField DataField="LastModifiedDate" HeaderText="Last Modified date" DataFormatString="{0:dd/MMM/yyyy}" ReadOnly="true"/>
                        <asp:BoundField DataField="PercentOverLimit" HeaderText="Percent Over Limit" ReadOnly="true"/>
                    </Columns>

                </asp:GridView>
            </fieldset>

        </div>

        
        <div id="divAllAlerts">

            <fieldset id="fsAllAlerts">
                <legend>Water Use Alerts All</legend>

                Start Date&nbsp; &nbsp;
                <asp:TextBox ID="txtStartDate" runat="server" ClientIDMode="Static" Width="100" EnableViewState="false"></asp:TextBox> &nbsp; &nbsp;

                End Date&nbsp; &nbsp;
                <asp:TextBox ID="txtEndDate" runat="server" ClientIDMode="Static" Width="100" EnableViewState="false"></asp:TextBox> &nbsp; &nbsp;
                
                <asp:CheckBox ID="cbxIncludeIgnored" runat="server" CausesValidation="false" OnCheckedChanged="cbxIncludeIgnored_OnCheckedChanged"/>
                Include Ignored&nbsp; &nbsp;
                
                <asp:CheckBox ID="cbkMyAllAlerts" runat="server" CausesValidation="false" OnCheckedChanged="cbkMyAllAlerts_OnCheckedChanged"/>
                My Alerts&nbsp; &nbsp;

                <asp:DropDownList ID="ddlAllConsents" runat="server" CausesValidation="false" Width="100"/>
                Consent&nbsp; &nbsp;

                <asp:Button ID="btnRefreshAll" runat="server" CausesValidation="False" CommandName="RefreshAllAlerts" Text="Refresh" OnClick="btnRefreshAll_Click"/>

                <asp:GridView ID="gvAllAlerts" runat="server" AutoGenerateColumns="False" ClientIDMode="Static"
                    OnRowDataBound="gvAllAlerts_RowDataBound" OnRowCommand="gvAllAlerts_RowCommand" OnSelectedIndexChanged="gvAllAlerts_SelectedIndexChanged" >
                    <Columns>
                        <asp:BoundField DataField="RunDate" HeaderText="Run Date" DataFormatString="{0:dd/MMM/yyyy}" />
                        <asp:BoundField DataField="ConsentID" HeaderText="Consent No/Water Group" />
                        <asp:TemplateField ItemStyle-Width="200" HeaderText="Meters" ItemStyle-Wrap="true">
                            <ItemTemplate><%# Eval("MetersTruncated")%> </ItemTemplate>
                        </asp:TemplateField>                        
                        <asp:BoundField DataField="DisplayableType" HeaderText="Type" ItemStyle-Width="90"/>
                        <asp:BoundField DataField="Limit" HeaderText="Limit" />
                        <asp:BoundField DataField="MaxUse" HeaderText="Max Use" />
                        <asp:BoundField DataField="MaxUseNumeric" HeaderText="Max Use Numeric" />
                        <asp:BoundField DataField="Status" HeaderText="Status" ItemStyle-Width="80"/>
                        <asp:BoundField DataField="LastModifiedDate" HeaderText="Last Modified date" DataFormatString="{0:dd/MMM/yyyy}" />
                        <asp:BoundField DataField="Username" HeaderText="User" />
                        <asp:BoundField DataField="PercentOverLimit" HeaderText="Percent Over Limit" />
                        <asp:BoundField DataField="MaxAvDate" HeaderText="Max Average Date" DataFormatString="{0:dd/MMM/yyyy}" />
                        <asp:BoundField DataField="FirstNCDate" HeaderText="First Non-Compliance Date" DataFormatString="{0:dd/MMM/yyyy}" />
                        <asp:BoundField DataField="LastNCDate" HeaderText="Last Non-Compliance Date" DataFormatString="{0:dd/MMM/yyyy}" />
                        <asp:BoundField DataField="Comment" HeaderText="Comments"/>
                        <asp:BoundField DataField="IgnoreAsText" HeaderText="Ignore"/>
                        <asp:BoundField DataField="IgnoreReason" HeaderText="IgnoreReason"/>

                    </Columns>

                </asp:GridView>
            </fieldset>

        </div>

        
        <div id="divStatistics">

            <fieldset id="fsStatistics">
                <legend>Alert Statistics</legend>

                Start Date&nbsp; &nbsp;
                <asp:TextBox ID="txtStatsStartDate" runat="server" ClientIDMode="Static" Width="100" EnableViewState="false"></asp:TextBox> &nbsp; &nbsp;
                <asp:Button ID="btnRefreshStats" runat="server" CausesValidation="False" Text="Refresh" CommandName="RefreshStatsTab" OnClick="btnRefreshStats_Click"/>

                <asp:GridView ID="gvStatistics" runat="server" AutoGenerateColumns="False" ClientIDMode="Static">
                    <Columns>
                        <asp:BoundField DataField="StatsType" HeaderText="Stats Type" />
                        <asp:BoundField DataField="Count" HeaderText="Count" />
                    </Columns>

                </asp:GridView>
            </fieldset>

        </div>

     </div>

</asp:Content>
