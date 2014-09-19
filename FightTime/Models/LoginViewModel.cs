using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using FightTime.Model;

namespace FightTime.Models
{
    public class LutadorViewModel
    {
        public Int64 LutadorId { get; set; }
        public string Nome { get; set; }
        public string Apelido { get; set; }
        public ClasseLutador Classe { get; set; }

        public int Saude { get; set; }
        public int Forca { get; set; }
        public int Velocidade { get; set; }
        public int Agilidade { get; set; }
        public Int64 Experiencia { get; set; }
        public int Nivel { get; set; }
        
        public static LutadorViewModel LutadorEntityToViewModel(Lutador lutador)
        {
            if (lutador == null)
                return new LutadorViewModel();

            var lutadorVm = new LutadorViewModel()
                                {
                                    Nome = lutador.Nome,
                                    Apelido = lutador.Apelido
                                };
            return lutadorVm;
        }
    }

    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}