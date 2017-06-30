using System;

namespace ButtonOffice.Transportation
{
	internal class Edge
	{
        public CreateUseGoalDelegate CreateUseGoalFunction;
		public Node From;
		public Node To;
		public Double Weight;

		internal Goal CreateUseGoal()
		{
			return CreateUseGoalFunction(this);
		}
	}
}
