using Microsoft.EntityFrameworkCore;

namespace MOROPO.PremierAutomotiveExport.WebOlupin.Persistence.ORM.EF.Repositories
{
    public class RepresentativeRepository : Framework.Guama.Core.Persistence.ORM.EF.Repositories.Contract.AbstractEntityRepository<PremierAutomotiveExport.Persistence.Domain.Entities.Representative, System.Guid>
    {
        public RepresentativeRepository(DbContext dbContext) : base(dbContext)
        {
        }

        protected override System.Linq.IQueryable<PremierAutomotiveExport.Persistence.Domain.Entities.Representative> GetQueryableDbSet()
        {
            return base.GetQueryableDbSet().Include(entity => entity.Identity).ThenInclude(entity => entity.Image);
        }

        public override System.Func<PremierAutomotiveExport.Persistence.Domain.Entities.Representative, object> GetPagingOrderSelector()
        {
            return entity => entity.Priority;
        }
    }
}
