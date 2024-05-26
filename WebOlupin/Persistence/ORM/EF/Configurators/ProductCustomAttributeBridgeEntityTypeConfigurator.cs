using MOROPO.PremierAutomotiveExport.Persistence.Domain.Entities;

namespace MOROPO.PremierAutomotiveExport.WebOlupin.Persistence.ORM.EF.Configurators
{
    public class ProductCustomAttributeBridgeEntityTypeConfigurator : Framework.Guama.Core.Persistence.ORM.EF.Configurators.AbstractBridgeEntityTypeConfigurator<ProductCustomAttributeBridge, Product, System.Guid, CustomAttribute, System.Guid>
    {
        public ProductCustomAttributeBridgeEntityTypeConfigurator()
        {
            prowNavigationExpression = entity => entity.CustomAttributeBridges;
        }
    }
}
