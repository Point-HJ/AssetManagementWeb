using AssetManagementWeb.Models;
using AssetManagementWeb.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using AssetManagementWeb.Database;

namespace AssetManagementWeb.Controllers
{
    public class AssetController : Controller
    {
        // GET: Asset
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Test()
        {
            return View();
        }

        public ActionResult List()
        {
            List<LocatedAssetsViewModel> model = new List<LocatedAssetsViewModel>();

            PointSQLSrv1Entities entities = new PointSQLSrv1Entities();
            try
            {
                List<AssetLocations> assets = entities.AssetLocations.ToList();

                //muodostetaan näkymämalli tietokannan rivien pohjalta

                foreach (AssetLocations asset in assets )
                {
                    LocatedAssetsViewModel view = new LocatedAssetsViewModel();
                    view.Id = asset.Id;
                    view.LocationCode = asset.AssetLocation.Code;
                    view.LocationName = asset.AssetLocation.Name;
                    view.AssetCode = asset.Assets.Code;
                    view.AssetName = asset.Assets.Type + ": "+asset.Assets.Model;
                    view.LastSeen = asset.LastSeen.ToString();

                    model.Add(view);
                }
            }
            finally
            {
                entities.Dispose();
            }

            return View(model);
        }


        public ActionResult ListJson()
        {
            List<LocatedAssetsViewModel> model = new List<LocatedAssetsViewModel>();

            PointSQLSrv1Entities entities = new PointSQLSrv1Entities();
            try
            {
                List<AssetLocations> assets = entities.AssetLocations.ToList();

                //muodostetaan näkymämalli tietokannan rivien pohjalta

                foreach (AssetLocations asset in assets)
                {
                    LocatedAssetsViewModel view = new LocatedAssetsViewModel();
                    view.Id = asset.Id;
                    view.LocationCode = asset.AssetLocation.Code;
                    view.LocationName = asset.AssetLocation.Name;
                    view.AssetCode = asset.Assets.Code;
                    view.AssetName = asset.Assets.Type + ": " + asset.Assets.Model;
                    view.LastSeen = asset.LastSeen.ToString();

                    model.Add(view);
                }
            }
            finally
            {
                entities.Dispose();
            }

            return Json(model, JsonRequestBehavior.AllowGet);
        }



        // POST: Asset/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        public JsonResult AssignLocation()
        {

            string json = Request.InputStream.ReadToEnd();
            AssingnLocationModel inputData = 
                JsonConvert.DeserializeObject<AssingnLocationModel>(json);

            bool success = false;
            string error = "";
            PointSQLSrv1Entities entities = new PointSQLSrv1Entities();
            try
            {
                //haetaan ensin paikan id-numero koodin perusteella
                int locationId = (from l in entities.AssetLocation
                                  where l.Code == inputData.LocationCode
                                  select l.Id).FirstOrDefault();

                //haetaan laitteen id-numero koodin perusteella
                int assetId = (from a in entities.Assets
                                  where a.Code == inputData.AssetCode
                                  select a.Id).FirstOrDefault();

                if ((locationId > 0) && (assetId > 0))
                {
                    //tallennetaan uusi rivi aíkaleiman kanssa kantaan
                    AssetLocations newEntry = new AssetLocations();
                    newEntry.LocationId = locationId;
                    newEntry.AssetId = assetId;
                    newEntry.LastSeen = DateTime.Now;

                    entities.AssetLocations.Add(newEntry);
                    entities.SaveChanges();

                    success = true;
                }
            }
            catch (Exception ex)
            {
                error = ex.GetType().Name + ": " + ex.Message;
            }

            finally
            {
                entities.Dispose();
            }

            //palautetaan JSON-muotoinen tulos kutsujalle
            var result = new { success = success, error = error };
            return Json(result);
        }

        // GET: Asset/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Asset/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Asset/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Asset/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
