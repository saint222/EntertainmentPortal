using System.Collections.Generic;
using System.Threading.Tasks;

namespace EP.Balda.Data.Contracts
{
    public interface IWordRepository<T> where T : class
    {
        string Get(string inp);
        string Get(int inp);
        void Add(string entity);
        void Update(string str, T entity);
        void Remove(string str);
        List<string> GetAll();
        Task SaveChanges();
    }
}