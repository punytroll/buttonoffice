using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace ButtonOffice.Transportation
{
    internal class Node
    {
        internal Int32 Floor
        {
            get;
            private set;
        }

        internal List<Edge> IncomingEdges
        {
            get;
            private set;
        }

        internal List<Edge> OutgoingEdges
        {
            get;
            private set;
        }

        internal Double X
        {
            get;
            private set;
        }

        internal Node(Double X, Int32 Floor)
        {
            this.Floor = Floor;
            IncomingEdges = new List<Edge>();
            OutgoingEdges = new List<Edge>();
            this.X = X;
        }
    }
}
