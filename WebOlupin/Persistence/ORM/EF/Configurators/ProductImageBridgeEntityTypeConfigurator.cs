using MOROPO.Framework.Guama.Media.Persistence.Entities;
using MOROPO.PremierAutomotiveExport.Persistence.Domain.Entities;

namespace MOROPO.PremierAutomotiveExport.WebOlupin.Persistence.ORM.EF.Configurators
{
    public class ProductImageBridgeEntityTypeConfigurator : Framework.Guama.Core.Persistence.ORM.EF.Configurators.AbstractBridgeEntityTypeConfigurator<ProductImageBridge, Product, System.Guid, Image, System.Guid>
    {
        public ProductImageBridgeEntityTypeConfigurator()
        {
            prowNavigationExpression = entity => entity.ExtraImageBridges;
        }
    }
}
