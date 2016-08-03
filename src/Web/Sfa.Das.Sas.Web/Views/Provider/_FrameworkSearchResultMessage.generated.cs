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
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/Provider/_FrameworkSearchResultMessage.cshtml")]
    public partial class FrameworkSearchResultMessage : System.Web.Mvc.WebViewPage<Sfa.Das.Sas.Web.ViewModels.ProviderFrameworkSearchResultViewModel>
    {

#line 35 "..\..\Views\Provider\_FrameworkSearchResultMessage.cshtml"
public System.Web.WebPages.HelperResult  RenderErrorMessage()
{
#line default
#line hidden
return new System.Web.WebPages.HelperResult(__razor_helper_writer => {

#line 36 "..\..\Views\Provider\_FrameworkSearchResultMessage.cshtml"
 


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "    <p>\r\n        There was a problem performing a search. Try again later.\r\n    <" +
"/p>\r\n");


#line 40 "..\..\Views\Provider\_FrameworkSearchResultMessage.cshtml"


#line default
#line hidden
});

#line 40 "..\..\Views\Provider\_FrameworkSearchResultMessage.cshtml"
}
#line default
#line hidden

#line 42 "..\..\Views\Provider\_FrameworkSearchResultMessage.cshtml"
public System.Web.WebPages.HelperResult  RenderZeroNationalProviders()
{
#line default
#line hidden
return new System.Web.WebPages.HelperResult(__razor_helper_writer => {

#line 43 "..\..\Views\Provider\_FrameworkSearchResultMessage.cshtml"
 


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "    <p");

WriteLiteralTo(__razor_helper_writer, " id=\"standard-provider-search-message\"");

WriteLiteralTo(__razor_helper_writer, ">\r\n        Sorry, there are currently no results for the filters you applied\'.\r\n " +
"   </p>\r\n");

WriteLiteralTo(__razor_helper_writer, "    <div>\r\n        <p>You can:</p>\r\n        <ul");

WriteLiteralTo(__razor_helper_writer, " class=\"list list-bullet\"");

WriteLiteralTo(__razor_helper_writer, ">\r\n            <li");

WriteLiteralTo(__razor_helper_writer, " class=\"return-search-results\"");

WriteLiteralTo(__razor_helper_writer, ">");


#line 50 "..\..\Views\Provider\_FrameworkSearchResultMessage.cshtml"
                WriteTo(__razor_helper_writer, Html.ActionLink("return to your apprenticeship training search results", "StandardResults", "Provider", new { @apprenticeshipid = @Model.FrameworkId, @postcode = @Model.PostCode }, new { }));


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "</li>\r\n            <li");

WriteLiteralTo(__razor_helper_writer, " class=\"start-again\"");

WriteLiteralTo(__razor_helper_writer, ">");


#line 51 "..\..\Views\Provider\_FrameworkSearchResultMessage.cshtml"
      WriteTo(__razor_helper_writer, Html.ActionLink("start your keyword search again", "Search", "Apprenticeship"));


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "</li>\r\n        </ul>\r\n    </div>\r\n");


#line 54 "..\..\Views\Provider\_FrameworkSearchResultMessage.cshtml"


#line default
#line hidden
});

#line 54 "..\..\Views\Provider\_FrameworkSearchResultMessage.cshtml"
}
#line default
#line hidden

#line 56 "..\..\Views\Provider\_FrameworkSearchResultMessage.cshtml"
public System.Web.WebPages.HelperResult  RenderAllOneResults()
{
#line default
#line hidden
return new System.Web.WebPages.HelperResult(__razor_helper_writer => {

#line 57 "..\..\Views\Provider\_FrameworkSearchResultMessage.cshtml"
 


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "    <p");

WriteLiteralTo(__razor_helper_writer, " id=\"standard-provider-search-message\"");

WriteLiteralTo(__razor_helper_writer, ">\r\n        There is <b");

WriteLiteralTo(__razor_helper_writer, " id=\"total-results\"");

WriteLiteralTo(__razor_helper_writer, ">");


#line 59 "..\..\Views\Provider\_FrameworkSearchResultMessage.cshtml"
         WriteTo(__razor_helper_writer, Model.TotalResults);


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "</b> training provider for <b");

WriteLiteralTo(__razor_helper_writer, " id=\"standard-name\"");

WriteLiteralTo(__razor_helper_writer, ">");


#line 59 "..\..\Views\Provider\_FrameworkSearchResultMessage.cshtml"
                                                                             WriteTo(__razor_helper_writer, Model.Title);


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, " level ");


