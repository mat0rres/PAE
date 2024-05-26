namespace MOROPO.PremierAutomotiveExport.WebOlupin.Persistence.ORM.EF.Configurators
{
    public class RepresentativeEntityTypeConfigurator : Framework.Guama.Core.Persistence.ORM.EF.Configurators.AbstractEntityTypeConfigurator<PremierAutomotiveExport.Persistence.Domain.Entities.Representative>
    {
        protected override void ApplayRules(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<PremierAutomotiveExport.Persistence.Domain.Entities.Representative> entityTypeBuilder)
        {
            base.ApplayRules(entityTypeBuilder);

            entityTypeBuilder.HasOne(entity => entity.Identity).WithOne().HasForeignKey<PremierAutomotiveExport.Persistence.Domain.Entities.Representative>(entity => entity.IdentityId).IsRequired();
        }
    }
}
