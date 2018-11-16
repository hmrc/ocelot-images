using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using ImageAPI.Contexts;
using ImageAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ImageAPI.Controllers
{
    [Route("api/[controller]")]
    public class ImageController : Controller
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly Context _context;
        string result;
        public ImageController(Context context, IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }
        // GET api/values
        [HttpGet]
        public async Task<IActionResult> GetAllImages()
        {
            var images = await _context.Images.ToListAsync();
            return Ok(images);
        }

        [HttpGet, Route("SearchImageByID")]
        // to test on chrome  http://localhost:41446/api/image/SearchImageByID?id=1&id=2&id=41
        public async Task<IActionResult> GetImageByID(List<int> id)
        {
            var items = await _context.Images.Where(x => id.Contains(x.Id)).ToListAsync();
            if (items == null)
            {
                return NotFound();
            }
            return Ok(items);
        }

        [HttpGet, Route("SearchImageByPID")]
        // to test on chrome  http://localhost:41446/api/image/SearchImageByPID?pid=1234567
        public async Task<IActionResult> SearchImageByPID(int pid)
        {
            var items = await _context.Images.Where(x => x.UploadedByPID == pid).ToListAsync();
            if (items == null)
            {
                return NotFound();
            }
            return Ok(items);
        }

        [HttpGet, Route("SearchImageByPlaceholder")]
        // to test on chrome /api/image/SearchImageByPlaceholder?placeholder="hello"
        public async Task<IActionResult> GetImageByPlaceholder(string placeholder)
        {
          var result = await _context.Images.Where(x => x.Placeholder == placeholder).ToListAsync();

            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpGet, Route("ApproveImages")]
        // to test on chrome http://localhost:41446/api/image/ApproveImages?approved=true
        public async Task<IActionResult> GetAllApprovedImages(bool approved)
        {
            var userPid = int.Parse(User.Identity.Name.Substring(User.Identity.Name.IndexOf(@"\") + 1));

            var items = await _context.Images.Where(x => x.Approved == approved &&
                                                        x.UploadedByPID != userPid)
                                            .OrderBy(o => o.UploadedDate)
                                            .Take(5)
                                            .ToListAsync();
            if (items == null || !items.Any())
            {
                return NotFound();
            }
            return Ok(items);
        }

        [HttpPost, Route("UploadImage")]
        public async Task<IActionResult> UploadImages(string description, string placeholder, IFormFile files)
        {
            var userPid = User.Identity.Name.Substring(User.Identity.Name.IndexOf(@"\") + 1);
            var image = new Image();
            if (files == null)
            {
                return Content("file not selected");
            }
            result = description;
            // var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", files.GetFilename());
            var path = _hostingEnvironment.WebRootPath + @"\images\" + files.GetFilename();
            image.ImageName = files.GetFilename();
            image.Path = path.Substring(path.LastIndexOf(@"\") - 7);
            image.Description = description;
            image.UploadedDate = DateTime.Now;
            image.Placeholder = placeholder;
            image.UploadedByPID = Convert.ToInt32(userPid);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await files.CopyToAsync(stream);
            }
            _context.Add(image);
            await _context.SaveChangesAsync();
            return Ok("Model is working fine" + "\n" + path.Substring(52) + "\n" + result);            
        }

        // PUT api/values/5
        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateImageDetails(int id, [FromBody]ImagePatch item)
        {
            var userPid = User.Identity.Name.Substring(User.Identity.Name.IndexOf(@"\") + 1);            
            var result = _context.Images.FirstOrDefault(t => t.Id == id);
            if (result == null)
            {
                return NotFound();
            }
            //result.Description = item.Description;
            result.Approved = item.Approved;
            result.ImageName = item.ImageName;
            result.ApprovedByPID = Convert.ToInt32(userPid);
           // result.Placeholder = item.Placeholder;
            result.ApprovedDate = DateTime.Now;
            _context.Images.Update(result);
            await _context.SaveChangesAsync();
            return new NoContentResult();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteImage(int id)
        {
            var result = await _context.Images.FindAsync(id);
            if (result == null)
            {
                return NotFound();
            }
            var imageName = result.ImageName;
            //var dirPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\", "images" , imageName);
            var dirPath = _hostingEnvironment.WebRootPath + @"\images\" + imageName;
            var getfile = dirPath;
            if (System.IO.File.Exists(getfile))
            {
                System.IO.File.Delete(getfile);
            }
            _context.Images.Remove(result);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
