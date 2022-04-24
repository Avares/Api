using Api.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Data.Interface
{
    public interface IToDo
    {
        List<ToDo> GetAllToDos();
        ToDo GetToDo(int ToDoId);
        List<ToDo> GetToDoByDate(DateTime dt);
        void CreateToDo(ToDo todo);
        void UpdateToDo(ToDo todo);
        void SetPercentage(int ToDoId, int percent);
        void DeleteToDo(int ToDoId);
        void MarkAsDone(int ToDoId);
    }
}
