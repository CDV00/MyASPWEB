using MyClass.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClass.DAO
{
    public class SupplierDAO
    {
        private MyDBContext db = new MyDBContext();
        //Trả về danh sách các mẫu tin
        public List<Supplier> getList(string Status = "All")
        {
            List<Supplier> list = null;
            switch (Status)
            {
                case "Index":
                    {
                        list = db.Suppliers.Where(m => m.Status != 0).ToList();
                        break;
                    }
                case "Trash":
                    {
                        list = db.Suppliers.Where(m => m.Status == 0).ToList();
                        break;
                    }
                case "HienHanh":
                    {
                        list = db.Suppliers.Where(m => m.Status == 1).ToList();
                        break;
                    }

                default:
                    list = db.Suppliers.ToList();
                    break;
            }
            return list;
        }
        //Trả về một mẫu tin
        public Supplier getRow(int? id)
        {
            if (id == null)
            {
                return null;
            }
            else
            {
                return db.Suppliers.Find(id);
            }
        }
        public Supplier getRow(String slug)
        {
            var row = db.Suppliers.Where(m => m.Slug == slug).FirstOrDefault();
            return row;
        }
        public long getCount()
        {
            var count = db.Suppliers.Count();
            return count;
        }
        //Thêm mẫu tin
        public int Insert(Supplier row)
        {
            db.Suppliers.Add(row);
            return db.SaveChanges();
        }
        //Thêm mẫu tin
        public int Update(Supplier row)
        {
            db.Entry(row).State = EntityState.Modified;
            return db.SaveChanges();
        }
        //Thêm mẫu tin
        public int Delete(Supplier row)
        {
            db.Suppliers.Remove(row);
            return db.SaveChanges();
        }

    }
}