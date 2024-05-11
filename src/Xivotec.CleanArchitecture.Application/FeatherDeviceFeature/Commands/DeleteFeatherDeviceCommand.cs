using AutoMapper;
using MediatR;
using Xivotec.CleanArchitecture.Application.Common.Persistence.Interfaces;
using Xivotec.CleanArchitecture.Application.FeatherDeviceFeature.Dtos;
using Xivotec.CleanArchitecture.Domain.FeatherDeviceAggregate.Entities;

namespace Xivotec.CleanArchitecture.Application.FeatherDeviceFeature.Commands;

/// <summary>
/// Command which deletes the requested <see cref="FeatherDeviceDto"/> from the repository.
/// </summary>
/// <param name="device">The <see cref="FeatherDeviceDto"/> to be deleted.</param>
public record DeleteFeatherDeviceCommand(FeatherDeviceDto device) : IRequest;

public class DeleteFeatherDeviceHandler : IRequestHandler<DeleteFeatherDeviceCommand>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteFeatherDeviceHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(DeleteFeatherDeviceCommand request, CancellationToken cancellationToken)
    {
        var itemToDelete = _mapper.Map<FeatherDevice>(request.device);

        var repo = _unitOfWork.GetRepository<FeatherDevice>();
        await repo.DeleteAsync(itemToDelete);
    }
}
