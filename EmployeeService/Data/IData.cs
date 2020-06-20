using System.Collections.Generic;
using EmployeeService.Models;

namespace EmployeeService.Data
{
    public interface IData
    {
        public IEnumerable<Employee> GetAll();
        public Employee GetById(int Id);
        public Employee Add(Employee emp);
        public bool Delete(int Id);
    }
}