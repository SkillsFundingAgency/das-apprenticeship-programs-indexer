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

#line 68 "..\..\Views\Provider\StandardResults.cshtml"
public System.Web.WebPages.HelperResult GetPaginationBackLink()
{
#line default
#line hidden
return new System.Web.WebPages.HelperResult(__razor_helper_writer => {

#line 69 "..\..\Views\Provider\StandardResults.cshtml"
 
    if (Model.ActualPage > 1)
    {
        var previousPage = @Model.ActualPage - 1;
        var url = @Url.Action("StandardResults", "Provider", GetNavigationRouteValues(previousPage, Model.DeliveryModes), null);

        

#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "<a");

WriteAttributeTo(__razor_helper_writer, "href", Tuple.Create(" href=\"", 2321), Tuple.Create("\"", 2332)

#line 75 "..\..\Views\Provider\StandardResults.cshtml"
, Tuple.Create(Tuple.Create("", 2328), Tuple.Create<System.Object, System.Int32>(url

#line default
#line hidden
, 2328), false)
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


#line 78 "..\..\Views\Provider\StandardResults.cshtml"
    WriteTo(__razor_helper_writer, previousPage);


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, " of ");


#line 78 "..\..\Views\Provider\StandardResults.cshtml"
                     WriteTo(__razor_helper_writer, Model.LastPage);


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "</span>\r\n        </a>\r\n");


#line 80 "..\..\Views\Provider\StandardResults.cshtml"
    }


#line default
#line hidden
});

#line 81 "..\..\Views\Provider\StandardResults.cshtml"
}
#line default
#line hidden

#line 83 "..\..\Views\Provider\StandardResults.cshtml"
public System.Web.WebPages.HelperResult GetPaginationNextLink()
{
#line default
#line hidden
return new System.Web.WebPages.HelperResult(__razor_helper_writer => {

#line 84 "..\..\Views\Provider\StandardResults.cshtml"
 
    if (Model.ActualPage < Model.LastPage)
    {
        var nextPage = @Model.ActualPage + 1;

        var url = @Url.Action("StandardResults", "Provider", GetNavigationRouteValues(nextPage, Model.DeliveryModes), null);
        

#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "<a");

WriteAttributeTo(__razor_helper_writer, "href", Tuple.Create(" href=\"", 2920), Tuple.Create("\"", 2931)

#line 90 "..\..\Views\Provider\StandardResults.cshtml"
, Tuple.Create(Tuple.Create("", 2927), Tuple.Create<System.Object, System.Int32>(url

#line default
#line hidden
, 2927), false)
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


#line 93 "..\..\Views\Provider\StandardResults.cshtml"
    WriteTo(__razor_helper_writer, nextPage);


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, " of ");


#line 93 "..\..\Views\Provider\StandardResults.cshtml"
                 WriteTo(__razor_helper_writer, Model.LastPage);


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "</span>\r\n        </a>\r\n");


#line 95 "..\..\Views\Provider\StandardResults.cshtml"
    }


#line default
#line hidden
});

#line 96 "..\..\Views\Provider\StandardResults.cshtml"
}
#line default
#line hidden

