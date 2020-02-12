using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication7.Repository
{
    interface IRepository<T>
    {
        IQueryable<T> GetElements { get; }
        void SaveProduct(T product);
        T DeleteProduct(int? Id);
    }
}
