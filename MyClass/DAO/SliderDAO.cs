using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyClass.Models;

    public class SliderDAO
    {
        private MyDBContext db = new MyDBContext();
        //Trả về danh sách các mẫu tin
        public List<Slider> getList(string Status = "All")
        {
            List<Slider> list = null;
            switch (Status)
            {
                case "Index":
                    {
                        list = db.Sliders.Where(m => m.Status != 0).ToList();
                        break;
                    }
                case "Trash":
                    {
                        list = db.Sliders.Where(m => m.Status == 0).ToList();
                        break;
                    }

                default:
                    list = db.Sliders.ToList();
                    break;
            }
            return list;
        }
        //Trả về một mẫu tin
        public Slider getRow(int? id)
        {
            if (id == null)
            {
                return null;
            }
            else
            {
                return db.Sliders.Find(id);
            }
        }
        //Thêm mẫu tin
        public int Insert(Slider row)
        {
            db.Sliders.Add(row);
            return db.SaveChanges();
        }
        //Thêm mẫu tin
        public int Update(Slider row)
        {
            db.Entry(row).State = EntityState.Modified;
            return db.SaveChanges();
        }
        //Thêm mẫu tin
        public int Delete(Slider row)
        {
            db.Sliders.Remove(row);
            return db.SaveChanges();
        }

    }
