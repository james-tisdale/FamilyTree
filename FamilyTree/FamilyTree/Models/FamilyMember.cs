using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FamilyTree.Models
{
    public class FamilyMember
    {
        public FamilyMember()
        {
        }
        public int ID{ get; set; }
        public int GroupID { get; set; }
        public string GroupName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string Generation { get; set; }
        public string MaidenName { get; set; }
        public string AstroSign { get; set; }
        public int BirthMonth { get; set; }
        public int BirthYear { get; set; }
        public int BirthDay { get; set; }
        public DateTime objBirthDate { get; set; }
    }
}
