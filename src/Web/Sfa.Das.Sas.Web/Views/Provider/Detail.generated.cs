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
    
    #line 6 "..\..\Views\Provider\Detail.cshtml"
    using Sfa.Das.Sas.ApplicationServices.Models;
    
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

#line 103 "..\..\Views\Provider\Detail.cshtml"
public System.Web.WebPages.HelperResult ShowTrainingLocation(string title)
{
#line default
#line hidden
return new System.Web.WebPages.HelperResult(__razor_helper_writer => {

#line 104 "..\..\Views\Provider\Detail.cshtml"
 
if (@Model != null)
{


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "        <dt");

WriteLiteralTo(__razor_helper_writer, " class=\"training-location-title\"");

WriteLiteralTo(__razor_helper_writer, ">");


#line 107 "..\..\Views\Provider\Detail.cshtml"
              WriteTo(__razor_helper_writer, title);


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "</dt>\r\n");


#line 108 "..\..\Views\Provider\Detail.cshtml"

    if (@Model.DeliveryModes.Count == 1 && @Model.DeliveryModes.Contains("100PercentEmployer"))
    {


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "            <dd");

WriteLiteralTo(__razor_helper_writer, " id=\"training-location\"");

WriteLiteralTo(__razor_helper_writer, " class=\"training-location\"");

WriteLiteralTo(__razor_helper_writer, ">\r\n                Training takes place at your location\r\n            </dd>\r\n");


#line 114 "..\..\Views\Provider\Detail.cshtml"
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


#line 118 "..\..\Views\Provider\Detail.cshtml"
WriteTo(__razor_helper_writer, Model.Location.LocationName);


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, " ");


#line 118 "..\..\Views\Provider\Detail.cshtml"
               WriteTo(__razor_helper_writer, Model.Address.Address1);


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, " ");


#line 118 "..\..\Views\Provider\Detail.cshtml"
                                       WriteTo(__razor_helper_writer, Model.Address.Address2);


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, " ");


#line 118 "..\..\Views\Provider\Detail.cshtml"
                                                               WriteTo(__razor_helper_writer, Model.Address.Town);


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, " ");


#line 118 "..\..\Views\Provider\Detail.cshtml"
                                                                                   WriteTo(__razor_helper_writer, Model.Address.County);


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, " ");


#line 118 "..\..\Views\Provider\Detail.cshtml"
                                                                                                         WriteTo(__razor_helper_writer, Model.Address.Postcode);


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "\r\n            </dd>\r\n");


#line 120 "..\..\Views\Provider\Detail.cshtml"
    }
}


#line default
#line hidden
});

#line 122 "..\..\Views\Provider\Detail.cshtml"
}
#line default
#line hidden

#line 124 "..\..\Views\Provider\Detail.cshtml"
public System.Web.WebPages.HelperResult GetStandardPropertyHtml(string title, string id, string item, bool hideIfEmpty = false)
{
#line default
#line hidden
return new System.Web.WebPages.HelperResult(__razor_helper_writer => {

#line 125 "..\..\Views\Provider\Detail.cshtml"
 
if (!string.IsNullOrEmpty(item) || !hideIfEmpty)
{


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "        <dt");

WriteLiteralTo(__razor_helper_writer, " class=\"phone-title\"");

WriteLiteralTo(__razor_helper_writer, ">");


#line 128 "..\..\Views\Provider\Detail.cshtml"
  WriteTo(__razor_helper_writer, title);


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "</dt>\r\n");

WriteLiteralTo(__razor_helper_writer, "        <dd");

WriteAttributeTo(__razor_helper_writer, "id", Tuple.Create(" id=\"", 5298), Tuple.Create("\"", 5306)

#line 129 "..\..\Views\Provider\Detail.cshtml"
, Tuple.Create(Tuple.Create("", 5303), Tuple.Create<System.Object, System.Int32>(id

#line default
#line hidden
, 5303), false)
);

WriteLiteralTo(__razor_helper_writer, " class=\"phone\"");

WriteLiteralTo(__razor_helper_writer, ">");


#line 129 "..\..\Views\Provider\Detail.cshtml"
     WriteTo(__razor_helper_writer, Html.Raw(item));


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "</dd>\r\n");


#line 130 "..\..\Views\Provider\Detail.cshtml"
}


#line default
#line hidden
});

