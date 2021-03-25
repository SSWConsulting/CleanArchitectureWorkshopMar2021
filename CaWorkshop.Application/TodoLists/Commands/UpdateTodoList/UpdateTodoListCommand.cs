using System.Threading;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using CaWorkshop.Application.Common.Interfaces;
using MediatR;

namespace CaWorkshop.Application.TodoLists.Commands.UpdateTodoList
{
    public class UpdateTodoListCommand : IRequest
    {
        public int Id { get; set; }

        public string Title { get; set; }
    }

    public class UpdateTodoListCommandHandler
        : IRequestHandler<UpdateTodoListCommand>
    {
        private readonly IApplicationDbContext _context;

        public UpdateTodoListCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateTodoListCommand request,
            CancellationToken cancellationToken)
        {
            var entity = await _context.TodoLists.FindAsync(new object[] { request.Id }, cancellationToken);

            Guard.Against.NotFound(entity, request.Id);

            entity.Title = request.Title;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
