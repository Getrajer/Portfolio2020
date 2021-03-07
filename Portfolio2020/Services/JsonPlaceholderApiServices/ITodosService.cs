using Portfolio2020.Models.JsonPlaceholderApiModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portfolio2020.Services.JsonPlaceholderApiServices
{
    public interface ITodosService
    {
        Task<IEnumerable<JPTodo>> GetAllTodos();
        Task<JPTodo> GetTodoById(int id);
        Task<IEnumerable<JPTodo>> GetTodosByUserId(int userId);
        Task<JPTodo> AddNewTodo(JPTodo newTodo);
        Task<JPTodo> UpdateTodo(JPTodo todoToUpdate);
        Task DeleteTodo(int toDoId);
    }
}
