using curewell.DAL.Contracts;
using curewell.Entity.Models;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace curewell.DAL.Repositories
{
    public class CureWellRepository : ICureWellRepository
    {
        private readonly IConfiguration _configuration;
        private readonly string connectionString;

        private readonly SqlCommand cmd;

        public CureWellRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            connectionString = _configuration.GetConnectionString("DefaultConnection") ?? string.Empty;
            cmd = new SqlCommand { CommandType = CommandType.StoredProcedure };
        }

        public List<Doctor> doctorList = new();
        public List<Specialization> specializationList = new();
        public List<Surgery> surgeryList = new();
        public List<DoctorSpecialization> doctorSpecializationList = new();


        public bool AddDoctor(Doctor dObj)
        {
            try
            {
                using SqlConnection con = new(connectionString);
                cmd.Connection = con;
                cmd.CommandText = "usp_insertDoctor";
                cmd.Parameters.AddWithValue("@doctorName", dObj.DoctorName);
                con.Open();
                var rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteDoctor(int id)
        {
            try
            {
                using SqlConnection con = new(connectionString);
                cmd.Connection = con;
                cmd.CommandText = "usp_deleteDoctor";
                cmd.Parameters.AddWithValue("@doctorID", id);
                con.Open();
                var rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<Doctor>? GetAllDoctors()
        {
            try
            {
                using (SqlConnection con = new(connectionString))
                {
                    cmd.Connection = con;
                    cmd.CommandText = "usp_getAllDoctors";
                    con.Open();
                    using SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Doctor doctor = new()
                        {
                            DoctorId = Convert.ToInt32(reader["DoctorID"]),
                            DoctorName = reader["DoctorName"].ToString() ?? string.Empty,
                        };

                        doctorList.Add(doctor);
                    }
                }
                return doctorList;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<Specialization>? GetAllSpecializations()
        {
            try
            {
                using (SqlConnection con = new(connectionString))
                {
                    cmd.Connection = con;
                    cmd.CommandText = "usp_getAllSpecializations";
                    con.Open();
                    using SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Specialization specialization = new()
                        {
                            SpecializationCode = reader["SpecializationCode"].ToString() ?? string.Empty,
                            SpecializationName = reader["SpecializationName"].ToString() ?? string.Empty,
                        };

                        specializationList.Add(specialization);
                    }
                }
                return specializationList;
            }
            catch (Exception)
            {

                return null;
            }

        }

        public List<Surgery>? GetAllSurgeryTypeForToday()
        {
            try
            {
                using (SqlConnection con = new(connectionString))
                {
                    cmd.Connection = con;
                    cmd.CommandText = "usp_getAllSurgeries";
                    con.Open();
                    using SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Surgery surgery = new()
                        {
                            SurgeryId = Convert.ToInt32(reader["SurgeryID"]),
                            DoctorId = Convert.ToInt32(reader["DoctorId"]),
                            SurgeryDate = Convert.ToDateTime(reader["SurgeryDate"]),
                            SurgeryCategory = reader["SurgeryCategory"].ToString() ?? string.Empty,
                            StartTime = Convert.ToDecimal(reader["StartTime"]),
                            EndTime = Convert.ToDecimal(reader["EndTime"])
                        };
                        surgeryList.Add(surgery);
                    }
                }
                return surgeryList;
            }
            catch (Exception)
            {
                return null;
            }

        }

        public List<DoctorSpecialization>? GetDoctorsBySpecialization(string specializationCode)
        {
            try
            {
                using (SqlConnection con = new(connectionString))
                {
                    cmd.Connection = con;
                    cmd.CommandText = "usp_getDoctorsBySpecialization";
                    cmd.Parameters.AddWithValue("@specializationCode", specializationCode);
                    con.Open();
                    using SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        DoctorSpecialization doctorSpecialization = new()
                        {
                            DoctorId = Convert.ToInt32(reader["DoctorID"]),
                            SpecializationCode = reader["SpecializationCode"].ToString() ?? string.Empty,
                            SpecializationDate = Convert.ToDateTime(reader["SpecializationDate"]),
                        };

                        doctorSpecializationList.Add(doctorSpecialization);
                    }
                }
                return doctorSpecializationList;
            }
            catch (Exception)
            {
                return null;
            }

        }

        public bool UpdateDoctorDetails(Doctor dObj)
        {
            try
            {
                using SqlConnection con = new(connectionString);
                cmd.Connection = con;
                cmd.CommandText = "usp_updateDoctor";
                cmd.Parameters.AddWithValue("@doctorID", dObj.DoctorId);
                cmd.Parameters.AddWithValue("@newName", dObj.DoctorName);
                con.Open();
                var rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool UpdateSurgery(Surgery sObj)
        {
            try
            {
                using SqlConnection con = new(connectionString);
                cmd.Connection = con;
                cmd.CommandText = "usp_updateSurgery";
                cmd.Parameters.AddWithValue("@surgeryID", sObj.SurgeryId);
                cmd.Parameters.AddWithValue("@startTime", sObj.StartTime);
                cmd.Parameters.AddWithValue("@endTime", sObj.EndTime);
                con.Open();
                var rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
