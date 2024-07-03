﻿using Demo.DAL.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;
using Microsoft.AspNetCore.Http;

namespace Demo.PL.ViewModels
{
    public class EmployeeViewModel
    {


        public int Id { get; set; }

        public IFormFile  Image { get; set; }
        public string ImageName { get; set; }

        [Required(ErrorMessage = "Name Is Required")]
        [MaxLength(50, ErrorMessage = "Max Length Is 50 Chars")]
        [MinLength(5, ErrorMessage = "Min Length Is 5 Chars")]
        public string Name { get; set; }

        [Range(22, 39, ErrorMessage = "Age must Be In Range 22,39")]
        public int? Age { get; set; }

        [RegularExpression("^[0-9]{1,3}-[a-z]{5,10}-[a-zA-Z]{4,10}-[a-zA-z]{5,10}$", ErrorMessage = "Address Must Be Like 123-Street-City-Country")]
        public string Address { get; set; }

        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }


        public bool IsActve { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [Phone]
        public string PhoneNumber { get; set; }


        public DateTime HireDate { get; set; }


        public Department Department { get; set; }

        [ForeignKey(nameof(Department))]
        public int? DeptId { get; set; }

    }
}
