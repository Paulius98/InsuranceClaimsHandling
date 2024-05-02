using Claims.Application.Exceptions;
using Claims.Domain.Entities;
using Claims.Domain.Interfaces.Repositories;
using MediatR;

namespace Claims.Application.Queries.Covers;

public class GetCoverByIdQueryHandler : IRequestHandler<GetCoverByIdQuery, Cover>
{
    private readonly ICoverRepository _repository;

    public GetCoverByIdQueryHandler(ICoverRepository repository)
    {
        _repository = repository;
    }

    public async Task<Cover> Handle(GetCoverByIdQuery request, CancellationToken cancellationToken)
    {
        var cover = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (cover is null)
        {
            throw new CoverNotFoundException();
        }

        return cover;
    }
}
