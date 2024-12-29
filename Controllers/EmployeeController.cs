using DisconnectedArchtechture_BusinessLogic.InterFaces;
using DisconnectedArchtechture_BusinessLogic.ModelDTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DisconnectedArchtechture_InMultilayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        public IEmployeeServices _services;
        public EmployeeController(IEmployeeServices services)
        {
            _services = services;
        }
        [HttpPost]
        [Route("AddEmployee")]
        public async Task<IActionResult> Post([FromBody] EmployeeDTO employeeDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, ModelState);
                }
                else
                {
                    await _services.AddEmployee(employeeDTO);
                    return StatusCode(StatusCodes.Status200OK, "Inserted");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "server error");
            }
        }
        [HttpGet(Name = "GetAllCustomer")]
        public async Task<IActionResult> GetAllCustomer()
        {
            try
            {
                var bookingData = await _services.GetAllEmployee();
                if (bookingData != null)
                {
                    return StatusCode(StatusCodes.Status200OK, bookingData);
                }
                else
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "Bad input request");
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "System or Server Error");
            }
        }
        [HttpDelete]
        [Route("DeleteCustomerById/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id < 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Bad input request");
            }

            try
            {
                var countryData = await _services.DeleteEmployeeById(id);
                if (countryData == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "customer Id not found");
                }
                else
                {
                    // var Data = await _bookingservice.DeleteBookingDetilsById(id);
                    return StatusCode(StatusCodes.Status204NoContent, "customer details deleted successfully");
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "System or Server Error");
            }
        }
        [HttpPut]
        [Route("UpdateCustomer")]
        public async Task<IActionResult> PUT([FromBody] EmployeeDTO employeeDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, ModelState);
                }
                var countryData = await _services.UpdateEmployee(employeeDTO);
                return StatusCode(StatusCodes.Status201Created, "Customer Details Updated Succesfully");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "System or Server Error");
            }
        }
        [HttpGet]
        [Route("GetCustomerById/{id}")]
        public async Task<IActionResult> Get(int id)
        {
            if (id < 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Bad input request");
            }
            try
            {
                var bookingData = await _services.GetEmployeeById(id);
                if (bookingData == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "Customer Id not found");
                }
                else
                {
                    return StatusCode(StatusCodes.Status200OK, bookingData);
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "System or Server Error");
            }
        }
    }
}
