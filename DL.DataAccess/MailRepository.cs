using System;
using DL.DataAccess.Abstract;
using DL.Models;
using System.Data.Common;
using Dapper;
using System.Collections.Generic;

namespace DL.DataAccess
{
    public class MailRepository : IRepository<Mail>
    {
        private DbConnection _connection;

        public MailRepository(DbConnection connection)
        {
            _connection = connection;
        }

        public void Add(Mail item)
        {
            var sqlQuery = "insert into Mails (Id,CreationDate,DeletedDate,Theme,Text,ReceiverId) values(@Id,@CreationDate,@DeletedDate,@Theme,@Text,@ReceiverId)";
            var result = _connection.Execute(sqlQuery, new { item.Id, item.CreationDate, item.DeletedDate, item.Theme, item.Text, item.ReceiverId });

            if (result < 1) throw new Exception("Запись не вставлена!");
        }

        public void Delete(Guid id)
        {
            var sqlQuery = "update Mails set DeletedDate = GetDate() where Id = @id";
            var result = _connection.Execute(sqlQuery, new { id });

            if (result < 1) throw new Exception("Запись не удалена!");
        }

        public void Dispose()
        {
            _connection.Close();
        }

        public void Update(Mail item)
        {
            var sqlQuery = "update Mails set CreationDate = @CreationDate, DeletedDate = @DeletedDate, Theme = @Theme, Text = @Text, ReceiverId = @ReceiverId where Id = @Id";
            var result = _connection.Execute(sqlQuery, new { item.Id, item.CreationDate, item.DeletedDate, item.Theme, item.Text, item.ReceiverId });

            if (result < 1) throw new Exception("Запись не обновлена!");
        }

        public ICollection<Mail> GetAll()
        {
            var sqlQuery = "select * from Mails";
            return _connection.Query<Mail>(sqlQuery).AsList();
        }
    }
}