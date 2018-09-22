namespace FAS.Web.Infrastructure.TagHelpers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Razor.TagHelpers;

    public class ImageTagHelper : TagHelper
    {
        public string Source { get; set; }    

        public string AlternativeText { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "img";
            output.TagMode = TagMode.SelfClosing;
            output.Attributes.SetAttribute("src", this.Source);

            if(!string.IsNullOrWhiteSpace(this.AlternativeText))
            {
                output.Attributes.SetAttribute("alt", this.AlternativeText);
            }
            base.Process(context, output);
        }
    }
}
