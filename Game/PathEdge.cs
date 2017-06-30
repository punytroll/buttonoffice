using System;

namespace ButtonOffice
{
    internal class PathEdge
    {
        public delegate Goal CreateUseGoalDelegate();

        public CreateUseGoalDelegate CreateUseGoalFunction;
    }
}
