using System;
using System.Diagnostics.CodeAnalysis;
using Application.Common.Models.Account;
// ReSharper disable All

namespace Application.Common.Models;
[ExcludeFromCodeCoverage]
public abstract class AuditableView
{
    public DateTime? Created { get; set; }
    public CreatedByView? CreatedBy { get; set; }
    public DateTime? LastModified { get; set; }
    public LastModifiedByView? LastModifiedBy { get; set; }
}
