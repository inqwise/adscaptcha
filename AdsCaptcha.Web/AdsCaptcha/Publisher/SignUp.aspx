<%@ Page EnableViewStateMac="false" Title="Site Owners, Sign Up Step 1 | Inqwise" Language="C#" MasterPageFile="~/OldAdsCaptcha.Master" AutoEventWireup="true" CodeFile="SignUp.aspx.cs" Inherits="Inqwise.AdsCaptcha.Publisher.SignUp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="AjaxControlToolkit" %>
<%@ Register TagPrefix="paymentPreferences" TagName="bank" Src="~/Controls/PaymentPreferences/PaymentPreferencesBank.ascx" %>
<%@ Register TagPrefix="paymentPreferences" TagName="check" Src="~/Controls/PaymentPreferences/PaymentPreferencesCheck.ascx" %>
<%@ Register TagPrefix="paymentPreferences" TagName="paypal" Src="~/Controls/PaymentPreferences/PaymentPreferencesPayPal.ascx" %>
<%@ Register TagPrefix="paymentPreferences" TagName="later" Src="~/Controls/PaymentPreferences/PaymentPreferencesLater.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server"> 
 <style type="text/css">
   .InputField, .TextareaField
   {
   border: 2px solid #DEDEDD;
    border-radius: 3px 3px 3px 3px;
    color: #333333;
    font: 20px Arial,Helvetica,sans-serif;
    padding: 6px;
    width: 200px;
   }
   
   .SelectField
   {
   	width: 215px;
   }
   
   .ButtonHolder
   {
   	margin-top:30px;
   }
   
   .ButtonHolder a
   {
   	color:#FFFFFF;
   }
   
   .container {
       /*background: -moz-radial-gradient(center center , ellipse farthest-corner, #FFFFFF 0%, #EAEAEA 100%) repeat scroll 0 0 transparent;
    border: 2px solid #FFFFFF;
    box-shadow: 0 5px 22px #BBBBBB;
    height: 370px;*/
    margin: 0px auto;
    overflow: hidden;
    padding: 50px 0  0 120px;
    position: relative;
    width: auto;
}

#content h4
{
	background-position: left 4px !important;
    margin-bottom: 30px;
    line-height: 1;
}
   </style>

	<script src="//ajax.googleapis.com/ajax/libs/jquery/1.7.2/jquery.min.js" type="text/javascript"></script>
    <script src="//ajax.googleapis.com/ajax/libs/jqueryui/1.7.3/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../js/jquery.dropshadow.js" type="text/javascript"></script>
    <script src="../js/jquery.qtip-1.0.0-rc3.min.js" type="text/javascript"></script>
    <script src="../js/common.js" type="text/javascript"></script>
    <script src="../js/calander.js" type="text/javascript"></script>
    <script src="../js/pwdstrength.js" type="text/javascript"></script>

</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="BodyContent" runat="server">
id="body" class="page"
</asp:Content> 
<asp:Content ID="Content2" ContentPlaceHolderID="LoginContent" runat="server"> 
    <li>|
    </li>
    <li><a id="loginBox" runat="server" href="~/Publisher/Login.aspx" class="login-window">SIGN IN</a></li>
    <li>|
    </li>
    <li><a id="signBox" href="SignUp.aspx">SIGN UP</a></li>
