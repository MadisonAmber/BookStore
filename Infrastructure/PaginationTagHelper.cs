using BookStore.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Infrastructure
{
    [HtmlTargetElement("div", Attributes = "page-model")]
    public class PaginationTagHelper : TagHelper
    {
        private IUrlHelperFactory uhf;

        public PaginationTagHelper(IUrlHelperFactory temp)
        {
            uhf = temp;
        }

        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext vc { get; set; }

        public PageInfo PageModel { get; set; }
        public string PageAction { get; set; }

        public override void Process(TagHelperContext thc, TagHelperOutput tho)
        {
            IUrlHelper uh = uhf.GetUrlHelper(vc);

            TagBuilder final = new TagBuilder("div");

            TagBuilder ul = new TagBuilder("ul");
            ul.Attributes["class"] = "pagination justify-content-center";

            if (PageModel.CurrentPage != 1)
            {
                ul.InnerHtml.AppendHtml(createBootstrapStyledListItem(PageModel.CurrentPage - 1, "Previous"));
            }

            for (int i = 1; i <= PageModel.TotalPages; i++)
            {
                ul.InnerHtml.AppendHtml(createBootstrapStyledListItem(i, i.ToString()));
            }

            if (PageModel.CurrentPage != PageModel.TotalPages)
            {
                ul.InnerHtml.AppendHtml(createBootstrapStyledListItem(PageModel.CurrentPage + 1, "Next"));
            }

            final.InnerHtml.AppendHtml(ul);

            tho.Content.AppendHtml(final.InnerHtml);
        }

        private TagBuilder createBootstrapStyledListItem(int currentPageNum, string pageText)
        {
            IUrlHelper uh = uhf.GetUrlHelper(vc);

            TagBuilder tb = new TagBuilder("a");

            tb.Attributes["href"] = uh.Action(PageAction, new { pageNum = currentPageNum });
            tb.Attributes["class"] = "page-link";

            tb.InnerHtml.Append(pageText);

            TagBuilder listItem = new TagBuilder("li");
            listItem.Attributes["class"] = "page-item";
            if (PageModel.CurrentPage == currentPageNum)
            {
                listItem.Attributes["class"] += " active";
            }
            listItem.InnerHtml.AppendHtml(tb);

            return listItem;
        }
    }
}
