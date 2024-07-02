using Demo.BLL.Interfaces;
using Demo.DAL.Contexts;
using Demo.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Repositories
{
    public class EmployeeRepository :GenericRepository<Employee>, IEmployeeRepository
    {
        private readonly MVCAppDbContext _dbContext;

        public EmployeeRepository(MVCAppDbContext dbContext):base(dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<Employee> GetEmplyeesByAdress(string adress)
            =>  _dbContext.Employees.Where(E => E.Address.ToLower() == adress.ToLower());

        public IQueryable<Employee> GetEmplyeesByEmployeeName(string SearchName)
        => _dbContext.Employees.Where(e=>e.Name.ToLower().Contains(SearchName.ToLower())).Include(e=>e.Department);
    }
}
