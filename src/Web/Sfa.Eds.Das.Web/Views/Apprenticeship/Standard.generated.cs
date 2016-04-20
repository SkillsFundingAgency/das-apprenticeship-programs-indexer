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

namespace Sfa.Eds.Das.Web.Views.Apprenticeship
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
    
    #line 1 "..\..\Views\Apprenticeship\Standard.cshtml"
    using Resources = Sfa.Eds.Das.Resources.EquivalenceLevels;
    
    #line default
    #line hidden
    using Sfa.Eds.Das.Web;
    
    #line 2 "..\..\Views\Apprenticeship\Standard.cshtml"
    using Sfa.Eds.Das.Web.Extensions;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/Apprenticeship/Standard.cshtml")]
    public partial class Standard : System.Web.Mvc.WebViewPage<Sfa.Eds.Das.Web.ViewModels.StandardViewModel>
    {

#line 110 "..\..\Views\Apprenticeship\Standard.cshtml"
public System.Web.WebPages.HelperResult GetStandardDetailItem(string title, string item, string unit = "")
{
#line default
#line hidden
return new System.Web.WebPages.HelperResult(__razor_helper_writer => {

#line 111 "..\..\Views\Apprenticeship\Standard.cshtml"
 
    if (!string.IsNullOrEmpty(item))
    {


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "            <dt>");


#line 114 "..\..\Views\Apprenticeship\Standard.cshtml"
WriteTo(__razor_helper_writer, title);


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "</dt>\r\n");

WriteLiteralTo(__razor_helper_writer, "                <dd>");


#line 115 "..\..\Views\Apprenticeship\Standard.cshtml"
WriteTo(__razor_helper_writer, item);


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, " ");


#line 115 "..\..\Views\Apprenticeship\Standard.cshtml"
WriteTo(__razor_helper_writer, unit);


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "</dd>\r\n");


#line 116 "..\..\Views\Apprenticeship\Standard.cshtml"
    }


#line default
#line hidden
});

#line 117 "..\..\Views\Apprenticeship\Standard.cshtml"
}
#line default
#line hidden

#line 119 "..\..\Views\Apprenticeship\Standard.cshtml"
public System.Web.WebPages.HelperResult GetStandardLevel(string item)
{
#line default
#line hidden
return new System.Web.WebPages.HelperResult(__razor_helper_writer => {

#line 120 "..\..\Views\Apprenticeship\Standard.cshtml"
 
    if (!string.IsNullOrEmpty(item))
    {


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "        <dt>Level</dt>\r\n");


#line 124 "..\..\Views\Apprenticeship\Standard.cshtml"
        var equivalence = string.Empty;
        switch (int.Parse(@item))
        {
            case 1:
                equivalence = @Resources.FirstLevel;
                break;
            case 2:
                equivalence = @Resources.SecondLevel;
                break;
            case 3:
                equivalence = @Resources.ThirdLevel;
                break;
            case 4:
                equivalence = @Resources.FourthLevel;
                break;
            case 5:
                equivalence = @Resources.FifthLevel;
                break;
            case 6:
                equivalence = @Resources.SixthLevel;
                break;
            case 7:
                equivalence = @Resources.SeventhLevel;
                break;
            case 8:
                equivalence = @Resources.EighthLevel;
                break;
            default:
                break;

        }


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "        <dd");

WriteLiteralTo(__razor_helper_writer, " id=\"level\"");

WriteLiteralTo(__razor_helper_writer, ">");


#line 155 "..\..\Views\Apprenticeship\Standard.cshtml"
WriteTo(__razor_helper_writer, item);


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, " (equivalent to ");


#line 155 "..\..\Views\Apprenticeship\Standard.cshtml"
              WriteTo(__razor_helper_writer, equivalence);


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, ")</dd>\r\n");


#line 156 "..\..\Views\Apprenticeship\Standard.cshtml"
    }


#line default
#line hidden
});

#line 157 "..\..\Views\Apprenticeship\Standard.cshtml"
}
#line default
#line hidden

