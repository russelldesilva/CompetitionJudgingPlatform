#pragma checksum "C:\Users\lolot\source\repos\web2021apr_p04_t2\WEB-ASG\Views\Admin\ViewAreaInterest.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "7b175fd065b45803f181eecc4962bc6af0207b6a"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Admin_ViewAreaInterest), @"mvc.1.0.view", @"/Views/Admin/ViewAreaInterest.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "C:\Users\lolot\source\repos\web2021apr_p04_t2\WEB-ASG\Views\_ViewImports.cshtml"
using WEB_ASG;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\lolot\source\repos\web2021apr_p04_t2\WEB-ASG\Views\_ViewImports.cshtml"
using WEB_ASG.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\lolot\source\repos\web2021apr_p04_t2\WEB-ASG\Views\Admin\ViewAreaInterest.cshtml"
using System.Diagnostics;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"7b175fd065b45803f181eecc4962bc6af0207b6a", @"/Views/Admin/ViewAreaInterest.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"625d7de4302b70a4946f0bd7ad85a9108931d931", @"/Views/_ViewImports.cshtml")]
    public class Views_Admin_ViewAreaInterest : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<WEB_ASG.Models.AreaInterest>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn-link"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "EditComp", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Index", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.LabelTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 4 "C:\Users\lolot\source\repos\web2021apr_p04_t2\WEB-ASG\Views\Admin\ViewAreaInterest.cshtml"
  
    ViewData["Title"] = "ViewAreaInterest";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<h1>Area of Interest:</h1>\r\n\r\n<div>\r\n    <h3 class=\"text-muted\">");
#nullable restore
#line 11 "C:\Users\lolot\source\repos\web2021apr_p04_t2\WEB-ASG\Views\Admin\ViewAreaInterest.cshtml"
                      Write(Html.DisplayFor(model => model.Name));

#line default
#line hidden
#nullable disable
            WriteLiteral("</h3>\r\n    <hr />\r\n    <dl class=\"row\">\r\n        <dt class=\"col-3\">\r\n            ");
#nullable restore
#line 15 "C:\Users\lolot\source\repos\web2021apr_p04_t2\WEB-ASG\Views\Admin\ViewAreaInterest.cshtml"
       Write(Html.DisplayNameFor(model => model.AreaInterestID));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dt>\r\n        <dd class=\"col-9\">\r\n            ");
#nullable restore
#line 18 "C:\Users\lolot\source\repos\web2021apr_p04_t2\WEB-ASG\Views\Admin\ViewAreaInterest.cshtml"
       Write(Html.DisplayTextFor(model => model.AreaInterestID));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dd>\r\n    </dl>\r\n    <dl class=\"row\">\r\n        <dt class=\"col-3\">\r\n            ");
#nullable restore
#line 23 "C:\Users\lolot\source\repos\web2021apr_p04_t2\WEB-ASG\Views\Admin\ViewAreaInterest.cshtml"
       Write(Html.DisplayNameFor(model => model.Name));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dt>\r\n        <dd class=\"col-9\">\r\n            ");
#nullable restore
#line 26 "C:\Users\lolot\source\repos\web2021apr_p04_t2\WEB-ASG\Views\Admin\ViewAreaInterest.cshtml"
       Write(Html.DisplayTextFor(model => model.Name));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dd>\r\n    </dl>\r\n    <h5 class=\"text-muted\">Competitions:</h5>\r\n");
#nullable restore
#line 30 "C:\Users\lolot\source\repos\web2021apr_p04_t2\WEB-ASG\Views\Admin\ViewAreaInterest.cshtml"
     if (Model.CompetitonList.Count > 0)
    {
        for (int i = 0; i < Model.CompetitonList.Count; i++)
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <dl class=\"row\">\r\n                <dt class=\"col-3\">\r\n                    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("label", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "7b175fd065b45803f181eecc4962bc6af0207b6a6895", async() => {
                WriteLiteral("\r\n                        ");
#nullable restore
#line 37 "C:\Users\lolot\source\repos\web2021apr_p04_t2\WEB-ASG\Views\Admin\ViewAreaInterest.cshtml"
                   Write(Model.CompetitonList[i].CompetitionID);

#line default
#line hidden
#nullable disable
                WriteLiteral("\r\n                    ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.LabelTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper);
#nullable restore
#line 36 "C:\Users\lolot\source\repos\web2021apr_p04_t2\WEB-ASG\Views\Admin\ViewAreaInterest.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper.For = ModelExpressionProvider.CreateModelExpression(ViewData, __model => __model.CompetitonList[i].CompetitionID);

#line default
#line hidden
#nullable disable
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-for", __Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper.For, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n                </dt>\r\n                <dd class=\"col-9\">\r\n                    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("label", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "7b175fd065b45803f181eecc4962bc6af0207b6a8809", async() => {
                WriteLiteral("\r\n                        ");
#nullable restore
#line 42 "C:\Users\lolot\source\repos\web2021apr_p04_t2\WEB-ASG\Views\Admin\ViewAreaInterest.cshtml"
                   Write(Model.CompetitonList[i].CompetitionName);

#line default
#line hidden
#nullable disable
                WriteLiteral(" | ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "7b175fd065b45803f181eecc4962bc6af0207b6a9369", async() => {
                    WriteLiteral("Edit");
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
                __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_1.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
                if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
                {
                    throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-compID", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
                }
                BeginWriteTagHelperAttribute();
#nullable restore
#line 42 "C:\Users\lolot\source\repos\web2021apr_p04_t2\WEB-ASG\Views\Admin\ViewAreaInterest.cshtml"
                                                                                                                   WriteLiteral(Model.CompetitonList[i].CompetitionID);

#line default
#line hidden
#nullable disable
                __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
                __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["compID"] = __tagHelperStringValueBuffer;
                __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-compID", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["compID"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n                    ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.LabelTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper);
#nullable restore
#line 41 "C:\Users\lolot\source\repos\web2021apr_p04_t2\WEB-ASG\Views\Admin\ViewAreaInterest.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper.For = ModelExpressionProvider.CreateModelExpression(ViewData, __model => __model.CompetitonList[i].CompetitionName);

#line default
#line hidden
#nullable disable
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-for", __Microsoft_AspNetCore_Mvc_TagHelpers_LabelTagHelper.For, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n                </dd>\r\n            </dl>\r\n");
#nullable restore
#line 46 "C:\Users\lolot\source\repos\web2021apr_p04_t2\WEB-ASG\Views\Admin\ViewAreaInterest.cshtml"
        }
    }
    else
    {

#line default
#line hidden
#nullable disable
            WriteLiteral("        <p class=\"text-secondary\">No competitions for this area of interest</p>\r\n");
#nullable restore
#line 51 "C:\Users\lolot\source\repos\web2021apr_p04_t2\WEB-ASG\Views\Admin\ViewAreaInterest.cshtml"
    }

#line default
#line hidden
#nullable disable
            WriteLiteral("</div>\r\n<div>\r\n    ");
#nullable restore
#line 54 "C:\Users\lolot\source\repos\web2021apr_p04_t2\WEB-ASG\Views\Admin\ViewAreaInterest.cshtml"
Write(Html.ActionLink("Delete Area of Interest", "CreateAreaInterests", new { /* id = Model.PrimaryKey */ }));

#line default
#line hidden
#nullable disable
            WriteLiteral(" |\r\n    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "7b175fd065b45803f181eecc4962bc6af0207b6a14027", async() => {
                WriteLiteral("Back to List");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n</div>\r\n");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<WEB_ASG.Models.AreaInterest> Html { get; private set; }
    }
}
#pragma warning restore 1591
