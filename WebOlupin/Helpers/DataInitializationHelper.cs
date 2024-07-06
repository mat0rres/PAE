using System.Linq;
using MOROPO.Framework.Guama.Core.Security.Common.Persistence.ORM.EF.Extensions;
using MOROPO.PremierAutomotiveExport.Persistence.Domain.Entities;
using MOROPO.PremierAutomotiveExport.Persistence.Domain.Enumerations;

namespace MOROPO.PremierAutomotiveExport.WebOlupin.Helpers
{
    public class DataInitializationHelper : Framework.Guama.Core.Persistence.ORM.EF.Helpers.Facade.IDataInitializationHelper
    {
        public void InitialiseData(Microsoft.EntityFrameworkCore.DbContext dbContext)
        {
            var richTextBlockDbSet = dbContext.Set<RichTextBlock>();

            if (!richTextBlockDbSet.Any(entity => entity.Id == RichTextBlockId.About))
            {
                richTextBlockDbSet.Add(new RichTextBlock { Id = RichTextBlockId.About, Content = "Gubergren no accumsan illum consetetur hendrerit gubergren consequat voluptua facilisis invidunt ut justo diam duis soluta enim nonumy duis amet" });
            }

            if (!richTextBlockDbSet.Any(entity => entity.Id == RichTextBlockId.Contact))
            {
                richTextBlockDbSet.Add(new RichTextBlock { Id = RichTextBlockId.Contact, Content = "Gubergren no accumsan illum consetetur hendrerit gubergren consequat voluptua facilisis invidunt ut justo diam duis soluta enim nonumy duis amet" });
            }

            var permissionDbSet = dbContext.Set<Framework.Guama.Core.Security.Common.Persistence.Entities.Permission>();

            if (!permissionDbSet.Any(role => role.Code.Equals(Constants.Authorization.TraceViewerPermission)))
            {
                permissionDbSet.Add(Constants.Authorization.TraceViewerPermission.ToPermissionEntity());
            }

            #region BrandPromotion

            if (!permissionDbSet.Any(item => item.Code.Equals(Constants.Authorization.BrandPromotionViewerPermission)))
            {
                permissionDbSet.Add(Constants.Authorization.BrandPromotionViewerPermission.ToPermissionEntity(Constants.Authorization.BrandPromotionManagementPermissionGroup));
            }
            if (!permissionDbSet.Any(item => item.Code.Equals(Constants.Authorization.BrandPromotionCreatorPermission)))
            {
                permissionDbSet.Add(Constants.Authorization.BrandPromotionCreatorPermission.ToPermissionEntity(Constants.Authorization.BrandPromotionManagementPermissionGroup));
            }
            if (!permissionDbSet.Any(item => item.Code.Equals(Constants.Authorization.BrandPromotionEditorPermission)))
            {
                permissionDbSet.Add(Constants.Authorization.BrandPromotionEditorPermission.ToPermissionEntity(Constants.Authorization.BrandPromotionManagementPermissionGroup));
            }
            if (!permissionDbSet.Any(item => item.Code.Equals(Constants.Authorization.BrandPromotionRemoverPermission)))
            {
                permissionDbSet.Add(Constants.Authorization.BrandPromotionRemoverPermission.ToPermissionEntity(Constants.Authorization.BrandPromotionManagementPermissionGroup));
            }

            #endregion

            #region Brand

            if (!permissionDbSet.Any(item => item.Code.Equals(Constants.Authorization.BrandViewerPermission)))
            {
                permissionDbSet.Add(Constants.Authorization.BrandViewerPermission.ToPermissionEntity(Constants.Authorization.BrandManagementPermissionGroup));
            }
            if (!permissionDbSet.Any(item => item.Code.Equals(Constants.Authorization.BrandCreatorPermission)))
            {
                permissionDbSet.Add(Constants.Authorization.BrandCreatorPermission.ToPermissionEntity(Constants.Authorization.BrandManagementPermissionGroup));
            }
            if (!permissionDbSet.Any(item => item.Code.Equals(Constants.Authorization.BrandEditorPermission)))
            {
                permissionDbSet.Add(Constants.Authorization.BrandEditorPermission.ToPermissionEntity(Constants.Authorization.BrandManagementPermissionGroup));
            }
            if (!permissionDbSet.Any(item => item.Code.Equals(Constants.Authorization.BrandRemoverPermission)))
            {
                permissionDbSet.Add(Constants.Authorization.BrandRemoverPermission.ToPermissionEntity(Constants.Authorization.BrandManagementPermissionGroup));
            }

            #endregion

            #region BrandSupportOffer

            if (!permissionDbSet.Any(item => item.Code.Equals(Constants.Authorization.BrandSupportOfferViewerPermission)))
            {
                permissionDbSet.Add(Constants.Authorization.BrandSupportOfferViewerPermission.ToPermissionEntity(Constants.Authorization.BrandSupportOfferManagementPermissionGroup));
            }
            if (!permissionDbSet.Any(item => item.Code.Equals(Constants.Authorization.BrandSupportOfferCreatorPermission)))
            {
                permissionDbSet.Add(Constants.Authorization.BrandSupportOfferCreatorPermission.ToPermissionEntity(Constants.Authorization.BrandSupportOfferManagementPermissionGroup));
            }
            if (!permissionDbSet.Any(item => item.Code.Equals(Constants.Authorization.BrandSupportOfferEditorPermission)))
            {
                permissionDbSet.Add(Constants.Authorization.BrandSupportOfferEditorPermission.ToPermissionEntity(Constants.Authorization.BrandSupportOfferManagementPermissionGroup));
            }
            if (!permissionDbSet.Any(item => item.Code.Equals(Constants.Authorization.BrandSupportOfferRemoverPermission)))
            {
                permissionDbSet.Add(Constants.Authorization.BrandSupportOfferRemoverPermission.ToPermissionEntity(Constants.Authorization.BrandSupportOfferManagementPermissionGroup));
            }

            #endregion

            #region Category

            if (!permissionDbSet.Any(item => item.Code.Equals(Constants.Authorization.CategoryViewerPermission)))
            {
                permissionDbSet.Add(Constants.Authorization.CategoryViewerPermission.ToPermissionEntity(Constants.Authorization.CategoryManagementPermissionGroup));
            }
            if (!permissionDbSet.Any(item => item.Code.Equals(Constants.Authorization.CategoryCreatorPermission)))
            {
                permissionDbSet.Add(Constants.Authorization.CategoryCreatorPermission.ToPermissionEntity(Constants.Authorization.CategoryManagementPermissionGroup));
            }
            if (!permissionDbSet.Any(item => item.Code.Equals(Constants.Authorization.CategoryEditorPermission)))
            {
                permissionDbSet.Add(Constants.Authorization.CategoryEditorPermission.ToPermissionEntity(Constants.Authorization.CategoryManagementPermissionGroup));
            }
            if (!permissionDbSet.Any(item => item.Code.Equals(Constants.Authorization.CategoryRemoverPermission)))
            {
                permissionDbSet.Add(Constants.Authorization.CategoryRemoverPermission.ToPermissionEntity(Constants.Authorization.CategoryManagementPermissionGroup));
            }

            #endregion

            #region ContentBlock

            if (!permissionDbSet.Any(item => item.Code.Equals(Constants.Authorization.RichTextBlockViewerPermission)))
            {
                permissionDbSet.Add(Constants.Authorization.RichTextBlockViewerPermission.ToPermissionEntity(Constants.Authorization.RichTextBlockManagementPermissionGroup));
            }
            if (!permissionDbSet.Any(item => item.Code.Equals(Constants.Authorization.RichTextBlockCreatorPermission)))
            {
                permissionDbSet.Add(Constants.Authorization.RichTextBlockCreatorPermission.ToPermissionEntity(Constants.Authorization.RichTextBlockManagementPermissionGroup));
            }
            if (!permissionDbSet.Any(item => item.Code.Equals(Constants.Authorization.RichTextBlockEditorPermission)))
            {
                permissionDbSet.Add(Constants.Authorization.RichTextBlockEditorPermission.ToPermissionEntity(Constants.Authorization.RichTextBlockManagementPermissionGroup));
            }
            if (!permissionDbSet.Any(item => item.Code.Equals(Constants.Authorization.RichTextBlockRemoverPermission)))
            {
                permissionDbSet.Add(Constants.Authorization.RichTextBlockRemoverPermission.ToPermissionEntity(Constants.Authorization.RichTextBlockManagementPermissionGroup));
            }

            #endregion

            #region Post

            if (!permissionDbSet.Any(item => item.Code.Equals(Constants.Authorization.PostViewerPermission)))
            {
                permissionDbSet.Add(Constants.Authorization.PostViewerPermission.ToPermissionEntity(Constants.Authorization.PostManagementPermissionGroup));
            }
            if (!permissionDbSet.Any(item => item.Code.Equals(Constants.Authorization.PostCreatorPermission)))
            {
                permissionDbSet.Add(Constants.Authorization.PostCreatorPermission.ToPermissionEntity(Constants.Authorization.PostManagementPermissionGroup));
            }
            if (!permissionDbSet.Any(item => item.Code.Equals(Constants.Authorization.PostEditorPermission)))
            {
                permissionDbSet.Add(Constants.Authorization.PostEditorPermission.ToPermissionEntity(Constants.Authorization.PostManagementPermissionGroup));
            }
            if (!permissionDbSet.Any(item => item.Code.Equals(Constants.Authorization.PostRemoverPermission)))
            {
                permissionDbSet.Add(Constants.Authorization.PostRemoverPermission.ToPermissionEntity(Constants.Authorization.PostManagementPermissionGroup));
            }

            #endregion

            #region Service

            if (!permissionDbSet.Any(item => item.Code.Equals(Constants.Authorization.ServiceViewerPermission)))
            {
                permissionDbSet.Add(Constants.Authorization.ServiceViewerPermission.ToPermissionEntity(Constants.Authorization.ServiceManagementPermissionGroup));
            }
            if (!permissionDbSet.Any(item => item.Code.Equals(Constants.Authorization.ServiceCreatorPermission)))
            {
                permissionDbSet.Add(Constants.Authorization.ServiceCreatorPermission.ToPermissionEntity(Constants.Authorization.ServiceManagementPermissionGroup));
            }
            if (!permissionDbSet.Any(item => item.Code.Equals(Constants.Authorization.ServiceEditorPermission)))
            {
                permissionDbSet.Add(Constants.Authorization.ServiceEditorPermission.ToPermissionEntity(Constants.Authorization.ServiceManagementPermissionGroup));
            }
            if (!permissionDbSet.Any(item => item.Code.Equals(Constants.Authorization.ServiceRemoverPermission)))
            {
                permissionDbSet.Add(Constants.Authorization.ServiceRemoverPermission.ToPermissionEntity(Constants.Authorization.ServiceManagementPermissionGroup));
            }

            #endregion

            #region ShowcaseItem

            if (!permissionDbSet.Any(item => item.Code.Equals(Constants.Authorization.ShowcaseItemViewerPermission)))
            {
                permissionDbSet.Add(Constants.Authorization.ShowcaseItemViewerPermission.ToPermissionEntity(Constants.Authorization.ShowcaseItemManagementPermissionGroup));
            }
            if (!permissionDbSet.Any(item => item.Code.Equals(Constants.Authorization.ShowcaseItemCreatorPermission)))
            {
                permissionDbSet.Add(Constants.Authorization.ShowcaseItemCreatorPermission.ToPermissionEntity(Constants.Authorization.ShowcaseItemManagementPermissionGroup));
            }
            if (!permissionDbSet.Any(item => item.Code.Equals(Constants.Authorization.ShowcaseItemEditorPermission)))
            {
                permissionDbSet.Add(Constants.Authorization.ShowcaseItemEditorPermission.ToPermissionEntity(Constants.Authorization.ShowcaseItemManagementPermissionGroup));
            }
            if (!permissionDbSet.Any(item => item.Code.Equals(Constants.Authorization.ShowcaseItemRemoverPermission)))
            {
                permissionDbSet.Add(Constants.Authorization.ShowcaseItemRemoverPermission.ToPermissionEntity(Constants.Authorization.ShowcaseItemManagementPermissionGroup));
            }

            #endregion

            #region Product

            if (!permissionDbSet.Any(item => item.Code.Equals(Constants.Authorization.ProductViewerPermission)))
            {
                permissionDbSet.Add(Constants.Authorization.ProductViewerPermission.ToPermissionEntity(Constants.Authorization.ProductManagementPermissionGroup));
            }
            if (!permissionDbSet.Any(item => item.Code.Equals(Constants.Authorization.ProductCreatorPermission)))
            {
                permissionDbSet.Add(Constants.Authorization.ProductCreatorPermission.ToPermissionEntity(Constants.Authorization.ProductManagementPermissionGroup));
            }
            if (!permissionDbSet.Any(item => item.Code.Equals(Constants.Authorization.ProductEditorPermission)))
            {
                permissionDbSet.Add(Constants.Authorization.ProductEditorPermission.ToPermissionEntity(Constants.Authorization.ProductManagementPermissionGroup));
            }
            if (!permissionDbSet.Any(item => item.Code.Equals(Constants.Authorization.ProductRemoverPermission)))
            {
                permissionDbSet.Add(Constants.Authorization.ProductRemoverPermission.ToPermissionEntity(Constants.Authorization.ProductManagementPermissionGroup));
            }

            #endregion

            #region Representative

            if (!permissionDbSet.Any(item => item.Code.Equals(Constants.Authorization.RepresentativeViewerPermission)))
            {
                permissionDbSet.Add(Constants.Authorization.RepresentativeViewerPermission.ToPermissionEntity(Constants.Authorization.RepresentativeManagementPermissionGroup));
            }
            if (!permissionDbSet.Any(item => item.Code.Equals(Constants.Authorization.RepresentativeCreatorPermission)))
            {
                permissionDbSet.Add(Constants.Authorization.RepresentativeCreatorPermission.ToPermissionEntity(Constants.Authorization.RepresentativeManagementPermissionGroup));
            }
            if (!permissionDbSet.Any(item => item.Code.Equals(Constants.Authorization.RepresentativeEditorPermission)))
            {
                permissionDbSet.Add(Constants.Authorization.RepresentativeEditorPermission.ToPermissionEntity(Constants.Authorization.RepresentativeManagementPermissionGroup));
            }
            if (!permissionDbSet.Any(item => item.Code.Equals(Constants.Authorization.RepresentativeRemoverPermission)))
            {
                permissionDbSet.Add(Constants.Authorization.RepresentativeRemoverPermission.ToPermissionEntity(Constants.Authorization.RepresentativeManagementPermissionGroup));
            }

            #endregion

            #region CustomAttribute

            if (!permissionDbSet.Any(item => item.Code.Equals(Constants.Authorization.CustomAttributeViewerPermission)))
            {
                permissionDbSet.Add(Constants.Authorization.CustomAttributeViewerPermission.ToPermissionEntity(Constants.Authorization.CustomAttributeManagementPermissionGroup));
            }
            if (!permissionDbSet.Any(item => item.Code.Equals(Constants.Authorization.CustomAttributeCreatorPermission)))
            {
                permissionDbSet.Add(Constants.Authorization.CustomAttributeCreatorPermission.ToPermissionEntity(Constants.Authorization.CustomAttributeManagementPermissionGroup));
            }
            if (!permissionDbSet.Any(item => item.Code.Equals(Constants.Authorization.CustomAttributeEditorPermission)))
            {
                permissionDbSet.Add(Constants.Authorization.CustomAttributeEditorPermission.ToPermissionEntity(Constants.Authorization.CustomAttributeManagementPermissionGroup));
            }
            if (!permissionDbSet.Any(item => item.Code.Equals(Constants.Authorization.CustomAttributeRemoverPermission)))
            {
                permissionDbSet.Add(Constants.Authorization.CustomAttributeRemoverPermission.ToPermissionEntity(Constants.Authorization.CustomAttributeManagementPermissionGroup));
            }

            #endregion

            #region PageHeader

            if (!permissionDbSet.Any(item => item.Code.Equals(Constants.Authorization.PageHeaderViewerPermission)))
            {
                permissionDbSet.Add(Constants.Authorization.PageHeaderViewerPermission.ToPermissionEntity(Constants.Authorization.PageHeaderManagementPermissionGroup));
            }
            if (!permissionDbSet.Any(item => item.Code.Equals(Constants.Authorization.PageHeaderCreatorPermission)))
            {
                permissionDbSet.Add(Constants.Authorization.PageHeaderCreatorPermission.ToPermissionEntity(Constants.Authorization.PageHeaderManagementPermissionGroup));
            }
            if (!permissionDbSet.Any(item => item.Code.Equals(Constants.Authorization.PageHeaderEditorPermission)))
            {
                permissionDbSet.Add(Constants.Authorization.PageHeaderEditorPermission.ToPermissionEntity(Constants.Authorization.PageHeaderManagementPermissionGroup));
            }
            if (!permissionDbSet.Any(item => item.Code.Equals(Constants.Authorization.PageHeaderRemoverPermission)))
            {
                permissionDbSet.Add(Constants.Authorization.PageHeaderRemoverPermission.ToPermissionEntity(Constants.Authorization.PageHeaderManagementPermissionGroup));
            }

            #endregion

            #region Commercial

            if (!permissionDbSet.Any(item => item.Code.Equals(Constants.Authorization.CommercialViewerPermission)))
            {
                permissionDbSet.Add(Constants.Authorization.CommercialViewerPermission.ToPermissionEntity(Constants.Authorization.CommercialManagementPermissionGroup));
            }
            if (!permissionDbSet.Any(item => item.Code.Equals(Constants.Authorization.CommercialCreatorPermission)))
            {
                permissionDbSet.Add(Constants.Authorization.CommercialCreatorPermission.ToPermissionEntity(Constants.Authorization.CommercialManagementPermissionGroup));
            }
            if (!permissionDbSet.Any(item => item.Code.Equals(Constants.Authorization.CommercialEditorPermission)))
            {
                permissionDbSet.Add(Constants.Authorization.CommercialEditorPermission.ToPermissionEntity(Constants.Authorization.CommercialManagementPermissionGroup));
            }
            if (!permissionDbSet.Any(item => item.Code.Equals(Constants.Authorization.CommercialRemoverPermission)))
            {
                permissionDbSet.Add(Constants.Authorization.CommercialRemoverPermission.ToPermissionEntity(Constants.Authorization.CommercialManagementPermissionGroup));
            }

            #endregion

            if (dbContext is Framework.Guama.Core.Persistence.ORM.EF.Context.Contract.ITraceableDbContext)
            {
                ((Framework.Guama.Core.Persistence.ORM.EF.Context.Contract.ITraceableDbContext)dbContext).SaveChanges("system");
            }
            else
            {
                dbContext.SaveChanges();
            }
        }
    }
}
