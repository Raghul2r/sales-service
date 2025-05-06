using SalesAndService.Models;
using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.EntityFrameworkCore;

namespace SalesAndService.Services
{
    public class CsvLoader
    {
        private readonly ApplicationDbContext _context;

        public CsvLoader(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task LoadDataAsync(string filePath)
        {
            using var reader = new StreamReader(filePath);
            using var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture));
            var records = csv.GetRecords<dynamic>().ToList();

            foreach (var record in records)
            {
                string prodId = record["Product ID"];
                string custId = record["Customer ID"];
                string orderId = record["Order ID"];

                var product = await _context.Products.FirstOrDefaultAsync(p => p.ProductId == prodId);
                if (product == null) 
                {
                    product = new Product
                    {
                        ProductId = prodId,
                        ProductName = record["Product Name"],
                        Category = record["Category"]
                    };
                    _context.Products.Add(product); 
                }

                var customer = await _context.Customers.FirstOrDefaultAsync(c => c.CustomerId == custId);
                if (customer == null)
                {
                    customer = new Customer
                    {
                        CustomerId = custId,
                        Name = record["Customer Name"],
                        Email = record["Customer Email"],
                        Address = record["Customer Address"]
                    };
                    _context.Customers.Add(customer); 
                }

                if (await _context.Orders.AnyAsync(o => o.OrderId == orderId))
                    continue;

                var order = new Order
                {
                    OrderId = orderId,
                    ProductId = product.Id,
                    CustomerId = customer.Id,
                    Region = record["Region"],
                    DateOfSale = DateTime.Parse(record["Date of Sale"]),
                    QuantitySold = int.Parse(record["Quantity Sold"]),
                    UnitPrice = decimal.Parse(record["Unit Price"]),
                    Discount = decimal.Parse(record["Discount"]),
                    ShippingCost = decimal.Parse(record["Shipping Cost"]),
                    PaymentMethod = record["Payment Method"]
                };
                _context.Orders.Add(order); 
            }

            await _context.SaveChangesAsync();
        }

    }
}
