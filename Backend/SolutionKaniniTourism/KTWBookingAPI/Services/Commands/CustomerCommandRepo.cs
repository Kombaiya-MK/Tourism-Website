using KTWBookingAPI.Interfaces;
using KTWBookingAPI.Models;
using KTWBookingAPI.Utilities.CustomExceptions;
using Microsoft.EntityFrameworkCore;

namespace KTWBookingAPI.Services.Commands
{
    public class CustomerCommandRepo : ICommandRepo<Customer , string>
    {
        private readonly BookingContext _context;
        private readonly ILogger<Customer> _logger;

        public CustomerCommandRepo(BookingContext context, ILogger<Customer> logger)
        {
            _context = context;
            _logger = logger;
        }

        //Add Customer 
        public async Task<Customer> Add(Customer item)
        {
            if (item == null)
            {
                _logger.LogError("Empty object being Passed");
                throw new EmptyValueException("Customer Object is null");
            }
            _logger.LogInformation("Into the Add Method");
            var Customer = _context.Customers.Add(item);
            if (Customer == null)
            {
                _logger.LogError("Unable to add object");
                throw new UnableToAddException("Unable To Add Customer Service");
            }
            await _context.SaveChangesAsync();
            _logger.LogInformation("Customer Added Successfully");
            return item;
            throw new UnableToAddException("Unable To Add Customer");
        }


        //Update Customer
        public async Task<Customer> Update(Customer item)
        {
            if (item == null)
            {
                _logger.LogError("Empty object being Passed");
                throw new EmptyValueException("Customer Object is null");
            }
            var Customer = new Customer();
            Customer = await _context.Customers.FirstOrDefaultAsync(x => x.CustomerId == item.CustomerId);
            if (Customer == null)
                throw new EmptyValueException("Invalid Object!!! No such user Exist!!");
            if (item != null)
            {
                Customer.CustomerName = (item.CustomerName != null && item.CustomerName.Length > 0) ? item.CustomerName : Customer.CustomerName;
                Customer.CustomerGender = (item.CustomerGender != null && item.CustomerGender.Length > 0) ? item.CustomerGender : Customer.CustomerGender;
                Customer.CustomerAge = (item.CustomerAge != null && item.CustomerAge.Length > 0) ? item.CustomerAge : Customer.CustomerAge;
                await _context.SaveChangesAsync();
            }
            return Customer;
            throw new UnableToUpdateException("Unable to update the travel agent");
        }
    }
}
