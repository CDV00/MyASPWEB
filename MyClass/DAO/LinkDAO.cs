using MyClass.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClass.DAO
{
    public class LinkDAO
    {
        private MyDBContext db = new MyDBContext();
      
        //Trả về một mẫu tin
        public Link getRow(int tableId ,string typeLink)
        {
            return db.Links.Where(m=>m.TableId==tableId && m.TypeLink == typeLink).FirstOrDefault(); 
        }
       
        //Thêm mẫu tin
        public int Insert(Link row)
        {
            db.Links.Add(row);
            return db.SaveChanges();
        }
        //cập nhập mẫu tin
        public int Update(Link row)
        {
            db.Entry(row).State = EntityState.Modified;
            return db.SaveChanges();
        }
        //xóa mẫu tin
        public int Delete(Link row)
        {
            db.Links.Remove(row);
            return db.SaveChanges();
        }

    }
}
