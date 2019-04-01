using Dapper;
using Npgsql;
using System;

namespace OdevTakip.Services
{
    /// <summary>
    /// Database üzerinde tüm işlemleri yapmamızı sağlar
    /// </summary>
    public static class GenericRepository
    {
        static string connectionString =
            "User ID=postgres;Password=localpass;Host=localhost;Port=5432;Database=dbodevtakip;";
        public static bool Insert(string sql, object model = null)
        {
            try
            {
                using (NpgsqlConnection npgsqlConnection = new NpgsqlConnection(connectionString))
                {
                    npgsqlConnection.Open();
                    npgsqlConnection.Execute(sql, model);
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

    }
}
