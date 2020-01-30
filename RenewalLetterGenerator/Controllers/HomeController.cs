using RenewalLetterGenerator.Interfaces;
using RenewalLetterGenerator.Models;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using System.Linq;

namespace RenewalLetterGenerator.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProcessFile _processFile;

        public HomeController(IProcessFile processFile)
        {
            _processFile = processFile;
            
        }
    
        public ActionResult Upload()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Upload(HttpPostedFileBase upload)
        {

            if (ModelState.IsValid)
            {

                if (upload != null && upload.ContentLength > 0)
                {

                    //need to check and change if we should not use .csv
                    if (upload.FileName.EndsWith(".csv"))
                    {
                        //declaring success flag
                        bool success = false;

                        //reading the values from a csv file and mapping it to the view Model 
                        var viewModel = _processFile.ReadCsvFile(upload);

                        if (viewModel != null && viewModel.Count > 0)
                        {
                             success = _processFile.GenerateRenewalLetters(viewModel);
                        }

                        var MemberDetails = new List<MemberDetailsModel>();

                        foreach (var member in viewModel)
                        {

                            MemberDetails.Add(member.MemDetails);
                        }
                        ViewBag.AnyFileNewlyGenerated = MemberDetails.Any(x => x.IsGeneratedNow == true);
                        return View(MemberDetails);
                    }
                    else
                    {
                        // displays error if differnt format file is 
                        ModelState.AddModelError("File", "This file format is not supported");
                        return View();
                    }
                }
                else
                {
                    // displays error if upload 
                    ModelState.AddModelError("File", "Please Upload Your file");
                }
            }
            return View();
        }
    }
}