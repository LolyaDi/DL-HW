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
            var receivers = new List<Receiver>();

            _unitOfWork = new UnitOfWork();

            var data = _unitOfWork.Receivers.GetAll();

            foreach (var item in data)
            {
                receivers.Add(item);
            }

            var mails = new List<Mail>();

            string mailInfo, mailTheme, mailText;
           
            System.Console.WriteLine("Введите тему сообщения:");
            mailTheme = System.Console.ReadLine();
            
            System.Console.WriteLine("Введите текст сообщения");
            mailText = System.Console.ReadLine();
            
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
                var mail = new Mail
                {
                    Theme = mailTheme,
                    Text = mailText,
                    Receiver = receiver,
                    ReceiverId = receiver.Id
                };

                mails.Add(mail);
            }

            foreach(var mail in mails)
            {
                _unitOfWork.Mails.Add(mail);
            }

            System.Console.WriteLine("Сообщение успешно отправлено!");

            _unitOfWork.Dispose();

            System.Console.ReadLine();
        }
    }
}
