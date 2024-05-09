using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Firebase.Auth.Providers;
using Firebase.Auth;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.Elfie.Model.Strings;
using Microsoft.CodeAnalysis.Elfie.Serialization;
using Microsoft.EntityFrameworkCore;
using WebsiteQuanLySinhVien.Models;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;
using Firebase.Storage;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using System.Diagnostics;

namespace WebsiteQuanLySinhVien.Controllers
{
    public class SinhViensController : Controller
    {
        private readonly IHostingEnvironment _environment;
        // Configure Firebase
        private static string apiKey = "AIzaSyDpxildMkKaPBhkK3GstXmk_RhvAd_fbX0";
        private static string authDomain = "storagequanlysinhvien.firebaseapp.com";
        private static string Bucket = "storagequanlysinhvien.appspot.com";
        private static string AuthEmail = "nhatld1410@gmail.com";
        private static string AuthPassword = "nhat1410@";
        private readonly DbQuanLySinhVienContext _context;

        public SinhViensController(DbQuanLySinhVienContext context, IHostingEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        // GET: SinhViens
        public async Task<IActionResult> Index()
        {
            var dbQuanLySinhVienContext = _context.SinhViens.Include(s => s.LopSinhHoatNavigation);
            return View(await dbQuanLySinhVienContext.ToListAsync());
        }

        // GET: SinhViens/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sinhVien = await _context.SinhViens
                .Include(s => s.LopSinhHoatNavigation)
                .FirstOrDefaultAsync(m => m.MaSinhVien == id);
            if (sinhVien == null)
            {
                return NotFound();
            }

            return View(sinhVien);
        }

        // GET: SinhViens/Create
        public IActionResult Create()
        {
            ViewData["LopSinhHoat"] = new SelectList(_context.LopSinhHoats, "MaLop", "MaLop");
            return View();
        }

        // POST: SinhViens/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaSinhVien,HoTen,NgaySinh,Cccd,LopSinhHoat,MatKhau")] SinhVien sinhVien, IFormFile HinhAnhSinhVien, IFormFile HinhAnhTheSinhVien, IFormFile HinhAnhCccd)
        {
            if (ModelState.IsValid)
            {
                sinhVien.HinhAnhSinhVien = Upload(HinhAnhSinhVien).Result;
                sinhVien.HinhAnhTheSinhVien = Upload(HinhAnhTheSinhVien).Result;
                sinhVien.HinhAnhCccd = Upload(HinhAnhCccd).Result;
                _context.Add(sinhVien);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["LopSinhHoat"] = new SelectList(_context.LopSinhHoats, "MaLop", "MaLop", sinhVien.LopSinhHoat);
            return View(sinhVien);
        }

        public async Task<string> Upload(IFormFile file)
        {
            FileStream fs;
            string foldername = "firebaseFiles";
            string path = Path.Combine(_environment.WebRootPath, $"images/{foldername}");
            if (Directory.Exists(path))
            {
                using (fs = new FileStream(Path.Combine(path, file.FileName), FileMode.Create))
                {
                    await file.CopyToAsync(fs);
                }
            }
            else
            {
                Directory.CreateDirectory(path);
            }
            fs = new FileStream(Path.Combine(path, file.FileName), FileMode.Open);
            //Firebase uploading stuffs
            FirebaseAuthConfig config = new FirebaseAuthConfig();
            config.ApiKey = apiKey;
            config.AuthDomain = authDomain;
            config.Providers = new FirebaseAuthProvider[] { new GoogleProvider().AddScopes("email"), new EmailProvider() };
            var auth = new FirebaseAuthClient(config);
            var a = await auth.SignInWithEmailAndPasswordAsync(AuthEmail, AuthPassword);
            // Cancellation token
            var canellation = new CancellationTokenSource();

            var task = new FirebaseStorage(Bucket)
                .Child("assets")
                .Child($"{file.FileName}")
                .PutAsync(fs, canellation.Token);
            return await task;
        }


        // GET: SinhViens/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sinhVien = await _context.SinhViens.FindAsync(id);
            if (sinhVien == null)
            {
                return NotFound();
            }
            ViewData["LopSinhHoat"] = new SelectList(_context.LopSinhHoats, "MaLop", "MaLop", sinhVien.LopSinhHoat);
            return View(sinhVien);
        }

        // POST: SinhViens/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaSinhVien,HoTen,NgaySinh,Cccd,LopSinhHoat,MatKhau")] SinhVien sinhVien, IFormFile HinhAnhSinhVien, IFormFile HinhAnhTheSinhVien, IFormFile HinhAnhCccd)
        {
            if (id != sinhVien.MaSinhVien)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sinhVien);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SinhVienExists(sinhVien.MaSinhVien))
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
            ViewData["LopSinhHoat"] = new SelectList(_context.LopSinhHoats, "MaLop", "MaLop", sinhVien.LopSinhHoat);
            return View(sinhVien);
        }

        // GET: SinhViens/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sinhVien = await _context.SinhViens
                .Include(s => s.LopSinhHoatNavigation)
                .FirstOrDefaultAsync(m => m.MaSinhVien == id);
            if (sinhVien == null)
            {
                return NotFound();
            }

            return View(sinhVien);
        }

        // POST: SinhViens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sinhVien = await _context.SinhViens.FindAsync(id);
            if (sinhVien != null)
            {
                _context.SinhViens.Remove(sinhVien);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SinhVienExists(int id)
        {
            return _context.SinhViens.Any(e => e.MaSinhVien == id);
        }


        [HttpPost]
        public async Task<IActionResult> Search(string searchString)
        {
            var listHocSinh = _context.SinhViens.Where(a => a.HoTen.Contains(searchString)).Include(a => a.LopSinhHoatNavigation);
            //foreach(var x in listHocSinh)
            //{
            //    Debug.WriteLine(x);
            //}
            //return RedirectToAction(nameof(Index));
            return View("Index", await listHocSinh.ToListAsync());
        }
    }
}
