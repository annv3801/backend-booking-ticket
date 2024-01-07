using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using System.Text.Json.Serialization;
using Application.Common.Models;

namespace Application.Common;

[ExcludeFromCodeCoverage]
public static class Constants
{
    public static class FieldLength
    {
        public const int TextMinLength = 3;
        public const int TextMaxLength = 255;
        public const int MiddleTextLength = 50;
        public const int UrlMaxLength = 1000;
        public const int DescriptionMaxLength = 1000;
        public const int RecaptchaMaxLength = 1000;
        public const int FirebaseTokenMaxLength = 700;
    }
    public static readonly JsonSerializerOptions JsonSerializerOptions = new JsonSerializerOptions()
    {
        ReferenceHandler = ReferenceHandler.Preserve,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        DictionaryKeyPolicy = JsonNamingPolicy.CamelCase
    };

    public static class LoginProviders
    {
        public const string Self = "SELF";
    }

    public static class SupportedCultures
    {
        public static string[] Cultures =
        {
            English,
            Japanese,
            Vietnamese
        };

        public const string English = "en-US";
        public const string Japanese = "ja-JP";
        public const string Vietnamese = "vi-VN";
    }

    public static class Pagination
    {
        public const int DefaultPage = 1;
        public const int DefaultSize = 100;
        public const bool DefaultOrderByDesc = false;
        public const int DefaultCurrentPage = 0;
        public const int DefaultTotalPages = 0;
        public const int DefaultTotalItems = 0;
        public const string DefaultOrderBy = "";
    }
    public static string JsonDateTimeFormat = "yyyy'-'MM'-'dd'T'HH':'mm':'ssZ";
    
    public static class MimeTypes
    {
        public static class Text
        {
            public const string Plain = "text/plain";
            public const string Html = "text/html";
            public const string Xml = "text/xml";
            public const string RichText = "text/richtext";
        }

        public static class Application
        {
            public const string Soap = "application/soap+xml";
            public const string Octet = "application/octet-stream";
            public const string Rtf = "application/rtf";
            public const string Pdf = "application/pdf";
            public const string Zip = "application/zip";
            public const string Json = "application/json";
            public const string Xml = "application/xml";
        }

        public static class Image
        {
            public const string Gif = "image/gif";
            public const string Tiff = "image/tiff";
            public const string Jpeg = "image/jpeg";
        }
    }
}