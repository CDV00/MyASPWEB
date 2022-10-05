using MyClass.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClass.DAO
{
    public class UserDAO
    {
        private MyDBContext db = new MyDBContext();
        //Trả về danh sách các mẫu tin
        public List<User> getList(string Status = "All")
        {
            List<User> list = null;
            switch (Status)
            {
                case "Index":
                    {
                        list = db.Users.Where(m => m.Status != 0).ToList();
                        break;
                    }
                case "Trash":
                    {
                        list = db.Users.Where(m => m.Status == 0).ToList();
                        break;
                    }

                default:
                    list = db.Users.ToList();
                    break;
            }
            return list;
        }
        //Trả về một mẫu tin
        public User getRow(int? id)
        {
            if (id == null)
            {
                return null;
            }
            else
            {
                return db.Users.Find(id);
            }
        }
        //Thêm mẫu tin
        public int Insert(User row)
        {
            db.Users.Add(row);
            return db.SaveChanges();
        }
        //Thêm mẫu tin
        public int Update(User row)
        {
            db.Entry(row).State = EntityState.Modified;
            return db.SaveChanges();
        }
        //Thêm mẫu tin
        public int Delete(User row)
        {
            db.Users.Remove(row);
            return db.SaveChanges();
        }

    }
}
