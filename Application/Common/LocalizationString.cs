using System.Diagnostics.CodeAnalysis;

namespace Application.Common;
[ExcludeFromCodeCoverage]
public static class LocalizationString
{
    public static class Common
    {
        public const string Success = "Success";
        public const string Error = "Error";
        public const string DataValidationError = "Data validation error";
        public const string UnknownFieldName = "General";
        public const string CommitFailed = "Cannot finish you action at this time, try again";
        public const string ViewListFailed = "Cannot finish you action at this time, try again";
        public const string ItemNotFound = "Item is not found, deactivated or deleted";
        public const string EmptyField = "{PropertyName} is empty";
        public const string IncorrectFormatField = "{PropertyName} ({PropertyValue}) format is not correct";
        public const string MaxLengthField = "The length of {PropertyName} must be {MaxLength} characters or fewer. You entered {TotalLength} characters";
        public const string MinLengthField = "The length of {PropertyName} must be at least {MaxLength} characters. You entered {TotalLength} characters";
        public const string DuplicatedItem = "Item is duplicated";
        public const string DuplicatedField = "{PropertyName} is duplicated";
        public const string NotValidEnumValue = "{PropertyName} value ({PropertyValue}) is not valid";
        public const string SaveFailed = "Save failed";
    }

    public static class Account
    {
        public const string FailedToCreateAccount = "Failed to create account {0}";
        public const string CreatedAccount = "Created Account {0}";
        public const string FailedToSignInWithUserName = "Failed to sign in with user name {0}";
        public const string NotFound = "Account is not found, deactivated or deleted";
        public const string AccountIsNotActive = "Your account is not active, you must activate your account first";
        public const string NotLockedYet= "Account is not locked yet, you can not unlock account {0}";
        public const string AccountIsPendingApproval = "Your account is pending for approval";
        public const string AccountIsPendingConfirmation = "Your account is pending for your confirmation";
        public const string UserNameOrPasswordIncorrectWithLockoutEnabled = "User name or password is incorrect ({0} times left)";
        public const string UserNameOrPasswordIncorrectWithoutLockOut = "User name or password is incorrect";
        public const string ChangePasswordRequired = "You need to change your password first";
        public const string PasswordExpired = "Your password is expired";
        public const string LockedOutAffected = "You account is temporarily being locked out, it will be unlocked at {0}";
        public const string AccountLockedOut = "Your account has been locked out";
        public const string NotLogin = "You are not logged in yet";
        public const string PasswordIncorrect = "Password is incorrect, please try again";
        public const string ChangedPasswordSuccess = "Password changed successfully ";
        public const string FailedToChangePassword = "Failed to change password";
        public const string FailedToLockAccount = "Failed to lock Account {0}";
        public const string LockedYet= "Account is locked yet, you can not lock account";
        public const string LockedAccount = "Locked Account {0}";
        public const string PermissionDenied = "Permission denied, You can update account who belong your tenant only ";
        public const string Unlocked = "Unlocked Account {0}";        
        public const string FailedToUnlock = "Failed to unlock Account {0}";
        public const string AccountIsNotInActive = "Your account is not inactive, you can not activate your account";
        public const string ActivatedAccount = "Activated Account {0} ";
        public const string FailedToActiveAccount = "Failed to active account {0}";
        public const string AccountIsDeactivate = "Your account is already in inactive status, you can not deactivate";
        public const string DeactivatedAccount = "Deactivated account {0}";
        public const string FailedToDeactivateAccount = "Failed to deactivate account {0}";
        public const string AccountIsDeleted = "Your account is deleted, you can not delete account again";
        public const string DeletedAccount = "Deleted Account {0}";
        public const string FailedToDeleteAccount = "Failed to delete account {0}";
        public const string FailedToViewAccountDetailByAdmin = "Failed to view account {0} by admin";
        public const string ViewedAccountDetailByAdmin = "Viewed account {0} by admin";
        public const string DuplicatedEmail = "Email is duplicated";
        public const string DuplicatedPhoneNumber = "PhoneNumber is duplicated";
        public const string UpdatedMyAccount = "Updated your account";
        public const string FailedToUpdateMyAccount = "Failed to update your account";
        public const string UpdatedAccount = "Updated Account {0}";
        public const string FailedToUpdateAccount = "Failed to update Account {0}";
        public const string LoggedOut = "You are logged out";
        public const string FailedToLogOut = "Failed to log out ";
        public const string LoggedIn = "You are logged in";
    }

    public static class Category
    {
        public const string DuplicateCategory = "Category is duplicate";
        public const string NotFoundCategory = "Category not found";
        public const string FailedToCreate = "Failed to create Category {0}";
        public const string AlreadyDeleted = "Category is already deleted, you cannot delete it again";
        
        public const string Deleted = "Deleted Category {0}";
        public const string FailedToDelete = "Failed to delete Category {0}";
        
        public const string Updated = "Updated Category {0}";
        public const string FailedToUpdate = "Failed to update Category {0}";
        
