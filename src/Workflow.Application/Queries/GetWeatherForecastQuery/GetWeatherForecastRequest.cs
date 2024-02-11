using MediatR;

namespace Workflow.Application.Queries.GetWeatherForecastQuery;

public record GetWeatherForecastRequest : IRequest<IEnumerable<GetWeatherForecastResponse>> { }
