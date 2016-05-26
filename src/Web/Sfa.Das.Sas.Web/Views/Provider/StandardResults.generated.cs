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
    using Sfa.Das.Sas.Web;
    
    #line 1 "..\..\Views\Provider\StandardResults.cshtml"
    using Sfa.Das.Sas.Web.Extensions;
    
    #line default
    #line hidden
    
    #line 2 "..\..\Views\Provider\StandardResults.cshtml"
    using Sfa.Das.Sas.Web.ViewModels;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/Provider/StandardResults.cshtml")]
    public partial class StandardResults : System.Web.Mvc.WebViewPage<ProviderStandardSearchResultViewModel>
    {

#line 57 "..\..\Views\Provider\StandardResults.cshtml"
public System.Web.WebPages.HelperResult GetPaginationBackLink()
{
#line default
#line hidden
return new System.Web.WebPages.HelperResult(__razor_helper_writer => {

#line 58 "..\..\Views\Provider\StandardResults.cshtml"
 
    if (Model.ActualPage > 1)
    {
        var previousPage = @Model.ActualPage - 1;
        var url = @Url.Action("StandardResults", "Provider", GetNavigationRouteValues(previousPage, Model.DeliveryModes), null);

        

#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "<a");

WriteAttributeTo(__razor_helper_writer, "href", Tuple.Create(" href=\"", 1678), Tuple.Create("\"", 1689)

#line 64 "..\..\Views\Provider\StandardResults.cshtml"
, Tuple.Create(Tuple.Create("", 1685), Tuple.Create<System.Object, System.Int32>(url

#line default
#line hidden
, 1685), false)
);

WriteLiteralTo(__razor_helper_writer, " style=\"visibility: visible\"");

WriteLiteralTo(__razor_helper_writer, " class=\"page-navigation__btn prev\"");

WriteLiteralTo(__razor_helper_writer, ">\r\n            <i");

WriteLiteralTo(__razor_helper_writer, " class=\"arrow-button fa fa-angle-left\"");

WriteLiteralTo(__razor_helper_writer, "></i>\r\n            <span");

WriteLiteralTo(__razor_helper_writer, " class=\"description\"");

WriteLiteralTo(__razor_helper_writer, ">Previous <span");

WriteLiteralTo(__razor_helper_writer, " class=\"hide-mob\"");

WriteLiteralTo(__razor_helper_writer, ">page</span></span>\r\n            <span");

WriteLiteralTo(__razor_helper_writer, " class=\"counter\"");

WriteLiteralTo(__razor_helper_writer, ">");


#line 67 "..\..\Views\Provider\StandardResults.cshtml"
    WriteTo(__razor_helper_writer, previousPage);


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, " of ");


#line 67 "..\..\Views\Provider\StandardResults.cshtml"
                     WriteTo(__razor_helper_writer, Model.LastPage);


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "</span>\r\n        </a>\r\n");


#line 69 "..\..\Views\Provider\StandardResults.cshtml"
    }


#line default
#line hidden
});

#line 70 "..\..\Views\Provider\StandardResults.cshtml"
}
#line default
#line hidden

#line 73 "..\..\Views\Provider\StandardResults.cshtml"
public System.Web.WebPages.HelperResult GetPaginationNextLink()
{
#line default
#line hidden
return new System.Web.WebPages.HelperResult(__razor_helper_writer => {

#line 74 "..\..\Views\Provider\StandardResults.cshtml"
 
    if (Model.ActualPage < Model.LastPage)
    {
        var nextPage = @Model.ActualPage + 1;

        var url = @Url.Action("StandardResults", "Provider", GetNavigationRouteValues(nextPage, Model.DeliveryModes), null);
        

#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "<a");

WriteAttributeTo(__razor_helper_writer, "href", Tuple.Create(" href=\"", 2279), Tuple.Create("\"", 2290)

#line 80 "..\..\Views\Provider\StandardResults.cshtml"
, Tuple.Create(Tuple.Create("", 2286), Tuple.Create<System.Object, System.Int32>(url

#line default
#line hidden
, 2286), false)
);

WriteLiteralTo(__razor_helper_writer, " style=\"visibility: visible\"");

WriteLiteralTo(__razor_helper_writer, " class=\"page-navigation__btn next\"");

WriteLiteralTo(__razor_helper_writer, ">\r\n            <i");

WriteLiteralTo(__razor_helper_writer, " class=\"arrow-button fa fa-angle-right\"");

WriteLiteralTo(__razor_helper_writer, "></i>\r\n            <span");

WriteLiteralTo(__razor_helper_writer, " class=\"description\"");

WriteLiteralTo(__razor_helper_writer, ">Next <span");

WriteLiteralTo(__razor_helper_writer, " class=\"hide-mob\"");

WriteLiteralTo(__razor_helper_writer, ">page</span></span>\r\n            <span");

WriteLiteralTo(__razor_helper_writer, " class=\"counter\"");

WriteLiteralTo(__razor_helper_writer, ">");


#line 83 "..\..\Views\Provider\StandardResults.cshtml"
    WriteTo(__razor_helper_writer, nextPage);


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, " of ");


#line 83 "..\..\Views\Provider\StandardResults.cshtml"
                 WriteTo(__razor_helper_writer, Model.LastPage);


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "</span>\r\n        </a>\r\n");


#line 85 "..\..\Views\Provider\StandardResults.cshtml"
    }


#line default
#line hidden
});

