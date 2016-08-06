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
    
    #line 1 "..\..\Views\Apprenticeship\Framework.cshtml"
    using Sfa.Das.Sas.Resources;
    
    #line default
    #line hidden
    using Sfa.Das.Sas.Web;
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/Apprenticeship/Framework.cshtml")]
    public partial class Framework : System.Web.Mvc.WebViewPage<Sfa.Das.Sas.Web.ViewModels.FrameworkViewModel>
    {
        public Framework()
        {
        }
        public override void Execute()
        {
            
            #line 4 "..\..\Views\Apprenticeship\Framework.cshtml"
  
    ViewBag.Title = @Model.Title + " - Apprenticeship Framework";   

            
            #line default
            #line hidden
WriteLiteral("\r\n\r\n<main");

WriteLiteral(" id=\"content\"");

WriteLiteral(">\r\n    <div");

WriteLiteral(" class=\"grid-row\"");

WriteLiteral(">\r\n        <div");

WriteLiteral(" class=\"column-two-thirds\"");

WriteLiteral(">\r\n                <h1");

WriteLiteral(" class=\"heading-xlarge\"");

WriteLiteral(">\r\n");

WriteLiteral("                    ");

            
            #line 12 "..\..\Views\Apprenticeship\Framework.cshtml"
               Write(Model.Title);

            
            #line default
            #line hidden
WriteLiteral("\r\n                </h1>\r\n                <p>\r\n                    What qualificat" +
"ions an apprentice can achieve as part of this apprenticeship.\r\n                " +
"</p>\r\n");

            
            #line 17 "..\..\Views\Apprenticeship\Framework.cshtml"
                
            
            #line default
            #line hidden
            
            #line 17 "..\..\Views\Apprenticeship\Framework.cshtml"
                 if (Model.ExpiryDateString != null)
                {

            
            #line default
            #line hidden
WriteLiteral("                    <div");

WriteLiteral(" class=\"expire-date\"");

WriteLiteral(">\r\n                        <div>\r\n                            <i");

WriteLiteral(" class=\"fa fa-exclamation-circle\"");

WriteLiteral(" aria-hidden=\"true\"");

WriteLiteral("></i>\r\n                        </div>\r\n                        <span>\r\n          " +
"                  This apprenticeship is closed to new starters from ");

            
            #line 24 "..\..\Views\Apprenticeship\Framework.cshtml"
                                                                          Write(Model.ExpiryDateString);

            
            #line default
            #line hidden
WriteLiteral("\r\n                        </span>\r\n                    </div>\r\n");

            
            #line 27 "..\..\Views\Apprenticeship\Framework.cshtml"
                }

            
            #line default
            #line hidden
WriteLiteral("        </div>\r\n\r\n        <div");

WriteLiteral(" class=\"column-third\"");

WriteLiteral(">\r\n            <div");

WriteLiteral(" class=\"related-container\"");

WriteLiteral(">\r\n                <aside");

WriteLiteral(" class=\"hidden-for-mobile-tablet\"");

WriteLiteral(">\r\n\r\n                    <a");

WriteLiteral(" class=\"button ui-find-training-providers\"");

WriteAttribute("href", Tuple.Create(" href=\"", 1210), Tuple.Create("\"", 1345)
            
            #line 34 "..\..\Views\Apprenticeship\Framework.cshtml"
, Tuple.Create(Tuple.Create("", 1217), Tuple.Create<System.Object, System.Int32>(Url.Action("SearchForFrameworkProviders", "Apprenticeship", new {frameworkId = Model.FrameworkId, keywords = Model.SearchTerm})
            
            #line default
            #line hidden
, 1217), false)
);

WriteLiteral(">\r\n                        Find training providers\r\n                    </a>\r\n   " +
"             </aside>\r\n            </div>\r\n\r\n        </div>\r\n    </div>\r\n\r\n    <" +
"section>\r\n        <header>\r\n            <h2");

WriteLiteral(" class=\"heading-large\"");

WriteLiteral(">\r\n                Summary of this apprenticeship framework\r\n            </h2>\r\n " +
"       </header>\r\n\r\n        <dl");

WriteLiteral(" class=\"data-list\"");

WriteLiteral(">\r\n");

            
            #line 51 "..\..\Views\Apprenticeship\Framework.cshtml"
            
            
            #line default
            #line hidden
            
            #line 51 "..\..\Views\Apprenticeship\Framework.cshtml"
             if (!string.IsNullOrEmpty(Model.FrameworkOverview))
            {

            
            #line default
            #line hidden
WriteLiteral("                <dt");

WriteLiteral(" class=\"overviewTitle\"");

WriteLiteral(">Overview of role</dt>\r\n");

WriteLiteral("                <dd");

WriteLiteral(" class=\"overviewText\"");

WriteLiteral(">");

            
            #line 54 "..\..\Views\Apprenticeship\Framework.cshtml"
                                    Write(Model.FrameworkOverview);

            
            #line default
            #line hidden
WriteLiteral(" </dd>\r\n");

            
            #line 55 "..\..\Views\Apprenticeship\Framework.cshtml"
            }

            
            #line default
            #line hidden
WriteLiteral("            <dt>Level</dt>\r\n            <dd>\r\n");

WriteLiteral("                ");

            
            #line 58 "..\..\Views\Apprenticeship\Framework.cshtml"
           Write(Model.Level);

            
            #line default
            #line hidden
WriteLiteral(" (equivalent to ");

            
            #line 58 "..\..\Views\Apprenticeship\Framework.cshtml"
                                       Write(EquivalenveLevelService.GetApprenticeshipLevel(Model.Level.ToString()));

            
            #line default
            #line hidden
WriteLiteral(" )\r\n            </dd>\r\n");

            
            #line 60 "..\..\Views\Apprenticeship\Framework.cshtml"
            
            
            #line default
            #line hidden
            
            #line 60 "..\..\Views\Apprenticeship\Framework.cshtml"
             if (!string.IsNullOrEmpty(Model.TypicalLengthMessage))
            {

            
            #line default
            #line hidden
WriteLiteral("                <dt>Typical length</dt>\r\n");

WriteLiteral("                <dd>");

            
            #line 63 "..\..\Views\Apprenticeship\Framework.cshtml"
               Write(Model.TypicalLengthMessage);

            
            #line default
            #line hidden
WriteLiteral(" </dd>\r\n");

            
            #line 64 "..\..\Views\Apprenticeship\Framework.cshtml"
            }

            
            #line default
            #line hidden
WriteLiteral("            <dt>Entry requirements</dt>\r\n            <dd");

WriteLiteral(" class=\"entry-requirements\"");

WriteLiteral(">\r\n");

            
            #line 67 "..\..\Views\Apprenticeship\Framework.cshtml"
                
            
            #line default
            #line hidden
            
            #line 67 "..\..\Views\Apprenticeship\Framework.cshtml"
                 if (!string.IsNullOrEmpty(@Model.EntryRequirements))
                {

            
            #line default
            #line hidden
WriteLiteral("                    <div>");

            
            #line 69 "..\..\Views\Apprenticeship\Framework.cshtml"
                    Write(Model.EntryRequirements);

            
            #line default
            #line hidden
WriteLiteral("</div>\r\n");

            
            #line 70 "..\..\Views\Apprenticeship\Framework.cshtml"
                }

            
            #line default
            #line hidden
WriteLiteral("                <div>Your chosen training provider can advise you about entry req" +
"uirements for apprentices.</div>\r\n            </dd>\r\n\r\n            <dt>Suitable " +
"roles for apprentices</dt>\r\n            <dd");

WriteLiteral(" class=\"job-roles\"");

WriteLiteral(">\r\n");

            
            #line 76 "..\..\Views\Apprenticeship\Framework.cshtml"
                
            
            #line default
            #line hidden
            
            #line 76 "..\..\Views\Apprenticeship\Framework.cshtml"
                 if (Model.JobRoles != null && Model.JobRoles.Any())
                {

            
            #line default
            #line hidden
WriteLiteral("                    <ul>\r\n");

            
            #line 79 "..\..\Views\Apprenticeship\Framework.cshtml"
                        
            
            #line default
            #line hidden
            
            #line 79 "..\..\Views\Apprenticeship\Framework.cshtml"
                         foreach (var jobRole in Model.JobRoles.Take(5))
                        {

            
            #line default
            #line hidden
WriteLiteral("                            <li>");

            
            #line 81 "..\..\Views\Apprenticeship\Framework.cshtml"
                           Write(jobRole);

            
            #line default
            #line hidden
WriteLiteral("</li>\r\n");

            
            #line 82 "..\..\Views\Apprenticeship\Framework.cshtml"
                        }

            
            #line default
            #line hidden
WriteLiteral("                        ");

            
            #line 83 "..\..\Views\Apprenticeship\Framework.cshtml"
                         if (Model.JobRoles.Count() > 5)
                        {
                            foreach (var jobRole in Model.JobRoles.Skip(5))
                            {

            
            #line default
            #line hidden
WriteLiteral("                                <li");

WriteLiteral(" class=\"default-hidden\"");

WriteLiteral(">");

            
            #line 87 "..\..\Views\Apprenticeship\Framework.cshtml"
                                                      Write(jobRole);

            
            #line default
            #line hidden
WriteLiteral("</li>\r\n");

            
            #line 88 "..\..\Views\Apprenticeship\Framework.cshtml"
                            }

            
            #line default
            #line hidden
WriteLiteral("                            <a");

WriteLiteral(" href=\"#\"");

WriteLiteral(" class=\"showmore hidden\"");

WriteLiteral(">Show more</a>\r\n");

            
            #line 90 "..\..\Views\Apprenticeship\Framework.cshtml"
                        }

            
            #line default
            #line hidden
WriteLiteral("                    </ul>\r\n");

            
            #line 92 "..\..\Views\Apprenticeship\Framework.cshtml"
                }

            
            #line default
            #line hidden
WriteLiteral(@"                    
                <div>
                    Your chosen training provider can advise you about the kinds of skills apprentices will learn.    
                </div>
            </dd>
            <dt>Qualifications</dt>
            <dd>
                <div");

WriteLiteral(" class=\"qualifications\"");

WriteLiteral(">\r\n");

            
            #line 101 "..\..\Views\Apprenticeship\Framework.cshtml"
                    
            
            #line default
            #line hidden
            
            #line 101 "..\..\Views\Apprenticeship\Framework.cshtml"
                     if (Model.CompetencyQualification!= null && Model.CompetencyQualification.Any())
                    {

            
            #line default
            #line hidden
WriteLiteral("                        <div");

WriteLiteral(" class=\"competencyQualifications\"");

WriteLiteral(">\r\n                            Apprentices will achieve a practical (or \'competen" +
"ce\') qualification:\r\n                            <br />\r\n                       " +
"     <ul>\r\n");

            
            #line 107 "..\..\Views\Apprenticeship\Framework.cshtml"
                                
            
            #line default
            #line hidden
            
            #line 107 "..\..\Views\Apprenticeship\Framework.cshtml"
                                 foreach (var qualification in Model.CompetencyQualification.Take(3))
                                {

            
            #line default
            #line hidden
WriteLiteral("                                    <li>");

            
            #line 109 "..\..\Views\Apprenticeship\Framework.cshtml"
                                   Write(qualification);

            
            #line default
            #line hidden
WriteLiteral("</li>\r\n");

            
            #line 110 "..\..\Views\Apprenticeship\Framework.cshtml"
                                }

            
            #line default
            #line hidden
WriteLiteral("                                \r\n");

            
            #line 112 "..\..\Views\Apprenticeship\Framework.cshtml"
                                
            
            #line default
            #line hidden
            
            #line 112 "..\..\Views\Apprenticeship\Framework.cshtml"
                                 if (Model.CompetencyQualification.Count() > 3)
                                {
                                    foreach (var qualification in Model.CompetencyQualification.Skip(3))
                                    {

            
            #line default
            #line hidden
WriteLiteral("                                        <li");

WriteLiteral(" class=\"default-hidden\"");

WriteLiteral(">");

            
            #line 116 "..\..\Views\Apprenticeship\Framework.cshtml"
                                                              Write(qualification);

            
            #line default
            #line hidden
WriteLiteral("</li>\r\n");

            
            #line 117 "..\..\Views\Apprenticeship\Framework.cshtml"
                                    }

            
            #line default
            #line hidden
WriteLiteral("                                    <a");

WriteLiteral(" href=\"#\"");

WriteLiteral(" class=\"showmore hidden\"");

WriteLiteral(">Show more</a>\r\n");

            
            #line 119 "..\..\Views\Apprenticeship\Framework.cshtml"
                                }

            
            #line default
            #line hidden
WriteLiteral("                            </ul>\r\n                        </div>\r\n");

            
            #line 122 "..\..\Views\Apprenticeship\Framework.cshtml"
                    }

            
            #line default
            #line hidden
WriteLiteral("\r\n");

            
            #line 124 "..\..\Views\Apprenticeship\Framework.cshtml"
                    
            
            #line default
            #line hidden
            
            #line 124 "..\..\Views\Apprenticeship\Framework.cshtml"
                     if (Model.KnowledgeQualification!= null && Model.KnowledgeQualification.Any())
                    {

            
            #line default
            #line hidden
WriteLiteral("                        <div");

WriteLiteral(" class=\"knowledgeQualifications\"");

WriteLiteral(">\r\n                            Apprentices will also achieve a theory-based (or \'" +
"knowledge\') qualification:\r\n                            <br />\r\n                " +
"            <ul>\r\n");

            
            #line 130 "..\..\Views\Apprenticeship\Framework.cshtml"
                                
            
            #line default
            #line hidden
            
            #line 130 "..\..\Views\Apprenticeship\Framework.cshtml"
                                 foreach (var qualification in Model.KnowledgeQualification.Take(3))
                                {

            
            #line default
            #line hidden
WriteLiteral("                                    <li>");

            
            #line 132 "..\..\Views\Apprenticeship\Framework.cshtml"
                                   Write(qualification);

            
            #line default
            #line hidden
WriteLiteral("</li>\r\n");

            
            #line 133 "..\..\Views\Apprenticeship\Framework.cshtml"
                                }

            
            #line default
            #line hidden
WriteLiteral("                                \r\n");

            
            #line 135 "..\..\Views\Apprenticeship\Framework.cshtml"
                                
            
            #line default
            #line hidden
            
            #line 135 "..\..\Views\Apprenticeship\Framework.cshtml"
                                 if (Model.KnowledgeQualification.Count() > 3)
                                {
                                    foreach (var qualification in Model.KnowledgeQualification.Skip(3))
                                    {

            
            #line default
            #line hidden
WriteLiteral("                                        <li");

WriteLiteral(" class=\"default-hidden\"");

WriteLiteral(">");

            
            #line 139 "..\..\Views\Apprenticeship\Framework.cshtml"
                                                              Write(qualification);

            
            #line default
            #line hidden
WriteLiteral("</li>\r\n");

            
            #line 140 "..\..\Views\Apprenticeship\Framework.cshtml"
                                    }

            
            #line default
            #line hidden
WriteLiteral("                                    <a");

WriteLiteral(" href=\"#\"");

WriteLiteral(" class=\"showmore hidden\"");

WriteLiteral(">Show more</a>\r\n");

            
            #line 142 "..\..\Views\Apprenticeship\Framework.cshtml"
                                }

            
            #line default
            #line hidden
WriteLiteral("                            </ul>\r\n                        </div>\r\n");

            
            #line 145 "..\..\Views\Apprenticeship\Framework.cshtml"
                    }

            
            #line default
            #line hidden
WriteLiteral("\r\n");

            
            #line 147 "..\..\Views\Apprenticeship\Framework.cshtml"
                    
            
            #line default
            #line hidden
            
            #line 147 "..\..\Views\Apprenticeship\Framework.cshtml"
                     if (Model.CombinedQualificiation != null && Model.CombinedQualificiation.Any())
                    {

            
            #line default
            #line hidden
WriteLiteral("                        <div");

WriteLiteral(" class=\"combinedQualification\"");

WriteLiteral(">\r\n                            Apprentices will achieve a practical and theory-ba" +
"sed (or \'combined\') qualification:\r\n                            <br />\r\n        " +
"                    <ul>\r\n");

            
            #line 153 "..\..\Views\Apprenticeship\Framework.cshtml"
                                
            
            #line default
            #line hidden
            
            #line 153 "..\..\Views\Apprenticeship\Framework.cshtml"
                                 foreach (var qualification in Model.CombinedQualificiation.Take(3))
                                {

            
            #line default
            #line hidden
WriteLiteral("                                    <li>");

            
            #line 155 "..\..\Views\Apprenticeship\Framework.cshtml"
                                   Write(qualification);

            
            #line default
            #line hidden
WriteLiteral("</li>\r\n");

            
            #line 156 "..\..\Views\Apprenticeship\Framework.cshtml"
                                }

            
            #line default
            #line hidden
WriteLiteral("                                \r\n");

            
            #line 158 "..\..\Views\Apprenticeship\Framework.cshtml"
                                
            
            #line default
            #line hidden
            
            #line 158 "..\..\Views\Apprenticeship\Framework.cshtml"
                                 if (Model.CombinedQualificiation.Count() > 3)
                                {
                                    foreach (var qualification in Model.CombinedQualificiation.Skip(3))
                                    {

            
            #line default
            #line hidden
WriteLiteral("                                        <li");

WriteLiteral(" class=\"default-hidden\"");

WriteLiteral(">");

            
            #line 162 "..\..\Views\Apprenticeship\Framework.cshtml"
                                                              Write(qualification);

            
            #line default
            #line hidden
WriteLiteral("</li>\r\n");

            
            #line 163 "..\..\Views\Apprenticeship\Framework.cshtml"
                                    }

            
            #line default
            #line hidden
WriteLiteral("                                    <a");

WriteLiteral(" href=\"#\"");

WriteLiteral(" class=\"showmore hidden\"");

WriteLiteral(">Show more</a>\r\n");

            
            #line 165 "..\..\Views\Apprenticeship\Framework.cshtml"
                                }

            
            #line default
            #line hidden
WriteLiteral("                            </ul>\r\n                        </div>\r\n");

            
            #line 168 "..\..\Views\Apprenticeship\Framework.cshtml"
                    }

            
            #line default
            #line hidden
WriteLiteral("                    </div>\r\n\r\n");

            
            #line 171 "..\..\Views\Apprenticeship\Framework.cshtml"
                    
            
            #line default
            #line hidden
            
            #line 171 "..\..\Views\Apprenticeship\Framework.cshtml"
                     if (Model.CompletionQualifications != string.Empty)
                    {

            
            #line default
            #line hidden
WriteLiteral("                        <p");

WriteLiteral(" class=\"completionQualifications\"");

WriteLiteral(">");

            
            #line 173 "..\..\Views\Apprenticeship\Framework.cshtml"
                                                       Write(Model.CompletionQualifications);

            
            #line default
            #line hidden
WriteLiteral("</p>\r\n");

            
            #line 174 "..\..\Views\Apprenticeship\Framework.cshtml"
                    }

            
            #line default
            #line hidden
WriteLiteral("                    <p");

WriteLiteral(" class=\"qualificationsStatic\"");

WriteLiteral(">Your chosen training provider can advise you about the qualifications open to ap" +
"prentices as they can change depending on individual and other circumstances.</p" +
">\r\n                </dd>\r\n\r\n            <dt>Professional registration</dt>\r\n    " +
"        <dd>");

            
            #line 179 "..\..\Views\Apprenticeship\Framework.cshtml"
           Write(Model.ProfessionalRegistration);

            
            #line default
            #line hidden
WriteLiteral("</dd>\r\n        </dl>\r\n\r\n        <div");

WriteLiteral(" id=\"bottom-actions\"");

WriteLiteral(">\r\n                <a");

WriteLiteral(" class=\"button ui-find-training-providers\"");

WriteLiteral(" id=\"ga-find-provider-bottom\"");

WriteAttribute("href", Tuple.Create(" href=\"", 8268), Tuple.Create("\"", 8405)
            
            #line 183 "..\..\Views\Apprenticeship\Framework.cshtml"
                , Tuple.Create(Tuple.Create("", 8275), Tuple.Create<System.Object, System.Int32>(Url.Action("SearchForFrameworkProviders", "Apprenticeship", new { frameworkId = Model.FrameworkId, keywords = Model.SearchTerm })
            
            #line default
            #line hidden
, 8275), false)
);

WriteLiteral(">\r\n                    Find training providers\r\n                </a>\r\n        </d" +
"iv>\r\n    </section>\r\n</main>");

        }
    }
}
#pragma warning restore 1591
