<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Print.aspx.cs" Inherits="Inqwise.AdsCaptcha.Advertiser.Print" %>
<%@ Register Assembly="ComponentArt.Web.UI" Namespace="ComponentArt.Web.UI" TagPrefix="ComponentArt" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Print billing invoice</title>
     <script src="//ajax.googleapis.com/ajax/libs/jquery/1.6.2/jquery.min.js" type="text/javascript"></script>
                    <script type="text/javascript">
                    var sortedDataField = '';
                    var sortedDescending = false;
                    function Grid_onSortChange(sender, e) {
                        sortedDataField = e.get_column().get_dataField();
                        sortedDescending = e.get_descending();
                    }
                    function ViewPaymentDetails(id) {
                        window.open('ViewPaymentDetails.aspx?PaymentId=' + id + '','Payment Details','width=600,height=400');
                    }
                    $(document).ready(function() {
                       window.print();
                    });
                    
                </script>
    <style type="text/css">
        .logo {left:32px;height:80px;width:260px;background-image:url(../images/logo.jpg);background-repeat:no-repeat;cursor:hand;cursor:pointer;}
        table td{vertical-align:top;padding:10px;}
        /*** GRID ***/
.grid {width:100%;font-size:12px;border-style:solid;border-width:1px;color:#3b3b3b;background:#fff;}
.headingCell {width:auto;padding:0 3px 0 3px;font-weight:bold;cursor:default;line-height:30px;border-bottom-style:solid;border-bottom-width:1px;}
.headingCellText {}
.sortable {cursor:hand;cursor:pointer;}
.headingCellHover {}
.headingCellActive {}
.dataCell {background:#fff;padding:0px 3px 0px 3px;height:24px;line-height:24px;cursor:default;border-bottom-style:solid;border-bottom-width:1px;}
.dataCell a {color:#3b3b3b;text-decoration:underline;}
.dataCell a:hover {color:#3b3b3b;text-decoration:underline;}
.row {background:#fff;cursor:default;}
.alternatingRow {cursor:default;}
.alternatingRow td.dataCell {/*background-color:#f7f9f4;*/}
.hoverRow {cursor:default;}
.hoverRow td.dataCell {background:#fbfbfb;}
.summaryRow {font-weight:bold;}
.selectedRow {cursor:default;}
.selectedRow td.dataCell {background:#f6f6f6;}
.scrollBar {background-image:url(../images/table/scroller_bg.gif);}
.scrollGrip {background-image:url(../images/table/scroll_gripBg.gif);}
.scrollPopup {background-color:#fff;border:1px solid #666;border-right-width:2px;border-bottom-width:2px;height:23px;}
.action a {color:#3b3b3b;text-decoration:underline;}
.action a:hover {color:#3b3b3b;text-decoration:underline;}
/* filters */
.filter {text-decoration:none;font-weight:normal;}
.filterSelected {text-decoration:none;font-weight:normal;color:#000;}

/* information */
.noFound {width:100;font-size:14px;font-weight:bold;color:Red;text-align:center;}
.gridActions {float:right;text-align:right;color:#666;margin:5px;}
.gridActions a {text-decoration:none;font-weight:bold;}
.gridActions a:hover {text-decoration:none;color:#524743}
.gridRemark {float:left;text-align:left;color:#666;margin:5px;}
.Explanation {font-size:10px;color:#aaa;line-height:14px;}

/* summary table */
.summaryTable {font-size:13px;}
.summaryTable th {color:#fff;font-weight:bold;padding:8px;border-right:solid 1px #fff;text-align:center;}
.summaryTable th:last-child {border-right-style:none;}
.summaryTable td {color:#000;padding:8px;border-right:solid 1px #e5e5e5;border-bottom:solid 1px #e5e5e5;text-align:center;}
.summaryTable td:last-child {border-right-style:none;}
.summaryTable td a,
.summaryTable td a:hover {color:#0000ff;}

/* statuses */
.Running {color:#347c17;}
.Paused {color:#717d7d;}
.Pending {color:#ff8040;}
.Rejected {color:#f62817;}
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
      
        
        <table>
            <tr>
                <td><div class="logo"></div></td>
            </tr>
             <tr>
                <td> <table>
                <tr>
                    <td style="border-width:0 1px 0 0; border-style:solid;">
                        <table>
                            <tr>
                                <td>
                                    AdsCaptcha<sup>TM</sup><br />
                                    Ha'omanim 10, Tel Aviv<br />
                                    Israel <br />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Literal ID="lblAdvetiserAddress" runat="server"></asp:Literal>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td>
                        <table>
                           <tr>
                                <th>Invoice <br /> <asp:Literal ID="lblInvoiceDates" runat="server"></asp:Literal> </th>
                           </tr>
                           <tr>
                                <td>
                                    <table>
                                        <tr>
                                            <td>InvoiceDate</td>
                                            <td><asp:Literal ID="lblInvoiceDate" runat="server"></asp:Literal></td>
                                        </tr>
                                    </table>
                                </td>
                           </tr>
                        </table>
                    </td>
                </tr>
            </table></td>
            </tr>
             <tr>
                <td>
                
                <ComponentArt:Grid ID="Grid" 
                    RunningMode="Client"
                    AllowPaging="true"
                    CssClass="grid"
                    DataAreaCssClass="gridData"
                    FooterCssClass="gridFooter"
                    ImagesBaseUrl="../images/table"
                    ShowHeader="false"
                    ShowFooter="false"
                    AllowColumnResizing="false"
                    AllowMultipleSelect="false"
                    EmptyGridText="No transactions found."
                    PageSize="20"
                    ScrollBar="Auto"
                    ScrollTopBottomImagesEnabled="true"
                    ScrollTopBottomImageHeight="2"
                    ScrollTopBottomImageWidth="16"
                    ScrollImagesFolderUrl="../images/table"
                    ScrollButtonWidth="16"
                    ScrollButtonHeight="17"
                    ScrollBarCssClass="scrollBar"
                    ScrollGripCssClass="scrollGrip"
                    ScrollBarWidth="16"
                    LoadingPanelClientTemplateId="LoadingFeedbackTemplate"
                    LoadingPanelPosition="MiddleCenter"
                    runat="server"
                    ClientIDMode="AutoID"
                    >
                    <ClientEvents>
                        <SortChange eventhandler="Grid_onSortChange" />
                    </ClientEvents>
                    <Levels>
                        <ComponentArt:GridLevel 
                            DataMember="Billings"
                            DataKeyField="Billing_Date"
                            TableHeadingCssClass="gridHeader"
                            HeadingCellCssClass="headingCell"
                            HeadingRowCssClass="headingRow"
                            HeadingTextCssClass="headingCellText"
                            HeadingCellHoverCssClass="headingCellHover"
                            HeadingCellActiveCssClass="headingCellActive"                    
                            DataCellCssClass="dataCell"
                            RowCssClass="row" AlternatingRowCssClass="alternatingRow"
                            HoverRowCssClass="hoverRow"
                            SelectedRowCssClass="row"
                            ShowFooterRow="false"
                            AllowSorting="True"
                            SortAscendingImageUrl="sortAscAdv.gif"
                            SortDescendingImageUrl="sortDescAdv.gif"
                            SortImageWidth="8"
                            SortImageHeight="7"
                            >
                            <Columns>
                                <ComponentArt:GridColumn DataField="Advertiser_Id" Visible="False" />
                                <ComponentArt:GridColumn DataField="Payment_Id" Visible="False" />
                                <ComponentArt:GridColumn DataField="Date" Align="Center" HeadingCellClientTemplateId="HeadingSortableCellTemplate" AllowSorting="True" FormatString="MMM dd, yyyy" FixedWidth="true" Width="100" />
                                <ComponentArt:GridColumn DataField="Description" HeadingText="Description" Align="Left" AllowSorting="False" DataCellClientTemplateId="DescriptionTemplate" />
                                <ComponentArt:GridColumn DataField="Type_Id" Visible="False" />
                                <ComponentArt:GridColumn DataField="Type" Visible="False" HeadingText="Transaction Type" Align="Center" HeadingCellClientTemplateId="HeadingSortableCellTemplate" AllowSorting="True" />
                                <ComponentArt:GridColumn DataField="Charge" HeadingText="Charges" Align="Center" HeadingCellClientTemplateId="HeadingSortableCellTemplate" AllowSorting="True" DefaultSortDirection="Descending" DataCellClientTemplateId="CurrencyTemplate" FixedWidth="true" Width="100" />
                                <ComponentArt:GridColumn DataField="Credit" HeadingText="Credits" Align="Center" HeadingCellClientTemplateId="HeadingSortableCellTemplate" AllowSorting="True" DefaultSortDirection="Descending" DataCellClientTemplateId="CurrencyTemplate" FixedWidth="true" Width="100" />
                                <ComponentArt:GridColumn DataField="Balance" HeadingText="Balance" Align="Center" HeadingCellClientTemplateId="HeadingSortableCellTemplate" AllowSorting="True" DefaultSortDirection="Descending" DataCellClientTemplateId="CurrencyTemplate" FixedWidth="true" Width="100" />
                            </Columns>
                        </ComponentArt:GridLevel>
                    </Levels>
                    
                    <ClientTemplates>
                        <ComponentArt:ClientTemplate ID="HeadingSortableCellTemplate">
                            <table width="100%" cellspacing="0" cellpadding="0" border="0">
                                <tbody>
                                    <tr valign="middle">
                                        <td align="left" class="headingCellText ## if (DataItem.get_allowSorting()) 'sortable' ##" style="white-space:nowrap; text-align:## DataItem.get_align() ##;">
                                            ##DataItem.get_headingText()####if (sortedDataField == DataItem.get_dataField()) '<img style="padding-left:3px;" width="' + Grid.get_levels()[0].SortImageWidth + '" height="' + Grid.get_levels()[0].SortImageHeight + '" src="' + (sortedDescending ? Grid.get_levels()[0].SortDescendingImageUrl : Grid.get_levels()[0].SortAscendingImageUrl) + '" alt="Sort" />';##
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </ComponentArt:ClientTemplate>
                        <ComponentArt:ClientTemplate ID="DescriptionTemplate">
                            ## DataItem.getCurrentMember().get_value() ##
                            ## if (DataItem.GetMember("Payment_Id").Value != null) 
                                '<span class="Explanation">(<a href="javascript:ViewPaymentDetails(' + DataItem.GetMember("Payment_Id").Value + ');">View Details</a>)</span>'
                            ##
                        </ComponentArt:ClientTemplate>
                        <ComponentArt:ClientTemplate ID="CurrencyTemplate">
                            ## if (DataItem.getCurrentMember().get_value() != null) '$'####DataItem.getCurrentMember().get_value() ##
                        </ComponentArt:ClientTemplate>
                        <ComponentArt:ClientTemplate ID="LoadingFeedbackTemplate">
                            <table cellspacing="0" cellpadding="0" border="0">
                                <tr>
                                    <td style="font-size:10px;">Loading...&nbsp;</td>
                                    <td><img src="../images/table/spinner.gif" width="16" height="16" border="0" alt="Loading" /></td>
                                </tr>
                            </table>
                        </ComponentArt:ClientTemplate>
                    </ClientTemplates>
                </ComponentArt:Grid>
                </td>
            </tr>
        </table>
        
    </div>
    </form>
</body>
</html>
