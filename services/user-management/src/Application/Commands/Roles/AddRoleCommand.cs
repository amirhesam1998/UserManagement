using MediatR;
using Shared.ResultManagement;

namespace Application.Commands.Roles
{
    public class AddRoleCommand : IRequest<Result<Guid , string>>
    {
        public string Title { get; init; }
        public string? Slug { get; init; }
        public string Description { get; init; }

        public AddRoleCommand(string title, string slug, string description)
        {
            Title = title;
            Slug = slug;
            Description = description;
        }

    }

    
        
}
