using Claims.Domain.Entities;
using Claims.Domain.Interfaces.Repositories;
using MediatR;

namespace Claims.Application.Commands.Covers;

public class CreateCoverCommandHandler : IRequestHandler<CreateCoverCommand, Cover>
{
    private readonly ICoverRepository _repository;

    public CreateCoverCommandHandler(ICoverRepository repository)
    {
        _repository = repository;
    }

    public async Task<Cover> Handle(CreateCoverCommand request, CancellationToken cancellationToken)
    {
        var cover = Cover.Create(request.StartDate, request.EndDate, request.Type);
        cover.ValidateDateRange();

        await _repository.AddAsync(cover, cancellationToken);
        return cover;
    }
}
