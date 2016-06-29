﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Sfa.Das.Sas.Web.Views.Provider
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Text;
    using System.Web;
    using System.Web.Helpers;
    using System.Web.Mvc;
    using System.Web.Mvc.Ajax;
    using System.Web.Mvc.Html;
    using System.Web.Optimization;
    using System.Web.Routing;
    using System.Web.Security;
    using System.Web.UI;
    using System.Web.WebPages;
    
    #line 5 "..\..\Views\Provider\Detail.cshtml"
    using FeatureToggle.Core.Fluent;
    
    #line default
    #line hidden
    
    #line 4 "..\..\Views\Provider\Detail.cshtml"
    using Sfa.Das.Sas.ApplicationServices.FeatureToggles;
    
    #line default
    #line hidden
    
    #line 2 "..\..\Views\Provider\Detail.cshtml"
    using Sfa.Das.Sas.Resources;
    
    #line default
    #line hidden
    using Sfa.Das.Sas.Web;
    
    #line 1 "..\..\Views\Provider\Detail.cshtml"
    using Sfa.Das.Sas.Web.Extensions;
    
    #line default
    #line hidden
    
    #line 3 "..\..\Views\Provider\Detail.cshtml"
    using Sfa.Das.Sas.Web.ViewModels;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/Provider/Detail.cshtml")]
    public partial class Detail : System.Web.Mvc.WebViewPage<ApprenticeshipDetailsViewModel>
    {

#line 104 "..\..\Views\Provider\Detail.cshtml"
public System.Web.WebPages.HelperResult ShowTrainingLocation(string title)
{
#line default
#line hidden
return new System.Web.WebPages.HelperResult(__razor_helper_writer => {

#line 105 "..\..\Views\Provider\Detail.cshtml"
 
if (@Model != null)
{


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "        <dt");

WriteLiteralTo(__razor_helper_writer, " class=\"training-location-title\"");

WriteLiteralTo(__razor_helper_writer, ">");


#line 108 "..\..\Views\Provider\Detail.cshtml"
              WriteTo(__razor_helper_writer, title);


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "</dt>\r\n");


#line 109 "..\..\Views\Provider\Detail.cshtml"

    if (@Model.DeliveryModes.Count == 1 && @Model.DeliveryModes.Contains("100PercentEmployer"))
    {


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "            <dd");

WriteLiteralTo(__razor_helper_writer, " id=\"training-location\"");

WriteLiteralTo(__razor_helper_writer, " class=\"training-location\"");

WriteLiteralTo(__razor_helper_writer, ">\r\n                Training takes place at your location\r\n            </dd>\r\n");


#line 115 "..\..\Views\Provider\Detail.cshtml"
    }
    else
    {


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "            <dd");

WriteLiteralTo(__razor_helper_writer, " id=\"training-location\"");

WriteLiteralTo(__razor_helper_writer, " class=\"training-location\"");

WriteLiteralTo(__razor_helper_writer, ">\r\n");

WriteLiteralTo(__razor_helper_writer, "                ");


#line 119 "..\..\Views\Provider\Detail.cshtml"
WriteTo(__razor_helper_writer, Model.Location.LocationName);


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, " ");


#line 119 "..\..\Views\Provider\Detail.cshtml"
               WriteTo(__razor_helper_writer, Model.Address.Address1);


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, " ");


#line 119 "..\..\Views\Provider\Detail.cshtml"
                                       WriteTo(__razor_helper_writer, Model.Address.Address2);


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, " ");


#line 119 "..\..\Views\Provider\Detail.cshtml"
                                                               WriteTo(__razor_helper_writer, Model.Address.Town);


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, " ");


#line 119 "..\..\Views\Provider\Detail.cshtml"
                                                                                   WriteTo(__razor_helper_writer, Model.Address.County);


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, " ");


#line 119 "..\..\Views\Provider\Detail.cshtml"
                                                                                                         WriteTo(__razor_helper_writer, Model.Address.Postcode);


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "\r\n            </dd>\r\n");


#line 121 "..\..\Views\Provider\Detail.cshtml"
    }
}


#line default
#line hidden
});

#line 123 "..\..\Views\Provider\Detail.cshtml"
}
#line default
#line hidden

