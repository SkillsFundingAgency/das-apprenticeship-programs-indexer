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

namespace Sfa.Das.Sas.Web.Views.Apprenticeship
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
    
    #line 1 "..\..\Views\Apprenticeship\SearchResults.cshtml"
    using Sfa.Das.Sas.Web.ViewModels;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/Apprenticeship/SearchResults.cshtml")]
    public partial class SearchResults : System.Web.Mvc.WebViewPage<ApprenticeshipSearchResultViewModel>
    {

#line 88 "..\..\Views\Apprenticeship\SearchResults.cshtml"
public System.Web.WebPages.HelperResult GetPaginationBackLink()
{
#line default
#line hidden
return new System.Web.WebPages.HelperResult(__razor_helper_writer => {

#line 89 "..\..\Views\Apprenticeship\SearchResults.cshtml"
 
    if (Model.ActualPage > 1)
    {
        var previousPage = @Model.ActualPage - 1;
        var title = $"Previous page {@previousPage} of {@Model.LastPage}";
        var url = @Url.Action("SearchResults", "Apprenticeship", GetNavigationRouteValues(Model.SearchTerm, @previousPage, Model.AggregationLevel), null);
            

#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "<a");

WriteAttributeTo(__razor_helper_writer, "href", Tuple.Create(" href=\"", 3339), Tuple.Create("\"", 3350)

#line 95 "..\..\Views\Apprenticeship\SearchResults.cshtml"
, Tuple.Create(Tuple.Create("", 3346), Tuple.Create<System.Object, System.Int32>(url

#line default
#line hidden
, 3346), false)
);

WriteLiteralTo(__razor_helper_writer, " style=\"visibility: visible\"");

WriteLiteralTo(__razor_helper_writer, " class=\"page-navigation__btn prev\"");

WriteLiteralTo(__razor_helper_writer, ">\r\n                <i");

WriteLiteralTo(__razor_helper_writer, " class=\"arrow-button fa fa-angle-left\"");

WriteLiteralTo(__razor_helper_writer, "></i>\r\n                <span");

WriteLiteralTo(__razor_helper_writer, " class=\"description\"");

WriteLiteralTo(__razor_helper_writer, ">Previous <span");

WriteLiteralTo(__razor_helper_writer, " class=\"hide-mob\"");

WriteLiteralTo(__razor_helper_writer, ">page</span></span>\r\n                <span");

WriteLiteralTo(__razor_helper_writer, " class=\"counter\"");

WriteLiteralTo(__razor_helper_writer, ">");


#line 98 "..\..\Views\Apprenticeship\SearchResults.cshtml"
        WriteTo(__razor_helper_writer, previousPage);


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, " of ");


#line 98 "..\..\Views\Apprenticeship\SearchResults.cshtml"
                         WriteTo(__razor_helper_writer, Model.LastPage);


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "</span>\r\n            </a>\r\n");


#line 100 "..\..\Views\Apprenticeship\SearchResults.cshtml"
    }


#line default
#line hidden
});

#line 101 "..\..\Views\Apprenticeship\SearchResults.cshtml"
}
#line default
#line hidden