#line 131 "..\..\Views\Provider\Detail.cshtml"
}
#line default
#line hidden

#line 133 "..\..\Views\Provider\Detail.cshtml"
public System.Web.WebPages.HelperResult GetEmailPropertyHtml(string title, string id, string item, bool hideIfEmpty = false)
{
#line default
#line hidden
return new System.Web.WebPages.HelperResult(__razor_helper_writer => {

#line 134 "..\..\Views\Provider\Detail.cshtml"
 
if (!string.IsNullOrEmpty(item) || !hideIfEmpty)
{


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "        <dt");

WriteLiteralTo(__razor_helper_writer, " class=\"email-title\"");

WriteLiteralTo(__razor_helper_writer, ">");


#line 137 "..\..\Views\Provider\Detail.cshtml"
  WriteTo(__razor_helper_writer, title);


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "</dt>\r\n");

WriteLiteralTo(__razor_helper_writer, "        <dd");

WriteAttributeTo(__razor_helper_writer, "id", Tuple.Create(" id=\"", 5558), Tuple.Create("\"", 5566)

#line 138 "..\..\Views\Provider\Detail.cshtml"
, Tuple.Create(Tuple.Create("", 5563), Tuple.Create<System.Object, System.Int32>(id

#line default
#line hidden
, 5563), false)
);

WriteLiteralTo(__razor_helper_writer, " class=\"email\"");

WriteLiteralTo(__razor_helper_writer, "><a");

WriteAttributeTo(__razor_helper_writer, "href", Tuple.Create(" href=\"", 5584), Tuple.Create("\"", 5613)
, Tuple.Create(Tuple.Create("", 5591), Tuple.Create("mailto:", 5591), true)

#line 138 "..\..\Views\Provider\Detail.cshtml"
, Tuple.Create(Tuple.Create("", 5598), Tuple.Create<System.Object, System.Int32>(Html.Raw(item)

#line default
#line hidden
, 5598), false)
);

WriteLiteralTo(__razor_helper_writer, ">");


#line 138 "..\..\Views\Provider\Detail.cshtml"
                                      WriteTo(__razor_helper_writer, Html.Raw(item));


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "</a></dd>\r\n");


#line 139 "..\..\Views\Provider\Detail.cshtml"
}


#line default
#line hidden
});

#line 140 "..\..\Views\Provider\Detail.cshtml"
}
#line default
#line hidden

#line 142 "..\..\Views\Provider\Detail.cshtml"
public System.Web.WebPages.HelperResult GetStandardPropertyAsLinkHtml(string title, string cssClass, string classTitle, string classIdentifier, string url, string urlTitle = "")
{
#line default
#line hidden
return new System.Web.WebPages.HelperResult(__razor_helper_writer => {

#line 143 "..\..\Views\Provider\Detail.cshtml"
 
if (!string.IsNullOrEmpty(url))
{
    var linkProtocol = url.StartsWith("http") ? string.Empty : "http://";


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "        <dt");

WriteAttributeTo(__razor_helper_writer, "class", Tuple.Create(" class=\"", 5921), Tuple.Create("\"", 5940)

#line 147 "..\..\Views\Provider\Detail.cshtml"
, Tuple.Create(Tuple.Create("", 5929), Tuple.Create<System.Object, System.Int32>(classTitle

#line default
#line hidden
, 5929), false)
);

WriteLiteralTo(__razor_helper_writer, ">");


#line 147 "..\..\Views\Provider\Detail.cshtml"
  WriteTo(__razor_helper_writer, title);


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "</dt>\r\n");

WriteLiteralTo(__razor_helper_writer, "        <dd>\r\n            <a");

WriteAttributeTo(__razor_helper_writer, "href", Tuple.Create(" href=\"", 5983), Tuple.Create("\"", 6007)

#line 149 "..\..\Views\Provider\Detail.cshtml"
, Tuple.Create(Tuple.Create("", 5990), Tuple.Create<System.Object, System.Int32>(linkProtocol

#line default
#line hidden
, 5990), false)

#line 149 "..\..\Views\Provider\Detail.cshtml"
, Tuple.Create(Tuple.Create("", 6003), Tuple.Create<System.Object, System.Int32>(url

#line default
#line hidden
, 6003), false)
);

WriteLiteralTo(__razor_helper_writer, " rel=\"external\"");

WriteLiteralTo(__razor_helper_writer, " target=\"_blank\"");

WriteAttributeTo(__razor_helper_writer, "class", Tuple.Create(" class=\"", 6039), Tuple.Create("\"", 6073)

#line 149 "..\..\Views\Provider\Detail.cshtml"
, Tuple.Create(Tuple.Create("", 6047), Tuple.Create<System.Object, System.Int32>(cssClass

#line default
#line hidden
, 6047), false)

#line 149 "..\..\Views\Provider\Detail.cshtml"
       , Tuple.Create(Tuple.Create(" ", 6056), Tuple.Create<System.Object, System.Int32>(classIdentifier

#line default
#line hidden
, 6057), false)
);

WriteLiteralTo(__razor_helper_writer, ">\r\n");


#line 150 "..\..\Views\Provider\Detail.cshtml"
                

#line default
#line hidden

#line 150 "..\..\Views\Provider\Detail.cshtml"
                 if (string.IsNullOrEmpty(urlTitle))
                {
                    

#line default
#line hidden

#line 152 "..\..\Views\Provider\Detail.cshtml"
WriteTo(__razor_helper_writer, url);


#line default
#line hidden

#line 152 "..\..\Views\Provider\Detail.cshtml"
                        
                }
                else
                {
                    

#line default
#line hidden

#line 156 "..\..\Views\Provider\Detail.cshtml"
WriteTo(__razor_helper_writer, urlTitle);


#line default
#line hidden

#line 156 "..\..\Views\Provider\Detail.cshtml"
                             
                }


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "            </a>\r\n        </dd>\r\n");


#line 160 "..\..\Views\Provider\Detail.cshtml"
}



