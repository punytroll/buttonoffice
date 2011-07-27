namespace ButtonOffice
{
    internal enum ActionState
    {
        Arriving,
        AtHome,
        Cleaning,
        GoingToClean,
        GoingToDesk,
        GoingToRepair,
        Leaving,
        New,
        PickTrash,
        PushingButton,
        Repairing,
        Stay,
        WaitingForBrokenThings,
        WaitingToGoHome,
        WalkLeft,
        WalkRight,
        Working
    }

    internal enum AnimationState
    {
        Accounting,
        Cleaning,
        Hidden,
        PushingButton,
        Repairing,
        Standing,
        Walking
    }

    internal enum BrokenThing
    {
        FirstComputer,
        SecondComputer,
        ThirdComputer,
        FourthComputer,
        FirstLamp,
        SecondLamp,
        ThirdLamp
    }

    public enum GoalState
    {
        Active,
        Done,
        Inactive,
        Terminated
    }

    internal enum LivingSide
    {
        Left,
        Right
    }

    internal enum Type
    {
        Accountant,
        Cat,
        ITTech,
        Janitor,
        Office,
        Worker
    }
}
