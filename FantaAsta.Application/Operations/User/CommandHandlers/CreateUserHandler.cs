using FantaAsta.Application.Interfaces.Common;
using FantaAsta.Application.Operations.User.Commands;
using FantaAsta.Domain.Models;
using MediatR;

namespace FantaAsta.Application.Operations.User.CommandHandlers;

public class CreateUserHandler : IRequestHandler<CreateUser, UserEntity>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateUserHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<UserEntity> Handle(CreateUser request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}