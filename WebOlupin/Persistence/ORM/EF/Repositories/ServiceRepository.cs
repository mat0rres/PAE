using Microsoft.EntityFrameworkCore;

namespace MOROPO.PremierAutomotiveExport.WebOlupin.Persistence.ORM.EF.Repositories
{
    public class ServiceRepository : Framework.Guama.Core.Persistence.ORM.EF.Repositories.Contract.AbstractEntityRepository<PremierAutomotiveExport.Persistence.Domain.Entities.Service, System.Guid>
    {
        public ServiceRepository(DbContext dbContext) : base(dbContext)
        {
        }

        protected override System.Linq.IQueryable<PremierAutomotiveExport.Persistence.Domain.Entities.Service> GetQueryableDbSet()
        {
            return base.GetQueryableDbSet().Include(entity => entity.FeatureImage);
        }

        public override System.Func<PremierAutomotiveExport.Persistence.Domain.Entities.Service, object> GetPagingOrderSelector()
        {
            return entity => entity.Priority;
        }
    }
}
