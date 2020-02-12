using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebApplication7.ViewModels;

using System.Text.Encodings.Web;
namespace WebApplication7.HtmlHelper
{
    public static class PagingHelpers
    {
        public static HtmlString PageLinks(this IHtmlHelper html, PagingInfo paginginfo,
                    Func<int, string> pageUrl)
        {

            var writer = new System.IO.StringWriter();
            //if(paginginfo.CurrentPage-1 ==0)
            //{
            //    TagBuilder tag = new TagBuilder("a");
            //    tag.MergeAttribute("href", pageUrl(paginginfo.CurrentPage - 1));
            //    tag.AddCssClass("disabled");
            //    tag.WriteTo(writer, HtmlEncoder.Default);
            //}
            //else 
            //{
            //    TagBuilder tag = new TagBuilder("a");
            //    tag.MergeAttribute("href", pageUrl(paginginfo.CurrentPage - 1));
            //    tag.WriteTo(writer, HtmlEncoder.Default);
            //}

            //TagBuilder tag1 = new TagBuilder("a");
            //tag1.MergeAttribute("href", pageUrl(paginginfo.CurrentPage));
            //tag1.AddCssClass("selected");
            //tag1.WriteTo(writer, HtmlEncoder.Default);
            //if(paginginfo.TotalPages > paginginfo.CurrentPage+1)
            //{
            //    TagBuilder tag2 = new TagBuilder("a");
            //    tag2.MergeAttribute("href", pageUrl(paginginfo.CurrentPage - 1));
            //    tag2.AddCssClass("disabled");
            //    tag2.WriteTo(writer, HtmlEncoder.Default);
            //}
            //else
            //{
            //    TagBuilder tag2 = new TagBuilder("a");
            //    tag2.MergeAttribute("href", pageUrl(paginginfo.CurrentPage - 1));
            //    tag2.WriteTo(writer, HtmlEncoder.Default);
            //}
            //TagBuilder tag2 = new TagBuilder("a");
            //tag2.MergeAttribute("href", pageUrl(paginginfo.CurrentPage + 1));
            //tag2.WriteTo(writer, HtmlEncoder.Default);

            TagBuilder tag1 = new TagBuilder("a");
            tag1.MergeAttribute("href", pageUrl(1));
            tag1.InnerHtml.Append("1");
            tag1.WriteTo(writer, HtmlEncoder.Default);
            if (paginginfo.CurrentPage - 1 > 1)
            {
                TagBuilder tag2 = new TagBuilder("a");
                tag2.MergeAttribute("href", pageUrl((int)paginginfo.CurrentPage - 1));
                tag2.InnerHtml.Append((paginginfo.CurrentPage - 1).ToString());
                tag2.WriteTo(writer, HtmlEncoder.Default);
            }
            if (paginginfo.CurrentPage != 1)
            {
                TagBuilder tag3 = new TagBuilder("a");
                tag3.MergeAttribute("href", pageUrl((int)paginginfo.CurrentPage));
                tag3.InnerHtml.Append((paginginfo.CurrentPage).ToString());
                tag3.AddCssClass("selected");
                tag3.WriteTo(writer, HtmlEncoder.Default);
            }
            if (paginginfo.CurrentPage + 1 >= paginginfo.TotalPages)
            { }
            else
            {
                TagBuilder tag4 = new TagBuilder("a");
                tag4.MergeAttribute("href", pageUrl((int)paginginfo.CurrentPage + 1));
                tag4.InnerHtml.Append((paginginfo.CurrentPage + 1).ToString());
                tag4.WriteTo(writer, HtmlEncoder.Default);
            }
            if (paginginfo.CurrentPage != paginginfo.TotalPages)
            {
                TagBuilder tag5 = new TagBuilder("a");
                tag5.MergeAttribute("href", pageUrl(paginginfo.TotalPages));
                tag5.InnerHtml.Append((paginginfo.TotalPages).ToString());
                tag5.WriteTo(writer, HtmlEncoder.Default);
            }
            //for (int i = 1; i <= paginginfo.TotalPages; i++)
            //{
            //    TagBuilder tag = new TagBuilder("a"); // Construct an <a> tag
            //    tag.MergeAttribute("href", pageUrl(i));
            //    tag.InnerHtml.Append(i.ToString());
            //    if (i == paginginfo.CurrentPage)
            //    {
            //        tag.AddCssClass("selected");
            //    }
            //    tag.WriteTo(writer, HtmlEncoder.Default);
            //}

            return new HtmlString(writer.ToString());
        }
    }
}