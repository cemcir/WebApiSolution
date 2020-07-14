using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using WebApi.EmployeeService.Models;
using WebApi.Entities.Models;
using HttpGetAttribute = System.Web.Http.HttpGetAttribute;
using HttpPostAttribute = System.Web.Http.HttpPostAttribute;

namespace WebApi.EmployeeService.Controllers
{
    public class EmployesController : ApiController
    {
        EmployeeContext context = new EmployeeContext();
        
        [HttpGet]
        public IHttpActionResult Get() {
            try {
                var employees = context.Employees.ToList();
                if (employees.Count > 0) {
                    return Ok(new EmployeeListModel() {
                        Employees=employees
                    });
                }
                else {
                    return Ok(new EmployeeListModel() {
                        Employees = null
                    });
                }
            }
            catch (Exception) {
                return BadRequest("Bir Hata Meydana Geldi Lütfen Daha Sonra Tekrar Deneyiniz ");
            }
        }       
        
        [HttpGet]
        public IHttpActionResult Get(int Id)
        {
            try {           
                Employees employee = context.Employees.Where(x => x.ID == Id).FirstOrDefault();
                if (employee == null) {
                    return StatusCode(System.Net.HttpStatusCode.NotFound);
                    //return Ok(new EmployeeModel());
                }
                else {
                    return Ok(AutoMapper.Mapper.Map<EmployeeModel>(employee));
                }
            }
            catch (Exception) {
                return BadRequest("Bir Hata Meydana Geldi Lütfen Daha Sonra Tekrar Deneyiniz");
            }
        }
        
        [HttpPost]
        public IHttpActionResult Post([FromBody] EmployeeModel model) {
            try {
                if (ModelState.IsValid) {
                    Employees employees = AutoMapper.Mapper.Map<Employees>(model);
                    context.Employees.Add(employees);
                    context.SaveChanges();
                }
                return StatusCode(System.Net.HttpStatusCode.Created);
            }
            catch (Exception) {
                return BadRequest("Bir Hata Meydana Geldi Lütfen Daha Sonra Tekrar Deneyiniz");
            }
        }
    }
}
