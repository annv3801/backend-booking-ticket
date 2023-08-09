namespace Domain.Constants;
public static class JwtClaimTypes
{
    public const string Subject = "sub";
    public const string Name = "name";
    public const string GivenName = "given_name";
    public const string FamilyName = "family_name";
    public const string MiddleName = "middle_name";
    public const string NickName = "nickname";
    public const string PreferredUserName = "preferred_username";
    public const string Profile = "profile";
    public const string Picture = "picture";
    public const string WebSite = "website";
    public const string Email = "email";
    public const string EmailVerified = "email_verified";
    public const string Gender = "gender";
    public const string BirthDate = "birthdate";
    public const string ZoneInfo = "zoneinfo";
    public const string Locale = "locale";
    public const string PhoneNumber = "phone_number";
    public const string PhoneNumberVerified = "phone_number_verified";
    public const string Address = "address";
    public const string Audience = "aud";
    public const string Issuer = "iss";
    public const string NotBefore = "nbf";
    public const string Expiration = "exp";
    public const string UpdatedAt = "updated_at";
    public const string IssuedAt = "iat";
    public const string AuthenticationMethod = "amr";
    public const string SessionId = "sid";
    public const string AuthenticationContextClassReference = "acr";
    public const string AuthenticationTime = "auth_time";
    public const string AuthorizedParty = "azp";
    public const string AccessTokenHash = "at_hash";
    public const string AuthorizationCodeHash = "c_hash";
    public const string StateHash = "s_hash";
    public const string Nonce = "nonce";
    public const string JwtId = "jti";
    public const string Events = "events";
    public const string ClientId = "client_id";
    public const string Scope = "scope";
    public const string Actor = "act";
    public const string MayAct = "may_act";
    public const string Id = "id";
    public const string UserId = "uid";
    public const string IdentityProvider = "idp";
    public const string UserName = "user_name";
    public const string SupplierCode = "supcode";
    public const string ReferenceTokenId = "reference_token_id";
    public const string Confirmation = "cnf";
    public class JwtTypes
    {
        public const string AccessToken = "at+jwt";
        public const string AuthorizationRequest = "oauth-authz-req+jwt";
    }
}
