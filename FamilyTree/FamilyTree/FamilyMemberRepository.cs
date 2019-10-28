using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FamilyTree.Models;
using MySql.Data.MySqlClient;

namespace FamilyTree
{
    public class FamilyMemberRepository
    {
        private static string connectionString = System.IO.File.ReadAllText("ConnectionString.txt");

        public List<FamilyMember> GetAllFamilyMembers()
        {
            MySqlConnection conn = new MySqlConnection(connectionString);
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT * FROM FamilyMembers;";

            using (conn)
            {
                conn.Open();
                MySqlDataReader reader = cmd.ExecuteReader();

                List<FamilyMember> allFamilyMembers = new List<FamilyMember>();

                while (reader.Read() == true)
                {
                    FamilyMember currentFamilyMember = new FamilyMember();
                    currentFamilyMember.ID = reader.GetInt32("ID");
                    currentFamilyMember.GroupID = reader.GetInt32("GroupID");
                    currentFamilyMember.FirstName = reader.GetString("FirstName");
                    currentFamilyMember.LastName = reader.GetString("LastName");
                    currentFamilyMember.Gender = reader.GetString("Gender");
                    try
                    {
                        currentFamilyMember.MaidenName = reader.GetString("MaidenName");
                    }
                    catch(System.Data.SqlTypes.SqlNullValueException)
                    {
                        currentFamilyMember.MaidenName = null;
                    }
                    try
                    {
                        currentFamilyMember.BirthDate = reader.GetString("BirthDate");
                    }
                    catch (System.Data.SqlTypes.SqlNullValueException)
                    {
                        currentFamilyMember.BirthDate = null;
                    }
                    try
                    {
                        currentFamilyMember.Generation = reader.GetString("Generation");
                    }
                    catch (System.Data.SqlTypes.SqlNullValueException)
                    {
                        currentFamilyMember.Generation = null;
                    }

                    allFamilyMembers.Add(currentFamilyMember);
                }
                return allFamilyMembers;
            }
        }
    }
}
