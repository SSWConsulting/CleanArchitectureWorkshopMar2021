using CaWorkshop.Application.Common.Interfaces;
using CaWorkshop.Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;

namespace CaWorkshop.Application.TodoLists.Commands.CreateTodoList
{
    public class CreateTodoListCommand : IRequest<int>
    {
        public string Title { get; set; }
    }

    public class CreateTodoListCommandValidator
        : AbstractValidator<CreateTodoListCommand>
    {
        private readonly IApplicationDbContext _context;

        public CreateTodoListCommandValidator(
            IApplicationDbContext context)
        {
            _context = context;

            RuleFor(v => v.Title)
                .MaximumLength(240)
                .NotEmpty()
                .MustAsync(BeUniqueTitle)
                    .WithErrorCode("IsUniqueTitle")
                    .WithMessage(command => $"The 'Title' ({command.Title}) must be unique.");
        }

        public async Task<bool> BeUniqueTitle(string title,
            CancellationToken cancellationToken)
        {
            return await _context.TodoLists
                .AllAsync(l => l.Title != title, cancellationToken);
        }
    }

    public class CreateTodoListCommandHandler
        : IRequestHandler<CreateTodoListCommand, int>
    {
        private readonly IApplicationDbContext _context;

        public CreateTodoListCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateTodoListCommand request,
            CancellationToken cancellationToken)
        {
            var entity = new TodoList();

            entity.Title = request.Title;

            _context.TodoLists.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
}
