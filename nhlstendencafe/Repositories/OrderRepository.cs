using System.Data;
using Dapper;

namespace nhlstendencafe.Repositories;

public class OrderRepository {

    private IDbConnection GetConnection()
    {
        return new DbUtils().GetDbConnection();
    }

    public long CreateOrder(int userId, int tableNumber)
    {
        using var connection = GetConnection();
        var orderId = connection.ExecuteScalar<int>(
        @"
            INSERT INTO Orders (UserId, TableNumber) 
            VALUES (@UserId, @TableNumber);
            SELECT LAST_INSERT_ID();", new
        {
            UserId = userId,
            TableNumber = tableNumber
        });
        return orderId;
    }

    public void AddOrderLine(int orderId, int productId, int quantity, int amountPaid)
    {
        using var connection = GetConnection();
        connection.Execute(@"
        INSERT INTO OrderLine (OrderId, ProductId, Quantity, AmountPaid)
        VALUES (@OrderId, @ProductId, @Quantity, @AmountPaid);
    ", new
        {
            OrderId = orderId,
            ProductId = productId,
            Quantity = quantity,
            AmountPaid = amountPaid
        });
    }

    public IEnumerable<dynamic> GetOrderLinesForOrder(int orderId)
    {
        using var connection = GetConnection();
        var orderLines = connection.Query<dynamic>(@"
        SELECT * 
        FROM OrderLine 
        WHERE OrderId = @OrderId;
    ", new { OrderId = orderId });
        return orderLines;
    }
    
    public void UpdateOrderTableNumber(int orderId, int newTableNumber)
    {
        using var connection = GetConnection();
        connection.Execute(@"
        UPDATE Orders 
        SET TableNumber = @NewTableNumber 
        WHERE OrderId = @OrderId;
    ", new
        {
            NewTableNumber = newTableNumber,
            OrderId = orderId
        });
    }

    public int GetTotalAmountPaidForOrder(int orderId)
    {
        using var connection = GetConnection();
        var totalAmountPaid = connection.ExecuteScalar<int>(@"
        SELECT SUM(AmountPaid) 
        FROM OrderLine
        WHERE OrderId = @OrderId;
    ", new { OrderId = orderId });
        return totalAmountPaid;
    }



}
