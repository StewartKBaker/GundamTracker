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

    public IEnumerable<Gundam> GetAllGunpla()
    {
        return _conn.Query<Gundam>("SELECT * FROM products;");
    }

    public IEnumerable<Gundam> GetGradeGunpla(string grade)
    {
        return _conn.Query<Gundam>($"SELECT * FROM products WHERE Grade = '{grade}'");
    }

    public Gundam GetGunpla(int id)
    {
        var product = _conn.QuerySingle<Gundam>("SELECT * FROM products WHERE GundamID = @id;", new { id = id });
        return product;
    }

    public void UpdateGunpla(Gundam gundam)
    {
        _conn.Execute(
            "UPDATE products SET Name = @name, Price = @price, Grade = @grade, Built = @built, Rating = @rating WHERE GundamID = @id",
            new
            {
                name = gundam.Name, 
                price = gundam.Price, 
                grade = gundam.Grade, 
                built = gundam.Built,
                rating = gundam.Rating, 
                id = gundam.GundamID
            });
    }

    public void InsertGunpla(Gundam gunplaToInsert)
    {
        _conn.Execute(
            "INSERT INTO products (Name, Price, Grade, Built, Rating) VALUES (@newName, @newPrice, @newGrade, @newBuilt, @newRating);",
            new
            {
                newName = gunplaToInsert.Name, 
                newPrice = gunplaToInsert.Price, 
                newGrade = gunplaToInsert.Grade,
                newBuilt = gunplaToInsert.Built, 
                newRating = gunplaToInsert.Rating
            });
    }

    public void DeleteGunpla(Gundam gundam)
    {
        _conn.Execute("DELETE FROM products WHERE GundamID = @id;", new { id = gundam.GundamID });
    }
}