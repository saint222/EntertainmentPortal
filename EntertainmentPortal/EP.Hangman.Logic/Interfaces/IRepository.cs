using System;
using System.Collections.Generic;
using System.Text;

namespace EP.Hagman.Logic.Interfaces
{
    public interface IRepository<T> where T : class
    {
        T Create(T item); //POST
        T Select(T item); //GET
        T Update(T item, string updatedLetter); //PUT
        T Delete(T item); //DELETE
    }
}
