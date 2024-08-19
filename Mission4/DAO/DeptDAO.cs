using Mission4.Entity;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;

namespace Mission4.DAO
{
    public class DeptDAO : IDAO<Dept>
    {
        private static DeptDAO _deptDAO;
        private static readonly object _lock = new object();

        private DeptDAO() { }

        public static DeptDAO GetInstance()
        {
            lock (_lock)
            {
                if (_deptDAO == null)
                {
                    _deptDAO = new DeptDAO();
                }
            }
            return _deptDAO;
        }

        public List<Dept> GetAll()
        {
            List<Dept> depts = new List<Dept>();
            string sql = "SELECT * FROM Dept";

            using (var db = new DBHelper())
            {
                var con = db.Connection;
                con.Open();
                var cmd = new SqlCommand(sql, con);
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var dept = new Dept
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        DeptName = reader["DeptName"].ToString(),
                        LocId = Convert.ToInt32(reader["LocId"])
                    };

                    depts.Add(dept);
                }

                reader.Close();
            }

            return depts;
        }

        public Dept Get(int id)
        {
            Dept dept = null;
            string sql = "SELECT * FROM Dept WHERE Id = @Id";

            using (var db = new DBHelper())
            {
                var con = db.Connection;
                con.Open();
                var cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@Id", id);

                var reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    dept = new Dept
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        DeptName = reader["DeptName"].ToString(),
                        LocId = Convert.ToInt32(reader["LocId"])
                    };
                }

                reader.Close();
            }

            return dept;
        }

        public int Add(Dept dept)
        {
            int affectedRows = 0;
            string sql = "INSERT INTO Dept (DeptName, LocId) VALUES (@DeptName, @LocId)";

            using (var db = new DBHelper())
            {
                var con = db.Connection;
                con.Open();
                var cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@DeptName", dept.DeptName);
                cmd.Parameters.AddWithValue("@LocId", dept.LocId);

                affectedRows = cmd.ExecuteNonQuery();
            }

            return affectedRows;
        }

        public int Update(Dept dept)
        {
            int affectedRows = 0;
            string sql = "UPDATE Dept SET DeptName = @DeptName, LocId = @LocId WHERE Id = @Id";

            using (var db = new DBHelper())
            {
                var con = db.Connection;
                con.Open();
                var cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@Id", dept.Id);
                cmd.Parameters.AddWithValue("@DeptName", dept.DeptName);
                cmd.Parameters.AddWithValue("@LocId", dept.LocId);

                affectedRows = cmd.ExecuteNonQuery();
            }

            return affectedRows;
        }

        public int Delete(int id)
        {
            int affectedRows = 0;
            string sql = "DELETE FROM Dept WHERE Id = @Id";

            using (var db = new DBHelper())
            {
                var con = db.Connection;
                con.Open();
                var cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@Id", id);

                affectedRows = cmd.ExecuteNonQuery();
            }

            return affectedRows;
        }
    }
}



