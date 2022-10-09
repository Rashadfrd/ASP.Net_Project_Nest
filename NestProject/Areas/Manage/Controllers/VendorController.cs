using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NestProject.DAL;
using NestProject.Models;
using NestProject.Utilities.Constants;
using NestProject.Utilities.Extensions;

namespace NestProject.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class VendorController : Controller
    {
        NestContext _context { get; }

        public VendorController(NestContext context)
        {
            _context = context;
        }
        // GET: VendorController
        public ActionResult Index()
        {
            var vendors = _context.Partners.Include(x => x.Products);
            return View(vendors);
        }

        // GET: VendorController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: VendorController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: VendorController/Create
        [HttpPost]
        public ActionResult Create(Partner partner)
        {
            if (partner.ImageFileProfile is null ) ModelState.AddModelError("ImageFileProfile", "Zehmet olmasa sekil elave ele qardasim");
            var fileprofile = partner.ImageFileProfile;
            if (!fileprofile.CheckFileExtension("image/"))
            {
                ModelState.AddModelError("ImageFileProfile", "Yüklədiyiniz fayl şəkil deyil");
                return View();
            }
            if (fileprofile.CheckFileSize(2))
            {
                ModelState.AddModelError("ImageFileProfile", "Yüklədiyiniz fayl 2mb-dan artıq olmamalıdır");
                return View();
            }
            string newFileName = Guid.NewGuid().ToString();
            newFileName += fileprofile.CutFileName(60);
            fileprofile.SaveFile(Path.Combine("imgs", "vendor", newFileName));
            partner.ProfileImageUrl = newFileName;
            var backImg = partner.ImageFileBg;
            partner.BgImageUrl = "vendor-header-bg.png";
            if (backImg != null)
            {
                if (!backImg.CheckFileExtension("image/"))
                {
                    ModelState.AddModelError("ImageFileBg", "Yüklədiyiniz fayl şəkil deyil");
                    return View();
                }
                if (backImg.CheckFileSize(1))
                {
                    ModelState.AddModelError("ImageFileBg", "Yüklədiyiniz şəkil 2mb-dan artıq olmamalıdır");
                    return View();
                }
                string newBackImgName = Guid.NewGuid() + backImg.CutFileName();
                backImg.SaveFile(Path.Combine("imgs", "vendor", newBackImgName));
                partner.BgImageUrl = newBackImgName;
            }
            partner.IsDeleted = false;
            _context.Partners.Add(partner);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        // GET: VendorController/Edit/5
        public ActionResult Update(int? id)
        {
            if (id is null) return BadRequest();
            var partner = _context.Partners.Find(id);
            if (partner is null) return NotFound();
            return View(partner);
        }

        // POST: VendorController/Edit/5
        [HttpPost]
        public ActionResult Update(int? id, Partner partner)
        {
            if (partner.ImageFileProfile is null) ModelState.AddModelError("ImageFileProfile", "Zehmet olmasa sekil elave edin");
            if (!ModelState.IsValid) return View();
            if (id is null || id != partner.Id) return BadRequest();
            var part = _context.Partners.Find(id);
            var file = partner.ImageFileProfile;
            if (!file.CheckFileExtension("image/"))
            {
                ModelState.AddModelError("ImageFileProfile", "Yüklədiyiniz fayl şəkil deyil");
                return View();
            }
            if (file.CheckFileSize(2))
            {
                ModelState.AddModelError("ImageFileProfile", "Yüklədiyiniz fayl 2mb-dan artıq olmamalıdır");
                return View();
            }
            string newFileName = Guid.NewGuid().ToString();
            newFileName += file.CutFileName(60);
            RemoveFile(Path.Combine("imgs", part.ProfileImageUrl));
            file.SaveFile(Path.Combine("imgs", "vendor", newFileName));
            part.ProfileImageUrl = newFileName;

            var filebg = partner.ImageFileBg;
            if (!filebg.CheckFileExtension("image/"))
            {
                ModelState.AddModelError("ImageFileBg", "Yüklədiyiniz fayl şəkil deyil");
                return View();
            }
            if (filebg.CheckFileSize(2))
            {
                ModelState.AddModelError("ImageFileBg", "Yüklədiyiniz fayl 2mb-dan artıq olmamalıdır");
                return View();
            }
            string newFileNamebg = Guid.NewGuid().ToString();
            newFileNamebg += filebg.CutFileName(60);
            RemoveFile(Path.Combine("imgs", part.BgImageUrl));
            filebg.SaveFile(Path.Combine("imgs", "vendor", newFileNamebg));
            part.BgImageUrl = newFileNamebg;
            part.Name = partner.Name;
            part.Description = partner.Description;
            part.Number = partner.Number;
            part.Address = partner.Address;
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }


        public ActionResult Delete(int? id)
        {
            if (id is null) return BadRequest();
            var partner = _context.Partners.Include(x => x.Products).FirstOrDefault(x => x.Id == id);
            if (partner is null) return NotFound();
            if (partner.Products.Count > 0) return BadRequest("Bu vendora aid productlar movcuddur");
            if (partner.IsDeleted == false)
            {
                partner.IsDeleted = true;
            }
            else
            {
                RemoveFile(Path.Combine("vendor", partner.ProfileImageUrl));
                RemoveFile(Path.Combine("vendor", partner.BgImageUrl));
                _context.Partners.Remove(partner);
            }
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public static void RemoveFile(string path)
        {
            path = Path.Combine(Constants.RootPath, "img", path);
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
        }
    }
}
