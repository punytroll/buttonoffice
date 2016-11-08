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
            _Mind.SetRootGoal(new ButtonOffice.Goals.WorkerThink());
        }
    }
}
