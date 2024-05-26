using Microsoft.AspNetCore.Mvc;
using MOROPO.Framework.Guama.Core.Persistence.Repositories;
using MOROPO.PremierAutomotiveExport.Persistence.Domain.Entities;
using System.Linq;

namespace MOROPO.PremierAutomotiveExport.WebOlupin.Components
{
    public class FrontendNavigationBarViewComponent : ViewComponent
    {
        private readonly IEntityRepository<Category, int> categoryRepository;

        public FrontendNavigationBarViewComponent(IEntityRepository<Category, int> categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }

        public IViewComponentResult Invoke()
        {
            return View(categoryRepository.FindBy(entity => entity.Enabled && entity.IsOnNavigation).ToArray().Select(entity =>
            {
                var model = new Models.View.CategoryDisplayViewModel();

                model.Import(entity);

                return model;
            }).ToArray());
        }
    }
}
