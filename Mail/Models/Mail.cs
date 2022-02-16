﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mail.Models
{
    public class Mail
    {
        public int Id { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string Recipient { get; set; }
        public DateTime DateTime { get; set; }
        public string Result { get; set; }
        public string FailedMessange { get; set; }
    }
}

