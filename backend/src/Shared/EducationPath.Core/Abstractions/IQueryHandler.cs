using CSharpFunctionalExtensions;
using EducationPath.SharedKernel.Errors;

namespace EducationPath.Core.Abstractions;

public interface IQueryHandler<TResponse, in TQuery>
    where TQuery : IQuery
{
    Task<TResponse> Handle(TQuery query, CancellationToken cancellationToken = default);
}

public interface IQueryHandlerAsync<TResponse>
{
    Task<TResponse> Handle(CancellationToken cancellationToken = default);
}

public interface IQueryHandler<TResponse>
{
    TResponse Handle();
}


public interface IQueryHandlerWithResult<TResponse, in TQuery>
    where TQuery : IQuery
{
    Task<Result<TResponse, ErrorList>> Handle(TQuery query, CancellationToken cancellationToken = default);
}