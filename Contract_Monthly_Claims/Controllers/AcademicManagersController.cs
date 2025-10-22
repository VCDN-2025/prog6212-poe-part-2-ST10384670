
using Contract_Monthly_Claims.Models;
using Microsoft.AspNetCore.Mvc;

namespace Contract_Monthly_Claims.Controllers
{
    public class AcademicManagersController : Controller
    {

        private static List<Claim> claims => ClaimStorage.Claims;


        public IActionResult Index()
        {
            var verified = claims.Where(c => c.Status == "Verified").ToList();
            return View(verified);
        }

        [HttpPost]
        public IActionResult ApproveClaim(int id)
        {
            var claim = claims.FirstOrDefault(c => c.ClaimId == id);
            if (claim != null)
                claim.Status = "Approved";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult RejectClaim(int id)
        {
            var claim = claims.FirstOrDefault(c => c.ClaimId == id);
            if (claim != null)
                claim.Status = "Rejected";
            return RedirectToAction("Index");
        }
    }
}
