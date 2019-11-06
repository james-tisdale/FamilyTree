using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FamilyTree.Models;
using MySql.Data.MySqlClient;

namespace FamilyTree
{
    public class GroupNameRepository
    {
        private static string connectionString = System.IO.File.ReadAllText("ConnectionString.txt");

        public List<GroupNames> GetGroupNames()
        {
            MySqlConnection conn = new MySqlConnection(connectionString);
            MySqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT * FROM Categories;";

            using (conn)
            {
                conn.Open();
                MySqlDataReader reader = cmd.ExecuteReader();

                var allGroupNames = new List<GroupNames>();

                while (reader.Read() == true)
                {
                    var currentGroupName = new GroupNames();
                    currentGroupName.GroupID = reader.GetInt32("GroupID");
                    currentGroupName.GroupName = reader.GetString("GroupName");
                    allGroupNames.Add(currentGroupName);
                }
                return allGroupNames;
            }
        }
    }
}
