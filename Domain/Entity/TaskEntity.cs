using Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity
{
    public class TaskEntity : BaseEntity, IEntityId<long>, IAuditable
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public Report Report { get; set; }
        public long ReportId { get; set; }
        public List<TimeSheet> TimeSheets { get; set; }
    }
}
