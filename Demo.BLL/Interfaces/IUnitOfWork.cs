using Demo.BLL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Interfaces
{
    public interface IUnitOfWork 
    {
        public DepartmentRepository  DepartmentRepository { get; set; }

        public EmployeeRepository EmployeeRepository { get; set; }


        Task<int> CompleteAsync();





    }
}
