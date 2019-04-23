using System.Collections.Generic;

namespace DL.Models
{
    public class Receiver: Entity
    {
        public string Fullname { get; set; }

        public string Address { get; set; }

        public ICollection<Mail> Mails { get; set; }
    }
}
