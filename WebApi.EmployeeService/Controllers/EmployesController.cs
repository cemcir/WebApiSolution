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
using ActionNameAttribute = System.Web.Http.ActionNameAttribute;
using HttpDeleteAttribute = System.Web.Http.HttpDeleteAttribute;
using HttpGetAttribute = System.Web.Http.HttpGetAttribute;
using HttpPostAttribute = System.Web.Http.HttpPostAttribute;
using HttpPutAttribute = System.Web.Http.HttpPutAttribute;
using RouteAttribute = System.Web.Http.RouteAttribute;

namespace WebApi.EmployeeService.Controllers
{
    public class EmployesController : ApiController
    {
        EmployeeContext context = new EmployeeContext();
        /*
        [HttpGet]
        public HttpResponseMessage GetByGender(string gender = "All")
        {
            var model = new EmployeeListModel();
            string employeesGender = gender.ToLower();
            List<Employees> employees;
            try {
                if (employeesGender == "all") {
                    employees = context.Employees.ToList();
                    if (employees.Count > 0) {
                        return Request.CreateResponse(HttpStatusCode.OK, model.Employees=employees); 
                    }
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Kayıtlı Veri Bulunamadı");
                }
                else {
                    employees = context.Employees.Where(x => x.Gender.ToLower() == employeesGender).ToList();
                    if (employees.Count>0) {
                        return Request.CreateResponse(HttpStatusCode.OK,model.Employees=employees);
                    }
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Gender için değer All ,Male veya Female olmalı");
                }
            }
            catch (Exception ex) {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
        */
        [HttpGet]
        public HttpResponseMessage LoadAllEmployees() {
            try {
                var employees = context.Employees.ToList();
                if (employees.Count > 0) {
                    return Request.CreateResponse(HttpStatusCode.OK,employees);
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
        public HttpResponseMessage LoadAllEmployeesById(int Id)
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
        public HttpResponseMessage Post([FromBody]EmployeeModel model) {
            try {
                Employees employees = AutoMapper.Mapper.Map<Employees>(model);
                if (ModelState.IsValid) {
                    context.Employees.Add(employees);
                    context.SaveChanges();

                    return Request.CreateResponse(HttpStatusCode.Created, employees);
                }
                else {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest,"");
                }
            }
            catch (Exception ex) {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest,ex);
            }
        }

        [HttpDelete]
        public HttpResponseMessage Delete(int Id)
        {
            try {
                var entity = context.Employees.FirstOrDefault(x=>x.ID==Id);
                if (entity == null) {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Böyle Bir Kişi Bulunamadı");
                }
                else {
                    context.Employees.Remove(entity);
                    context.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.OK);
                }
            }
            catch (Exception ex) {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest,ex);
            }
        }

        [HttpPut]
        public HttpResponseMessage Put([FromBody]int Id,[FromUri]EmployeeModel model)
        {
            try {
                var entity = context.Employees.FirstOrDefault(x => x.ID == Id);
                if (ModelState.IsValid) {
                    if (entity == null) {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Böyle Bir Kişi Bulunamadı");
                    }
                    else {
                        entity.FirstName = model.FirstName;
                        entity.LastName = model.LastName;
                        entity.Salary = model.Salary;
                        entity.Gender = model.Gender;
                        context.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK, entity);
                    }
                }
                else {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest,"");
                }
            }
            catch (Exception ex) {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }           
        }
    }
}
