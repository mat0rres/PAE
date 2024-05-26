namespace MOROPO.PremierAutomotiveExport.WebOlupin.Models.View
{
    public class BrandPromotionDisplayViewModel : Framework.Guama.Core.Persistence.Presentation.Models.Contract.IEntityDisplayModel<PremierAutomotiveExport.Persistence.Domain.Entities.BrandPromotion, System.Guid>
    {
        public System.Guid Id { get; set; }

        public BrandDisplayViewModel Brand { get; set; }

        public ImageDisplayViewModel FeatureImage { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public void Import(PremierAutomotiveExport.Persistence.Domain.Entities.BrandPromotion entity)
        {
            Id = entity.Id;
            Title = entity.Title;
            Description = entity.Description;
            Brand = new BrandDisplayViewModel();
            FeatureImage = new ImageDisplayViewModel();

            Brand.Import(entity.Brand);
            FeatureImage.Import(entity.FeatureImage);
        }
    }
}
