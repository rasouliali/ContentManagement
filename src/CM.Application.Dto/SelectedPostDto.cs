﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CM.Application.Dto
{
    public class SelectedPostDto
    {
        public int UserId { get; set; }
        public int PostId { get; set; }
    }
}
