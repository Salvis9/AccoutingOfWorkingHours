using Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity
{
    public class TimeSheet : IEntityId<long>, IAuditable
    {
        public long Id { get; set; }
        public double Hours { get; set; }
        public string Description { get; set; }
        public TaskEntity TaskEntity { get; set; }
        public long? TaskEntityId { get; set; } //Nullable, чтобы задача могла быть изменена только в разрешенных случаях
        public DateTime CreatedAt { get; set; }
        public long CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public long? UpdatedBy { get; set; }
    }
}