#line 159 "..\..\Views\Apprenticeship\Standard.cshtml"
public System.Web.WebPages.HelperResult GetStandardProperty(string title, string id, string item, bool hideIfEmpty = false)
{
#line default
#line hidden
return new System.Web.WebPages.HelperResult(__razor_helper_writer => {

#line 160 "..\..\Views\Apprenticeship\Standard.cshtml"
 
    if (!string.IsNullOrEmpty(item) || !hideIfEmpty)
    {



#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "        <dt>");


#line 164 "..\..\Views\Apprenticeship\Standard.cshtml"
WriteTo(__razor_helper_writer, title);


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "</dt>\r\n");

WriteLiteralTo(__razor_helper_writer, "        <dd");

WriteAttributeTo(__razor_helper_writer, "id", Tuple.Create(" id=\"", 6585), Tuple.Create("\"", 6593)

#line 165 "..\..\Views\Apprenticeship\Standard.cshtml"
, Tuple.Create(Tuple.Create("", 6590), Tuple.Create<System.Object, System.Int32>(id

#line default
#line hidden
, 6590), false)
);

WriteLiteralTo(__razor_helper_writer, ">");


#line 165 "..\..\Views\Apprenticeship\Standard.cshtml"
WriteTo(__razor_helper_writer, Html.MarkdownToHtml(item));


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "</dd>\r\n");


#line 166 "..\..\Views\Apprenticeship\Standard.cshtml"

    }


#line default
#line hidden
});

#line 168 "..\..\Views\Apprenticeship\Standard.cshtml"
}
#line default
#line hidden

#line 170 "..\..\Views\Apprenticeship\Standard.cshtml"
public System.Web.WebPages.HelperResult GetDocumentItem(string pdfUrl, string title)
{
#line default
#line hidden
return new System.Web.WebPages.HelperResult(__razor_helper_writer => {

#line 171 "..\..\Views\Apprenticeship\Standard.cshtml"
 
    if (!string.IsNullOrEmpty(title))
    {


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "            <li>\r\n                <a");

WriteAttributeTo(__razor_helper_writer, "href", Tuple.Create(" href=\"", 6781), Tuple.Create("\"", 6795)

#line 175 "..\..\Views\Apprenticeship\Standard.cshtml"
, Tuple.Create(Tuple.Create("", 6788), Tuple.Create<System.Object, System.Int32>(pdfUrl

#line default
#line hidden
, 6788), false)
);

WriteLiteralTo(__razor_helper_writer, " target=\"_blank\"");

WriteLiteralTo(__razor_helper_writer, ">\r\n");

WriteLiteralTo(__razor_helper_writer, "                    ");


#line 176 "..\..\Views\Apprenticeship\Standard.cshtml"
WriteTo(__razor_helper_writer, title);


#line default
#line hidden
WriteLiteralTo(__razor_helper_writer, "\r\n                </a>\r\n                <p>\r\n                    PDF, 268KB, 2 pa" +
"ges\r\n                </p>\r\n            </li>\r\n");


#line 182 "..\..\Views\Apprenticeship\Standard.cshtml"
    }


#line default
#line hidden
});

