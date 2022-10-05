using MyClass.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClass.DAO
{
    public class OrderDetailDAO
    {
        private MyDBContext db = new MyDBContext();
        
        public Orderdetail getRow(int? id)
        {
            if (id == null)
            {
                return null;
            }
            else
            {
                return db.Orderdetails.Find(id);
            }
        }

        public long getCount()
        {
            var count = db.Orderdetails.Count();
            return count;
        }
        
        //Thêm mẫu tin s
        public bool InsertUser(Orderdetail detail)
        {
            try
            {
                db.Orderdetails.Add(detail);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }

        }


        //cập nhập mẫu tin
        public int Update(Orderdetail row)
        {
            db.Entry(row).State = EntityState.Modified;
            return db.SaveChanges();
        }
        //xóa mẫu tin
        public int Delete(Orderdetail row)
        {
            db.Orderdetails.Remove(row);
            return db.SaveChanges();
        }

    }
}