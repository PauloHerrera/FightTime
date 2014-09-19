using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using FightTime.Filters;
using FightTime.Model;
using FightTime.Model.Interfaces;
using FightTime.Models;
using Newtonsoft.Json;
using Ninject;

namespace FightTime.Controllers
{
    [Authorize]
    public class DojoController : Controller
    {
        private readonly IRepositorioGenerico<Usuario> _repositorioUsuario;
        private readonly IRepositorioGenerico<Lutador> _repositorioLutador;

        [Inject]
        public DojoController(IRepositorioGenerico<Usuario> repositorioUsuario, IRepositorioGenerico<Lutador> repositorioLutador)
        {
            _repositorioUsuario = repositorioUsuario;
            _repositorioLutador = repositorioLutador;
        }

        public ActionResult Index()
        {
            var userInfo = FormsAuthentication.Decrypt(Request.Cookies[FormsAuthentication.FormsCookieName].Value).Name;

            if (string.IsNullOrEmpty(userInfo))
                RedirectToAction("Login","Account");

            var usuarioId = userInfo.Contains("|") ?  Int64.Parse(userInfo.Split('|').First()) : 0;

            var entity = _repositorioUsuario.Get(usuarioId);
                
            if(entity.Lutador != null)
            {
                var lutadorViewModel = LutadorViewModel.LutadorEntityToViewModel(entity.Lutador);

                return View(lutadorViewModel);        
            }else
            {
                return RedirectToAction("CriarLutador");        
            }
            
        }

        public ActionResult CriarLutador()
        {
            return View();
        }


        [HttpPost]
        public ActionResult CriarLutador(LutadorViewModel lutadorViewModel)
        {
            var userInfo = FormsAuthentication.Decrypt(Request.Cookies[FormsAuthentication.FormsCookieName].Value).Name;
            var usuarioId = userInfo.Contains("|") ? Int64.Parse(userInfo.Split('|').First()) : 0;

            if (string.IsNullOrEmpty(userInfo))
                RedirectToAction("Login", "Account");

            var lutador = new Lutador()
                              {
                                  Nome = lutadorViewModel.Nome,
                                  Apelido = lutadorViewModel.Apelido
                              };

            var usuario = _repositorioUsuario.Get(usuarioId);
            usuario.Lutador = lutador;

            _repositorioUsuario.Update(usuario);
            
            //_repositorioLutador.Add(lutador);
            
            return RedirectToAction("Index");      
            
        }

        public ActionResult Simulacao01()
        {
            //TESTEEE
            var lutadores = _repositorioLutador.GetAll();
            var lutadorA = lutadores.FirstOrDefault(x => x.LutadorId == 3);
            var lutadorB = lutadores.FirstOrDefault(x => x.LutadorId == 4);
            
            var controleDaLuta = new ControleDeLutaViewModel()
                                     {
                                         Lutador1 = lutadorA == null ? new LutadorViewModel() : new LutadorViewModel()
                                         {
                                             LutadorId = lutadorA.LutadorId,
                                             Nome = lutadorA.Nome,
                                             Classe = lutadorA.Classe,
                                             Apelido = lutadorA.Apelido,
                                             Saude = lutadorA.Saude,
                                             Agilidade = lutadorA.Agilidade,
                                             Forca = lutadorA.Forca,
                                             Velocidade = lutadorA.Velocidade,
                                             Experiencia = lutadorA.Experiencia,
                                             Nivel = lutadorA.Nivel
                                         },
                                         Lutador2 = lutadorB == null ? new LutadorViewModel() : new LutadorViewModel()
                                                        {
                                                            LutadorId = lutadorB.LutadorId,
                                                            Nome = lutadorB.Nome,
                                                            Classe = lutadorB.Classe,
                                                            Apelido = lutadorB.Apelido,
                                                            Saude = lutadorB.Saude,
                                                            Agilidade = lutadorB.Agilidade,
                                                            Forca = lutadorB.Forca,
                                                            Velocidade = lutadorB.Velocidade,
                                                            Experiencia = lutadorB.Experiencia,
                                                            Nivel = lutadorB.Nivel
                                                        },
                                     };



            return View(controleDaLuta);
        }
    }
}
