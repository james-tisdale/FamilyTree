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

                    try
                    {
                        familyMember.GroupID = reader.GetInt32("GroupID");
                    }
                    catch (System.Data.SqlTypes.SqlNullValueException)
                    {
                        familyMember.GroupID = null;
                    }

                    familyMember.FirstName = reader.GetString("FirstName");
                    familyMember.LastName = reader.GetString("LastName");
                    try
                    {
                        familyMember.GroupName = reader.GetString("GroupName");
                    }
                    catch (System.Data.SqlTypes.SqlNullValueException)
                    {
                        familyMember.GroupName = null;
                    }
                    try
                    {
                        familyMember.AstroSign = reader.GetString("AstroSign");
                    }
                    catch (System.Data.SqlTypes.SqlNullValueException)
                    {
                        familyMember.AstroSign = null;
                    }
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
                    try
                    {
                        familyMember.GroupID = reader.GetInt32("GroupID");
                    }
                    catch (System.Data.SqlTypes.SqlNullValueException)
                    {
                        familyMember.GroupID = null;
                    }
                    familyMember.FirstName = reader.GetString("FirstName");
                    familyMember.LastName = reader.GetString("LastName");

                    var groupRepo = new GroupNameRepository();
                    groupRepo.AssignGroupNameToFamilyMember(familyMember);

                    try
                    {
                        familyMember.AstroSign = reader.GetString("AstroSign");
                    }
                    catch (System.Data.SqlTypes.SqlNullValueException)
                    {
                        familyMember.AstroSign = null;
                    }
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

                    try
                    {
                        familyMember.GroupName = reader.GetString("GroupName");
                    }
                    catch (System.Data.SqlTypes.SqlNullValueException)
                    {
                        familyMember.GroupName = null;
                    }

                    try
                    {
                        familyMember.AstroSign = reader.GetString("AstroSign");
                    }
                    catch (System.Data.SqlTypes.SqlNullValueException)
                    {
                        familyMember.AstroSign = null;
                    }


                    familyMember.BirthDay = reader.GetInt32("BirthDay");

                    familyMember.BirthMonth = reader.GetInt32("BirthMonth");
                    familyMember.BirthYear = reader.GetInt32("BirthYear");
                    familyMember.objBirthDate = new DateTime(familyMember.BirthYear, familyMember.BirthMonth, familyMember.BirthDay);
                    //familyMember.objBirthDate = currentDateTime;
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
                "BirthDay, BirthYear, Gender, MaidenName, GroupID) VALUES (@FirstName, @LastName, @BirthMonth, " +
                "@BirthDay, @BirthYear, @Gender, @MaidenName, @GroupID);";

            cmd.Parameters.AddWithValue("FirstName", familyMemberToInsert.FirstName);
            cmd.Parameters.AddWithValue("LastName", familyMemberToInsert.LastName);
            cmd.Parameters.AddWithValue("BirthMonth", familyMemberToInsert.BirthMonth);
            cmd.Parameters.AddWithValue("BirthDay", familyMemberToInsert.BirthDay);
            cmd.Parameters.AddWithValue("BirthYear", familyMemberToInsert.BirthYear);
            cmd.Parameters.AddWithValue("Gender", familyMemberToInsert.Gender);
            cmd.Parameters.AddWithValue("MaidenName", familyMemberToInsert.MaidenName);
            cmd.Parameters.AddWithValue("GroupID", familyMemberToInsert.GroupID);

            //AssignGroupID(familyMemberToInsert);
            //AssignGeneration(familyMemberToInsert);
            //AssignAstroSign(familyMemberToInsert);

            using (conn)
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }


        }

        public void UpdateFamilyMember(FamilyMember familyMemberToUpdate)
        {
            MySqlConnection conn = new MySqlConnection(connectionString);
            MySqlCommand cmd = conn.CreateCommand();

            cmd.CommandText = ("UPDATE familymembers SET FirstName = @FirstName, LastName = @LastName," +
                " BirthMonth = @BirthMonth, BirthDay = @BirthDay, BirthYear = @BirthYear WHERE ID = @id");
            cmd.Parameters.AddWithValue("FirstName", familyMemberToUpdate.FirstName);
            cmd.Parameters.AddWithValue("LastName", familyMemberToUpdate.LastName);
            cmd.Parameters.AddWithValue("id", familyMemberToUpdate.ID);
            cmd.Parameters.AddWithValue("BirthMonth", familyMemberToUpdate.BirthMonth);
            cmd.Parameters.AddWithValue("BirthDay", familyMemberToUpdate.BirthDay);
            cmd.Parameters.AddWithValue("BirthYear", familyMemberToUpdate.BirthYear);

            using (conn)
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }


        public FamilyMember AssignGroups()
        {
            var groupRepo = new GroupNameRepository();

            var groupNameList = groupRepo.GetGroupNames();

            FamilyMember fam = new FamilyMember();
            fam.GroupNameList = groupNameList;

            if (fam.GroupID == 1)
            {
                fam.GroupName = "Tisdale";
            }

            if (fam.GroupID == 2)
            {
                fam.GroupName = "Denny";
            }

            if (fam.GroupID == 3)
            {
                fam.GroupName = "Harkness";
            }

            if (fam.GroupID == 4)
            {
                fam.GroupName = "Littleton";
            }

            if (fam.GroupID == 5)
            {
                fam.GroupName = "Elliot";
            }

            if (fam.GroupID == 6)
            {
                fam.GroupName = "Lee";
            }

            return fam;
        }

        public void AssignAstroSign(FamilyMember familyMember)
        {
            int month = familyMember.objBirthDate.Month;
            int day = familyMember.objBirthDate.Day;

            switch (month)
            {
                case 1:
                    if (day <= 19)
                    {
                        familyMember.AstroSign = "Capricorn";
                        break;
                    }
                    else
                    {
                        familyMember.AstroSign = "Aquarius";
                        break;
                    }

                case 2:
                    if (day <= 18)
                    {
                        familyMember.AstroSign = "Aquarius";
                        break;
                    }
                    else
                    {
                        familyMember.AstroSign = "Pisces";
                        break;
                    }

                case 3:
                    if (day <= 20)
                    {
                        familyMember.AstroSign = "Pisces";
                        break;
                    }
                    else
                    {
                        familyMember.AstroSign = "Aries";
                        break;
                    }
                case 4:
                    if (day <= 19)
                    {
                        familyMember.AstroSign = "Aries";
                        break;
                    }
                    else
                    {
                        familyMember.AstroSign = "Taurus";
                        break;
                    }
                case 5:
                    if (day <= 20)
                    {
                        familyMember.AstroSign = "Taurus";
                        break;
                    }
                    else
                    {
                        familyMember.AstroSign = "Gemini";
                        break;
                    }
                case 6:
                    if (day <= 20)
                    {
                        familyMember.AstroSign = "Gemini";
                        break;
                    }
                    else
                    {
                        familyMember.AstroSign = "Cancer";
                        break;
                    }
                case 7:
                    if (day <= 22)
                    {
                        familyMember.AstroSign = "Cancer";
                        break;
                    }
                    else
                    {
                        familyMember.AstroSign = "Leo";
                        break;
                    }
                case 8:
                    if (day <= 22)
                    {
                        familyMember.AstroSign = "Leo";
                        break;
                    }
                    else
                    {
                        familyMember.AstroSign = "Virgo";
                        break;
                    }
                case 9:
                    if (day <= 22)
                    {
                        familyMember.AstroSign = "Virgo";
                        break;
                    }
                    else
                    {
                        familyMember.AstroSign = "Libra";
                        break;
                    }
                case 10:
                    if (day <= 22)
                    {
                        familyMember.AstroSign = "Libra";
                        break;
                    }
                    else
                    {
                        familyMember.AstroSign = "Scorpio";
                        break;
                    }
                case 11:
                    if (day <= 21)
                    {
                        familyMember.AstroSign = "Scorpio";
                        break;
                    }
                    else
                    {
                        familyMember.AstroSign = "Sagittarius";
                        break;
                    }
                case 12:
                    if (day <= 21)
                    {
                        familyMember.AstroSign = "Sagittarius";
                        break;
                    }
                    else
                    {
                        familyMember.AstroSign = "Capricorn";
                        break;
                    }
            }
        }

        public void AssignGeneration(FamilyMember familyMember)
        {
            int year = familyMember.objBirthDate.Year;
            int month = familyMember.objBirthDate.Month;
            int day = familyMember.objBirthDate.Day;

            if (familyMember.objBirthDate.Year > 2014)
            {
                familyMember.Generation = "Generation Alpha";
            }
            if (familyMember.objBirthDate.Year > 1994)
            {
                familyMember.Generation = "Generation Z";
            }

            if (familyMember.objBirthDate.Year > 1980)
            {
                familyMember.Generation = "Millennial";
            }

            if (familyMember.objBirthDate.Year > 1964)
            {
                familyMember.Generation = "Generation X";
            }

            if (familyMember.objBirthDate.Year > 1945)
            {
                familyMember.Generation = "Baby Boomer";
            }

            if (familyMember.objBirthDate.Year > 1924)
            {
                familyMember.Generation = "The Silent Generation";
            }
        }


    }
}
