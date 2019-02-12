using POPS.API.Models;
using POPS.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace POPS.API.Controllers
{ 
    public class OrderController : ApiController
    {
        PurchaseOrderProcessingEntity obj = new PurchaseOrderProcessingEntity();

        // GET: api/Order
        [Route("api/GetAll")]
        public IHttpActionResult GetAllItem()
        {
            List<Item> list = new List<Item>();

            var result =  obj.GetAll();
            list = result.Select(x => new Item
            {
                ItemCode = x.ITCODE,
                ItemDesc = x.ITDESC,
                ItemRate = x.ITRATE,
                SuppNo = x.SUPLNO
            }).ToList();

            if(list.Count == 0)
            {
                return NotFound();
            }
            return Ok(list);
        }

        // GET: api/Order/5
        [Route("api/OrderDetails/{id}")]
        public OrderDetail Get(int id)
        {
            OrderDetail orderDetail = new OrderDetail();

            var result = obj.GetOrderDetails(id);

            orderDetail.ItemCode = result.ITCODE;
            orderDetail.OrderNumber = result.PONO;
            orderDetail.SupplierCode = result.SUPLNO;
            orderDetail.OrderDate = Convert.ToDateTime(result.PODATE).ToString("MM/dd/yyyy");

            return orderDetail;
        }

        [Route("api/OrderList")]
        public List<OrderDetail> Get()
        {
            List<OrderDetail> orderList = new List<OrderDetail>();

            var result = obj.GetOrderList();

            orderList = result.Select(x => new OrderDetail
            {
                ItemCode = x.ITCODE,
                OrderNumber = x.PONO,
                Quantity = x.QTY,
              
            }).ToList();
            
            return orderList;
        }

        // POST: api/Order
        [Route("api/Add")]
        public int Post(CreateOrder order)
        {
            return obj.CreateOrder(order.ItemCode, order.SupCode, order.Qty);
        }
               

        // DELETE: api/Order/5
        public IHttpActionResult Delete(int id)
        {  
            if (id < 0)
                return BadRequest("Not a valid order number");

            obj.DeleteOrder(id);

            return Ok();

        }
    }
}