#line 98 "..\..\Views\Provider\StandardResults.cshtml"
public System.Web.WebPages.HelperResult FilterForm(string cssClass)
{
#line default
#line hidden
return new System.Web.WebPages.HelperResult(__razor_helper_writer => {

#line 99 "..\..\Views\Provider\StandardResults.cshtml"
 
    if (!Model.DeliveryModes.IsNullOrEmpty())
    {
        var hideMenu = Model.DeliveryModes.All(deliveryMode => deliveryMode.Count == 0);

        if (@Model.TotalResults != 0 || !hideMenu)
        {


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "            <div");

WriteAttributeTo(__razor_helper_writer, "class", Tuple.Create(" class=\"", 3505), Tuple.Create("\"", 3522)

#line 106 "..\..\Views\Provider\StandardResults.cshtml"
, Tuple.Create(Tuple.Create("", 3513), Tuple.Create<System.Object, System.Int32>(cssClass

#line default
#line hidden
, 3513), false)
);

WriteLiteralTo(__razor_helper_writer, ">\r\n                <form");

WriteLiteralTo(__razor_helper_writer, " method=\"get\"");

WriteLiteralTo(__razor_helper_writer, " autocomplete=\"off\"");

WriteAttributeTo(__razor_helper_writer, "action", Tuple.Create(" action=\"", 3579), Tuple.Create("\"", 3607)

#line 107 "..\..\Views\Provider\StandardResults.cshtml"
, Tuple.Create(Tuple.Create("", 3588), Tuple.Create<System.Object, System.Int32>(Model.AbsolutePath

#line default
#line hidden
, 3588), false)
);

WriteLiteralTo(__razor_helper_writer, ">\r\n                    <input");

WriteLiteralTo(__razor_helper_writer, " type=\"hidden\"");

WriteLiteralTo(__razor_helper_writer, " name=\"PostCode\"");

WriteAttributeTo(__razor_helper_writer, "value", Tuple.Create(" value=\"", 3667), Tuple.Create("\"", 3690)

#line 108 "..\..\Views\Provider\StandardResults.cshtml"
, Tuple.Create(Tuple.Create("", 3675), Tuple.Create<System.Object, System.Int32>(Model.PostCode

#line default
#line hidden
, 3675), false)
);

WriteLiteralTo(__razor_helper_writer, " />\r\n                    <input");

WriteLiteralTo(__razor_helper_writer, " type=\"hidden\"");

WriteLiteralTo(__razor_helper_writer, " name=\"apprenticeshipid\"");

WriteAttributeTo(__razor_helper_writer, "value", Tuple.Create(" value=\"", 3760), Tuple.Create("\"", 3785)

#line 109 "..\..\Views\Provider\StandardResults.cshtml"
, Tuple.Create(Tuple.Create("", 3768), Tuple.Create<System.Object, System.Int32>(Model.StandardId

#line default
#line hidden
, 3768), false)
);

WriteLiteralTo(__razor_helper_writer, "/>\r\n                    <input");

WriteLiteralTo(__razor_helper_writer, " type=\"hidden\"");

WriteLiteralTo(__razor_helper_writer, " name=\"showAll\"");

WriteAttributeTo(__razor_helper_writer, "value", Tuple.Create(" value=\"", 3845), Tuple.Create("\"", 3878)

#line 110 "..\..\Views\Provider\StandardResults.cshtml"
, Tuple.Create(Tuple.Create("", 3853), Tuple.Create<System.Object, System.Int32>(Model.ShowAll.ToString()

#line default
#line hidden
, 3853), false)
);

WriteLiteralTo(__razor_helper_writer, " />\r\n");


#line 111 "..\..\Views\Provider\StandardResults.cshtml"
                    

#line default
#line hidden

#line 111 "..\..\Views\Provider\StandardResults.cshtml"
                      
                        Html.RenderPartial("_FilterProviders", Model.DeliveryModes);
                    

#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "\r\n");


#line 114 "..\..\Views\Provider\StandardResults.cshtml"
                    

#line default
#line hidden

#line 114 "..\..\Views\Provider\StandardResults.cshtml"
                      
                        Html.RenderPartial("_FilterNationalProviders", Model.DeliveryModes);
                    

#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "\r\n                    <input");

WriteLiteralTo(__razor_helper_writer, " type=\"submit\"");

WriteLiteralTo(__razor_helper_writer, " value=\"Update results\"");

WriteLiteralTo(__razor_helper_writer, " class=\"button margin-top-x2 postcode-search-button\"");

WriteLiteralTo(__razor_helper_writer, " />\r\n                </form>\r\n            </div>\r\n");


#line 120 "..\..\Views\Provider\StandardResults.cshtml"
        }
    }


#line default
#line hidden
});