#line 104 "..\..\Views\Apprenticeship\SearchResults.cshtml"
public System.Web.WebPages.HelperResult GetPaginationNextLink()
{
#line default
#line hidden
return new System.Web.WebPages.HelperResult(__razor_helper_writer => {

#line 105 "..\..\Views\Apprenticeship\SearchResults.cshtml"
 
    if (Model.ActualPage < Model.LastPage)
    {
        var nextPage = @Model.ActualPage + 1;
        var title = $"Next page {@nextPage} of {@Model.LastPage}";

        //var url = $"Apprenticeship/SearchResults/keywords={Model.SearchTerm}&page={nextPage}";
        var url = @Url.Action("SearchResults", "Apprenticeship", GetNavigationRouteValues(Model.SearchTerm, nextPage, Model.AggregationLevel), null);
            

#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "<a");

WriteAttributeTo(__razor_helper_writer, "href", Tuple.Create(" href=\"", 4151), Tuple.Create("\"", 4162)

#line 113 "..\..\Views\Apprenticeship\SearchResults.cshtml"
, Tuple.Create(Tuple.Create("", 4158), Tuple.Create<System.Object, System.Int32>(url

#line default
#line hidden
, 4158), false)
);

WriteLiteralTo(__razor_helper_writer, " style=\"visibility: visible\"");

WriteLiteralTo(__razor_helper_writer, " class=\"page-navigation__btn next\"");

WriteLiteralTo(__razor_helper_writer, ">\r\n                <i");

WriteLiteralTo(__razor_helper_writer, " class=\"arrow-button fa fa-angle-right\"");

WriteLiteralTo(__razor_helper_writer, "></i>\r\n                <span");

WriteLiteralTo(__razor_helper_writer, " class=\"description\"");

WriteLiteralTo(__razor_helper_writer, ">Next <span");

WriteLiteralTo(__razor_helper_writer, " class=\"hide-mob\"");

WriteLiteralTo(__razor_helper_writer, ">page</span></span>\r\n                <span");

WriteLiteralTo(__razor_helper_writer, " class=\"counter\"");

WriteLiteralTo(__razor_helper_writer, ">");


#line 116 "..\..\Views\Apprenticeship\SearchResults.cshtml"
        WriteTo(__razor_helper_writer, nextPage);


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, " of ");


#line 116 "..\..\Views\Apprenticeship\SearchResults.cshtml"
                     WriteTo(__razor_helper_writer, Model.LastPage);


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "</span>\r\n            </a>\r\n");


#line 118 "..\..\Views\Apprenticeship\SearchResults.cshtml"
    }


#line default
#line hidden
});

#line 119 "..\..\Views\Apprenticeship\SearchResults.cshtml"
}
#line default
#line hidden

        #line 123 "..\..\Views\Apprenticeship\SearchResults.cshtml"
 
    RouteValueDictionary GetNavigationRouteValues(string keywords, int page, IEnumerable<LevelAggregationViewModel> selectedLevels)
    {

        var rv = new RouteValueDictionary { { "keywords", Model.SearchTerm }, { "page", page } };
        int i = 0;
        foreach (var level in selectedLevels.Where(m => m.Checked))
        {
            rv.Add("SelectedLevels[" + i + "]", level.Value);
            i++;
        }
        return rv;
    }

        #line default
        #line hidden

#line 139 "..\..\Views\Apprenticeship\SearchResults.cshtml"
public System.Web.WebPages.HelperResult GetStandardTitle(ApprenticeshipSearchResultItemViewModel item)
{
#line default
#line hidden
return new System.Web.WebPages.HelperResult(__razor_helper_writer => {

#line 140 "..\..\Views\Apprenticeship\SearchResults.cshtml"
 


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "    <h2");

WriteLiteralTo(__razor_helper_writer, " class=\"result-title\"");

WriteLiteralTo(__razor_helper_writer, ">\r\n");

WriteLiteralTo(__razor_helper_writer, "        ");


#line 142 "..\..\Views\Apprenticeship\SearchResults.cshtml"
WriteTo(__razor_helper_writer, Html.ActionLink(item.Title, "Standard", "Apprenticeship", new { @id = item.StandardId }, null));


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, " \r\n        <span>new</span>\r\n    </h2>\r\n");


#line 145 "..\..\Views\Apprenticeship\SearchResults.cshtml"


#line default
#line hidden
});

#line 145 "..\..\Views\Apprenticeship\SearchResults.cshtml"
}
#line default
#line hidden

#line 147 "..\..\Views\Apprenticeship\SearchResults.cshtml"
public System.Web.WebPages.HelperResult GetFrameworkTitle(ApprenticeshipSearchResultItemViewModel item)
{
#line default
#line hidden
return new System.Web.WebPages.HelperResult(__razor_helper_writer => {

#line 148 "..\..\Views\Apprenticeship\SearchResults.cshtml"
 


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "    <h2");

WriteLiteralTo(__razor_helper_writer, " class=\"result-title\"");

WriteLiteralTo(__razor_helper_writer, ">\r\n");

WriteLiteralTo(__razor_helper_writer, "        ");


#line 150 "..\..\Views\Apprenticeship\SearchResults.cshtml"
WriteTo(__razor_helper_writer, Html.ActionLink(item.Title, "Framework", "Apprenticeship", new { @id = item.FrameworkId }, null));


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "\r\n    </h2>\r\n");


#line 152 "..\..\Views\Apprenticeship\SearchResults.cshtml"


#line default
#line hidden
});

