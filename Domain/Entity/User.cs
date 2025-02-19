﻿using Domain.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity
{
    public class User : IEntityId<long>, IAuditable
    {
        public long Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public List<Report> Reports { get; set; }
        public DateTime CreateAt { get; set; }
        public long CreatedBy { get; set; }
        public DateTime UpdateAt { get; set; }
        public long UpdatedBy { get; set; }
    }
}
