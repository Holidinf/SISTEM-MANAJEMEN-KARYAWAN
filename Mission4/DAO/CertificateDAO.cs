using Mission4.Entity;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;


namespace Mission4.DAO
{
    public class CertificateDAO : IDAO<Certificate>
    {
        private static CertificateDAO _certificateDAO;
        private static readonly object _lock = new object();

        private CertificateDAO() { }

        public static CertificateDAO GetInstance()
        {
            lock (_lock)
            {
                if (_certificateDAO == null)
                {
                    _certificateDAO = new CertificateDAO();
                }
            }
            return _certificateDAO;
        }

        public List<Certificate> GetAll(int empId)
        {
            List<Certificate> certificateList = new List<Certificate>();
            string sql = "SELECT * FROM Certificate WHERE EmpId = @EmpId";

            using (var db = new DBHelper())
            {
                var con = db.Connection;
                con.Open();
                var cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@EmpId", empId);

                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var certificate = new Certificate
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        EmpId = Convert.ToInt32(reader["EmpId"]),
                        CertificateName = reader["CertificateName"].ToString()
                    };

                    certificateList.Add(certificate);
                }

                reader.Close();
            }

            return certificateList;
        }

        public Certificate Get(int id)
        {
            Certificate certificate = null;
            string sql = "SELECT * FROM Certificate WHERE Id = @Id";

            using (var db = new DBHelper())
            {
                var con = db.Connection;
                con.Open();
                var cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@Id", id);

                var reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    certificate = new Certificate
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        EmpId = Convert.ToInt32(reader["EmpId"]),
                        CertificateName = reader["CertificateName"].ToString()
                    };
                }

                reader.Close();
            }

            return certificate;
        }

        public int Add(Certificate certificate)
        {
            int affectedRows = 0;
            string sql = "INSERT INTO Certificate (EmpId, CertificateName) VALUES (@EmpId, @CertificateName)";

            using (var db = new DBHelper())
            {
                var con = db.Connection;
                con.Open();
                var cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@EmpId", certificate.EmpId);
                cmd.Parameters.AddWithValue("@CertificateName", certificate.CertificateName);

                affectedRows = cmd.ExecuteNonQuery();
            }

            return affectedRows;
        }

        public int Update(Certificate certificate)
        {
            int affectedRows = 0;
            string sql = "UPDATE Certificate SET EmpId = @EmpId, CertificateName = @CertificateName WHERE Id = @Id";

            using (var db = new DBHelper())
            {
                var con = db.Connection;
                con.Open();
                var cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@Id", certificate.Id);
                cmd.Parameters.AddWithValue("@EmpId", certificate.EmpId);
                cmd.Parameters.AddWithValue("@CertificateName", certificate.CertificateName);

                affectedRows = cmd.ExecuteNonQuery();
            }

            return affectedRows;
        }

        public int Delete(int id)
        {
            int affectedRows = 0;
            string sql = "DELETE FROM Certificate WHERE Id = @Id";

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

        public List<Certificate> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}

