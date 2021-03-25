using CaWorkshop.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CaWorkshop.Infrastructure.Persistence
{
    public static class ApplicationDbContextInitialiser
    {
        public static void Initialise(ApplicationDbContext context)
        {
            context.Database.Migrate();

            var list = new TodoList
            {
                Title = "Todo List",
                Items =
                {
                    new TodoItem { Title = "Make a todo list" },
                    new TodoItem { Title = "Check off the first item" },
                    new TodoItem { Title = "Realise you've already done two things on the list!"},
                    new TodoItem { Title = "Reward yourself with a nice, long nap" },
                }
            };

            context.TodoLists.Add(list);
            context.SaveChanges();
        }
    }
}