#line 152 "..\..\Views\Apprenticeship\SearchResults.cshtml"
}
#line default
#line hidden

#line 154 "..\..\Views\Apprenticeship\SearchResults.cshtml"
public System.Web.WebPages.HelperResult GetApprenticeshipDetailItem(string title, string id, string item, string unit = "")
{
#line default
#line hidden
return new System.Web.WebPages.HelperResult(__razor_helper_writer => {

#line 155 "..\..\Views\Apprenticeship\SearchResults.cshtml"
 
    if (!string.IsNullOrEmpty(item))
    {


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "        <dt>");


#line 158 "..\..\Views\Apprenticeship\SearchResults.cshtml"
WriteTo(__razor_helper_writer, title);


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "</dt>\r\n");

WriteLiteralTo(__razor_helper_writer, "        <dd");

WriteAttributeTo(__razor_helper_writer, "class", Tuple.Create(" class=\"", 5632), Tuple.Create("\"", 5643)

#line 159 "..\..\Views\Apprenticeship\SearchResults.cshtml"
, Tuple.Create(Tuple.Create("", 5640), Tuple.Create<System.Object, System.Int32>(id

#line default
#line hidden
, 5640), false)
);

WriteLiteralTo(__razor_helper_writer, ">");


#line 159 "..\..\Views\Apprenticeship\SearchResults.cshtml"
WriteTo(__razor_helper_writer, item);


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, " ");


#line 159 "..\..\Views\Apprenticeship\SearchResults.cshtml"
WriteTo(__razor_helper_writer, unit);


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "</dd>\r\n");


#line 160 "..\..\Views\Apprenticeship\SearchResults.cshtml"
    }


#line default
#line hidden
});

#line 161 "..\..\Views\Apprenticeship\SearchResults.cshtml"
}
#line default
#line hidden

