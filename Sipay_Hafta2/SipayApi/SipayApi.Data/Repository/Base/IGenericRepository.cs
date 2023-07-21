using static Dapper.SqlMapper;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace SipayApi.Data.Repository;

public interface IGenericRepository<Entity> where Entity : class
{
    // GenericRepository'de gelen kriterlere göre filtreleme işlemini gerçekleştirecek Where metodu
    public IEnumerable<Entity> Where(Expression<Func<Entity, bool>> expression);
   
}
