
using Gallery.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gallery.Services.Interfaces
{
    public interface IPhotoService
    {

        public  Task<List<Photo>> GetPhotosAsync(int page, int perPage = 30);
    }
}
