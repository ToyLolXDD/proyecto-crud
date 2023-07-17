using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using System.Windows.Forms;

namespace CRUDTUtoriall.Controllers
{
    public class HomeController : Controller
    {
        MVCCRUDDBContext _context = new MVCCRUDDBContext();
        public ActionResult Index()
        {
            var listofData = _context.Student.ToList();
            return View(listofData);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost] 
        public ActionResult Create(Student model) 
        { 
            if (_context.Student.Any(s => s.StudentId == model.StudentId))
            {
                
                TempData["ErrorMessage"] = "El ID ya existe. Por favor, ingrese un ID único.";
                return View(model);
            }
            _context.Student.Add(model);
            _context.SaveChanges();
            TempData["SuccessMessage"] = "Data Insert Successfully";
            return View();
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var data = _context.Student.Where(x => x.StudentId == id).FirstOrDefault();
            return View(data);
        }
        [HttpPost]
        public ActionResult Edit(Student model) 
        {
            var data = _context.Student.Where(x => x.StudentId == model.StudentId).FirstOrDefault();
            if (data != null) 
            { 
                data.StudentCity = model.StudentCity;
                data.StudentName = model.StudentName;
                data.StudentFees = model.StudentFees;
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        public ActionResult Detail(int id)
        {
            var data = _context.Student.Where(x => x.StudentId == id).FirstOrDefault();
            return View(data);
        }
        public ActionResult Delete(int id)
        {
            var data = _context.Student.Where(x => x.StudentId == id).FirstOrDefault();
            _context.Student.Remove(data);
            _context.SaveChanges();
            ViewBag.Message = "Record Delete Successfully";
            return RedirectToAction("Index");
        }
    }
}