using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POPS.Entity
{
    public class PurchaseOrderProcessingEntity
    {
        public List<ITEM> GetAll()
        {
            using (PODbEntities db = new PODbEntities())
            {
                return db.ITEMs.ToList();
            }
        }

        public int CreateOrder(string itemCode,string supNo,int qty)
        {
            int orderNo = 0;
            using (PODbEntities db = new PODbEntities())
            {
                POMASTER pomaster = new POMASTER();
                pomaster.PODATE = DateTime.Now;
                pomaster.SUPLNO = supNo;
                pomaster.ITCODE = itemCode;

                db.POMASTERs.Add(pomaster);
                db.SaveChanges();

                PODETAIL orderDetail = new PODETAIL();
                orderDetail.ITCODE = itemCode;
                orderDetail.QTY = qty;
                orderNo = db.POMASTERs.Where(x => x.ITCODE == itemCode).Max(O => O.PONO);
                orderDetail.PONO = orderNo;

                db.PODETAILs.Add(orderDetail);
                db.SaveChanges();
            }

            return orderNo;
        }

        public POMASTER GetOrderDetails(int id)
        {
            using (PODbEntities db = new PODbEntities())
            {
                return db.POMASTERs.Where(x => x.PONO == id).FirstOrDefault();
            }
        }

        public List<PODETAIL> GetOrderList()
        {
            using (PODbEntities db = new PODbEntities())
            {
                return db.PODETAILs.ToList();
            }
        }

        public void DeleteOrder(int id)
        {
            using (PODbEntities db = new PODbEntities())
            {
                var orderDetail = db.PODETAILs.Where(x => x.PONO == id).FirstOrDefault();

                db.Entry(orderDetail).State = System.Data.Entity.EntityState.Deleted;
                db.SaveChanges();

                var orderMaster = db.POMASTERs.Where(x => x.PONO == id).FirstOrDefault();
                db.Entry(orderMaster).State = System.Data.Entity.EntityState.Deleted;
                db.SaveChanges();
            }
        }
    }
}