#line default
#line hidden
});

#line 162 "..\..\Views\Provider\Detail.cshtml"
}
#line default
#line hidden

#line 162 "..\..\Views\Provider\Detail.cshtml"
public System.Web.WebPages.HelperResult GetSatisfactionsHtml()
 {
#line default
#line hidden
return new System.Web.WebPages.HelperResult(__razor_helper_writer => {

#line 163 "..\..\Views\Provider\Detail.cshtml"
  


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "     <div");

WriteLiteralTo(__razor_helper_writer, " class=\"rates-list\"");

WriteLiteralTo(__razor_helper_writer, ">\r\n         <div>\r\n             <b>Employer satisfaction:</b>\r\n             <span" +
"");

WriteLiteralTo(__razor_helper_writer, " id=\"employer-satisfaction\"");

WriteLiteralTo(__razor_helper_writer, ">\r\n");

WriteLiteralTo(__razor_helper_writer, "            ");


#line 168 "..\..\Views\Provider\Detail.cshtml"
WriteTo(__razor_helper_writer, Model.EmployerSatisfactionMessage);


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "\r\n        </span>\r\n");

WriteLiteralTo(__razor_helper_writer, "             ");


#line 170 "..\..\Views\Provider\Detail.cshtml"
WriteTo(__razor_helper_writer, CreateProgressBar(Model.EmployerSatisfactionMessage, Model.EmployerSatisfaction));


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "\r\n         </div>\r\n\r\n\r\n         <div>\r\n             <b>Learner satisfaction:</b>\r" +
"\n             <span");

WriteLiteralTo(__razor_helper_writer, " id=\"learner-satisfaction\"");

WriteLiteralTo(__razor_helper_writer, ">\r\n");

WriteLiteralTo(__razor_helper_writer, "            ");


#line 177 "..\..\Views\Provider\Detail.cshtml"
WriteTo(__razor_helper_writer, Model.LearnerSatisfactionMessage);


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "\r\n        </span>\r\n");

WriteLiteralTo(__razor_helper_writer, "             ");


#line 179 "..\..\Views\Provider\Detail.cshtml"
WriteTo(__razor_helper_writer, CreateProgressBar(Model.LearnerSatisfactionMessage, (int)Model.LearnerSatisfaction));


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "\r\n\r\n         </div>\r\n\r\n");


#line 183 "..\..\Views\Provider\Detail.cshtml"
         

#line default
#line hidden

#line 183 "..\..\Views\Provider\Detail.cshtml"
          if (!(Model.EmployerSatisfactionMessage == "no data available" && Model.LearnerSatisfactionMessage == "no data available"))
         {


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "         <p");

WriteLiteralTo(__razor_helper_writer, " class=\"satisfaction-source\"");

