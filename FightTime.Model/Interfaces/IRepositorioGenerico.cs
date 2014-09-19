using System.Linq;

namespace FightTime.Model.Interfaces
{
    public interface IRepositorioGenerico<T>
    {
        T Add(T entity);
        T Remove(T entity);
        void Remove(long id);
        void Update(T entity);
        IQueryable<T> GetAll();
        T Get(object key);
        void Salvar();
    }
}
