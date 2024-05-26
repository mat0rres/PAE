namespace MOROPO.PremierAutomotiveExport.WebOlupin.Models.View
{
    public class ProductDisplayViewModel : Framework.Guama.Core.Persistence.Presentation.Models.Contract.IEntityDisplayModel<PremierAutomotiveExport.Persistence.Domain.Entities.Product, System.Guid>
    {
        public System.Guid Id { get; set; }

        public bool Enabled { get; set; }

        public string Description { get; set; }

        public CategoryDisplayViewModel Category { get; set; }

        public ImageDisplayViewModel FeatureImage { get; set; }

        public VideoDisplayViewModel Video { get; set; }

        public BrandDisplayViewModel Brand { get; set; }

        public string Model { get; set; }

        public void Import(PremierAutomotiveExport.Persistence.Domain.Entities.Product entity)
        {
            Id = entity.Id;
            Enabled = entity.Enabled;
            Description = entity.Description;
            FeatureImage = new ImageDisplayViewModel();
            Brand = new BrandDisplayViewModel();
            Model = entity.Model;

            FeatureImage.Import(entity.FeatureImage);

            Brand.Import(entity.Brand);
        }
    }
}
