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
                    FamilyMember familyMember = new FamilyMember();
                    familyMember.ID = reader.GetInt32("ID");
                    familyMember.GroupID = reader.GetInt32("GroupID");
                    familyMember.FirstName = reader.GetString("FirstName");
                    familyMember.LastName = reader.GetString("LastName");
                    familyMember.GroupName = reader.GetString("GroupName");
                    familyMember.AstroSign = reader.GetString("AstroSign");
                    familyMember.BirthDay = reader.GetInt32("BirthDay");
                    familyMember.BirthMonth = reader.GetInt32("BirthMonth");
                    familyMember.BirthYear = reader.GetInt32("BirthYear");
                    DateTime currentDateTime = new DateTime(familyMember.BirthYear, familyMember.BirthMonth, familyMember.BirthDay);
                    familyMember.objBirthDate = currentDateTime;
                    familyMember.Gender = reader.GetString("Gender");
                    try
                    {
                        familyMember.MaidenName = reader.GetString("MaidenName");
                    }
                    catch (System.Data.SqlTypes.SqlNullValueException)
                    {
                        familyMember.MaidenName = null;
                    }
                    try
                    {
                        familyMember.Generation = reader.GetString("Generation");
                    }
                    catch (System.Data.SqlTypes.SqlNullValueException)
                    {
                        familyMember.Generation = null;
                    }

                    allFamilyMembers.Add(familyMember);
                }
                return allFamilyMembers;
            }
        }

        public FamilyMember GetFamilyMember(int id)
        {
            MySqlConnection conn = new MySqlConnection(connectionString);
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT * FROM FamilyMembers WHERE ID = @id;";
            cmd.Parameters.AddWithValue("id", id);

            using (conn)
            {
                conn.Open();
                MySqlDataReader reader = cmd.ExecuteReader();

                var familyMember = new FamilyMember();

                while (reader.Read() == true)
                {
                    FamilyMember currentFamilyMember = new FamilyMember();
                    familyMember.ID = reader.GetInt32("ID");
                    familyMember.GroupID = reader.GetInt32("GroupID");
                    familyMember.FirstName = reader.GetString("FirstName");
                    familyMember.LastName = reader.GetString("LastName");
                    familyMember.GroupName = reader.GetString("GroupName");
                    familyMember.AstroSign = reader.GetString("AstroSign");
                    familyMember.BirthDay = reader.GetInt32("BirthDay");
                    familyMember.BirthMonth = reader.GetInt32("BirthMonth");
                    familyMember.BirthYear = reader.GetInt32("BirthYear");
                    DateTime currentDateTime = new DateTime(familyMember.BirthYear, familyMember.BirthMonth, familyMember.BirthDay);
                    familyMember.objBirthDate = currentDateTime;
                    familyMember.Gender = reader.GetString("Gender");
                    try
                    {
                        familyMember.MaidenName = reader.GetString("MaidenName");
                    }
                    catch (System.Data.SqlTypes.SqlNullValueException)
                    {
                        familyMember.MaidenName = null;
                    }
                    try
                    {
                        familyMember.Generation = reader.GetString("Generation");
                    }
                    catch (System.Data.SqlTypes.SqlNullValueException)
                    {
                        familyMember.Generation = null;
                    }

                }

                return familyMember;
            }

        }

        public List<FamilyMember> GetFamilyByGroup(int id)
        {
            MySqlConnection conn = new MySqlConnection(connectionString);
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT * FROM FamilyMembers WHERE GroupID = @id;";
            cmd.Parameters.AddWithValue("@id", id);

            using (conn)
            {
                conn.Open();
                MySqlDataReader reader = cmd.ExecuteReader();

                List<FamilyMember> allFamilyMembersByGroup = new List<FamilyMember>();
                while (reader.Read() == true)
                {
                    FamilyMember familyMember = new FamilyMember();
                    familyMember.ID = reader.GetInt32("ID");
                    familyMember.GroupID = reader.GetInt32("GroupID");
                    familyMember.FirstName = reader.GetString("FirstName");
                    familyMember.LastName = reader.GetString("LastName");
                    familyMember.GroupName = reader.GetString("GroupName");
                    familyMember.AstroSign = reader.GetString("AstroSign");
                    familyMember.BirthDay = reader.GetInt32("BirthDay");
                    familyMember.BirthMonth = reader.GetInt32("BirthMonth");
                    familyMember.BirthYear = reader.GetInt32("BirthYear");
                    DateTime currentDateTime = new DateTime(familyMember.BirthYear, familyMember.BirthMonth, familyMember.BirthDay);
                    familyMember.objBirthDate = currentDateTime;
                    familyMember.Gender = reader.GetString("Gender");
                    try
                    {
                        familyMember.MaidenName = reader.GetString("MaidenName");
                    }
                    catch (System.Data.SqlTypes.SqlNullValueException)
                    {
                        familyMember.MaidenName = null;
                    }
                    try
                    {
                        familyMember.Generation = reader.GetString("Generation");
                    }
                    catch (System.Data.SqlTypes.SqlNullValueException)
                    {
                        familyMember.Generation = null;
                    }

                    allFamilyMembersByGroup.Add(familyMember);
                }
                return allFamilyMembersByGroup;
            }
        }

        public void InsertFamilyMember(FamilyMember familyMemberToInsert)
        {
            MySqlConnection conn = new MySqlConnection(connectionString);

            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "INSERT INTO FamilyMembers (FirstName, LastName, BirthMonth, " +
                "BirthDay, BirthYear, Gender, MaidenName) VALUES (@FirstName, @LastName, @BirthMonth, " +
                "@BirthDay, @BirthYear, @Gender, @MaidenName);";

            cmd.Parameters.AddWithValue("FirstName", familyMemberToInsert.FirstName);
            cmd.Parameters.AddWithValue("LastName", familyMemberToInsert.LastName);
            cmd.Parameters.AddWithValue("BirthMonth", familyMemberToInsert.BirthMonth);
            cmd.Parameters.AddWithValue("BirthDay", familyMemberToInsert.BirthDay);
            cmd.Parameters.AddWithValue("BirthYear", familyMemberToInsert.BirthYear);
            cmd.Parameters.AddWithValue("Gender", familyMemberToInsert.Gender);
            cmd.Parameters.AddWithValue("MaidenName", familyMemberToInsert.MaidenName);

            using (conn)
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