#line 59 "..\..\Views\Provider\_FrameworkSearchResultMessage.cshtml"
                                                                                                WriteTo(__razor_helper_writer, Model.FrameworkLevel);


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "</b> in England.\r\n    </p>\r\n");

WriteLiteralTo(__razor_helper_writer, "    <p>\r\n        Results are ordered by distance from \'<b");

WriteLiteralTo(__razor_helper_writer, " id=\"postalcode\"");

WriteLiteralTo(__razor_helper_writer, ">");


#line 62 "..\..\Views\Provider\_FrameworkSearchResultMessage.cshtml"
                                   WriteTo(__razor_helper_writer, Model.PostCode);


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "</b>\'.\r\n    </p>\r\n");

WriteLiteralTo(__razor_helper_writer, "    <p>\r\n        Results labelled <span");

WriteLiteralTo(__razor_helper_writer, " class=\"tag tag-new\"");

WriteLiteralTo(__razor_helper_writer, ">National</span> are training providers who are willing to offer apprenticeship t" +
"raining across England\r\n    </p>\r\n");


#line 67 "..\..\Views\Provider\_FrameworkSearchResultMessage.cshtml"


#line default
#line hidden
});

#line 67 "..\..\Views\Provider\_FrameworkSearchResultMessage.cshtml"
}
#line default
#line hidden

#line 69 "..\..\Views\Provider\_FrameworkSearchResultMessage.cshtml"
public System.Web.WebPages.HelperResult  RenderAllResults()
{
#line default
#line hidden
return new System.Web.WebPages.HelperResult(__razor_helper_writer => {

#line 70 "..\..\Views\Provider\_FrameworkSearchResultMessage.cshtml"
 


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "    <p");

WriteLiteralTo(__razor_helper_writer, " id=\"standard-provider-search-message\"");

WriteLiteralTo(__razor_helper_writer, ">\r\n        There are <b");

WriteLiteralTo(__razor_helper_writer, " id=\"total-results\"");

WriteLiteralTo(__razor_helper_writer, ">");


#line 72 "..\..\Views\Provider\_FrameworkSearchResultMessage.cshtml"
          WriteTo(__razor_helper_writer, Model.TotalResults);


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "</b> training providers for <b");

WriteLiteralTo(__razor_helper_writer, " id=\"standard-name\"");

WriteLiteralTo(__razor_helper_writer, ">");


#line 72 "..\..\Views\Provider\_FrameworkSearchResultMessage.cshtml"
                                                                               WriteTo(__razor_helper_writer, Model.Title);


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, " level ");


#line 72 "..\..\Views\Provider\_FrameworkSearchResultMessage.cshtml"
                                                                                                  WriteTo(__razor_helper_writer, Model.FrameworkLevel);


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "</b> in England.\r\n    </p>\r\n");

WriteLiteralTo(__razor_helper_writer, "    <p>\r\n        Results are ordered by distance from \'<b");

WriteLiteralTo(__razor_helper_writer, " id=\"postalcode\"");

WriteLiteralTo(__razor_helper_writer, ">");


#line 75 "..\..\Views\Provider\_FrameworkSearchResultMessage.cshtml"
                                   WriteTo(__razor_helper_writer, Model.PostCode);


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "</b>\'.\r\n    </p>\r\n");

WriteLiteralTo(__razor_helper_writer, "    <p>\r\n        Results labelled <span");

WriteLiteralTo(__razor_helper_writer, " class=\"tag tag-new\"");

WriteLiteralTo(__razor_helper_writer, ">National</span> are training providers who are willing to offer apprenticeship t" +
"raining across England\r\n    </p>\r\n");


