using AcmeApp.Models;
using AcmeApp.RequestModel;
using AcmeApp.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AcmeApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [EnableCors("AllowCors")]
    public class EmployeeController : ControllerBase
    {
        private readonly ACMEDBContext _acmContext;
        private readonly ILogger<EmployeeController> _logger;
        private readonly EmployeeService _customerService;

        public EmployeeController(ILogger<EmployeeController> logger,
            EmployeeService customerService,
            ACMEDBContext acmContext)
        {
            _logger = logger;
            _customerService = customerService;
            _acmContext = acmContext;
        }

        [HttpGet("getAll")]
        public Task<ApiResponse<List<EmployeeRequest>>> Get()
        {
            return _customerService.GetAllCustomer(_acmContext);
        }

        [HttpGet("getById/{id:int}")]
        public Task<ApiResponse<EmployeeRequest>> GetById([BindRequired] int id)
        {
            return _customerService.GetCustomerId(_acmContext, id);
        }

        [HttpPost("add")]
        public Task<ApiResponse<bool>> Post([FromBody] EmployeeRequest customer)
        {
            return _customerService.AddlCustomer(_acmContext, customer);
        }

        [HttpPut("update/{id:int}")]
        public Task<ApiResponse<bool>> Put([BindRequired] int id, [FromBody] EmployeeRequest customer)
        {
            return _customerService.UpdateCustomer(_acmContext, id, customer);
        }

        [HttpDelete("delete/{id:int}")]
        public Task<ApiResponse<bool>> Delete([BindRequired] int id)
        {
            return _customerService.DeleteCustomer(_acmContext, id);
        }
    }
}
