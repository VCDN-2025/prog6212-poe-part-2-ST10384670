using Contract_Monthly_Claims.Models;
using Microsoft.AspNetCore.Mvc;

namespace Contract_Monthly_Claims.Controllers
{
    public class ClaimsController : Controller
    {
        // Shared in-memory list
        private static List<Claim> claims => ClaimStorage.Claims;


        // GET: /Claims
        [HttpGet]
        public IActionResult Index()
        {
            return RedirectToAction("ViewClaims");
        }

        // GET: /Claims/SubmitClaim
        [HttpGet]
        public IActionResult SubmitClaim()
        {
            return View("SubmitClaim");
        }

        // POST: /Claims/SubmitClaim
        [HttpPost]
        public IActionResult SubmitClaim(Claim claim, IFormFile document)
        {
            if (!ModelState.IsValid)
                return View("SubmitClaim", claim);

            if (document != null && document.Length > 0)
            {
                claim.SupportingDocuments.Add(new SupportingDocument
                {
                    FileName = document.FileName,
                    FilePath = "#",
                    FileSize = document.Length
                });
            }

            claim.ClaimId = claims.Count + 1;
            claim.Status = "Pending";             // Ensure initial status is Pending
            claim.CurrentApprover = "Programme Coordinator"; // Set first approver
            claims.Add(claim);

            return RedirectToAction("ViewClaims");
        }

        // GET: /Claims/ViewClaims
        [HttpGet]
        public IActionResult ViewClaims()
        {
            return View("ViewClaims", claims ?? new List<Claim>());
        }

        // =========================
        // Coordinator Actions
        // =========================
        [HttpGet]
        public IActionResult CoordinatorClaims()
        {
            var coordinatorClaims = claims.Where(c => c.Status == "Pending").ToList();
            return View("CoordinatorClaims", coordinatorClaims);
        }

        [HttpPost]
        public IActionResult VerifyClaim(int id, bool isApproved)
        {
            var claim = claims.FirstOrDefault(c => c.ClaimId == id);
            if (claim != null)
            {
                if (isApproved)
                {
                    claim.Status = "Verified";
                    claim.CurrentApprover = "Academic Manager"; // next stage
                }
                else
                {
                    claim.Status = "Rejected";
                    claim.CurrentApprover = "None";
                }
            }
            return RedirectToAction("CoordinatorClaims");
        }

        [HttpPost]
        public IActionResult RejectClaim(int id)
        {
            var claim = claims.FirstOrDefault(c => c.ClaimId == id);
            if (claim != null)
            {
                claim.Status = "Rejected";
                claim.CurrentApprover = "None";
            }
            return RedirectToAction("CoordinatorClaims");
        }

        // =========================
        // Academic Manager Actions
        // =========================
        [HttpGet]
        public IActionResult ManagerClaims()
        {
            var managerClaims = claims.Where(c => c.Status == "Verified").ToList();
            return View("ManagerClaims", managerClaims);
        }

        [HttpPost]
        public IActionResult ApproveClaim(int id)
        {
            var claim = claims.FirstOrDefault(c => c.ClaimId == id);
            if (claim != null)
            {
                claim.Status = "Approved";
                claim.CurrentApprover = "None";
            }
            return RedirectToAction("ManagerClaims");
        }

        [HttpPost]
        public IActionResult RejectByManager(int id)
        {
            var claim = claims.FirstOrDefault(c => c.ClaimId == id);
            if (claim != null)
            {
                claim.Status = "Rejected";
                claim.CurrentApprover = "None";
            }
            return RedirectToAction("ManagerClaims");
        }
    }
}
