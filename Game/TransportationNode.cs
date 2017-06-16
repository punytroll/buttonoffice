using System;

namespace ButtonOffice
{
    internal class TransportationNode
    {
        public delegate Goal CreateUseGoalDelegate();

        public CreateUseGoalDelegate GetCreateUseGoalFunction()
        {
            return _CreateUseGoalFunction;
        }

        public Single GetX()
        {
            return _X;
        }

        public Single GetY()
        {
            return _Y;
        }

        public void SetCreateUseGoalFunction(CreateUseGoalDelegate CreateUseGoalFunction)
        {
            _CreateUseGoalFunction = CreateUseGoalFunction;
        }

        public void SetX(Single X)
        {
            _X = X;
        }

        public void SetY(Single Y)
        {
            _Y = Y;
        }

        private CreateUseGoalDelegate _CreateUseGoalFunction;
        private Single _X;
        private Single _Y;
    }
}
