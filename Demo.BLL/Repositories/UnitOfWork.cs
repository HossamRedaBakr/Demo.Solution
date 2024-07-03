using Demo.BLL.Interfaces;
using Demo.DAL.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Repositories
{
    public class UnitOfWork : IUnitOfWork ,IDisposable
    {
        private readonly DbContext _dbContext;

        public DepartmentRepository DepartmentRepository { get; set; }
        public EmployeeRepository EmployeeRepository { get; set; }

        public UnitOfWork(MVCAppDbContext dbContext)
        {
            _dbContext = dbContext;

             EmployeeRepository = new EmployeeRepository(dbContext);
             DepartmentRepository = new DepartmentRepository(dbContext);
        }

        public async Task<int> CompleteAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
