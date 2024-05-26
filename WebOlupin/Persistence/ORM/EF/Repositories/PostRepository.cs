using Microsoft.EntityFrameworkCore;
using MOROPO.PremierAutomotiveExport.Persistence.Domain.Entities;
using System.Linq;

namespace MOROPO.PremierAutomotiveExport.WebOlupin.Persistence.ORM.EF.Repositories
{
    public class PostRepository : Framework.Guama.Core.Persistence.ORM.EF.Repositories.Contract.AbstractEntityRepository<Post, long>
    {
        public PostRepository(DbContext dbContext) : base(dbContext)
        {
        }

        protected override IQueryable<Post> GetQueryableDbSet()
        {
            return base.GetQueryableDbSet().Include(entity => entity.FeatureImage);
        }
    }
}
