namespace MOROPO.PremierAutomotiveExport.Persistence.Domain.Entities
{
    public class ProductCustomAttributeBridge : Framework.Guama.Core.Persistence.Entities.Contract.IBridge<Product, System.Guid, CustomAttribute, System.Guid>
    {
        public System.Guid ProwId { get; set; }
        public Product Prow { get; set; }

        public System.Guid SternId { get; set; }
        public CustomAttribute Stern { get; set; }

        public string Value { get; set; }
    }
}
