namespace MOROPO.PremierAutomotiveExport.WebOlupin.Persistence.ORM.EF.Configurators
{
    public class BrandEntityTypeConfigurator : Framework.Guama.Core.Persistence.ORM.EF.Configurators.AbstractEntityTypeConfigurator<PremierAutomotiveExport.Persistence.Domain.Entities.Brand>
    {
        protected override void ApplayRules(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<PremierAutomotiveExport.Persistence.Domain.Entities.Brand> entityTypeBuilder)
        {
            base.ApplayRules(entityTypeBuilder);

            entityTypeBuilder.HasOne(entity => entity.Identity).WithOne().HasForeignKey<PremierAutomotiveExport.Persistence.Domain.Entities.Brand>(entity => entity.IdentityId).IsRequired();
        }
    }
}
