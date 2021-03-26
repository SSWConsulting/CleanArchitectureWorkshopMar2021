using System.Threading;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using CaWorkshop.Application.Common.Interfaces;
using CaWorkshop.Application.Common.Security;
using MediatR;

namespace CaWorkshop.Application.TodoItems.Commands.DeleteTodoItem
{
    [Authorize]
    public class DeleteTodoItemCommand : IRequest
    {
        public long Id { get; set; }
    }

    public class DeleteTodoItemCommandHandler
        : IRequestHandler<DeleteTodoItemCommand>
    {
        private readonly IApplicationDbContext _context;

        public DeleteTodoItemCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteTodoItemCommand request,
            CancellationToken cancellationToken)
        {
            var entity = await _context.TodoItems.FindAsync(request.Id);

            Guard.Against.NotFound(entity, request.Id);

            _context.TodoItems.Remove(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
