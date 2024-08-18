using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCPelicula.Models;
using MVCPelicula.ViewModels;

//autenticacion
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;


namespace MVCPelicula.Controllers
{
    public class UsuariosController : Controller
    {
        //Base de datos
        private readonly PeliculasDBContext _context;

        public UsuariosController(PeliculasDBContext context)
        {
            _context = context;
        }

        //GET
        [HttpGet]
        public IActionResult Registrar()
        {
            if (User.Identity!.IsAuthenticated) return RedirectToAction("Index", "Peliculas");
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Registrar(UsuarioVM usuariovm)
        {
            if (usuariovm.Clave != usuariovm.ConfirmarClave)
            {
                ViewData["Mensaje"] = "Las contraseñas no coinciden";
            }
            else
            {

                //Si coinciden se creara un nuevo usuario
                Usuario usuario = new Usuario()
                {
                    Correo = usuariovm.Correo,
                    Clave = usuariovm.Clave
                };

                await _context.Usuarios.AddAsync(usuario);
                await _context.SaveChangesAsync();


                if (usuario.ID != 0)
                {
                    return RedirectToAction("Login", "Usuarios");
                }

                ViewData["Mensaje"] = "Error al registar usuario";


            }
            return View();
        }


        //GET
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Login(LoginVM loginvm)
        {
            Usuario? usuario_encontrado = await _context.Usuarios
                .Where(u => u.Correo == loginvm.Correo && u.Clave == loginvm.Clave)
                .FirstOrDefaultAsync();

            if (usuario_encontrado == null)
            {
                ViewData["Mensaje"] = "Error. El usuario o contraseña es incorrecto";
                return View();
            }
            else
            {
                //AUTENTICACION

                List<Claim> claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Email, usuario_encontrado.Correo)
                };

                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims,CookieAuthenticationDefaults.AuthenticationScheme);
                AuthenticationProperties properties = new AuthenticationProperties()
                {
                    AllowRefresh = true,
                };
                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    properties
                    );
                return RedirectToAction("Index", "Peliculas");
            }
        }
    }

}