</asp:Content>
<asp:content ContentPlaceHolderID="MainContent" runat="server">


   <div id="boxes"></div><!--end boxes-->

    <div id="status">
        <div class="inner-status">
            <div class="status-box">
                <h2>Sign up</h2><!-- <div class="status-number">144,453,448</div> -->
            </div><!--end status-box-->
        </div><!--end inner-status-->
    </div><!--end status-->

    <div id="content" class="container">
        <div class="inner-content">
            <br>

        <asp:ScriptManager ID="ScriptManager" runat="server" EnablePartialRendering="true" />
        <asp:UpdatePanel ID="UpdatePanel" runat="server">
            <ContentTemplate>
        
                        <h4>Account Information</h4>

            <div class="description">
            <div class="Section">
                <table>
                    <tr>
                        <td class="FieldHeader">E-mail:*</td>
                        <td>
                            <asp:textbox ID="textEmail" runat="server" CssClass="InputField" MaxLength="100" ValidationGroup="Form" />
                            <asp:RegularExpressionValidator ID="validatorEmail1"
                                runat="server"
                                ControlToValidate="textEmail" ValidationGroup="Form" SetFocusOnError="true" 
                                ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                CssClass="ValidationMessage" ErrorMessage="* Not a valid e-mail"
                                Display="dynamic" />
                            <asp:RequiredFieldValidator ID="validatorEmail2"
                                runat="server" ControlToValidate="textEmail" ValidationGroup="Form"
                                CssClass="ValidationMessage" ErrorMessage="* Required" Display="dynamic" SetFocusOnError="true" />
                            <asp:CustomValidator ID="validatorEmail3"
                                runat="server" ControlToValidate="textEmail" Display="Dynamic"
                                OnServerValidate="checkEmailExsists_ServerValidate" ValidationGroup="Form"
                                CssClass="ValidationMessage" ErrorMessage="* User already exsists" SetFocusOnError="true" />
                            <asp:CustomValidator ID="validatorEmail4"
                                runat="server" ControlToValidate="textEmail" Display="Dynamic"
                                OnServerValidate="checkWaitForActivation_ServerValidate" ValidationGroup="Form"
                                CssClass="ValidationMessage" ErrorMessage="* User waiting for activation" SetFocusOnError="true" />
                        </td>
                    </tr>
                    <tr>
                        <td class="FieldHeader">Password:*</td>
                        <td>
                            <asp:textbox ID="textPassword" runat="server" CssClass="InputField" MaxLength="50" TextMode="Password" onkeyup="runPassword(this.value,'Password_Strength');" />
                            <asp:RequiredFieldValidator ID="validatorPassword1"
                                runat="server" ControlToValidate="textPassword" ValidationGroup="Form"
                                CssClass="ValidationMessage" ErrorMessage="* Required" Display="dynamic" SetFocusOnError="true" />
                            <asp:RegularExpressionValidator ID="validatorPassword2"
                                runat="server" ControlToValidate="textPassword" ValidationGroup="Form"
                                CssClass="ValidationMessage" ErrorMessage="* Minimum 5 letters" Display="dynamic"
                                ValidationExpression=".{5}.*" SetFocusOnError="true" />
		                    <div style="width:100px;"> 
			                    <div id="Password_Strength_text" style="font-size:10px;"></div>
			                    <div id="Password_Strength_bar" style="font-size:1px; height:2px; width:0px; border:1px solid white;"></div> 
		                    </div>
                        </td>
                    </tr>
                    <tr>
                        <td class="FieldHeader">Confirm Password:*</td>
                        <td>
                            <asp:textbox ID="textPasswordConfirm" runat="server" CssClass="InputField" MaxLength="50" TextMode="Password" />
                            <asp:RequiredFieldValidator ID="validatorConfirmPassword1" 
                                runat="server" ControlToValidate="textPasswordConfirm" ValidationGroup="Form"
                                CssClass="ValidationMessage" ErrorMessage="* Required" Display="dynamic" SetFocusOnError="true" />
                            <asp:CompareValidator ID="validatorConfirmPassword2" 
                                runat="server" ControlToValidate="textPasswordConfirm" ControlToCompare="textPassword"
                                Type="String" Operator="Equal" ValidationGroup="Form" 
                                CssClass="ValidationMessage" ErrorMessage="* Must be identical to password" SetFocusOnError="true" />
                        </td>
                    </tr>
                </table>
            </div>
                  
            <!--h2>Personal Information</h2-->
            <div class="Section" style="display:none;">
                <table>
                    <tr>
                        <td class="FieldHeader">First Name:*</td>
                        <td>
                            <asp:textbox ID="textFirstName" runat="server" CssClass="InputField" MaxLength="30" Text="" />
                                                  </td>
                    </tr>
                    <tr>
                        <td class="FieldHeader">Last Name:*</td>
                        <td>
                            <asp:textbox ID="textLastName" runat="server" CssClass="InputField" MaxLength="30" Text="" />
                         </td>
                    </tr>
                    <tr>
                        <td class="FieldHeader">Company Name:</td>
                        <td>
                            <asp:textbox ID="textCompanyName" runat="server" CssClass="InputField" MaxLength="50" />
                        </td>
                    </tr>
                    <tr>
                        <td class="FieldHeader">Country:*</td>
                        <td>
                            <asp:DropDownList ID="listCountry" runat="server" CssClass="SelectField" DataTextField="Country_Name" DataValueField="Country_Id"  />
                        </td>
                    </tr>
                    <tr>
                        <td class="FieldHeader">Address:*</td>
                        <td>
                            <asp:textbox ID="textAddress" runat="server" CssClass="InputField" MaxLength="100" Text="" />
                         </td>
                    </tr>
                    <tr>
                        <td class="FieldHeader">City/Town:*</td>
                        <td>
                            <asp:textbox ID="textCity" runat="server" CssClass="InputField" MaxLength="50" Text="" />
                        </td>
                    </tr>
                    <tr>
                        <td class="FieldHeader">State/Province:</td>
                        <td>
                            <asp:textbox ID="textState" runat="server" CssClass="InputField" MaxLength="50" />
                        </td>
                    </tr>
                    <tr>
                        <td class="FieldHeader">Zip/Postal Code:</td>
                        <td>
                            <asp:textbox ID="textZipCode" runat="server" CssClass="InputField" MaxLength="10" Text="" />
                        </td>
                    </tr>
                    <tr>
                        <td class="FieldHeader">Phone Number 1:*</td>
                        <td>
                            <asp:textbox ID="textPhone" runat="server" CssClass="InputField" MaxLength="30" Text="" />
                        </td>
                    </tr>
                    <tr>
                        <td class="FieldHeader">Phone Number 2:</td>
                        <td>
                            <asp:textbox ID="textPhone2" runat="server" CssClass="InputField" MaxLength="30" />
                        </td>
                    </tr>
                </table>
            </div>
                    
            <!--h2>Payment Preferences</h2-->
            <div class="Section" style="display:none;">
                <p>Choose the payment method you would like us to pay you:</p>
                <table>
                    <tr>
                        <td class="FieldHeader">Payment Method:*</td>
                        <td>
                            <asp:DropDownList ID="listCreditMethod" runat="server" CssClass="SelectField" DataTextField="Item_Desc" DataValueField="Item_Id" />
                        </td>
                    </tr>
                </table>
                <div id="ssl">
                    <span id="siteseal"><script type="text/javascript" src="https://seal.godaddy.com/getSeal?sealID=<%=ConfigurationSettings.AppSettings["SSL_GoDaddy_Seal_Id"]%>"></script></span>
                </div>
            </div>
            
            <div class="PaymentMethodDetails" style="display:none;">
                <table>
                    <tr>
                        <td>
                        <div id="PaymentBank_Section">
                            <paymentPreferences:bank id="ctlPaymentBank" runat="server"/>
                        </div>
                        <div id="PaymentCheck_Section">
                            <paymentPreferences:check id="ctlPaymentCheck" runat="server"/>
                        </div>
                        <div id="PaymentPayPal_Section">
                            <paymentPreferences:paypal id="ctlPaymentPayPal" runat="server"/>
                        </div>
                        <div id="PaymentLater_Section">
                            <paymentPreferences:later id="ctlPaymentLater" runat="server"/>
                        </div>
                        </td>
                    </tr>
                </table>
            </div>  
            </div>
                      

            <h4>E-mail Preferences</h4>

            <div class="description">
            <div class="Section">
                <asp:CheckBox ID="checkGetEmailAnnouncements" runat="server" Checked="true" />
                I would like to receive E-mail service announcements about my agreement with Inqwise.

                <br />
                
                <asp:CheckBox ID="checkGetEmailNewsletters" runat="server" Checked="true" />
                I would like to receive periodic newsletters with advice, news and occasional surveys.
            </div>
            </div>

              <h4>Policies </h4>
              <div class="description">
            <div class="Section">
