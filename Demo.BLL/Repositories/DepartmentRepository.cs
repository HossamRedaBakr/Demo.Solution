﻿using Demo.BLL.Interfaces;
using Demo.DAL.Contexts;
using Demo.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly MVCAppDbContext _dbContext;

        public DepartmentRepository(MVCAppDbContext dbContext)  //Ask CLR To Creat Object From DbContext
        {
            _dbContext = dbContext;
        }

        public int Add(Department department)
        {
          _dbContext.Add(department);
          return _dbContext.SaveChanges();
        }

        public int Delete(Department department)
        {
            _dbContext.Remove(department);
                return _dbContext.SaveChanges();
        }

        public IEnumerable<Department> GetAll()
        => _dbContext.Departments.ToList();
          

        public Department GetById(int? id)
        {

            return _dbContext.Departments.Find(id); //Search local if not ecxisted search in database
        }

        public int Update(Department department)
        {
            _dbContext.Update(department);
            return _dbContext.SaveChanges();
        }
    }
}
