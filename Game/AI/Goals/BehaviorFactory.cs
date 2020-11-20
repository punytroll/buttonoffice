using ButtonOffice.AI.Goals;
using System;
using System.Collections.Generic;

namespace ButtonOffice.AI.BehaviorTrees
{
    internal static class BehaviorFactory
    {
        public static Goal CreateBehavior(String BehaviorName)
        {
            switch(BehaviorName)
            {
            case "AccountantThink":
                {
                    return new Repeat()
                           {
                               Count = null,
                               Behavior = new Sequence()
                                          {
                                              Behaviors = new List<Goal>()
                                                          {
                                                              new PlanNextWorkDay(),
                                                              new WaitUntilTimeToArrive(),
                                                              new EnterFromLivingSide(),
                                                              new SetTravelLocationToOwnDesk(),
                                                              new TravelToMemoryLocation(),
                                                              new SitDownAtOwnDesk(),
                                                              new Accounting(),
                                                              new CollectWage(),
                                                              new StandUpFromOwnDesk(),
                                                              new SetTravelLocationToHome(),
                                                              new TravelToMemoryLocation(),
                                                              new ExitToLivingSide()
                                                          }
                                          }
                           };
                }
            default:
                {
                    throw new ArgumentException();
                }
            }
        }
    }
}
