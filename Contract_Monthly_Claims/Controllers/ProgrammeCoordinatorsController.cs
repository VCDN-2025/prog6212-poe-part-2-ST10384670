
using Contract_Monthly_Claims.Models;
using Microsoft.AspNetCore.Mvc;

namespace Contract_Monthly_Claims.Controllers
{
    public class ProgrammeCoordinatorsController : Controller
    {

        private static List<Claim> claims => ClaimStorage.Claims;


        public IActionResult Index()
        {
            var pending = claims.Where(c => c.Status == "Pending").ToList();
            return View(pending);
        }

        [HttpPost]
        public IActionResult VerifyClaim(int id)
        {
            var claim = claims.FirstOrDefault(c => c.ClaimId == id);
            if (claim != null)
                claim.Status = "Verified";
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
