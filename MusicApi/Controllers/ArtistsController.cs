using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MusicApi.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArtistsController : ControllerBase
    {
        private ApiDbContext _dbContext;
        public ArtistsController(ApiDbContext dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
