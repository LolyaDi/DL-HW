using System;

namespace DL.Models
{
    public abstract class Entity
    {
        public Guid Id { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime? DeletedDate { get; set; }


        public Entity()
        {
            Id = Guid.NewGuid();
            CreationDate = DateTime.Now;
        }
    }
}
