using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FightTime.Data.Config;
using FightTime.Model;
using FightTime.Model.Interfaces;

namespace FightTime.Data.Queries
{
    public class RepositorioLutador : IRepositorioLutador, IDisposable
    {
        public EntityContext db = new EntityContext();

        public List<Lutador> ListaLutadores()
        {
            try
            {
                var query = db.Lutadores.ToList();
                            
                
//                var empresa = query.ToList();

                return query;
            }
            catch (EntitySqlException ex)
            {
                throw new Exception(ex.Message);
            }
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
