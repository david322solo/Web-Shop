using System;
using System.Collections.Generic;
using System.Data.OracleClient;
using System.Text;
using System.Linq;
using System.Data;
using WebApplication7.Models;
using WebApplication7.ViewModels;

namespace WebApplication7.DataBase
{
    public class DB
    {
        private string connectionString = "Data Source = (DESCRIPTION = " +
                   "(ADDRESS = (PROTOCOL = TCP)(HOST = DESKTOP-BVQAVK6)(PORT = 1521))" +
                  "(CONNECT_DATA =" +
                 "(SERVER = DEDICATED)" +
                  "(SERVICE_NAME = pdb_course)" +
                    ")" +
                   ");User Id = PAS_COURSE;password = 12345;";
        private OracleConnection connection;
        public DB(string connectionString)
        {
            this.connectionString = connectionString;
            connection = new OracleConnection(connectionString);
            try
            {
                connection.Open();
            }
            catch (OracleException ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("Try again later");
            }
            finally
            {
                connection.Close();
            }
        }
        public DB()
        {
            connection = new OracleConnection(connectionString);
        }
        public void AddCategory(Category category)
        {
            connection.Open();
            OracleCommand command = new OracleCommand("add_category", connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            OracleParameter uName = new OracleParameter();
            uName.ParameterName = "c_name";
            uName.OracleType = OracleType.NVarChar;
            uName.Value = category.Name;
            command.Parameters.Add(uName);
            var reader = command.ExecuteReader();
            uName.Dispose();
            command.Dispose();
            reader.Close();
            connection.Close();
        }
        public void Export(string tableName)
        {
            connection.Open();
            OracleCommand command = new OracleCommand("table_to_xml_file", connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            OracleParameter table = new OracleParameter();
            table.ParameterName = "table_name";
            table.Value = tableName.ToUpper();
            command.Parameters.Add(table);
            var reader = command.ExecuteReader();
            table.Dispose();
            command.Dispose();
            reader.Close();
            connection.Close();
        }
        public void Import(string tableName)
        {
            connection.Open();
            OracleCommand command = new OracleCommand("IMPORT_"+tableName.ToUpper(), connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            var reader = command.ExecuteReader();
            command.Dispose();
            reader.Close();
            connection.Close();
        }
        public void AddColore(Colore colore)
        {
            connection.Open();
            OracleCommand command = new OracleCommand("add_colore", connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            OracleParameter uName = new OracleParameter();
            uName.ParameterName = "c_name";
            uName.OracleType = OracleType.NVarChar;
            uName.Value = colore.NameColore;
            command.Parameters.Add(uName);
            var reader = command.ExecuteReader();
            uName.Dispose();
            command.Dispose();
            reader.Close();
            connection.Close();
        }
        public void AddRole(string id, string role)
        {
            int uId= Users.FirstOrDefault(r => r.Login == id).Id;
            int irole = Roles.FirstOrDefault(r => r.RoleName == role).Id;
            connection.Open();
            OracleCommand command = new OracleCommand("add_rl", connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            OracleParameter uName = new OracleParameter();
            uName.ParameterName = "u_id";
            uName.OracleType = OracleType.Number;
            uName.Value = uId;
            command.Parameters.Add(uName);
            OracleParameter uName1 = new OracleParameter();
            uName1.ParameterName = "u_name";
            uName1.OracleType = OracleType.Number;
            uName1.Value = irole;
            command.Parameters.Add(uName1);
            command.ExecuteNonQuery();
            uName.Dispose();
            uName1.Dispose();
            command.Dispose();
            connection.Close();
        }
        public void AddBrand(Brand brand)
        {
            connection.Open();
            OracleCommand command = new OracleCommand("add_brand", connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            OracleParameter uName = new OracleParameter();
            uName.ParameterName = "c_name";
            uName.OracleType = OracleType.NVarChar;
            uName.Value = brand.Name;
            command.Parameters.Add(uName);
            var reader = command.ExecuteReader();
            uName.Dispose();
            command.Dispose();
            reader.Close();
            connection.Close();
        }
        public void AddSezone(Sezone sezone)
        {
            connection.Open();
            OracleCommand command = new OracleCommand("add_sezone", connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            OracleParameter uName = new OracleParameter();
            uName.ParameterName = "c_name";
            uName.OracleType = OracleType.NVarChar;
            uName.Value = sezone.Name;
            command.Parameters.Add(uName);
            var reader = command.ExecuteReader();
            uName.Dispose();
            command.Dispose();
            reader.Close();
            connection.Close();
        }
        public void AddMaterial(Material material)
        {
            connection.Open();
            OracleCommand command = new OracleCommand("add_material", connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            OracleParameter uName = new OracleParameter();
            uName.ParameterName = "c_name";
            uName.OracleType = OracleType.NVarChar;
            uName.Value = material.Name;
            command.Parameters.Add(uName);
            var reader = command.ExecuteReader();
            uName.Dispose();
            command.Dispose();
            reader.Close();
            connection.Close();
        }
        public void AddUser(User user)
        {
            connection.Open();
            OracleCommand command = new OracleCommand("add_user", connection);
            List<Product> products = new List<Product>();
            command.CommandType = System.Data.CommandType.StoredProcedure;
            OracleParameter uName = new OracleParameter();
            uName.ParameterName = "u_name";
            uName.OracleType = OracleType.NVarChar;
            uName.Value = user.Name;
            command.Parameters.Add(uName);
            OracleParameter uCity = new OracleParameter();
            uCity.ParameterName = "u_city";
            uCity.OracleType = OracleType.NVarChar;
            uCity.Value = user.City;
            command.Parameters.Add(uCity);
            OracleParameter uRegion = new OracleParameter();
            uRegion.ParameterName = "u_region";
            uRegion.OracleType = OracleType.NVarChar;
            uRegion.Value = user.Region;
            command.Parameters.Add(uRegion);
            OracleParameter uCountry = new OracleParameter();
            uCountry.ParameterName = "u_country";
            uCountry.OracleType = OracleType.NVarChar;
            uCountry.Value = user.Country;
            command.Parameters.Add(uCountry);
            OracleParameter uPhone = new OracleParameter();
            uPhone.ParameterName = "u_phone";
            uPhone.OracleType = OracleType.NVarChar;
            uPhone.Value = user.Phone;
            command.Parameters.Add(uPhone);
            OracleParameter uEmail = new OracleParameter();
            uEmail.ParameterName = "u_email";
            uEmail.OracleType = OracleType.NVarChar;
            uEmail.Value = user.Phone;
            command.Parameters.Add(uEmail);
            OracleParameter uLogin = new OracleParameter();
            uLogin.ParameterName = "u_login";
            uLogin.OracleType = OracleType.NVarChar;
            uLogin.Value = user.Login;
            command.Parameters.Add(uLogin);
            OracleParameter uPassword = new OracleParameter();
            uPassword.ParameterName = "u_password";
            uPassword.OracleType = OracleType.NVarChar;
            uPassword.Value = user.Password;
            command.Parameters.Add(uPassword);

            var reader = command.ExecuteReader();
            uName.Dispose();
            uCity.Dispose();
            uRegion.Dispose();
            uCountry.Dispose();
            uPhone.Dispose();
            uEmail.Dispose();
            uLogin.Dispose();
            uPassword.Dispose();
            command.Dispose();
            reader.Close();
            connection.Close();
        }
        public void Delete(int? id)
        {

            connection.Open();
            OracleCommand command = new OracleCommand("delete_product", connection);
            List<Product> products = new List<Product>();
            command.CommandType = System.Data.CommandType.StoredProcedure;
            OracleParameter emp_cur = new OracleParameter();
            emp_cur.ParameterName = "p_id";
            emp_cur.OracleType = OracleType.Number;
            emp_cur.Value = id;
            command.Parameters.Add(emp_cur);
            try
            {
                var reader = command.ExecuteReader();
                emp_cur.Dispose();
                command.Dispose();
                reader.Close();
                connection.Close();
            }
            catch (Exception ex)
            {
                emp_cur.Dispose();
                command.Dispose();
                connection.Close();
                Console.WriteLine(ex.Message);
            }
        }
        public void DeleteCat(int? id)
        {
            connection.Open();
            OracleCommand command = new OracleCommand("delete_category", connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            OracleParameter emp_cur = new OracleParameter();
            emp_cur.ParameterName = "c_id";
            emp_cur.OracleType = OracleType.Number;
            emp_cur.Value = id;
            command.Parameters.Add(emp_cur);
            var reader = command.ExecuteReader();
            emp_cur.Dispose();
            command.Dispose();
            reader.Close();
            connection.Close();
        }
        public void DeleteColore(int? id)
        {
            connection.Open();
            OracleCommand command = new OracleCommand("delete_colore", connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            OracleParameter emp_cur = new OracleParameter();
            emp_cur.ParameterName = "c_id";
            emp_cur.OracleType = OracleType.Number;
            emp_cur.Value = id;
            command.Parameters.Add(emp_cur);
            var reader = command.ExecuteReader();
            emp_cur.Dispose();
            command.Dispose();
            reader.Close();
            connection.Close();
        }
        public IQueryable<User> Users
        {

            get
            {
                connection.Open();
                OracleCommand command = new OracleCommand("get_users", connection);
                List<User> users = new List<User>();
                command.CommandType = System.Data.CommandType.StoredProcedure;
                OracleParameter emp_cur = new OracleParameter();
                emp_cur.ParameterName = "p_users";
                emp_cur.OracleType = OracleType.Cursor;
                emp_cur.Direction = ParameterDirection.Output;
                command.Parameters.Add(emp_cur);
                var reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        try
                        {
                            users.Add(new User()
                            {
                                Id = (int)(reader.GetOracleNumber(0)),
                                Name = !reader.IsDBNull(1) ? (string)(reader.GetOracleString(1)) : null,
                                City = !reader.IsDBNull(2) ? (string)(reader.GetOracleString(2)) : null,
                                Region = !reader.IsDBNull(3) ? (string)(reader.GetOracleString(3)) : null,
                                Country = !reader.IsDBNull(4) ? (string)(reader.GetOracleString(4)) : null,
                                Phone = !reader.IsDBNull(5) ? (string)(reader.GetOracleString(5)) : null,
                                Email = !reader.IsDBNull(6) ? (string)(reader.GetOracleString(6)) : null,
                                Login = !reader.IsDBNull(7) ? (string)(reader.GetOracleString(7)) : null,
                                Password = !reader.IsDBNull(8) ? (string)(reader.GetOracleString(8)) : null,
                                Roles = GetRoles((int)(reader.GetOracleNumber(0))).ToList()
                            });
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    }
                }
                emp_cur.Dispose();
                command.Dispose();
                reader.Close();

                connection.Close();
                return users.AsQueryable();
            }
        }
        public IQueryable<Role> Roles
        {

            get
            {
                connection.Open();
                OracleCommand command = new OracleCommand("get_roles_all", connection);
                List<Role> roles = new List<Role>();
                command.CommandType = System.Data.CommandType.StoredProcedure;
                OracleParameter emp_cur = new OracleParameter();
                emp_cur.ParameterName = "p_roles";
                emp_cur.OracleType = OracleType.Cursor;
                emp_cur.Direction = ParameterDirection.Output;
                command.Parameters.Add(emp_cur);
                var reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        try
                        {
                            roles.Add(new Role()
                            {
                                Id = (int)(reader.GetOracleNumber(0)),
                                RoleName = !reader.IsDBNull(1) ? (string)(reader.GetOracleString(1)) : null,
                            });
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    }
                }
                emp_cur.Dispose();
                command.Dispose();
                reader.Close();

                connection.Close();
                return roles.AsQueryable();
            }
        }
        public IQueryable<AllHistory> Histories
        {

            get
            {
                connection.Open();
                OracleCommand command = new OracleCommand("get_all_history", connection);
                List<AllHistory> allHistories = new List<AllHistory>();
                command.CommandType = System.Data.CommandType.StoredProcedure;
                OracleParameter emp_cur = new OracleParameter();
                emp_cur.ParameterName = "p_history";
                emp_cur.OracleType = OracleType.Cursor;
                emp_cur.Direction = ParameterDirection.Output;
                command.Parameters.Add(emp_cur);
                var reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        try
                        {
                            allHistories.Add(new AllHistory()
                            {
                                UserName = !reader.IsDBNull(0) ? (string)(reader.GetOracleString(0)) : null,
                                Name = !reader.IsDBNull(1) ? (string)(reader.GetOracleString(1)) : null,
                                Price = (decimal)(reader.GetOracleNumber(2)),
                                Quantity = (int)(reader.GetOracleNumber(3)),
                                dateTime = (DateTime)(reader.GetOracleDateTime(4)),
                                DeliveryAddress = !reader.IsDBNull(5) ? (string)(reader.GetOracleString(5)) : null
                            });
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    }
                }
                emp_cur.Dispose();
                command.Dispose();
                reader.Close();

                connection.Close();
                return allHistories.AsQueryable();
            }
        }

        public IQueryable<Role> GetRoles(int Id)
        {
            OracleCommand command = new OracleCommand("get_roles", connection);
            List<Role> roles = new List<Role>();
            command.CommandType = System.Data.CommandType.StoredProcedure;
            OracleParameter uId = new OracleParameter();
            uId.ParameterName = "id_user";
            uId.OracleType = OracleType.Number;
            uId.Value = Id;
            command.Parameters.Add(uId);
            OracleParameter emp_curs = new OracleParameter();
            emp_curs.ParameterName = "p_roles";
            emp_curs.OracleType = OracleType.Cursor;
            emp_curs.Direction = ParameterDirection.Output;
            command.Parameters.Add(emp_curs);
            var reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    try
                    {
                        roles.Add(new Role()
                        {
                            Id = (int)(reader.GetOracleNumber(0)),
                            RoleName = (string)(reader.GetOracleString(1))
                        });
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
            emp_curs.Dispose();
            command.Dispose();
            reader.Close();
            uId.Dispose();
            return roles.AsQueryable();
        }
        public IQueryable<Product> Products
        {

            get
            {
                connection.Open();
                OracleCommand command = new OracleCommand("get_products", connection);
                List<Product> products = new List<Product>();
                command.CommandType = System.Data.CommandType.StoredProcedure;
                OracleParameter emp_cur = new OracleParameter();
                emp_cur.ParameterName = "p_cursor";
                emp_cur.OracleType = OracleType.Cursor;
                emp_cur.Direction = ParameterDirection.Output;
                command.Parameters.Add(emp_cur);
                var reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        try
                        {
                            products.Add(new Product()
                            {
                                Id = (int)(reader.GetOracleNumber(0)),
                                Name = (string)(reader.GetOracleString(1)),
                                NameSezone = (string)(reader.GetOracleString(2)),
                                NameColore = (string)(reader.GetOracleString(3)),
                                NameMaterial = (string)(reader.GetOracleString(4)),
                                NameBrand = (string)(reader.GetOracleString(5)),
                                Category = (string)(reader.GetOracleString(6)),
                                Price = (decimal)(reader.GetOracleNumber(7)),
                                Description = (string)(reader.GetOracleString(8)),
                                ImageMimeType = !reader.IsDBNull(9) ? (string)(reader.GetOracleString(9)) : null,
                                ImageData = !reader.IsDBNull(10) ? (byte[])(reader.GetOracleBinary(10)) : null

                            });
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    }
                }
                emp_cur.Dispose();
                command.Dispose();
                reader.Close();

                connection.Close();
                return products.AsQueryable();
            }
        }
        public int Count()
        {
            string count = "select count(idproduct) from product";
            connection.Open();
            OracleCommand command = new OracleCommand();
            command.Connection = connection;
            command.CommandText = count;
            int c = Decimal.ToInt32((decimal)command.ExecuteScalar());
            command.Dispose();
            connection.Dispose();
            connection.Close();
            return c;
        }

        public int CountLastWeek()
        {
            connection.Open();
            OracleCommand command = new OracleCommand("added_last_week_count", connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            OracleParameter emp_cur = new OracleParameter();
            emp_cur.ParameterName = "p_product";
            emp_cur.OracleType = OracleType.Cursor;
            emp_cur.Direction = ParameterDirection.Output;
            command.Parameters.Add(emp_cur);
            var reader = command.ExecuteReader();
            int c = 0;
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    try
                    {
                        c = !reader.IsDBNull(0) ? (int)reader.GetOracleNumber(0) : 0;  
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
            emp_cur.Dispose();
            command.Dispose();
            reader.Close();

            connection.Close();
            return c;
        }
        public int Count(string category)
        {
            string count = "select count(idproduct) from product where idcategory=:cat";
            OracleParameter page1 = new OracleParameter();
            page1.ParameterName = "cat";
            page1.OracleType = OracleType.Number;
            page1.Value = Categories.FirstOrDefault(p=>p.Name==category).Id;
            connection.Open();
            OracleCommand command = new OracleCommand();
            command.Connection = connection;
            command.CommandText = count;
            command.Parameters.Add(page1);
            int c = Decimal.ToInt32((decimal)command.ExecuteScalar());
            command.Dispose();
            page1.Dispose();
            connection.Dispose();
            connection.Close();
            return c;
        }

        public IQueryable<LastProductViewModel> LastProduct()
        {
            string select = "select idproduct,nameproduct,brand.namebrand,price,product_date from product_log " +
                "inner join product on product.idproduct = product_log.id_product " +
                "inner join brand on brand.idbrand = product.idbrand " +
                "order by  product_log.product_date desc FETCH FIRST 10 ROWS ONLY";
            connection.Open();
            OracleCommand command = new OracleCommand();
            command.Connection = connection;
            command.CommandText = select;
            List<LastProductViewModel> lasts = new List<LastProductViewModel>();
            var reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    try
                    {
                        lasts.Add(new LastProductViewModel()
                        {
                            Id = (int)(reader.GetOracleNumber(0)),
                            NameProduct = (string)(reader.GetOracleString(1)),
                            NameBrand = (string)(reader.GetOracleString(2)),
                            Price = (decimal)(reader.GetOracleNumber(3)),
                            DateRegister = (DateTime)(reader.GetOracleDateTime(4))
                        });
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }

            }
            command.Dispose();
            connection.Dispose();
            connection.Close();
            return lasts.AsQueryable();
        }

        public Product FindProduct(int id)
        {

            string select = "select product.idproduct,product.nameproduct,sezone.namesezone, colore.namecolore,material.namematerial, brand.namebrand,category.namecategory, product.price,product.description,product.imagemimetype,product.imagedata" +
                 " from product " +
                 " inner join brand on brand.idbrand = product.idbrand " +
                 "inner join colore on colore.idcolore = product.idcolore " +
                 "inner join material on material.idmaterial = product.idmaterial " +
                 "inner join sezone on sezone.idsezone = product.idsezone " +
                 "inner join category on category.idcategory = product.idcategory " +
                 "where  idproduct=:id";

            OracleParameter ProdId = new OracleParameter();
            ProdId.ParameterName = "id";
            ProdId.OracleType = OracleType.Number;
            ProdId.Value = id;
            connection.Open();
            OracleCommand command = new OracleCommand();
            command.Connection = connection;
            command.CommandText = select;
            command.Parameters.Add(ProdId);
            Product product = new Product();
            var reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    try
                    {
                        product = new Product()
                        {
                            Id = (int)(reader.GetOracleNumber(0)),
                            Name = (string)(reader.GetOracleString(1)),
                            NameSezone = (string)(reader.GetOracleString(2)),
                            NameColore = (string)(reader.GetOracleString(3)),
                            NameMaterial = (string)(reader.GetOracleString(4)),
                            NameBrand = (string)(reader.GetOracleString(5)),
                            Category = (string)(reader.GetOracleString(6)),
                            Price = (decimal)(reader.GetOracleNumber(7)),
                            Description = (string)(reader.GetOracleString(8)),
                            ImageMimeType = !reader.IsDBNull(9) ? (string)(reader.GetOracleString(9)) : null,
                            ImageData = !reader.IsDBNull(10) ? (byte[])(reader.GetOracleBinary(10)) : null
                        };
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
            ProdId.Dispose();
            command.Dispose();
            reader.Close();

            connection.Close();
            return product;
        }
        public IQueryable<Product> FindProductByName(string name)
        {

            connection.Open();
            OracleCommand command = new OracleCommand("get_product_by_name", connection);
            List<Product> products = new List<Product>();
            command.CommandType = System.Data.CommandType.StoredProcedure;
            OracleParameter emp_cur = new OracleParameter();
            emp_cur.ParameterName = "p_prod";
            emp_cur.OracleType = OracleType.Cursor;
            emp_cur.Direction = ParameterDirection.Output;
            command.Parameters.Add(emp_cur);
            OracleParameter ProdName = new OracleParameter();
            ProdName.ParameterName = "name";
            ProdName.OracleType = OracleType.NVarChar;
            ProdName.Value = name;
            command.Parameters.Add(ProdName);
            var reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    try
                    {
                        products.Add(
                        new Product()
                        {
                            Id = (int)(reader.GetOracleNumber(0)),
                            Name = (string)(reader.GetOracleString(1)),
                            NameSezone = (string)(reader.GetOracleString(2)),
                            NameColore = (string)(reader.GetOracleString(3)),
                            NameMaterial = (string)(reader.GetOracleString(4)),
                            NameBrand = (string)(reader.GetOracleString(5)),
                            Category = (string)(reader.GetOracleString(6)),
                            Price = (decimal)(reader.GetOracleNumber(7)),
                            Description = (string)(reader.GetOracleString(8)),
                            ImageMimeType = !reader.IsDBNull(9) ? (string)(reader.GetOracleString(9)) : null,
                            ImageData = !reader.IsDBNull(10) ? (byte[])(reader.GetOracleBinary(10)) : null
                        });
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
            ProdName.Dispose();
            command.Dispose();
            reader.Close();

            connection.Close();
            return products.AsQueryable();
        }
        public IQueryable<Product> Offset(int page, int count)
        {

            string select = "select product.idproduct,product.nameproduct,sezone.namesezone, colore.namecolore,material.namematerial, brand.namebrand,category.namecategory, product.price,product.description,product.imagemimetype,product.imagedata" +
                 " from product " +
                 " inner join brand on brand.idbrand = product.idbrand " +
                 "inner join colore on colore.idcolore = product.idcolore " +
                 "inner join material on material.idmaterial = product.idmaterial " +
                 "inner join sezone on sezone.idsezone = product.idsezone " +
                 "inner join category on category.idcategory = product.idcategory " +
                 "order by idproduct OFFSET :p_page ROWS FETCH NEXT :p_count ROWS ONLY";

            List<Product> products = new List<Product>();
            OracleParameter page1 = new OracleParameter();
            page1.ParameterName = "p_page";
            page1.OracleType = OracleType.Number;
            page1.Value = page;
            OracleParameter count1 = new OracleParameter();
            count1.ParameterName = "p_count";
            count1.OracleType = OracleType.Number;
            count1.Value = count;
            connection.Open();
            OracleCommand command = new OracleCommand();
            command.Connection = connection;
            command.CommandText = select;
            command.Parameters.Add(page1);
            command.Parameters.Add(count1);
            var reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    try
                    {
                        products.Add(new Product()
                        {
                            Id = (int)(reader.GetOracleNumber(0)),
                            Name = (string)(reader.GetOracleString(1)),
                            NameSezone = (string)(reader.GetOracleString(2)),
                            NameColore = (string)(reader.GetOracleString(3)),
                            NameMaterial = (string)(reader.GetOracleString(4)),
                            NameBrand = (string)(reader.GetOracleString(5)),
                            Category = (string)(reader.GetOracleString(6)),
                            Price = (decimal)(reader.GetOracleNumber(7)),
                            Description = (string)(reader.GetOracleString(8)),
                            ImageMimeType = !reader.IsDBNull(9) ? (string)(reader.GetOracleString(9)) : null,
                            ImageData = !reader.IsDBNull(10) ? (byte[])(reader.GetOracleBinary(10)) : null

                        });
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
            page1.Dispose();
            count1.Dispose();
            command.Dispose();
            reader.Close();

            connection.Close();
            return products.AsQueryable();
        }

        public IQueryable<Product> OffsetCategory(int page, int count,string category)
        {

            int cat = Categories.FirstOrDefault(c => c.Name == category).Id;
            string select = "select product.idproduct,product.nameproduct,sezone.namesezone, colore.namecolore,material.namematerial, brand.namebrand,category.namecategory, product.price,product.description,product.imagemimetype,product.imagedata" +
                 " from product " +
                 " inner join brand on brand.idbrand = product.idbrand " +
                 "inner join colore on colore.idcolore = product.idcolore " +
                 "inner join material on material.idmaterial = product.idmaterial " +
                 "inner join sezone on sezone.idsezone = product.idsezone " +
                 "inner join category on category.idcategory = product.idcategory and product.idcategory = " +cat+
                 "order by idproduct OFFSET :p_page ROWS FETCH NEXT :p_count ROWS ONLY";

            List<Product> products = new List<Product>();
          
            OracleParameter page1 = new OracleParameter();
            page1.ParameterName = "p_page";
            page1.OracleType = OracleType.Number;
            page1.Value = page;
            OracleParameter count1 = new OracleParameter();
            count1.ParameterName = "p_count";
            count1.OracleType = OracleType.Number;
            count1.Value = count;
            connection.Open();
            OracleCommand command = new OracleCommand();
            command.Connection = connection;
            command.CommandText = select;
            command.Parameters.Add(page1);
            command.Parameters.Add(count1);
            var reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    try
                    {
                        products.Add(new Product()
                        {
                            Id = (int)(reader.GetOracleNumber(0)),
                            Name = (string)(reader.GetOracleString(1)),
                            NameSezone = (string)(reader.GetOracleString(2)),
                            NameColore = (string)(reader.GetOracleString(3)),
                            NameMaterial = (string)(reader.GetOracleString(4)),
                            NameBrand = (string)(reader.GetOracleString(5)),
                            Category = (string)(reader.GetOracleString(6)),
                            Price = (decimal)(reader.GetOracleNumber(7)),
                            Description = (string)(reader.GetOracleString(8)),
                            ImageMimeType = !reader.IsDBNull(9) ? (string)(reader.GetOracleString(9)) : null,
                            ImageData = !reader.IsDBNull(10) ? (byte[])(reader.GetOracleBinary(10)) : null

                        });
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
            page1.Dispose();
            count1.Dispose();
            command.Dispose();
            reader.Close();

            connection.Close();
            return products.AsQueryable();
        }
        public void Add(Product product)
        {
            string insert = "insert into product(idproduct, nameproduct,idsezone,idcolore,idmaterial,idbrand,idcategory,price,description,imagedata,imagemimetype)" +
               "values(seq_prod.nextval, :p_name,:p_sezone,:p_colore, :p_material, :p_brand," +
               " :p_category,:p_price,:p_desc,:p_data,:p_mime)";
            OracleParameter pName = new OracleParameter();
            pName.ParameterName = "p_name";
            pName.Value = product.Name;
            pName.OracleType = OracleType.NVarChar;
            OracleParameter pSezone = new OracleParameter();
            pSezone.ParameterName = "p_sezone";
            pSezone.OracleType = OracleType.Number;
            pSezone.Value = Sezones.FirstOrDefault(s => s.Name == product.NameSezone).Id;
            OracleParameter pColore = new OracleParameter();
            pColore.ParameterName = "p_colore";
            pColore.OracleType = OracleType.Number;
            pColore.Value = Colors.FirstOrDefault(s => s.NameColore == product.NameColore).Id;
            OracleParameter pMaterial = new OracleParameter();
            pMaterial.ParameterName = "p_material";
            pMaterial.OracleType = OracleType.Number;
            pMaterial.Value = Materials.FirstOrDefault(s => s.Name == product.NameMaterial).Id;
            OracleParameter pBrand = new OracleParameter();
            pBrand.ParameterName = "p_brand";
            pBrand.OracleType = OracleType.Number;
            pBrand.Value = Brands.FirstOrDefault(s => s.Name == product.NameBrand).Id;
            OracleParameter pCategory = new OracleParameter();
            pCategory.ParameterName = "p_category";
            pCategory.OracleType = OracleType.Number;
            pCategory.Value = Categories.FirstOrDefault(s => s.Name == product.Category).Id;
            OracleParameter pPrice = new OracleParameter();
            pPrice.ParameterName = "p_price";
            pPrice.OracleType = OracleType.Number;
            pPrice.Value = product.Price;
            OracleParameter pDesc = new OracleParameter();
            pDesc.ParameterName = "p_desc";
            pDesc.OracleType = OracleType.NVarChar;
            pDesc.Value = product.Description;
            OracleParameter pData = new OracleParameter();
            pData.ParameterName = "p_data";
            pData.OracleType = OracleType.Blob;
            pData.Value = product.ImageData;
            OracleParameter pType = new OracleParameter();
            pType.ParameterName = "p_mime";
            pType.OracleType = OracleType.NVarChar;
            pType.Value = product.ImageMimeType;
            connection.Open();
            OracleCommand command = new OracleCommand();
            command.Connection = connection;
            command.CommandText = insert;
            command.Parameters.Add(pName);
            command.Parameters.Add(pSezone);
            command.Parameters.Add(pColore);
            command.Parameters.Add(pMaterial);
            command.Parameters.Add(pBrand);
            command.Parameters.Add(pCategory);
            command.Parameters.Add(pPrice);
            command.Parameters.Add(pDesc);
            command.Parameters.Add(pData);
            command.Parameters.Add(pType);



            var reader = command.ExecuteNonQuery();
            command.Dispose();
            pName.Dispose();
            pSezone.Dispose();
            pColore.Dispose();
            pMaterial.Dispose();
            pBrand.Dispose();
            pCategory.Dispose();
            pPrice.Dispose();
            pDesc.Dispose();
            pData.Dispose();
            pType.Dispose();
            connection.Close();
        }
        public void UpdateCategory(Category category)
        {
            connection.Open();
            OracleCommand command = new OracleCommand("update_category", connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            OracleParameter pId = new OracleParameter();
            pId.ParameterName = "c_id";
            pId.Value = category.Id;
            pId.OracleType = OracleType.Number;
            command.Parameters.Add(pId);
            OracleParameter pName = new OracleParameter();
            pName.ParameterName = "c_name";
            pName.Value = category.Name;
            pName.OracleType = OracleType.NVarChar;
            command.Parameters.Add(pName);
            command.ExecuteNonQuery();
            pId.Dispose();
            pName.Dispose();
            connection.Close();
        }
        public void UpdateColore(Colore colore)
        {
            connection.Open();
            OracleCommand command = new OracleCommand("update_colore", connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            OracleParameter pId = new OracleParameter();
            pId.ParameterName = "c_id";
            pId.Value = colore.Id;
            pId.OracleType = OracleType.Number;
            command.Parameters.Add(pId);
            OracleParameter pName = new OracleParameter();
            pName.ParameterName = "c_name";
            pName.Value = colore.NameColore;
            pName.OracleType = OracleType.NVarChar;
            command.Parameters.Add(pName);
            command.ExecuteNonQuery();
            pId.Dispose();
            pName.Dispose();
            connection.Close();
        }
        public void UpdateProduct(Product product)
        {
           

            string sqlupdate1 = "update product set " +
                 "nameproduct=:name, " +
                 "idsezone=:idsez, " +
                 "idcolore=:idcol," +
                 "idmaterial=:idmat, " +
                 "idbrand=:idbrand," +
                 "idcategory=:idcat," +
                 "price=:pr, " +
                 "description=:descr, " +
                 "imagedata = :parm, " +
                 "imagemimetype=:type " +
                     "where idproduct = :id";

            OracleParameter blobParameter = new OracleParameter();
            blobParameter.OracleType = OracleType.Blob;
            blobParameter.ParameterName = "parm";
            blobParameter.Value = product.ImageData;
            OracleParameter blobParameter1 = new OracleParameter();
            blobParameter1.OracleType = OracleType.Number;
            blobParameter1.ParameterName = "idsez";
            blobParameter1.Value = Sezones.FirstOrDefault(s => s.Name == product.NameSezone).Id;
            OracleParameter blobParameter2 = new OracleParameter();
            blobParameter2.OracleType = OracleType.Number;
            blobParameter2.ParameterName = "id";
            blobParameter2.Value = product.Id;
            OracleParameter blobParameter3 = new OracleParameter();
            blobParameter3.OracleType = OracleType.NVarChar;
            blobParameter3.ParameterName = "name";
            blobParameter3.Value = product.Name;
            OracleParameter blobParameter4 = new OracleParameter();
            blobParameter4.OracleType = OracleType.Number;
            blobParameter4.ParameterName = "idcol";
            blobParameter4.Value = Colors.FirstOrDefault(s => s.NameColore == product.NameColore).Id;
            OracleParameter blobParameter5 = new OracleParameter();
            blobParameter5.OracleType = OracleType.Number;
            blobParameter5.ParameterName = "idmat";
            blobParameter5.Value = Materials.FirstOrDefault(s => s.Name == product.NameMaterial).Id;
            OracleParameter blobParameter6 = new OracleParameter();
            blobParameter6.OracleType = OracleType.Number;
            blobParameter6.ParameterName = "idbrand";
            blobParameter6.Value = Brands.FirstOrDefault(s => s.Name == product.NameBrand).Id;
            OracleParameter blobParameter7 = new OracleParameter();
            blobParameter7.OracleType = OracleType.Number;
            blobParameter7.ParameterName = "idcat";
            blobParameter7.Value = Categories.FirstOrDefault(s => s.Name == product.Category).Id;
            OracleParameter blobParameter8 = new OracleParameter();
            blobParameter8.OracleType = OracleType.Number;
            blobParameter8.ParameterName = "pr";
            blobParameter8.Value = product.Price;
            OracleParameter blobParameter10 = new OracleParameter();
            blobParameter10.OracleType = OracleType.NVarChar;
            blobParameter10.ParameterName = "type";
            blobParameter10.Value = product.ImageMimeType;
            OracleParameter blobParameter11 = new OracleParameter();
            blobParameter11.OracleType = OracleType.NVarChar;
            blobParameter11.ParameterName = "descr";
            blobParameter11.Value = product.Description;
            connection.Open();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = connection;
            cmd.CommandText = sqlupdate1;
            cmd.Parameters.Add(blobParameter);
            cmd.Parameters.Add(blobParameter1);
            cmd.Parameters.Add(blobParameter2);
            cmd.Parameters.Add(blobParameter3);
            cmd.Parameters.Add(blobParameter4);
            cmd.Parameters.Add(blobParameter5);
            cmd.Parameters.Add(blobParameter6);
            cmd.Parameters.Add(blobParameter7);
            cmd.Parameters.Add(blobParameter8);
            cmd.Parameters.Add(blobParameter10);
            cmd.Parameters.Add(blobParameter11);
            cmd.ExecuteNonQuery();
            connection.Dispose();
            cmd.Dispose();
            blobParameter.Dispose();
            blobParameter1.Dispose();
            blobParameter2.Dispose();
            blobParameter3.Dispose();
            blobParameter4.Dispose();
            blobParameter5.Dispose();
            blobParameter6.Dispose();
            blobParameter7.Dispose();
            blobParameter8.Dispose();
            blobParameter10.Dispose();
            blobParameter11.Dispose();

            connection.Close();


      
        }
        public void InsertOrder(Cart cart, string Name, string address)
        {
            foreach (var cartLine in cart.Lines)
            {
                connection.Open();
                OracleCommand command = new OracleCommand("insert_order", connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                OracleParameter pId = new OracleParameter();
                pId.ParameterName = "rel_o_idproduct";
                pId.Value = cartLine.Product.Id;
                pId.OracleType = OracleType.Number;
                command.Parameters.Add(pId);
                OracleParameter quantity = new OracleParameter();
                quantity.ParameterName = "rel_o_quontity";
                quantity.OracleType = OracleType.NVarChar;
                quantity.Value = cartLine.Quantity;
                command.Parameters.Add(quantity);
                OracleParameter pName = new OracleParameter();
                pName.ParameterName = "rel_o_name";
                pName.OracleType = OracleType.NVarChar;
                pName.Value = Name;
                command.Parameters.Add(pName);
                OracleParameter pAddress = new OracleParameter();
                pAddress.ParameterName = "rel_o_address";
                pAddress.OracleType = OracleType.NVarChar;
                pAddress.Value = address;
                command.Parameters.Add(pAddress);
                var reader = command.ExecuteNonQuery();
                command.Dispose();
                pName.Dispose();
                pId.Dispose();
                pAddress.Dispose();
                quantity.Dispose();
                connection.Close();
            }

        }
        public IQueryable<Colore> Colors
        {

            get
            {
                connection.Open();
                OracleCommand command = new OracleCommand("get_colors", connection);
                List<Colore> colores = new List<Colore>();
                command.CommandType = System.Data.CommandType.StoredProcedure;
                OracleParameter emp_cur = new OracleParameter();
                emp_cur.ParameterName = "p_colore";
                emp_cur.OracleType = OracleType.Cursor;
                emp_cur.Direction = ParameterDirection.Output;
                command.Parameters.Add(emp_cur);
                var reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        colores.Add(new Colore()
                        {
                            Id = (int)(reader.GetOracleNumber(0)),
                            NameColore = (string)(reader.GetOracleString(1))
                        });
                    }
                }
                emp_cur.Dispose();
                command.Dispose();
                reader.Close();
                connection.Close();
                return colores.AsQueryable();
            }
        }
        public IQueryable<Material> Materials
        {

            get
            {
                connection.Open();
                OracleCommand command = new OracleCommand("get_materials", connection);
                List<Material> materials = new List<Material>();
                command.CommandType = System.Data.CommandType.StoredProcedure;
                OracleParameter emp_cur = new OracleParameter();
                emp_cur.ParameterName = "p_material";
                emp_cur.OracleType = OracleType.Cursor;
                emp_cur.Direction = ParameterDirection.Output;
                command.Parameters.Add(emp_cur);
                var reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        materials.Add(new Material()
                        {
                            Id = (int)(reader.GetOracleNumber(0)),
                            Name = (string)(reader.GetOracleString(1))
                        });
                    }
                }
                emp_cur.Dispose();
                command.Dispose();
                reader.Close();
                connection.Close();
                return materials.AsQueryable();
            }
        }
        public IQueryable<Sezone> Sezones
        {

            get
            {
                connection.Open();
                OracleCommand command = new OracleCommand("get_sezones", connection);
                List<Sezone> sezones = new List<Sezone>();
                command.CommandType = System.Data.CommandType.StoredProcedure;
                OracleParameter emp_cur = new OracleParameter();
                emp_cur.ParameterName = "p_sezone";
                emp_cur.OracleType = OracleType.Cursor;
                emp_cur.Direction = ParameterDirection.Output;
                command.Parameters.Add(emp_cur);
                var reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        sezones.Add(new Sezone()
                        {
                            Id = (int)(reader.GetOracleNumber(0)),
                            Name = (string)(reader.GetOracleString(1))
                        });
                    }
                }
                emp_cur.Dispose();
                command.Dispose();
                reader.Close();
                connection.Close();
                return sezones.AsQueryable();
            }
        }
        public IQueryable<Brand> Brands
        {

            get
            {
                connection.Open();
                OracleCommand command = new OracleCommand("get_brands", connection);
                List<Brand> brands = new List<Brand>();
                command.CommandType = System.Data.CommandType.StoredProcedure;
                OracleParameter emp_cur = new OracleParameter();
                emp_cur.ParameterName = "p_brand";
                emp_cur.OracleType = OracleType.Cursor;
                emp_cur.Direction = ParameterDirection.Output;
                command.Parameters.Add(emp_cur);
                var reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        brands.Add(new Brand()
                        {
                            Id = (int)(reader.GetOracleNumber(0)),
                            Name = (string)(reader.GetOracleString(1))
                            //Ider = (int)(reader.GetOracleNumber(2))
                        });
                    }
                }
                emp_cur.Dispose();
                command.Dispose();
                reader.Close();
                connection.Close();
                return brands.AsQueryable();
            }
        }
        public IQueryable<Category> Categories
        {

            get
            {
                connection.Open();
                OracleCommand command = new OracleCommand("get_categories", connection);
                List<Category> categories = new List<Category>();
                command.CommandType = System.Data.CommandType.StoredProcedure;
                OracleParameter emp_cur = new OracleParameter();
                emp_cur.ParameterName = "p_category";
                emp_cur.OracleType = OracleType.Cursor;
                emp_cur.Direction = ParameterDirection.Output;
                command.Parameters.Add(emp_cur);
                var reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        categories.Add(new Category()
                        {
                            Id = (int)(reader.GetOracleNumber(0)),
                            Name = (string)(reader.GetOracleString(1)),
                        });
                    }
                }
                emp_cur.Dispose();
                command.Dispose();
                reader.Close();
                connection.Close();
                return categories.AsQueryable();
            }
        }

        //public IQueryable<Category> Categories
        //{
        //    get
        //    {
        //        OracleCommand command = new OracleCommand("get_categorys", connection);
        //        List<Category> categories = new List<Category>();
        //        command.CommandType = System.Data.CommandType.StoredProcedure;
        //        OracleParameter emp_cur = new OracleParameter();
        //        emp_cur.ParameterName = "p_cursor_category";
        //        emp_cur.OracleType = OracleType.Cursor;
        //        emp_cur.Direction = ParameterDirection.Output;
        //        command.Parameters.Add(emp_cur);
        //        var reader = command.ExecuteReader();
        //        if (reader.HasRows)
        //        {
        //            while (reader.Read())
        //            {
        //                categories.Add(new Category()
        //                {
        //                    Id = (int)(reader.GetOracleNumber(0)),
        //                    Name = (string)reader.GetOracleString(1)
        //                });
        //            }
        //        }
        //        return categories.AsQueryable();
        //    }
        //}

        public IQueryable<History> GetHistory(string Name)
        {
            connection.Open();
            OracleCommand command = new OracleCommand("get_history", connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            OracleParameter pIuUser = new OracleParameter();
            pIuUser.ParameterName = "nameuser";
            pIuUser.Value = Name;
            pIuUser.OracleType = OracleType.NVarChar;
            command.Parameters.Add(pIuUser);
            OracleParameter emp_cur = new OracleParameter();
            emp_cur.ParameterName = "p_history";
            emp_cur.OracleType = OracleType.Cursor;
            emp_cur.Direction = ParameterDirection.Output;
            command.Parameters.Add(emp_cur);
            var reader = command.ExecuteReader();
            List<History> histories = new List<History>();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    try
                    {
                        histories.Add(
                            new History()
                            {
                                Name = !reader.IsDBNull(0) ? (string)(reader.GetOracleString(0)) : null,
                                Quantity = (int)(reader.GetOracleNumber(2)),
                                Price = (decimal)(reader.GetOracleNumber(1)),
                                dateTime = (DateTime)(reader.GetDateTime(3)),
                                DeliveryAddress = !reader.IsDBNull(4) ? (string)(reader.GetOracleString(4)) : null
                            });
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
            pIuUser.Dispose();
            emp_cur.Dispose();
            command.Dispose();
            reader.Close();
            connection.Close();
            return histories.AsQueryable();
        }
    }
}