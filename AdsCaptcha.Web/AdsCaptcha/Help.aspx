<%@ Page EnableViewStateMac="false" Title="FAQ | Inqwise" Language="C#" MasterPageFile="AdsCaptcha.Master" AutoEventWireup="true" CodeFile="Help.aspx.cs" Inherits="Inqwise.AdsCaptcha.Help" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server"> 

</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="BodyContent" runat="server">
id="supportpage" class="page"
</asp:Content> 
<asp:Content ID="LoginContent" ContentPlaceHolderID="LoginContent" runat="server">
	
	
	<div>
		<ul class="menu-top">
			<li><a href="aboutus.aspx" title="About Us">About Us</a></li>
			<li><a href="products.aspx" title="Products">Products</a></li>
		</ul>
	</div>
	<div class="menu-lobby-container">	
		<ul class="menu-lobby">
			<li><a href="advertiser/StartPage.aspx" title="Advertisers" class="button-green"><span>Advertisers</span></a></li>
			<li><a href="publisher/StartPage.aspx" title="Site Owners" class="button-green"><span>Site Owners</span></a></li>
		</ul>
	</div>
	
</asp:Content>
<asp:content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

	<div class="content">
		
		<div class="breadcrumbs">
			<a href="/" title="Home">Home</a>&nbsp;&rsaquo;&nbsp;FAQ
		</div>
		<div class="inner-wrapper">
			<div class="inner-content">
				<div class="page-heading-wrapper">
					<h1>Frequently Asked Questions</h1>
				</div>
				<div>

	            <h4>For Site Owners</h4>
	            <br/><br/>
	            <h3 class="sub-title">How do I get paid</h3>
	            <div class="desc">
	            	We share 50% of our revenues with the Site owners (Publishers) payment will be made monthly, with a minimum threshold of 150USD by Pay Pal or Bank Transfer (If pay out is over 500USD) 
	            </div>
	            <h3 class="sub-title">How many times a day the back end reports are being update ?</h3>
	            <div class="desc">
	            	The reports are updated every 6 hours. The displayed Time Zone is GMT
	            </div>
	            <h3 class="sub-title">Why my FITs are increasing but revenue does not ?</h3>
	            <div class="desc">
	            	Revenue share is applicable on commercial captchas when a user successfully FITs a captcha or Clicks on the ad unit, we try to match and optimize the sites profile to the existing campaigns according the users IP and the site category. If your users don't fall into any of our advertisers active campaigns we will serve a security only captcha.
	            </div>
	            <h3 class="sub-title">Do you have an affiliate program?</h3>
	            <div class="desc">
	            	We currently do not have an affiliate program
	            </div>
	            <h3 class="sub-title">Do you have test environment for my Captcha ?</h3>
	            <div class="desc">
	            	At the moment we do not support test enviroment
	            </div>
	            <h3 class="sub-title">WHAT IF I HAVE A QUESTION NOT LISTED HERE?</h3>
	            <div class="desc">    
	            	You are welcome to click on the <a href="ContactUs.aspx">Contact Us</a> and send us any questions or inquires you might have. Complete the form according to the information fields listed and we will answer your question as soon as possible.
	            </div>
	            <h3 class="sub-title">Is Inqwise's SLIDING CAPTCHA service Free?</h3>
	            <div class="desc">
					The Non commercial and the House Ads CAPTCHA service is Free (up to 15,000 Captcha Per Month) if you need to serve more than 15,000 CAPTCHAs per month <a href="mailto:support@Inqwise.com">please email</a>
				</div>
				<h3 class="sub-title">What is the difference between Commercial and non Commercial Captchas ?</h3>
				<div class="desc">
                    <ul style="list-style-type:none;">
	                    <li>*Non Commercial  - Security Only  service. A random  
	                    picture  will be Displayed in your CAPTCHA image. The
	                    service is Free up to 15,000 CAPTCHAS per month.
	                    <a href="mailto:support@Inqwise.com">Email if you need more</a>
	                    </li>
	                    <li>*Commercial - Income generating CAPTCHA. An  ad   
	                     will be displayed as CAPTCHA image, your  revenue will  
	                     be generated as your users will  Fit /Click the ad in
	                     your CAPTCHA  (No revenue is Guaranteed). We share 50% of our revenue with
	                     the Site owners
	                     </li>
                     </ul>
				</div>
				<h3 class="sub-title">What is House ads CAPTCHA?</h3>
				<div class="desc">
                    *House Ads - Use your CAPTCHA to promote your Brand /  
                     Products / Services in your own site. Your CAPTCHA images will be
                     displayed only on your site.  The service is Free up to
                    15,000 CAPTCHAs per month.
                    <a href="mailto:support@Inqwise.com">Email if you need more</a>
	            </div>
		        <h3 class="sub-title">Does Sliding captcha support audio challenge captcha for Visually impaired?</h3>
		        <div class="desc">
		        	Visually impaired users can click the audio button  to hear a instruction  instead of the visual challenge.
		        </div>
		        <br />
	            <h4>For Advertisers</h4>
	            <br/><br/>
	            <h3 class="sub-title">What type of Ad unit Inqwise supports?</h3>
	           	<div class="desc">
	                We support 2 types of IAB ad units: <br />
	                300W x 250H and 180W x 150H
				</div>
	            <h3 class="sub-title">What goals can I set for my campaign ?</h3>
	            <div class="desc">
	            	You can buy clicks -  Pay Per Click  PPC  (Traffic to your target site/landing page) or Engagement Pay Per Engagement PPE  A successful Fit by a user which mean a proven engagement with your brand.
	            </div>
	            <h3 class="sub-title">How do I set the PPC or PPE price</h3>
	            <div class="desc">
	            	Inqwise's platform is based on a bidding system, the higher the bid the higher the number of exposures you'll receive to you campaign.
	            </div>
	            <h3 class="sub-title">How long it take till I will start to get traffic to my campaign?</h3>
	            <div class="desc">
	            	Once you upload your creative, set up the goal and put your credit card details, your campaign will be approved and will go live. 
				</div>

				</div>

			</div>
		</div>
		
		<div class="coa_footer">
			<div class="coa_container">
		    	<a href="publisher/signup.aspx" class="fl_right_a"><img src="css/Inqwise/images/singup_btn.jpg" width="200px" /></a>
		  		<div class="fl_right" style="padding-left:26px">
					<h4>FOR <span class="uppercase">SITE OWNERS</span></h4>
					<div class="description">More revenue, Lower Bounce Rate with Pre-Roll SKIP ad and Sliding Captcha</div>
				</div>
				<a href="advertiser/signup.aspx" class="fl_right_a"><img src="css/Inqwise/images/singup_btn.jpg" width="200px" /></a>
		    	<div class="fl_right">
					<h4>FOR <span class="uppercase">ADVERTISERS</span></h4>
					<div class="description">Interactive advertising the user can't disregard!</div>
		  		</div>
		  	</div>
		</div>
		
	</div>


</asp:content>

