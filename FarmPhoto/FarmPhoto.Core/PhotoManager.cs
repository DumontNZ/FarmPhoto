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
        /// Creates the photo from an uploaded file slow option probably going to become obsolete.
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

            Photo thumbnailData = ScaleImage(image, 200, 200);
            Photo photoData = ScaleImage(image, 800, 800);

            photo.FileSize = photoData.PhotoData.Length;
            photo.ThumbnailSize = thumbnailData.PhotoData.Length;

            photo.PhotoData = photoData.PhotoData;
            photo.ThumbnailData = thumbnailData.PhotoData;

            photo.Width = photoData.Width;
            photo.Height = photoData.Height;

            return _photoRepository.Create(photo);
        }

        /// <summary>
        /// Creates the photo created from a file on the Server.
        /// </summary>
        /// <param name="photo">The photo.</param>
        /// <returns></returns>
        public int CreatePhoto(Photo photo)
        {
            return _photoRepository.Create(photo);
        }



        /// <summary>
        /// Gets all photos that have been approved if gallery otherwise unapproved if admin.
        /// </summary>
        /// <param name="from">Starting Image</param>
        /// <param name="to">last Image</param>
        /// <param name="approved">if set to <c>true</c> [approved].</param>
        /// <returns></returns>
        public IList<Photo> Get(int from, int to, bool approved = true)
        {
            return _photoRepository.Get(from, to, approved);
        }

        /// <summary>
        /// Updates the specified photos details.
        /// </summary>
        /// <param name="photo">The photo.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public int Update(Photo photo)
        {
            return _photoRepository.Update(photo);
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
        /// <param name="from">Starting Image</param>
        /// <param name="to">last Image</param>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        public IList<Photo> Get(int from, int to, User user)
        {
            return _photoRepository.Get(from, to, user);
        }

        /// <summary>
        /// Gets photos that have been tagged with tag.
        /// </summary>
        /// <param name="from">Starting Image</param>
        /// <param name="to">last Image</param>
        /// <param name="tag">The tag.</param>
        /// <returns></returns>
        public IList<Photo> Get(int from, int to, Tag tag)
        {
            return _photoRepository.Get(from, to, tag);
        }

        /// <summary>
        /// Updates the specified photo to apporved.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="approved">if set to <c>true</c> [approved].</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public int Update(int id, bool approved)
        {
            return _photoRepository.Update(id, approved);
        }

        /// <summary>
        /// Deletes the specified photo.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public int Delete(int id)
        {
            return _photoRepository.Delete(id);
        }

        public IList<Photo> Search(int from, int to, string query)
        {
            return _photoRepository.Search(from, to, query);
        }

        /// <summary>
        /// Scales the image.
        /// </summary>
        /// <param name="image">The image.</param>
        /// <param name="maxWidth">Width of the max.</param>
        /// <param name="maxHeight">Height of the max.</param>
        /// <returns></returns>
        private static Photo ScaleImage(Image image, int maxWidth, int maxHeight)
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

            var photo = new Photo
                {
                    PhotoData = ms.ToArray(),
                    Width = newWidth,
                    Height = newHeight
                };

            ms.Dispose();

            return photo;
        }
    }
}