#line 163 "..\..\Views\Apprenticeship\SearchResults.cshtml"
public System.Web.WebPages.HelperResult FilterForm(string className)
{
#line default
#line hidden
return new System.Web.WebPages.HelperResult(__razor_helper_writer => {

#line 164 "..\..\Views\Apprenticeship\SearchResults.cshtml"
 
    if (Model.TotalResults > 0)
    {


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "        <div");

WriteAttributeTo(__razor_helper_writer, "class", Tuple.Create(" class=\"", 5768), Tuple.Create("\"", 5786)

#line 167 "..\..\Views\Apprenticeship\SearchResults.cshtml"
, Tuple.Create(Tuple.Create("", 5776), Tuple.Create<System.Object, System.Int32>(className

#line default
#line hidden
, 5776), false)
);

WriteLiteralTo(__razor_helper_writer, ">\r\n            <form");

WriteLiteralTo(__razor_helper_writer, " method=\"get\"");

WriteLiteralTo(__razor_helper_writer, " autocomplete=\"off\"");

WriteLiteralTo(__razor_helper_writer, ">\r\n                <input");

WriteLiteralTo(__razor_helper_writer, " type=\"hidden\"");

WriteLiteralTo(__razor_helper_writer, " name=\"Keywords\"");

WriteAttributeTo(__razor_helper_writer, "value", Tuple.Create(" value=\"", 5894), Tuple.Create("\"", 5919)

#line 169 "..\..\Views\Apprenticeship\SearchResults.cshtml"
, Tuple.Create(Tuple.Create("", 5902), Tuple.Create<System.Object, System.Int32>(Model.SearchTerm

#line default
#line hidden
, 5902), false)
);

WriteLiteralTo(__razor_helper_writer, " />\r\n                <input");

WriteLiteralTo(__razor_helper_writer, " type=\"hidden\"");

WriteLiteralTo(__razor_helper_writer, " name=\"Page\"");

WriteAttributeTo(__razor_helper_writer, "value", Tuple.Create(" value=\"", 5973), Tuple.Create("\"", 5998)

#line 170 "..\..\Views\Apprenticeship\SearchResults.cshtml"
, Tuple.Create(Tuple.Create("", 5981), Tuple.Create<System.Object, System.Int32>(Model.ActualPage

#line default
#line hidden
, 5981), false)
);

WriteLiteralTo(__razor_helper_writer, " />\r\n                <input");

WriteLiteralTo(__razor_helper_writer, " type=\"hidden\"");

WriteLiteralTo(__razor_helper_writer, " name=\"Keywords\"");

WriteAttributeTo(__razor_helper_writer, "value", Tuple.Create(" value=\"", 6056), Tuple.Create("\"", 6081)

#line 171 "..\..\Views\Apprenticeship\SearchResults.cshtml"
, Tuple.Create(Tuple.Create("", 6064), Tuple.Create<System.Object, System.Int32>(Model.SearchTerm

#line default
#line hidden
, 6064), false)
);

WriteLiteralTo(__razor_helper_writer, " />\r\n\r\n                <fieldset");

WriteLiteralTo(__razor_helper_writer, " class=\"filters filters-accordion\"");

WriteLiteralTo(__razor_helper_writer, ">\r\n                    <h3");

WriteLiteralTo(__razor_helper_writer, " class=\"toggler heading-small\"");

WriteLiteralTo(__razor_helper_writer, ">Apprenticeship Level</h3>\r\n                    <div");

WriteLiteralTo(__razor_helper_writer, " class=\"toggled-content\"");

WriteLiteralTo(__razor_helper_writer, ">\r\n                        <ul");

WriteLiteralTo(__razor_helper_writer, " name=\"alist22\"");

WriteLiteralTo(__razor_helper_writer, ">\r\n");


#line 177 "..\..\Views\Apprenticeship\SearchResults.cshtml"
                            

#line default
#line hidden

#line 177 "..\..\Views\Apprenticeship\SearchResults.cshtml"
                             foreach (var item in Model.AggregationLevel.OrderBy(m => m.Value))
                            {


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "                                <li>\r\n                                    <input");

WriteAttributeTo(__razor_helper_writer, "value", Tuple.Create(" value=\"", 6536), Tuple.Create("\"", 6555)

#line 180 "..\..\Views\Apprenticeship\SearchResults.cshtml"
, Tuple.Create(Tuple.Create("", 6544), Tuple.Create<System.Object, System.Int32>(item.Value

#line default
#line hidden
, 6544), false)
);

WriteAttributeTo(__razor_helper_writer, "id", Tuple.Create(" id=\"", 6556), Tuple.Create("\"", 6587)
, Tuple.Create(Tuple.Create("", 6561), Tuple.Create("SelectedLevels_", 6561), true)

#line 180 "..\..\Views\Apprenticeship\SearchResults.cshtml"
  , Tuple.Create(Tuple.Create("", 6576), Tuple.Create<System.Object, System.Int32>(item.Value

#line default
#line hidden
, 6576), false)
);

WriteLiteralTo(__razor_helper_writer, " name=\"SelectedLevels\"");

WriteLiteralTo(__razor_helper_writer, " type=\"checkbox\"");

WriteLiteralTo(__razor_helper_writer, " ");


#line 180 "..\..\Views\Apprenticeship\SearchResults.cshtml"
                                                                                                       WriteTo(__razor_helper_writer, Html.Raw(item.Checked ? "checked" : ""));


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, " ");


#line 180 "..\..\Views\Apprenticeship\SearchResults.cshtml"
                                                                                                                                                WriteTo(__razor_helper_writer, Html.Raw(item.Count == 0 ? "disabled" : ""));


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, " />\r\n                                    <label");

WriteAttributeTo(__razor_helper_writer, "for", Tuple.Create(" for=\"", 6759), Tuple.Create("\"", 6791)
, Tuple.Create(Tuple.Create("", 6765), Tuple.Create("SelectedLevels_", 6765), true)

#line 181 "..\..\Views\Apprenticeship\SearchResults.cshtml"
, Tuple.Create(Tuple.Create("", 6780), Tuple.Create<System.Object, System.Int32>(item.Value

#line default
#line hidden
, 6780), false)
);

WriteAttributeTo(__razor_helper_writer, "class", Tuple.Create(" class=\"", 6792), Tuple.Create("\"", 6844)

#line 181 "..\..\Views\Apprenticeship\SearchResults.cshtml"
   , Tuple.Create(Tuple.Create("", 6800), Tuple.Create<System.Object, System.Int32>(Html.Raw(item.Count == 0 ? "disabled" : "")

#line default
#line hidden
, 6800), false)
);

WriteLiteralTo(__razor_helper_writer, ">Level ");


#line 181 "..\..\Views\Apprenticeship\SearchResults.cshtml"
                                                                                                         WriteTo(__razor_helper_writer, item.Value);


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, " (");


