﻿using Ardalis.GuardClauses;
using CaWorkshop.Application.Common.Exceptions;
using CaWorkshop.Application.Common.Interfaces;
using CaWorkshop.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CaWorkshop.Application.TodoItems.Commands.DeleteTodoItem
{
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

            Guard.Against.NotFound(nameof(TodoItem), request.Id);

            _context.TodoItems.Remove(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
