using MOROPO.Framework.Guama.Core.Persistence.Repositories;
using MOROPO.PremierAutomotiveExport.Persistence.Domain.Entities;
using MOROPO.PremierAutomotiveExport.Persistence.Domain.Enumerations;

namespace MOROPO.PremierAutomotiveExport.WebOlupin.Controllers.Frontend
{
    public class ContactController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly IEntityRepository<RichTextBlock, RichTextBlockKind> richTextBlockRepository;

        public ContactController(IEntityRepository<RichTextBlock, RichTextBlockKind> richTextBlockRepository)
        {
            this.richTextBlockRepository = richTextBlockRepository;
        }

        public Microsoft.AspNetCore.Mvc.IActionResult Index()
        {
            var model = new Models.View.RichTextBlockDisplayViewModel();

            model.Import(richTextBlockRepository.Get(RichTextBlockKind.Contact));

            return View(model);
        }
    }
}
