﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interface
{
    public interface IEntityId<T> where T : struct
    {
        public T Id { get; set; }
    }
}
