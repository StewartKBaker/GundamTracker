using System.Data;
using Dapper;
using Gundam_Tracker.Models;

namespace Gundam_Tracker;

public class GundamRepo : IGundamRepo
{
    private readonly IDbConnection _conn; //creates connection

    public GundamRepo(IDbConnection dbConn)
    {
        _conn = dbConn;
    }

    public IEnumerable<Gundam> GetAllGunpla() //gets all gundam from the gundam database
    {
        return _conn.Query<Gundam>("SELECT * FROM products;");
    }

    public IEnumerable<Gundam> GetGradeGunpla(string grade) //gets all gundam matching a specific grade from database
    {
        return _conn.Query<Gundam>($"SELECT * FROM products WHERE Grade = '{grade}'");
    }

    public IEnumerable<Gundam> GetBuiltGunpla(int built) //gets all gundam based on build status from database
    {
        return _conn.Query<Gundam>($"SELECT * FROM products WHERE Built = '{built}'"); 
    }

    public Gundam GetGunpla(int id) //gets a specific gundam from database
    {
        var product = _conn.QuerySingle<Gundam>("SELECT * FROM products WHERE GundamID = @id;", new { id = id });
        return product;
    }

    public void UpdateGunpla(Gundam gundam) //updates specific gundam in database
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

    public void InsertGunpla(Gundam gunplaToInsert) //adds new gundam to database
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

    public void DeleteGunpla(Gundam gundam) //deletes gundam from database
    {
        _conn.Execute("DELETE FROM products WHERE GundamID = @id;", new { id = gundam.GundamID });
    }
}