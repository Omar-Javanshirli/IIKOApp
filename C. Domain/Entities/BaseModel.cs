﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C._Domain.Entities
{
    public class BaseModel
    {
        public int Id { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UodateDate { get; set; }
        public bool DeleteStatus { get; set; }
    }
}
