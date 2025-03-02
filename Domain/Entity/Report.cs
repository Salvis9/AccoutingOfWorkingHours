using Domain.Entity;
using Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity
{
    public class Report : BaseEntity, IEntityId<long>, IAuditable
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get;set; }
        public bool IsActive { get; set; }
        public User User { get; set; }
        public long UserId { get; set; }
        public List<TaskEntity> TasksEntity { get; set; }
    }
}
