namespace MOROPO.PremierAutomotiveExport.Persistence.Domain.Entities
{
    public class ProductImageBridge : Framework.Guama.Core.Persistence.Entities.Contract.IBridge<Product, System.Guid, Framework.Guama.Media.Persistence.Entities.Image, System.Guid>
    {
        public System.Guid ProwId { get; set; }
        public Product Prow { get; set; }

        public System.Guid SternId { get; set; }
        public Framework.Guama.Media.Persistence.Entities.Image Stern { get; set; }
    }
}
