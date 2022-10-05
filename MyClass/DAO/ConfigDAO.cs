using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyClass.Models;
namespace MyClass.DAO
{
    public class ConfigDAO
    {
        private MyDBContext db = new MyDBContext();
        //Trả về danh sách các mẫu tin
        public List<Config> getList()
        {
            return db.Configs.ToList();
        }
        //Trả về một mẫu tin
        public Config getRow(int? id)
        {
            if (id == null)
            {
                return null;
            }
            else
            {
                return db.Configs.Find(id);
            }
        }
        //Thêm mẫu tin
        public int Insert(Config row)
        {
            db.Configs.Add(row);
            return db.SaveChanges();
        }
        //Thêm mẫu tin
        public int Update(Config row)
        {
            db.Entry(row).State = EntityState.Modified;
            return db.SaveChanges();
        }
        //Thêm mẫu tin
        public int Delete(Config row)
        {
            db.Configs.Remove(row);
            return db.SaveChanges();
        }

    }
}
