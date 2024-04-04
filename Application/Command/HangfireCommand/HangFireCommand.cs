using MediatR;

namespace Application.Command.HangfireCommand
{
    public class HangFireCommand : IRequest<string>
    {
        public string? Subject { get; set; }
        public string? Body { get; set; }
    }
}