#line 183 "..\..\Views\Apprenticeship\Standard.cshtml"
}
#line default
#line hidden

        public Standard()
        {
        }
        public override void Execute()
        {
            
            #line 5 "..\..\Views\Apprenticeship\Standard.cshtml"
  
    ViewBag.Title = "Standard - " + @Model.Title;

            
            #line default
            #line hidden
WriteLiteral("\r\n\r\n<main");

WriteLiteral(" id=\"content\"");

WriteLiteral(">\r\n\r\n    <p");

WriteLiteral(" class=\"small-btm-margin\"");

WriteLiteral(">\r\n");

WriteLiteral("        ");

            
            #line 12 "..\..\Views\Apprenticeship\Standard.cshtml"
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

            
            #line 20 "..\..\Views\Apprenticeship\Standard.cshtml"
               Write(Model.Title);

            
            #line default
            #line hidden
WriteLiteral("\r\n                </h1>\r\n");

            
            #line 22 "..\..\Views\Apprenticeship\Standard.cshtml"
                
            
            #line default
            #line hidden
            
            #line 22 "..\..\Views\Apprenticeship\Standard.cshtml"
                 if (!string.IsNullOrEmpty(@Model.IntroductoryText))
                {

            
            #line default
            #line hidden
WriteLiteral("                    <div");

WriteLiteral(" class=\"standard-result-summary\"");

WriteLiteral(">\r\n");

WriteLiteral("                        ");

            
            #line 25 "..\..\Views\Apprenticeship\Standard.cshtml"
                   Write(Html.MarkdownToHtml(Model.IntroductoryText));

            
            #line default
            #line hidden
WriteLiteral("\r\n                    </div>\r\n");

            
            #line 27 "..\..\Views\Apprenticeship\Standard.cshtml"
                }

            
            #line default
            #line hidden
WriteLiteral("            </div>\r\n        </div>\r\n\r\n\r\n        <div");

WriteLiteral(" class=\"column-third\"");

WriteLiteral(">\r\n");

            
            #line 33 "..\..\Views\Apprenticeship\Standard.cshtml"
            
            
            #line default
            #line hidden
            
            #line 33 "..\..\Views\Apprenticeship\Standard.cshtml"
             using (Html.BeginForm("StandardResults", "Provider", FormMethod.Get, new {@class = "search-box hidden-for-mobile postcode-form-top" }))
            {

            
            #line default
            #line hidden
WriteLiteral("                <div");

WriteLiteral(" class=\"related-container\"");

WriteLiteral(">\r\n                    <aside");

WriteLiteral(" class=\"related\"");

WriteLiteral(">\r\n\r\n                        <h2");

WriteLiteral(" class=\"heading-medium\"");

WriteLiteral(">Find training providers</h2>\r\n                        <div");

WriteAttribute("class", Tuple.Create(" class=\"", 1303), Tuple.Create("\"", 1356)
, Tuple.Create(Tuple.Create("", 1311), Tuple.Create("form-group", 1311), true)
            
            #line 39 "..\..\Views\Apprenticeship\Standard.cshtml"
, Tuple.Create(Tuple.Create(" ", 1321), Tuple.Create<System.Object, System.Int32>(@Model.HasError ? " error" : ""
            
            #line default
            #line hidden
, 1322), false)
);

WriteLiteral(">\r\n                            <label");

WriteLiteral(" class=\"form-label\"");

WriteLiteral(" for=\"postcode\"");

WriteLiteral(">\r\n                                Enter postcode");

WriteLiteral("\r\n                                <p>\r\n");

            
            #line 43 "..\..\Views\Apprenticeship\Standard.cshtml"
                                    
            
            #line default
            #line hidden
            
            #line 43 "..\..\Views\Apprenticeship\Standard.cshtml"
                                      
                                        Html.RenderPartial("_BlankFieldErrorMessage");
                                    
            
            #line default
            #line hidden
WriteLiteral("\r\n                                </p>\r\n                            </label>\r\n   " +
"                         <input");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" id=\"apprenticeshipid\"");

WriteLiteral(" name=\"apprenticeshipid\"");

WriteLiteral(" class=\"text-box form-control\"");

WriteAttribute("value", Tuple.Create(" value=\"", 1908), Tuple.Create("\"", 1933)
            
            #line 48 "..\..\Views\Apprenticeship\Standard.cshtml"
                                                    , Tuple.Create(Tuple.Create("", 1916), Tuple.Create<System.Object, System.Int32>(Model.StandardId
            
            #line default
            #line hidden
, 1916), false)
);

WriteLiteral(">\r\n                            <input");

WriteLiteral(" type=\"search\"");

WriteLiteral(" id=\"postcode\"");

WriteLiteral(" name=\"PostCode\"");

WriteLiteral(" class=\"text-box form-control postcode-search-box\"");

WriteLiteral(" maxlength=\"200\"");

WriteLiteral(" placeholder=\"\"");

WriteLiteral(">\r\n                        </div>\r\n                        <input");

WriteLiteral(" class=\"button margin-top-x2 postcode-search-button\"");

WriteLiteral(" id=\"submit-postcode\"");

WriteLiteral(" type=\"submit\"");

WriteLiteral(" value=\"Search\"");

WriteLiteral("/>\r\n                    </aside>\r\n                </div>\r\n");

            
            #line 54 "..\..\Views\Apprenticeship\Standard.cshtml"
            }

            
            #line default
            #line hidden
