using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApi.Entities.Models;

namespace WebApi.EmployeeService.Models
{
    public class EmployeeListModel
    {
        public List<Employees> Employees { get; set; }

        public int Count = 0;
    }
}