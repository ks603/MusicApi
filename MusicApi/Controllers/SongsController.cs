﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MusicApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SongsController : ControllerBase
    {
        private static List<Song> songs = new List<Song>()
        {
            new Song(){Id = 0 , Title = "Willow", Language = "English"},
            new Song(){Id = 1 , Title = "After Glow", Language = "English"},
        };

        [HttpGet]

        public IEnumerable<Song> Get()
        {
            return songs;
        }

        [HttpPost]

        public void Post([FromBody]Song song)
        {
            songs.Add(song);
        }
    }
}