#line 80 "..\..\Views\Provider\_FrameworkSearchResultMessage.cshtml"


#line default
#line hidden
});

#line 80 "..\..\Views\Provider\_FrameworkSearchResultMessage.cshtml"
}
#line default
#line hidden

#line 82 "..\..\Views\Provider\_FrameworkSearchResultMessage.cshtml"
public System.Web.WebPages.HelperResult RenderZeroResult()
{
#line default
#line hidden
return new System.Web.WebPages.HelperResult(__razor_helper_writer => {

#line 83 "..\..\Views\Provider\_FrameworkSearchResultMessage.cshtml"
 


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "    <p>\r\n        Sorry, there are currently no training providers for ");


#line 85 "..\..\Views\Provider\_FrameworkSearchResultMessage.cshtml"
                               WriteTo(__razor_helper_writer, Model.Title);


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, " level ");


#line 85 "..\..\Views\Provider\_FrameworkSearchResultMessage.cshtml"
                                                  WriteTo(__razor_helper_writer, Model.FrameworkLevel);


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, " for \'<b");

WriteLiteralTo(__razor_helper_writer, " id=\"postalcode\"");

WriteLiteralTo(__razor_helper_writer, ">");


#line 85 "..\..\Views\Provider\_FrameworkSearchResultMessage.cshtml"
                                                                                                WriteTo(__razor_helper_writer, Model.PostCode);


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "</b>\'.\r\n    </p>\r\n");

WriteLiteralTo(__razor_helper_writer, "    <div>\r\n        You can:\r\n        <ul");

WriteLiteralTo(__razor_helper_writer, " class=\"list list-bullet\"");

WriteLiteralTo(__razor_helper_writer, ">\r\n");


#line 90 "..\..\Views\Provider\_FrameworkSearchResultMessage.cshtml"
            

#line default
#line hidden

#line 90 "..\..\Views\Provider\_FrameworkSearchResultMessage.cshtml"
             if (Model.TotalProvidersCountry > 0)
            {


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "                <li");

WriteLiteralTo(__razor_helper_writer, " class=\"total-providers-country\"");

WriteLiteralTo(__razor_helper_writer, ">");


#line 92 "..\..\Views\Provider\_FrameworkSearchResultMessage.cshtml"
                      WriteTo(__razor_helper_writer, Html.ActionLink($"view all ({@Model.TotalProvidersCountry}) training providers", "FrameworkResults", "Provider", new { @apprenticeshipId = @Model.FrameworkId, @postcode = Model.PostCode, @showAll = true }, new { @class = "" }));


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, " for ");


#line 92 "..\..\Views\Provider\_FrameworkSearchResultMessage.cshtml"
                                                                                                                                                                                                                                                              WriteTo(__razor_helper_writer, Model.FrameworkName);


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, " in England</li>\r\n");


#line 93 "..\..\Views\Provider\_FrameworkSearchResultMessage.cshtml"
            }


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "            <li");

WriteLiteralTo(__razor_helper_writer, " class=\"return-search-results\"");

WriteLiteralTo(__razor_helper_writer, ">");


#line 94 "..\..\Views\Provider\_FrameworkSearchResultMessage.cshtml"
                WriteTo(__razor_helper_writer, Html.ActionLink("return to your apprenticeship training search results", "SearchResults", "Apprenticeship", new { @keywords = @Model.SearchTerms }, new { }));


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "</li>\r\n            <li");

WriteLiteralTo(__razor_helper_writer, " class=\"start-again\"");

WriteLiteralTo(__razor_helper_writer, ">");


#line 95 "..\..\Views\Provider\_FrameworkSearchResultMessage.cshtml"
      WriteTo(__razor_helper_writer, Html.ActionLink("start your keyword search again", "Search", "Apprenticeship"));


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "</li>\r\n        </ul>\r\n    </div>\r\n");


#line 98 "..\..\Views\Provider\_FrameworkSearchResultMessage.cshtml"


#line default
#line hidden
});

#line 98 "..\..\Views\Provider\_FrameworkSearchResultMessage.cshtml"
}
#line default
#line hidden

