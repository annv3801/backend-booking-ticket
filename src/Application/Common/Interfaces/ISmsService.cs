using Application.Common.Models;
using Nobi.Core.Responses;

namespace Application.Common.Interfaces
{
    public interface ISmsService
    {
        /// <summary>
        /// Send out the OTP to the given phone number
        /// </summary>
        /// <param name="phoneNumber">Phone number to receive</param>
        /// <param name="otp">OTP to send</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<RequestResult<SmsResult>> SendOtpAsync(string? phoneNumber, string? otp,
            CancellationToken cancellationToken = default(CancellationToken));
        

        /// <summary>
        /// Generate randomized OTP with 6 digits
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns>SmsResult.Otp</returns>
        Task<RequestResult<SmsResult>> GenerateOtpAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}