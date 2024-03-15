using Microsoft.AspNetCore.Mvc;
using NetCoreSeguridadDoctores.Filters;
using NetCoreSeguridadDoctores.Models;
using NetCoreSeguridadDoctores.Repositories;

namespace NetCoreSeguridadDoctores.Controllers
{
    public class DoctoresController : Controller
    {
        private RepositoryDoctores repo;
        private RepositoryEnfermos repoEnfermos;
        public DoctoresController
            (RepositoryDoctores repo, RepositoryEnfermos repoEnfermos)
        {
            this.repo = repo;
            this.repoEnfermos = repoEnfermos;
        }

        public async Task<IActionResult> Index()
        {
            
            return View();
        }

        public async Task<IActionResult> Detalles(int iddoctor)
        {
            Doctor doctor = await 
                this.repo.FindDoctorAsync(iddoctor);
            return View(doctor);
        }

        [AuthorizeDoctores]
        public IActionResult PerfilDoctor()
        {
            return View();
        }

        [AuthorizeDoctores(Policy = "PERMISOSELEVADOS")]
        public async Task<IActionResult> EliminarEnfermo(int idenfermo)
        {
            //int idenfermo = int.Parse(TempData["ENFERMO"].ToString());
            await this.repoEnfermos.EliminarEnfermoAsync(idenfermo);
            return RedirectToAction("Index","Enfermos");
        }

        [AuthorizeDoctores]
        public async Task<IActionResult> Confirmar(int idenfermo)
        {
            //TempData["ENFERMO"] = idenfermo;
            Enfermo e = await this.repoEnfermos.FindEnfermoAsync(idenfermo);
            return View(e);
        }

        [AuthorizeDoctores(Policy = "AdminOnly")]
        public IActionResult AdminDoctores()
        {
            return View();
        }

        [AuthorizeDoctores(Policy = "SoloRicos")]
        public IActionResult SoloRicos()
        {
            return View();
        }
    }
}