#line 100 "..\..\Views\Provider\_FrameworkSearchResultMessage.cshtml"
public System.Web.WebPages.HelperResult  RenderMessageOneResult()
{
#line default
#line hidden
return new System.Web.WebPages.HelperResult(__razor_helper_writer => {

#line 101 "..\..\Views\Provider\_FrameworkSearchResultMessage.cshtml"
 


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "    <p>\r\n        There is <b");

WriteLiteralTo(__razor_helper_writer, " id=\"total-results\"");

WriteLiteralTo(__razor_helper_writer, ">");


#line 103 "..\..\Views\Provider\_FrameworkSearchResultMessage.cshtml"
         WriteTo(__razor_helper_writer, Model.TotalResults);


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "</b> training provider for the apprenticeship: <b");

WriteLiteralTo(__razor_helper_writer, " id=\"standard-name\"");

WriteLiteralTo(__razor_helper_writer, ">");


#line 103 "..\..\Views\Provider\_FrameworkSearchResultMessage.cshtml"
                                                                                                 WriteTo(__razor_helper_writer, Model.Title);


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, " level ");


#line 103 "..\..\Views\Provider\_FrameworkSearchResultMessage.cshtml"
                                                                                                                    WriteTo(__razor_helper_writer, Model.FrameworkLevel);


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "</b>.\r\n    </p>\r\n");

WriteLiteralTo(__razor_helper_writer, "    <p>\r\n        Results are ordered by distance from \'<b");

WriteLiteralTo(__razor_helper_writer, " id=\"postalcode\"");

WriteLiteralTo(__razor_helper_writer, ">");


#line 106 "..\..\Views\Provider\_FrameworkSearchResultMessage.cshtml"
                                   WriteTo(__razor_helper_writer, Model.PostCode);


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "</b>\'.\r\n    </p>\r\n");

WriteLiteralTo(__razor_helper_writer, "    <p>\r\n        Results labelled <span");

WriteLiteralTo(__razor_helper_writer, " class=\"tag tag-new\"");

WriteLiteralTo(__razor_helper_writer, ">National</span> are training providers who are willing to offer apprenticeship t" +
"raining across England\r\n    </p>\r\n");


#line 111 "..\..\Views\Provider\_FrameworkSearchResultMessage.cshtml"


#line default
#line hidden
});

#line 111 "..\..\Views\Provider\_FrameworkSearchResultMessage.cshtml"
}
#line default
#line hidden

#line 114 "..\..\Views\Provider\_FrameworkSearchResultMessage.cshtml"
public System.Web.WebPages.HelperResult  RenderMessage()
{
#line default
#line hidden
return new System.Web.WebPages.HelperResult(__razor_helper_writer => {

#line 115 "..\..\Views\Provider\_FrameworkSearchResultMessage.cshtml"
 


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "    <p>\r\n        There are <b");

WriteLiteralTo(__razor_helper_writer, " id=\"total-results\"");

WriteLiteralTo(__razor_helper_writer, ">");


#line 117 "..\..\Views\Provider\_FrameworkSearchResultMessage.cshtml"
          WriteTo(__razor_helper_writer, Model.TotalResults);


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "</b> training providers for the apprenticeship: <b>");


#line 117 "..\..\Views\Provider\_FrameworkSearchResultMessage.cshtml"
                                                                                WriteTo(__razor_helper_writer, Model.Title);


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, " level ");


#line 117 "..\..\Views\Provider\_FrameworkSearchResultMessage.cshtml"
                                                                                                   WriteTo(__razor_helper_writer, Model.FrameworkLevel);


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "</b>.\r\n    </p>\r\n");

WriteLiteralTo(__razor_helper_writer, "    <p>\r\n        Results are ordered by distance from \'<b>");


#line 120 "..\..\Views\Provider\_FrameworkSearchResultMessage.cshtml"
                   WriteTo(__razor_helper_writer, Model.PostCode);


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "</b>\'.\r\n    </p>\r\n");

WriteLiteralTo(__razor_helper_writer, "    <p>\r\n        Results labelled <span");

