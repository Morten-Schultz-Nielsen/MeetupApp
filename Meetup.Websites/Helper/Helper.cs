using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Meetup.Websites;
using System.Web;

namespace Meetup.Helper
{
    public static class Helper
    {
        /// <summary>
        /// Takes a picture file and crops and converts it into a string.
        /// </summary>
        /// <param name="picture">The picture file to convert</param>
        /// <param name="pictureWidth">the width the picture should have when converted</param>
        /// <param name="pictureHeight">the height the picture should have when converted</param>
        /// <returns>A link to the picture</returns>
        public static string PictureFileToString(this HttpPostedFileBase picture, int pictureWidth = 512, int pictureHeight = 512)
        {
            Image image = Image.FromStream(picture.InputStream);
            int imageWidth = Math.Min(image.Size.Width, pictureWidth);
            int imageHeight = Math.Min(image.Size.Height, pictureHeight);
            Image resizedImage = new Bitmap(pictureWidth, pictureHeight);

            using(Graphics g = Graphics.FromImage(resizedImage))
            {
                g.DrawImage(image, new Rectangle(0, 0, imageWidth, imageHeight), new Rectangle(0, 0, pictureWidth, pictureHeight), GraphicsUnit.Pixel);
            }
            image = resizedImage;

            string pictureDataString = "";
            byte[] pictureBytes;
            using(MemoryStream stream = new MemoryStream())
            {
                image.Save(stream, System.Drawing.Imaging.ImageFormat.Png);

                using(BinaryReader reader = new BinaryReader(stream))
                {
                    reader.BaseStream.Position = 0;
                    pictureBytes = reader.ReadBytes((int)stream.Length);
                }
                pictureDataString = Convert.ToBase64String(pictureBytes);
            }

            return pictureDataString;
        }

        /// <summary>
        /// Returns the id of the user who is logged in
        /// </summary>
        /// <param name="controller">the controller who wants the id</param>
        /// <returns>The id of the logged in user</returns>
        public static int? UserId(this Controller controller)
        {
            string userId = controller.User.Identity.GetUserId();
            if(userId == null)
            {
                return null;
            }
            return controller.HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(userId).InfoId;
        }
    }
}
