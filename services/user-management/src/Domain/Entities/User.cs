
using Domain.Enums;
using Domain.ValueObject;
using Shared.ResultManagement;

namespace Domain.Entities
{
    public class User
    {
        public Guid Id { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public Email Email { get; private set; }
        public string Password { get; private set; }
        public UserName Username { get; private set; }
        public UserType Type { get; private set; }
        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
        //public DateTime UpdatedAt { get; private set; } = DateTime.UtcNow;

        public ICollection<Role> Roles { get; private set; } = new List<Role>();
        protected User() { }

        public User(string firstName, string lastName, Email email, string passwordHash, UserName username , UserType type)
        {
            Id = Guid.NewGuid();
            var firstNameResult = ValidateName(firstName, "First name");
            if (firstNameResult.IsFailure)
                throw new ArgumentException(firstNameResult.Error); // یا هندل بهتر با custom exception

            var lastNameResult = ValidateName(lastName, "Last name");
            if (lastNameResult.IsFailure)
                throw new ArgumentException(lastNameResult.Error);

            FirstName = firstNameResult.Value!;
            LastName = lastNameResult.Value!;
            Email = email;
            Password = ValidatePassword(passwordHash);
            Username = username;
            Type = type;
            CreatedAt = DateTime.UtcNow;
            //UpdatedAt = DateTime.UtcNow;
        }



        private Result<string, string> ValidateName(string value, string fieldName)
        {
            if (string.IsNullOrWhiteSpace(value))
                return Result<string, string>.Failure($"{fieldName} is required.");

            return Result<string, string>.Success(value.Trim());
        }

        private string ValidatePassword(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Password hash is required.");
            return value;
        }

        public Result<string, string> UpdateFirstName(string firstName)
        {
            var result = ValidateName(firstName, "First name");
            if (result.IsFailure)
                return result;

            FirstName = result.Value!;
            return Result<string, string>.Success(FirstName);
        }

        public Result<string, string> UpdateLastName(string lastName)
        {
            var result = ValidateName(lastName, "First name");
            if (result.IsFailure)
                return result;

            LastName = result.Value!;
            return Result<string, string>.Success(LastName);
        }

        public void UpdateEmail(Email email)
        {
            Email = email ?? throw new ArgumentNullException(nameof(email));
        }

        public void UpdateUsername(UserName username)
        {
            Username = username ?? throw new ArgumentNullException(nameof(username));
        }

        public Result<string, string> UpdatePassword(string newPassword)
        {
            if (string.IsNullOrWhiteSpace(newPassword))
                return Result<string, string>.Failure("Password is required.");

            Password = newPassword;
            return Result<string, string>.Success(Password);
        }

        public void UpdateUserType(UserType type)
        {
            Type = type;
        }


        public void AssignRole(Role role)
        {
            if (!Roles.Any(r => r.Title == role.Title))
                Roles.Add(role);
        }

        public void RemoveRole(Role role)
        {
            //Roles.RemoveAll(r => r.Title == role.Title);
        }

        public bool HasPermission(PermissionName permission)
        {
            return Roles
                .SelectMany(r => r.Permissions)
                .Any(p => p.Name == permission);
        }

    }
}
