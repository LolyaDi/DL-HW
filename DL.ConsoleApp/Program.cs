using DL.DataAccess;
using DL.Models;
using System.Collections.Generic;
using System.Linq;

namespace DL.ConsoleApp
{
    public class Program
    {
        private static UnitOfWork _unitOfWork;

        static void Main(string[] args)
        {
            var receivers = new List<Receiver>();

            _unitOfWork = new UnitOfWork();

            var data = _unitOfWork.Receivers.GetAll();

            foreach (var item in data)
            {
                receivers.Add(item);
            }

            _unitOfWork.Dispose();

            var mails = new List<Mail>();

            string mailInfo;

            var mail = new Mail();
            
            System.Console.WriteLine("Введите тему сообщения:");
            mailInfo = System.Console.ReadLine();

            mail.Theme = mailInfo;

            System.Console.WriteLine("Введите текст сообщения");
            mailInfo = System.Console.ReadLine();

            mail.Text = mailInfo;

            System.Console.WriteLine("Выберите номера получателей через пробел:");

            int i = 0;

            System.Console.WriteLine(string.Format("{0}) {1,-20}","№","ФИО"));
            foreach (var receiver in receivers)
            {
                System.Console.WriteLine(string.Format("{0}) {1,-20}", ++i, receiver.Fullname));
            }

            mailInfo = System.Console.ReadLine();

            string[] splitted = mailInfo.Split(' ');
            int[] receiversArray = new int[splitted.Length];

            for (int j = 0; j < splitted.Length; j++)
            {
                bool isParsed = int.TryParse(splitted[j], out receiversArray[j]);

                if (!isParsed || receiversArray[j] > receivers.Count || receiversArray[j] <= 0)
                {
                    System.Console.WriteLine("Вы неверно ввели данные. Поплатитесь ж за это!");
                    System.Console.ReadLine();
                    return;
                }
            }

            var currentMailReceivers = new List<Receiver>();

            for (int z = 0; z < receiversArray.Length; z++)
            {
                currentMailReceivers.Add(receivers[receiversArray[z] - 1]);
            }
            
            foreach(var receiver in currentMailReceivers)
            {
                System.Console.WriteLine(receiver.Fullname);
            }

            System.Console.ReadLine();
        }
    }
}
