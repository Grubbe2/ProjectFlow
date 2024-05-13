using CharlotteSchou_Booking.Viewmodel;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;

namespace CharlotteSchou_Booking.Model.Persistens
{
    public class JobfunctionRepo : Repo<Jobfunction>
    {
        private List<Jobfunction> jobfunctions = new List<Jobfunction>();

        public JobfunctionRepo()
        {
            // refers to the connectionstring in the apsettings.json file
            IConfigurationRoot config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            ConnectionString = config.GetConnectionString("MyDBConnection");
            InitializeRepository();
        }

        //Establishes connection to the database
        public override void InitializeRepository()
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT JobfunctionId, Name, Description from JOBFUNCTION", con);
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {

                        Jobfunction jobfunction = new Jobfunction()
                        {
                            JobfunctionId = int.Parse(dr["JobfunctionId"].ToString()),
                            Name = dr["Name"].ToString(),
                            Description = dr["Description"].ToString(),

                        };
                        jobfunctions.Add(jobfunction);
                    }

                }
            }
        }

        //Creates a Jobfunction object in the database
        public void Create(Jobfunction jobfunctiontobeadded)
        {

            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open(); SqlCommand cmd = new SqlCommand("INSERT INTO JOBFUNCTION (Name, CustomerType) "
                    + "VALUES(@Name, @CustomerType)" + "SELECT @@IDENTITY", con);
                cmd.Parameters.Add("@Name", SqlDbType.NVarChar).Value = jobfunctiontobeadded.Name;
                cmd.Parameters.Add("@CustomerType", SqlDbType.NVarChar).Value = jobfunctiontobeadded.Description;

                cmd.ExecuteNonQuery();
            }
            jobfunctions.Add(jobfunctiontobeadded);
        }

        //returns the list of all jobfunctions
        public override List<Jobfunction> LoadAll()
        {
            return jobfunctions;
        }

        //returns a jobfunction object with a specific JobfunctionId
        public override Jobfunction Load(int JobfunctionId)
        {
            Jobfunction result = new();
            foreach (Jobfunction a in jobfunctions)
            {
                if (a.JobfunctionId == JobfunctionId)
                {
                    result = a;
                    break;
                }

            }
            return result;
        }

        //removes a Jobfunction object from the database
        public override void Remove(int jobfunctionId)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("DELETE FROM JOBFUNCTION WHERE JobfunctionId = @JobfunctionId", con);
                cmd.Parameters.Add("@JobfunctionId", SqlDbType.Int).Value = jobfunctionId;
                cmd.ExecuteNonQuery();
            }
            jobfunctions.Remove(Load(jobfunctionId));
        }

        //updates one or more attributes for a Jobfunction Object
        public void Update(int jobfunctionId, string name, string description)
        {
            foreach (Jobfunction a in jobfunctions)
            {
                if (a.JobfunctionId == jobfunctionId)
                {
                    if (a.Name != name) { a.Name = name; }
                    if (a.Description != description)
                    {
                        a.Description = description;
                    }
                }
            }
        }
    }
}
