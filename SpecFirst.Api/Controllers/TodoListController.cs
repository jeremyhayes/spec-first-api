using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace SpecFirst.Api.Controllers
{
    public class TodoListController : ITodoListController
    {
        private readonly ILogger<TodoListController> _logger;

        public TodoListController(ILogger<TodoListController> logger)
        {
            _logger = logger;
        }

        public Task<ObservableCollection<TodoList>> TodoListGetAsync()
        {
            _logger.LogInformation("get lists");

            var result = new ObservableCollection<TodoList>
            {
                new TodoList
                {
                    Id = 1,
                    Title = "Prove spec-first API design."
                }
            };
            
            return Task.FromResult(result);
        }

        public Task TodoListPostAsync(TodoList body)
        {
            _logger.LogInformation("create list");
            // ...
            return Task.CompletedTask;
        }
    }
}
