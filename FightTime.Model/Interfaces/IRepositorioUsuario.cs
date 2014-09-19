using System;
using System.Collections.Generic;

namespace FightTime.Model.Interfaces
{
    public interface IRepositorioUsuario
    {
        Usuario Login(string usuario, string password);
        Usuario Add(Usuario usuario);
    }
    
}
