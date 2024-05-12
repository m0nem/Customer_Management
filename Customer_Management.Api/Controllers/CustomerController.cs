using Customer_Management.Application.DTOs.Customer;
using Customer_Management.Application.Features.Customer.Requests.Commands;
using Customer_Management.Application.Features.Customer.Requests.Queries;
using Customer_Management.Application.Responses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace Customer_Management.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CustomerController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CustomerController(IMediator mediator)
        {
            _mediator = mediator;
        }
        // GET: api/<CustomerController>
        [HttpGet]
        public async Task<ActionResult<List<CustomerDto>>> Get()
        {
            var customers = await _mediator.Send(new GetCustomerListRequest());
            return Ok(customers);
        }

        // GET api/<CustomerController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GetCustomerDetailRequest>> Get(int id)
        {
            var customer = await _mediator.Send(new GetCustomerDetailRequest { Id = id });
            return Ok(customer);
        }

        // POST api/<CustomerController> 
        [HttpPost]
        public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateCustomerDto customerDto)
        {
            var command = new CreateCustomerCommand { CustomerDto = customerDto };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        // PUT api/<CustomerController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] UpdateCustomerDto customerDto)
        {
            var command = new UpdateCustomerCommand { Customer = customerDto };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        // DELETE api/<CustomerController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var command = new DeleteCustomerCommand { Id = id };
            await _mediator.Send(command);
            return NoContent();
        }
    }
}
