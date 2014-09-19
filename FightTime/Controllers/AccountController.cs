using System.Security.Cryptography;
using System.Web.Mvc;
using System.Web.Security;
using FightTime.Helpers;
using FightTime.Model;
using FightTime.Model.Interfaces;
using FightTime.Models;
using Ninject;

namespace FightTime.Controllers
{
    public class AccountController : Controller
    {
        //
        // GET: /Account/Login

        private IRepositorioUsuario _repositorio;

        [Inject]
        public AccountController(IRepositorioUsuario repositorio)
        {
            _repositorio = repositorio;
        }

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            var userLogin = _repositorio.Login(model.Usuario, GeneralHelper.ComputeHash(model.Password, new SHA256CryptoServiceProvider()));

            if (userLogin != null)
            {
                SetUserCookie(userLogin);

                //FormsAuthentication.SetAuthCookie(model.Usuario, false);

                //SetUserCookie(userLogin);
                return RedirectToLocal(returnUrl);
            }
            
            
            ModelState.AddModelError("", "The user name or password provided is incorrect.");
            return View(model);
        }

        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register

        [HttpPost]
        public ActionResult Register(RegisterViewModel model)
        {
           // Attempt to register the user
                try
                {
                    var usuario = new Usuario()
                                      {
                                          Username = model.UserName,
                                          Password =
                                              GeneralHelper.ComputeHash(model.Password,
                                                                        new SHA256CryptoServiceProvider())
                                      };

                    _repositorio.Add(usuario);

                    SetUserCookie(usuario);
                    
                    return RedirectToAction("Index", "Dojo");
                }
                catch (MembershipCreateUserException e)
                {
                    
                }
            
            // If we got this far, something failed, redisplay form
            return View(model);
        }


        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Home");
        }

        private void SetUserCookie(Usuario userLogin)
        {
            FormsAuthentication.SetAuthCookie(userLogin.UsuarioId + "|" + userLogin.Username, false);
            
            //OOOLLLDDDD
            //var userCookie = new HttpCookie("fighttimeuser", json);
            //userCookie.Expires.AddDays(1);
            //HttpContext.Response.SetCookie(userCookie);
            //HttpContext.Response.Cookies.Add(userCookie);
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl) && returnUrl != "/")
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Dojo");
            }
        }
    }
}
