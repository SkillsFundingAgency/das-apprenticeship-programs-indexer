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

#line 56 "..\..\Views\Provider\Detail.cshtml"
public System.Web.WebPages.HelperResult ShowTrainingLocation(string title)
{
#line default
#line hidden
return new System.Web.WebPages.HelperResult(__razor_helper_writer => {

#line 57 "..\..\Views\Provider\Detail.cshtml"
 
if (@Model != null)
{
    if (@Model.DeliveryModes.Count == 1 && @Model.DeliveryModes.Contains("100PercentEmployer"))
    {


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "            <dt>");


#line 62 "..\..\Views\Provider\Detail.cshtml"
WriteTo(__razor_helper_writer, title);


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "</dt>\r\n");

WriteLiteralTo(__razor_helper_writer, "            <dd>Training will take place at your location</dd>\r\n");


#line 64 "..\..\Views\Provider\Detail.cshtml"
    }
    else
    {


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "            <dt>");


#line 67 "..\..\Views\Provider\Detail.cshtml"
WriteTo(__razor_helper_writer, title);


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "</dt>\r\n");

WriteLiteralTo(__razor_helper_writer, "            <dd>");


#line 68 "..\..\Views\Provider\Detail.cshtml"
WriteTo(__razor_helper_writer, Model.LocationName);


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, " ");


#line 68 "..\..\Views\Provider\Detail.cshtml"
      WriteTo(__razor_helper_writer, Model.Address.Address1);


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, " ");


#line 68 "..\..\Views\Provider\Detail.cshtml"
                              WriteTo(__razor_helper_writer, Model.Address.Address2);


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, " ");


#line 68 "..\..\Views\Provider\Detail.cshtml"
                                                      WriteTo(__razor_helper_writer, Model.Address.Postcode);


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "</dd>\r\n");


#line 69 "..\..\Views\Provider\Detail.cshtml"
    }
}


#line default
#line hidden
});

#line 71 "..\..\Views\Provider\Detail.cshtml"
}
#line default
#line hidden

#line 74 "..\..\Views\Provider\Detail.cshtml"
public System.Web.WebPages.HelperResult GetStandardProperty(string title, string item, bool hideIfEmpty = false)
{
#line default
#line hidden
return new System.Web.WebPages.HelperResult(__razor_helper_writer => {

#line 75 "..\..\Views\Provider\Detail.cshtml"
 
if (!string.IsNullOrEmpty(item) || !hideIfEmpty)
{


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "        <dt>");


#line 78 "..\..\Views\Provider\Detail.cshtml"
WriteTo(__razor_helper_writer, title);


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "</dt>\r\n");

WriteLiteralTo(__razor_helper_writer, "        <dd>");


#line 79 "..\..\Views\Provider\Detail.cshtml"
WriteTo(__razor_helper_writer, Html.Raw(item));


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "</dd>\r\n");


#line 80 "..\..\Views\Provider\Detail.cshtml"
}


#line default
#line hidden
});

#line 81 "..\..\Views\Provider\Detail.cshtml"
}
#line default
#line hidden

#line 83 "..\..\Views\Provider\Detail.cshtml"
public System.Web.WebPages.HelperResult GetStandardProperty(string title, List<string> items, bool hideIfEmpty = false)
{
#line default
#line hidden
return new System.Web.WebPages.HelperResult(__razor_helper_writer => {

#line 84 "..\..\Views\Provider\Detail.cshtml"
 
if (items != null)
{


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "        <dt>");


#line 87 "..\..\Views\Provider\Detail.cshtml"
WriteTo(__razor_helper_writer, title);


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "</dt>\r\n");

WriteLiteralTo(__razor_helper_writer, "        <dd>\r\n            <ul>\r\n");


#line 90 "..\..\Views\Provider\Detail.cshtml"
                

#line default
#line hidden

#line 90 "..\..\Views\Provider\Detail.cshtml"
                 foreach (var item in items)
                {
                    switch (item)
                    {
                        case "100PercentEmployer":


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "                            <li>");


#line 95 "..\..\Views\Provider\Detail.cshtml"
  WriteTo(__razor_helper_writer, Html.Raw("at your location"));


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "</li>\r\n");


#line 96 "..\..\Views\Provider\Detail.cshtml"
                            break;
                        case "BlockRelease":


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "                            <li>");


#line 98 "..\..\Views\Provider\Detail.cshtml"
  WriteTo(__razor_helper_writer, Html.Raw("block release"));


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "</li>\r\n");


