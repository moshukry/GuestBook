using Microsoft.VisualStudio.TestTools.UnitTesting;
using GuestBook.Controllers;
using GuestBook.Models;
using Microsoft.AspNetCore.Mvc;

namespace GuestBook.UnitTests
{
    [TestClass]
    public class OperationsControllerTests
    {
        private OperationsController OperationsController = new OperationsController(new GuestBookContext());
        //[TestMethod]
        //public void LogIn_LoggedHasCookies_RedirectToMessages()
        //{
        //    //Act
        //    ViewResult result = OperationsController.LogIn() as ViewResult;
        //    //Assert
        //    Assert.AreEqual("LogIn",result.ViewName);
        //}
        [TestMethod]
        public void LogInPost_Wrongdata_Redirect()
        {
            ViewResult result = OperationsController.LogIn(new User { UserEmail = "abc", Password = "123" }, false) as ViewResult;
            //Assert
            Assert.AreEqual("LogIn", result.ViewName);
        }
        //[TestMethod]
        //public void LogInPost_Rightdata_RedirectToMessages()
        //{
        //    var cookies = new HttpCookieCollection();
        //    cookies.Add(new HttpCookie("usercookie"));

        //    var mockHttpContext = Substitute.For<HttpContextBase>();
        //    mockHttpContext.Request.Cookies.Returns(cookies);

        //    var sut = new MyController();
        //    sut.ControllerContext = new ControllerContext
        //    {
        //        Controller = sut,
        //        HttpContext = mockHttpContext
        //    };
        //    RedirectToActionResult result = (RedirectToActionResult)OperationsController.LogIn(new User { UserEmail = "shukry@abc.com", Password = "123" }, true);

        //    Assert.AreEqual("Index", result.ActionName);
        //    Assert.AreEqual("Messages", result.ControllerName);
        //}
    }
}