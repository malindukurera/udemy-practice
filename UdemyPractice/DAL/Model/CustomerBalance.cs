﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DAL.Model
{
    public class CustomerBalance
    {
        public long CustomerBalanceId { get; set; }
        public string Email { get; set; }
        public decimal Balance { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}
