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

namespace Sfa.Eds.Das.Web.Views.Provider
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
    using Sfa.Eds.Das.Web;
    
    #line 1 "..\..\Views\Provider\Detail.cshtml"
    using Sfa.Eds.Das.Web.Extensions;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/Provider/Detail.cshtml")]
    public partial class Detail : System.Web.Mvc.WebViewPage<Sfa.Eds.Das.Web.ViewModels.ProviderViewModel>
    {

#line 65 "..\..\Views\Provider\Detail.cshtml"
public System.Web.WebPages.HelperResult ShowTrainingLocation(string title)
{
#line default
#line hidden
return new System.Web.WebPages.HelperResult(__razor_helper_writer => {

#line 66 "..\..\Views\Provider\Detail.cshtml"
 
if (@Model != null)
{


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "        <dt>");


#line 69 "..\..\Views\Provider\Detail.cshtml"
WriteTo(__razor_helper_writer, title);


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "</dt>\r\n");


#line 70 "..\..\Views\Provider\Detail.cshtml"

    if (@Model.DeliveryModes.Count == 1 && @Model.DeliveryModes.Contains("100PercentEmployer"))
    {


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "            <dd");

WriteLiteralTo(__razor_helper_writer, " id=\"training-location\"");

WriteLiteralTo(__razor_helper_writer, ">Training will take place at your location</dd>\r\n");


#line 74 "..\..\Views\Provider\Detail.cshtml"
    }
    else
    {


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "            <dd");

WriteLiteralTo(__razor_helper_writer, " id=\"training-location\"");

WriteLiteralTo(__razor_helper_writer, ">");


#line 77 "..\..\Views\Provider\Detail.cshtml"
         WriteTo(__razor_helper_writer, Model.Location.LocationName);


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, " ");


#line 77 "..\..\Views\Provider\Detail.cshtml"
                                      WriteTo(__razor_helper_writer, Model.Address.Address1);


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, " ");


#line 77 "..\..\Views\Provider\Detail.cshtml"
                                                              WriteTo(__razor_helper_writer, Model.Address.Address2);


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, " ");


#line 77 "..\..\Views\Provider\Detail.cshtml"
                                                                                      WriteTo(__razor_helper_writer, Model.Address.Postcode);


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "</dd>\r\n");


#line 78 "..\..\Views\Provider\Detail.cshtml"
    }
}


#line default
#line hidden
});

#line 80 "..\..\Views\Provider\Detail.cshtml"
}
#line default
#line hidden

#line 82 "..\..\Views\Provider\Detail.cshtml"
public System.Web.WebPages.HelperResult GetStandardPropertyHtml(string title, string id, string item, bool hideIfEmpty = false)
{
#line default
#line hidden
return new System.Web.WebPages.HelperResult(__razor_helper_writer => {

#line 83 "..\..\Views\Provider\Detail.cshtml"
 
    if (!string.IsNullOrEmpty(item) || !hideIfEmpty)
    {


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "        <dt>");


#line 86 "..\..\Views\Provider\Detail.cshtml"
WriteTo(__razor_helper_writer, title);


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "</dt>\r\n");

WriteLiteralTo(__razor_helper_writer, "        <dd");

WriteAttributeTo(__razor_helper_writer, "id", Tuple.Create(" id=\"", 2963), Tuple.Create("\"", 2971)

#line 87 "..\..\Views\Provider\Detail.cshtml"
, Tuple.Create(Tuple.Create("", 2968), Tuple.Create<System.Object, System.Int32>(id

#line default
#line hidden
, 2968), false)
);

WriteLiteralTo(__razor_helper_writer, ">");


#line 87 "..\..\Views\Provider\Detail.cshtml"
WriteTo(__razor_helper_writer, Html.Raw(item));


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "</dd>\r\n");


#line 88 "..\..\Views\Provider\Detail.cshtml"
    }


#line default
#line hidden
});

#line 89 "..\..\Views\Provider\Detail.cshtml"
}
#line default
#line hidden

