namespace MOROPO.PremierAutomotiveExport.WebOlupin.Extensions
{
    public static class ManagementDashboardSectionExtensions
    {
        public static Microsoft.AspNetCore.Html.HtmlString ToIconographyHtmlString(this Framework.Guama.Core.Presentation.Web.Backend.Models.View.ManagementDashboardSectionViewModel target)
        {
            if (target.Controller == "users") return new Microsoft.AspNetCore.Html.HtmlString("fas fa-fw fa-user");

            if (target.Controller == "roles") return new Microsoft.AspNetCore.Html.HtmlString("fas fa-fw fa-users");

            if (target.Controller == "traces") return new Microsoft.AspNetCore.Html.HtmlString("fas fa-fw fa-book");

            if (target.Controller == "richtextblocks") return new Microsoft.AspNetCore.Html.HtmlString("fas fa-fw fa-align-justify");

            if (target.Controller == "pageheaders") return new Microsoft.AspNetCore.Html.HtmlString("fas fa-fw fa-image");

            if (target.Controller == "showcaseitems") return new Microsoft.AspNetCore.Html.HtmlString("fas fa-fw fa-file-image");

            if (target.Controller == "brandpromotions") return new Microsoft.AspNetCore.Html.HtmlString("fas fa-fw fa-flag");

            if (target.Controller == "brandsupportoffers") return new Microsoft.AspNetCore.Html.HtmlString("fas fa-fw fa-cubes");

            if (target.Controller == "posts") return new Microsoft.AspNetCore.Html.HtmlString("fas fa-fw fa-newspaper");

            if (target.Controller == "services") return new Microsoft.AspNetCore.Html.HtmlString("fas fa-fw fa-handshake");

            if (target.Controller == "products") return new Microsoft.AspNetCore.Html.HtmlString("fas fa-fw fa-motorcycle");

            if (target.Controller == "categories") return new Microsoft.AspNetCore.Html.HtmlString("fas fa-fw fa-tags");

            if (target.Controller == "brands") return new Microsoft.AspNetCore.Html.HtmlString("fas fa-fw fa-certificate");

            if (target.Controller == "representatives") return new Microsoft.AspNetCore.Html.HtmlString("fas fa-fw fa-id-badge");

            return new Microsoft.AspNetCore.Html.HtmlString("fas fa-fw fa-cog");
        }
    }
}
