namespace MOROPO.PremierAutomotiveExport.WebOlupin.Models.View
{
    public class BrandSupportOfferDisplayViewModel : Framework.Guama.Core.Persistence.Presentation.Models.Contract.IEntityDisplayModel<PremierAutomotiveExport.Persistence.Domain.Entities.BrandSupportOffer, System.Guid>
    {
        public System.Guid Id { get; set; }

        public bool Enabled { get; set; }

        public BrandDisplayViewModel Brand { get; set; }

        public ImageDisplayViewModel FeatureImage { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public void Import(PremierAutomotiveExport.Persistence.Domain.Entities.BrandSupportOffer entity)
        {
            Id = entity.Id;
            Enabled = entity.Enabled;
            Title = entity.Title;
            Description = entity.Description;
            Brand = new BrandDisplayViewModel();

            Brand.Import(entity.Brand);

            if (entity.FeatureImage != null)
            {
                FeatureImage = new ImageDisplayViewModel();

                FeatureImage.Import(entity.FeatureImage);
            }
        }
    }
}
