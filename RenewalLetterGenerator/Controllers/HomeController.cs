using RenewalLetterGenerator.Helper;
using RenewalLetterGenerator.Interfaces;
using RenewalLetterGenerator.Models;
using RenewalLetterGenerator.ServiceLayer;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;

namespace RenewalLetterGenerator.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProcessFile _processFile;

        public HomeController(IProcessFile processFile)
        {
            _processFile = processFile;
            ////using property injection need
            //_processFile = new ProcessFileFacadeService();
        }
    
        public ActionResult Upload()
        {
            //this view bag data is used to display the destination set in web config 
            ViewBag.DestinationPath = GenericHelper.GetValueFromWebConfig("Destination_File_Path");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Upload(HttpPostedFileBase upload)
        {
            //this view bag data is used to display the destination set in web config 
            ViewBag.DestinationPath = GenericHelper.GetValueFromWebConfig("Destination_File_Path");

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