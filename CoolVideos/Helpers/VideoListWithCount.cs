using CoolVideos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoolVideos.Helpers
{
    public class VideoListWithCount
    {
        public List<Video> Videos { get; set; }
        public int VideoListCount { get; set; }

        public VideoListWithCount(List<Video> videos, int videosTotalCount)
        {
            Videos = videos;
            VideoListCount = videosTotalCount;
        }
    }
}
