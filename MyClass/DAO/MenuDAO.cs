using MyClass.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClass.DAO
{
    public class MenuDAO
    {
    private MyDBContext db = new MyDBContext();
    //Trả về danh sách các mẫu tin
    public List<Menu> getList(string Status = "All")
    {
        List<Menu> list = null;
        switch (Status)
        {
            case "Index":
                {
                    list = db.Menus.Where(m => m.Status != 0).ToList();
                    break;
                }
            case "Trash":
                {
                    list = db.Menus.Where(m => m.Status == 0).ToList();
                    break;
                }

            default:
                list = db.Menus.ToList();
                break;
        }
        return list;
    }
    //Trả về một mẫu tin
    public Menu getRow(int? id)
    {
        if (id == null)
        {
            return null;
        }
        else
        {
            return db.Menus.Find(id);
        }
    }
    //Thêm mẫu tin
    public int Insert(Menu row)
    {
        db.Menus.Add(row);
        return db.SaveChanges();
    }
    //Thêm mẫu tin
    public int Update(Menu row)
    {
        db.Entry(row).State = EntityState.Modified;
        return db.SaveChanges();
    }
    //Thêm mẫu tin
    public int Delete(Menu row)
    {
        db.Menus.Remove(row);
        return db.SaveChanges();
    }

}
}

