using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.IO;
using System.Windows;
using PrescriptionManagement.Model;

namespace PrescriptionManagement.Services
{
    internal class DatabasesManager
    {
        private readonly string _connStr;
        private readonly string _dbFile = "data.db";
        private readonly string _dbFilePath;
        public DatabasesManager()
        {
            var appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            _dbFilePath = Path.Combine(appData, "pms", _dbFile);
            Directory.CreateDirectory(Path.Combine(appData, "pms"));
            _connStr = $"Data Source={_dbFilePath};";


            using (var conn = new SQLiteConnection(_connStr))
            {
                conn.Open();
                string createTableQuery = @"CREATE TABLE IF NOT EXISTS Patients (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        Name TEXT NOT NULL,
                        Age INTEGER NOT NULL,
                        Gender TEXT,
                        Date DATE
                    )";
                using (var cmd = new SQLiteCommand(createTableQuery, conn))
                {
                    cmd.ExecuteNonQuery();
                }
                conn.Close();
            }
        }
            public void AddPatient(Patient patient)
            {
                using (var conn = new SQLiteConnection(_connStr))
                {
                    conn.Open();

                    string insertQuery = @"
                        INSERT INTO Patients (Name, Age, Gender, Date)
                        VALUES (@Name, @Age, @Gender, @Date)";
                    using (var cmd = new SQLiteCommand(insertQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@Name", patient.Name);
                        cmd.Parameters.AddWithValue("@Age", patient.Age);
                        cmd.Parameters.AddWithValue("@Gender", patient.Gender);
                        cmd.Parameters.AddWithValue("@Date", patient.Date.ToString("yyyy-MM-dd"));
                        cmd.ExecuteNonQuery();
                    }

                    conn.Close();
                }
            }

        public List<Patient> GetAllPatients()
        {
            var patients = new List<Patient>();
            using (var conn = new SQLiteConnection(_connStr))
            {
                conn.Open();
                string selectQuery = "select * from Patients;";
                using(var cmd = new SQLiteCommand(selectQuery, conn))
                {
                    using(var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            patients.Add(new Patient()
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                Name = reader["Name"].ToString(),
                                Age = Convert.ToInt32(reader["Age"]),
                                Gender = reader["Gender"].ToString(),
                                Date = DateTime.Parse(reader["Date"].ToString())
                            });
                        }
                    }
                }
                conn.Close();
            }
            return patients;
        }

        public void DeletePatient(Patient patient)
        {
            using (var conn = new SQLiteConnection(_connStr))
            {
                conn.Open();
                string deleteQuery = @"DELETE FROM Patients WHERE Id = @Id";
                using (var cmd = new SQLiteCommand(deleteQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", patient.Id);
                    cmd.ExecuteNonQuery();
                }
                conn.Close();
            }
        }
    }

}




    

