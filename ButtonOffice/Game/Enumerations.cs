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

    internal enum LivingSide
    {
        Left,
        Right
    }

    internal enum Trash
    {
        FirstTrash,
        SecondTrash,
        ThirdTrash,
        FourthTrash
    }

    internal enum Type
    {
        Cat,
        ITTech,
        Janitor,
        Office,
        Worker
    }
}
