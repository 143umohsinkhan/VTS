using DAL;
using Microsoft.AspNet.Identity;
using Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Utility;

namespace VST.Controllers
{
    public class BaseController : Controller
    {
        IRepository<VTSUSER, long> userRepository;
        public UserRepository UserRepository
        {
            get
            {
                return userRepository as UserRepository;
            }
        }

        public BaseController(IRepository<VTSUSER, long> userRepository)
        {
            this.userRepository = userRepository;
        }

        // GET: Base
        public ActionResult Index()
        {
            return View();
        }

        public void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        public string UploadFileDeleteExisting(HttpPostedFileBase imagefile, string Username, string oldFilePath = "", bool IsDelete = false)
        {
            if (imagefile == null || imagefile.ContentLength <= 0) return string.Empty;

            var newFilename = "/"+VTSConstants.UploadFileFolderName+"/"+Username+DateTime.Now.ToFileTime()+imagefile.FileName+"";
            string _path = GetFullPath(newFilename);
            imagefile.SaveAs(_path);

            if (IsDelete && !oldFilePath.IsNull())
            {
                if (System.IO.File.Exists(GetFullPath(oldFilePath)))
                    System.IO.File.Delete(GetFullPath(oldFilePath));
            }
            return newFilename;
        }

        string GetFullPath(string fileName)
        {
            return Path.Combine(Server.MapPath("~" + fileName));
        }

        public void AddErrors(string message)
        {
            AddErrors(new IdentityResult(message));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && userRepository != null)
            {
                userRepository.Dispose();
                userRepository = null;
            }
            base.Dispose(disposing);
        }
    }
}