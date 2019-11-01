using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FamilyTree.Models;

namespace FamilyTree.Controllers
{
    public class FamilyMemberController : Controller
    {
        public IActionResult Index()
        {
            FamilyMemberRepository repo = new FamilyMemberRepository();
            List<FamilyMember> allFamilyMembers = repo.GetAllFamilyMembers();

            return View(allFamilyMembers);
        }

        public IActionResult ViewFamilyMember(int id)
        {
            FamilyMemberRepository repo = new FamilyMemberRepository();
            FamilyMember familyMember = repo.GetFamilyMember(id);

            return View(familyMember);
        }

        public IActionResult ViewFamilyGroup(int id)
        {
            FamilyMemberRepository repo = new FamilyMemberRepository();
            List<FamilyMember> familyGroup = repo.GetFamilyByGroup(id);

            return View(familyGroup);
        }

        public IActionResult InsertFamilyMember()
        {
            FamilyMemberRepository repo = new FamilyMemberRepository();
            FamilyMember familyMember = repo.InsertFamilyMember();

            return View(familyMember);
        }
    }
}