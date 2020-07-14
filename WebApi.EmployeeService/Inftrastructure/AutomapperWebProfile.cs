using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApi.EmployeeService.Models;
using WebApi.Entities.Models;

namespace WebApi.EmployeeService.Inftrastructure
{
    public class AutomapperWebProfile:AutoMapper.Profile
    {
        public AutomapperWebProfile()
        {
            CreateMap<Employees, EmployeeModel>();
            CreateMap<EmployeeModel, Employees>();
        }

        public static void Run()
        {
            AutoMapper.Mapper.Initialize(a=>{
                a.AddProfile<AutomapperWebProfile>();
            });
        }
    }
}