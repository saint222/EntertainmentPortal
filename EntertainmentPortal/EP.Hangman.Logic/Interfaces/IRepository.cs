using System;
using System.Collections.Generic;
using System.Text;

namespace EP.Hagman.Logic.Interfaces
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetList();
        void Create(T item);
        void Read(T item);
        void Update(T item);
    }
}
