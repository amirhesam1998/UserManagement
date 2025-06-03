
using System.Text.Json.Serialization;
using Domain.Enums;
using Domain.ValueObject;
using MediatR;
using Shared.ResultManagement;

namespace Application.Commands.Users
{
    public class EditUserCommand : IRequest<Result<Guid, string>>
    {
        [JsonIgnore]
        public Guid Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; } = string.Empty;
        public int Password { get; set; } 
        public int Password_confirm { get; set; }
        public string? Username { get; set; } = string.Empty;
        public UserType? Type { get; set; }
    }
}
