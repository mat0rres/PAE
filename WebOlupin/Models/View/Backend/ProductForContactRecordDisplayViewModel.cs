namespace MOROPO.PremierAutomotiveExport.WebOlupin.Models.View.Backend
{
    public class ProductForContactRecordDisplayViewModel : Framework.Guama.Core.Persistence.Presentation.Models.Contract.IEntityDisplayModel<PremierAutomotiveExport.Persistence.Domain.Entities.ProductForContactRecord, long>
    {
        public long Id { get; set; }

        public System.DateTime CreatedAt { get; set; } = System.DateTime.Now;

        public System.DateTime? ReadAt { get; set; }

        public string Name { get; set; }

        public string LastName { get; set; }

        public string EmailAddress { get; set; }

        public string PhoneNumber { get; set; }

        public string CI { get; set; }

        public string City { get; set; }

        public string PostalAddress { get; set; }

        public string Message { get; set; }

        public ProductDisplayViewModel Product { get; set; }

        public ProductColorDisplayViewModel ProductColor { get; set; }

        public int Quantity { get; set; }

        public void Import(PremierAutomotiveExport.Persistence.Domain.Entities.ProductForContactRecord entity)
        {
            Id = entity.Id;
            CreatedAt = entity.CreatedAt;
            ReadAt = entity.ReadAt;
            EmailAddress = entity.EmailAddress;
            PhoneNumber = entity.PhoneNumber;
            Name = entity.Name;
            LastName = entity.LastName;
            CI = entity.CI;
            City = entity.City;
            PostalAddress = entity.PostalAddress;
            Message = entity.Message;
            Quantity = entity.Quantity;
            Product = new ProductDisplayViewModel
            {
                Id = entity.ProductId,
                Brand = new BrandDisplayViewModel
                {
                    Id = entity.Product.BrandId,
                    Name = entity.Product.Brand.Identity.Name
                },
                Model = entity.Product.Model
            };
            ProductColor = new ProductColorDisplayViewModel
            {
                Id = entity.ColorId,
                Label = entity.Color.Label,
                Color = entity.Color.Color
            };
        }
    }
}
