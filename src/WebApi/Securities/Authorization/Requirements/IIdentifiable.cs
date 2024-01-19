using System;

namespace WebApi.Securities.Authorization.Requirements
{
    /// <summary>
    /// To differentiate roles, perms and scopes 
    /// </summary>
    public interface IIdentifiable
    {
        /// <summary>
        /// Act as ID
        /// </summary>
        Guid Identifier { get; }
    }
}