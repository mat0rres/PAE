using MOROPO.Framework.Guama.Core.Persistence.Repositories;
using MOROPO.PremierAutomotiveExport.Persistence.Domain.Entities;
using MOROPO.PremierAutomotiveExport.WebOlupin.Models.View;
using System.Linq;

namespace MOROPO.PremierAutomotiveExport.WebOlupin.Controllers.Frontend
{
    public class NewsController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly IEntityRepository<Post, long> postRepository;

        public NewsController(IEntityRepository<Post, long> postRepository)
        {
            this.postRepository = postRepository;
        }

        public Microsoft.AspNetCore.Mvc.IActionResult Index()
        {
            return View(postRepository.FindBy(entity => entity.Enabled).OrderByDescending(entity => entity.CreatedAt).ToArray().Select(entity =>
            {
                var _model = new PostDisplayViewModel();

                _model.Import(entity);

                return _model;
            }).ToArray());
        }
    }
}
