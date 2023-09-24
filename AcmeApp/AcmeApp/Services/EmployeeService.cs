using AcmeApp.Models;
using AcmeApp.RequestModel;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace AcmeApp.Services
{
    public class EmployeeService
    {
        public async Task<ApiResponse<List<EmployeeRequest>>> GetAllCustomer(ACMEDBContext _acmContext)
        {
            var customers = await _acmContext.Employee.Include(obj => obj.Person).ToListAsync();
            return new ApiResponse<List<EmployeeRequest>>
            {
                Status = HttpStatusCode.OK,
                Result = customers.Select(obj => new EmployeeRequest
                {
                    Id = obj.EmployeeId,
                    FirstName = obj.Person.FirstName,
                    LastName = obj.Person.LastName,
                    EmployeeNumber = obj.EmployeeNumber,
                    BirthDate = obj.Person.BirthDate,
                    EmployeeDate = obj.EmployeeDate,
                    TerminatedDate = obj.TerminatedDate
                }).ToList(),
                Message = "Data Fetched Successfully!"
            };
        }

        public async Task<ApiResponse<EmployeeRequest>> GetCustomerId(ACMEDBContext _acmContext, int id)
        {
            var customer = await _acmContext.Employee.Include(obj => obj.Person).FirstOrDefaultAsync(obj => obj.EmployeeId == id);

            return new ApiResponse<EmployeeRequest>
            {
                Status = HttpStatusCode.OK,
                Result = new EmployeeRequest
                {
                    Id = customer.EmployeeId,
                    FirstName = customer.Person.FirstName,
                    LastName = customer.Person.LastName,
                    EmployeeNumber = customer.EmployeeNumber,
                    BirthDate = customer.Person.BirthDate,
                    EmployeeDate = customer.EmployeeDate,
                    TerminatedDate = customer.TerminatedDate
                },
                Message = "Data Fetched Successfully!"
            };
        }

        public async Task<ApiResponse<bool>> AddlCustomer(ACMEDBContext _acmContext, EmployeeRequest customer)
        {
            var newCustomer = new Employee
            {
                EmployeeNumber = customer.EmployeeNumber,
                EmployeeDate = customer.EmployeeDate,
                TerminatedDate = customer.TerminatedDate,
                Person = new Person
                {
                    FirstName = customer.FirstName,
                    LastName = customer.LastName,
                    BirthDate = customer.BirthDate
                }
            };

            await _acmContext.Employee.AddAsync(newCustomer);
            await _acmContext.SaveChangesAsync();
            return new ApiResponse<bool>
            {
                Status = HttpStatusCode.OK,
                Result = true,
                Message = "Employee Added Successfully"
            };
        }

        public async Task<ApiResponse<bool>> UpdateCustomer(ACMEDBContext _acmContext, int id, EmployeeRequest customer)
        {
            var existingCustomer = await _acmContext.Employee.Include(obj => obj.Person).FirstOrDefaultAsync(obj => obj.EmployeeId == id);

            existingCustomer.EmployeeNumber = customer.EmployeeNumber;
            existingCustomer.EmployeeDate = customer.EmployeeDate;
            existingCustomer.TerminatedDate = customer.TerminatedDate;
            existingCustomer.Person = new Person
            {
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                BirthDate = customer.BirthDate
            };

            _acmContext.Employee.Update(existingCustomer);
            await _acmContext.SaveChangesAsync();
            return new ApiResponse<bool>
            {
                Status = HttpStatusCode.OK,
                Result = true,
                Message = "Employee Updated Successfully"
            };
        }

        public async Task<ApiResponse<bool>> DeleteCustomer(ACMEDBContext _acmContext, int id)
        {
            var existingCustomer = await _acmContext.Employee.Include(obj => obj.Person).FirstOrDefaultAsync(obj => obj.EmployeeId == id);

            _acmContext.Employee.Remove(existingCustomer);
            await _acmContext.SaveChangesAsync();
            return new ApiResponse<bool>
            {
                Status = HttpStatusCode.OK,
                Result = true,
                Message = "Employee Deleted Successfully"
            };
        }
    }
}
