using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FightTime.Model
{
    public class Lutador
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

        public virtual List<Habilidade> Habilidades { get; set; }
    }
}
