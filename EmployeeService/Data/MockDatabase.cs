using System.Collections.Generic;
using EmployeeService.Models;

namespace EmployeeService.Data
{
    public class MockDatabase : IData
    {
        private static Dictionary<int, Employee> _map;

        public MockDatabase()
        {
            _map = new Dictionary<int, Employee>();
            _map[1] = new Employee { Id=1, FirstName = "Jack", LastName = "Ryan", PhoneNumber = "760-000-0000" };
            _map[2] = new Employee { Id=2, FirstName = "Jason", LastName = "Borne", PhoneNumber = "760-000-0001" };
            _map[3] = new Employee { Id=3, FirstName = "James", LastName = "Bond", PhoneNumber = "760-000-0010" };
        }
        public Employee Add(Employee emp)
        {
            int nextIndex = 0;
            foreach (var item in _map)
            {
                nextIndex = nextIndex < item.Key ? item.Key : nextIndex;
            }
            nextIndex++;

            emp.Id = nextIndex;
            _map[nextIndex] = emp;

            return emp;
        }

        public bool Delete(int Id)
        {
            if (_map.ContainsKey(Id))
            {
                _map.Remove(Id);
                return true;
            }

            return false ;
        }

        public IEnumerable<Employee> GetAll()
        {
            List<Employee> list = new List<Employee>();

            foreach (var item in _map)
            {
                list.Add(item.Value);
            }

            return (list);
        }

        public Employee GetById(int Id)
        {
            if (_map.ContainsKey(Id))
            {
                return _map[Id];
            }

            return (new Employee());
        }
    }
}