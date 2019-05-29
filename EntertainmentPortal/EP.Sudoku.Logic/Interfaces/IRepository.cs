using System;
using System.Collections.Generic;
using System.Text;

namespace EP.Sudoku.Logic.Interfaces
{
    public interface IRepository<T> where T : class
    {
        T Create(T item); //POST
        T Read(T item); //GET
        T Update(T item); //PUT
        T Delete(T item); //DELETE
    }

}
