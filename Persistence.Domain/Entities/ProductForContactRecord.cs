namespace MOROPO.PremierAutomotiveExport.Persistence.Domain.Entities
{
    public class ProductForContactRecord : Contract.AbstractContactRecord
    {
        [System.ComponentModel.DataAnnotations.Required]
        public string LastName { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        [System.ComponentModel.DataAnnotations.RegularExpression("^[0-9]{11}$")]
        public string CI { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        public string City { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        public string PostalAddress { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.ForeignKey(nameof(ProductId))]
        public Product Product { get; set; }
        public System.Guid ProductId { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.ForeignKey(nameof(ColorId))]
        public ProductColor Color { get; set; }
        public System.Guid ColorId { get; set; }

        public int Quantity { get; set; }
    }
}
