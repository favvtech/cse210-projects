using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        // Create addresses
        Address address1 = new Address("123 Main St", "New York", "NY", "USA");
        Address address2 = new Address("456 Oak Ave", "Toronto", "Ontario", "Canada");

        // Create customers
        Customer customer1 = new Customer("John Smith", address1);
        Customer customer2 = new Customer("Maria Garcia", address2);

        // Create products for Order 1
        Product product1 = new Product("Laptop", "LAP001", 999.99, 1);
        Product product2 = new Product("Mouse", "MOU001", 25.50, 2);
        Product product3 = new Product("Keyboard", "KEY001", 75.00, 1);

        // Create products for Order 2
        Product product4 = new Product("Monitor", "MON001", 299.99, 1);
        Product product5 = new Product("Webcam", "WEB001", 89.99, 1);
        Product product6 = new Product("Headphones", "HEA001", 149.99, 2);

        // Create orders
        Order order1 = new Order(customer1);
        order1.AddProduct(product1);
        order1.AddProduct(product2);
        order1.AddProduct(product3);

        Order order2 = new Order(customer2);
        order2.AddProduct(product4);
        order2.AddProduct(product5);
        order2.AddProduct(product6);

        // Display Order 1 information
        Console.WriteLine("=== ORDER 1 ===");
        Console.WriteLine(order1.GetPackingLabel());
        Console.WriteLine(order1.GetShippingLabel());
        Console.WriteLine($"Total Cost: ${order1.CalculateTotalCost():F2}");
        Console.WriteLine();

        // Display Order 2 information
        Console.WriteLine("=== ORDER 2 ===");
        Console.WriteLine(order2.GetPackingLabel());
        Console.WriteLine(order2.GetShippingLabel());
        Console.WriteLine($"Total Cost: ${order2.CalculateTotalCost():F2}");
        Console.WriteLine();
    }
}