#line 125 "..\..\Views\Provider\Detail.cshtml"
public System.Web.WebPages.HelperResult GetStandardPropertyHtml(string title, string id, string item, bool hideIfEmpty = false)
{
#line default
#line hidden
return new System.Web.WebPages.HelperResult(__razor_helper_writer => {

#line 126 "..\..\Views\Provider\Detail.cshtml"
 
if (!string.IsNullOrEmpty(item) || !hideIfEmpty)
{


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "        <dt");

WriteLiteralTo(__razor_helper_writer, " class=\"phone-title\"");

WriteLiteralTo(__razor_helper_writer, ">");


#line 129 "..\..\Views\Provider\Detail.cshtml"
  WriteTo(__razor_helper_writer, title);


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "</dt>\r\n");

WriteLiteralTo(__razor_helper_writer, "        <dd");

WriteAttributeTo(__razor_helper_writer, "id", Tuple.Create(" id=\"", 5445), Tuple.Create("\"", 5453)

#line 130 "..\..\Views\Provider\Detail.cshtml"
, Tuple.Create(Tuple.Create("", 5450), Tuple.Create<System.Object, System.Int32>(id

#line default
#line hidden
, 5450), false)
);

WriteLiteralTo(__razor_helper_writer, " class=\"phone\"");

WriteLiteralTo(__razor_helper_writer, ">");


#line 130 "..\..\Views\Provider\Detail.cshtml"
     WriteTo(__razor_helper_writer, Html.Raw(item));


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "</dd>\r\n");


#line 131 "..\..\Views\Provider\Detail.cshtml"
}


#line default
#line hidden
});

#line 132 "..\..\Views\Provider\Detail.cshtml"
}
#line default
#line hidden

#line 134 "..\..\Views\Provider\Detail.cshtml"
public System.Web.WebPages.HelperResult GetEmailPropertyHtml(string title, string id, string item, bool hideIfEmpty = false)
{
#line default
#line hidden
return new System.Web.WebPages.HelperResult(__razor_helper_writer => {

#line 135 "..\..\Views\Provider\Detail.cshtml"
 
if (!string.IsNullOrEmpty(item) || !hideIfEmpty)
{


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "        <dt");

WriteLiteralTo(__razor_helper_writer, " class=\"email-title\"");

WriteLiteralTo(__razor_helper_writer, ">");


#line 138 "..\..\Views\Provider\Detail.cshtml"
  WriteTo(__razor_helper_writer, title);


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "</dt>\r\n");

WriteLiteralTo(__razor_helper_writer, "        <dd");

WriteAttributeTo(__razor_helper_writer, "id", Tuple.Create(" id=\"", 5705), Tuple.Create("\"", 5713)

#line 139 "..\..\Views\Provider\Detail.cshtml"
, Tuple.Create(Tuple.Create("", 5710), Tuple.Create<System.Object, System.Int32>(id

#line default
#line hidden
, 5710), false)
);

WriteLiteralTo(__razor_helper_writer, " class=\"email\"");

WriteLiteralTo(__razor_helper_writer, "><a");

WriteAttributeTo(__razor_helper_writer, "href", Tuple.Create(" href=\"", 5731), Tuple.Create("\"", 5760)
, Tuple.Create(Tuple.Create("", 5738), Tuple.Create("mailto:", 5738), true)

#line 139 "..\..\Views\Provider\Detail.cshtml"
, Tuple.Create(Tuple.Create("", 5745), Tuple.Create<System.Object, System.Int32>(Html.Raw(item)

#line default
#line hidden
, 5745), false)
);

WriteLiteralTo(__razor_helper_writer, ">");


#line 139 "..\..\Views\Provider\Detail.cshtml"
                                      WriteTo(__razor_helper_writer, Html.Raw(item));


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "</a></dd>\r\n");


#line 140 "..\..\Views\Provider\Detail.cshtml"
}


#line default
#line hidden
});

#line 141 "..\..\Views\Provider\Detail.cshtml"
}
#line default
#line hidden

