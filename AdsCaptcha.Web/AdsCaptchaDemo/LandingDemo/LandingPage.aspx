<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="LandingPage.aspx.cs" Inherits="LandingDemo_LandingPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
 <script src="http://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.6.2.min.js" type="text/javascript"></script>
<script type="text/javascript">

    $(document).ready(function () {

        $("#<%=btnSend.ClientID %>").bind("click", function (e) {

            var sMailTo = "mailto:";
            var sBody = window.location.href ;
            sMailTo += escape("") + "?subject=" + escape("Inqwise demo: <%=DemoUrl %>") + "&body=" + escape(sBody);
            window.location = sMailTo;
            //$('<a href="' + sMailTo + '">click</a>').appendTo('body').click().remove();

        });
    });
</script>
</asp:Content>
<asp:Content ID="TitleContent" runat="server" ContentPlaceHolderID="TitleHolder">
Demo: <asp:Literal ID="lblHeader" runat="server"></asp:Literal>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
<div class="description">
    <asp:Button ID="btnEdit" runat="server" Text="Edit this demo"   
        CssClass="btn" onclick="btnEdit_Click"/>
    <input type="button" class="btn" value="Send to client" id="btnSend" runat="server"/>    
</div>
<div class="description">
    <asp:Label ID="lblResult" runat="server" EnableViewState="False" 
        Visible="False" Font-Bold="True" Font-Size="20px"></asp:Label>
        </div>
    <h4 style="margin-left: 126px;">Sample form</h4>
    <div class="description" style="margin-top:10px;margin-left: 86px;">
        <div style="float:left;color:#5B5757; font-family:'oswaldregular';margin:7px 7px 7px 39px;">eMail: </div>
        <div style="float:left"><input name="demail" class="InputField" value="" type="text" size="30" style="width:310px;" /></div>
        <div style="float:left;color:#5B5757; font-family:'oswaldregular';margin:7px;font-size:13px;display:none;">We respest your privacy</div>

        <div style="clear:both;width:100%;height:1px;"></div>

        <div style="float:left;color:#5B5757; font-family:'oswaldregular';margin:7px;">username: </div>
        <div style="float:left"><input name="dname" class="InputField" value="" type="text" size="30" style="width:310px;" /></div>
        <div style="float:left;color:#5B5757; font-family:'oswaldregular';margin:7px;font-size:13px;display:none;">No spaces please</div>
        
        <!--div style="clear:both;width:100%;height:1px;"></div>
        
        <div style="float:left;color:#5B5757; font-family:'oswaldregular';margin:7px 7px 7px 8px;">password: </div>
        <div style="float:left"><input name="dpassword" class="InputField" value="" type="text" size="30" style="width:310px;" /></div>
        <div style="float:left;color:#5B5757; font-family:'oswaldregular';margin:7px;font-size:13px;">At least 4 characters</div-->


     </div>
     <br /><br />
      <table>
        <tr>
            <td style="width:110px;" valign="top">  
            <img src="../styles/Inqwise/images/recaptcha.png" alt="recaptcha" style="margin: 0;" />
            <div style="width:68px;margin: -20px 0 0 50px; text-align:center;color: #5B5757;font-family: 'oswaldregular';font-size: 27px;font-weight: 400;">Are You a Robot?</div>
            </td>
             <td  style="width:400px;"><div id="sliderholder">
          <script type="text/javascript" src="../Slider/Get.ashx?w=<%=DemoWidth%>&h=<%=DemoHeight%>&demo=<%=DemoUrl%>"></script>
          </div></td>
                        
        </tr>
        <tr>
          <td align="left" colspan="2"><asp:Button ID="btnSubmit" runat="server" Text="" style="background: url('../styles/Inqwise/images/singup_btn.jpg') repeat scroll 0% 0% transparent; width: 272px; height: 72px; display: block; margin: 0 0 20px 200px; cursor:pointer;" 
                  onclick="btnSubmit_Click" /></td>
        </tr>
     </table>
      

         <br />
          
          <br />
</asp:Content>
  
 