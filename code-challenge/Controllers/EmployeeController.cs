using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using challenge.Services;
using challenge.Models;

namespace challenge.Controllers
{
    [Route("api/employee")]
    public class EmployeeController : Controller
    {
        private readonly ILogger _logger;
        private readonly IEmployeeService _employeeService;

        public EmployeeController(ILogger<EmployeeController> logger, IEmployeeService employeeService)
        {
            _logger = logger;
            _employeeService = employeeService;
        }

        [HttpPost]
        public IActionResult CreateEmployee([FromBody] Employee employee)
        {
            _logger.LogDebug($"Received employee create request for '{employee.FirstName} {employee.LastName}'");

            _employeeService.Create(employee);

            return CreatedAtRoute("getEmployeeById", new { id = employee.EmployeeId }, employee);
        }

        [HttpGet("{id}", Name = "getEmployeeById")]
        public IActionResult GetEmployeeById(String id)
        {
            _logger.LogDebug($"Received employee get request for '{id}'");

            var employee = _employeeService.GetById(id);

            if (employee == null)
                return NotFound();

            return Ok(employee);
        }

        [HttpPut("{id}")]
        public IActionResult ReplaceEmployee(String id, [FromBody] Employee newEmployee)
        {
            _logger.LogDebug($"Recieved employee update request for '{id}'");

            var existingEmployee = _employeeService.GetById(id);
            if (existingEmployee == null)
                return NotFound();

            _employeeService.Replace(existingEmployee, newEmployee);

            return Ok(newEmployee);
        }

        // api/employee/reporting/0
        [HttpGet]
        [Route("reporting/{id}")]
        public IActionResult GetEmployeeReporting(String id)
        {
            _logger.LogDebug($"Recieved get request for employee reporting details  for '{id}'");

            ReportingStructure employeeReportingDetails = _employeeService.GetEmployeeReportingDetails(id);
            if (employeeReportingDetails == null)
                return NotFound();

            return Ok(employeeReportingDetails);
        }

        [HttpPost]
        [Route("compensation")]
        public IActionResult CreateEmployeeCompensation([FromBody] EmployeeCompensationRequest employeeCompensation)
        {
            _logger.LogDebug($"Received employee create compensation request for '{employeeCompensation.employeeId}'");

            if (!ModelState.IsValid)
                return BadRequest();

            Employee emp = this._employeeService.GetById(employeeCompensation.employeeId);
            if (emp == null)
                return NotFound();

            if (this._employeeService.CreateEmployeeCompensation(EmployeeCompensationRequest.MapTo(employeeCompensation)))
                return CreatedAtRoute("reateEmployeeCompensation", employeeCompensation);
            else
                return StatusCode(500, "Create Compensation Failed");
        }

        [HttpGet]
        [Route("compensation/{id}")]
        public IActionResult GetEmployeeCompensation(String id)
        {
            _logger.LogDebug($"Received employee compensation get request for '{id}'");

            EmployeeCompensation empCompensation = this._employeeService.GetEmployeeCompensation(id);

            if (empCompensation == null)
                return NotFound();

            return Ok(EmployeeCompensationDTO.MapFrom(empCompensation,this._employeeService.GetById(id)));
        }


    }
}