#line 143 "..\..\Views\Provider\Detail.cshtml"
public System.Web.WebPages.HelperResult GetStandardPropertyAsLinkHtml(string title, string cssClass, string classTitle, string classIdentifier, string url, string urlTitle = "")
{
#line default
#line hidden
return new System.Web.WebPages.HelperResult(__razor_helper_writer => {

#line 144 "..\..\Views\Provider\Detail.cshtml"
 
if (!string.IsNullOrEmpty(url))
{
    var linkProtocol = url.StartsWith("http") ? string.Empty : "http://";


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "        <dt");

WriteAttributeTo(__razor_helper_writer, "class", Tuple.Create(" class=\"", 6068), Tuple.Create("\"", 6087)

#line 148 "..\..\Views\Provider\Detail.cshtml"
, Tuple.Create(Tuple.Create("", 6076), Tuple.Create<System.Object, System.Int32>(classTitle

#line default
#line hidden
, 6076), false)
);

WriteLiteralTo(__razor_helper_writer, ">");


#line 148 "..\..\Views\Provider\Detail.cshtml"
  WriteTo(__razor_helper_writer, title);


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "</dt>\r\n");

WriteLiteralTo(__razor_helper_writer, "        <dd>\r\n            <a");

WriteAttributeTo(__razor_helper_writer, "href", Tuple.Create(" href=\"", 6130), Tuple.Create("\"", 6154)

#line 150 "..\..\Views\Provider\Detail.cshtml"
, Tuple.Create(Tuple.Create("", 6137), Tuple.Create<System.Object, System.Int32>(linkProtocol

#line default
#line hidden
, 6137), false)

#line 150 "..\..\Views\Provider\Detail.cshtml"
, Tuple.Create(Tuple.Create("", 6150), Tuple.Create<System.Object, System.Int32>(url

#line default
#line hidden
, 6150), false)
);

WriteLiteralTo(__razor_helper_writer, " rel=\"external\"");

WriteLiteralTo(__razor_helper_writer, " target=\"_blank\"");

WriteAttributeTo(__razor_helper_writer, "class", Tuple.Create(" class=\"", 6186), Tuple.Create("\"", 6220)

#line 150 "..\..\Views\Provider\Detail.cshtml"
, Tuple.Create(Tuple.Create("", 6194), Tuple.Create<System.Object, System.Int32>(cssClass

#line default
#line hidden
, 6194), false)

#line 150 "..\..\Views\Provider\Detail.cshtml"
       , Tuple.Create(Tuple.Create(" ", 6203), Tuple.Create<System.Object, System.Int32>(classIdentifier

#line default
#line hidden
, 6204), false)
);

WriteLiteralTo(__razor_helper_writer, ">\r\n");


#line 151 "..\..\Views\Provider\Detail.cshtml"
                

#line default
#line hidden

#line 151 "..\..\Views\Provider\Detail.cshtml"
                 if (string.IsNullOrEmpty(urlTitle))
                {
                    

#line default
#line hidden

#line 153 "..\..\Views\Provider\Detail.cshtml"
WriteTo(__razor_helper_writer, url);


#line default
#line hidden

#line 153 "..\..\Views\Provider\Detail.cshtml"
                        
                }
                else
                {
                    

#line default
#line hidden

#line 157 "..\..\Views\Provider\Detail.cshtml"
WriteTo(__razor_helper_writer, urlTitle);


#line default
#line hidden

#line 157 "..\..\Views\Provider\Detail.cshtml"
                             
                }


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "            </a>\r\n        </dd>\r\n");


#line 161 "..\..\Views\Provider\Detail.cshtml"
}



#line default
#line hidden
});

#line 163 "..\..\Views\Provider\Detail.cshtml"
}
#line default
#line hidden

