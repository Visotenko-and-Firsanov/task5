﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessengerClient.Presentation
{
    public interface IView
    {
        object DataSource { get; set; }
    }
}
