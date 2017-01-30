using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinQ
{
    class Products
    {
        public int ProductID;
        public string ProductName;
        public int Price;
        public int unitsInStock;
        public Products(int ID, string Name, int Price, int Units)
        {
            ProductID = ID;
            ProductName = Name;
            this.Price = Price;
            unitsInStock = Units;
        }

    }

    class Customers
    {

        public int ID;
        public string Name;
        public Customers(int ID, string Name)
        {
            this.ID = ID;
            this.Name = Name;
        }
    }
    class Orders
    {

        public int ID;
        public DateTime DateOrdered;
        public Products ProductName;
        public Customers customer;
        public Orders(int ID, DateTime orderDate, Products productName, Customers CustomerName)
        {
            this.ID = ID;
            DateOrdered = orderDate;
            ProductName = productName;
            customer = CustomerName;
        }
    }

    class LinQAssignment
    {
        static void Main(string[] args)
        {

            List<Products> products = new List<Products>(5);
            List<Customers> customers = new List<Customers>(5);
            List<Orders> orders = new List<LinQ.Orders>(10);

            products.Add(new Products(2, "Tablet", 300, 25));
            products.Add(new Products(3, "Spray", 50, 30));
            products.Add(new Products(4, "Laptop", 500, 45));
            products.Add(new Products(5, "Dongle", 20, 60));
            products.Add(new Products(1, "Mobile", 200, 15));

            customers.Add(new Customers(1, "Harsha"));
            customers.Add(new Customers(2, "ShivP"));
            customers.Add(new Customers(3, "ShivK"));
            customers.Add(new Customers(5, "Kiana"));
            customers.Add(new Customers(4, "Aditi"));

            orders.Add(new Orders(1, Convert.ToDateTime("1/1/2017"), products[1], customers[1]));
            orders.Add(new Orders(2, Convert.ToDateTime("1/2/2016"), products[2], customers[2]));
            orders.Add(new Orders(3, Convert.ToDateTime("1/3/2016"), products[0], customers[3]));
            orders.Add(new Orders(4, Convert.ToDateTime("1/4/2016"), products[3], customers[4]));
            orders.Add(new Orders(5, Convert.ToDateTime("1/5/2016"), products[4], customers[0]));
            orders.Add(new Orders(6, Convert.ToDateTime("1/6/2016"), products[4], customers[4]));
            orders.Add(new Orders(7, Convert.ToDateTime("1/7/2016"), products[2], customers[3]));
            orders.Add(new Orders(8, Convert.ToDateTime("1/8/2017"), products[4], customers[4]));
            orders.Add(new Orders(9, Convert.ToDateTime("1/9/2017"), products[0], customers[2]));
            orders.Add(new Orders(10, Convert.ToDateTime("1/10/2016"), products[3], customers[1]));




            var ProductinStock = from product in products
                                 where product.unitsInStock > 0 && product.Price > 100
                                 select product.ProductName;
            Console.WriteLine("Products with cost greater than 100 and in stock: ");
            foreach (var expensiveProducts in ProductinStock)
            {
                Console.WriteLine(expensiveProducts);
            }


            var Order = from order in orders
                        group order by order.customer.ID into CustomerGroup
                        select new
                        {
                            Order = CustomerGroup.Sum(o => o.ProductName.Price),
                            key = CustomerGroup.Key
                        };

            Console.WriteLine("Heighest to lowest value spent by the 5 customers:");
            foreach (var ordered in Order)
                Console.WriteLine(ordered);





            DateTime date = DateTime.Now;

            var customerName = from order in orders
                               where DateTime.Compare(order.DateOrdered, date.AddMonths(-1)) > 0
                               select order.customer.Name;


            Console.WriteLine("Customer who bought a product in the last month:");
            foreach (var Name in customerName)
            {
                Console.WriteLine(Name);
            }


            var ProductName = from order in orders
                              group order by order.ProductName.ProductID into OrderGroup
                              select new
                              {
                                  value = OrderGroup.Count(),
                                  key = OrderGroup.Key
                              };

            foreach (var Name in ProductName)
                Console.WriteLine(Name);




            Console.ReadKey();

        }

    }



}




