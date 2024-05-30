﻿using Demo.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Interfaces
{
    public interface IGenericRepository<T> 
    {
        T GetById(int? id);
        int Add(T item);
        int Update(T item);

        int Delete(T item);

        IEnumerable<T> GetAll();

    }
}