using Microsoft.VisualStudio.TestTools.UnitTesting;
using Contract_Monthly_Claims.Controllers;
using Contract_Monthly_Claims.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace mccpoe2
{
    [TestClass]
    public sealed class ClaimsControllerTests
    {
        [TestInitialize]
        public void TestInitialize()
        {
            // Clear the shared list completely before each test
            ClaimStorage.Claims.Clear();
        }

        [TestMethod]
        public void SubmitClaim_ValidClaim_AddsClaimToList()
        {
            // Arrange
            var controller = new ClaimsController();
            var claim = new Claim
            {
                ClaimId = 1, // explicitly set
                LecturerId = 1,
                Lecturer = new Lecturer { FullName = "John Doe" },
                HoursWorked = 10,
                HourlyRate = 200,
                Notes = "Test",
                SupportingDocuments = new List<SupportingDocument>()
            };

            // Act
            var result = controller.SubmitClaim(claim, null!) as RedirectToActionResult;

            // Assert
            Assert.AreEqual(1, ClaimStorage.Claims.Count);
            Assert.AreEqual("Pending", ClaimStorage.Claims[0].Status);
            Assert.AreEqual("ViewClaims", result!.ActionName);
        }

        [TestMethod]
        public void VerifyClaim_Approved_UpdatesStatusToVerified()
        {
            // Arrange
            var claim = new Claim
            {
                ClaimId = 1,
                Status = "Pending",
                CurrentApprover = "Programme Coordinator",
                Lecturer = new Lecturer { FullName = "John Doe" },
                SupportingDocuments = new List<SupportingDocument>()
            };
            ClaimStorage.Claims.Add(claim);

            var controller = new ClaimsController();

            // Act
            var result = controller.VerifyClaim(1, true) as RedirectToActionResult;

            // Assert
            Assert.AreEqual("Verified", claim.Status);
            Assert.AreEqual("Academic Manager", claim.CurrentApprover);
            Assert.AreEqual("CoordinatorClaims", result!.ActionName);
        }

        [TestMethod]
        public void ApproveClaim_Manager_UpdatesStatusToApproved()
        {
            // Arrange
            var claim = new Claim
            {
                ClaimId = 1,
                Status = "Verified", // required initial status for manager approval
                CurrentApprover = "Academic Manager",
                Lecturer = new Lecturer { FullName = "John Doe" },
                SupportingDocuments = new List<SupportingDocument>()
            };
            ClaimStorage.Claims.Add(claim);

            var controller = new ClaimsController();

            // Act
            var result = controller.ApproveClaim(1) as RedirectToActionResult;

            // Assert
            Assert.AreEqual("Approved", claim.Status);
            Assert.AreEqual("None", claim.CurrentApprover);
            Assert.AreEqual("ManagerClaims", result!.ActionName);
        }

        [TestMethod]
        public void RejectClaim_Coordinator_UpdatesStatusToRejected()
        {
            // Arrange
            var claim = new Claim
            {
                ClaimId = 1,
                Status = "Pending", // required initial status
                CurrentApprover = "Programme Coordinator",
                Lecturer = new Lecturer { FullName = "John Doe" },
                SupportingDocuments = new List<SupportingDocument>()
            };
            ClaimStorage.Claims.Add(claim);

            var controller = new ClaimsController();

            // Act
            var result = controller.RejectClaim(1) as RedirectToActionResult;

            // Assert
            Assert.AreEqual("Rejected", claim.Status);
            Assert.AreEqual("None", claim.CurrentApprover);
            Assert.AreEqual("CoordinatorClaims", result!.ActionName);
        }

        [TestMethod]
        public void ManagerClaims_ReturnsOnlyVerifiedClaims()
        {
            // Arrange
            ClaimStorage.Claims.Add(new Claim
            {
                ClaimId = 1,
                Status = "Verified", // should be returned
                Lecturer = new Lecturer { FullName = "John Doe" },
                SupportingDocuments = new List<SupportingDocument>()
            });
            ClaimStorage.Claims.Add(new Claim
            {
                ClaimId = 2,
                Status = "Pending", // should NOT be returned
                Lecturer = new Lecturer { FullName = "Jane Doe" },
                SupportingDocuments = new List<SupportingDocument>()
            });

            var controller = new ClaimsController();

            // Act
            var result = controller.ManagerClaims() as ViewResult;
            var model = (result!.Model as List<Claim>)!;

            // Assert
            Assert.AreEqual(1, model.Count);
            Assert.AreEqual(1, model[0].ClaimId);
        }
    }
}
