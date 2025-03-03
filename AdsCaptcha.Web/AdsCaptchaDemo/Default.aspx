<%@ Page Title="Inqwise demo" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
<style type="text/css">
.descriptionslogan
{
		font-family:"bitterregular";
	font-size:18px;
	height:auto;
	padding-bottom:10px;
	line-height:27px;
	width:1050px;
	margin: 0 auto;
}
.CAPTCHAs
{
	color:gray;
	font-family:'oswaldregular';
	font-size:14px;
	font-weight:400;
}
</style>
</asp:Content>
<asp:Content ID="TitleContent" runat="server" ContentPlaceHolderID="TitleHolder">
Home
</asp:Content>
<asp:Content ID="SloganHolder" runat="server" ContentPlaceHolderID="SloganHolder">
     <div class="descriptionslogan">
                <center>
                    <br>
                         <div class="status-number">
                        Inqwise is breakthrough advertising.
                    </div><!-- status-number -->

                    <div class="CAPTCHAs">
                        The most enjoyable, effective and powerful ad messaging on the web today.
                    </div><!-- CAPTCHAs -->
                </center><br />
                In a world of Banner Blindness and constantly decreasing
 CTRs Inqwise’s provides breakthrough solution set for advertisers to 
validate user’s engagement with gam-ish innovative way.<br />
</div>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h4>
        Inqwise demos
    </h4>
     <br />
    <div class="description">
        Select Inqwise Demo on the right side menu.
    </div>
    <br />
    <br />
    <br />
    <div class="description">
    <div id="captchademo">
                    <img src="Styles/Inqwise/images/captha.jpg" width="200" height="210" />

                    <ul style="padding-top:30px;">
                        <li style="list-style: none"><strong>Slider Units:</strong></li>

                        <li>Lift your brand with full user engagement</li>

                        <li>Combine clicks and ad performance</li>

                        <li>Unifies social interaction with liking your brand</li>
                    </ul>
                </div>
                </div>
    <br />
    <br />
    <br />
</asp:Content>
