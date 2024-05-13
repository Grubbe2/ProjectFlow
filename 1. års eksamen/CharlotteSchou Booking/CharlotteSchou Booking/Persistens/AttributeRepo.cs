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
    public class AttributeRepo : Repo <Attribute>
    {
        private List<Attribute> attributes = new List<Attribute>();
       
        public AttributeRepo()
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
                SqlCommand cmd = new SqlCommand("SELECT AttributeId, Characteristics, Services from ATTRIBUTE", con);
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        Attribute attribute = new Attribute()
                        {
                            AttributeId = int.Parse(dr["AttributeId"].ToString()),
                            Characteristics = dr["Characteristics"].ToString(),
                            Service = dr["Services"].ToString(),
                        };
                        attributes.Add(attribute);
                    }

                }
            }
        }

        //Creates a new Attribute object in the database
        public void Create(Attribute attributeToBeAdded)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open(); SqlCommand cmd = new SqlCommand("INSERT INTO ATTRIBUTE (Characteristics, Services)"
                    + "VALUES(@Characteristics, @Services)" + "SELECT @@IDENTITY", con);
                cmd.Parameters.Add("@Characteristics", SqlDbType.NVarChar).Value = attributeToBeAdded.Characteristics;
                cmd.Parameters.Add("@Services", SqlDbType.NVarChar).Value = attributeToBeAdded.Service;
                cmd.ExecuteNonQuery();
            }
            attributes.Add(attributeToBeAdded);
        }

        //returns the list of all attributes
        public override List<Attribute> LoadAll()
        {
            return attributes;
        }

        //returns Attribute with specific AttributeId
        public override Attribute Load(int AttributeId)
        {
            Attribute result = new();
            foreach (Attribute a in attributes)
            {
                if (a.AttributeId == AttributeId)
                {
                    result = a;
                    break;
                }

            }
            return result;
        }

        //removes an Attribute from the database
        public override void Remove(int AttributeId)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("DELETE FROM ATTRIBUTE WHERE AttributeId = @AttributeId", con);
                cmd.Parameters.Add("@AttributeId", SqlDbType.Int).Value = AttributeId;
                cmd.ExecuteNonQuery();
            }
            attributes.Remove(Load(AttributeId));
        }

        //updates one or more of the attributes for the Attributes object in the database
        public void Update(int attributeId, string characteristics, string service)
        {
            foreach (Attribute a in attributes)
            {
                if (a.AttributeId == attributeId)
                {
                    if (a.Characteristics != characteristics) { a.Characteristics = characteristics; }
                    if (a.Service != service) { a.Service = service; }
                  
                }
            }
        }
    }
}
