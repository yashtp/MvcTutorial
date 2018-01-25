using Helper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;

namespace DataAccessLayer
{
    public class EmpDataAccess
    {
        string a = ConfigurationManager.ConnectionStrings["EmployeeContext"].ConnectionString;
        public string _connectionstring = @"Data Source=MYNEWPC\SQLEXPRESS;Initial Catalog=EmployeeInfo;Integrated Security=True;Pooling=False";

        public List<Employee> GetEmployees()
        {
            var list = new List<Employee>();
            SqlConnection conn = new SqlConnection(a);
            try
            {
                conn.Open();
                var cmd = new SqlCommand(@"select EmployeeId,Name,Gender,City from dbo.tblemployee", conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var temp = new Employee
                    {
                        EmployeeId = reader.GetInt32(0),
                        Name = reader.GetString(reader.GetOrdinal("Name")),
                        Gender = reader.GetString(reader.GetOrdinal("Gender")),
                        City = reader.GetString(reader.GetOrdinal("City"))
                    };
                    list.Add(temp);
                }
            }
            catch (Exception e)
            {
                return new List<Employee>();
            }
            finally
            {
                conn.Close();
            }
            return list;
        }

        public List<Employee> GetEmployees(int? id)
        {
            var list = new List<Employee>();
            SqlConnection conn = new SqlConnection(a);
            try
            {
                conn.Open();
                var cmd = new SqlCommand(@"select * from dbo.tblemployee inner join tbldepartment on tblemployee.DepartmentId = tbldepartment.Id
where tbldepartment.Id = @id", conn);
                cmd.Parameters.AddWithValue("@id", id);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var temp = new Employee
                    {
                        EmployeeId = reader.GetInt32(0),
                        Name = reader.GetString(reader.GetOrdinal("Name")),
                        Gender = reader.GetString(reader.GetOrdinal("Gender")),
                        City = reader.GetString(reader.GetOrdinal("City"))
                    };
                    list.Add(temp);
                }
            }
            catch (Exception e)
            {
                return new List<Employee>();
            }
            finally
            {
                conn.Close();
            }
            return list;
        }

        public bool AddEmployee(Employee employee)
        {
            if (string.IsNullOrEmpty(employee.Name) || string.IsNullOrEmpty(employee.Gender) || string.IsNullOrEmpty(employee.City))
            {
                return false;
            }
            SqlConnection conn = new SqlConnection(a);
            try
            {
                conn.Open();
                var cmd = new SqlCommand(@"insert into dbo.tblemployee (Name,Gender,City) 
                values (@name,@gender,@city);", conn);
                cmd.Parameters.AddWithValue("@name", employee.Name);
                cmd.Parameters.AddWithValue("@gender", employee.Gender);
                cmd.Parameters.AddWithValue("@city", employee.City);
                var count = cmd.ExecuteNonQuery();
                if (count != 0)
                {
                    return true;
                }
                return false;

            }
            catch (Exception e)
            {
                return false;
            }
            finally
            {
                conn.Close();
            }
        }

        public bool UpdateEmployee(Employee employee)
        {
            SqlConnection conn = new SqlConnection(a);
            try
            {
                conn.Open();
                var cmd = new SqlCommand(@"update dbo.tblemployee set Name= @name,Gender=@gender,City=@city where EmployeeId = @id;", conn);
                cmd.Parameters.AddWithValue("@id", employee.EmployeeId);
                cmd.Parameters.AddWithValue("@name", employee.Name);
                cmd.Parameters.AddWithValue("@gender", employee.Gender);
                cmd.Parameters.AddWithValue("@city", employee.City);
                var count = cmd.ExecuteNonQuery();
                if (count != 0)
                {
                    return true;
                }
                return false;

            }
            catch (Exception e)
            {
                return false;
            }
            finally
            {
                conn.Close();
            }
        }

        public bool DeleteEmployee(int id)
        {
            SqlConnection conn = new SqlConnection(a);
            try
            {
                conn.Open();
                var cmd = new SqlCommand(@"delete from dbo.tblemployee where EmployeeId = @id",conn);
                cmd.Parameters.AddWithValue("@id", id);
                var count = cmd.ExecuteNonQuery();
                if (count != 0)
                {
                    return true;
                }
                return false;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        public ObservableCollection<DepartmentTotals> GetDepartmentTotals()
        {
            var list = new ObservableCollection<DepartmentTotals>();
            SqlConnection conn = new SqlConnection(a);
            try
            {
                conn.Open();
                var cmd = new SqlCommand(@"SELECT
  tbldepartment.Name,
  Count(tblemployee.DepartmentId) AS Count_dep
FROM
  tbldepartment
  INNER JOIN tblemployee ON tblemployee.DepartmentId = tbldepartment.Id
GROUP BY
tbldepartment.Name order by Count_dep desc", conn);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var temp = new DepartmentTotals
                    {
                        Name = reader.GetString(reader.GetOrdinal("Name")),
                        Total = reader.GetInt32(reader.GetOrdinal("Count_dep"))
                    };

                    list.Add(temp);
                }

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                conn.Close();
            }
            return list;
        }
    }
}
