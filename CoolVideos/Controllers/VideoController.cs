using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CoolVideos.Helpers;
using CoolVideos.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Net.Http.Headers;
using Microsoft.VisualBasic.CompilerServices;

namespace CoolVideos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VideoController : ControllerBase
    {
        private readonly CoolVideosContext _context;

        public VideoController(CoolVideosContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Video>>> GetVideos()
        {
            var videos = await _context.Videos.Include(v => v.Category).Include(v => v.User).ToListAsync();

            foreach(Video video in videos)
            {
                video.User.toSmallUser();
                includeLikes(video);
            }

            return videos;
        }

        [HttpGet]
        [Route("latest")]
        public async Task<ActionResult<IEnumerable<Video>>> GetVideoLatest([FromQuery] int n)
        {
            var videos = await _context.Videos.OrderByDescending(v => v.Id).Take(n).Include(v => v.User).Include(v => v.Category).ToListAsync();

            if (videos == null)
                return NotFound();

            foreach (Video video in videos)
            {
                video.User.toSmallUser();
                includeLikes(video);
            }

            return videos;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Video>> GetVideo(int id)
        {
            var video = await _context.Videos.Include(v => v.Category).Include(v => v.User).FirstOrDefaultAsync(v => v.Id == id);

            if (video == null)
            {
                return NotFound();
            }

            includeLikes(video);
            video.User.toSmallUser();

            return video;
        }

        [HttpGet]
        [Route("search")]
        public async Task<ActionResult<VideoListWithCount>> GetVideoByQuery([FromQuery] string query, [FromQuery] int categoryID, [FromQuery] int userID, [FromQuery] PaginationParameters paginationParameters)
        {
            IQueryable<Video> videos = _context.Videos;

            if (categoryID < 0)
                return BadRequest();
            else if (categoryID != 0)
                videos = videos.Where(v => v.CategoryId == categoryID);

            if (userID < 0)
                return BadRequest();
            else if (userID != 0)
                videos = videos.Where(v => v.UserId == userID);

            if (!string.IsNullOrEmpty(query))
            {
                videos = videos.Where(v => v.Title.ToLower().Contains(query)
                        || v.Description.ToLower().Contains(query)
                        || (v.User.FirstName.ToLower() + " " + v.User.LastName.ToLower()).Contains(query));
            }

            if (videos == null)
            {
                return NotFound();
            }

            int videosTotalCount = videos.Count();
            List<Video> videosList = await videos.Skip((paginationParameters.PageNumber - 1) * paginationParameters.PageSize)
                .Take(paginationParameters.PageSize)
                .Include(v => v.Category)
                .Include(v => v.User)
                .ToListAsync();

            foreach (Video video in videosList)
            {
                video.User.toSmallUser();
                includeLikes(video);
            }

            return new VideoListWithCount(videosList, videosTotalCount);
        }

        [HttpGet]
        [Route("category/{categoryId:int}")]
        public async Task<ActionResult<IEnumerable<Video>>> GetVideosByCategory(int categoryId, [FromQuery]int n)
        {
            var videos = await _context.Videos
                .Where(v => v.CategoryId == categoryId)
                .Take(n)
                .Include(v => v.Category)
                .Include(v => v.User)
                .ToListAsync();

            if (videos == null)
                return NotFound();

            foreach (Video video in videos)
            {
                video.User.toSmallUser();
                includeLikes(video);
            }

            return videos;
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Video>> PostVideo(Video video)
        {
            try
            {
                _context.Videos.Add(video);
            }
            catch
            {
                return BadRequest();
            }

            try
            {
                await _context.SaveChangesAsync();
                video.Uri = string.Concat(video.Id, ".mp4");
                video.Image = string.Concat(video.Id, ".jpg");
                _context.Videos.Update(video);
                await _context.SaveChangesAsync();
            }
            catch
            {
                return StatusCode(503);
            }

            return Ok(video);
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVideo(int id, Video video)
        {
            if (id != video.Id)
            {
                return BadRequest();
            }

            if (!isUserVideoOwner(video))
            {
                return StatusCode(403);
            }

            try
            {
                _context.Videos.Update(video);
            }
            catch
            {
                return BadRequest();
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch
            {
                return StatusCode(503);
            }

            return Ok();
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVideo(int id)
        {
            var video = await _context.Videos.FindAsync(id);

            if (video == null)
                return NotFound();

            if (!isUserVideoOwner(video))
                return StatusCode(403);

            try
            {
                _context.Videos.Remove(video);
                var imageFile = Path.Combine(Directory.GetCurrentDirectory(), "Resources", "Images", video.Image);
                var videoFile = Path.Combine(Directory.GetCurrentDirectory(), "Resources", "Videos", video.Uri);
                if (System.IO.File.Exists(imageFile))
                    System.IO.File.Delete(imageFile);
                if (System.IO.File.Exists(videoFile))
                    System.IO.File.Delete(videoFile);
            }
            catch (Exception exception)
            {
                return BadRequest();
            }

            var likes = _context.Likes.Where(l => l.videoID == video.Id).ToList();
            if (likes.Any())
            {
                _context.Likes.RemoveRange(likes);
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception exception)
            {
                return StatusCode(503);
            }

            return Ok();
        }

        private void includeLikes(Video video)
        {
            video.Likes = _context.Likes.Where(l => l.videoID == video.Id).Count();
        }

        private bool isUserVideoOwner(Video video)
        {
            var userId = User.Claims.FirstOrDefault(u => u.Type == "Id");

            if (userId == null)
                return false;

            return int.Parse(userId.Value) == video.UserId;
        }
    }
}