#line 163 "..\..\Views\Provider\Detail.cshtml"
public System.Web.WebPages.HelperResult GetSatisfactionsHtml()
{
#line default
#line hidden
return new System.Web.WebPages.HelperResult(__razor_helper_writer => {

#line 164 "..\..\Views\Provider\Detail.cshtml"
 


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "    <p>\r\n        <b>Employer satisfaction:</b>\r\n        <span");

WriteLiteralTo(__razor_helper_writer, " id=\"employer-satisfaction\"");

WriteLiteralTo(__razor_helper_writer, ">\r\n");

WriteLiteralTo(__razor_helper_writer, "            ");


#line 168 "..\..\Views\Provider\Detail.cshtml"
WriteTo(__razor_helper_writer, Model.EmployerSatisfactionMessage);


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "\r\n        </span>\r\n");


#line 170 "..\..\Views\Provider\Detail.cshtml"
        

#line default
#line hidden

#line 170 "..\..\Views\Provider\Detail.cshtml"
         if (@Model.EmployerSatisfactionMessage != "No data available")
        {


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "            <div");

WriteLiteralTo(__razor_helper_writer, " class=\"progress-container\"");

WriteLiteralTo(__razor_helper_writer, ">\r\n                <div");

WriteLiteralTo(__razor_helper_writer, " class=\"progressbar\"");

WriteAttributeTo(__razor_helper_writer, "style", Tuple.Create(" style=\"", 6833), Tuple.Create("\"", 6876)
, Tuple.Create(Tuple.Create("", 6841), Tuple.Create("width:", 6841), true)

#line 173 "..\..\Views\Provider\Detail.cshtml"
, Tuple.Create(Tuple.Create(" ", 6847), Tuple.Create<System.Object, System.Int32>(Model.EmployerSatisfaction

#line default
#line hidden
, 6848), false)
, Tuple.Create(Tuple.Create("", 6875), Tuple.Create("%", 6875), true)
);

WriteLiteralTo(__razor_helper_writer, "></div>\r\n            </div>\r\n");


#line 175 "..\..\Views\Provider\Detail.cshtml"
        }


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "    </p>\r\n");


#line 177 "..\..\Views\Provider\Detail.cshtml"



#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "    <p>\r\n        <b>Learner satisfaction:</b>\r\n        <span");

WriteLiteralTo(__razor_helper_writer, " id=\"learner-satisfaction\"");

WriteLiteralTo(__razor_helper_writer, ">\r\n");

WriteLiteralTo(__razor_helper_writer, "            ");


#line 181 "..\..\Views\Provider\Detail.cshtml"
WriteTo(__razor_helper_writer, Model.LearnerSatisfactionMessage);


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "\r\n        </span>\r\n");


#line 183 "..\..\Views\Provider\Detail.cshtml"
        

#line default
#line hidden

#line 183 "..\..\Views\Provider\Detail.cshtml"
         if (@Model.LearnerSatisfactionMessage != "No data available")
        {


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "            <div");

WriteLiteralTo(__razor_helper_writer, " class=\"progress-container\"");

WriteLiteralTo(__razor_helper_writer, ">\r\n                <div");

WriteLiteralTo(__razor_helper_writer, " class=\"progressbar\"");

WriteAttributeTo(__razor_helper_writer, "style", Tuple.Create(" style=\"", 7251), Tuple.Create("\"", 7292)
, Tuple.Create(Tuple.Create("", 7259), Tuple.Create("width:", 7259), true)

#line 186 "..\..\Views\Provider\Detail.cshtml"
, Tuple.Create(Tuple.Create("", 7265), Tuple.Create<System.Object, System.Int32>(Model.LearnerSatisfaction

#line default
#line hidden
, 7265), false)
, Tuple.Create(Tuple.Create("", 7291), Tuple.Create("%", 7291), true)
);

WriteLiteralTo(__razor_helper_writer, "></div>\r\n            </div>\r\n");


#line 188 "..\..\Views\Provider\Detail.cshtml"
        }


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "\r\n    </p>\r\n");


#line 191 "..\..\Views\Provider\Detail.cshtml"


#line default
#line hidden
});

#line 191 "..\..\Views\Provider\Detail.cshtml"
}
#line default
#line hidden

