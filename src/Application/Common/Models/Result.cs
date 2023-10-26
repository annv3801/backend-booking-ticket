using System.Diagnostics.CodeAnalysis;
using Application.Common.Models;
using MediatR;

// ReSharper disable All
#pragma warning disable 8625

namespace Nobi.Core.Responses
{
    /// <summary>
    /// To generate result for all actions in this system.
    /// Result contains:
    ///     1. Succeeded flag: true/false
    ///     2. Errors list: List of errors if any in case of failure
    ///     3. Data object: Data object if action need to pass data back to caller
    ///     4. Message string: Message content if needed
    /// </summary>
    /// <typeparam name="TResultDataType"></typeparam>
    [ExcludeFromCodeCoverage]
    public class RequestResult<TResultDataType> : IRequest<Unit>
    {
        internal RequestResult(bool success, TResultDataType? data, string? message, IEnumerable<ErrorItem>? errors)
        {
            Success = success;
            Errors = errors?.ToArray();
            Data = data;
            Message = message;
        }

        public TResultDataType? Data { get; private set; }
        public bool Success { get; private set; }
        public IEnumerable<ErrorItem>? Errors { get; private set; }
        public string? Message { get; set; }

        /// <summary>
        /// Handle success
        /// </summary>
        /// <param name="data"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public static RequestResult<TResultDataType> Succeed(string? message = null, TResultDataType? data = default)
        {
            return new RequestResult<TResultDataType>(true, data, message, Array.Empty<ErrorItem>());
        }

        /// <summary>
        /// Handle failure while processing request
        /// </summary>
        /// <param name="message"></param>
        /// <param name="errors"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static RequestResult<TResultDataType> Fail(string? message, IEnumerable<ErrorItem>? errors = null, TResultDataType? data = default)
        {
            return new RequestResult<TResultDataType>(false, data, message, errors);
        }

    }
}