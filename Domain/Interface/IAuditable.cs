using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interface
{
    public interface IAuditable
    {
        public DateTime CreateAt { get; set; }
        public long CreatedBy { get; set; }
        public DateTime UpdateAt { get; set; }
        public long UpdatedBy { get; set; }
    }
}
