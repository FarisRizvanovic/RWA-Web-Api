namespace RWA_Web_Api.Util;

public static class FileUtils
{
    public static bool IsImage(IFormFile file)
    {
        return file.ContentType.ToLower() == "image/jpg" ||
               file.ContentType.ToLower() == "image/jpeg" ||
               file.ContentType.ToLower() == "image/pjpeg" ||
               file.ContentType.ToLower() == "image/gif" ||
               file.ContentType.ToLower() == "image/x-png" ||
               file.ContentType.ToLower() == "image/png";
    }
}