using CaWorkshop.WebUI.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace CaWorkshop.WebUI.Data
{
    public static class ApplicationDbContextInitialiser
    {
        public static void Initialise(ApplicationDbContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.Migrate();

            var list = new TodoList
            {
                Title = "Todo List",
                Items =
                {
                    new Todolist { Title = "Make a todo list" },
                    new Todolist { Title = "Check off the first item" },
                    new Todolist { Title = "Realise you've already done two things on the list!"},
                    new Todolist { Title = "Reward yourself with a nice, long nap" },
                }
            };

            context.TodoLists.Add(list);
            context.SaveChanges();
        }
    }
}
