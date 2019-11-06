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
            var groupRepo = new GroupNameRepository();
            

            return View(allFamilyMembers);
        }

        public IActionResult ViewFamilyMember(int id)
        {
            FamilyMemberRepository repo = new FamilyMemberRepository();
            FamilyMember familyMember = repo.GetFamilyMember(id);
            //familyMember = repo.AssignGroups();

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
            var prod = repo.AssignGroups();
            

            return View(prod);
        }

        public IActionResult InsertFamilyMemberToDatabase(FamilyMember familyMemberToInsert)
        {
            FamilyMemberRepository repo = new FamilyMemberRepository();
            repo.InsertFamilyMember(familyMemberToInsert);
            repo.AssignAstroSign(familyMemberToInsert);
            repo.AssignGeneration(familyMemberToInsert);

            var groupRepo = new GroupNameRepository();
            groupRepo.AssignGroupNameToFamilyMember(familyMemberToInsert);
            groupRepo.AssignAstroSignToFamilyMember(familyMemberToInsert);

            //var fam = repo.AssignGroups();

            return RedirectToAction("Index");
        }
    }
}