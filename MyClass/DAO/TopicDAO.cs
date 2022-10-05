using MyClass.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClass.DAO
{
    public class TopicDAO
    {
        private MyDBContext db = new MyDBContext();
        //Trả về danh sách các mẫu tin
        public List<Topic> getList(string Status = "All")
        {
            List<Topic> list = null;
            switch (Status)
            {
                case "Index":
                    {
                        list = db.Topics.Where(m => m.Status != 0).ToList();
                        break;
                    }
                case "Trash":
                    {
                        list = db.Topics.Where(m => m.Status == 0).ToList();
                        break;
                    }

                default:
                    list = db.Topics.ToList();
                    break;
            }
            return list;
        }

        //

        public List<Topic> getList(int id)
        {
            List<Topic> list = null;
            list = db.Topics.Where(m => m.Status != 1 && m.Id==id).ToList();
            return list;
        }

        //Trả về một mẫu tin
        public Topic getRow(int? id)
        {
            if (id == null)
            {
                return null;
            }
            else
            {
                return db.Topics.Find(id);
            }
        }
        public Topic getRow(String slug)
        {
            var row = db.Topics.Where(m => m.Slug == slug).FirstOrDefault();
            return row;
        }
        public long getCount()
        {
            var count = db.Topics.Count();
            return count;
        }
        //Thêm mẫu tin
        public int Insert(Topic row)
        {
            db.Topics.Add(row);
            return db.SaveChanges();
        }
        //cập nhập mẫu tin
        public int Update(Topic row)
        {
            db.Entry(row).State = EntityState.Modified;
            return db.SaveChanges();
        }
        //xóa mẫu tin
        public int Delete(Topic row)
        {
            db.Topics.Remove(row);
            return db.SaveChanges();
        }

    }
}