#line 122 "..\..\Views\Provider\StandardResults.cshtml"
}
#line default
#line hidden

        #line 125 "..\..\Views\Provider\StandardResults.cshtml"
 
    RouteValueDictionary GetNavigationRouteValues(int page, IEnumerable<DeliveryModeViewModel> deliveryModes)
    {

        var rv = new RouteValueDictionary { { "apprenticeshipid", Model.StandardId }, { "postcode", Model.PostCode }, { "page", page }, { "showall", Model.ShowAll} };
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

            
            #line 10 "..\..\Views\Provider\StandardResults.cshtml"
    
            
            #line default
            #line hidden
            
            #line 10 "..\..\Views\Provider\StandardResults.cshtml"
     if (Model.Hits.Count() != 0)
    {

            
            #line default
            #line hidden
WriteLiteral("        <p");

WriteLiteral(" class=\"small-btm-margin\"");

WriteLiteral(">\r\n");

WriteLiteral("            ");

            
            #line 13 "..\..\Views\Provider\StandardResults.cshtml"
       Write(Html.ActionLink("Find providers for a different postcode", "SearchForStandardProviders", "Apprenticeship", new { @standardId = Model.StandardId, @keywords = Model.SearchTerms }, new { @class = "link-back new-postcode-search" }));

            
            #line default
            #line hidden
WriteLiteral("\r\n        </p>\r\n");

            
            #line 15 "..\..\Views\Provider\StandardResults.cshtml"
    }

            
            #line default
            #line hidden
WriteLiteral("    <div");

WriteLiteral(" class=\"grid-row\"");

WriteLiteral(">\r\n        <div");

WriteLiteral(" class=\"column-two-thirds\"");

WriteLiteral(">\r\n            <div");

WriteLiteral(" class=\"hgroup\"");

WriteLiteral(">\r\n                <h1");

WriteLiteral(" class=\"heading-xlarge\"");

WriteLiteral(">\r\n                    Search results\r\n                </h1>\r\n                \r\n " +
"               <p>\r\n");

            
            #line 24 "..\..\Views\Provider\StandardResults.cshtml"
                    
            
            #line default
            #line hidden
            
            #line 24 "..\..\Views\Provider\StandardResults.cshtml"
                      
                        Html.RenderPartial("_StandardSearchResultMessage");
                    
            
            #line default
            #line hidden
WriteLiteral("\r\n                </p>\r\n            </div>\r\n        </div>\r\n\r\n    </div>\r\n    <di" +
"v");

WriteLiteral(" class=\"grid-row\"");

WriteLiteral(">\r\n        <div");

WriteLiteral(" class=\"column-third\"");

WriteLiteral(">\r\n            <div");

WriteLiteral(" class=\"editSearch\"");

WriteLiteral(">\r\n");

            
            #line 35 "..\..\Views\Provider\StandardResults.cshtml"
                
            
            #line default
            #line hidden
            
            #line 35 "..\..\Views\Provider\StandardResults.cshtml"
                 if (Model.Hits.Any() && !Model.HasError)
                {

            
            #line default
            #line hidden
WriteLiteral("                    <h2");

WriteLiteral(" class=\"heading-medium\"");

WriteLiteral(">\r\n                        <a");

WriteLiteral(" href=\"#EditSearch\"");

WriteLiteral(">Edit search</a>\r\n                    </h2>\r\n");

WriteLiteral("                    <div");

WriteLiteral(" id=\"EditSearch\"");

WriteLiteral(">\r\n");

WriteLiteral("                        ");

            
            #line 41 "..\..\Views\Provider\StandardResults.cshtml"
                   Write(FilterForm("filter-box"));

            
            #line default
            #line hidden
WriteLiteral("\r\n                    </div>\r\n");

            
            #line 43 "..\..\Views\Provider\StandardResults.cshtml"
                }

            
            #line default
            #line hidden
WriteLiteral("            </div>\r\n        </div>\r\n\r\n        <div");

WriteLiteral(" id=\"provider-results\"");

WriteLiteral(" class=\"column-two-thirds\"");

WriteLiteral(">\r\n");

            
            #line 48 "..\..\Views\Provider\StandardResults.cshtml"
            
            
            #line default
            #line hidden
            
            #line 48 "..\..\Views\Provider\StandardResults.cshtml"
              
                Html.RenderPartial("_StandardProviderInformation");
            
            
            #line default
            #line hidden
WriteLiteral("\r\n            <div");

WriteLiteral(" class=\"page-navigation\"");

WriteLiteral(">\r\n");

WriteLiteral("                ");

            
            #line 52 "..\..\Views\Provider\StandardResults.cshtml"
           Write(GetPaginationBackLink());

            
            #line default
            #line hidden
WriteLiteral("\r\n");

WriteLiteral("                ");

            
            #line 53 "..\..\Views\Provider\StandardResults.cshtml"
           Write(GetPaginationNextLink());

            
            #line default
            #line hidden
WriteLiteral("\r\n            </div>\r\n        </div>\r\n    </div>\r\n</main>\r\n\r\n");

            
            #line 59 "..\..\Views\Provider\StandardResults.cshtml"
 if (Model.TotalResults == 0)
{

            
            #line default
            #line hidden
WriteLiteral("    <script>\r\n        window.onload = function() {\r\n            SearchAndShortlis" +
"t.analytics.pushEvent(\"Provider Search\", \"No results\", \"Search\");\r\n        }\r\n  " +
"  </script> \r\n");

            
            #line 66 "..\..\Views\Provider\StandardResults.cshtml"
}

            
            #line default
            #line hidden
WriteLiteral("\r\n");

WriteLiteral("\r\n");

WriteLiteral("\r\n");

WriteLiteral("\r\n");

        }
    }
}
#pragma warning restore 1591
