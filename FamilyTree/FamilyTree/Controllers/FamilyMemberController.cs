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
    }
}