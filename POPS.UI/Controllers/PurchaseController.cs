using POPS.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace POPS.UI.Controllers
{
    [RoutePrefix("Purchase")]
    public class PurchaseController : Controller
    {
        // GET: Purchase
        public ActionResult Index()
        {
            List<Item> list = new List<Item>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost/POPS/api/");
                var responseTask =  client.GetAsync("GetAll");
                responseTask.Wait();

                if(responseTask.Result.IsSuccessStatusCode)
                {
                    var readtask =  responseTask.Result.Content.ReadAsStringAsync();
                    readtask.Wait();
                    list = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Item>>(readtask.Result);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Server Error.");
                }
            }
                return View(list);
        }

        // GET: Purchase/Details/5
        [Route("Details/{id}")]
        public ActionResult Details(int id)
        {
            OrderDetail details = new OrderDetail();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost/POPS/api/");
                var response = client.GetAsync("OrderDetails/" + id.ToString());
                response.Wait();

                if (response.Result.IsSuccessStatusCode)
                {
                    var readtask = response.Result.Content.ReadAsStringAsync();
                    readtask.Wait();
                    details = Newtonsoft.Json.JsonConvert.DeserializeObject<OrderDetail>(readtask.Result);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Server Error.");
                }
            }
            return View(details);
        }
        
        [Route("OrderSummary")]
        public ActionResult OrderList()
        {
            List<OrderDetail> list = new List<OrderDetail>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost/POPS/api/");
                var response = client.GetAsync("OrderList");
                response.Wait();

                if (response.Result.IsSuccessStatusCode)
                {
                    var readtask = response.Result.Content.ReadAsStringAsync();
                    readtask.Wait();
                    list = Newtonsoft.Json.JsonConvert.DeserializeObject<List<OrderDetail>>(readtask.Result);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Server Error.");
                }
            }
            return View("OrderList",list);
        }
        

        // GET: Purchase/Delete/5
        [Route("Delete/{id}")]
        public ActionResult Delete(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost/POPS/api/Order/");
                var deleteTask = client.DeleteAsync(Convert.ToString(id));
                deleteTask.Wait();

                if (deleteTask.Result.IsSuccessStatusCode)
                {
                    return RedirectToAction("OrderSummary");
                }
            }
            return RedirectToAction("Index");
           
        }
    }
}
