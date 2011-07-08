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
        WaitingForBrokenThings,
        WaitingToGoHome,
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
        Office,
        ITTech,
        Janitor,
        Worker
    }
}