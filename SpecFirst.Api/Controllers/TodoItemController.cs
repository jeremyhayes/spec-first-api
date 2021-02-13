using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace SpecFirst.Api.Controllers
{
    public class TodoItemController : ITodoItemController
    {
        private readonly ILogger<TodoItemController> _logger;

        public TodoItemController(ILogger<TodoItemController> logger)
        {
            _logger = logger;
        }

        public Task<ObservableCollection<TodoItem>> TodoItemAsync(long todoListId)
        {
            _logger.LogInformation("get items for list");

            var result = new ObservableCollection<TodoItem>
            {
                new TodoItem
                {
                    Id = 1,
                    Name = "Create spec-first POC.",
                    IsComplete = true,
                },
                new TodoItem
                {
                    Id = 2,
                    Name = "???"
                },
                new TodoItem
                {
                    Id = 3,
                    Name = "Profit."
                }
            };
            
            return Task.FromResult(result);
        }
    }
}
