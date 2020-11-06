using ButtonOffice.Goals;

namespace ButtonOffice
{
    internal class Worker : Person
    {
        public Worker()
        {
            _ArrivesAtMinuteOfDay = RandomNumberGenerator.GetUInt32(Data.WorkerStartMinute, 300) % 1440;
            _BackgroundColor = Data.WorkerBackgroundColor;
            _BorderColor = Data.WorkerBorderColor;
            _Wage = Data.WorkerWage;
            _WorkMinutes = Data.WorkerWorkMinutes;
            
            var Mind = new GoalMind();
            
            Mind.SetRootGoal(new WorkerThink());
            _Mind = Mind;
        }
    }
}
