using Claims.Domain.Entities;
using Claims.Domain.Interfaces.Repositories;
using MediatR;

namespace Claims.Application.Queries.Covers;

public class GetCoversQueryHandler : IRequestHandler<GetCoversQuery, IEnumerable<Cover>>
{
    private readonly ICoverRepository _repository;

    public GetCoversQueryHandler(ICoverRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Cover>> Handle(GetCoversQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAllAsync(cancellationToken);
    }
}