<textarea cols="80" rows="10" class="TextareaField" style="font-family: arial, helvetica, sans-serif; font-size: 14px; padding: 6px; width: 700px; border: 2px solid #DEDEDD;">
USER LICENSE AGREEMENT

Welcome to the website of Inqwise Ltd. ("Inqwise").

BY CLICKING ON THE "I ACCEPT" BUTTON BELOW, YOU AGREE TO BECOME BOUND BY THE TERMS AND CONDITIONS OF THIS AGREEMENT (THE "TERMS") BY WHICH Inqwise PROVIDES YOU WITH THE SERVICES DESCRIBED IN SECTION 3 BELOW (THE "SERVICES") THROUGH Inqwise WEBSITE (THE "SITE"). 

YOU ARE ENCOURAGED TO READ THESE TERMS CAREFULLY.

You may not use the Services if you do not accept the Terms.  If you do not accept and agree to these Terms, please do not access the Site or use the Services. You may not use the Services and may not accept the Terms if (a) you are not of legal age to form a binding contract with Inqwise, or (b) you are a person barred from receiving the Services under applicable laws including the laws of the country in which you are resident or from which you use the Services.

Inqwise reserves the right to change these Terms at any time. We recommend that you periodically check this Site for changes.

1. Usage License

Subject to the terms and conditions set forth herein, Inqwise grants you a limited, non transferable license to access the Site and use the Services in accordance with these Terms and the instructions and guidelines posted on the Site, solely for internal and personal use.   You agree that your failure to adhere to any of these conditions shall constitute a breach of these Terms on your part:

