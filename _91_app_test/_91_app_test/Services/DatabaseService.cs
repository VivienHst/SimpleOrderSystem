using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using _91_app_test.Models;
using Dapper;
using MySqlConnector;

namespace _91_app_test
{
    public interface IDatabaseService
    {
        public List<T> Query<T>(DatabaseObject databaseObject);
        public T SingleQuery<T>(DatabaseObject databaseObject);
        int Insert(DatabaseObject databaseObject);
        public int Update(DatabaseObject databaseObject);

    }

    public class DatabaseService : IDatabaseService
    {
        public int Insert(DatabaseObject databaseObject)
        {
            int result = 0;
            using (MySqlConnection conn = new MySqlConnection(databaseObject.connStr))
            {
                if (conn.State == ConnectionState.Closed) conn.Open();
                using (var transaction = conn.BeginTransaction())
                {
                    try
                    {
                        result = conn.Execute(databaseObject.sqlStatement, databaseObject.data, transaction);
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw ex;
                    }
                }

            }
            return result;
        }

        public List<T> Query<T>(DatabaseObject databaseObject)
        {
            List<T> result;
            using (MySqlConnection conn = new MySqlConnection(databaseObject.connStr))
            {
                try
                {
                    if (conn.State == ConnectionState.Closed) conn.Open();
                    result = conn.Query<T>(databaseObject.sqlStatement, databaseObject.data).AsList<T>();
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
            return result;
        }

        public T SingleQuery<T>(DatabaseObject databaseObject)
        {
            T result;

            using (MySqlConnection conn = new MySqlConnection(databaseObject.connStr))
            {
                try
                {
                    if (conn.State == ConnectionState.Closed) conn.Open();
                    result = conn.Query<T>(databaseObject.sqlStatement, databaseObject.data).FirstOrDefault();
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
            return result;
        }

        public int Update(DatabaseObject databaseObject)
        {
            int result = 0;
            using (MySqlConnection conn = new MySqlConnection(databaseObject.connStr))
            {
                if (conn.State == ConnectionState.Closed) conn.Open();

                using (var transaction = conn.BeginTransaction())
                {
                    try
                    {
                        result = conn.Execute(databaseObject.sqlStatement, databaseObject.data, transaction);
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw ex;
                    }
                }

            }
            return result;
        }
    }
}
