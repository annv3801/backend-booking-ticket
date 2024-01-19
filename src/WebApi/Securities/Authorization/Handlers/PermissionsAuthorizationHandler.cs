using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using WebApi.Common;
using WebApi.Common.Constants;
using WebApi.Securities.Authorization.Requirements;

namespace WebApi.Securities.Authorization.Handlers
{
    // We set the TRequirement as one of our custom requirements which is PermissionsRequirement. This handler
    // will be automatically triggered if registered in startup and a policy has this type of requirement.
    // Type of permission in table RolePermissions must be "Permission"
    /// <inheritdoc />
    [ExcludeFromCodeCoverage]
    public class PermissionsAuthorizationHandler : AuthorizationHandler<PermissionsRequirement>
    {
        private readonly ILogger<PermissionsAuthorizationHandler> _logger;

        /// <inheritdoc />
        public PermissionsAuthorizationHandler(ILogger<PermissionsAuthorizationHandler> logger)
        {
            _logger = logger;
        }

        /// <inheritdoc />
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionsRequirement requirement)
        {
            try
            {
                // If the user is not authenticated first, do nothing and return
                if (context.User.Identity != null && !context.User.Identity.IsAuthenticated)
                {
                    return Task.CompletedTask;
                }

                // If the user was already authorized, do nothing and return
                if (context.HasSucceeded)
                {
                    return Task.CompletedTask;
                }

                // If the user has super powers, authorize and return
                if (context.User.IsInRole(Role.SuperAdmin))
                {
                    Utility.Succeed(context, requirement.Identifier);
                    return Task.CompletedTask;
                }

                // If the user does not exist or the requirements are not properly set, do nothing and return
                if (string.IsNullOrWhiteSpace(requirement.Permissions))
                {
                    return Task.CompletedTask;
                }

                // Get the requirements based on the format "R1|R2|R3|RN" and mandate having at least 1 of them
                var requirementTokens = requirement.Permissions.Split("|", StringSplitOptions.RemoveEmptyEntries);

                // ReSharper disable once ConstantConditionalAccessQualifier
                if (requirementTokens?.Any() != true)
                {
                    return Task.CompletedTask;
                }

                // Create a list of requirements of format "application:area:permission", if any of them fail in format
                // or if there isn't at least one proper requirement, do nothing and return
                List<(string, string, string)> expectedRequirements = new List<(string, string, string)>();

                foreach (var token in requirementTokens)
                {
                    var separatedTokens = token.Split(":");

                    // ReSharper disable once ConstantConditionalAccessQualifier
                    if (separatedTokens?.Any() != true || separatedTokens.Length != 3)
                    {
                        return Task.CompletedTask;
                    }

                    expectedRequirements.Add((separatedTokens[0], separatedTokens[1], separatedTokens[2]));
                }

                if (expectedRequirements.Count == 0)
                {
                    return Task.CompletedTask;
                }

                // Fetch user permission claims
                // ReSharper disable once ConstantConditionalAccessQualifier
                var userPermissionClaims = context.User.Claims?.Where(c =>
                    string.Equals(c.Type, IdentityType.Permission, StringComparison.OrdinalIgnoreCase));


                // For each of user claims, challenge them against the expected permissions with the following rules:
                // (1) The first part must be the application name, no wild-cards are allowed.
                // (2) The second part should be the area name, the '*' is accepted as a wild-card.
                // (3) The third and final part should be the permission name, the '*' is accepted as a wild-card.
                // Example:
                // Expected             User                    Result
                // --------             -------                 -------
                // sales:*:*            sales:contacts:read     match
                // sales:contacts:read  sales:*:*               match
                // sales:contacts:read  sales:contacts:*        match
                // sales:contacts:read  sales:contacts:read     match
                // sales:contacts:read  sales:*:read            match
                // sales:contacts:read  sales:shipping:*        fail
                // sales:contacts:read  sales:contacts:write    fail
                // sales:contacts:read  finance:*:*             fail
                // ...
                // ReSharper disable once ConstantNullCoalescingCondition
                // ReSharper disable once LoopCanBeConvertedToQuery
                foreach (var claim in userPermissionClaims ?? Enumerable.Empty<Claim>())
                {
                    // ReSharper disable once ConstantConditionalAccessQualifier
                    var userPermission = claim.Value?.Split(':');

                    // ReSharper disable once ConditionIsAlwaysTrueOrFalse
                    if (userPermission == null || userPermission.Length != 3)
                    {
                        continue;
                    }

                    var match = expectedRequirements
                        .Where(r =>
                            r.Item1 == userPermission[0]
                            && (r.Item2 == "*" || userPermission[1] == "*" || r.Item2 == userPermission[1])
                            && (r.Item3 == "*" || userPermission[2] == "*" || r.Item3 == userPermission[2]));

                    // ReSharper disable once InvertIf
                    if (match.Any())
                    {
                        // Succeed all requirements in the context having the same identifier
                        Utility.Succeed(context, requirement.Identifier);
                        break;
                    }
                }

                return Task.CompletedTask;
            }
            catch (Exception e)
            {
                _logger.LogCritical(e, "Permission HandleRequirementAsync");
                throw;
            }
        }
    }
}