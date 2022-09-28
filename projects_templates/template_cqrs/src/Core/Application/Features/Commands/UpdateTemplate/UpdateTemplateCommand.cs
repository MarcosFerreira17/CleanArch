using MediatR;

namespace Application.Features.Commands.UpdateTemplate;

public class UpdateTemplateCommand : IRequest
{
    public long Id { get; set; }
    public string Example { get; set; }
}
