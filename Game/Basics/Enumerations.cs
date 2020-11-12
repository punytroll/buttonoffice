namespace ButtonOffice
{
    public enum ActionState
    {
        Stay,
        WalkLeft,
        WalkRight
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
    
    public enum GoalState
    {
        Pristine,
        Executing,
        Succeeded,
        Failed,
        Terminated
    }
    
    public enum LivingSide
    {
        Left,
        Right
    }
}
