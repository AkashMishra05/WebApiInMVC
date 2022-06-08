using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using WebApi.Models;

namespace WebApi.Controllers
{
    public class EmployeeController : ApiController
    {
        private DBModel db = new DBModel();

        // GET: api/Employee
        public IQueryable<Employee> GetEmployees()
        {
            return db.Employees.Where(x => x.IsDeleted == true);
        }

        // GET: api/Employee/5
        [ResponseType(typeof(Employee))]
        public IHttpActionResult GetEmployee(int id)
        {
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return NotFound();
            }
            return Ok(employee);
        }

        // PUT: api/Employee/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutEmployee(int id, Employee employee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != employee.EmployeeID)
            {
                return BadRequest();
            }
            employee.IsDeleted = true;
            employee.ModifedOn = DateTime.Now;
            employee.ModifiedBy = 1;
            db.Entry(employee).State = System.Data.Entity.EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Employee
        [ResponseType(typeof(Employee))]
        public IHttpActionResult PostEmployee(Employee employee)
        {
            if (ModelState.IsValid)
            {
                if (db.Employees.Any(ac => ac.Name.Equals(employee.Name)))
                {

                    return StatusCode(HttpStatusCode.Conflict);
                }
                else
                {
                    db.SaveChanges();
                }
            }
            employee.IsDeleted = true;
            employee.CreatedOn = DateTime.Now;
            employee.CreatedBy = 1;
            db.Employees.Add(employee);
            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (EmployeeExists(employee.EmployeeID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = employee.EmployeeID }, employee);
        }
        // PUT: api/Employee/5
        [ResponseType(typeof(Employee))]
        public IHttpActionResult DeleteEmployee(int id)
        {
            Employee _employee = db.Employees.Find(id);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //if (id != employee.EmployeeID)
            //{
            //    return BadRequest();
            //}
            _employee.IsDeleted = false;
            _employee.ModifedOn = DateTime.Now;
            _employee.ModifiedBy = 1;
            db.Entry(_employee).State = System.Data.Entity.EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }


        //DELETE: api/Employee/5
        //[ResponseType(typeof(Employee))]
        //public IHttpActionResult DeleteEmployee(int id)
        //{
        //    Employee employee = db.Employees.Find(id);
        //    if (employee == null)
        //    {
        //        return NotFound();
        //    }

        //    db.Employees.Remove(employee);
        //    db.SaveChanges();

        //    return Ok(employee);
        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EmployeeExists(int id)
        {
            return db.Employees.Count(e => e.EmployeeID == id) > 0;
        }
    }
}