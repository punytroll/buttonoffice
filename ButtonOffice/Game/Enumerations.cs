namespace ButtonOffice
{
    public enum ActionState
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

    public enum AnimationState
    {
        Accounting,
        Cleaning,
        Hidden,
        PushingButton,
        Repairing,
        Standing,
        Walking
    }

    public enum BrokenThing
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

    public enum LivingSide
    {
        Left,
        Right
    }

    public enum Type
    {
        Accountant,
        Bathroom,
        Cat,
        ITTech,
        Janitor,
        Office,
        Worker
    }
}
