using Microsoft.AspNetCore.Mvc;
using SipayApi.Base;
using SipayApi.Data;
using SipayApi.Data.Domain;

namespace SipayApi.Service;


[NonController]
[ApiController]
[Route("sipy/api/[controller]")]
public class Customer1Controller : ControllerBase
{
    private readonly SimDbContext dbContext;
    public Customer1Controller(SimDbContext dbContext)
    {
        this.dbContext = dbContext;
    }


    [HttpGet]
    public ApiResponse<List<Customer>> GetAll()
    {
        var entityList = dbContext.Set<Customer>().ToList();
        return new ApiResponse<List<Customer>>(entityList);
    }

    [HttpGet("{id}")]
    public ApiResponse<Customer> Get(int id)
    {
        var entity = dbContext.Set<Customer>().Find(id);
        return new ApiResponse<Customer>(entity);
    }


    [HttpPost]
    public ApiResponse Post([FromBody] Customer request)
    {
        dbContext.Set<Customer>().Add(request);
        dbContext.SaveChanges();
        return new ApiResponse();
    }

    [HttpPut("{id}")]
    public ApiResponse Put(int id, [FromBody] Customer request)
    {
        request.CustomerNumber = id;
        dbContext.Set<Customer>().Update(request);
        dbContext.SaveChanges();
        return new ApiResponse();
    }


    [HttpDelete("{id}")]
    public ApiResponse Delete(int id)
    {
        var entity = dbContext.Set<Customer>().Find(id);
        dbContext.Set<Customer>().Remove(entity);
        dbContext.SaveChanges();
        return new ApiResponse();
    }
}