WriteLiteralTo(__razor_helper_writer, ">\r\n             Source: <a");

WriteLiteralTo(__razor_helper_writer, " href=\"https://www.gov.uk/government/collections/fe-choices-information-for-provi" +
"ders\"");

WriteLiteralTo(__razor_helper_writer, " target=\"_blank\"");

WriteLiteralTo(__razor_helper_writer, ">Skills Funding Agency FE Choices</a>\r\n         </p>\r\n");


#line 188 "..\..\Views\Provider\Detail.cshtml"
         }


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "         <hr/>\r\n\r\n         <div>\r\n             <b>Achievement rate:</b>\r\n        " +
"     <span");

WriteLiteralTo(__razor_helper_writer, " class=\"national-rate\"");

WriteLiteralTo(__razor_helper_writer, ">\r\n");

WriteLiteralTo(__razor_helper_writer, "            ");


#line 194 "..\..\Views\Provider\Detail.cshtml"
WriteTo(__razor_helper_writer, Model.AchievementRateMessage);


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "\r\n        </span>\r\n");


#line 196 "..\..\Views\Provider\Detail.cshtml"
             

#line default
#line hidden

#line 196 "..\..\Views\Provider\Detail.cshtml"
              if (Model.AchievementRateMessage != "no data available")
             {
                 

#line default
#line hidden

#line 198 "..\..\Views\Provider\Detail.cshtml"
WriteTo(__razor_helper_writer, CreateProgressBar(Model.AchievementRateMessage, Model.AchievementRate));


#line default
#line hidden

#line 198 "..\..\Views\Provider\Detail.cshtml"
                                                                                        


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "                 <p>Cohort size: ");


#line 199 "..\..\Views\Provider\Detail.cshtml"
   WriteTo(__razor_helper_writer, Model.OverallCohort);


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "</p>\r\n");


#line 200 "..\..\Views\Provider\Detail.cshtml"
             }


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "         </div>\r\n");


#line 202 "..\..\Views\Provider\Detail.cshtml"
        

#line default
#line hidden

#line 202 "..\..\Views\Provider\Detail.cshtml"
         if (Model.AchievementRateMessage != "no data available")
        {


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "         <div>\r\n             <b>National achievement:</b>\r\n             <span");

WriteLiteralTo(__razor_helper_writer, " class=\"national-rate\"");

WriteLiteralTo(__razor_helper_writer, ">\r\n");

WriteLiteralTo(__razor_helper_writer, "             ");


#line 207 "..\..\Views\Provider\Detail.cshtml"
WriteTo(__razor_helper_writer, Model.NationalAchievementRateMessage);


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "\r\n         </span>\r\n");

WriteLiteralTo(__razor_helper_writer, "             ");


#line 209 "..\..\Views\Provider\Detail.cshtml"
WriteTo(__razor_helper_writer, CreateProgressBar(Model.NationalAchievementRateMessage, Model.NationalAchievementRate));


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "\r\n         </div>\r\n");


#line 211 "..\..\Views\Provider\Detail.cshtml"
        }


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "     </div>\r\n");


#line 213 "..\..\Views\Provider\Detail.cshtml"


#line default
#line hidden
});

#line 213 "..\..\Views\Provider\Detail.cshtml"
}
#line default
#line hidden

#line 215 "..\..\Views\Provider\Detail.cshtml"
public System.Web.WebPages.HelperResult CreateProgressBar(string message, int progress)
{
#line default
#line hidden
return new System.Web.WebPages.HelperResult(__razor_helper_writer => {

#line 216 "..\..\Views\Provider\Detail.cshtml"
 
    if (message != "no data available")
    {


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "        <div");

WriteLiteralTo(__razor_helper_writer, " class=\"progress-container\"");

WriteLiteralTo(__razor_helper_writer, ">\r\n            <div");

WriteLiteralTo(__razor_helper_writer, " class=\"progressbar\"");

WriteAttributeTo(__razor_helper_writer, "style", Tuple.Create(" style=\"", 8382), Tuple.Create("\"", 8407)
, Tuple.Create(Tuple.Create("", 8390), Tuple.Create("width:", 8390), true)

#line 220 "..\..\Views\Provider\Detail.cshtml"
, Tuple.Create(Tuple.Create(" ", 8396), Tuple.Create<System.Object, System.Int32>(progress

#line default
#line hidden
, 8397), false)
, Tuple.Create(Tuple.Create("", 8406), Tuple.Create("%", 8406), true)
);

WriteLiteralTo(__razor_helper_writer, "></div>\r\n        </div>\r\n");


#line 222 "..\..\Views\Provider\Detail.cshtml"
    }


#line default
#line hidden
});

