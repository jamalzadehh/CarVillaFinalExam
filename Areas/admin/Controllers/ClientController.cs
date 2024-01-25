using Carvilla.DAL;
using Carvilla.Models;
using Carvilla.ViewModels.Client;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Carvilla.Areas.admin.Controllers
{
    [Area("admin")]
    //[Authorize(Roles ="admin")]
    //[AutoValidateAntiforgeryToken]
    public class ClientController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        public ClientController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public IActionResult Index()
        {
            var clients = _context.Clients.ToList();

            return View(clients);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(ClientCreateVM vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            var existed = await _context.Clients.AnyAsync(x => x.FullName.ToLower().Contains(vm.FullName.ToLower()));
            if (!existed)
            {
                ModelState.AddModelError("FullName", "Bu adda client movcuddur");
            }
            if (!vm.Image.ContentType.Contains("image"))
            {
                ModelState.AddModelError("image", "File tipi yanlisdir");
            }
            if (vm.Image.Length < 4 * 1024 * 1024)
            {
                ModelState.AddModelError("image", "Seklin olcusu 4 mb dan az olmalidir");
            }
            string filename = Guid.NewGuid() + vm.Image.FileName;
            string path = Path.Combine(_env.WebRootPath, "images", filename);
            using (FileStream stream = new(path, FileMode.Create))
            {
                await vm.Image.CopyToAsync(stream);
            }
            Clients clients = new()
            {
                FullName = vm.FullName,
                Description = vm.Description,
                City = vm.City,
                ImageUrl = filename
            };

            await _context.Clients.AddAsync(clients);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            if (id <= 0) return BadRequest();
            var existed = await _context.Clients.FirstOrDefaultAsync(x => x.Id == id);
            if (existed == null) return NotFound();
            ClientUpdateVM vm = new()
            {
                Id = id,
                FullName = existed.FullName,
                Description = existed.Description,
                City = existed.City,
                ImageUrl = existed.ImageUrl,
            };
            return View(vm);
        }
        [HttpPost]
        public async Task<IActionResult> Update(ClientUpdateVM vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            var existed = await _context.Clients.FirstOrDefaultAsync(x => x.Id == vm.Id);
            if (existed == null) return NotFound();
            var isExisted = await _context.Clients.AnyAsync(x => x.FullName.ToLower().Contains(existed.FullName.ToLower()) && x.Id != vm.Id);
            if (isExisted)
            {
                ModelState.AddModelError("FullName", "Bu adda client movcuddur");
            }
            if (vm.Image != null)
            {
                string filename = Guid.NewGuid() + vm.Image.FileName;
                string path = Path.Combine(_env.WebRootPath, "images");
                if (System.IO.File.Exists(path + "/" + existed.ImageUrl))
                {
                    System.IO.File.Delete(path + "/" + existed.ImageUrl);
                }
                using (FileStream stream = new(path + "/" + filename, FileMode.Create))
                {
                    await vm.Image.CopyToAsync(stream);
                }
                existed.ImageUrl = filename;
            }
            
            existed.FullName = vm.FullName;
            existed.Description = vm.Description;
            existed.City = vm.City;

            _context.Clients.Update(existed);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0) return BadRequest();
            var existed = await _context.Clients.FirstOrDefaultAsync(x => x.Id == id);
            if (existed == null) return NotFound();
            string path = Path.Combine(_env.WebRootPath, "assets/img", existed.ImageUrl);
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
            _context.Clients.Remove(existed);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
