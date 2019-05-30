using System.Collections.Generic;
using System.Threading.Tasks;
using EP.Balda.Data.Contracts;

namespace EP.Balda.Data.EntityFramework
{
    public class WordRepository<T> : IWordRepository<T> where T : class
    {
        private readonly BaldaDictionaryDbContext _dictionaryDbContext;

        public WordRepository(BaldaDictionaryDbContext dictionaryDbContext)
        {
            _dictionaryDbContext = dictionaryDbContext;
        }

        public string Get(string str)
        {
            var dbSet  = BaldaDictionaryDbContext.Set<T>();
            var result = dbSet.Find(wrd => wrd.Contains(str));
            return result;
        }

        public string Get(int id)
        {
            var dbSet  = BaldaDictionaryDbContext.Set<T>().ToArray();
            var result = dbSet.GetValue(id).ToString();
            return result;
        }

        public List<string> GetAll()
        {
            return BaldaDictionaryDbContext.Set<T>();
        }

        public void Add(string entity)
        {
            var dbSet = BaldaDictionaryDbContext.Set<T>();
            dbSet.Add(entity);
        }

        public void Update(string str, T entity)
        {
            var dbSet        = BaldaDictionaryDbContext.Set<T>();
            var changeEntity = dbSet.Find(wrd => wrd.Contains(str));
            dbSet.Remove(changeEntity);
            dbSet.Add(entity.ToString());
        }

        public void Remove(string str)
        {
            var dbSet  = BaldaDictionaryDbContext.Set<T>();
            var entity = dbSet.Find(wrd => wrd.Contains(str));
            dbSet.Remove(entity);
        }

        public async Task SaveChanges()
        {
            await BaldaDictionaryDbContext.SaveChangesAsync();
        }
    }
}