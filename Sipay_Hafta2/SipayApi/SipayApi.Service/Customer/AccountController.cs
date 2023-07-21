using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SipayApi.Base;
using SipayApi.Data.Domain;
using SipayApi.Data.Repository;
using SipayApi.Schema;

namespace SipayApi.Service;



[ApiController]
[Route("sipy/api/[controller]")]
public class AccountController : ControllerBase
{
    private readonly IAccountRepository repository;
    private readonly IMapper mapper;
    public AccountController(IAccountRepository repository, IMapper mapper)
    {
        this.repository = repository;
        this.mapper = mapper;
    }

}
