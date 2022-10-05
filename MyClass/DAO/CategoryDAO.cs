using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyClass.Models;

namespace MyClass.DAO
{
    public class CategoryDAO
    {
        private MyDBContext db = new MyDBContext();
        //Trả về danh sách các mẫu tin
        public List<Category> getList(string Status="All")
        {
            List<Category> list = null;
            switch (Status)
            {
                case "Index":
                    {
                        list = db.Categorys.Where(m => m.Status != 0).ToList();
                        break;
                    }
                case "Trash":
                    {
                        list = db.Categorys.Where(m => m.Status == 0).ToList();
                        break;
                    }
                case "HienHanh":
                    {
                        list = db.Categorys.Where(m => m.Status == 1).ToList();
                        break;
                    }

                default:
                    list = db.Categorys.ToList();
                    break;
            }
            return list;
        }

        

        //Trả về một mẫu tin
        public Category getRow(int? id)
        {
            if (id == null)
            {
                return null;
            }
            else
            {
                return db.Categorys.Find(id);
            }
        }
        public Category getRow(String slug)
        {
            var row = db.Categorys.Where(m => m.Slug == slug).FirstOrDefault();
            return row;
        }
        public long getCount()
        {
            var count = db.Categorys.Count();
            return count;
        }
        //Thêm mẫu tin
        public int Insert(Category row)
        {
            db.Categorys.Add(row);
            return db.SaveChanges();
        }
        //cập nhập mẫu tin
        public int Update(Category row)
        {
            db.Entry(row).State = EntityState.Modified;
            return db.SaveChanges();
        }
        //xóa mẫu tin
        public int Delete(Category row)
        {
            db.Categorys.Remove(row);
            return db.SaveChanges();
        }

    }
}
