namespace MOROPO.PremierAutomotiveExport.Persistence.Domain.Entities
{
    public class ProductFeature : Framework.Guama.Core.Persistence.Entities.Contract.IEntity<long>
    {
        [System.ComponentModel.DataAnnotations.Key]
        public long Id { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        public string Description { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.ForeignKey(nameof(ProductId))]
        public Product Product { get; set; }
        public System.Guid ProductId { get; set; }
    }
}
