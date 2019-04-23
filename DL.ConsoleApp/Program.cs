using DL.DataAccess;
using DL.Models;
using System.Collections.Generic;

namespace DL.ConsoleApp
{
    public class Program
    {
        private static UnitOfWork _unitOfWork;

        static void Main(string[] args)
        {
            _unitOfWork = new UnitOfWork();

            var data = _unitOfWork.Receivers.GetAll();

            var receivers = new List<Receiver>();

            foreach(var item in data)
            {
                receivers.Add(item);
            }
            
            _unitOfWork.Receivers.Delete(receivers[0].Id);

            _unitOfWork.Dispose();

            System.Console.ReadLine();
        }
    }
}