WriteLiteral("\r\n        </div>\r\n        </div>\r\n\r\n        <section>\r\n            <header>\r\n    " +
"            <h2");

WriteLiteral(" class=\"heading-large\"");

WriteLiteral(">\r\n                    Summary of apprenticeship standard\r\n                </h2>\r" +
"\n            </header>\r\n            <dl");

WriteLiteral(" class=\"data-list\"");

WriteLiteral(">\r\n");

WriteLiteral("                ");

            
            #line 66 "..\..\Views\Apprenticeship\Standard.cshtml"
           Write(GetStandardProperty("Overview of role", "overview", Model.OverviewOfRole));

            
            #line default
            #line hidden
WriteLiteral("\r\n");

WriteLiteral("                ");

            
            #line 67 "..\..\Views\Apprenticeship\Standard.cshtml"
           Write(GetStandardLevel(@Model.NotionalEndLevel.ToString()));

            
            #line default
            #line hidden
WriteLiteral("\r\n");

WriteLiteral("                ");

            
            #line 68 "..\..\Views\Apprenticeship\Standard.cshtml"
           Write(GetStandardProperty("Typical length", "length", @Model.TypicalLengthMessage));

            
            #line default
            #line hidden
WriteLiteral("\r\n");

WriteLiteral("                ");

            
            #line 69 "..\..\Views\Apprenticeship\Standard.cshtml"
           Write(GetStandardProperty("Entry requirements", "entry-requirements", Model.EntryRequirements));

            
            #line default
            #line hidden
WriteLiteral("\r\n");

WriteLiteral("                ");

            
            #line 70 "..\..\Views\Apprenticeship\Standard.cshtml"
           Write(GetStandardProperty("What apprentices will learn", "will-learn", Model.WhatApprenticesWillLearn));

            
            #line default
            #line hidden
WriteLiteral("\r\n");

WriteLiteral("                ");

            
            #line 71 "..\..\Views\Apprenticeship\Standard.cshtml"
           Write(GetStandardProperty("Qualifications", "qualifications", Model.Qualifications));

            
            #line default
            #line hidden
WriteLiteral("\r\n");

WriteLiteral("                ");

            
            #line 72 "..\..\Views\Apprenticeship\Standard.cshtml"
           Write(GetStandardProperty("Professional registration", "professional-registration", Model.ProfessionalRegistration, true));

            
            #line default
            #line hidden
WriteLiteral("\r\n            </dl>\r\n");

            
            #line 74 "..\..\Views\Apprenticeship\Standard.cshtml"
            
            
            #line default
            #line hidden
            
            #line 74 "..\..\Views\Apprenticeship\Standard.cshtml"
             if (!string.IsNullOrEmpty($"{Model.AssessmentPlanPdfUrlTitle}{Model.StandardPdfUrlTitle}"))
            {

            
            #line default
            #line hidden
WriteLiteral("                <div");

WriteLiteral(" class=\"panel-indent panel-indent-info\"");

WriteLiteral(">\r\n                    <h2");

WriteLiteral(" class=\"heading-large\"");

WriteLiteral(">\r\n                        Documents\r\n                    </h2>\r\n                " +
"    <ul>\r\n");

WriteLiteral("                        ");

            
            #line 81 "..\..\Views\Apprenticeship\Standard.cshtml"
                   Write(GetDocumentItem(@Model.StandardPdf, "Standard"));

            
            #line default
            #line hidden
WriteLiteral("\r\n");

WriteLiteral("                        ");

            
            #line 82 "..\..\Views\Apprenticeship\Standard.cshtml"
                   Write(GetDocumentItem(@Model.AssessmentPlanPdf, "Assessment Plan"));

            
            #line default
            #line hidden
WriteLiteral("\r\n                    </ul>\r\n                </div>\r\n");

            
            #line 85 "..\..\Views\Apprenticeship\Standard.cshtml"
            }

            
            #line default
            #line hidden
