using System;
using System.Collections.Generic;
using System.Text;

namespace EP.Hagman.Data.Models
{
    public class TemporaryData<T> where T : class
    {
        public TemporaryData()
        {
            TempData = new List<T>();
        }
        public List<T> TempData { get; set; }
    }
}
