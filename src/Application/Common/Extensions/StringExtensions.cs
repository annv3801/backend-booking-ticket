using Application.Common.Interfaces;
using Application.Common.Models;

namespace Application.Common.Extensions;
public static class StringExtensions
{
    /// <summary>
    /// To build error with unknown field
    /// </summary>
    /// <param name="value"></param>
    /// <param name="localizationService"></param>
    /// <returns></returns>
    public static IEnumerable<ErrorItem> ToErrors(this string value, IStringLocalizationService localizationService)
    {
        return new[]
        {
            new ErrorItem()
            {
                Error = localizationService[value].Value,
                FieldName = LocalizationString.Common.UnknownFieldName
            }
        };
    }
}
