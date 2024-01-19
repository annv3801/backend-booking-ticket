using System;
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
    /// <summary>
    /// Type of Scope in table RolePermissions must be Scope
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class ScopesAuthorizationHandler : AuthorizationHandler<ScopesRequirement>
    {
        private readonly ILogger<ScopesAuthorizationHandler> _logger;

        /// <inheritdoc />
        public ScopesAuthorizationHandler(ILogger<ScopesAuthorizationHandler> logger)
        {
            _logger = logger;
        }

        /// <inheritdoc />
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
            ScopesRequirement requirement)
        {
            try
            {
                if (context.User.Identity != null && !context.User.Identity.IsAuthenticated)
                {
                    return Task.CompletedTask;
                }

                if (context.HasSucceeded)
                {
                    return Task.CompletedTask;
                }

                if (context.User.IsInRole(Role.SuperAdmin))
                {
                    Utility.Succeed(context, requirement.Identifier);
                    return Task.CompletedTask;
                }

                if (requirement == null || string.IsNullOrWhiteSpace(requirement.Scopes))
                {
                    return Task.CompletedTask;
                }

                var requirementTokens = requirement.Scopes.Split("|", StringSplitOptions.RemoveEmptyEntries);

                if (requirementTokens.Any() != true)
                {
                    return Task.CompletedTask;
                }

                var expectedRequirements = requirementTokens.ToList();

                if (expectedRequirements.Count == 0)
                {
                    return Task.CompletedTask;
                }

                var userScopeClaims = context.User.Claims.Where(c =>
                    string.Equals(c.Type, IdentityType.Scope, StringComparison.OrdinalIgnoreCase));

                // ReSharper disable once LoopCanBeConvertedToQuery
                // ReSharper disable once ConstantNullCoalescingCondition
                foreach (var claim in userScopeClaims ?? Enumerable.Empty<Claim>())
                {
                    var match = expectedRequirements
                        .Where(r => string.Equals(r, claim.Value, StringComparison.OrdinalIgnoreCase));
                    // ReSharper disable once InvertIf
                    if (match.Any())
                    {
                        Utility.Succeed(context, requirement.Identifier);
                        break;
                    }
                }

                return Task.CompletedTask;
            }
            catch (Exception e)
            {
                _logger.LogCritical(e, "Scope HandleRequirementAsync");
                throw;
            }
        }
    }
}