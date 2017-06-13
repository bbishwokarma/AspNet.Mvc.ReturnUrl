using System.Linq;
using System.Net;
using System.Web.Mvc;
using ReturnUrlTest.DataAccess;
using ReturnUrlTest.Models;
using ReturnUrlTest.ViewModels;
using AspNet.Mvc.RedirectAssist;

namespace ReturnUrlTest.Controllers
{
    public class StudentsController : Controller
    {
        private StudentDataAccess db = new StudentDataAccess();

        [ReturnsUsingParameter(Constants.ReturnsToStudents)]
        public ActionResult Index(StudentsViewModel model)
        {
            if (model == null)
            {
                model = new StudentsViewModel();
            }
            if (string.IsNullOrEmpty(model.SearchName))
            {
                model.Students = db.Students;
            }
            else
            {
                model.SearchName = model.SearchName.ToLower().Trim();
                model.Students = db.Students.Where(s => s.FirstName.ToLower().Contains(model.SearchName) || s.LastName.ToLower().Contains(model.SearchName)).ToList();
            }
            return View(model);
        }

        [RedirectsBackUsingParameter(Constants.ReturnsToStudents)]
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.FirstOrDefault(s => s.StudentId == id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        [RedirectsBackUsingParameter(Constants.ReturnsToStudents)]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Student student)
        {
            if (ModelState.IsValid)
            {
                db.Students.Add(student);
                return RedirectToAction("Index");
            }
            return View(student);
        }

        [RedirectsBackUsingParameter(Constants.ReturnsToStudents)]
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.FirstOrDefault(s => s.StudentId == id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Student student)
        {
            if (ModelState.IsValid)
            {
                var existing = db.Students.FirstOrDefault(s => s.StudentId == student.StudentId);
                if (existing != null)
                {
                    db.Students.Remove(existing);
                }
                db.Students.Add(student);
                return RedirectToAction("Index");
            }
            return View(student);
        }

        [RedirectsBackUsingParameter(Constants.ReturnsToStudents)]
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.FirstOrDefault(s => s.StudentId == id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Student student = db.Students.FirstOrDefault(s => s.StudentId == id);
            db.Students.Remove(student);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
