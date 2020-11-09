using System;
using System.Diagnostics;

namespace ButtonOffice.AI.Goals
{
    internal class Repair : Goal
    {
        protected override void _OnExecute(Game Game, Actor Actor, Double DeltaGameMinutes)
        {
            var ITTech = Actor as ITTech;
            
            Debug.Assert(ITTech != null);
            
            var RepairingTarget = ITTech.GetRepairingTarget();
            
            if(RepairingTarget is Computer)
            {
                ITTech.SetActionFraction(ITTech.GetActionFraction() + Data.ITTechRepairComputerSpeed * DeltaGameMinutes);
            }
            else if(RepairingTarget is Lamp)
            {
                ITTech.SetActionFraction(ITTech.GetActionFraction() + Data.ITTechRepairLampSpeed * DeltaGameMinutes);
            }
            if(ITTech.GetActionFraction() >= 1.0)
            {
                ITTech.SetActionFraction(1.0);
            }
            ITTech.SetAnimationFraction(ITTech.GetAnimationFraction() + Data.ITTechRepairSpeed * DeltaGameMinutes);
            if((ITTech.GetActionFraction() == 1.0) && (ITTech.GetAnimationFraction() >= 1.0))
            {
                if(RepairingTarget is Computer)
                {
                    ((Computer)RepairingTarget).SetRepaired();
                }
                else if(RepairingTarget is Lamp)
                {
                    ((Lamp)RepairingTarget).SetRepaired();
                }
                ITTech.SetRepairingTarget(null);
                ITTech.Desk.TrashLevel += 1.0;
                Finish(Game, Actor);
            }
            while(ITTech.GetAnimationFraction() >= 1.0)
            {
                ITTech.SetAnimationFraction(ITTech.GetAnimationFraction() - 1.0);
            }
        }
    }
}
