﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer
{
    public class feadbackResponseModel
    {
        public int FeedbackId { get; set; }
        public int UserId { get; set; }
        public decimal Rating { get; set; }
        public string Comment { get; set; }
        public int BookId { get; set; }
        public string FullName { get; set; }
    }
}
