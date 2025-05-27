
using Application.Interfaces;
using Domain.Entities;
using Domain.ValueObject;
using MediatR;
using Shared.ResultManagement;

namespace Application.Commands.Permissions
{
    public class EditPermissionHandler : IRequestHandler<EditPermissionCommand, Result<Guid, string>>
    {
        private readonly IPermissionRepository _permissionRepository;

        public EditPermissionHandler(IPermissionRepository permissionRepository)
        {
            _permissionRepository = permissionRepository;
        }

        public async Task<Result<Guid, string>> Handle(EditPermissionCommand request, CancellationToken cancellationToken)
        {
            var permission = await _permissionRepository.GetByIdAsync(request.Id, cancellationToken);
            if (permission == null)
                return Result<Guid, string>.Failure("Permission not found.");

            var permissionNameResult = PermissionName.Create(request.Name);
            if (!permissionNameResult.IsSuccess)
                return Result<Guid, string>.Failure(permissionNameResult.Error!);

            // تغییر مقادیر
            permission.SetPermissionId(string.IsNullOrWhiteSpace(request.PermissionId)
                ? null
                : Guid.TryParse(request.PermissionId, out var pid) ? pid : null);

            permission.ToggleActive(); // تغییر وضعیت به مقدار جدید
            if (permission.Active != request.Active)
                permission.ToggleActive();

            permission.UpdateOrdering(request.Ordering);

            // تغییر نام و توضیحات فقط با استفاده از Reflection یا متد اضافه در دامین کلاس
            typeof(Permission).GetProperty("Name")?.SetValue(permission, permissionNameResult.Value);
            typeof(Permission).GetProperty("Description")?.SetValue(permission, request.Description);

            await _permissionRepository.UpdateAsync(permission, cancellationToken);

            return Result<Guid, string>.Success(permission.Id);
        }
    }
}