* In order to use the Services, you must submit certain identifying information, which must be accurate and updated. It being clarified that the information will be kept and maintained by Inqwise according to the provisions of theses Terms of Service and the Privacy Policy of Inqwise as posted on the Site.
* You are responsible for any activity that occurs under your screen name or caller ID.
* You are responsible for keeping your password secure.
* You must not abuse, harass, threaten, impersonate or intimidate other users of the Site or the Inqwise Services.
* You may not use the Site or the Services for any illegal or unauthorized purpose. International users agree to comply with all local laws regarding online conduct and acceptable content.
* You are solely responsible for your conduct and any data, text, information, screen names, graphics, photos, profiles, audio and video clips, links that you create, submit, post, and display on or through the Site or Services, including any advertising or other content made available to you or submitted by you and any website or other content published by or associated with you (the "Content"). You understand that all Content which you may have access to as part of, or through your use of the Services are the sole responsibility of the person from which such content originated. You hereby acknowledge and agree that if you use any of the Services to contribute or make available Content, AdsCaptha is hereby granted a non-exclusive, worldwide, royalty-free, transferable right to fully exploit such Content (including all related intellectual property rights) and to allow others to do so in connection with the Services and the Site. 
* Content presented to you at the Site or as part of the Services, including but not limited to advertisements within the Services may be protected by intellectual property rights which are owned by the sponsors or Advertisers who provide that Content to Inqwise (or by other persons or companies on their behalf). You may not modify, rent, lease, loan, sell, distribute or create derivative works based on this Content (either in whole or in part) unless you have been specifically told that you may do so by Inqwise or by the owners of that Content, in a separate written agreement. 
* You must not modify, adapt or hack the Site or modify another website, application or service so as to falsely imply that it is associated with the Site or the Inqwise Services.
* You must not create or submit unwanted email or args of any form to any other users or memberrs of the Site or the Inqwise Services.
* You must not distribute any part of or parts of the Site, the Services or the Content, in any medium or through any media formats or through any media channels without Inqwise's prior written authorization;
* You must not transmit any worms or viruses or any code of a destructive nature.
* You must not, in the use of the Site or the Services, including by the uploading or downloading of Content violate any laws in your jurisdiction (including but not limited to copyright laws).
* You must not infringe any patent, patent application, trademark, trade secret, copyright, right of publicity or any other right of any other person or entity.
* You must not collect or harvest any personally identifiable information, including, without limitation, account names or caller IDs from the Site.
* You must not post or upload any Content that is unlawful, threatening, abusive, harassing, defamatory, libelous, deceptive, fraudulent, invasive of another's privacy, tortuous, obscene, offensive or profane.
* You must not circumvent, disable or otherwise interfere with any security related features of the Site and the Services.
* You may use the Site and the Services solely for your personal, non-commercial use.

Violation of any of these agreements will result in the termination of your account and Inqwise preserves the right to deny and block your access to the Site.

All rights to use the Site and Services are provided on a non exclusive basis. Inqwise reserves the rights to terminate your license to use the Site at any time and for any reason.

2. User Responsibility

2.1 By using the Services, you represent and warrant that (a) you are 18 years of age or older; and (b) your use of the Services does not violate any applicable law or regulation. Your account may be terminated without warning, if we believe that you are under 18 years of age, if we believe that you are under 18 years of age and you represent yourself as 18 or older, or if we believe you are over 18 and represent yourself as under 18.

2.2 When you sign up to Inqwise, you will also be asked to choose a password. You are entirely responsible for maintaining the confidentiality of your password. You agree not to use the account, username, or password of another member at any time or to disclose your password to any third party. You agree to notify Inqwise immediately if you suspect any unauthorized use of your account or access to your password. You are solely responsible for any and all use of your account.

2.3 You are solely responsible for your use of the Site and Services. Because Inqwise merely serves as a repository of information, user-posted content does not represent the advice, views, opinions or beliefs of Inqwise, and Inqwise makes no claim of accuracy of any user-posted material.

2.4 Inqwise archives links to third-party websites. The linked websites' content, business practices and privacy policies are not under our control, and we are not responsible for the content of any linked website or any link contained in a linked website. The inclusion of a link on the Site or Services does not imply any endorsement by or any affiliation with Inqwise. In accessing the Site and Services or following links to third-party websites you may be exposed to content that you consider offensive or inappropriate. You agree that your only recourse is to stop using the Site and Services.

2.5 You understand that by accessing the Site or using the Services you may be exposed to Content that you may find offensive, indecent or objectionable and that, in this respect, you use the Services at your own risk.

3. The Services

3.1 AdsCpatcha's Site offers a platform through which media buyers, advertising agencies and any other person who may be interested (each, an "Advertiser") may advertise such Advertiser's services/products upon "CAPTCHAS" that are placed by websites operators who are interested in such service ("Publisher").  The Services provided by the Inqwise enable to facilitate, promote, monitor and manage the performance of advertising campaigns upon CAPTCHAS ("Advertised CAPTCHA"), as more fully described on the Site and as selected by the Advertiser or the Publisher (as the case may be) through the application process provided on the Site. 

3.2 Inqwise may change, suspend or discontinue the Services at any time, including the availability of any feature, advertisement, publisher or content, without notice or liability. Inqwise reserves the right (but shall have no obligation) to pre-screen, review, flag, filter, modify, refuse to display or remove any or all Content from the Services. Inqwise reserves the right, at its discretion, to refuse to allow access to the Services to any applicant at any time and for any reason (including, but not limited to, upon receipt of claims or allegations from third parties or authorities relating to the Content or if Inqwise is concerned that an Advertiser or a Publisher may have breached the terms of this Agreement).   

