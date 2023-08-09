using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using Application.Common.Interfaces;
using Microsoft.Extensions.Localization;
using WebApi.Localization;

namespace WebApi.Services;
[ExcludeFromCodeCoverage]
public class StringLocalizationServiceService : IStringLocalizationService
{
    private readonly IStringLocalizer<SharedLocalization> _localizer;

    public StringLocalizationServiceService(IStringLocalizer<SharedLocalization> localizer)
    {
        _localizer = localizer;
    }

    public LocalizedString this[string key] => _localizer[key];

    
    public LocalizedString this[string key, params object[] args] => _localizer[key, args];

    
    public LocalizedString this[string key, CultureInfo cultureInfo, params object[] args] =>
        throw new System.NotImplementedException();
}