WriteLiteralTo(__razor_helper_writer, " class=\"tag tag-new\"");

WriteLiteralTo(__razor_helper_writer, ">National</span> are training providers who are willing to offer apprenticeship t" +
"raining across England\r\n    </p>\r\n");


#line 125 "..\..\Views\Provider\_FrameworkSearchResultMessage.cshtml"


#line default
#line hidden
});

#line 125 "..\..\Views\Provider\_FrameworkSearchResultMessage.cshtml"
}
#line default
#line hidden

        public FrameworkSearchResultMessage()
        {
        }
        public override void Execute()
        {
WriteLiteral("<div");

WriteLiteral(" class=\"result-message\"");

WriteLiteral(">\r\n\r\n");

            
            #line 5 "..\..\Views\Provider\_FrameworkSearchResultMessage.cshtml"
    
            
            #line default
            #line hidden
            
            #line 5 "..\..\Views\Provider\_FrameworkSearchResultMessage.cshtml"
     if (Model.HasError)
    {
        
            
            #line default
            #line hidden
            
            #line 7 "..\..\Views\Provider\_FrameworkSearchResultMessage.cshtml"
   Write(RenderErrorMessage());

            
            #line default
            #line hidden
            
            #line 7 "..\..\Views\Provider\_FrameworkSearchResultMessage.cshtml"
                             
    }
    else if (Model.ShowNationalProviders && Model.TotalResults == 0)
    {
        
            
            #line default
            #line hidden
            
            #line 11 "..\..\Views\Provider\_FrameworkSearchResultMessage.cshtml"
   Write(RenderZeroNationalProviders());

            
            #line default
            #line hidden
            
            #line 11 "..\..\Views\Provider\_FrameworkSearchResultMessage.cshtml"
                                      
    }
    else if (Model.ShowAll && Model.TotalResults == 1)
    {
        
            
            #line default
            #line hidden
            
            #line 15 "..\..\Views\Provider\_FrameworkSearchResultMessage.cshtml"
   Write(RenderAllOneResults());

            
            #line default
            #line hidden
            
            #line 15 "..\..\Views\Provider\_FrameworkSearchResultMessage.cshtml"
                              
    }
    else if (Model.ShowAll && Model.TotalResults != 1)
    {
        
            
            #line default
            #line hidden
            
            #line 19 "..\..\Views\Provider\_FrameworkSearchResultMessage.cshtml"
   Write(RenderAllResults());

            
            #line default
            #line hidden
            
            #line 19 "..\..\Views\Provider\_FrameworkSearchResultMessage.cshtml"
                           
    }
    else if (Model.TotalResults == 0)
    {
        
            
            #line default
            #line hidden
            
            #line 23 "..\..\Views\Provider\_FrameworkSearchResultMessage.cshtml"
   Write(RenderZeroResult());

            
            #line default
            #line hidden
            
            #line 23 "..\..\Views\Provider\_FrameworkSearchResultMessage.cshtml"
                           ;
    }
    else if (Model.TotalResults == 1)
    {
        
            
            #line default
            #line hidden
            
            #line 27 "..\..\Views\Provider\_FrameworkSearchResultMessage.cshtml"
   Write(RenderMessageOneResult());

            
            #line default
            #line hidden
            
            #line 27 "..\..\Views\Provider\_FrameworkSearchResultMessage.cshtml"
                                 
    }
    else
    {
        
            
            #line default
            #line hidden
            
            #line 31 "..\..\Views\Provider\_FrameworkSearchResultMessage.cshtml"
   Write(RenderMessage());

            
            #line default
            #line hidden
            
            #line 31 "..\..\Views\Provider\_FrameworkSearchResultMessage.cshtml"
                        
    }

            
            #line default
            #line hidden
WriteLiteral("</div>\r\n\r\n");

WriteLiteral("\r\n");

WriteLiteral("\r\n");

WriteLiteral("\r\n");

WriteLiteral("\r\n");

WriteLiteral("\r\n");

WriteLiteral("\r\n\r\n");

WriteLiteral("\r\n\r\n");

        }
    }
}
#pragma warning restore 1591