#line 193 "..\..\Views\Provider\Detail.cshtml"
public System.Web.WebPages.HelperResult GetDeliveryModesHtml(string title, List<string> items, bool hideIfEmpty = false)
{
#line default
#line hidden
return new System.Web.WebPages.HelperResult(__razor_helper_writer => {

#line 194 "..\..\Views\Provider\Detail.cshtml"
 
if (items != null)
{


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "        <dt");

WriteLiteralTo(__razor_helper_writer, " class=\"training-structure\"");

WriteLiteralTo(__razor_helper_writer, ">");


#line 197 "..\..\Views\Provider\Detail.cshtml"
         WriteTo(__razor_helper_writer, title);


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "</dt>\r\n");

WriteLiteralTo(__razor_helper_writer, "        <dd");

WriteLiteralTo(__razor_helper_writer, " id=\"delivery-modes\"");

WriteLiteralTo(__razor_helper_writer, ">\r\n            <ul>\r\n");


#line 200 "..\..\Views\Provider\Detail.cshtml"
                

#line default
#line hidden

#line 200 "..\..\Views\Provider\Detail.cshtml"
                 if (items.Exists(m => m.Equals("DayRelease")))
                {


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "                    <li");

WriteLiteralTo(__razor_helper_writer, " class=\"day-release\"");

WriteLiteralTo(__razor_helper_writer, ">");


#line 202 "..\..\Views\Provider\Detail.cshtml"
              WriteTo(__razor_helper_writer, Html.Raw("day release"));


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "</li>\r\n");


#line 203 "..\..\Views\Provider\Detail.cshtml"
                }


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "                ");


#line 204 "..\..\Views\Provider\Detail.cshtml"
                 if (items.Exists(m => m.Equals("BlockRelease")))
                {


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "                    <li");

WriteLiteralTo(__razor_helper_writer, " class=\"block-release\"");

WriteLiteralTo(__razor_helper_writer, ">");


#line 206 "..\..\Views\Provider\Detail.cshtml"
                WriteTo(__razor_helper_writer, Html.Raw("block release"));


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "</li>\r\n");


#line 207 "..\..\Views\Provider\Detail.cshtml"
                }


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "                ");


#line 208 "..\..\Views\Provider\Detail.cshtml"
                 if (items.Exists(m => m.Equals("100PercentEmployer")))
                {


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "                    <li");

WriteLiteralTo(__razor_helper_writer, " class=\"hundred-percent-employer\"");

WriteLiteralTo(__razor_helper_writer, ">");


#line 210 "..\..\Views\Provider\Detail.cshtml"
                           WriteTo(__razor_helper_writer, Html.Raw("at your location"));


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "</li>\r\n");


#line 211 "..\..\Views\Provider\Detail.cshtml"
                }


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "            </ul>\r\n            <details>\r\n                <summary>Explain traini" +
"ng options</summary>\r\n                <div");

WriteLiteralTo(__razor_helper_writer, " class=\"panel panel-border-narrow\"");

WriteLiteralTo(__razor_helper_writer, ">\r\n                    <p>\r\n                        <span");

WriteLiteralTo(__razor_helper_writer, " class=\"bold-small\"");

WriteLiteralTo(__razor_helper_writer, ">Day release:</span> eg one day a week at the training provider\'s location.\r\n    " +
"                </p>\r\n                    <p>\r\n                        <span");

WriteLiteralTo(__razor_helper_writer, " class=\"bold-small\"");

WriteLiteralTo(__razor_helper_writer, ">Block release:</span> eg 3-4 weeks at the training provider\'s location.\r\n       " +
"             </p>\r\n                    <p>\r\n                        <span");

WriteLiteralTo(__razor_helper_writer, " class=\"bold-small\"");

WriteLiteralTo(__razor_helper_writer, ">At your location:</span> the provider comes to your workplace.\r\n                " +
"    </p>\r\n                </div>\r\n            </details>\r\n        </dd>\r\n");


#line 228 "..\..\Views\Provider\Detail.cshtml"
}


#line default
#line hidden
});

#line 229 "..\..\Views\Provider\Detail.cshtml"
}
#line default
#line hidden

