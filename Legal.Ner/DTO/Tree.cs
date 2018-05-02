﻿using System.Collections.Generic;

namespace Legal.Ner.DTO
{
    public class Tree
    {
        public List<NonTerminal> NonTerminals { get; set; }
        public List<Terminal> Terminals { get; set; }
        public List<TreeEdge> TreeEdges { get; set; }
    }
}
