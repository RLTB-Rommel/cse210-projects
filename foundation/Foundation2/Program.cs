    using System;
    using System.Collections.Generic;
using System.Runtime.InteropServices;

class Address
    {
        private string street;
        private string city;
        private string state;
        private string country;

        //constructor to initialize address fields

        public Address(string street, string city, string state, string country)
        {
            this.street = street;
            this.city = city;
            this.state = state;
            this.country = country;
        }
        //method to check if the address is in the USA
        public bool IsInUSA()
        {
            return country.Equals("USA", StringComparison.OrdinalIgnoreCase);
        }

        public override string ToString()
        {
            return $"{street}\n{city}, {state}\n{country}";
        }
    }

    class Customer
    {
        private string name;
        private Address address;
        public Customer(string name, Address address)
        {
            this.name = name;
            this.address = address;
        }

        public bool IsInUSA()
        {
            return address.IsInUSA();
        }

        public string GetName()
        {
            return name;
        }

        public string GetAddress()
        {
            return address.ToString();
        }
    }

    class Product
    {
        private string name;
        private string productId;
        private double price;
        private int quantity;

        //constructor to initialize product fields

        public Product(string name, string productId, double price, int quantity)
        {
            this.name = name;
            this.productId = productId;
            this.price = price;
            this.quantity = quantity;
        }

        //method to calculate total cost of the product
        public double TotalCost()
        {
            return price*quantity;
        }
        //getter method for product's name
        public string GetName()
        {
            return name;
        }
        //getter method for product ID
        public string GetProductId()
        {
            return productId;
        }

    }
    
    //class representing an order
    class Order
    {
        
        private Customer customer;
        private List<Product> products;
            
        public Order(Customer customer)
        {
            this.customer = customer;
            this.products = new List<Product>();
        }

        public void AddProduct (Product product)
        {
            products.Add(product);
        }

        public double TotalCost()

        {
            double total = 0;
            foreach ( var product in products)
            {
                total += product.TotalCost();
            }

            double shippingCost = customer.IsInUSA()? 5.0 : 35.0;
            return total + shippingCost;

        }

        public string PackingLabel()
        {
            string labels = "Packing Label:\n";
            foreach(var product in products)
            {
                    labels += $"{product.GetName()}(ID: {product.GetProductId()})\n";
            }

            return labels;
        }

        public string ShippingLabel()
    {
        return $"Shipping Label:\n{customer.GetName()}\n{customer.GetAddress()}";
    }
    //Main program class
    class Program
    {
        static void Main(string[] args)
        {

            //Creating address instances

            Address address1 = new Address("123 Elm St","Springfield","IL","USA");
            Address address2 = new Address("456 Oak St","Toronto","ON","Canada");

            //Creating customer instances

            Customer customer1 = new Customer("Juan Dela Cruz", address1);
            Customer customer2 = new Customer("Joel Legazpi", address2);

            //creating product instances

            Product product1 = new Product("Widget", "W123", 10.00, 3);
            Product product2 = new Product("Gadget ", "G456", 15.50, 2);
            Product product3 = new Product("Pillow ", "T879", 5.75, 5);

            //creating  order instances and adding products

            Order order1 = new Order(customer1);
            order1.AddProduct(product1);
            order1.AddProduct(product2);

            Order order2 = new Order(customer2);
            order2.AddProduct(product3);

            // displaying packing labels, shipping labels, and total costs for each order

            foreach(var order in new [] {order1, order2})
            {
                Console.WriteLine(order.PackingLabel());
                Console.WriteLine(order.ShippingLabel());
                Console.WriteLine($"Total Cost: ${order.TotalCost():F2}\n");
            }
        }
    }
}
