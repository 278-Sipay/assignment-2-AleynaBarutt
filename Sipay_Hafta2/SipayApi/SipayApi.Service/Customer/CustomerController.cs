using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SipayApi.Base;
using SipayApi.Data.Domain;
using SipayApi.Data.Repository;
using SipayApi.Schema;

namespace SipayApi.Service;


[ApiController]
[Route("sipy/api/[controller]")]
public class CustomerController : ControllerBase
{
    private readonly ICustomerRepository repository;
    private readonly IMapper mapper;
    public CustomerController(ICustomerRepository repository, IMapper mapper)
    {
        this.repository = repository;
        this.mapper = mapper;
    }


}
