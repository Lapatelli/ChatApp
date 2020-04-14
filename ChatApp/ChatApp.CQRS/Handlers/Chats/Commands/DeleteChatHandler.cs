using ChatApp.CQRS.Commands.Chats;
using ChatApp.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ChatApp.CQRS.Handlers.Chats.Commands
{
    public class DeleteChatHandler : IRequestHandler<DeleteChatCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteChatHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(DeleteChatCommand command, CancellationToken cancellationToken)
        {
            await _unitOfWork.ChatRepository.DeleteChatAsync(command.ChatId);

            return Unit.Value;
        }
    }
}
