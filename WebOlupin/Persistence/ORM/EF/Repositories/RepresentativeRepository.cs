using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace MOROPO.PremierAutomotiveExport.WebOlupin.Persistence.ORM.EF.Repositories
{
    public class RepresentativeRepository : Framework.Guama.Core.Persistence.ORM.EF.Repositories.Contract.AbstractEntityRepository<PremierAutomotiveExport.Persistence.Domain.Entities.Representative, System.Guid>
    {
        public RepresentativeRepository(DbContext dbContext) : base(dbContext)
        {
        }

        protected override IQueryable<PremierAutomotiveExport.Persistence.Domain.Entities.Representative> GetQueryableDbSet()
        {
            return base.GetQueryableDbSet().Include(entity => entity.Identity).ThenInclude(entity => entity.Image);
        }
    }
}
