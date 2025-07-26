using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using PruebaTecnicaEmpleados.Presentation.Models;
using PruebaTecnicaEmpleados.Presentation.Services.Interface;

namespace PruebaTecnicaEmpleados.Presentation.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUserService _apiService;
        public UsersController(IUserService apiService)
        {
            _apiService = apiService;

        }
       
    
        public async Task<ActionResult> Index()
        {
            List<User> usuarios = await _apiService.GetUsersAsync();
            if (usuarios == null)
            {
                
                ViewBag.ErrorMessage = "No se pudieron cargar los usuarios. Intente de nuevo más tarde.";
                return View(new List<User>());
            }
           
            if (TempData["SuccessMessage"] != null)
            {
                ViewBag.SuccessMessage = TempData["SuccessMessage"];
            }
            if (TempData["ErrorMessage"] != null)
            {
                ViewBag.ErrorMessage = TempData["ErrorMessage"];
            }
            return View(usuarios);
        }
        public ActionResult Create()
        {
            return View();
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken] // Protección contra ataques CSRF (Cross-Site Request Forgery)
        public async Task<ActionResult> Create(User user)
        {
            if (ModelState.IsValid) 
            {
                bool success = await _apiService.AddUserAsync(user);
                if (success)
                {
                    TempData["SuccessMessage"] = "Usuario agregado exitosamente.";
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError("", "Ocurrió un error al intentar agregar el usuario. Por favor, intente de nuevo.");
            }
            
            return View(user);
        }

  
        public async Task<ActionResult> Edit(int? id)
        {
            System.Diagnostics.Debug.WriteLine("Entrando a Edit con id = " + id);
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest); 
            }

            User user = await _apiService.GetUserByIdAsync(id.Value);

            if (user == null)
            {
                return HttpNotFound(); 
            }

            return View(user);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(User user)
        {
            if (ModelState.IsValid)
            {
                bool success = await _apiService.UpdateUserAsync(user);
                if (success)
                {
                    TempData["SuccessMessage"] = "Usuario actualizado exitosamente.";
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError("", "Ocurrió un error al intentar actualizar el usuario. Por favor, intente de nuevo.");
            }
            return View(user);
        }


        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }

            User user = await _apiService.GetUserByIdAsync(id.Value);

            if (user == null)
            {
                return HttpNotFound();
            }

            return View(user);
        }

   
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            bool success = await _apiService.DeleteUserAsync(id);

            if (success)
            {
                TempData["SuccessMessage"] = "Usuario eliminado exitosamente.";
            }
            else
            {
                TempData["ErrorMessage"] = "Ocurrió un error al intentar eliminar el usuario.";
            }

            return RedirectToAction("Index");
        }

        // Opcional: Si tienes una vista de detalles (para mostrar solo un usuario)
        // GET: Users/Details/5
        //public async Task<ActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
        //    }

        //    User user = await _apiService.GetUserByIdAsync(id.Value);

        //    if (user == null)
        //    {
        //        return HttpNotFound();
        //    }

        //    return View(user);
        //}
    }
}