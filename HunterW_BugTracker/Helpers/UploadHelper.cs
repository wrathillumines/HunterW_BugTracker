using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace HunterW_BugTracker.Helpers
{
    public class UploadHelper
    {
        public static bool IsWebFriendlyImage(HttpPostedFileBase file)
        {
            if (file == null)
                return false;

            if (file.ContentLength > 2 * 1024 * 1024 || file.ContentLength < 1024)
                return false;

            try
            {
                using (var image = Image.FromStream(file.InputStream))
                {
                    return ImageFormat.Jpeg.Equals(image.RawFormat) ||
                        ImageFormat.Png.Equals(image.RawFormat);
                }
            }
            catch
            {
                return false;
            }
        }

        public static bool IsValidAttachment(HttpPostedFileBase file)
        {
            try
            {
                if (file == null)
                    return false;

                if (file.ContentLength > 5 * 1024 * 1024)
                    return false;

                var extvalid = false;

                foreach (var extension in WebConfigurationManager.AppSettings["AttachmentExtensions"].Split(','))
                {
                    if (Path.GetExtension(file.FileName) == extension)
                    {
                        extvalid = true;
                        break;
                    }
                }
                return IsWebFriendlyImage(file) || extvalid;
            }
            catch
            {
                return false;
            }
        }

        public static string GetIconPath(string filePath)
        {
            switch(Path.GetExtension(filePath))
            {
                case ".doc":
                    return "/FileIcons/doc.png";
                case ".docx":
                    return "/FileIcons/docx.png";
                case ".jpg":
                    return "/FileIcons/jpg.png";
                case ".pdf":
                    return "/FileIcons/pdf.png";
                case ".png":
                    return "/FileIcons/png.png";
                case ".rtf":
                    return "/FileIcons/rtf.png";
                case ".txt":
                    return "/FileIcons/txt.png";
                case ".xls":
                    return "/FileIcons/xls.png";
                case ".xlsx":
                    return "/FileIcons/xlsx.png";
                default:
                    return "/FileIcons/file.png";
            }
        }
    }
}