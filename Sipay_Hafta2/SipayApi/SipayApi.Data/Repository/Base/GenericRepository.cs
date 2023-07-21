using Microsoft.EntityFrameworkCore;
using SipayApi.Base;
using System.Linq.Expressions;
using static Dapper.SqlMapper;

namespace SipayApi.Data.Repository;

public class GenericRepository<Entity> : IGenericRepository<Entity> where Entity : BaseModel
{
    private readonly SimDbContext dbContext;
    

    public GenericRepository(SimDbContext dbContext)
    {
        this.dbContext = dbContext;
       
    }
    //GenericRepository'de gelen kriterlere göre filtreleme işlemini gerçekleştirecek Where metodu implement edildi. 
    public IEnumerable<Entity> Where(Expression<Func<Entity, bool>> expression)
    {
        return dbContext.Set<Entity>().Where(expression);
    }
}
