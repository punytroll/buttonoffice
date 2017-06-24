using System;

namespace ButtonOffice
{
    internal class PathEdge
    {
        public delegate Goal CreateUseGoalDelegate(Double ToX, Double ToY);

        public CreateUseGoalDelegate CreateUseGoalFunction;
        public Double ToX;
        public Double ToY;
    }
}