#line 99 "..\..\Views\Provider\Detail.cshtml"
                            break;
                        case "DayRelease":


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "                            <li>");


#line 101 "..\..\Views\Provider\Detail.cshtml"
  WriteTo(__razor_helper_writer, Html.Raw("day release"));


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "</li>\r\n");


#line 102 "..\..\Views\Provider\Detail.cshtml"
                            break;
                        default:
                            break;
                    }
                }


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "            </ul>\r\n        </dd>\r\n");


#line 109 "..\..\Views\Provider\Detail.cshtml"
}


#line default
#line hidden
});

#line 110 "..\..\Views\Provider\Detail.cshtml"
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

WriteLiteral(">\r\n                    <span");

WriteLiteral(" class=\"heading-secondary\"");

WriteLiteral(">Apprenticeship standard and assessment plan</span>\r\n");

WriteLiteral("                    ");

            
            #line 19 "..\..\Views\Provider\Detail.cshtml"
               Write(Model.Name);

            
            #line default
            #line hidden
WriteLiteral("\r\n                </h1>\r\n\r\n            </div>\r\n        </div>\r\n\r\n        <div");

WriteLiteral(" class=\"column-third\"");

WriteLiteral(">\r\n");

            
            #line 26 "..\..\Views\Provider\Detail.cshtml"
            
            
            #line default
            #line hidden
            
            #line 26 "..\..\Views\Provider\Detail.cshtml"
             using (Html.BeginForm("SearchResults", "Provider", FormMethod.Get, new { @class = "search-box" }))
            {

            
            #line default
            #line hidden
WriteLiteral("                <div");

WriteLiteral(" class=\"related-container\"");

WriteLiteral(">\r\n                    <aside");

WriteLiteral(" class=\"related\"");

WriteLiteral(">\r\n                        <h2");

WriteLiteral(" class=\"heading-medium\"");

WriteLiteral(">Provider quality assassment</h2>\r\n                        <h3>Learner satisfacti" +
"on: ");

            
            #line 31 "..\..\Views\Provider\Detail.cshtml"
                                             Write(Model.LearnerSatisfaction);

            
            #line default
            #line hidden
WriteLiteral(" %</h3>\r\n                        <h3>Employer satisfaction: ");

            
            #line 32 "..\..\Views\Provider\Detail.cshtml"
                                              Write(Model.EmployerSatisfaction);

            
            #line default
            #line hidden
WriteLiteral(" %</h3>\r\n                    </aside>\r\n                </div>\r\n");

            
            #line 35 "..\..\Views\Provider\Detail.cshtml"
            }

            
            #line default
            #line hidden
WriteLiteral("        </div>\r\n    </div>\r\n\r\n    <section>\r\n        <header>\r\n            <h2");

WriteLiteral(" class=\"heading-large\"");

WriteLiteral(">\r\n");

WriteLiteral("                ");

            
            #line 42 "..\..\Views\Provider\Detail.cshtml"
           Write(Model.StandardNameWithLevel);

            
            #line default
            #line hidden
WriteLiteral("\r\n            </h2>\r\n        </header>\r\n        <dl");

WriteLiteral(" class=\"data-list\"");

WriteLiteral(">\r\n");

WriteLiteral("            ");

            
            #line 46 "..\..\Views\Provider\Detail.cshtml"
       Write(GetStandardProperty("Training details", @Model.StandardInfoUrl));

            
            #line default
            #line hidden
WriteLiteral("\r\n");

WriteLiteral("            ");

            
            #line 47 "..\..\Views\Provider\Detail.cshtml"
       Write(GetStandardProperty("Phone", Model.Phone));

            
            #line default
            #line hidden
WriteLiteral("\r\n");

WriteLiteral("            ");

            
            #line 48 "..\..\Views\Provider\Detail.cshtml"
       Write(GetStandardProperty("Email", Model.Email));

            
            #line default
            #line hidden
WriteLiteral("\r\n");

WriteLiteral("            ");

            
            #line 49 "..\..\Views\Provider\Detail.cshtml"
       Write(GetStandardProperty("Training options", Model.DeliveryModes));

            
            #line default
            #line hidden
WriteLiteral("\r\n");

WriteLiteral("            ");

            
            #line 50 "..\..\Views\Provider\Detail.cshtml"
       Write(ShowTrainingLocation("Training location"));

            
            #line default
            #line hidden
WriteLiteral("\r\n        </dl>\r\n    </section>\r\n</main>\r\n\r\n\r\n");

WriteLiteral("\r\n\r\n");

WriteLiteral("\r\n");

        }
    }
}
#pragma warning restore 1591
