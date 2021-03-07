using Microsoft.AspNetCore.Components;
using Portfolio2020.Models.JsonPlaceholderApiModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Portfolio2020.Services.JsonPlaceholderApiServices
{
    public class ServiceTodos : ITodosService
    {
        private readonly HttpClient httpClient;

        public ServiceTodos(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<JPTodo> AddNewTodo(JPTodo newTodo)
        {
            return await httpClient.PostJsonAsync<JPTodo>($"todos", newTodo);
        }

        public async Task DeleteTodo(int toDoId)
        {
            await httpClient.DeleteAsync($"todos/{toDoId}");
        }

        public async Task<IEnumerable<JPTodo>> GetAllTodos()
        {
            return await httpClient.GetJsonAsync<JPTodo[]>($"todos");
        }

        public async Task<JPTodo> GetTodoById(int id)
        {
            return await httpClient.GetJsonAsync<JPTodo>($"todos/{id}");
        }

        public async Task<IEnumerable<JPTodo>> GetTodosByUserId(int userId)
        {
            return await httpClient.GetJsonAsync<JPTodo[]>($"todos/{userId}/users");
        }

        public async Task<JPTodo> UpdateTodo(JPTodo todoToUpdate)
        {
            return await httpClient.PutJsonAsync<JPTodo>($"todos/{todoToUpdate.Id}", todoToUpdate);
        }
    }
}
