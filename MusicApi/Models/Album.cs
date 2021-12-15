using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicApi.Models
{
    public class Album
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public string ImageUrl { get; set; }
        public int ArtistId { get; set; }

    }
}