4. Confidential Information and Non-Disclosure

4.1 The term "Confidential Information" shall include all information provided by Inqwise to you, or your affiliates, employees, officers, directors, agents or representatives, including without limitation, the Web Site and any and all of Inqwise's design specifications, drawings, written manuals, software programs, business plans, financial information, technical and marketing information and evaluations, service plans and customer information designated orally or in writing as confidential or otherwise which by its nature should be considered confidential. For purposes herein, Inqwise shall be deemed the "Disclosing Party" and you shall be deemed the "Receiving Party". Confidential Information shall not include information which can be demonstrated: (a) to have been rightfully in the possession of the Receiving Party from a source other than the Disclosing Party prior to the time of disclosure of said information to the Receiving Party ("Time of Disclosure"); (b) to have been in the public domain prior to the Time of Disclosure; (c) to have become part of the public domain after the Time of Disclosure by a publication or by any other means, except an unauthorized act or omission or breach of this Agreement on the part of the Receiving Party, or its employees; (d) to have been supplied to the Receiving Party after the Time of Disclosure without restriction by a third party who is under no obligation to the Disclosing Party to maintain such information in confidence; or (e) to be required to be disclosed by law or court order, provided that the Receiving Party shall provide the Disclosing Party with prompt notice sufficient for the Disclosing Party to have a reasonable opportunity to prevent such disclosure and shall use best efforts to limit the information to be disclosed. 

4.2 Use and Disclosure of Confidential Information.  The Receiving Party shall, with respect to any Confidential Information: (i) use such Confidential Information only in connection with the performance of the Receiving Party's obligations and exercise of Receiving Party's rights under this Agreement; (ii) if the Receiving Party is incorporated - restrict disclosure of such Confidential Information within the Receiving Party's organization to only those employees and consultants and affiliates who have a need to know such Confidential Information in connection with the performance of this Agreement and (iii) except as expressly contemplated under the preceding clause (ii), not disclose such Confidential Information to any third party unless authorized in writing by Inqwise to do so.  If the Receiving Party or any of its affiliates, employees, officers, directors, agents or representatives shall attempt to improperly use or knowingly disclose any of the Confidential Information, the Disclosing Party shall have the right, in addition to such other remedies which may be available to it, to injunctive relief enjoining such acts or attempts; it being acknowledged that legal remedies are inadequate.

5. Limitation of Liability

THE SERVICES, CONTENT AND SITE ARE PROVIDED ON AN "AS IS" BASIS, WITHOUT WARRANTIES OF ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING, WITHOUT LIMITATION, IMPLIED WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE OR NON-INFRINGEMENT. Inqwise DOES NOT WARRANT THE RESULTS OF USE OF THE SERVICES, INCLUDING, WITHOUT LIMITATION, THE RESULTS OF ANY ADVERTISING CAMPAIGN, AND EACH OF ADVERTISER AND PUBLISHER ASSUMES ALL RISK AND RESPONSIBILITY WITH RESPECT THERETO.

IN NO EVENT SHALL Inqwise BE LIABLE TO USER, ADVERTISER, PUBLISHER AND ANY OF THEIR EMPLOYEES, AGENTS OR ANY OTHER THIRD PARTIES FOR ANY INDIRECT, INCIDENTAL, CONSEQUENTIAL, SPECIAL, PUNITIVE OR EXEMPLARY DAMAGES OR LOSSES, INCLUDING WITHOUT LIMITATION LOSS OF USE, LOSS OF OR DAMAGE TO RECORDS OR DATA, COST OF PROCUREMENT OF SUBSTITUTE GOODS, SERVICES OR TECHNOLOGY, REVENUE AND/OR PROFITS, SUSTAINED OR INCURRED REGARDLESS OF THE FORM OF ACTION, WHETHER IN CONTRACT, TORT OR OTHERWISE, INCLUDING WITHOUT LIMITATION NEGLIGENCE, STRICT LIABILITY, INDEMNITY OR OTHERWISE, AND WHETHER OR NOT SUCH DAMAGES WERE FORESEEN OR UNFORESEEN AND REGARDLESS OF WHETHER Inqwise HAD RECEIVED NOTICE OR HAD BEEN ADVISED, OR KNEW OR SHOULD HAVE KNOWN, OF THE POSSIBILITY OF SUCH DAMAGES OR LOSSES. WITHOUT LIMITATION OF THE FOREGOING, Inqwise'S MAXIMUM LIABILITY FOR ANY AND ALL LOSSES OR DAMAGES (INCLUDING WITHOUT LIMITATION ANY LEGAL FEES OR EXPENSES) INCURRED BY A USER, ADVERTISER OR PUBLISHER IN CONNECTION WITH THIS AGREEMENT SHALL NOT EXCEED US$ 10,000. IN NO EVENT SHALL Inqwise BE LIABLE FOR ANY CLAIM THAT AROSE MORE THAN ONE (1) YEAR PRIOR TO THE INSTITUTION OF A SUIT THEREON.

