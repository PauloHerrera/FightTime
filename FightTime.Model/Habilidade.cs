using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FightTime.Model
{
    public class Habilidade
    {
        public Int64 HabilidadeId { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }

        public List<Lutador> Lutadores { get; set; }

    }
}
