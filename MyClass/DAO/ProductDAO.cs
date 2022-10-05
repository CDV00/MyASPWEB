using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyClass.Models;


namespace MyClass.DAO
{
    public class ProductDAO
    {
        private MyDBContext db = new MyDBContext();
        //Trả về danh sách các mẫu tin
        public List<Product> getList(string Status = "All")
        {
            List<Product> list = null;
            switch (Status)
            {
                case "Index":
                    {
                        list = db.Products.Where(m => m.Status != 0).ToList();
                        break;
                    }
                case "Trash":
                    {
                        list = db.Products.Where(m => m.Status == 0).ToList();
                        break;
                    }

                default:
                    list = db.Products.ToList();
                    break;
            }
            return list;
        }
        //

        public List<ProductInfo> getListJoin(string Status = "All")
        {
            List<ProductInfo> list = null;
            switch (Status)
            {
                case "Index":
                    {
                        list = db.Suppliers.Join(
                                db.Products.Join(
                                    db.Categorys,
                                    p => p.CategoryId,
                                    c => c.Id,
                                    (p, c) => new
                                    {
                                        p.Id,
                                        p.Name,
                                        p.CategoryId,
                                        p.SupplierId,
                                        p.Slug,
                                        p.Image,
                                        p.Detail,
                                        p.Number,
                                        p.Price,
                                        p.PriceSale,
                                        p.Metakey,
                                        p.Metadesc,
                                        p.Created_by,
                                        p.Created_at,
                                        p.Updated_by,
                                        p.Updated_at,
                                        p.Status,
                                        CategoryName = c.Name
                                    }
                                    ),
                                s => s.Id,
                                pr => pr.SupplierId,
                                (s, pr) => new ProductInfo
                                {
                                    Id = pr.Id,
                                    Name = pr.Name,
                                    CategoryName = pr.CategoryName,
                                    SupplierName = s.Name,
                                    CategoryId = pr.CategoryId,
                                    SupplierId = pr.SupplierId,
                                    Slug = pr.Slug,
                                    Image = pr.Image,
                                    Detail = pr.Detail,
                                    Number = pr.Number,
                                    Price = pr.Price,
                                    PriceSale = pr.PriceSale,
                                    Metakey = pr.Metakey,
                                    Metadesc = pr.Metadesc,
                                    Created_at = pr.Created_at,
                                    Created_by = pr.Created_by,
                                    Updated_at = pr.Updated_at,
                                    Updated_by = pr.Updated_by,
                                    Status = pr.Status
                                }
                                ).Where(m => m.Status != 0).ToList();
                        break;
                    }
                case "Trash":
                    {
                        list = db.Suppliers.Join(
                                db.Products.Join(
                                    db.Categorys,
                                    p => p.CategoryId,
                                    c => c.Id,
                                    (p, c) => new
                                    {
                                        p.Id,
                                        p.Name,
                                        p.CategoryId,
                                        p.SupplierId,
                                        p.Slug,
                                        p.Image,
                                        p.Detail,
                                        p.Number,
                                        p.Price,
                                        p.PriceSale,
                                        p.Metakey,
                                        p.Metadesc,
                                        p.Created_by,
                                        p.Created_at,
                                        p.Updated_by,
                                        p.Updated_at,
                                        p.Status,
                                        CategoryName = c.Name
                                    }
                                    ),
                                s => s.Id,
                                pr => pr.SupplierId,
                                (s, pr) => new ProductInfo
                                {
                                    Id = pr.Id,
                                    Name = pr.Name,
                                    CategoryName = pr.CategoryName,
                                    SupplierName = s.Name,
                                    CategoryId = pr.CategoryId,
                                    SupplierId = pr.SupplierId,
                                    Slug = pr.Slug,
                                    Image = pr.Image,
                                    Detail = pr.Detail,
                                    Number = pr.Number,
                                    Price = pr.Price,
                                    PriceSale = pr.PriceSale,
                                    Metakey = pr.Metakey,
                                    Metadesc = pr.Metadesc,
                                    Created_at = pr.Created_at,
                                    Created_by = pr.Created_by,
                                    Updated_at = pr.Updated_at,
                                    Updated_by = pr.Updated_by,
                                    Status = pr.Status
                                }
                                ).Where(m => m.Status == 0).ToList();
                        break;
                    }

                default:
                    list = db.Suppliers.Join(
                                db.Products.Join(
                                    db.Categorys,
                                    p => p.CategoryId,
                                    c => c.Id,
                                    (p, c) => new
                                    {
                                        p.Id,
                                        p.Name,
                                        p.CategoryId,
                                        p.SupplierId,
                                        p.Slug,
                                        p.Image,
                                        p.Detail,
                                        p.Number,
                                        p.Price,
                                        p.PriceSale,
                                        p.Metakey,
                                        p.Metadesc,
                                        p.Created_by,
                                        p.Created_at,
                                        p.Updated_by,
                                        p.Updated_at,
                                        p.Status,
                                        CategoryName = c.Name
                                    }
                                    ),
                                s => s.Id,
                                pr => pr.SupplierId,
                                (s, pr) => new ProductInfo
                                {
                                    Id = pr.Id,
                                    Name = pr.Name,
                                    CategoryName = pr.CategoryName,
                                    SupplierName = s.Name,
                                    CategoryId = pr.CategoryId,
                                    SupplierId = pr.SupplierId,
                                    Slug = pr.Slug,
                                    Image = pr.Image,
                                    Detail = pr.Detail,
                                    Number = pr.Number,
                                    Price = pr.Price,
                                    PriceSale = pr.PriceSale,
                                    Metakey = pr.Metakey,
                                    Metadesc = pr.Metadesc,
                                    Created_at = pr.Created_at,
                                    Created_by = pr.Created_by,
                                    Updated_at = pr.Updated_at,
                                    Updated_by = pr.Updated_by,
                                    Status = pr.Status
                                }
                                ).ToList();
                    break;
            }
            return list;
        }

        //Trả về một mẫu tin
        public Product getRow(int? id)
        {
            if (id == null)
            {
                return null;
            }
            else
            {
                return db.Products.Where(m => m.Id == id).FirstOrDefault();
            }
        }
        //
        public Product getRow(String slug)
        {
            var row = db.Products.Where(m => m.Slug == slug).FirstOrDefault();
            return row;
        }
        //Thêm mẫu tin
        public int Insert(Product row)
        {
            db.Products.Add(row);
            return db.SaveChanges();
        }
        //Thêm mẫu tin
        public int Update(Product row)
        {
            db.Entry(row).State = EntityState.Modified;
            return db.SaveChanges();
        }
        //Thêm mẫu tin
        public int Delete(Product row)
        {
            db.Products.Remove(row);
            return db.SaveChanges();
        }
        //sản phẩm sale
        public List<Product> getListSale()
        {
            return db.Products.Where(m => m.PriceSale > 0 && m.Status == 1).ToList();
        }
        public List<Product> getListCat(int id)
        {
            return db.Products.Where(m => m.CategoryId == id && m.Status == 1).ToList();
        }
        public List<Product> getListSup(int id)
        {
            return db.Products.Where(m => m.SupplierId == id && m.Status == 1).ToList();
        }
        public List<Product> getListSearch(string key)
        {
            //key = "%" + key + "%";
            /*query = query.Where(s => s.UnsignFullName.Contains(keyword));
            Contains() = '%keyword%'
            StartsWith() = LIKE 'keyword%'
            EndsWith() = LIKE '%keyword'*/
            return db.Products.Where(m => m.Name.Contains(key)).ToList();
        }
    }
}