        public const string FailedToViewList = "Failed to view list category";
        public const string EmptyCategoryList = "No Category";
        public const string ViewedListCategory = "Viewed list Category";
    }
    public static class Film
    {
        public const string NotFoundCategory = "Not found category";
        public const string FailedToCreate = "Failed to create Film {0}";
        public const string AlreadyDeleted = "Film is already deleted, you cannot delete it again";
        
        public const string Deleted = "Deleted Film {0}";
        public const string FailedToDelete = "Failed to delete Film {0}";
        
        public const string Updated = "Updated Film {0}";
        public const string FailedToUpdate = "Failed to update Film {0}";
        
        public const string FailedToViewList = "Failed to view list film";
        public const string EmptyFilmList = "No Film";
        public const string ViewedListFilm = "Viewed list Film";
    }
    public static class Theater
    {
        public const string FailedToCreate = "Failed to create Theater {0}";
        public const string AlreadyDeleted = "Theater is already deleted, you cannot delete it again";
        
        public const string Deleted = "Deleted Theater {0}";
        public const string FailedToDelete = "Failed to delete Theater {0}";
        
        public const string Updated = "Updated Theater {0}";
        public const string FailedToUpdate = "Failed to update Theater {0}";
        
        public const string FailedToViewList = "Failed to view list theater";
        public const string EmptyTheaterList = "No Theater";
        public const string ViewedListTheater = "Viewed list Theater";
    }
    public static class Room
    {
        public const string NotFoundTheater = "Not found theater";
        public const string FailedToCreate = "Failed to create Room {0}";
        public const string AlreadyDeleted = "Room is already deleted, you cannot delete it again";
        
        public const string Deleted = "Deleted Room {0}";
        public const string FailedToDelete = "Failed to delete Room {0}";
        
        public const string Updated = "Updated Room {0}";
        public const string FailedToUpdate = "Failed to update Room {0}";
        
        public const string FailedToViewList = "Failed to view list room";
        public const string EmptyRoomList = "No Room";
        public const string ViewedListRoom = "Viewed list Room";
    }
    public static class FilmSchedules
    {
        public const string NotFoundRoom = "Not found room";
        public const string NotFoundFilm = "Not found film";
        public const string FailedToCreate = "Failed to create FilmSchedules {0}";
        public const string AlreadyDeleted = "FilmSchedules is already deleted, you cannot delete it again";
        
        public const string Deleted = "Deleted FilmSchedules {0}";
        public const string FailedToDelete = "Failed to delete FilmSchedules {0}";
        
        public const string Updated = "Updated FilmSchedules {0}";
        public const string FailedToUpdate = "Failed to update FilmSchedules {0}";
        
        public const string FailedToViewList = "Failed to view list film schedules";
        public const string EmptyFilmSchedulesList = "No FilmSchedules";
        public const string ViewedListFilmSchedules = "Viewed list FilmSchedules";
    }
    public static class Seat
    {
        public const string NotFoundRoom = "Not found room";
        public const string FailedToCreate = "Failed to create Seat {0}";
        public const string AlreadyDeleted = "Seat is already deleted, you cannot delete it again";
        
        public const string Deleted = "Deleted Seat {0}";
        public const string FailedToDelete = "Failed to delete Seat {0}";
        
        public const string Updated = "Updated Seat {0}";
        public const string FailedToUpdate = "Failed to update Seat {0}";
        
        public const string FailedToViewList = "Failed to view list seat";
        public const string EmptySeatList = "No Seat";
        public const string ViewedListSeat = "Viewed list Seat";
    }
    public static class Ticket
    {
        public const string NotFoundSchedule = "Not found schedule";
        public const string FailedToCreate = "Failed to create Ticket {0}";
        public const string AlreadyDeleted = "Ticket is already deleted, you cannot delete it again";
        
        public const string Deleted = "Deleted Ticket {0}";
        public const string FailedToDelete = "Failed to delete Ticket {0}";
        
        public const string Updated = "Updated Ticket {0}";
        public const string FailedToUpdate = "Failed to update Ticket {0}";
        
        public const string FailedToViewList = "Failed to view list tickets";
        public const string EmptyTicketList = "No Ticket";
        public const string ViewedListTicket = "Viewed list Ticket";
    }
    public static class PasswordValidation
    {
        public const string UniqueCharsField = "The number character of {PropertyName} must be at least {UniqueCharacterRequired} unique characters. You entered {TotalUniqueCharacter} unique characters";

        public const string NonAlphanumericField = "The {PropertyName} required special characters. Your password do not have special characters. Please try again";

        public const string LowerCaseField = "The {PropertyName} must have at least one lowercase ('a'-'z'). Please try again";
        public const string UpperCaseField = "The {PropertyName} must have at least one uppercase ('A'-'Z'). Please try again";
        public const string DigitField = "The {PropertyName} must have at least one digit ('0'-'9'). Please try again";
        public const string FailedToConfirmPassword = "Your confirm password is not same as your new password ";
    }
}
