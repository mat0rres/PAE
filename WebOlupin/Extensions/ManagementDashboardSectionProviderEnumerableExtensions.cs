using MOROPO.Framework.Guama.Core.Presentation.Web.Backend.Models.View;
using System.Collections.Generic;

namespace MOROPO.PremierAutomotiveExport.WebOlupin.Extensions
{
    public static class ManagementDashboardSectionProviderEnumerableExtensions
    {
        public static Dictionary<string, ICollection<ManagementDashboardSectionViewModel>> ToManagementDashboardSectionGroups(this IEnumerable<Framework.Guama.Core.Presentation.Web.Backend.Providers.Facade.IManagementDashboardSectionProviderFacade> self)
        {
            var securitySections = new List<ManagementDashboardSectionViewModel>();

            foreach (var provider in self)
            {
                if (provider is Framework.Guama.Security.Personal.Presentation.Web.Backend.Providers.ManagementDashboardSectionProvider)
                {
                    securitySections.AddRange(provider.GetManagementDashboardSections());
                }
            }

            securitySections.Add(new ManagementDashboardSectionViewModel
            {
                Title = "Changes",
                Controller = "traces",
                Action = "index",
                Area = "security",
                Role = Constants.Authorization.TraceViewerPermission
            });

            var siteContentsSection = new List<ManagementDashboardSectionViewModel>
            {
                new ManagementDashboardSectionViewModel
                {
                    Title = "Page Headers",
                    Controller = "pageheaders",
                    Action= "index",
                    Area = "site",
                    Role = Constants.Authorization.PageHeaderViewerPermission
                },
                new ManagementDashboardSectionViewModel
                {
                    Title = "Text Blocks",
                    Controller = "richtextblocks",
                    Action= "index",
                    Area = "site",
                    Role = Constants.Authorization.RichTextBlockViewerPermission
                },
                new ManagementDashboardSectionViewModel
                {
                    Title = "Instantáneas",
                    Controller = "showcaseitems",
                    Action = "index",
                    Area = "site",
                    Role = Constants.Authorization.ShowcaseItemViewerPermission
                },
                new ManagementDashboardSectionViewModel
                {
                    Title = "Promotions",
                    Controller = "brandpromotions",
                    Action = "index",
                    Area = "site",
                    Role = Constants.Authorization.BrandPromotionViewerPermission
                },
                new ManagementDashboardSectionViewModel
                {
                    Title = "Support Offers",
                    Controller = "brandsupportoffers",
                    Action = "index",
                    Area = "site",
                    Role = Constants.Authorization.BrandSupportOfferViewerPermission
                },
                new ManagementDashboardSectionViewModel
                {
                    Title = "News",
                    Controller = "posts",
                    Action = "index",
                    Area = "site",
                    Role = Constants.Authorization.PostViewerPermission
                },
                new ManagementDashboardSectionViewModel
                {
                    Title = "Representatives",
                    Controller = "representatives",
                    Action = "index",
                    Area = "site",
                    Role = Constants.Authorization.RepresentativeViewerPermission
                }
            };

            var catalogSections = new List<ManagementDashboardSectionViewModel>
            {
                new ManagementDashboardSectionViewModel
                {
                    Title = "Services",
                    Controller = "services",
                    Action= "index",
                    Area = "catalog",
                    Role = Constants.Authorization.ServiceViewerPermission
                },
                new ManagementDashboardSectionViewModel
                {
                    Title = "Products",
                    Controller = "products",
                    Action= "index",
                    Area = "catalog",
                    Role = Constants.Authorization.ProductViewerPermission
                },
                new ManagementDashboardSectionViewModel
                {
                    Title = "Brands",
                    Controller = "brands",
                    Action= "index",
                    Area = "catalog",
                    Role = Constants.Authorization.BrandViewerPermission
                },
                new ManagementDashboardSectionViewModel
                {
                    Title = "Categories",
                    Controller = "categories",
                    Action= "index",
                    Area = "catalog",
                    Role = Constants.Authorization.CategoryViewerPermission
                },
                new ManagementDashboardSectionViewModel
                {
                    Title = "Custom Attributes",
                    Controller = "customattributes",
                    Action= "index",
                    Area = "catalog",
                    Role = Constants.Authorization.CustomAttributeViewerPermission
                }
            };

            return new Dictionary<string, ICollection<ManagementDashboardSectionViewModel>>
            {
                { "Security", securitySections },
                { "Site Contents", siteContentsSection },
                { "PAE Catalog", catalogSections }
            };
        }
    }
}
