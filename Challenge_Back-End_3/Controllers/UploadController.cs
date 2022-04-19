using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Challenge_Back_End_3.Controllers
{
    public class UploadController : Controller
    {
        private readonly string wwwrootDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");

        public IActionResult Index()
        {
            List<string> arquivo = Directory.GetFiles(wwwrootDirectory, "*.txt").Select(Path.GetFileName).ToList();
            return View(arquivo);
        }

        [HttpPost]
        public async Task<IActionResult> IndexAsync(IFormFile myFile)
        {
            if (myFile != null)
            {
                var path = Path.Combine(wwwrootDirectory, myFile.FileName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await myFile.CopyToAsync(stream);
                }
                Console.WriteLine("Nome do arquivo: " + myFile.FileName + " Tamanho do arquivo: " + myFile.Length/1024/1024 + "MB");

                var reader = new StreamReader(path);
                while (!reader.EndOfStream)
                {
                    Console.WriteLine(reader.ReadLine());
                }
            }

            return View();
        }
    }
}
