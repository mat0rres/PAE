using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace MOROPO.PremierAutomotiveExport.WebOlupin.Persistence.ORM.EF.Repositories
{
    public class BrandRepository : Framework.Guama.Core.Persistence.ORM.EF.Repositories.Contract.AbstractEntityRepository<PremierAutomotiveExport.Persistence.Domain.Entities.Brand, System.Guid>
    {
        public BrandRepository(DbContext dbContext) : base(dbContext)
        {
        }

        protected override IQueryable<PremierAutomotiveExport.Persistence.Domain.Entities.Brand> GetQueryableDbSet()
        {
            return base.GetQueryableDbSet().Include(entity => entity.Identity).ThenInclude(entity => entity.Image);
        }
    }
}