#line 181 "..\..\Views\Apprenticeship\SearchResults.cshtml"
                                                                                                                      WriteTo(__razor_helper_writer, item.Count);


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, ")</label>\r\n                                </li>\r\n");


#line 183 "..\..\Views\Apprenticeship\SearchResults.cshtml"
                            }


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "                        </ul>\r\n\r\n                        <details>\r\n             " +
"               <summary>Help with levels</summary>\r\n                            " +
"<div");

WriteLiteralTo(__razor_helper_writer, " class=\"panel panel-border-narrow\"");

WriteLiteralTo(__razor_helper_writer, @">
                                <p>
                                    What is here?
                                </p>
                            </div>
                        </details>
                    </div>
                </fieldset>
                <input");

WriteLiteralTo(__razor_helper_writer, " type=\"submit\"");

WriteLiteralTo(__razor_helper_writer, " class=\"button\"");

WriteLiteralTo(__razor_helper_writer, " value=\"Update results\"");

WriteLiteralTo(__razor_helper_writer, " />\r\n            </form>\r\n        </div>\r\n");


#line 199 "..\..\Views\Apprenticeship\SearchResults.cshtml"
    }


#line default
#line hidden
});

#line 200 "..\..\Views\Apprenticeship\SearchResults.cshtml"
}
#line default
#line hidden

        public SearchResults()
        {
        }
        public override void Execute()
        {
            
            #line 3 "..\..\Views\Apprenticeship\SearchResults.cshtml"
  
    ViewBag.Title = "Search Results";

            
            #line default
            #line hidden
WriteLiteral("\r\n<main");

WriteLiteral(" id=\"content\"");

WriteLiteral(" role=\"main\"");

WriteLiteral(">\r\n    <p");

WriteLiteral(" class=\"small-btm-margin\"");

WriteLiteral(">\r\n");

WriteLiteral("        ");

            
            #line 8 "..\..\Views\Apprenticeship\SearchResults.cshtml"
   Write(Html.ActionLink("Back", "Search", null, new { @class = "link-back" }));

            
            #line default
            #line hidden
WriteLiteral("\r\n    </p>\r\n    <div");

WriteLiteral(" id=\"apprenticeship-results\"");

WriteLiteral(" class=\"grid-row column-two-third\"");

WriteLiteral(">\r\n\r\n        <div");

WriteLiteral(" class=\"column-two-thirds\"");

WriteLiteral(">\r\n\r\n            <div>\r\n\r\n                <h1");

WriteLiteral(" class=\"heading-xlarge\"");

WriteLiteral(">\r\n                    Search results\r\n                </h1>\r\n\r\n            </div" +
">\r\n            <div>\r\n                <form");

WriteLiteral(" method=\"GET\"");

WriteLiteral(">\r\n                    <input");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" name=\"Keywords\"");

WriteAttribute("value", Tuple.Create(" value=\"", 659), Tuple.Create("\"", 684)
            
            #line 23 "..\..\Views\Apprenticeship\SearchResults.cshtml"
, Tuple.Create(Tuple.Create("", 667), Tuple.Create<System.Object, System.Int32>(Model.SearchTerm
            
            #line default
            #line hidden
, 667), false)
);

WriteLiteral(" />\r\n                    <input");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" name=\"page\"");

WriteAttribute("value", Tuple.Create(" value=\"", 742), Tuple.Create("\"", 767)
            
            #line 24 "..\..\Views\Apprenticeship\SearchResults.cshtml"
, Tuple.Create(Tuple.Create("", 750), Tuple.Create<System.Object, System.Int32>(Model.ActualPage
            
            #line default
            #line hidden
, 750), false)
);

WriteLiteral(" />\r\n                    <input");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" name=\"take\"");