#line 223 "..\..\Views\Provider\Detail.cshtml"
}
#line default
#line hidden

#line 225 "..\..\Views\Provider\Detail.cshtml"
public System.Web.WebPages.HelperResult GetDeliveryModesHtml(string title, List<string> items, bool hideIfEmpty = false)
{
#line default
#line hidden
return new System.Web.WebPages.HelperResult(__razor_helper_writer => {

#line 226 "..\..\Views\Provider\Detail.cshtml"
 
if (items != null)
{


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "        <dt");

WriteLiteralTo(__razor_helper_writer, " class=\"training-structure\"");

WriteLiteralTo(__razor_helper_writer, ">");


#line 229 "..\..\Views\Provider\Detail.cshtml"
         WriteTo(__razor_helper_writer, title);


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "</dt>\r\n");

WriteLiteralTo(__razor_helper_writer, "        <dd");

WriteLiteralTo(__razor_helper_writer, " id=\"delivery-modes\"");

WriteLiteralTo(__razor_helper_writer, ">\r\n            <ul>\r\n");


#line 232 "..\..\Views\Provider\Detail.cshtml"
                

#line default
#line hidden

#line 232 "..\..\Views\Provider\Detail.cshtml"
                 if (items.Exists(m => m.Equals("DayRelease")))
                {


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "                    <li");

WriteLiteralTo(__razor_helper_writer, " class=\"day-release\"");

WriteLiteralTo(__razor_helper_writer, ">");


#line 234 "..\..\Views\Provider\Detail.cshtml"
              WriteTo(__razor_helper_writer, Html.Raw("day release"));


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "</li>\r\n");


#line 235 "..\..\Views\Provider\Detail.cshtml"
                }


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "                ");


#line 236 "..\..\Views\Provider\Detail.cshtml"
                 if (items.Exists(m => m.Equals("BlockRelease")))
                {


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "                    <li");

WriteLiteralTo(__razor_helper_writer, " class=\"block-release\"");

WriteLiteralTo(__razor_helper_writer, ">");


#line 238 "..\..\Views\Provider\Detail.cshtml"
                WriteTo(__razor_helper_writer, Html.Raw("block release"));


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "</li>\r\n");


#line 239 "..\..\Views\Provider\Detail.cshtml"
                }


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "                ");


#line 240 "..\..\Views\Provider\Detail.cshtml"
                 if (items.Exists(m => m.Equals("100PercentEmployer")))
                {


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "                    <li");

WriteLiteralTo(__razor_helper_writer, " class=\"hundred-percent-employer\"");

WriteLiteralTo(__razor_helper_writer, ">");


#line 242 "..\..\Views\Provider\Detail.cshtml"
                           WriteTo(__razor_helper_writer, Html.Raw("at your location"));


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "</li>\r\n");


#line 243 "..\..\Views\Provider\Detail.cshtml"
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


#line 260 "..\..\Views\Provider\Detail.cshtml"
}


#line default
#line hidden
});

#line 261 "..\..\Views\Provider\Detail.cshtml"
}
#line default
#line hidden

