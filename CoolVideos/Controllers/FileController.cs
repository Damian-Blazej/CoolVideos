﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoolVideos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        [Route("image")]
        [HttpPost]
        [DisableRequestSizeLimit]
        public IActionResult UploadImage()
        {
            return UploadFile("image");
        }

        [Route("video")]
        [HttpPost]
        [DisableRequestSizeLimit]
        public IActionResult UploadVideo()
        {
            return UploadFile("video");
        }

        private IActionResult UploadFile(string type)
        {
            string resourcesSubFolder;

            switch (type) {
                case "image":
                    resourcesSubFolder = "Images";
                    break;
                case "video":
                    resourcesSubFolder = "Videos";
                    break;
                default:
                    return BadRequest();
            }

            try
            {
                var file = Request.Form.Files[0];
                var folderName = Path.Combine("Resources", resourcesSubFolder);
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

                if (file.Length > 0)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    var fullPath = Path.Combine(pathToSave, fileName);
                    var dbPath = Path.Combine(folderName, fileName);

                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }

                    return Ok(new { dbPath });
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }
    }
}