using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebApi.Entities.Models;

namespace WebApi.EmployeeService.Models
{
    public class EmployeeModel
    {
        public int ID { get; set; }

        [StringLength(50),Required]
        public string FirstName { get; set; }

        [StringLength(50),Required]
        public string LastName { get; set; }

        [StringLength(50),Required]
        public string Gender { get; set; }

        [Required]
        public int? Salary { get; set; }

    }
}