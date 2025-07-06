using MediatR;
using Shared.ResultManagement;

namespace Application.Commands.Roles
{
    public class AddRoleCommand : IRequest<Result<int , string>>
    {
        public string Title { get; }
        public string Description { get; }

        public AddRoleCommand(string title, string description)
        {
            Title = title;
            Description = description;
        }

    }

    
        
}