6. Intellectual Property (Trademarks & Copyrights)

6.1  Inqwise owns and shall own all right, title, and interest in and to the Site, the Services, the Confidential Information, PII collected at the Site patent, patent application, original content included on the Site (such as text, graphics, logos, software and the compilation of all content on the Site), Inqwise's trademarks, trade names, design, and any other data (including PII, as defined below) submitted by end users in response to an advertisement, including all source code, object code, operating instructions, and interfaces developed for or relating to the Services and/or the Site, together with all modifications, enhancements, revisions, changes, copies, partial copies, translations, compilations, and derivative works thereto, including all copyrights and other intellectual property rights relating thereto (the Intellectual Property). Publisher and Advertiser will have no rights with respect to the Intellectual Property other than those expressly granted hereunder. Publisher, Advertiser and/or any other user shall have no right to develop (or to permit any third party to develop) any software tool or other application that interfaces with the Services or the Site. To the extent Publisher, Advertiser or user develops (or permits any third party to develop) any such tool or other application in violation of the preceding sentence, all right, title, and interest in and to such tool (including any intellectual property rights with respect thereto) shall be deemed to be included in the Intellectual Property. To the extent any rights to any Intellectual Property would otherwise vest in Publisher or Advertiser, or any of their Affiliates, or any of their respective contractors or personnel: (i) Publisher/Advertiser hereby assigns (and shall cause its applicable affiliate or other contractors or personnel to assign) to Inqwise all right, title, and interest in and to such Intellectual Property and (ii) upon Inqwise's request, Publisher/Advertiser shall execute (and shall cause each applicable affiliate or other contractors or personnel to execute) such documents and provide such other assistance as may reasonably be requested by Inqwise to further evidence and perfect such assignment.

"PII" (Personally Identifiable Information), shall mean any piece of information which can be used to uniquely identify, contact, or locate a single person, or can be used with other sources to uniquely identify a single person. 

6.2  Inqwise, in its sole discretion, shall have the right to copy, sell, distribute, transfer, lease, assign, market, use, license, and re-market the non-PII collected pursuant to Section 8.3 below and at the Site (the "Data") and any PII voluntarily provided by end users without further obligation to Publisher. Publisher shall not make any use of, copy, make derivative works from, sell, transfer, lease, assign, redistribute, disclose, disseminate, or otherwise make available in any manner such Data, or any portion thereof, to any third-party.

7. Privacy

The use of the Services herein is also governed by our Privacy Policy, which is incorporated into these Terms of Service by this reference.

8. Publisher Acceptable Use Policy

8.1 Inqwise prohibits displaying advertisements on websites which contain the content set forth in Section 4.1 above as well as the following content:

* Promotion of illegitimate programs and offers, such as schemes or high yield investments.
* Content or site construction intended to deceive or mislead for purposes of increasing traffic or clicks.
* Little or no original content. This includes, but is not limited to:
* Scraped content: Sites with content taken from high-quality sources or Free Article sites. These are often template or cookie cutter sites.
* Auto-generated content: Sites with programmatically generated content. These sites often have random content that is confusing or nonsensical to a site visitor.
* Doorway sites: Sites with content designed to increase search engine rank, with little content for an actual site visitor.
* Made for ad sites: Sites that primarily consists of only advertisements do not constitute content for a landing page.
* Login to view any content: Sites with landing pages with only a login or registration form.
* Thin affiliate sites: These sites collect pay-per-click (PPC) revenue by sending visitors to the sites of affiliate programs, while providing little or no value-added content or service to the user. These sites usually have no original content and may be cookie-cutter sites or templates with no unique content.
* Site under construction: Sites that are blank, under construction, page with raw coding/incorrect coding, or take too long respond.

8.2 Publisher agrees to comply with the Publisher Acceptable Use Policy, as the same may be updated from time to time by Inqwise at its sole discretion. Publisher shall not, and shall not authorize or encourage any third party to (i) generate fraudulent  impressions or fraudulent typings of or fraudulent clicks on any advertisement, including but not limited to through repeated manual impressions, typings or clicks, the use of robots or other  automated query tools and/or computer generated search requests, and/or the fraudulent use of other search engine optimization services and/or software; (ii) edit, modify, filter or change the order of the information contained in any advertisement, or remove,  obscure or minimize any advertisement in any way;  (iii) redirect an end user away from any Web page accessed by an end user after clicking on any part of an advertisement ("Advertiser Page"), provide a version of the Advertiser Page different from the page an end user would access by going directly to the Advertiser Page or intersperse any content between the advertisement and the Advertiser Page; (iv) display any advertisements on any error page, registration or "thank you" page (e.g. a page that thanks a user after he/she has registered with the applicable website); (v) remove or modify any proprietary marking or restrictive legends placed on the Service or the Site; (vi) communicate directly with any Advertiser regarding the advertisements or Publisher's use of the Service; or (vii) initiate any communication of any kind that encourages, solicits, induces, diverts, entices, or otherwise diminishes Inqwise's business or relationship with any Advertiser or other Publisher. 

