using System;

namespace ButtonOffice
{
    internal class TransportationNode
    {
        public delegate Goal CreateUseGoalDelegate(Int32 TargetFloor);

        public CreateUseGoalDelegate GetCreateUseGoalFunction()
        {
            return _CreateUseGoalFunction;
        }

        public Int32 GetTargetFloor()
        {
            return _TargetFloor;
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

        public void SetTargetFloor(Int32 TargetFloor)
        {
            _TargetFloor = TargetFloor;
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
        private Int32 _TargetFloor;
        private Single _X;
        private Single _Y;
    }
}
