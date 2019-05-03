using Dapper;
using Npgsql;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Win32.SafeHandles;
using System;
using System.Runtime.InteropServices;

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

    static class CustomNpgsqlConnection
    {
        private static readonly string connectionString =
            "User ID=postgres;Password=localpass;Host=localhost;Port=5432;Database=dbodevtakip;";

        static NpgsqlConnection _npgsqlConnection;
        //proxy
        public static NpgsqlConnection npgsqlConnection
        {
            get
            {
                if (_npgsqlConnection == null)
                {
                    _npgsqlConnection = new NpgsqlConnection(connectionString);
                    _npgsqlConnection.Open();
                }

                return _npgsqlConnection;
            }
        }

    }

    /// <summary>
    /// Database üzerinde tüm işlemleri yapmamızı sağlar
    /// </summary>
    public class GenericRepository : IGenericRepository
    {


        public void BeginTransaction()
        {
            CustomNpgsqlConnection.npgsqlConnection.Execute("BEGIN TRANSACTION;");
        }

        public void RollBackTransaction()
        {
            CustomNpgsqlConnection.npgsqlConnection.Execute("ROLLBACK;");
        }

        public bool Delete(string sql, object model)
        {
            try
            {
                CustomNpgsqlConnection.npgsqlConnection.Execute(sql, model);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public T First<T>(string sql, object model) where T : class
        {
            try
            {

                return CustomNpgsqlConnection.npgsqlConnection.QueryFirst<T>(sql, model);
            }
            catch
            {
                return null;
            }
        }

        public bool Insert(string sql, object model = null)
        {
            try
            {

                CustomNpgsqlConnection.npgsqlConnection.Execute(sql, model);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public int InsertAndGetId(string sql, object model = null)
        {
            try
            {
                int id = 0;

                sql += " RETURNING Id";

                id = CustomNpgsqlConnection.npgsqlConnection.QueryFirst<int>(sql, model);

                return id;
            }
            catch
            {
                return 0;
            }
        }

        public IEnumerable<dynamic> Select(string sql, object model = null)
        {
            try
            {

                return CustomNpgsqlConnection.npgsqlConnection.Query(sql, model);
            }
            catch
            {
                return null;
            }
        }

        public List<TList> Select<TList>(string sql, object model = null) where TList : class
        {
            try
            {
                return CustomNpgsqlConnection.npgsqlConnection.Query<TList>(sql, model).ToList();
            }
            catch
            {
                return null;
            }
        }

        public bool Update(string sql, object model)
        {
            try
            {
                CustomNpgsqlConnection.npgsqlConnection.Execute(sql, model);

                return true;
            }
            catch
            {
                return false;
            }
        }

    }

    public static class SGenericRepository
    {
        private static readonly string connectionString =
            "User ID=postgres;Password=localpass;Host=localhost;Port=5432;Database=dbodevtakip;";

        public static List<dynamic> Select(string sql, object model = null)
        {
            try
            {
                return CustomNpgsqlConnection.npgsqlConnection.Query(sql, model).ToList();
            }
            catch
            {
                return null;
            }
        }


    }
}