8.3 Right to Access by Inqwise.  In order to use the Service, Publisher must make the Publisher websites available to receive and display the advertisements and to receive the Cookies (as defined below).  Publisher hereby grants Inqwise the right during the Term to (i) access, index, and cache requests made from the Publisher websites to the Service, including by automated means; (ii) use Cookies, web beacons, and other technologies on the Publisher websites via the advertisements in order to collect non-PII from and tailor advertisements to end users on Publisher websites and other websites; and (iii) collect and use PII voluntarily provided by end users in response to advertisements.

"Cookies" shall mean an Inqwise and/or a third parties alphanumeric identifier assigned to and stored on an end user's computer when such computer communicates with the Service's ad server. The Cookies are recorded by the ad server to enable the collection of non-PII from end users. 

9. Disclaimer

You acknowledge and agree that Inqwise has no special relationship with or fiduciary duty to any Advertiser or Publisher and that Inqwise has no control over, and no duty to take any action regarding: which users gains access to the Site or Services; what Content an Advertiser or Publisher access or receive via the Site or Services; what Content other users may make available, publish or promote in connection with the Services; what effects any Content may have on a user or its  customers; how users or customers may interpret, view or use the Content; what actions users or customers may take as a result of having been exposed to the Content, or whether Content is being displayed properly in connection with the Services.  Further, the Publisher specifically acknowledges and agrees that Inqwise has no control over (and is merely a passive conduit with respect to) any Content that may be submitted or published by any Advertiser, and that the Publisher is solely responsible (and assumes all liability and risk) for determining whether or not such Content is appropriate or acceptable to Publisher.  The Publisher hereby releases Inqwise from all liability in any way relating to the provision, use or other activity with respect to Content in connection with the Site or Services. Inqwise makes no representations concerning any content contained in or accessed through the Site or Services, and Inqwise will not be responsible or liable for the accuracy, copyright compliance, legality or decency of material contained in or accessed through the Site or Services. Inqwise makes no guarantee regarding the level of impressions or typings of or clicks on any advertisement, the timing of delivery of such impressions, typings and/or clicks, or the amount of any payment to be made to a Publisher in connection with the Services.

10. Fees and Payment

For as long as Publisher act in compliance with the terms and conditions set forth herein, the Publisher shall receive as payment a percentage of the sale price of advertisements displayed in connection with the Publisher's website as determined by Inqwise for Publisher's use of the Services. Publisher's payable revenue shall be determined on the first day of each month, on which day those revenues accrued to Publisher's account sixty (60) or more days earlier shall become payable. Payments to Publisher shall be sent by Company within approximately ten (10) days of the previous calendar month end, provided however that  (i) any payments that may become due to Publisher (as described on the Site) are specifically conditioned upon Inqwise's receipt of full payment from the applicable Advertiser, and that any such payments do not become due to Publisher until 60 days after Inqwise's receipt of full payment from the applicable Advertiser, and (ii) if Inqwise does not receive the applicable payment in full from any such Advertiser, or Inqwise'a payment from Advertiser is later reversed at any time, Inqwise may debit Publisher's account or reverse or stop payment on any payment previously made to Publisher in respect of such advertisements, and Inqwise shall have no liability or responsibility to Publisher (and Publisher hereby releases Inqwise) with respect thereto. If Publisher disputes any payment made in connection with the Services, Publisher must notify Inqwise in writing within thirty (30) days of any such payment. Failure to so notify Inqwise shall result in the waiver by Publisher of any claims related to such disputed payment. Inqwise shall not be liable for any payment based on (i) any fraudulent impressions, typings, clicks or acquisitions generated by any person, robot, automated program or similar device, as reasonably determined by Inqwise; (ii) advertisements delivered to end users whose browsers have JavaScript disabled; (iii) impressions, typings, clicks or acquisitions commingled with a significant number of fraudulent impressions clicks or acquisitions described in (i) above, (iv) any impressions, typings, clicks or acquisitions that arise in whole or in part as a result of any violation of Publisher Acceptable User Policy (including any clicks, impressions or acquisitions that result from a publisher site that violates the Publisher Acceptable User Policy); or (v) any impressions, typings, clicks or acquisitions that arise as a result of another breach of this Agreement by a user for any applicable pay period. 

