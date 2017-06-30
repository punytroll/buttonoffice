using System;
using System.Diagnostics;

namespace ButtonOffice.Transportation
{
	internal class Edge
	{
        private CreateUseGoalDelegate _CreateUseGoalFunction;
		internal Node From;
		internal Node To;
		internal Double Weight;

        internal CreateUseGoalDelegate CreateUseGoalFunction
        {
            set
            {
                _CreateUseGoalFunction = value;
            }
        }

		internal Goal CreateUseGoal()
		{
            Debug.Assert(_CreateUseGoalFunction != null);

			return _CreateUseGoalFunction(this);
		}
	}
}
