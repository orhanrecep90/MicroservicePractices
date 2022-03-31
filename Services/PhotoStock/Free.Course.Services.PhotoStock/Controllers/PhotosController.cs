using Free.Course.Services.PhotoStock.Dtos;
using FreeCourse.Shared.ControllerBases;
using FreeCourse.Shared.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Free.Course.Services.PhotoStock.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhotosController : CustomBaseController
    {
        [HttpPost]
        public async Task<IActionResult> SaveAsync(IFormFile photo,CancellationToken cancellationToken)
        {
            if (photo!=null)
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/photos", photo.FileName);
                using FileStream stream = new FileStream(path, FileMode.Create);
                await photo.CopyToAsync(stream, cancellationToken);

                var returnPath = "photos/" + photo.FileName;
                var photoDto = new PhotoDto { Url = returnPath };
                return CreateActionResultInstance(Response<PhotoDto>.Success(photoDto, 200));
            }
            return CreateActionResultInstance(Response<PhotoDto>.Fail("photo is empty",400));
        }

        [HttpDelete]
        public IActionResult PhotoDelete(string url)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/photos", url);
            if (!System.IO.File.Exists(path))
                CreateActionResultInstance(Response<NoContent>.Fail("photo not found!", 400));
            System.IO.File.Delete(path);
            return CreateActionResultInstance(Response<NoContent>.Success(204));
        }
    }
}
