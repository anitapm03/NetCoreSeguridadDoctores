using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using NetCoreSeguridadDoctores.Models;
using NetCoreSeguridadDoctores.Repositories;
using System.Security.Claims;

namespace NetCoreSeguridadDoctores.Controllers
{
    public class ManagedController : Controller
    {
        private RepositoryDoctores repo;

        public ManagedController(RepositoryDoctores repo)
        {
            this.repo = repo;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login
            (string username, string password)
        {
            int idDoctor = int.Parse(password);
            Doctor doctor = await 
                this.repo.LoginDoctorAsync(username, idDoctor);

            if (doctor != null)
            {
                ClaimsIdentity identity =
                    new ClaimsIdentity(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        ClaimTypes.Name, ClaimTypes.Role);

                Claim claimName = 
                    new Claim(ClaimTypes.Name, doctor.Apellido);
                identity.AddClaim(claimName);

                Claim claimId =
                    new Claim(ClaimTypes.NameIdentifier, doctor.IdDoctor.ToString());
                identity.AddClaim(claimId);

                Claim claimEspecialidad =
                    new Claim(ClaimTypes.Role, doctor.Especialidad);
                identity.AddClaim(claimEspecialidad);

                Claim claimSalario =
                    new Claim("Salario", doctor.Salario.ToString());
                identity.AddClaim(claimSalario);

                Claim claimHospital =
                    new Claim("Hospital", doctor.Hospital.ToString());
                identity.AddClaim(claimHospital);

                ClaimsPrincipal userPrincipal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    userPrincipal);

                return RedirectToAction("PerfilDoctor", "Doctores");
            }
            else
            {
                ViewData["MSG"] = "Credenciales incorrectas";
                return View();
            }
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync
                (CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Enfermos");
        }

        public async Task<IActionResult> ErrorAcceso()
        {
            return View();
        }
    }
}
