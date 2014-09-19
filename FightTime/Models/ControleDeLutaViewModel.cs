using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using FightTime.Model;

namespace FightTime.Models
{
    public class ControleDeLutaViewModel
    {
       
        public LutadorViewModel Lutador1 { get; set; }
        public LutadorViewModel Lutador2 { get; set; }
        
        public String Acao { get; set; }
    }
}