#line 86 "..\..\Views\Provider\StandardResults.cshtml"
}
#line default
#line hidden

#line 88 "..\..\Views\Provider\StandardResults.cshtml"
public System.Web.WebPages.HelperResult FilterForm(string cssClass)
{
#line default
#line hidden
return new System.Web.WebPages.HelperResult(__razor_helper_writer => {

#line 89 "..\..\Views\Provider\StandardResults.cshtml"
 
if (!Model.DeliveryModes.IsNullOrEmpty())
{


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "        <div");

WriteAttributeTo(__razor_helper_writer, "class", Tuple.Create(" class=\"", 2697), Tuple.Create("\"", 2714)

#line 92 "..\..\Views\Provider\StandardResults.cshtml"
, Tuple.Create(Tuple.Create("", 2705), Tuple.Create<System.Object, System.Int32>(cssClass

#line default
#line hidden
, 2705), false)
);

WriteLiteralTo(__razor_helper_writer, ">\r\n            <form");

WriteLiteralTo(__razor_helper_writer, " method=\"get\"");

WriteLiteralTo(__razor_helper_writer, " autocomplete=\"off\"");

WriteAttributeTo(__razor_helper_writer, "action", Tuple.Create(" action=\"", 2767), Tuple.Create("\"", 2801)

#line 93 "..\..\Views\Provider\StandardResults.cshtml"
, Tuple.Create(Tuple.Create("", 2776), Tuple.Create<System.Object, System.Int32>(Request.Url.AbsolutePath

#line default
#line hidden
, 2776), false)
);

WriteLiteralTo(__razor_helper_writer, ">\r\n                <input");

WriteLiteralTo(__razor_helper_writer, " type=\"hidden\"");

WriteLiteralTo(__razor_helper_writer, " name=\"PostCode\"");

WriteAttributeTo(__razor_helper_writer, "value", Tuple.Create(" value=\"", 2857), Tuple.Create("\"", 2880)

#line 94 "..\..\Views\Provider\StandardResults.cshtml"
, Tuple.Create(Tuple.Create("", 2865), Tuple.Create<System.Object, System.Int32>(Model.PostCode

#line default
#line hidden
, 2865), false)
);

WriteLiteralTo(__razor_helper_writer, " />\r\n                <input");

WriteLiteralTo(__razor_helper_writer, " type=\"hidden\"");

WriteLiteralTo(__razor_helper_writer, " name=\"apprenticeshipid\"");

WriteAttributeTo(__razor_helper_writer, "value", Tuple.Create(" value=\"", 2946), Tuple.Create("\"", 2971)

#line 95 "..\..\Views\Provider\StandardResults.cshtml"
, Tuple.Create(Tuple.Create("", 2954), Tuple.Create<System.Object, System.Int32>(Model.StandardId

#line default
#line hidden
, 2954), false)
);

WriteLiteralTo(__razor_helper_writer, " />\r\n");


#line 96 "..\..\Views\Provider\StandardResults.cshtml"
                

#line default
#line hidden

#line 96 "..\..\Views\Provider\StandardResults.cshtml"
                  
                    Html.RenderPartial("_FilterProviders", Model.DeliveryModes);
                

#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "\r\n                <input");

WriteLiteralTo(__razor_helper_writer, " type=\"submit\"");

WriteLiteralTo(__razor_helper_writer, " value=\"Update results\"");

WriteLiteralTo(__razor_helper_writer, " class=\"button margin-top-x2 postcode-search-button\"");

WriteLiteralTo(__razor_helper_writer, " />\r\n            </form>\r\n        </div>\r\n");


#line 102 "..\..\Views\Provider\StandardResults.cshtml"
                    }


#line default
#line hidden
});

