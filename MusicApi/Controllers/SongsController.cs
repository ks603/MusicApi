using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicApi.Data;
using MusicApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MusicApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SongsController : ControllerBase
    {
        private ApiDbContext _dbContext;
        public SongsController(ApiDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        // GET: api/<SongsController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _dbContext.Songs.ToListAsync());
        }

        // GET api/<SongsController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var song = await _dbContext.Songs.FindAsync(id);

            if (song == null)
            {
                return NotFound("No record found agianst this ID");
            }

            return Ok(song);
        }

        //// POST api/<SongsController>
        //[HttpPost]
        //public async Task<IActionResult> Post([FromBody] Song song)
        //{
        //    await _dbContext.Songs.AddAsync(song);
        //    await _dbContext.SaveChangesAsync();

        //    return StatusCode(StatusCodes.Status201Created);
        //}

        [HttpPost]
        public async Task<IActionResult> Post([FromForm] Song song)
        {
            string connectionString = @"DefaultEndpointsProtocol=https;AccountName=musicstorageacc1;AccountKey=Umgr0I1Yfs97E+PA5SkYA15DjAHQUZJuVnr/L97k16wFUoV3EkzxayiWsPDJ6aKCH7tkVt1kc7s2/IZFWklbuA==;EndpointSuffix=core.windows.net";
            string containerName = "test";

            BlobContainerClient blobContainerClient = new BlobContainerClient(connectionString,containerName);
            BlobClient blobClient = blobContainerClient.GetBlobClient(song.Image.FileName);
            var memoryStream = new MemoryStream();
            await song.Image.CopyToAsync(memoryStream);
            memoryStream.Position = 0;
            await blobClient.UploadAsync(memoryStream);
            song.ImageUrl = blobClient.Uri.AbsoluteUri;
            await _dbContext.Songs.AddAsync(song);
            await _dbContext.SaveChangesAsync();
            return StatusCode(StatusCodes.Status201Created);
        }

        // PUT api/<SongsController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Song songObj)
        {
           var song = await _dbContext.Songs.FindAsync(id);
            if (song == null)
            {
                return NotFound("No record found against this ID");
            }
            else
            {
                song.Title = songObj.Title;
                song.Language = songObj.Language;
                song.Duration = songObj.Duration;
                await _dbContext.SaveChangesAsync();

                return Ok("Record updated succefully");
            }
         
        }

        // DELETE api/<SongsController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var song = await _dbContext.Songs.FindAsync(id);

            if (song == null)
            {
                return NotFound("No record found");
            }
            else
            {
                _dbContext.Songs.Remove(song);
                await _dbContext.SaveChangesAsync();

                return Ok("Message Deleted");
            }
         
        }
    }
}