#line 91 "..\..\Views\Provider\Detail.cshtml"
public System.Web.WebPages.HelperResult GetEmailPropertyHtml(string title, string id, string item, bool hideIfEmpty = false)
{
#line default
#line hidden
return new System.Web.WebPages.HelperResult(__razor_helper_writer => {

#line 92 "..\..\Views\Provider\Detail.cshtml"
 
if (!string.IsNullOrEmpty(item) || !hideIfEmpty)
{


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "        <dt>");


#line 95 "..\..\Views\Provider\Detail.cshtml"
WriteTo(__razor_helper_writer, title);


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "</dt>\r\n");

WriteLiteralTo(__razor_helper_writer, "        <dd");

WriteAttributeTo(__razor_helper_writer, "id", Tuple.Create(" id=\"", 3193), Tuple.Create("\"", 3201)

#line 96 "..\..\Views\Provider\Detail.cshtml"
, Tuple.Create(Tuple.Create("", 3198), Tuple.Create<System.Object, System.Int32>(id

#line default
#line hidden
, 3198), false)
);

WriteLiteralTo(__razor_helper_writer, "><a");

WriteAttributeTo(__razor_helper_writer, "href", Tuple.Create(" href=\"", 3205), Tuple.Create("\"", 3234)
, Tuple.Create(Tuple.Create("", 3212), Tuple.Create("mailto:", 3212), true)

#line 96 "..\..\Views\Provider\Detail.cshtml"
, Tuple.Create(Tuple.Create("", 3219), Tuple.Create<System.Object, System.Int32>(Html.Raw(item)

#line default
#line hidden
, 3219), false)
);

WriteLiteralTo(__razor_helper_writer, ">");


#line 96 "..\..\Views\Provider\Detail.cshtml"
                        WriteTo(__razor_helper_writer, Html.Raw(item));


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "</a></dd>\r\n");


#line 97 "..\..\Views\Provider\Detail.cshtml"
}


#line default
#line hidden
});

#line 98 "..\..\Views\Provider\Detail.cshtml"
}
#line default
#line hidden

#line 100 "..\..\Views\Provider\Detail.cshtml"
public System.Web.WebPages.HelperResult GetStandardPropertyAsLinkHtml(string title, string id, string url, bool hideIfEmpty = false)
{
#line default
#line hidden
return new System.Web.WebPages.HelperResult(__razor_helper_writer => {

#line 101 "..\..\Views\Provider\Detail.cshtml"
 
    if (!string.IsNullOrEmpty(url) || !hideIfEmpty)
    {


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "        <dt>");


#line 104 "..\..\Views\Provider\Detail.cshtml"
WriteTo(__razor_helper_writer, title);


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "</dt>\r\n");

WriteLiteralTo(__razor_helper_writer, "        <dd");

WriteAttributeTo(__razor_helper_writer, "id", Tuple.Create(" id=\"", 3471), Tuple.Create("\"", 3479)

#line 105 "..\..\Views\Provider\Detail.cshtml"
, Tuple.Create(Tuple.Create("", 3476), Tuple.Create<System.Object, System.Int32>(id

#line default
#line hidden
, 3476), false)
);

WriteLiteralTo(__razor_helper_writer, ">\r\n            <a");

WriteLiteralTo(__razor_helper_writer, " class=\"pdf-url-link\"");

WriteAttributeTo(__razor_helper_writer, "href", Tuple.Create(" href=\"", 3518), Tuple.Create("\"", 3529)

#line 106 "..\..\Views\Provider\Detail.cshtml"
, Tuple.Create(Tuple.Create("", 3525), Tuple.Create<System.Object, System.Int32>(url

#line default
#line hidden
, 3525), false)
);

WriteLiteralTo(__razor_helper_writer, " target=\"_blank\"");

WriteLiteralTo(__razor_helper_writer, ">");


#line 106 "..\..\Views\Provider\Detail.cshtml"
                                  WriteTo(__razor_helper_writer, url);


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "</a>\r\n        </dd>\r\n");


#line 108 "..\..\Views\Provider\Detail.cshtml"
    }


#line default
#line hidden
});

