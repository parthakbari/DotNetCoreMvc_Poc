using HegicMvc_Poc.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using System;
using System.Collections.Generic;
using System.Linq;

namespace HegicMvc_Poc.Controllers
{
    public class VehicleController : BaseController
    {
        private readonly IHttpContextAccessor _httpContextAccessor = null;
        
        public VehicleController(IHttpContextAccessor _httpContextAccessor, IUserService _userService) : base(_httpContextAccessor,  _userService)
        {
           this._httpContextAccessor = _httpContextAccessor;
            
        }

        /// <summary>
        /// Index method
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            base.session.SetString("Organization", "HdfcErgo");
            return View();
        }

        public IActionResult GetMakeList()
        {
            string Name = base.session.GetString("Organization");
            VehicleRepositry vehicleRepositry = new VehicleRepositry();
            List<NameValueData> makeList = vehicleRepositry.GetMakeList();
            return PartialView("_MakeList", makeList);
        }

        public IActionResult GetModelVariantList()
        {
            VehicleRepositry vehicleRepositry = new VehicleRepositry();
            List<ModelVariant> modelList = vehicleRepositry.GetModelVariantList();
            return Json(modelList);
        }

        public PartialViewResult LoadVehicleBasicDetail(string mode, string value, string name)
        {
            switch (mode)
            {
                case "MD":
                    VehicleRepositry vehicleRepositry = new VehicleRepositry();
                    List<ModelVariant> modelList = vehicleRepositry.GetModelVariantList().Where(x => x.MakeId == Convert.ToInt64(value)).ToList();
                    return PartialView("_ModelList", modelList);
                default:
                    break;
            }
            return PartialView();

        }

        public void GenerateError()
        {
            var a = 1;
            var b = 0;
            var c = a / b;
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}