WriteLiteral("\r\n            <hr />\r\n\r\n");

            
            #line 89 "..\..\Views\Apprenticeship\Standard.cshtml"
            
            
            #line default
            #line hidden
            
            #line 89 "..\..\Views\Apprenticeship\Standard.cshtml"
             using (Html.BeginForm("StandardResults", "Provider", FormMethod.Get, new { @class = "search-box postcode-form-bottom" }))
            {

            
            #line default
            #line hidden
WriteLiteral("                <h2");

WriteLiteral(" class=\"heading-large\"");

WriteLiteral(">Find training providers</h2>\r\n");

WriteLiteral("                <div");

WriteAttribute("class", Tuple.Create(" class=\"", 4137), Tuple.Create("\"", 4190)
, Tuple.Create(Tuple.Create("", 4145), Tuple.Create("form-group", 4145), true)
            
            #line 92 "..\..\Views\Apprenticeship\Standard.cshtml"
, Tuple.Create(Tuple.Create(" ", 4155), Tuple.Create<System.Object, System.Int32>(@Model.HasError ? " error" : ""
            
            #line default
            #line hidden
, 4156), false)
);

WriteLiteral(">\r\n                    <label");

WriteLiteral(" class=\"form-label\"");

WriteLiteral(" for=\"postcode\"");

WriteLiteral(">\r\n                        Enter postcode");

WriteLiteral("\r\n                        <p>\r\n");

            
            #line 96 "..\..\Views\Apprenticeship\Standard.cshtml"
                            
            
            #line default
            #line hidden
            
            #line 96 "..\..\Views\Apprenticeship\Standard.cshtml"
                              
                                Html.RenderPartial("_BlankFieldErrorMessage");
                            
            
            #line default
            #line hidden
WriteLiteral("\r\n                        </p>\r\n                    </label>\r\n                   " +
" <input");

WriteLiteral(" type=\"hidden\"");

WriteLiteral(" id=\"apprenticeshipid2\"");

WriteLiteral(" name=\"apprenticeshipid\"");

WriteLiteral(" class=\"text-box form-control\"");

WriteAttribute("value", Tuple.Create(" value=\"", 4671), Tuple.Create("\"", 4696)
            
            #line 101 "..\..\Views\Apprenticeship\Standard.cshtml"
                                             , Tuple.Create(Tuple.Create("", 4679), Tuple.Create<System.Object, System.Int32>(Model.StandardId
            
            #line default
            #line hidden
, 4679), false)
);

WriteLiteral(">\r\n                    <input");

WriteLiteral(" type=\"search\"");

WriteLiteral(" id=\"postcode2\"");

WriteLiteral(" name=\"PostCode\"");

WriteLiteral(" class=\"text-box form-control postcode-search-box\"");

WriteLiteral(" maxlength=\"200\"");

WriteLiteral(" placeholder=\"\"");

WriteLiteral(">\r\n                </div>\r\n");

WriteLiteral("                <input");

WriteLiteral(" class=\"button margin-top-x2 postcode-search-button\"");

WriteLiteral(" id=\"submit-postcode2\"");

WriteLiteral(" type=\"submit\"");

WriteLiteral(" value=\"Search\"");

WriteLiteral(" />\r\n");

            
            #line 105 "..\..\Views\Apprenticeship\Standard.cshtml"
            }

            
            #line default
            #line hidden
WriteLiteral("        </section>\r\n\r\n</main>\r\n\r\n");

WriteLiteral("\r\n");

WriteLiteral("\r\n");

WriteLiteral("\r\n");

        }
    }
}
#pragma warning restore 1591