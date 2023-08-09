using System.Linq;
using System.Text.RegularExpressions;
using Application.Common;
using Application.Common.Interfaces;
using Application.Common.Models;
using FluentValidation;
using FluentValidation.Validators;

namespace Infrastructure.Common.Validators;
public static class PasswordValidator
{
    public static IRuleBuilderOptions<T, string> Password<T>(this IRuleBuilder<T, string> ruleBuilder, IStringLocalizationService localizationService, PasswordOptions options)
    {
        return ruleBuilder
                .NotEmpty().WithMessage(localizationService[LocalizationString.Common.EmptyField].Value)
                .MinimumLength(options.RequiredLength).WithMessage(localizationService[LocalizationString.Common.MinLengthField].Value)
                .RequiredUniqueChars(options.RequiredUniqueChars)
                .WithMessage(localizationService[LocalizationString.PasswordValidation.UniqueCharsField].Value)
                .RequireNonAlphanumeric(options.RequireNonAlphanumeric)
                .WithMessage(localizationService[LocalizationString.PasswordValidation.NonAlphanumericField].Value)
                .RequireLowerCase(options.RequireLowercase)
                .WithMessage(localizationService[LocalizationString.PasswordValidation.LowerCaseField].Value)
                .RequireUpperCase(options.RequireUppercase)
                .WithMessage(localizationService[LocalizationString.PasswordValidation.UpperCaseField].Value)
                .RequireDigit(options.RequireDigit)
                .WithMessage(localizationService[LocalizationString.PasswordValidation.DigitField].Value)
            ;
    }

    public static IRuleBuilderOptions<T, string> RequiredUniqueChars<T>(this IRuleBuilder<T, string> ruleBuilder, int minimumLength)
    {
        return ruleBuilder.SetValidator(new UniqueCharacterValidator<T>(minimumLength));
    }

    public static IRuleBuilderOptions<T, string> RequireNonAlphanumeric<T>(this IRuleBuilder<T, string> ruleBuilder, bool isRequire)
    {
        return ruleBuilder.SetValidator(new NonAlphanumericValidator<T>(isRequire));
    }

    public static IRuleBuilderOptions<T, string> RequireLowerCase<T>(this IRuleBuilder<T, string> ruleBuilder, bool isRequire)
    {
        return ruleBuilder.SetValidator(new LowerCaseValidator<T>(isRequire));
    }

    public static IRuleBuilderOptions<T, string> RequireUpperCase<T>(this IRuleBuilder<T, string> ruleBuilder, bool isRequire)
    {
        return ruleBuilder.SetValidator(new UpperCaseValidator<T>(isRequire));
    }

    public static IRuleBuilderOptions<T, string> RequireDigit<T>(this IRuleBuilder<T, string> ruleBuilder, bool isRequire)
    {
        return ruleBuilder.SetValidator(new DigitValidator<T>(isRequire));
    }
}

public class NonAlphanumericValidator<T> : PropertyValidator<T, string>
{
    // true : require special characters, false: not require special characters
    private bool IsRequire { get; set; }

    public NonAlphanumericValidator(bool isRequire)
    {
        this.IsRequire = isRequire;
    }

    public override bool IsValid(ValidationContext<T> context, string value)
    {
        var result = Regex.IsMatch(value, "[^a-zA-Z0-9]");
        if (IsRequire)
            return result;
        return !IsRequire;
    }

    public override string Name => "NonAlphanumericValidator";
}

public class UniqueCharacterValidator<T> : PropertyValidator<T, string>
{
    // value unique character
    private int UniqueCharacterRequired { get; set; }
    public override string Name => "UniqueCharacterValidator";

    public UniqueCharacterValidator(int uniqueCharacterRequired)
    {
        this.UniqueCharacterRequired = uniqueCharacterRequired;
    }

    public override bool IsValid(ValidationContext<T> context, string value)
    {
        // count occurrences of character
        var count = value.GroupBy(x => x).Select(v => v.Count()).Count(x => x == 1);
        context.MessageFormatter.AppendArgument("TotalUniqueCharacter", count).AppendArgument("UniqueCharacterRequired", UniqueCharacterRequired);
        return count >= UniqueCharacterRequired;
    }
}

public class LowerCaseValidator<T> : PropertyValidator<T, string>
{
    // true : require lower case, false: not require lower case
    private bool IsRequire { get; set; }

    public LowerCaseValidator(bool isRequire)
    {
        this.IsRequire = isRequire;
    }

    public override bool IsValid(ValidationContext<T> context, string value)
    {
        var result = Regex.IsMatch(value, "[a-z]");
        if (IsRequire)
            return result;
        return !IsRequire;
    }

    public override string Name => "LowercaseValidator";
}

public class UpperCaseValidator<T> : PropertyValidator<T, string>
{
    // true : require upper case, false: not require upper case
    private bool IsRequire { get; set; }

    public UpperCaseValidator(bool isRequire)
    {
        this.IsRequire = isRequire;
    }

    public override bool IsValid(ValidationContext<T> context, string value)
    {
        var result = Regex.IsMatch(value, "[A-Z]");
        if (IsRequire)
            return result;
        return !IsRequire;
    }

    public override string Name => "UpperCaseValidator";
}

public class DigitValidator<T> : PropertyValidator<T, string>
{
    // true : require digit, false: not require digit
    private bool IsRequire { get; set; }

    public DigitValidator(bool isRequire)
    {
        this.IsRequire = isRequire;
    }

    public override bool IsValid(ValidationContext<T> context, string value)
    {
        var result = Regex.IsMatch(value, "[0-9]");
        if (IsRequire)
            return result;
        return !IsRequire;
    }

    public override string Name => "DigitValidator";
}
