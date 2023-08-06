using System.Data.SqlClient;

namespace E._DAL.SQLServer.Infrastructure
{
    public interface IUnitOfWork
    {
        SqlTransaction BeginTransaction();
        SqlConnection GetConnection();
        SqlTransaction GetTransaction();
        void SaveChanges();
    }
}
