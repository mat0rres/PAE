namespace MOROPO.PremierAutomotiveExport.WebOlupin.Persistence.ORM.EF
{
    public class DbSchemaConfigurationProvider : Framework.Guama.Core.Persistence.ORM.EF.Providers.Facade.IDbSchemaConfigurationProviderFacade
    {
        public System.Collections.Generic.ICollection<Framework.Guama.Core.Persistence.ORM.EF.Configurators.IEntityTypeConfigurator> EntityTypeConfigurators
        {
            get
            {
                return
                [
                    new Configurators.ProductCustomAttributeBridgeEntityTypeConfigurator(),
                    new Configurators.ProductImageBridgeEntityTypeConfigurator(),
                    new Configurators.BrandEntityTypeConfigurator(),
                    new Configurators.RepresentativeEntityTypeConfigurator(),
                    new Configurators.CommonContactRecordEntityTypeConfigurator(),
                    new Configurators.ProductForContactRecordEntityTypeConfigurator()
                ];
            }
        }

        public string DefaultPersistenceSchema => null;
    }
}
