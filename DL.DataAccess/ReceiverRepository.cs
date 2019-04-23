using System;
using DL.DataAccess.Abstract;
using DL.Models;
using System.Data.Common;
using Dapper;
using System.Collections.Generic;

namespace DL.DataAccess
{
    public class ReceiverRepository : IRepository<Receiver>
    {
        private DbConnection _connection;

        public ReceiverRepository(DbConnection connection)
        {
            _connection = connection;
        }

        public void Add(Receiver item)
        {
            var sqlQuery = "insert into Receivers (Id,CreationDate,DeletedDate,FullName,Address) values(@Id,@CreationDate,@DeletedDate,@FullName,@Address)";
            var result = _connection.Execute(sqlQuery, item);

            if (result < 1) throw new Exception("Запись не вставлена!");
        }

        public void Delete(Guid id)
        {
            var sqlQuery = "update Receivers set DeletedDate = GetDate() where Id = @id";
            var result = _connection.Execute(sqlQuery, new { id });

            if (result < 1) throw new Exception("Запись не удалена!");
        }

        public void Dispose()
        {
            _connection.Close();
        }

        public ICollection<Receiver> GetAll()
        {
            var sqlQuery = "select * from Receivers";
            return _connection.Query<Receiver>(sqlQuery).AsList();
        }

        public void Update(Receiver item)
        {
            var sqlQuery = "update Receivers set CreationDate = @CreationDate, DeletedDate = @DeletedDate, FullName = @FullName, Address = @Address where Id = @Id";
            var result = _connection.Execute(sqlQuery, item);

            if(result < 1) throw new Exception("Запись не обновлена!");
        }
    }
}
