namespace ButtonOffice
{
    internal class Worker : ButtonOffice.Person
    {
        public Worker()
        {
            _ArrivesAtMinuteOfDay = ButtonOffice.RandomNumberGenerator.GetUInt32(ButtonOffice.Data.WorkerStartMinute, 300) % 1440;
            _BackgroundColor = ButtonOffice.Data.WorkerBackgroundColor;
            _BorderColor = ButtonOffice.Data.WorkerBorderColor;
            _Wage = ButtonOffice.Data.WorkerWage;
            _WorkMinutes = ButtonOffice.Data.WorkerWorkMinutes;
            _Mind.SetRootGoal(new ButtonOffice.Goals.WorkerThink());
        }
    }
}