#line 109 "..\..\Views\Provider\Detail.cshtml"
}
#line default
#line hidden

#line 110 "..\..\Views\Provider\Detail.cshtml"
public System.Web.WebPages.HelperResult GetSatisfactionsHtml()
{
#line default
#line hidden
return new System.Web.WebPages.HelperResult(__razor_helper_writer => {

#line 111 "..\..\Views\Provider\Detail.cshtml"
 


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "    <p>\r\n        <b>Learner satisfaction:</b>\r\n        <br/>\r\n");


#line 115 "..\..\Views\Provider\Detail.cshtml"
        

#line default
#line hidden

#line 115 "..\..\Views\Provider\Detail.cshtml"
         if (@Model.LearnerSatisfactionMessage != "No data available")
        {


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "            <span");

WriteLiteralTo(__razor_helper_writer, " id=\"learner-satisfaction\"");

WriteLiteralTo(__razor_helper_writer, " class=\"heading-large\"");

WriteLiteralTo(__razor_helper_writer, ">\r\n");

WriteLiteralTo(__razor_helper_writer, "                ");


#line 118 "..\..\Views\Provider\Detail.cshtml"
WriteTo(__razor_helper_writer, Model.LearnerSatisfactionMessage);


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "\r\n            </span>\r\n");


#line 120 "..\..\Views\Provider\Detail.cshtml"
        }
        else
        {


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "            <span");

WriteLiteralTo(__razor_helper_writer, " id=\"learner-satisfaction\"");

WriteLiteralTo(__razor_helper_writer, ">\r\n");

WriteLiteralTo(__razor_helper_writer, "                ");


#line 124 "..\..\Views\Provider\Detail.cshtml"
WriteTo(__razor_helper_writer, Model.LearnerSatisfactionMessage);


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "\r\n            </span>\r\n");


#line 126 "..\..\Views\Provider\Detail.cshtml"
        }


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "    </p>\r\n");


#line 128 "..\..\Views\Provider\Detail.cshtml"



#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "    <p>\r\n        <b>Employer satisfaction:</b>\r\n        <br/>\r\n");


#line 132 "..\..\Views\Provider\Detail.cshtml"
        

#line default
#line hidden

#line 132 "..\..\Views\Provider\Detail.cshtml"
         if (@Model.EmployerSatisfactionMessage != "No data available")
        {


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "            <span");

WriteLiteralTo(__razor_helper_writer, " id=\"employer-satisfaction\"");

WriteLiteralTo(__razor_helper_writer, " class=\"heading-large\"");

WriteLiteralTo(__razor_helper_writer, ">\r\n");

WriteLiteralTo(__razor_helper_writer, "                ");


#line 135 "..\..\Views\Provider\Detail.cshtml"
WriteTo(__razor_helper_writer, Model.EmployerSatisfactionMessage);


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "\r\n            </span>\r\n");


#line 137 "..\..\Views\Provider\Detail.cshtml"
        }
        else
        {


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "            <span");

WriteLiteralTo(__razor_helper_writer, " id=\"employer-satisfaction\"");

WriteLiteralTo(__razor_helper_writer, ">\r\n");

WriteLiteralTo(__razor_helper_writer, "                ");


#line 141 "..\..\Views\Provider\Detail.cshtml"
WriteTo(__razor_helper_writer, Model.EmployerSatisfactionMessage);


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "\r\n            </span>\r\n");


#line 143 "..\..\Views\Provider\Detail.cshtml"
        }


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "    </p>\r\n");


#line 145 "..\..\Views\Provider\Detail.cshtml"


#line default
#line hidden
});

#line 145 "..\..\Views\Provider\Detail.cshtml"
}
#line default
#line hidden

