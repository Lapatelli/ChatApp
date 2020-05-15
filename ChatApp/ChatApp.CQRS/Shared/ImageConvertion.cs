using Microsoft.AspNetCore.Http;
using System.Drawing;
using System.IO;
using System.Net;

namespace ChatApp.CQRS.Shared
{
    public static class ImageConvertion
    {
        public static byte[] ImageToByteArray(string photoUrl)
        {
            using (WebClient webClient = new WebClient())
            {
                byte[] data = webClient.DownloadData($"{photoUrl}");

                using (MemoryStream str = new MemoryStream(data))
                {
                    using (var img = Image.FromStream(str))
                    {
                        using (MemoryStream ms = new MemoryStream())
                        {
                            img.Save(ms, img.RawFormat);
                            return ms.ToArray();
                        }
                    }
                }
            }
        }

        public static byte[] PictureToByteArray(IFormFile picture)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                picture.CopyTo(ms);

                return ms.ToArray();
            }
        }
    }
}
