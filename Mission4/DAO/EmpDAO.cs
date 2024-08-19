using Mission4.Entity;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Diagnostics;
using System.Text;


namespace Mission4.DAO
{
    public class EmpDAO : IDAO<Emp>
    {
        private static EmpDAO empDAO;
        private static readonly object lockObject = new object();

        private EmpDAO() { }

        public static EmpDAO GetInstance()
        {
            if (empDAO == null)
            {
                lock (lockObject)
                {
                    if (empDAO == null)
                    {
                        empDAO = new EmpDAO();
                    }
                }
            }
            return empDAO;
        }

        public List<Emp> GetAll()
        {
            List<Emp> empList = new List<Emp>();
            string sql = "SELECT * FROM Emp";

            using (var db = new DBHelper())
            {
                var con = db.Connection;
                con.Open();

                var cmd = new SqlCommand(sql, con);
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var emp = new Emp
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        EmpNo = reader["EmpNo"].ToString(),
                        Password = reader["Password"].ToString(),
                        Name = reader["Name"].ToString(),
                        Title = reader["Title"].ToString(),
                        HireDate = Convert.ToDateTime(reader["HireDate"]),
                        Salary = Convert.ToInt32(reader["Salary"]),
                        Bonus = reader["Bonus"] as int?,
                        ManagerId = reader["ManagerId"] as int?,
                        DeptId = reader["DeptId"] as int?
                    };

                    // Fetch department if DeptId is not null
                    if (emp.DeptId.HasValue)
                    {
                        var dept = DeptDAO.GetInstance().Get(emp.DeptId.Value);
                        emp.Dept = dept;
                    }

                    empList.Add(emp);
                }

                reader.Close();
            }

            return empList;
        }

        public Emp Get(int id)
        {
            Emp emp = null;
            string sql = "SELECT * FROM Emp WHERE Id = @Id";

            using (var db = new DBHelper())
            {
                var con = db.Connection;
                con.Open();

                var cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@Id", id);
                var reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    emp = new Emp
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        EmpNo = reader["EmpNo"].ToString(),
                        Password = reader["Password"].ToString(),
                        Name = reader["Name"].ToString(),
                        Title = reader["Title"].ToString(),
                        HireDate = Convert.ToDateTime(reader["HireDate"]),
                        Salary = Convert.ToInt32(reader["Salary"]),
                        Bonus = reader["Bonus"] as int?,
                        ManagerId = reader["ManagerId"] as int?,
                        DeptId = reader["DeptId"] as int?
                    };

                    // Fetch department if DeptId is not null
                    if (emp.DeptId.HasValue)
                    {
                        var dept = DeptDAO.GetInstance().Get(emp.DeptId.Value);
                        emp.Dept = dept;
                    }
                }

                reader.Close();
            }

            return emp;
        }

        public Emp Get(string empNo)
        {
            Emp emp = null;
            string sql = "SELECT * FROM Emp WHERE EmpNo = @EmpNo";

            using (var db = new DBHelper())
            {
                var con = db.Connection;
                con.Open();

                var cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@EmpNo", empNo);
                var reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    emp = new Emp
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        EmpNo = reader["EmpNo"].ToString(),
                        Password = reader["Password"].ToString(),
                        Name = reader["Name"].ToString(),
                        Title = reader["Title"].ToString(),
                        HireDate = Convert.ToDateTime(reader["HireDate"]),
                        Salary = Convert.ToInt32(reader["Salary"]),
                        Bonus = reader["Bonus"] as int?,
                        ManagerId = reader["ManagerId"] as int?,
                        DeptId = reader["DeptId"] as int?
                    };

                    // Fetch department if DeptId is not null
                    if (emp.DeptId.HasValue)
                    {
                        var dept = DeptDAO.GetInstance().Get(emp.DeptId.Value);
                        emp.Dept = dept;
                    }
                }

                reader.Close();
            }

            return emp;
        }

        public int Add(Emp emp)
        {
            int affectedRows = 0;
            string sql = "INSERT INTO Emp (EmpNo, Password, Name, Title, HireDate, Salary, Bonus, ManagerId, DeptId) VALUES (@EmpNo, @Password, @Name, @Title, @HireDate, @Salary, @Bonus, @ManagerId, @DeptId)";

            using (var db = new DBHelper())
            {
                var con = db.Connection;
                con.Open();

                var cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@EmpNo", emp.EmpNo);
                cmd.Parameters.AddWithValue("@Password", emp.Password);
                cmd.Parameters.AddWithValue("@Name", emp.Name);
                cmd.Parameters.AddWithValue("@Title", emp.Title);
                cmd.Parameters.AddWithValue("@HireDate", emp.HireDate);
                cmd.Parameters.AddWithValue("@Salary", emp.Salary);
                cmd.Parameters.AddWithValue("@Bonus", (object)emp.Bonus ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@ManagerId", (object)emp.ManagerId ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@DeptId", (object)emp.DeptId ?? DBNull.Value);
                affectedRows = cmd.ExecuteNonQuery();
            }

            return affectedRows;
        }

        public int Update(Emp emp)
        {
            int affectedRows = 0;
            string sql = "UPDATE Emp SET EmpNo = @EmpNo, Password = @Password, Name = @Name, Title = @Title, HireDate = @HireDate, Salary = @Salary, Bonus = @Bonus, ManagerId = @ManagerId, DeptId = @DeptId WHERE Id = @Id";

            using (var db = new DBHelper())
            {
                var con = db.Connection;
                con.Open();

                var cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@Id", emp.Id);
                cmd.Parameters.AddWithValue("@EmpNo", emp.EmpNo);
                cmd.Parameters.AddWithValue("@Password", emp.Password);
                cmd.Parameters.AddWithValue("@Name", emp.Name);
                cmd.Parameters.AddWithValue("@Title", emp.Title);
                cmd.Parameters.AddWithValue("@HireDate", emp.HireDate);
                cmd.Parameters.AddWithValue("@Salary", emp.Salary);
                cmd.Parameters.AddWithValue("@Bonus", (object)emp.Bonus ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@ManagerId", (object)emp.ManagerId ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@DeptId", (object)emp.DeptId ?? DBNull.Value);
                affectedRows = cmd.ExecuteNonQuery();
            }

            return affectedRows;
        }

        public int Delete(int id)
        {
            int affectedRows = 0;
            string sql = "DELETE FROM Emp WHERE Id = @Id";

            using (var db = new DBHelper())
            {
                var con = db.Connection;
                con.Open();

                // Start a transaction
                using (var transaction = con.BeginTransaction())
                {
                    try
                    {
                        // Delete Certificates first
                        var certCmd = new SqlCommand("DELETE FROM Certificate WHERE EmpId = @Id", con, transaction);
                        certCmd.Parameters.AddWithValue("@Id", id);
                        affectedRows += certCmd.ExecuteNonQuery();

                        // Then delete Employee
                        var empCmd = new SqlCommand(sql, con, transaction);
                        empCmd.Parameters.AddWithValue("@Id", id);
                        affectedRows += empCmd.ExecuteNonQuery();

                        // Commit transaction
                        transaction.Commit();
                    }
                    catch
                    {
                        // Rollback transaction if an error occurs
                        transaction.Rollback();
                        throw;
                    }
                }
            }

            return affectedRows;
        }
    }
}



