using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web;
using System.Web.UI.HtmlControls;

namespace Inqwise.AdsCaptcha
{
    public static class Metadata
    {
        public enum Pages
        {
            General,
            AdvertiserStart,
            PublisherStart,
            DeveloperStart,
            Advertiser,
            AdvertiserSignUp,
            AdvertiserLogin,
            Publisher,
            PublisherSignUp,
            PublisherLogin,
            Developer,
            Admin,
            Resources,
            Help,
            AboutUs,
            ContactUs,
            TermsOfUse,
            Privacy,
            Products,
			CaseStudy,
			AdvertiserPreRoll,
			SiteOwnerPreRoll
        }

        private const string ROBOTS_ALLOW = "index, follow";
        private const string ROBOTS_DISALLOW = "noindex, nofollow";

        // Set metadata
        public static void SetMetadata(Pages page, HtmlHead header)
        {
            HtmlMeta meta;
            string title = "";
            string keywords = "";
            string description = "";
            string robots = "";

            switch (page)
            {
                case Pages.General:
                    title = "Skip Pre-Roll Video Ads, VAST, VPAID & Sliding CAPTCHA";
                    description = "offers a security and advertisement solution that replaces traditional CAPTCHA and enables sites owners to make money from CAPTCHA technology. slide-to-fit CAPTCHA alternative works on variety of web platforms, PCs, Macs, Smartphones and Tablets.";
                    keywords = "Captcha, Captcha Ads, Captcha advertising, ads Captcha";
                    robots = ROBOTS_ALLOW;
                    break;
                case Pages.DeveloperStart:
                    title = "Developer – Gain from Captchas and bre our partner | Inqwise.com";
                    description = "New Way to earn Money from your sites – integrate our unique Captcha codes into your websites and start getting money today!!! AdsCaptcha offer a Creative solution for your site security AND gives you money";
                    keywords = "Developer, affiliate, partner, Captcha earnings, Captcha ads";
                    robots = ROBOTS_ALLOW;
                    break;
                case Pages.AdvertiserStart:
                    title = "Sliding CAPTCHA Advertising Solution for Advertisers, Brands and Agencies";
                    description = "breakthrough advertising solution set for advertisers, the Sliding CAPTCHA, offers a fun and interactive experience that improves user’s engagement and increases click-through-rate.";
                    keywords = "Online Advertising, Unique message, Target Audience, Captcha";
                    robots = ROBOTS_ALLOW;
                    break;
                case Pages.AdvertiserSignUp:
                    title = "Sign Up -  Captcha Advertising Solutions for your Brand";
                    description = "Looking for a great advertising opportunity? Cpatcha Slide-To-Fit is the best advertising solution for your brand.";
                    keywords = "Publishers opportunities, Captcha ads, Site Profits, Gain over users";
                    robots = ROBOTS_ALLOW;
                    break;
                case Pages.AdvertiserLogin:
                    title = "Login to Inqwisey - Slide-To-Fit Captcha Advertising Solutions";
                    description = "Cpatcha Slide-To-Fit by is a unique advertising solution for any brand using online and mobile platforms as its marketing channels.";
                    keywords = "Publishers opportunities, Captcha ads, Site Profits, Gain over users";
                    robots = ROBOTS_ALLOW;
                    break;
                case Pages.PublisherStart:
                    title = "CAPTCHA Security Solution that Pays Site Owners";
                    description = "offers site owners a fun and enjoyable alternative for CAPTCHA security solution, the slide-to-fit CAPTCHA, which eliminates spam bots, improves user experience and enables them to make money from CAPTCHA.";
                    keywords = "Publishers opportunities, Captcha ads, Site Profits, Gain over users";
                    robots = ROBOTS_ALLOW;
                    break;
                case Pages.PublisherSignUp:
                    title = "Security Solution for Site Owners – Sign up to Inqwise";
                    description = "Sign up for and join the network of site owners who make money from 	CAPTCHA security alternatives and improve conversion rates, security and user experience.";
                    keywords = "Publishers opportunities, Captcha ads, Site Profits, Gain over users";
                    robots = ROBOTS_ALLOW;
                    break;
                case Pages.PublisherLogin:
                    title = "Login to Security Solutions for Publishers";
                    description = "Login to and follow your profits from advanced CAPTCHA technology solutions that protects site owners from spam bots and enables them to make money.";
                    keywords = "Publishers opportunities, Captcha ads, Site Profits, Gain over users";
                    robots = ROBOTS_ALLOW;
                    break;
                case Pages.Admin:
                    title = "Admin";
                    robots = ROBOTS_DISALLOW;
                    break;
                case Pages.Resources:
                    title = "Resources and API Overview";
                    description = "Documentation and examples of how to embed sliding CAPTCHA and advertising solution in your websites.";
                    keywords = "";
                    robots = ROBOTS_ALLOW;
                    break;
                case Pages.Help:
                    title = "FAQ about Security Solutions";
                    description = "Answers to the most frequently asked questions about security solutions 	and revenue programs for both site owners and advertisers.";
                    keywords = "FAQ, Help, Captcha ads";
                    robots = ROBOTS_ALLOW;
                    break;
                case Pages.Products:
                    title = "Products – CAPTCHA security for Site Owners and Advertisers";
                    description = "primary product is the slide-to-fit CAPTCHA, a multilingual and 	multiplatform security solution that improves user’s experience and conversion for 	site owners and engagement for brands and advertisers.";
                    keywords = "Captcha, Captcha Ads, Captcha advertising, ads Captcha";
                    robots = ROBOTS_ALLOW;
                    break;
                case Pages.AboutUs:
                    title = "About – CAPTCHA & Advertisement Solutions";
                    description = "All about Inqwise, the company that combines CAPTCHA security technology with advertising for site owners, brands and advertisers and enables them to profits from internet CAPTCHA security.";
                    keywords = "Captcha, Ads, about Us, Team";
                    robots = ROBOTS_ALLOW;
                    break;
                case Pages.ContactUs:
                    title = "Contact – Slide to Fit Captcha & Advertising";
                    description = "Contact with any question or inquiry regarding the slide-to-fit CAPTCHA or other security and advertisement solutions.";
                    keywords = "Captcha, Contact us, Form, Questions, Captcha Ads";
                    robots = ROBOTS_ALLOW;
                    break;
                case Pages.TermsOfUse:
                    title = "Sliding CAPTCHA Terms of Service";
                    description = "Welcome to the website. By accessing website or using the services offered you agree and acknowledge to be bound by these terms of Services.";
                    keywords = "Terms of Use, SLA, Captcha, Captcha Ads";
                    robots = ROBOTS_ALLOW;
                    break;
                case Pages.Privacy:
                    title = "Captcha Technology Privacy Policy";
                    description = "is the first and only internet company today that offers mutual benefits for both advertisers and site owners by using CAPTCHA technology. Using the strictest security measures in the world, proudly provide a free security tool that generates revenue for site owners.";
                    keywords = "Terms of Use, SLA, Captcha, Inqwise, Captcha Ads";
                    robots = ROBOTS_ALLOW;
                    break;
                case Pages.Developer:
                    title = "Inqwise.com | Developers";
                    robots = ROBOTS_DISALLOW;
                    break;
                case Pages.Advertiser:
                    title = "Inqwise.com | Advertisers";
                    robots = ROBOTS_DISALLOW;
                    break;
                case Pages.Publisher:
                    title = "Security Solution for Site Owners – Sign up to Inqwise";
                    robots = ROBOTS_DISALLOW;
                    break;
	                case Pages.CaseStudy:
	                    title = "Comcast Xfinity Campaign on AOL Properties Powered by Inqwise";
	                    description = "Case Study";
	                    keywords = "Case Study, Comcast Xfinity, AOL Pre-roll Skip Ad";
	                    robots = ROBOTS_ALLOW;
	                   	break;
   	                case Pages.AdvertiserPreRoll:
   	                    title = "Pre-Roll Skip Ad - Advertisers";
   	                    description = "Pre-Roll Skip Ad";
   	                    keywords = "Pre Roll Skip Ad";
   	                    robots = ROBOTS_ALLOW;
   	                   	break;
					case Pages.SiteOwnerPreRoll:
      	            	title = "Pre-Roll Skip Ad - Site Owners";
      	                description = "Pre-Roll Skip Ad";
      	                keywords = "Pre Roll Skip Ad";
      	                robots = ROBOTS_ALLOW;
      	                break;
            }

            // Set title.
            header.Title = title;

            // Set keywords.
            meta = new HtmlMeta();
            meta.Attributes.Add("name", "keywords");
            meta.Attributes.Add("content", keywords);
            header.Controls.Add(meta);

            // Set description.
            meta = new HtmlMeta();
            meta.Attributes.Add("name", "description");
            meta.Attributes.Add("content", description);
            header.Controls.Add(meta);

            // Set robots.
            string env = ConfigurationSettings.AppSettings["Environment"];
            if (env != "Prod") robots = ROBOTS_DISALLOW;

            meta.Attributes.Add("name", "robots");
            meta.Attributes.Add("content", robots);
            header.Controls.Add(meta);
        }

    }
}
