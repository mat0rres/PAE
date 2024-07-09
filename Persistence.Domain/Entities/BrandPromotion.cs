namespace MOROPO.PremierAutomotiveExport.Persistence.Domain.Entities
{
    public class BrandPromotion : Framework.Guama.Core.Persistence.Entities.Contract.IHabilitableEntity<System.Guid>
    {
        [System.ComponentModel.DataAnnotations.Key]
        public System.Guid Id { get; set; } = System.Guid.NewGuid();

        public bool Enabled { get; set; } = true;

        public int Priority { get; set; } = 1;

        [System.ComponentModel.DataAnnotations.Schema.ForeignKey(nameof(BrandId))]
        public Brand Brand { get; set; }
        public System.Guid BrandId { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.ForeignKey(nameof(FeatureImageId))]
        public Framework.Guama.Media.Persistence.Entities.Image FeatureImage { get; set; }
        public System.Guid FeatureImageId { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        public string Title { get; set; }

        public string Description { get; set; }
    }
}
