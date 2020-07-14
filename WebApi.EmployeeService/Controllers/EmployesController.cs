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
using HttpDeleteAttribute = System.Web.Http.HttpDeleteAttribute;
using HttpGetAttribute = System.Web.Http.HttpGetAttribute;
using HttpPostAttribute = System.Web.Http.HttpPostAttribute;
using HttpPutAttribute = System.Web.Http.HttpPutAttribute;

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

                    return Request.CreateResponse(HttpStatusCode.Created, employees);
                }
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Hata Meydana Geldi Daha Sonra Tekrar Deneyiniz");
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
        public HttpResponseMessage Put([FromBody]EmployeeModel model)
        {
            try {
                var entity = context.Employees.FirstOrDefault(x => x.ID == model.ID);

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
            catch (Exception ex) {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }           
        }
    }
}
