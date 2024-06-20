namespace MOROPO.PremierAutomotiveExport.WebOlupin.Models.View
{
	public class ProductColorDisplayViewModel : Framework.Guama.Core.Persistence.Presentation.Models.Contract.IEntityDisplayModel<PremierAutomotiveExport.Persistence.Domain.Entities.ProductColor, System.Guid>
	{
		public System.Guid Id { get; set; }

		public string Color { get; set; }

        public void Import(PremierAutomotiveExport.Persistence.Domain.Entities.ProductColor entity)
        {
            Id = entity.Id;
            Color = entity.Color;
        }
    }
}

