namespace MOROPO.PremierAutomotiveExport.Persistence.Domain.Entities
{
    public class Product : Framework.Guama.Core.Persistence.Entities.Contract.IHabilitableEntity<System.Guid>
    {
        [System.ComponentModel.DataAnnotations.Key]
        public System.Guid Id { get; set; } = System.Guid.NewGuid();

        public bool Enabled { get; set; }

        public int Priority { get; set; } = 1;

        public string Description { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.ForeignKey(nameof(CategoryId))]
        public Category Category { get; set; }
        public int CategoryId { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.ForeignKey(nameof(FeatureImageId))]
        public Framework.Guama.Media.Persistence.Entities.Image FeatureImage { get; set; }
        public System.Guid FeatureImageId { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.ForeignKey(nameof(VideoId))]
        public Framework.Guama.Media.Persistence.Entities.Video Video { get; set; }
        public System.Guid? VideoId { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.ForeignKey(nameof(BrandId))]
        public Brand Brand { get; set; }
        public System.Guid BrandId { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        public string Model { get; set; }

        public System.Collections.Generic.List<ProductFeature> Features { get; set; } = new System.Collections.Generic.List<ProductFeature>();

        public System.Collections.Generic.List<ProductColor> Colors {  get; set; } = new System.Collections.Generic.List<ProductColor>();

        public System.Collections.Generic.List<ProductImageBridge> ExtraImageBridges { get; set; } = new System.Collections.Generic.List<ProductImageBridge> { };

        public System.Collections.Generic.List<ProductCustomAttributeBridge> CustomAttributeBridges { get; set; } = new System.Collections.Generic.List<ProductCustomAttributeBridge>();
    }
}
