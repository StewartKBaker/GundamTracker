using System.Data;
using Dapper;
using Gundam_Tracker.Models;

namespace Gundam_Tracker;

public class GundamRepo : IGundamRepo
{
    private readonly IDbConnection _conn;

    public GundamRepo(IDbConnection dbConn)
    {
        _conn = dbConn;
    }

    public IEnumerable<Gundam> GetAllProducts()
    {
        return _conn.Query<Gundam>("SELECT * FROM products;");
    }
    
    
}