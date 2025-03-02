using Domain.Interface;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity
{
    public class TimeSheet : BaseEntity, IEntityId<long>, IAuditable
    {
        public long Id { get; set; }
        public double Hours { get; set; }
        public string Description { get; set; }
        public User User { get; set; }
        public long UserId { get; set; }
        public TaskEntity TaskEntity { get; set; }
        public long TaskEntityId { get; set; }
    }
}
