﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RavenDbDotNetCore31APITemplate
{
    public class RavenSettings
    {
        public DatabaseSettings Database { get; set; }
        public class DatabaseSettings
        {
            public string[] Urls { get; set; }
            public string DatabaseName { get; set; }
        }
    }
}
