﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using TarkovLens.Models.Items;

namespace TarkovLens.Documents.Items
{
    public class ModificationStock : BaseModification
    {
        [JsonPropertyName("foldRetractable")]
        public bool FoldRetractable { get; set; }
    }
}
