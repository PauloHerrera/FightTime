using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FightTime.Filters;
using FightTime.Model;
using FightTime.Model.Interfaces;
using FightTime.Models;
using Ninject;

namespace FightTime.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        // 2 jeitos de ser feito
        [Inject]
        public IRepositorioLutador Repositorio { get; set; }

        private IRepositorioGenerico<Lutador> _repositorio;

        [Inject]
        public HomeController(IRepositorioGenerico<Lutador> repositorio)
        {
            _repositorio = repositorio;
        }

        public ActionResult Index()
        {

            return View();
        }

        public ActionResult Simulacao01()
        {
            //TESTEEE
            var lutadores = _repositorio.GetAll();

            
            var lutadorA = lutadores.FirstOrDefault(x => x.LutadorId == 6);
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
