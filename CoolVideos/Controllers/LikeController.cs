using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoolVideos.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CoolVideos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LikeController : ControllerBase
    {
        private readonly CoolVideosContext _context;

        public LikeController(CoolVideosContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("video/{videoID}/user/{userID}")]
        [Authorize]
        public async Task<IActionResult> DidUserLikeVideo(int videoID, int userID)
        {
            bool didLike = await _context.Likes.FindAsync(videoID, userID) != null;
            return Ok(new { liked = didLike });
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> LikeVideo(Like like)
        {
             _context.Likes.Add(like);

            try
            {
                await _context.SaveChangesAsync();
                //return Ok(new { videoID = videoID, likes = _context.Likes.Where(l => l.videoID == videoID) });
                return Ok(new { like, likes = _context.Likes.Where(l => l.videoID == like.videoID).Count() });
            } 
            catch
            {
                return BadRequest();
            }
        }

        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> UnlikeVideo(Like like)
        {
            _context.Likes.Remove(like);
            
            try
            {
                await _context.SaveChangesAsync();
                return Ok(new { like, likes = _context.Likes.Where(l => l.videoID == like.videoID).Count() });
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}