using Microsoft.AspNetCore.Mvc;
using NetCoreSeguridadDoctores.Models;
using NetCoreSeguridadDoctores.Repositories;

namespace NetCoreSeguridadDoctores.Controllers
{
    public class EnfermosController : Controller
    {
        private RepositoryEnfermos repo;

        public EnfermosController(RepositoryEnfermos repo)
        {
            this.repo = repo;
        }
        public IActionResult Index()
        {
            List<Enfermo> enfermos = this.repo.GetEnfermos();
            return View(enfermos);
        }
    }
}
