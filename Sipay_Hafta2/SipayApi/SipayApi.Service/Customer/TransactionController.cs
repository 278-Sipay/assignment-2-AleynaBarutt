using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SipayApi.Base;
using SipayApi.Data.Domain;
using SipayApi.Data.Repository;
using SipayApi.Schema;
using System.Linq.Expressions;

namespace SipayApi.Service;



[ApiController]
[Route("sipy/api/[controller]")]
public class TransactionController : ControllerBase
{
    private readonly ITransactionRepository repository;
    private readonly IMapper mapper;
    public TransactionController(ITransactionRepository repository, IMapper mapper)
    {
        this.repository = repository;
        this.mapper = mapper;

    }

    // GetByParameter metodu filtre kriterlerine göre Transaction varlıklarını sorgular ve sonuçları TransactionResponse tipinde döndürür.
    [HttpGet("GetByParameter")]
    public ApiResponse<List<TransactionResponse>> GetByParameter([FromQuery] int? accountNumber, [FromQuery] decimal? minAmountCredit, [FromQuery] decimal? maxAmountCredit, [FromQuery] decimal? minAmountDebit, [FromQuery] decimal? maxAmountDebit, [FromQuery] string? description, [FromQuery] DateTime? beginDate, [FromQuery] DateTime? endDate, [FromQuery] string? referenceNumber)
    {
        Expression<Func<Transaction, bool>> filter = entity =>
        (!accountNumber.HasValue || entity.AccountNumber == accountNumber.Value) &&
        (!minAmountCredit.HasValue || entity.CreditAmount >= minAmountCredit.Value) &&
        (!maxAmountCredit.HasValue || entity.CreditAmount <= maxAmountCredit.Value) &&
        (!minAmountDebit.HasValue || entity.DebitAmount >= minAmountDebit.Value) &&
        (!maxAmountDebit.HasValue || entity.DebitAmount <= maxAmountDebit.Value) &&
        (string.IsNullOrEmpty(description) || entity.Description.Contains(description)) &&
        (!beginDate.HasValue || entity.TransactionDate >= beginDate.Value) &&
        (!endDate.HasValue || entity.TransactionDate <= endDate.Value) &&
        (string.IsNullOrEmpty(referenceNumber) || entity.ReferenceNumber == referenceNumber);

        //TransactionRepository'deki GenericRepository'de tanımlanan Where metodunu çağırır ve verilen filtre ifadesi olan filter kullanılarak Transaction varlıklarını sorgular.
        //Sonuç olarak, filtreleme kriterlerine uyan Transaction varlıklarının bir listesini döndürür.
        var result = repository.Where(filter).ToList();
        var mapped = mapper.Map<List<Transaction>, List<TransactionResponse>>(result);
        return new ApiResponse<List<TransactionResponse>>(mapped);
    }
}
