using System.Collections.Generic;
using Oracle.ManagedDataAccess.Client;
using EmployeeService.Models;
using System;
using System.Data;

namespace EmployeeService.Data
{
    public class OracleDatabase : IData
    {
        private static OracleConnection _con;

        public OracleDatabase()
        {
        }
        public Employee Add(Employee emp)
        {
            string connstr = "User Id=SYSTEM;Password=OracleDB1;Data Source=172.16.163.129:1521/XE;";
            _con = new OracleConnection(connstr);

            using (_con)
            {
                using (OracleCommand cmd = _con.CreateCommand())
                {
                    cmd.BindByName = true;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "add_employee";
                    cmd.Parameters.Add("fn", OracleDbType.Varchar2).Value = emp.FirstName;
                    cmd.Parameters.Add("ln", OracleDbType.Varchar2).Value = emp.LastName;
                    cmd.Parameters.Add("pn", OracleDbType.Varchar2).Value = emp.PhoneNumber;

                    try
                    {
                        _con.Open();
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }

            return emp;
        }

        public bool Delete(int Id)
        {
            string connstr = "User Id=SYSTEM;Password=OracleDB1;Data Source=172.16.163.129:1521/XE;";
            _con = new OracleConnection(connstr);

            using (_con)
            {
                using (OracleCommand cmd = _con.CreateCommand())
                {
                    cmd.BindByName = true;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "delete_employee";
                    cmd.Parameters.Add("ident", OracleDbType.Int32).Value = Id;

                    try
                    {
                        _con.Open();
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }

            return true;
        }

        public IEnumerable<Employee> GetAll()
        {
            List<Employee> list = new List<Employee>();

            string connstr = "User Id=SYSTEM;Password=OracleDB1;Data Source=172.16.163.129:1521/XE;";
            _con = new OracleConnection(connstr);

            using (_con)
            {
                using (OracleCommand cmd = _con.CreateCommand())
                {
                    try
                    {
                        _con.Open();
                        cmd.BindByName = true;

                        cmd.CommandText = "select id, first_name, last_name, phone_number from SYSTEM.get_all_employees()";

                        //         // Assign id to the department number 50 
                        //         OracleParameter id = new OracleParameter("id", 50);
                        //         cmd.Parameters.Add(id);

                        OracleDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            Employee emp = new Employee();
                            emp.Id = reader.GetInt32(0);
                            emp.FirstName = reader.GetString(1);
                            emp.LastName = reader.GetString(2);
                            emp.PhoneNumber = reader.GetString(3);
                            list.Add(emp);
                        }

                        reader.Dispose();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }

            return (list);
        }

        public Employee GetById(int Id)
        {
            throw new System.NotImplementedException();
        }
    }
}