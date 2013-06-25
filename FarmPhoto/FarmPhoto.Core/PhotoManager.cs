using System;
using System.IO;
using System.Web;
using System.Drawing;
using FarmPhoto.Domain;
using FarmPhoto.Repository;
using System.Drawing.Imaging;
using System.Collections.Generic;

namespace FarmPhoto.Core
{
    public class PhotoManager : IPhotoManager
    {
        private readonly IPhotoRepository _photoRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="PhotoManager"/> class.
        /// </summary>
        /// <param name="photoRepository">The photo repository.</param>
        public PhotoManager(IPhotoRepository photoRepository)
        {
            _photoRepository = photoRepository;
        }

        /// <summary>
        /// Creates the photo.
        /// </summary>
        /// <param name="photo">The photo.</param>
        /// <param name="file">The file.</param>
        /// <returns></returns>
        public int CreatePhoto(Photo photo, HttpPostedFileBase file)
        {
            var memoryStream = new MemoryStream();

            file.InputStream.CopyTo(memoryStream);

            Image image = Image.FromStream(memoryStream);

            memoryStream.Dispose();

            byte[] thumbnailData = ScaleImage(image, 200, 200);
            byte[] photoData = ScaleImage(image, 800, 800);

            photo.FileSize = photoData.Length;
            photo.ThumbnailSize = thumbnailData.Length;

            photo.PhotoData = photoData;
            photo.ThumbnailData = thumbnailData;

            return _photoRepository.Create(photo);
        }

        /// <summary>
        /// Gets all photos that have been approved.
        /// </summary>
        /// <param name="page">The page.</param>
        /// <param name="numberReturned">The number returned.</param>
        /// <returns></returns>
        public IList<Photo> Get(int page,int numberReturned = 20)
        {
            return _photoRepository.Get(numberReturned, page);
        }

        /// <summary>
        /// Gets the specified photo by id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="thumbnail">if set to <c>true</c> [thumbnail].</param>
        /// <returns></returns>
        public Photo Get(int id, bool thumbnail)
        {
            return _photoRepository.Get(id, thumbnail);
        }

        /// <summary>
        /// Returns all the users photos.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        public IList<Photo> Get(User user)
        {
            return _photoRepository.Get(user);
        }

        /// <summary>
        /// Scales the image.
        /// </summary>
        /// <param name="image">The image.</param>
        /// <param name="maxWidth">Width of the max.</param>
        /// <param name="maxHeight">Height of the max.</param>
        /// <returns></returns>
        private static byte[] ScaleImage(Image image, int maxWidth, int maxHeight)
        {
            var ratioX = (double)maxWidth / image.Width;
            var ratioY = (double)maxHeight / image.Height;
            var ratio = Math.Min(ratioX, ratioY);

            var newWidth = (int)(image.Width * ratio);
            var newHeight = (int)(image.Height * ratio);

            var newImage = new Bitmap(newWidth, newHeight);
            Graphics.FromImage(newImage).DrawImage(image, 0, 0, newWidth, newHeight);

            var ms = new MemoryStream();

            newImage.Save(ms, ImageFormat.Jpeg);

            byte[] byteArray = ms.ToArray();

            ms.Dispose();

            return byteArray;
        }
    }
}
