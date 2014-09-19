using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FightTime.Data.Config;
using FightTime.Model;
using FightTime.Model.Interfaces;

namespace FightTime.Data.Queries
{
    public class RepositorioUsuario : IRepositorioUsuario, IDisposable
    {
        public EntityContext db = new EntityContext();

        public Usuario Login(string usuario, string password)
        {
            var query = from p in db.Usuarios
                            where p.Username == usuario && p.Password == password
                            select p;

            return query.FirstOrDefault();

        }

        public Usuario Add(Usuario usuario)
        {
            db.Set<Usuario>().AddOrUpdate(usuario);
            db.SaveChanges();

            return usuario;
        }

        public void Dispose()
        {
            if (db != null)
            {
                db.Dispose();
                db = null;
            }

            GC.SuppressFinalize(this);
        }


    }
}
