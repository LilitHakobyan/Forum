using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Forum.Models
{
    public class Repasitory : IDisposable
    {
        private SqlConnection _connection;
        private SqlTransaction _transaction;
        public Repasitory(string connection)
        {
            _connection  = new SqlConnection(connection);
            _transaction = _connection.BeginTransaction();
        }

        public SqlCommand CreateCommand()
        {
            var command = _connection.CreateCommand();//new SqlCommand(commandText, connection);
            command.Transaction = _transaction;
            return command;
        }

        public void SaveChanges()
        {
            if (_transaction == null)
                throw new InvalidOperationException
                    ("Transaction have already been committed. Check your transaction handling.");

            _transaction.Commit();
            _transaction = null;
        }

        public void Dispose()
        {
            if (_transaction != null)
            {
                _transaction.Rollback();
                _transaction = null;
            }

            if (_connection != null)
            {
                _connection.Close();
                _connection = null;
            }
        }
    }
}