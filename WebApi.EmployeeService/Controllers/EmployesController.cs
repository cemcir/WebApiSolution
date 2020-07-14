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
        public HttpResponseMessage Get() {
            try {
                var employees = context.Employees.ToList();
                if (employees.Count > 0) {
                    return Request.CreateResponse(HttpStatusCode.OK, new EmployeeListModel() {
                        Employees=employees
                    });
                }
                else {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Kayıtlı Veri Bulunamadı");
                }
            }
            catch (Exception ex) {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }       
        
        [HttpGet]
        public HttpResponseMessage Get(int Id)
        {
            try {           
                Employees employee = context.Employees.Where(x => x.ID == Id).FirstOrDefault();
                if (employee !=null) {
                    var entity=AutoMapper.Mapper.Map<EmployeeModel>(employee);
                    return Request.CreateResponse(HttpStatusCode.OK,entity);
                }
                else {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Böyle Bir Kişi Mevcut Değil");
                }
            }
            catch (Exception ex) {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest,ex);
            }
        }
        
        [HttpPost]
        public HttpResponseMessage Post([FromBody] EmployeeModel model) {
            try {
                Employees employees = AutoMapper.Mapper.Map<Employees>(model);
                if (ModelState.IsValid) {
                    context.Employees.Add(employees);
                    context.SaveChanges();
                }
                return Request.CreateResponse(HttpStatusCode.Created, employees);   
            }
            catch (Exception ex) {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest,ex);
            }
        }


    }
}
