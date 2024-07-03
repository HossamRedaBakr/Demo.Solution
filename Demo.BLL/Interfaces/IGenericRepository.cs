using Demo.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Interfaces
{
    public interface IGenericRepository<T> 
    {
        Task<T> GetByIdAsync(int? id);
        Task AddAsync(T item);
        void Update(T item);

        void Delete(T item);

        Task<IEnumerable<T>> GetAllAsync();


    }
}
