using MyClass.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClass.DAO
{
    
    public class PostDAO
    {
        private MyDBContext db = new MyDBContext();

        public List<Post> getList(int id)
        {
            List<Post> list =db.Posts.Where(m => m.TopId == id && m.Status == 1).ToList();
            return list;
        }

        public List<Post> getRow(string slug)
        {
            List<Post> list = db.Posts.Where(m => m.Slug == slug && m.Status == 1).ToList();
            return list;
        }


        //Thêm mẫu tin
        public int Insert(Post row)
        {
            db.Posts.Add(row);
            return db.SaveChanges();
        }
        //cập nhập mẫu tin
        public int Update(Post row)
        {
            db.Entry(row).State = EntityState.Modified;
            return db.SaveChanges();
        }
        //xóa mẫu tin
        public int Delete(Post row)
        {
            db.Posts.Remove(row);
            return db.SaveChanges();
        }
    }
}
