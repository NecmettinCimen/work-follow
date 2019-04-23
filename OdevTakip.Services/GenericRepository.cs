using Dapper;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OdevTakip.Services
{
    public interface IGenericRepository
    {
        bool Insert(string sql, object model = null);
        int InsertAndGetId(string sql, object model = null);

        IEnumerable<dynamic> Select(string sql, object model = null);

        T First<T>(string sql, object model) where T : class;

        List<TList> Select<TList>(string sql, object model = null) where TList : class;

        bool Delete(string sql, object model);

        bool Update(string sql, object model);
    }

    /// <summary>
    /// Database üzerinde tüm işlemleri yapmamızı sağlar
    /// </summary>
    public class GenericRepository : IGenericRepository
    {
        private static readonly string connectionString =
            "User ID=postgres;Password=localpass;Host=localhost;Port=5432;Database=dbodevtakip;";

        public bool Delete(string sql, object model)
        {
            try
            {
                using (NpgsqlConnection npgsqlConnection = new NpgsqlConnection(connectionString))
                {
                    if (npgsqlConnection.State != System.Data.ConnectionState.Open)
                    {
                        npgsqlConnection.Open();
                    }

                    npgsqlConnection.Execute(sql, model);
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public T First<T>(string sql, object model) where T : class
        {
            try
            {
                using (NpgsqlConnection npgsqlConnection = new NpgsqlConnection(connectionString))
                {
                    if (npgsqlConnection.State != System.Data.ConnectionState.Open)
                    {
                        npgsqlConnection.Open();
                    }

                    return npgsqlConnection.QueryFirst<T>(sql, model);
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public bool Insert(string sql, object model = null)
        {
            try
            {
                using (NpgsqlConnection npgsqlConnection = new NpgsqlConnection(connectionString))
                {
                    if (npgsqlConnection.State != System.Data.ConnectionState.Open)
                    {
                        npgsqlConnection.Open();
                    }

                    npgsqlConnection.Execute(sql, model);
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public int InsertAndGetId(string sql, object model = null)
        {
            try
            {
                int id = 0;
                using (NpgsqlConnection npgsqlConnection = new NpgsqlConnection(connectionString))
                {
                    if (npgsqlConnection.State != System.Data.ConnectionState.Open)
                    {
                        npgsqlConnection.Open();
                    }

                    sql += " RETURNING Id";

                    id =  npgsqlConnection.QueryFirst<int>(sql, model);
                }

                return id;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public IEnumerable<dynamic> Select(string sql, object model = null)
        {
            try
            {
                using (NpgsqlConnection npgsqlConnection = new NpgsqlConnection(connectionString))
                {
                    if (npgsqlConnection.State != System.Data.ConnectionState.Open)
                    {
                        npgsqlConnection.Open();
                    }

                    return npgsqlConnection.Query(sql, model);
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<TList> Select<TList>(string sql, object model = null) where TList : class
        {
            try
            {
                using (NpgsqlConnection npgsqlConnection = new NpgsqlConnection(connectionString))
                {
                    if (npgsqlConnection.State != System.Data.ConnectionState.Open)
                    {
                        npgsqlConnection.Open();
                    }

                    return npgsqlConnection.Query<TList>(sql, model).ToList();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public bool Update(string sql, object model)
        {
            try
            {
                using (NpgsqlConnection npgsqlConnection = new NpgsqlConnection(connectionString))
                {
                    if (npgsqlConnection.State != System.Data.ConnectionState.Open)
                    {
                        npgsqlConnection.Open();
                    }

                    npgsqlConnection.Execute(sql, model);
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }

    public class SGenericRepository
    {
        private static readonly string connectionString =
            "User ID=postgres;Password=localpass;Host=localhost;Port=5432;Database=dbodevtakip;";

        public List<TList> Select<TList>(string sql, object model = null) where TList : class
        {
            try
            {
                using (NpgsqlConnection npgsqlConnection = new NpgsqlConnection(connectionString))
                {
                    if (npgsqlConnection.State != System.Data.ConnectionState.Open)
                    {
                        npgsqlConnection.Open();
                    }

                    return npgsqlConnection.Query<TList>(sql, model).ToList();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

    }
}