#line 231 "..\..\Views\Provider\Detail.cshtml"
public System.Web.WebPages.HelperResult GetShortlistLink(string providerId, int apprenticeshipId, int locationId, bool isShortlisted)
{
#line default
#line hidden
return new System.Web.WebPages.HelperResult(__razor_helper_writer => {

#line 232 "..\..\Views\Provider\Detail.cshtml"
 
    if (Is<ShortlistingFeature>.Enabled) {
        if (Model.Training == ApprenticeshipTrainingType.Framework)
        {
            if (isShortlisted)
            {
                

#line default
#line hidden

#line 238 "..\..\Views\Provider\Detail.cshtml"
WriteTo(__razor_helper_writer, Html.ActionLink("Remove this training provider", "RemoveFrameworkProvider", "ShortList",
                    new {apprenticeshipId, providerId, locationId},
                    new
                    {
                        @class = "link shortlist-link provider-shortlist-link",
                        rel = "nofollow",
                        data_apprenticeship = apprenticeshipId,
                        data_provider = providerId,
                        data_location = locationId,
                        data_action = "remove",
                        data_apprenticeship_type = "Framework"
                    }));


#line default
#line hidden

#line 249 "..\..\Views\Provider\Detail.cshtml"
                      
            }
            else
            {
                

#line default
#line hidden

#line 253 "..\..\Views\Provider\Detail.cshtml"
WriteTo(__razor_helper_writer, Html.ActionLink("Shortlist this training provider", "AddFrameworkProvider", "ShortList",
                    new {apprenticeshipId, providerId, locationId},
                    new
                    {
                        @class = "link shortlist-link provider-shortlist-link",
                        rel = "nofollow",
                        data_apprenticeship = apprenticeshipId,
                        data_provider = providerId,
                        data_location = locationId,
                        data_action = "add",
                        data_apprenticeship_type = "Framework"
                    }));


#line default
#line hidden

#line 264 "..\..\Views\Provider\Detail.cshtml"
                      
            }
        }
        else
        {
            if (isShortlisted)
            {
                

#line default
#line hidden

#line 271 "..\..\Views\Provider\Detail.cshtml"
WriteTo(__razor_helper_writer, Html.ActionLink("Remove this training provider", "RemoveStandardProvider", "ShortList",
                    new {apprenticeshipId, providerId, locationId},
                    new
                    {
                        @class = "link shortlist-link provider-shortlist-link",
                        rel = "nofollow",
                        data_apprenticeship = apprenticeshipId,
                        data_provider = providerId,
                        data_location = locationId,
                        data_action = "remove",
                        data_apprenticeship_type = "Standard"
                    }));


#line default
#line hidden

#line 282 "..\..\Views\Provider\Detail.cshtml"
                      
            }
            else
            {
                

#line default
#line hidden

#line 286 "..\..\Views\Provider\Detail.cshtml"
WriteTo(__razor_helper_writer, Html.ActionLink("Shortlist this training provider", "AddStandardProvider", "ShortList",
                    new {apprenticeshipId, providerId, locationId},
                    new
                    {
                        @class = "link shortlist-link provider-shortlist-link",
                        rel = "nofollow",
                        data_apprenticeship = apprenticeshipId,
                        data_provider = providerId,
                        data_location = locationId,
                        data_action = "add",
                        data_apprenticeship_type = "Standard"
                    }));


#line default
#line hidden

#line 297 "..\..\Views\Provider\Detail.cshtml"
                      
            }
        }
    }


#line default
#line hidden
});

#line 301 "..\..\Views\Provider\Detail.cshtml"
}
#line default
#line hidden

        public Detail()
        {
        }
        public override void Execute()
        {
            
            #line 8 "..\..\Views\Provider\Detail.cshtml"
  
    ViewBag.Title = "Provider - " + @Model.Name;

            
            #line default
            #line hidden
WriteLiteral("\r\n<main");

WriteLiteral(" id=\"content\"");

WriteLiteral(" class=\"provider-detail\"");

WriteLiteral(">\r\n    <div");

WriteLiteral(" class=\"grid-row\"");

WriteLiteral(">\r\n        <div");

WriteLiteral(" class=\"column-two-thirds\"");

WriteLiteral(">\r\n            <div>\r\n                <h1");

WriteLiteral(" class=\"heading-xlarge\"");

WriteLiteral(" id=\"provider-name\"");

WriteLiteral(">\r\n");

WriteLiteral("                    ");

            
            #line 16 "..\..\Views\Provider\Detail.cshtml"
               Write(Model.Name);

            
            #line default
            #line hidden
WriteLiteral("\r\n                </h1>\r\n                <div");

WriteLiteral(" id=\"marketing\"");

WriteLiteral(" class=\"provider-marketing-info\"");

WriteLiteral(">\r\n");

WriteLiteral("                    ");

            
            #line 19 "..\..\Views\Provider\Detail.cshtml"
               Write(Html.MarkdownToHtml(Model.ProviderMarketingInfo));

            
            #line default
            #line hidden
WriteLiteral("\r\n                </div>\r\n            </div>\r\n\r\n            <section>\r\n          " +
"      <header");

WriteLiteral(" class=\"hgroup\"");

WriteLiteral(">\r\n                    <h2");

WriteLiteral(" class=\"heading-large apprenticeship-name-level\"");

WriteLiteral(">\r\n");

WriteLiteral("                        ");

            
            #line 26 "..\..\Views\Provider\Detail.cshtml"
                   Write(Model.ApprenticeshipNameWithLevel);

            
            #line default
            #line hidden
WriteLiteral("\r\n                    </h2>\r\n                    <b>Level:</b>\r\n                 " +
"   <span>\r\n");

WriteLiteral("                        ");

            
            #line 30 "..\..\Views\Provider\Detail.cshtml"
                   Write(Model.ApprenticeshipLevel);

            
            #line default
            #line hidden
WriteLiteral(" (equivalent to ");

            
            #line 30 "..\..\Views\Provider\Detail.cshtml"
                                                             Write(EquivalenveLevelService.GetApprenticeshipLevel(@Model.ApprenticeshipLevel));

            
            #line default
            #line hidden
WriteLiteral(")\r\n                    </span>\r\n                </header>\r\n                <dl");

WriteLiteral(" class=\"data-list\"");

WriteLiteral(">\r\n");

WriteLiteral("                    ");

            
            #line 34 "..\..\Views\Provider\Detail.cshtml"
               Write(GetStandardPropertyAsLinkHtml("Website", "course-link", "apprenticeshipContactTitle", "apprenticeshipContact", @Model.Apprenticeship.ApprenticeshipInfoUrl, "training provider website"));

            
            #line default
            #line hidden
WriteLiteral("\r\n");

WriteLiteral("                    ");

            
            #line 35 "..\..\Views\Provider\Detail.cshtml"
               Write(GetStandardPropertyHtml("Phone", "phone", Model.ContactInformation.Phone));

            
            #line default
            #line hidden
WriteLiteral("\r\n");

WriteLiteral("                    ");

            
            #line 36 "..\..\Views\Provider\Detail.cshtml"
               Write(GetEmailPropertyHtml("Email", "email", Model.ContactInformation.Email));

            
            #line default
            #line hidden
WriteLiteral("\r\n");

WriteLiteral("                    ");

            
            #line 37 "..\..\Views\Provider\Detail.cshtml"
               Write(GetStandardPropertyAsLinkHtml("Contact page", "contact-link", "providerContactTitle", "providerContact", @Model.ContactInformation.ContactUsUrl, "contact this training provider"));

            
            #line default
            #line hidden
WriteLiteral("\r\n");

WriteLiteral("                    ");

            
            #line 38 "..\..\Views\Provider\Detail.cshtml"
               Write(GetDeliveryModesHtml("Training options", Model.DeliveryModes));

            
            #line default
            #line hidden
WriteLiteral("\r\n");

WriteLiteral("                    ");

            
            #line 39 "..\..\Views\Provider\Detail.cshtml"
               Write(ShowTrainingLocation("Training location"));

            
            #line default
            #line hidden
WriteLiteral("\r\n                </dl>\r\n\r\n            </section>\r\n\r\n            <section>\r\n     " +
"           <header>\r\n                    <h2");

WriteLiteral(" class=\"heading-large\"");

WriteLiteral(">Apprenticeship training information</h2>\r\n                </header>\r\n           " +
"     <article");

WriteLiteral(" class=\"apprenticeship-marketing-info\"");

WriteLiteral(">\r\n");

WriteLiteral("                    ");

            
            #line 49 "..\..\Views\Provider\Detail.cshtml"
               Write(Html.MarkdownToHtml(Model.Apprenticeship.ApprenticeshipMarketingInfo));

            
            #line default
            #line hidden
WriteLiteral("\r\n                    <br />\r\n                    <em>Content maintained by ");

            
            #line 51 "..\..\Views\Provider\Detail.cshtml"
                                         Write(Model.Name);

            
            #line default
            #line hidden
WriteLiteral("</em>\r\n                </article>\r\n            </section>\r\n\r\n            <div>\r\n");

WriteLiteral("                ");

            
            #line 56 "..\..\Views\Provider\Detail.cshtml"
           Write(GetShortlistLink(@Model.ProviderId, @Model.Apprenticeship.Code, @Model.Location.LocationId, @Model.IsShortlisted));

            
            #line default
            #line hidden
WriteLiteral("\r\n            </div>\r\n\r\n        </div>\r\n\r\n        <div");

WriteLiteral(" class=\"column-third\"");

WriteLiteral(">\r\n            <div");

WriteLiteral(" class=\"hidden-for-mobile\"");

WriteLiteral(">\r\n");

WriteLiteral("                    ");

            
            #line 63 "..\..\Views\Provider\Detail.cshtml"
               Write(GetShortlistLink(@Model.ProviderId, @Model.Apprenticeship.Code, @Model.Location.LocationId, @Model.IsShortlisted));

            
            #line default
            #line hidden
WriteLiteral("\r\n                </div>\r\n\r\n");

            
            #line 66 "..\..\Views\Provider\Detail.cshtml"
            
            
            #line default
            #line hidden
            
            #line 66 "..\..\Views\Provider\Detail.cshtml"
             using (Html.BeginForm("StandardResults", "Provider", FormMethod.Get, new { @class = "search-box" }))
            {

            
            #line default
            #line hidden
WriteLiteral("                <div");

WriteLiteral(" class=\"related-container\"");

WriteLiteral(">\r\n                    <aside");

WriteLiteral(" class=\"related\"");

WriteLiteral(">\r\n                        <h2");

WriteLiteral(" class=\"heading-medium\"");

WriteLiteral(">\r\n                            Training provider quality assessment\r\n            " +
"            </h2>\r\n");

WriteLiteral("                        ");

            
            #line 73 "..\..\Views\Provider\Detail.cshtml"
                   Write(GetSatisfactionsHtml());

            
            #line default
            #line hidden
WriteLiteral("\r\n                    </aside>\r\n                </div>\r\n");

            
            #line 76 "..\..\Views\Provider\Detail.cshtml"
            }

            
            #line default
            #line hidden
WriteLiteral("\r\n        </div>\r\n    </div>\r\n    <div");

WriteLiteral(" class=\"grid-row\"");

WriteLiteral(">\r\n        <div");

WriteLiteral(" class=\"column-two-thirds\"");

WriteLiteral(">\r\n\r\n            <div");

WriteLiteral(" class=\"survey-panel\"");

WriteLiteral(">\r\n                <h2");

WriteLiteral(" class=\"bold-large\"");

WriteLiteral(@">
                    Give us your feedback
                </h2>
                <p>
                    This is a new service and your feedback will help us improve it.<br />
                    Use the link below to take part in a short survey.
                </p>
                <a");

WriteLiteral(" href=\"https://www.surveymonkey.co.uk/r/F3LCBG6\"");

WriteLiteral(" target=\"_blank\"");

WriteLiteral(" class=\"button\"");

WriteLiteral(">Take the survey</a>\r\n            </div>\r\n            <aside");

WriteLiteral(" class=\"disclaimer\"");

WriteLiteral(">\r\n                <h3");

WriteLiteral(" class=\"heading-small\"");

WriteLiteral(@">Content disclaimer</h3>
                <p>
                    Skills Funding Agency cannot guarantee the accuracy of course information on this site and makes no representations about the quality of any courses which appear on the site. Skills Funding Agency is not liable for any losses suffered as a result of any party relying on the course information provided.
                </p>
            </aside>
        </div>
    </div>

</main>

");

WriteLiteral("\r\n");

WriteLiteral("\r\n");

WriteLiteral("\r\n");

WriteLiteral("\r\n");

WriteLiteral("\r\n");

WriteLiteral("\r\n");

        }
    }
}
#pragma warning restore 1591
