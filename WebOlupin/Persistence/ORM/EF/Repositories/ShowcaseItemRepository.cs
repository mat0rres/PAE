using Microsoft.EntityFrameworkCore;
using MOROPO.PremierAutomotiveExport.Persistence.Domain.Entities;
using System.Linq;

namespace MOROPO.PremierAutomotiveExport.WebOlupin.Persistence.ORM.EF.Repositories
{
    public class ShowcaseItemRepository : Framework.Guama.Core.Persistence.ORM.EF.Repositories.Contract.AbstractEntityRepository<ShowcaseItem, System.Guid>
    {
        public ShowcaseItemRepository(DbContext dbContext) : base(dbContext)
        {
        }

        protected override IQueryable<ShowcaseItem> GetQueryableDbSet()
        {
            return base.GetQueryableDbSet().Include(entity => entity.Image);
        }
    }
}
