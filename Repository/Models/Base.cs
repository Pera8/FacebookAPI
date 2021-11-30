using Repository.Interfaces;
using System;

namespace Repository.Models
{
    public abstract class Base : IBaseModel, IDeleteModel
    {
        public int Id { get; set; }
        public DateTime CreateOn { get; set; } = DateTime.Now;
        public DateTime? ModifiedOn { get; set; }
        public bool IsDeleted { get; set; }
    }
}