#line 103 "..\..\Views\Provider\StandardResults.cshtml"
}
#line default
#line hidden

        #line 106 "..\..\Views\Provider\StandardResults.cshtml"
 
    RouteValueDictionary GetNavigationRouteValues(int page, IEnumerable<DeliveryModeViewModel> deliveryModes)
    {

        var rv = new RouteValueDictionary { { "apprenticeshipid", Model.StandardId }, { "postcode", Model.PostCode }, { "page", page } };
        int i = 0;
        foreach (var deliveryMode in deliveryModes.Where(m => m.Checked))
        {
            rv.Add("DeliveryModes[" + i + "]", deliveryMode.Value);
            i++;
        }
        return rv;
    }

        #line default
        #line hidden
        
        public StandardResults()
        {
        }
        public override void Execute()
        {
            
            #line 5 "..\..\Views\Provider\StandardResults.cshtml"
  
    ViewBag.Title = "Provider Search Results";

            
            #line default
            #line hidden
WriteLiteral("\r\n\r\n<main");

WriteLiteral(" id=\"content\"");

WriteLiteral(" role=\"main\"");

WriteLiteral(">\r\n");

WriteLiteral("    ");

            
            #line 10 "..\..\Views\Provider\StandardResults.cshtml"
Write(Html.ActionLink("Back", "SearchForProviders", "Apprenticeship", new { @standardId = @Model.StandardId }, new { @class = "link-back" }));

            
            #line default
            #line hidden
WriteLiteral("\r\n    <div");

WriteLiteral(" id=\"provider-results\"");

WriteLiteral(" class=\"grid-row\"");

WriteLiteral(">\r\n\r\n        <div");

WriteLiteral(" class=\"column-two-thirds\"");

WriteLiteral(">\r\n\r\n            <div");

WriteLiteral(" class=\"hgroup\"");

WriteLiteral(">\r\n\r\n                <h1");

WriteLiteral(" class=\"heading-xlarge\"");

WriteLiteral(">\r\n                    Search results\r\n                </h1>\r\n\r\n            </div" +
">\r\n            <p>\r\n");

            
            #line 23 "..\..\Views\Provider\StandardResults.cshtml"
                
            
            #line default
            #line hidden
            
            #line 23 "..\..\Views\Provider\StandardResults.cshtml"
                  
                    Html.RenderPartial("_StandardSearchResultMessage");
                
            
            #line default
            #line hidden
WriteLiteral("\r\n            </p>\r\n\r\n        </div>\r\n\r\n        <div");

WriteLiteral(" class=\"column-third\"");

WriteLiteral(">\r\n");

WriteLiteral("            ");

            
            #line 31 "..\..\Views\Provider\StandardResults.cshtml"
       Write(FilterForm("filter-box"));

            
            #line default
            #line hidden
WriteLiteral("\r\n        </div>\r\n\r\n        <div");

WriteLiteral(" class=\"column-two-thirds\"");

WriteLiteral(">\r\n");

            
            #line 35 "..\..\Views\Provider\StandardResults.cshtml"
            
            
            #line default
            #line hidden
            
            #line 35 "..\..\Views\Provider\StandardResults.cshtml"
              
                Html.RenderPartial("_StandardProviderInformation");
            
            
            #line default
            #line hidden
WriteLiteral("\r\n            <div");

WriteLiteral(" class=\"page-navigation\"");

WriteLiteral(">\r\n");

WriteLiteral("                ");

            
            #line 39 "..\..\Views\Provider\StandardResults.cshtml"
           Write(GetPaginationBackLink());

            
            #line default
            #line hidden
WriteLiteral("\r\n");

WriteLiteral("                ");

            
            #line 40 "..\..\Views\Provider\StandardResults.cshtml"
           Write(GetPaginationNextLink());

            
            #line default
            #line hidden
WriteLiteral("\r\n            </div>\r\n        </div>\r\n\r\n\r\n    </div>\r\n</main>\r\n\r\n");

            
            #line 48 "..\..\Views\Provider\StandardResults.cshtml"
 if (Model.TotalResults == 0)
{

            
            #line default
            #line hidden
WriteLiteral("    <script>\r\n        window.onload = function() {\r\n            SearchAndShortlis" +
"t.analytics.pushEvent(\"Provider Search\", \"No results\", \"Search\");\r\n        }\r\n  " +
"  </script> \r\n");

            
            #line 55 "..\..\Views\Provider\StandardResults.cshtml"
}

            
            #line default
            #line hidden
WriteLiteral("\r\n");

WriteLiteral("\r\n\r\n");

WriteLiteral("\r\n");

WriteLiteral("\r\n");

        }
    }
}
#pragma warning restore 1591
