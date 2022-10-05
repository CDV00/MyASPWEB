using MyClass.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClass.DAO
{
    public class OrderDAO
    {
        private MyDBContext db = new MyDBContext();
        //Trả về danh sách các mẫu tin
        public List<Order> getList(string Status = "All")
        {
            List<Order> list = null;
            switch (Status)
            {
                case "Index":
                    {
                        list = db.Orders.Where(m => m.Status != 0).ToList();
                        break;
                    }
                case "Trash":
                    {
                        list = db.Orders.Where(m => m.Status == 0).ToList();
                        break;
                    }

                default:
                    list = db.Orders.ToList();
                    break;
            }
            return list;
        }
        //
        public List<ProductsBought> getListProductsBought(int userId)
        {
            List<ProductsBought> list = null;

            list = db.Orders.Join(
                db.Orderdetails.Join(
                    db.Products,
                    o => o.ProductId,
                    p => p.Id,
                    (o, p) => new
                    {
                        p.Name,
                        p.Slug,
                        p.Image,
                        o.OrderId,
                        o.Price,
                        o.Quantity,
                        o.Amount
                    }

                    ),
                ord => ord.Id,
                ordt => ordt.OrderId,
                (ord, ordt) => new ProductsBought
                {
                    Id = ord.Id,
                    Code = ord.Code,
                    UserId = ord.UserId,
                    CreatedDate = ord.CreatedDate,
                    ExportDate = ord.ExportDate,
                    DeliveryAddress = ord.DeliveryAddress,
                    DeliveryName = ord.DeliveryName,
                    DeliveryPhone = ord.DeliveryPhone,
                    DeliveryEmail = ord.DeliveryEmail,
                    Name = ordt.Name,
                    Slug = ordt.Slug,
                    Image = ordt.Image,
                    Price = ordt.Price,
                    Quantity = ordt.Quantity,
                    Amount = ordt.Amount,
                    Status = ord.Status
                }
                ).Where(m =>m.UserId == userId && m.Status == 1).ToList();

            return list;
        }


        //Trả về một mẫu tin
        public Order getRow(int? id)
        {
            if (id == null)
            {
                return null;
            }
            else
            {
                return db.Orders.Find(id);
            }
        }

        public long getCount()
        {
            var count = db.Orders.Count();
            return count;
        }
        //Thêm mẫu tin
        public int Insert(Order row)
        {
            db.Orders.Add(row);
            return db.SaveChanges();
        }
        //Thêm mẫu tin s
        public long InsertUser(Order order)
        {
            db.Orders.Add(order);
            db.SaveChanges();
            return order.Id;

        }


        //cập nhập mẫu tin
        public int Update(Order row)
        {
            db.Entry(row).State = EntityState.Modified;
            return db.SaveChanges();
        }
        //xóa mẫu tin
        public int Delete(Order row)
        {
            db.Orders.Remove(row);
            return db.SaveChanges();
        }
        //mẫu tin mới nhất
        public Order getRowNew()
        {
            return db.Orders.OrderByDescending(obj => obj.Id).Where(m => m.Status == 1).FirstOrDefault();
        }
    }
}