Inqwise reserves the right to withhold payment or charge back user's account due to any of the foregoing or any breach of this Agreement by any user.  Without derogating from the aforesaid, in each of the forgoing cases Inqwise shall have the right to deduct and set-off from any amounts due to the Publisher pursuant to this Agreement, the amounts which Inqwise's believes that have been generated from or made in connection with any fraudulent actions.  To ensure proper payment, each user is solely responsible for providing and maintaining accurate contact and payment information associated with such user's account and Inqwise User ID. Any bank fees related to returned or cancelled checks due to a contact or payment information error or omission may be deducted from the newly issued payment.  Without limiting the generality of the foregoing, Publisher acknowledges and agrees that checks payable to Publisher that are not cashed within 180 days from the date of issuance will be cancelled by Inqwise and will not be reissued, and in the event of such cancellation, Publisher irrevocably waives any right to payment of amounts represented by such checks. Each user agrees to pay all income taxes and other applicable taxes or charges imposed by any government entity in connection with such user's use of the Services.

11. Miscellaneous

11.1 The Terms constitute the whole legal agreement between you and Inqwise and govern your use of the Site and the Services and completely replace any prior agreements between you and Inqwise in relation to the Site or the Services.

11.2 Inqwise and you are independent entities, and nothing in the Terms, or via use of the Site or the Services, will create any partnership, joint venture, agency, franchise, sales representative, or employment relationship between Inqwise and you.

11.3 You agree that Inqwise may provide you with notices and\or argss and\or other materials, including but not limited to those regarding changes of the Terms, and marketing or commercial argss by email, regular mail, postings on the Site or during video call sessions, and text argss to mobile phone. If you wish to stop receiving notices, please e-mail us to support@Inqwise.com.

11.4 You agree that if Inqwise does not exercise or enforce any legal right or remedy which is contained in the Terms (or which Inqwise has the benefit of under any applicable law), this will not be taken to be a formal waiver of Inqwise's rights and that those rights or remedies will still be available to Inqwise.

11.5 No other person or company shall be a third party beneficiary of the Terms.

11.6 The Terms, and your relationship with Inqwise under the Terms, shall be governed exclusively by Israeli law. Any claim or dispute between you and Inqwise that arises in whole or in part from the Services or from the Terms shall be decided exclusively by the authorized courts of the State of Israel in the District of Tel Aviv-Jaffa. Notwithstanding this, you agree that Inqwise shall still be allowed to apply for injunctive remedies (or other equivalent types of urgent legal remedy) in any jurisdiction.

11.7 If any provision of the Terms is adjudged to be illegal or unenforceable, the continuation in full force of the remainder of the Terms will not be prejudiced, and the illegal or unenforceable provision of the Terms shall be severed accordingly.
</textarea>
                <br />
                <asp:CheckBox ID="checkCertifyPolicy" runat="server" />
                I have read this agreement and agree to all of the provisions contained above
                <asp:CustomValidator ID="validatorCertifyPolicy"
                    runat="server" Display="Dynamic"
                    OnServerValidate="checkCertifyPolicy_ServerValidate" ValidationGroup="Form"
                    CssClass="ValidationMessage" ErrorMessage="* Please certify Inqwise policy" />
            </div>
            </div>
            <div id="buttonHolder">                
                <asp:LinkButton id="buttonSubmit" runat="server" CssClass="btn" style="color:#FFFFFF;" OnClick="buttonSubmit_Click" CausesValidation="true" ValidationGroup="Form"><span>Submit</span></asp:LinkButton>
                <!-- <a id="buttonCancel" class="btn" href="StartPage.aspx" style="color:#FFFFFF;"><span>Cancel</span></a> -->
            </div>
            
            <script type="text/javascript" >
                $('#PaymentCheck_Section').ready(function() {
                    OnPaymentMethodChange();
                });
                pageLoad = function() {
                    OnPaymentMethodChange();
                };
            </script>

            </ContentTemplate>
        </asp:UpdatePanel>    
        </div>
    </div>
     <div class="clear"></div><!--end content-->
     
	 <!-- Google Code for Remarketing tag -->
	 <!-- Remarketing tags may not be associated with personally identifiable information or placed on pages related to sensitive categories. For instructions on adding this tag and more information on the above requirements, read the setup guide: google.com/ads/remarketingsetup -->
	 <script type="text/javascript">
	 /* <![CDATA[ */
	 var google_conversion_id = 994048181;
	 var google_conversion_label = "flM8CIuulwQQtfH_2QM";
	 var google_custom_params = window.google_tag_params;
	 var google_remarketing_only = true;
	 /* ]]> */
	 </script>
	 <script type="text/javascript" src="//www.googleadservices.com/pagead/conversion.js">
	 </script>
	 <noscript>
	 <div style="display:inline;">
	 <img height="1" width="1" style="border-style:none;" alt="" src="//googleads.g.doubleclick.net/pagead/viewthroughconversion/994048181/?value=0&amp;label=flM8CIuulwQQtfH_2QM&amp;guid=ON&amp;script=0"/>
	 </div>
	 </noscript>

</asp:content>