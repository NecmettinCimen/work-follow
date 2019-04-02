using Dapper;
using Npgsql;
using OdevTakip.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OdevTakip.Services
{
    public interface IGenericRepository
    {
        bool Insert(string sql, object model = null);

        IEnumerable<dynamic> Select(string sql, object model = null);

        T First<T>(string sql, object model) where T : class;

        List<TList> Select<TList>(string sql, object model) where TList : class;
    }

    /// <summary>
    /// Database üzerinde tüm işlemleri yapmamızı sağlar
    /// </summary>
    public class GenericRepository: IGenericRepository
    {
        private static readonly string connectionString =
            "User ID=postgres;Password=localpass;Host=localhost;Port=5432;Database=dbodevtakip;";

        public T First<T>(string sql, object model) where T: class
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
            catch (Exception ex)
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
            catch (Exception ex)
            {
                return false;
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

        public List<TList> Select<TList>(string sql, object model) where TList : class
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
