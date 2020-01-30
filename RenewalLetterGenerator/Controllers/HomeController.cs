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
        /// <summary>
        /// Get of Upload method
        /// </summary>
        /// <returns>Provides user with upload capability</returns>
        public ActionResult Upload()
        {

            return View();
        }
        /// <summary>
        /// post method of upload journey
        /// </summary>
        /// <param name="upload"></param>
        /// <returns>generates the files and also displays the list of valid members for whom files are generated</returns>
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

                        var memberDetails = new List<MemberDetailsModel>();

                        //filtering the data which was read from excel and displaying wether they are generated in last run
                        foreach (var member in viewModel)
                        {

                            memberDetails.Add(member.MemDetails);
                        }

                        //for displaying the message 
                        ViewBag.AnyFileNewlyGenerated = memberDetails.Any(x => x.IsGeneratedNow == true);
                        return View(memberDetails);
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