#line 263 "..\..\Views\Provider\Detail.cshtml"
public System.Web.WebPages.HelperResult GetShortlistLink(string ukprn, int apprenticeshipId, int locationId, bool isShortlisted)
{
#line default
#line hidden
return new System.Web.WebPages.HelperResult(__razor_helper_writer => {

#line 264 "..\..\Views\Provider\Detail.cshtml"
 
    if (Is<ShortlistingFeature>.Enabled) {
        if (Model.ApprenticeshipType == ApprenticeshipTrainingType.Framework)
        {
            if (isShortlisted)
            {
                

#line default
#line hidden

#line 270 "..\..\Views\Provider\Detail.cshtml"
WriteTo(__razor_helper_writer, Html.ActionLink("Remove this training provider", "RemoveFrameworkProvider", "ShortList",
                    new {apprenticeshipId, ukprn, locationId},
                    new
                    {
                        @class = "link shortlist-link provider-shortlist-link",
                        rel = "nofollow",
                        data_apprenticeship = apprenticeshipId,
                        data_provider = ukprn,
                        data_location = locationId,
                        data_action = "remove",
                        data_apprenticeship_type = "Framework"
                    }));


#line default
#line hidden

#line 281 "..\..\Views\Provider\Detail.cshtml"
                      
            }
            else
            {
                

#line default
#line hidden

#line 285 "..\..\Views\Provider\Detail.cshtml"
WriteTo(__razor_helper_writer, Html.ActionLink("Shortlist this training provider", "AddFrameworkProvider", "ShortList",
                    new {apprenticeshipId, ukprn, locationId},
                    new
                    {
                        @class = "link shortlist-link provider-shortlist-link",
                        rel = "nofollow",
                        data_apprenticeship = apprenticeshipId,
                        data_provider = ukprn,
                        data_location = locationId,
                        data_action = "add",
                        data_apprenticeship_type = "Framework"
                    }));


#line default
#line hidden

#line 296 "..\..\Views\Provider\Detail.cshtml"
                      
            }
        }
        else
        {
            if (isShortlisted)
            {
                

#line default
#line hidden

#line 303 "..\..\Views\Provider\Detail.cshtml"
WriteTo(__razor_helper_writer, Html.ActionLink("Remove this training provider", "RemoveStandardProvider", "ShortList",
                    new {apprenticeshipId, ukprn, locationId},
                    new
                    {
                        @class = "link shortlist-link provider-shortlist-link",
                        rel = "nofollow",
                        data_apprenticeship = apprenticeshipId,
                        data_provider = ukprn,
                        data_location = locationId,
                        data_action = "remove",
                        data_apprenticeship_type = "Standard"
                    }));


#line default
#line hidden

#line 314 "..\..\Views\Provider\Detail.cshtml"
                      
            }
            else
            {
                

#line default
#line hidden

#line 318 "..\..\Views\Provider\Detail.cshtml"
WriteTo(__razor_helper_writer, Html.ActionLink("Shortlist this training provider", "AddStandardProvider", "ShortList",
                    new {apprenticeshipId, ukprn, locationId},
                    new
                    {
                        @class = "link shortlist-link provider-shortlist-link",
                        rel = "nofollow",
                        data_apprenticeship = apprenticeshipId,
                        data_provider = ukprn,
                        data_location = locationId,
                        data_action = "add",
                        data_apprenticeship_type = "Standard"
                    }));


#line default
#line hidden

#line 329 "..\..\Views\Provider\Detail.cshtml"
                      
            }
        }
    }


#line default
#line hidden
});

#line 333 "..\..\Views\Provider\Detail.cshtml"
}
#line default
#line hidden

        public Detail()
        {
        }
        public override void Execute()
        {
            
            #line 9 "..\..\Views\Provider\Detail.cshtml"
  
    ViewBag.Title = @Model.Name + " - Apprenticeship Provider";

            
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

            
            #line 17 "..\..\Views\Provider\Detail.cshtml"
               Write(Model.Name);

            
            #line default
            #line hidden
WriteLiteral("\r\n                </h1>\r\n                <div");

WriteLiteral(" id=\"marketing\"");

WriteLiteral(" class=\"provider-marketing-info\"");

WriteLiteral(">\r\n");

WriteLiteral("                    ");

            
            #line 20 "..\..\Views\Provider\Detail.cshtml"
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

            
            #line 27 "..\..\Views\Provider\Detail.cshtml"
                   Write(Model.ApprenticeshipNameWithLevel);

            
            #line default
            #line hidden
WriteLiteral("\r\n                    </h2>\r\n                    <b>Level:</b>\r\n                 " +
"   <span>\r\n");

WriteLiteral("                        ");

            
            #line 31 "..\..\Views\Provider\Detail.cshtml"
                   Write(Model.ApprenticeshipLevel);

            
            #line default
            #line hidden
WriteLiteral(" (equivalent to ");

            
            #line 31 "..\..\Views\Provider\Detail.cshtml"
                                                             Write(EquivalenveLevelService.GetApprenticeshipLevel(@Model.ApprenticeshipLevel));

            
            #line default
            #line hidden
WriteLiteral(")\r\n                    </span>\r\n                </header>\r\n                <dl");

WriteLiteral(" class=\"data-list\"");

WriteLiteral(">\r\n");

WriteLiteral("                    ");

            
            #line 35 "..\..\Views\Provider\Detail.cshtml"
               Write(GetStandardPropertyAsLinkHtml("Website", "course-link", "apprenticeshipContactTitle", "apprenticeshipContact", @Model.Apprenticeship.ApprenticeshipInfoUrl, "training provider website"));

            
            #line default
            #line hidden
WriteLiteral("\r\n");

WriteLiteral("                    ");

            
            #line 36 "..\..\Views\Provider\Detail.cshtml"
               Write(GetStandardPropertyHtml("Phone", "phone", Model.ContactInformation.Phone));

            
            #line default
            #line hidden
WriteLiteral("\r\n");

WriteLiteral("                    ");

            
            #line 37 "..\..\Views\Provider\Detail.cshtml"
               Write(GetEmailPropertyHtml("Email", "email", Model.ContactInformation.Email));

            
            #line default
            #line hidden
WriteLiteral("\r\n");

WriteLiteral("                    ");

            
            #line 38 "..\..\Views\Provider\Detail.cshtml"
               Write(GetStandardPropertyAsLinkHtml("Contact page", "contact-link", "providerContactTitle", "providerContact", @Model.ContactInformation.ContactUsUrl, "contact this training provider"));

            
            #line default
            #line hidden
WriteLiteral("\r\n");

WriteLiteral("                    ");

            
            #line 39 "..\..\Views\Provider\Detail.cshtml"
               Write(GetDeliveryModesHtml("Training options", Model.DeliveryModes));

            
            #line default
            #line hidden
WriteLiteral("\r\n");

WriteLiteral("                    ");

            
            #line 40 "..\..\Views\Provider\Detail.cshtml"
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

            
            #line 50 "..\..\Views\Provider\Detail.cshtml"
               Write(Html.MarkdownToHtml(Model.Apprenticeship.ApprenticeshipMarketingInfo));

            
            #line default
            #line hidden
WriteLiteral("\r\n                    <br />\r\n                    <em>Content maintained by ");

            
            #line 52 "..\..\Views\Provider\Detail.cshtml"
                                         Write(Model.Name);

            
            #line default
            #line hidden
WriteLiteral("</em>\r\n                </article>\r\n            </section>\r\n\r\n            <div>\r\n");

WriteLiteral("                ");

            
            #line 57 "..\..\Views\Provider\Detail.cshtml"
           Write(GetShortlistLink(@Model.Ukprn, @Model.Apprenticeship.Code, @Model.Location.LocationId, @Model.IsShortlisted));

            
            #line default
            #line hidden
WriteLiteral("\r\n            </div>\r\n\r\n        </div>\r\n\r\n        <div");

WriteLiteral(" class=\"column-third\"");

WriteLiteral(">\r\n            <div");

WriteLiteral(" class=\"hidden-for-mobile\"");

WriteLiteral(">\r\n");

WriteLiteral("                    ");

            
            #line 64 "..\..\Views\Provider\Detail.cshtml"
               Write(GetShortlistLink(@Model.Ukprn, @Model.Apprenticeship.Code, @Model.Location.LocationId, @Model.IsShortlisted));

            
            #line default
            #line hidden
WriteLiteral("\r\n                </div>\r\n\r\n            <div");

WriteLiteral(" class=\"related-container\"");

WriteLiteral(">\r\n                <aside");

WriteLiteral(" class=\"related\"");

WriteLiteral(">\r\n                    <h2");

WriteLiteral(" class=\"heading-medium\"");

WriteLiteral(">\r\n                        Training provider quality assessment\r\n                " +
"    </h2>\r\n");

WriteLiteral("                    ");

            
            #line 72 "..\..\Views\Provider\Detail.cshtml"
               Write(GetSatisfactionsHtml());

            
            #line default
            #line hidden
WriteLiteral("\r\n                </aside>\r\n            </div>\r\n        </div>\r\n    </div>\r\n    <" +
"div");

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

WriteAttribute("href", Tuple.Create(" href=\"", 3790), Tuple.Create("\"", 3813)
            
            #line 88 "..\..\Views\Provider\Detail.cshtml"
, Tuple.Create(Tuple.Create("", 3797), Tuple.Create<System.Object, System.Int32>(Model.SurveyUrl
            
            #line default
            #line hidden
, 3797), false)
);

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

WriteLiteral("\r\n");

        }
    }
}
#pragma warning restore 1591
