using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FightTime.Model
{
    public class Usuario
    {
        public Int64 UsuarioId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        
        public virtual Lutador Lutador { get; set; }
    }
}
