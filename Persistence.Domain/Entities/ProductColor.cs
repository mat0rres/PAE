namespace MOROPO.PremierAutomotiveExport.Persistence.Domain.Entities
{
    public class ProductColor : Framework.Guama.Core.Persistence.Entities.Contract.IEntity<System.Guid>
    {
        [System.ComponentModel.DataAnnotations.Key]
        public System.Guid Id { get; set; } = System.Guid.NewGuid();

        [System.ComponentModel.DataAnnotations.Required]
        public string Label { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        public string Color { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.ForeignKey(nameof(ProductId))]
        public Product Product { get; set; }
        public System.Guid ProductId { get; set; }
    }
}