#line 147 "..\..\Views\Provider\Detail.cshtml"
public System.Web.WebPages.HelperResult GetDeliveryModesHtml(string title, List<string> items, bool hideIfEmpty = false)
{
#line default
#line hidden
return new System.Web.WebPages.HelperResult(__razor_helper_writer => {

#line 148 "..\..\Views\Provider\Detail.cshtml"
 
    if (items != null)
    {


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "        <dt>");


#line 151 "..\..\Views\Provider\Detail.cshtml"
WriteTo(__razor_helper_writer, title);


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "</dt>\r\n");

WriteLiteralTo(__razor_helper_writer, "        <dd");

WriteLiteralTo(__razor_helper_writer, " id=\"delivery-modes\"");

WriteLiteralTo(__razor_helper_writer, ">\r\n            <ul>\r\n");


#line 154 "..\..\Views\Provider\Detail.cshtml"
                

#line default
#line hidden

#line 154 "..\..\Views\Provider\Detail.cshtml"
                 foreach (var item in items)
                {
                    switch (item)
                    {
                        case "100PercentEmployer":


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "                            <li");

WriteLiteralTo(__razor_helper_writer, " class=\"100-percent-employer\"");

WriteLiteralTo(__razor_helper_writer, ">");


#line 159 "..\..\Views\Provider\Detail.cshtml"
                               WriteTo(__razor_helper_writer, Html.Raw("at your location"));


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "</li>\r\n");


#line 160 "..\..\Views\Provider\Detail.cshtml"
                            break;
                        case "BlockRelease":


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "                            <li");

WriteLiteralTo(__razor_helper_writer, " class=\"block-release\"");

WriteLiteralTo(__razor_helper_writer, ">");


#line 162 "..\..\Views\Provider\Detail.cshtml"
                        WriteTo(__razor_helper_writer, Html.Raw("block release"));


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "</li>\r\n");


#line 163 "..\..\Views\Provider\Detail.cshtml"
                            break;
                        case "DayRelease":


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "                            <li");

WriteLiteralTo(__razor_helper_writer, " class=\"day-release\"");

WriteLiteralTo(__razor_helper_writer, ">");


#line 165 "..\..\Views\Provider\Detail.cshtml"
                      WriteTo(__razor_helper_writer, Html.Raw("day release"));


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "</li>\r\n");


#line 166 "..\..\Views\Provider\Detail.cshtml"
                            break;
                        default:
                            break;
                    }
                }


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "            </ul>\r\n        </dd>\r\n");


#line 173 "..\..\Views\Provider\Detail.cshtml"
    }


#line default
#line hidden
});

#line 174 "..\..\Views\Provider\Detail.cshtml"
}
#line default
#line hidden

        public Detail()
        {
        }
        public override void Execute()
        {
            
            #line 4 "..\..\Views\Provider\Detail.cshtml"
  
    ViewBag.Title = "Provider - " + @Model.Name;

            
            #line default
            #line hidden
WriteLiteral("\r\n<main");

WriteLiteral(" id=\"content\"");

WriteLiteral(">\r\n\r\n    <p");

WriteLiteral(" class=\"small-btm-margin\"");

WriteLiteral(">\r\n");

WriteLiteral("        ");

            
            #line 10 "..\..\Views\Provider\Detail.cshtml"
   Write(Html.RenderAIfExists(@Model.SearchResultLink?.Title, @Model.SearchResultLink?.Url, "link-back"));

            
            #line default
            #line hidden
WriteLiteral("\r\n    </p>\r\n\r\n    <div");

WriteLiteral(" class=\"grid-row\"");

WriteLiteral(">\r\n\r\n        <div");

WriteLiteral(" class=\"column-two-thirds\"");

WriteLiteral(">\r\n            <div");

WriteLiteral(" class=\"hgroup\"");

WriteLiteral(">\r\n                <h1");

WriteLiteral(" class=\"heading-xlarge\"");

WriteLiteral(">\r\n");

WriteLiteral("                    ");

            
            #line 18 "..\..\Views\Provider\Detail.cshtml"
               Write(Model.Name);

            
            #line default
            #line hidden
WriteLiteral("\r\n                </h1>\r\n                <div");

WriteLiteral(" id=\"marketing\"");

WriteLiteral(">\r\n");

WriteLiteral("                    ");

            
            #line 21 "..\..\Views\Provider\Detail.cshtml"
               Write(Html.MarkdownToHtml(Model.ProviderMarketingInfo));

            
            #line default
            #line hidden
WriteLiteral("\r\n                </div>\r\n                \r\n            </div>\r\n        </div>\r\n\r" +
"\n        <div");

WriteLiteral(" class=\"column-third\"");

WriteLiteral(">\r\n");

            
            #line 28 "..\..\Views\Provider\Detail.cshtml"
            
            
            #line default
            #line hidden
            
            #line 28 "..\..\Views\Provider\Detail.cshtml"
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

WriteLiteral(">Training provider quality assessment</h2>\r\n");

WriteLiteral("                        ");

            
            #line 33 "..\..\Views\Provider\Detail.cshtml"
                   Write(GetSatisfactionsHtml());

            
            #line default
            #line hidden
WriteLiteral("\r\n                    </aside>\r\n                </div>\r\n");

            
            #line 36 "..\..\Views\Provider\Detail.cshtml"
            }

            
            #line default
            #line hidden
