using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StudenMVC.Models;

namespace StudenMVC.Controllers
{
    public class StudentsController : Controller
    {
        private readonly StudentDbContext _context;

        public StudentsController(StudentDbContext context)
        {
            _context = context;
        }

        // GET: Students
        public async Task<IActionResult> Index()
        {
            var studentDbContext = _context.Students.Include(s => s.Class);
            ViewBag.Classes = await GetClassesSelectList();
            return View(await studentDbContext.ToListAsync());
        }

        // POST: Students (Filter by Class)
        [HttpPost]
        public async Task<IActionResult> Index(IFormCollection form)
        {
            string cid = form["classID"].ToString();
            IQueryable<Student> students;

            if (!string.IsNullOrEmpty(cid))
            {
                students = _context.Students.Where(st => st.ClassId == cid);
            }
            else
            {
                students = _context.Students.Include(s => s.Class);
            }

            ViewBag.Classes = await GetClassesSelectList(cid);
            return View(await students.ToListAsync());
        }

        // Helper method to get classes SelectList
        private async Task<SelectList> GetClassesSelectList(string selectedClassId = null)
        {
            var classes = await _context.Classes.ToListAsync();
            return new SelectList(classes, "ClassId", "ClassName", selectedClassId);
        }

        // GET: Students/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var student = await _context.Students
                .Include(s => s.Class)
                .FirstOrDefaultAsync(m => m.StudentId == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // GET: Students/Create
        public async Task<IActionResult> Create()
        {
            ViewData["ClassId"] = await GetClassesSelectList();
            return View();
        }

        // POST: Students/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Student model, IFormFile uploadedImage)
        {
            if (ModelState.IsValid)
            {
                // Create a new student
                Student student = new Student
                {
                    StudentId = model.StudentId,
                    ClassId = model.ClassId,
                    Fullname = model.Fullname,
                    Address = model.Address,
                    Birthday = model.Birthday,
                    Image = uploadedImage != null ? model.StudentId + Path.GetExtension(uploadedImage.FileName) : null// Save the image with StudentId as the filename
                };

                // Handle image upload
                if (uploadedImage != null && uploadedImage.Length > 0)
                {
                    var filePath = Path.Combine("wwwroot/Images/" + student.Image);
                    using (var stream = System.IO.File.Create(filePath))
                    {
                        await uploadedImage.CopyToAsync(stream);
                    }
                }

                _context.Add(student);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["ClassId"] = await GetClassesSelectList(model.ClassId);
            return View(model);
        }

        // GET: Students/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            var studentData = new Student
            {
                StudentId = student.StudentId,
                ClassId = student.ClassId,
                Fullname = student.Fullname,
                Address = student.Address,
                Birthday = student.Birthday,
            };

            ViewData["ClassId"] = await GetClassesSelectList(student.ClassId);
            return View(student);
        }

        // POST: Students/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("StudentId,ClassId,Fullname,Address,Birthday,Image")] Student student, IFormFile uploadedImage)
        {
            Student studentData = new Student
            {
                StudentId = student.StudentId,
                ClassId = student.ClassId,
                Fullname = student.Fullname,
                Address = student.Address,
                Birthday = student.Birthday,
                Image = uploadedImage != null ?student.StudentId + Path.GetExtension(uploadedImage.FileName) : null// Save the image with StudentId as the filename
            };

            if (id != student.StudentId)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                // Handle image upload
                if (uploadedImage != null && uploadedImage.Length > 0)
                {
                    var filePath = Path.Combine("wwwroot/Images/" + student.Image);
                    using (var stream = System.IO.File.Create(filePath))
                    {
                        await uploadedImage.CopyToAsync(stream);
                    }
                }
                try
                {
                    if (uploadedImage != null && uploadedImage.Length > 0)
                    {
                        // Update the image if a new file is uploaded
                        var filePath = Path.Combine("wwwroot/Images/"+ student.Image);
                        using (var stream = System.IO.File.Create(filePath))
                        {
                            await uploadedImage.CopyToAsync(stream);
                        }
                    }

                    _context.Update(student);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentExists(student.StudentId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            ViewData["ClassId"] = await GetClassesSelectList(student.ClassId);
            return View(student);
        }

        // GET: Students/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var student = await _context.Students
                .Include(s => s.Class)
                .FirstOrDefaultAsync(m => m.StudentId == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student != null)
            {
                _context.Students.Remove(student);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool StudentExists(string id)
        {
            return _context.Students.Any(e => e.StudentId == id);
        }
    }
}
