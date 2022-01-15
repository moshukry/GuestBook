using Microsoft.VisualStudio.TestTools.UnitTesting;
using GuestBook.Controllers;
using GuestBook.Models;
using Microsoft.AspNetCore.Mvc;

namespace GuestBook.UnitTests
{
    [TestClass]
    public class RepliesControllerTests
    {
        private RepliesController replisController = new RepliesController(new GuestBookContext());
        [TestMethod]
        public void Create_ValidData_Redirects()
        {
            RedirectToActionResult result = replisController.Create(new Reply { ReplyBody = "abc", MessageId = 6 }) as RedirectToActionResult;
            //Assert
            Assert.AreEqual("Index", result.ActionName);
            Assert.AreEqual("Messages", result.ControllerName);
        }
        //[TestMethod]
        //public void Create_InvalidData_ReturnSameView()
        //{
        //    ViewResult result = replisController.Create(new Reply { ReplyBody=null,MessageId = 1 }) as ViewResult;
        //    //Assert
        //    Assert.AreEqual("Create", result.ViewName);
        //}
    }
}