WriteLiteral("        </div>\r\n    </div>\r\n\r\n    <section>\r\n        <header>\r\n            <h2");

WriteLiteral(" class=\"heading-large\"");

WriteLiteral(">\r\n");

WriteLiteral("                ");

            
            #line 43 "..\..\Views\Provider\Detail.cshtml"
           Write(Model.ApprenticeshipNameWithLevel);

            
            #line default
            #line hidden
WriteLiteral("\r\n            </h2>\r\n        </header>\r\n        <dl");

WriteLiteral(" class=\"data-list\"");

WriteLiteral(">\r\n");

WriteLiteral("            ");

            
            #line 47 "..\..\Views\Provider\Detail.cshtml"
       Write(GetStandardPropertyAsLinkHtml("Website course page", "course-link", @Model.Apprenticeship.ApprenticeshipInfoUrl));

            
            #line default
            #line hidden
WriteLiteral("\r\n");

WriteLiteral("            ");

            
            #line 48 "..\..\Views\Provider\Detail.cshtml"
       Write(GetStandardPropertyAsLinkHtml("Website contact page", "contact-link", @Model.ContactInformation.ContactUsUrl));

            
            #line default
            #line hidden
WriteLiteral("\r\n");

WriteLiteral("            ");

            
            #line 49 "..\..\Views\Provider\Detail.cshtml"
       Write(GetStandardPropertyHtml("Phone", "phone", Model.ContactInformation.Phone));

            
            #line default
            #line hidden
WriteLiteral("\r\n");

WriteLiteral("            ");

            
            #line 50 "..\..\Views\Provider\Detail.cshtml"
       Write(GetEmailPropertyHtml("Email", "email", Model.ContactInformation.Email));

            
            #line default
            #line hidden
WriteLiteral("\r\n");

WriteLiteral("            ");

            
            #line 51 "..\..\Views\Provider\Detail.cshtml"
       Write(GetDeliveryModesHtml("Training structure", Model.DeliveryModes));

            
            #line default
            #line hidden
WriteLiteral("\r\n");

WriteLiteral("            ");

            
            #line 52 "..\..\Views\Provider\Detail.cshtml"
       Write(ShowTrainingLocation("Training location"));

            
            #line default
            #line hidden
WriteLiteral("\r\n        </dl>\r\n\r\n    </section>\r\n\r\n    <section>\r\n        <header>\r\n           " +
" <h1");

WriteLiteral(" class=\"heading-medium\"");

WriteLiteral(">Apprenticeship training information</h1>\r\n        </header>\r\n        <p");

WriteLiteral(" class=\"apprenticeship-marketing-info\"");

WriteLiteral(">");

            
            #line 61 "..\..\Views\Provider\Detail.cshtml"
                                            Write(Html.MarkdownToHtml(Model.Apprenticeship.ApprenticeshipMarketingInfo));

            
            #line default
            #line hidden
WriteLiteral("</p>\r\n    </section>\r\n</main>\r\n\r\n");

WriteLiteral("\r\n");

WriteLiteral("\r\n");

WriteLiteral("\r\n");

WriteLiteral("\r\n");

        }
    }
}
#pragma warning restore 1591
