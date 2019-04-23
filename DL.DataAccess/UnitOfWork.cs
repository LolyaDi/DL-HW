using System.Configuration;
using System.Data.Common;
using System.Data.SqlClient;

namespace DL.DataAccess
{
    public class UnitOfWork
    {
        private DbConnection _connection;
        private MailRepository _mailRepository;
        private ReceiverRepository _receiverRepository;

        public UnitOfWork()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["appConnection"].ConnectionString;

            _connection = new SqlConnection(connectionString);
        }

        public MailRepository Mails
        {
            get
            {
                if (_mailRepository == null)
                    _mailRepository = new MailRepository(_connection);
                return _mailRepository;
            }
        }

        public ReceiverRepository Receivers
        {
            get
            {
                if (_receiverRepository == null)
                    _receiverRepository = new ReceiverRepository(_connection);
                return _receiverRepository;
            }
        }

        public void Dispose()
        {
            _connection.Close();
        }
    }
}
