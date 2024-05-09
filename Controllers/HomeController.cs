using Firebase.Auth;
using Firebase.Auth.Providers;
using Firebase.Storage;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.CodeAnalysis;
using System.Diagnostics;
using WebsiteQuanLySinhVien.Models;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;
using Microsoft.Build.Evaluation;
using NuGet.Protocol;

namespace WebsiteQuanLySinhVien.Controllers
{
    public class HomeController : Controller
    {
  //      const firebaseConfig = {
  //  apiKey: "AIzaSyDpxildMkKaPBhkK3GstXmk_RhvAd_fbX0",
  //  authDomain: ,
  //  projectId: "storagequanlysinhvien",
  //  storageBucket: "storagequanlysinhvien.appspot.com",
  //  messagingSenderId: "362954108458",
  //  appId: "1:362954108458:web:28b39560f6a11c139d9d53"
  //};
        private readonly IHostingEnvironment _environment;
        // Configure Firebase
        private static string apiKey = "AIzaSyDpxildMkKaPBhkK3GstXmk_RhvAd_fbX0";
        private static string authDomain = "storagequanlysinhvien.firebaseapp.com";
        private static string Bucket = "storagequanlysinhvien.appspot.com";
        private static string AuthEmail = "nhatld1410@gmail.com";
        private static string AuthPassword = "nhat1410@";

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, IHostingEnvironment environment)
        {
            _logger = logger;
            _environment = environment;
        }

        public IActionResult Index()
        {
            return View();
        }
  


        [HttpPost]
        public async Task<IActionResult> Index(IFormFile file)
        {
                //var fileupload = file
            FileStream fs;
            if (file.Length > 0)
            {
                //Upload file to firebase
                string foldername = "firebaseFiles";
                string path = Path.Combine(_environment.WebRootPath, $"images/{foldername}");
                if(Directory.Exists(path))
                {
                    using(fs = new FileStream(Path.Combine(path, file.FileName), FileMode.Create))
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
                config.Providers = new FirebaseAuthProvider[] {new GoogleProvider().AddScopes("email"),new EmailProvider()};
                var auth = new FirebaseAuthClient(config);
                var a = auth.SignInWithEmailAndPasswordAsync(AuthEmail, AuthPassword);

                // Cancellation token
                var canellation = new CancellationTokenSource();

                var task = new FirebaseStorage(
                    Bucket,
                    new FirebaseStorageOptions
                    {
                        AuthTokenAsyncFactory = () => Task.FromResult(a.Result.ToString()),
                        ThrowOnCancel = true
                    })
                    .Child("assets")
                    .Child($"{file.FileName}")
                    .PutAsync(fs, canellation.Token);

                string link = await task;
                try
                {
                    //ViewBag.link = await upload;
                    return Ok();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"*******{ex}*********");
                    throw;
                }
            }
            return BadRequest();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
