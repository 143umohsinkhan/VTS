using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using VST.Models;
using Repositories;
using DAL;
using System.Web.Security;
using Utility;
using System;
using System.IO;
using System.Linq;

namespace VST.Controllers
{
    [Authorize]
    public class AccountController : BaseController
    {
        string Email
        {
            get { return User.Identity.GetUserName(); }
        }


        public AccountController(IRepository<VTSUSER, long> userRepository) : base(userRepository)
        {

        }

        [AllowAnonymous]
        public ActionResult Unauthorized()
        {
            return View();
        }

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            if (Request.IsAuthenticated && returnUrl == null)
            {
                var userRole = UserRepository.GetRole(Email);
                return RedirectToLocal(returnUrl, userRole.Equals(VTSConstants.Admin));
            }
            else
                ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        //[ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var user = UserRepository.Authenticate(model.Email, model.Password);
                if (CheckLogin(user))
                {
                    Session[VTSConstants.UserID] = user.UserID;
                    FormsAuthentication.SetAuthCookie(user.Email, model.RememberMe);
                    return RedirectToLocal(returnUrl, user.IsAdmin);
                }
                else
                {
                    ModelState.AddModelError("", "Invalid username or password.");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        bool CheckLogin(VTSUSER user)
        {
            if (user == null) return false;
            if (user.IsAdmin) return true;
            return (user.CreatedOn.Value.Subtract(DateTime.Now).Days > 180) ? false : true;
        }

        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult EmailAlreadyUse(string Email)
        {
            if (UserRepository.Get(Email) != null)
                return Json("Email already exists", JsonRequestBehavior.AllowGet);
            else
                return Json("true", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel model, HttpPostedFileBase imagefile)
        {
            if (ModelState.IsValid)
            {
                // TODO : Check for file upload filter. 
                VTSUSER objUser = new VTSUSER();
                objUser.Address = model.Address;
                objUser.City = model.City;
                objUser.CreatedOn = DateTime.Now;
                if (!string.IsNullOrEmpty(model.DOB))
                {
                    try
                    {
                        objUser.DOB = Convert.ToDateTime(model.DOB);
                    }
                    catch
                    {
                        ModelState.AddModelError("DOB", "Invalid date of birth");
                        return View(model);
                    }
                }
                objUser.Email = model.Email;
                objUser.FirstName = model.FirstName;
                objUser.IsActive = false;
                objUser.IsAdmin = false;
                objUser.LastName = model.LastName;
                objUser.Password = model.Password;
                objUser.Phone = model.Phone;
                objUser.Photo = model.Photo;
                objUser.Pincode = model.Pin;
                objUser.Sex = Convert.ToBoolean(model.Sex.ToGenderConversion());
                objUser.State = model.State;
                objUser.Photo = UploadFileDeleteExisting(imagefile, objUser.Email.GetName());

                if (UserRepository.Get(objUser.Email) != null)
                {
                    AddErrors("Email already exists");
                    return View(model);
                }

                var createdUser = UserRepository.Create(objUser);
                if (createdUser != null)
                {
                    // Send an email with this link
                    var callbackUrl = Url.Action("ConfirmEmail", "Account", new { token = CryptoEngine.Encrypt(createdUser.UserID.ToString()) }, protocol: Request.Url.Scheme);
                    MailHelper.MailSend
                        (new MailMessageHelper
                        {
                            Body = "Please confirm your email by copy past link in browser : " + callbackUrl + "",
                            Subject = "Confirmation Mail",
                            To = createdUser.Email
                        });
                    return RedirectToAction("RegistrationComplete", "Account");
                }
                else
                {
                    AddErrors(VTSConstants.Error);
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        [AllowAnonymous]
        public ActionResult RegistrationComplete()
        {
            return View();
        }

        //
        // GET: /Account/ConfirmEmail
        [AllowAnonymous]
        public ActionResult ConfirmEmail(string token)
        {
            if (token == null)
            {
                ViewBag.Heading = "Invalid url";
                ViewBag.ErrorMessage = "Request page not found";
                return View("Error");
            }

            var activated = UserRepository.ActivateAccount(CryptoEngine.Decrypt(token).Tolong());

            if (activated)
                return View("ConfirmEmail");
            else
            {
                ViewBag.Heading = "Invalid url";
                ViewBag.ErrorMessage = "Request page not found";
                return View("Error");
            }
        }

        //
        // GET: /Account/ForgotPassword
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        //
        // POST: /Account/ForgotPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = UserRepository.Get(model.Email);
                if (user == null)
                {
                    ModelState.AddModelError("", "The user does not exist.");
                    return View();
                }

                else if (!(UserRepository.IsActive(model.Email)))
                {
                    ModelState.AddModelError("", "The user is not confirmed.");
                    return View();
                }

                // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                // Send an email with this link
                // string code = UserManager.GeneratePasswordResetTokenAsync(user.UserID.ToString()).Result;
                var callbackUrl = Url.Action("ResetPassword", "Account", new { token = CryptoEngine.Encrypt(user.UserID.ToStringSafe()) }, protocol: Request.Url.Scheme);
                MailHelper.MailSend(new MailMessageHelper
                {
                    Body = "Please reset your password by copy past link in browser : " + callbackUrl,
                    Subject = "Reset Password",
                    To = user.Email
                });
                return RedirectToAction("ForgotPasswordConfirmation", "Account");
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ForgotPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        //
        // GET: /Account/ResetPassword
        [AllowAnonymous]
        public ActionResult ResetPassword(string token)
        {
            if (token == null || (UserRepository.Get(CryptoEngine.Decrypt(token).Tolong()) == null))
            {
                ViewBag.Heading = "Invalid url";
                ViewBag.ErrorMessage = "Request page not found";
                return View("Error");
            }

            return View();
        }

        //
        // POST: /Account/ResetPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ResetPassword(ResetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = UserRepository.Get(model.Email);
                if (user == null)
                {
                    ModelState.AddModelError("", "No user found.");
                    return View();
                }

                user.Password = model.Password;
                user.LastUpdatePassword = DateTime.Now;
                var IsUpdated = UserRepository.Update(user.Email, user);
                if (IsUpdated)
                {
                    return RedirectToAction("ResetPasswordConfirmation", "Account");
                }
                else
                {
                    AddErrors("Failed to update password");
                    return View();
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        public ActionResult ChangePassword(long ID)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var getUser = UserRepository.Get(Email);
                if (!getUser.Password.Equals(model.CurrentPassword))
                {
                    ModelState.AddModelError("CurrentPassword", "Current password is invalid");
                    return View(model);
                }

                var IsUpdated = UserRepository.ChangePassword(Email, model.NewPassword);
                if (IsUpdated)
                {
                    ViewBag.StatusMessage = "Password updated successfully.";
                    return View();
                }
                else
                {
                    AddErrors(VTSConstants.Error);
                    return View(model);
                }
            }
            return View(model);
        }

        //
        // GET: /Account/ResetPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        //
        // GET: /Account/Manage
        public ActionResult Manage(string message)
        {
            ViewBag.StatusMessage = !string.IsNullOrEmpty(message) ? message : "";
            ViewBag.ReturnUrl = Url.Action("Manage");

            var loggedInuserModel = UserRepository.Get(Email);
            return View(loggedInuserModel);
        }

        //
        // POST: /Account/Manage
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Manage(VTSUSER model, HttpPostedFileBase imagefile)
        {
            ViewBag.ReturnUrl = Url.Action("Manage");
            if (ModelState.IsValid)
            {
                //var validateUploadFile = ValidaetUploadedFile(imagefile);
                //if (!validateUploadFile.IsNull())
                //{
                //    AddErrors(validateUploadFile);
                //    return View(model);
                //}

                var newFilePath = UploadFileDeleteExisting(imagefile, model.Email.GetName(), model.Photo, true); ;
                model.Photo = !newFilePath.IsNull() ? newFilePath : model.Photo;

                var updateModel = UserRepository.Update(Email, model);
                if (updateModel)
                {
                    return RedirectToAction("Manage", new { Message = "Update Profile Successfully" });
                }
                else
                    AddErrors(VTSConstants.Error);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            if (Session != null)
                Session.Clear();
            return RedirectToAction("Login", "Account");
        }

        public ActionResult ForceSignOut()
        {
            FormsAuthentication.SignOut();
            if (Session != null)
                Session.Clear();
            return RedirectToAction("Login", "Account");
        }
        #region Helpers

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private ActionResult RedirectToLocal(string returnUrl, bool IsAdmin)
        {
            if (Url.IsLocalUrl(returnUrl))
                return Redirect(returnUrl);
            else
                return IsAdmin ? RedirectToAction("AdminDashboard", "Home") : RedirectToAction("StudentDashboard", "Home");
        }

        string ValidaetUploadedFile(HttpPostedFileBase imageFile)
        {
            var validFileType = new[] { "jpg", "jpeg", "png" };
            int filesize = 550;

            // Skip validation as upload file is empty.
            if (imageFile == null || imageFile.ContentLength <= 0) return string.Empty;

            // Apply filetype checking.
            if (!validFileType.Contains(imageFile.FileName))
                return "Invalid file type";

            if (imageFile.ContentLength > (filesize * 1024))
                return "File size Should Be UpTo " + filesize + "KB";

            return string.Empty;
        }
    }
    #endregion
}
