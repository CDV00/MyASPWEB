using MyClass.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClass.DAO
{
    public class ContactDAO
    {
        private MyDBContext db = new MyDBContext();

        //Trả về một mẫu tin
        public Contact getRow(int Id)
        {
            return db.Contacts.Where(m => m.Id == Id && m.Status == 1).FirstOrDefault();
        }

        //Thêm mẫu tin
        public int Insert(Contact row)
        {
            db.Contacts.Add(row);
            return db.SaveChanges();
        }
        //cập nhập mẫu tin
        public int Update(Contact row)
        {
            db.Entry(row).State = EntityState.Modified;
            return db.SaveChanges();
        }
        //xóa mẫu tin
        public int Delete(Contact row)
        {
            db.Contacts.Remove(row);
            return db.SaveChanges();
        }
    }
}
