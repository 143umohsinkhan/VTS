using DAL;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Utility;
using VST.Models;
using VST.WebHelper;

namespace VST.Controllers
{
    [VTSAuthorizer]
    public class HomeController : BaseController
    {
        public HomeController(IRepository<VTSUSER, long> userRepository) : base(userRepository)
        {
        }

        [VTSAuthorizer(Roles = "User")]
        public ActionResult StudentDashboard()
        {
            return View();
        }

        [VTSAuthorizer(Roles = "Admin")]
        public ActionResult AdminDashboard()
        {
            return View();
        }

        [VTSAuthorizer(Roles = "Admin")]
        public ActionResult ViewAllUsers()
        {
            return View(UserRepository.Get());
        }

        public ActionResult Index()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        // GET: /ranu/Create  
        [VTSAuthorizer(Roles = "Admin")]
        public ActionResult CreateUser()
        {
            return View();
        }

        //  
        // POST: /ranu/Create  
        [HttpPost]
        [VTSAuthorizer(Roles = "Admin")]
        public ActionResult CreateUser(RegisterViewModel model, HttpPostedFileBase imagefile)
        {
            if (ModelState.IsValid)
            {
                // TODO : Check for file upload filter. 
                VTSUSER objUser = new VTSUSER();
                objUser.Address = model.Address;
                objUser.City = model.City;
                objUser.IsActive = true;
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
                objUser.IsAdmin = model.IsAdmin;
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
                    return RedirectToAction("ViewAllUsers", "Home");
                }
                else
                {
                    AddErrors(VTSConstants.Error);
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        [VTSAuthorizer(Roles = "Admin")]
        public ActionResult Edit(int id = 0)
        {
            var loggedInuserModel = UserRepository.Get(id);
            return View(loggedInuserModel);
        }

        [VTSAuthorizer(Roles = "Admin")]
        [HttpPost]
        public ActionResult Edit(int id, VTSUSER model)
        {
            try
            {
                ViewBag.ReturnUrl = Url.Action("Edit");
                if (ModelState.IsValid)
                {
                    var updateModel = UserRepository.Update(id, model);
                    if (updateModel)
                    {
                        return RedirectToAction("Edit", new { Message = "Update Successfully" });
                    }
                    else
                        AddErrors(VTSConstants.Error);
                }

                // If we got this far, something failed, redisplay form
                return View(model);
            }
            catch
            {
                return View(model);
            }
        }
    }
}