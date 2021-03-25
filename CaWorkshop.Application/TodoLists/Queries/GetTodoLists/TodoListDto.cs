using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using CaWorkshop.Domain.Entities;

namespace CaWorkshop.Application.TodoLists.Queries.GetTodoLists
{
    public class TodoListDto
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public IList<TodoItemDto> Items { get; set; }

        public static Expression<Func<TodoList, TodoListDto>> Projection
        {
            get
            {
                return list => new TodoListDto
                {
                    Id = list.Id,
                    Title = list.Title,
                    Items = list.Items.AsQueryable()
                        .Select(TodoItemDto.Projection)
                        .ToList()
                };
            }
        }
    }
}