WriteAttribute("value", Tuple.Create(" value=\"", 825), Tuple.Create("\"", 853)
            
            #line 25 "..\..\Views\Apprenticeship\SearchResults.cshtml"
, Tuple.Create(Tuple.Create("", 833), Tuple.Create<System.Object, System.Int32>(Model.ResultsToTake
            
            #line default
            #line hidden
, 833), false)
);

WriteLiteral("/>\r\n                    <select");

WriteLiteral(" name=\"order\"");

WriteLiteral(">\r\n                        <option");

WriteAttribute("selected", Tuple.Create(" selected=\"", 932), Tuple.Create("\"", 966)
            
            #line 27 "..\..\Views\Apprenticeship\SearchResults.cshtml"
, Tuple.Create(Tuple.Create("", 943), Tuple.Create<System.Object, System.Int32>(Model.SortOrder=="1"
            
            #line default
            #line hidden
, 943), false)
);

WriteLiteral(" value=\"1\"");

WriteLiteral(">Best match</option>\r\n                        <option");

WriteAttribute("selected", Tuple.Create(" selected=\"", 1030), Tuple.Create("\"", 1064)
            
            #line 28 "..\..\Views\Apprenticeship\SearchResults.cshtml"
, Tuple.Create(Tuple.Create("", 1041), Tuple.Create<System.Object, System.Int32>(Model.SortOrder=="2"
            
            #line default
            #line hidden
, 1041), false)
);

WriteLiteral(" value=\"2\"");

WriteLiteral(">Level (high to low)</option>\r\n                        <option");

WriteAttribute("selected", Tuple.Create(" selected=\"", 1137), Tuple.Create("\"", 1171)
            
            #line 29 "..\..\Views\Apprenticeship\SearchResults.cshtml"
, Tuple.Create(Tuple.Create("", 1148), Tuple.Create<System.Object, System.Int32>(Model.SortOrder=="3"
            
            #line default
            #line hidden
, 1148), false)
);

WriteLiteral(" value=\"3\"");

WriteLiteral(">Level (low to high)</option>\r\n                    </select>\r\n                   " +
" <input");

WriteLiteral(" type=\"submit\"");

WriteLiteral(" value=\"Sort\"");

WriteLiteral(" />\r\n                </form>\r\n            </div>\r\n            <p>\r\n");

            
            #line 35 "..\..\Views\Apprenticeship\SearchResults.cshtml"
                
            
            #line default
            #line hidden
            
            #line 35 "..\..\Views\Apprenticeship\SearchResults.cshtml"
                  
                    Html.RenderPartial("_SearchResultMessage");
                
            
            #line default
            #line hidden
WriteLiteral("\r\n            </p>\r\n\r\n        </div>\r\n\r\n        <div");

WriteLiteral(" class=\"column-one-third\"");

WriteLiteral(">\r\n");

WriteLiteral("            ");

            
            #line 43 "..\..\Views\Apprenticeship\SearchResults.cshtml"
       Write(FilterForm("filter-box"));

            
            #line default
            #line hidden
WriteLiteral("\r\n        </div>\r\n\r\n        <div");

WriteLiteral(" class=\"column-two-thirds\"");

