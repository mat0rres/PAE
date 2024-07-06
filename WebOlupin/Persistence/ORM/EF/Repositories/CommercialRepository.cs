namespace MOROPO.PremierAutomotiveExport.WebOlupin.Persistence.ORM.EF.Repositories
{
    public class CommercialRepository : Framework.Guama.Core.Persistence.ORM.EF.Repositories.Contract.AbstractEntityRepository<PremierAutomotiveExport.Persistence.Domain.Entities.Commercial, System.Guid>
    {
        public CommercialRepository(Microsoft.EntityFrameworkCore.DbContext dbContext) : base(dbContext)
        {
        }

        public override System.Func<PremierAutomotiveExport.Persistence.Domain.Entities.Commercial, object> GetPagingOrderSelector()
        {
            return entity => entity.FullName;
        }
    }
}
