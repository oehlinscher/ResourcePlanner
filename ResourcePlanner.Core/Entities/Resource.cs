﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResourcePlanner.Core.Entities
{
    public class Resource
    {
        public int Id { get; set; }

        public required string Name { get; set; }
    }
}
