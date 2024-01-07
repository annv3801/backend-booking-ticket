// ReSharper disable All

using System.Diagnostics.CodeAnalysis;

#pragma warning disable 8618
namespace Application.Common.Configurations
{
    /// <summary>
    /// To manage sms service provider configurations
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class SmsConfiguration
    {
        public string ApiKey { get; set; }
        public string ApiSecret { get; set; }
        public string BranchName { get; set; }
        public int SmsType { get; set; }
        public string OtpMessage { get; set; }
        public string OtpUrl { get; set; }

        /// <summary>
        /// Time to expire the OTP in the second
        /// </summary>
        public int Expiration { get; set; }

        public string WebsiteUrl { get; set; }
    }
}