WriteLiteral(">\r\n            \r\n");

            
            #line 48 "..\..\Views\Apprenticeship\SearchResults.cshtml"
            
            
            #line default
            #line hidden
            
            #line 48 "..\..\Views\Apprenticeship\SearchResults.cshtml"
             foreach (var item in Model.Results)
                {

            
            #line default
            #line hidden
WriteLiteral("                <article");

WriteAttribute("class", Tuple.Create(" class=\"", 1751), Tuple.Create("\"", 1799)
, Tuple.Create(Tuple.Create("", 1759), Tuple.Create("result", 1759), true)
            
            #line 50 "..\..\Views\Apprenticeship\SearchResults.cshtml"
, Tuple.Create(Tuple.Create(" ", 1765), Tuple.Create<System.Object, System.Int32>(item.ApprenticeshipType
            
            #line default
            #line hidden
, 1766), false)
, Tuple.Create(Tuple.Create("", 1792), Tuple.Create("-result", 1792), true)
);

WriteAttribute("id", Tuple.Create(" id=\"", 1800), Tuple.Create("\"", 1892)
            
            #line 50 "..\..\Views\Apprenticeship\SearchResults.cshtml"
, Tuple.Create(Tuple.Create("", 1805), Tuple.Create<System.Object, System.Int32>(item.ApprenticeshipType
            
            #line default
            #line hidden
, 1805), false)
, Tuple.Create(Tuple.Create("", 1831), Tuple.Create("-", 1831), true)
            
            #line 50 "..\..\Views\Apprenticeship\SearchResults.cshtml"
                          , Tuple.Create(Tuple.Create("", 1832), Tuple.Create<System.Object, System.Int32>(item.StandardId != 0 ? item.StandardId : item.FrameworkId
            
            #line default
            #line hidden
, 1832), false)
);

WriteLiteral(">\r\n                    <header>\r\n");

            
            #line 52 "..\..\Views\Apprenticeship\SearchResults.cshtml"
                        
            
            #line default
            #line hidden
            
            #line 52 "..\..\Views\Apprenticeship\SearchResults.cshtml"
                         if (item.StandardId != 0)
                        {
                            
            
            #line default
            #line hidden
            
            #line 54 "..\..\Views\Apprenticeship\SearchResults.cshtml"
                       Write(GetStandardTitle(item));

            
            #line default
            #line hidden
            
            #line 54 "..\..\Views\Apprenticeship\SearchResults.cshtml"
                                                   
                        }
                        else
                        {
                            
            
            #line default
            #line hidden
            
            #line 58 "..\..\Views\Apprenticeship\SearchResults.cshtml"
                       Write(GetFrameworkTitle(item));

            
            #line default
            #line hidden
            
            #line 58 "..\..\Views\Apprenticeship\SearchResults.cshtml"
                                                    
                        }

            
            #line default
            #line hidden
WriteLiteral("                    </header>\r\n                    <dl");

WriteLiteral(" class=\"result-data-list\"");

WriteLiteral(">\r\n");

WriteLiteral("                        ");

            
            #line 62 "..\..\Views\Apprenticeship\SearchResults.cshtml"
                   Write(GetApprenticeshipDetailItem("Level:", "level", item.Level));

            
            #line default
            #line hidden
WriteLiteral("\r\n");

WriteLiteral("                        ");

            
            #line 63 "..\..\Views\Apprenticeship\SearchResults.cshtml"
                   Write(GetApprenticeshipDetailItem("Typical length:", "length", item.TypicalLengthMessage));

            
            #line default
            #line hidden
WriteLiteral("\r\n                    </dl>\r\n                </article>\r\n");

            
            #line 66 "..\..\Views\Apprenticeship\SearchResults.cshtml"
            }

            
            #line default
            #line hidden
WriteLiteral("\r\n        </div>\r\n\r\n\r\n\r\n        <div");

WriteLiteral(" class=\"page-navigation\"");

WriteLiteral(">\r\n");

WriteLiteral("            ");

            
            #line 73 "..\..\Views\Apprenticeship\SearchResults.cshtml"
       Write(GetPaginationBackLink());

            
            #line default
            #line hidden
WriteLiteral("\r\n");

WriteLiteral("            ");

            
            #line 74 "..\..\Views\Apprenticeship\SearchResults.cshtml"
       Write(GetPaginationNextLink());

            
            #line default
            #line hidden
WriteLiteral("\r\n        </div> \r\n    </div>\r\n</main>\r\n\r\n");

            
            #line 79 "..\..\Views\Apprenticeship\SearchResults.cshtml"
 if (Model.TotalResults == 0)
{

            
            #line default
            #line hidden
WriteLiteral("    <script>\r\n        window.onload = function() {\r\n            SearchAndShortlis" +
"t.analytics.pushEvent(\"Apprenticeship Search\", \"No results\", \"Search\");\r\n       " +
" }\r\n    </script>\r\n");

            
            #line 86 "..\..\Views\Apprenticeship\SearchResults.cshtml"
}

            
            #line default
            #line hidden
WriteLiteral("\r\n");

WriteLiteral("\r\n\r\n");

WriteLiteral("\r\n\r\n");

WriteLiteral("\r\n\r\n");

WriteLiteral("\r\n");

WriteLiteral("\r\n");

WriteLiteral("\r\n");

        }
    }
}
#pragma warning restore 1591
