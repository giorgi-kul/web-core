﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebCore.Domain.Models.Admin
{
    public enum PageMode
    {
        None = -1,
        List = 0,
        Create = 1,
        Edit = 2,
        Details = 3
    }
}
