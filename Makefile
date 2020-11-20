default: all

BUTTON_OFFICE_SOURCES = \
	ButtonOffice/Application.cs \
	ButtonOffice/Canvas.cs \
	ButtonOffice/EntityPrototype.cs \
	ButtonOffice/FloatingText.cs \
	ButtonOffice/MainWindow.cs \
	ButtonOffice/MainWindow.designer.cs

COMMON_SOURCES = \
	Common/Extensions.cs \
	Common/Pair.cs \
	Common/ReferencePriorityQueueByList.cs

GAME_SOURCES = \
	Game/AI/Actor.cs \
	Game/AI/Goals/AccountantThink.cs \
	Game/AI/Goals/Accounting.cs \
	Game/AI/Goals/CatThink.cs \
	Game/AI/Goals/CleanDesk.cs \
	Game/AI/Goals/CleanDesks.cs \
	Game/AI/Goals/Goal.cs \
	Game/AI/Goals/GoHome.cs \
	Game/AI/Goals/GoToOwnDesk.cs \
	Game/AI/Goals/GoToWork.cs \
	Game/AI/Goals/ITTechThink.cs \
	Game/AI/Goals/JanitorThink.cs \
	Game/AI/Goals/Mind.cs \
	Game/AI/Goals/PlanNextWorkDay.cs \
	Game/AI/Goals/PushButton.cs \
	Game/AI/Goals/Repair.cs \
	Game/AI/Goals/StandByForRepairs.cs \
	Game/AI/Goals/TravelToLocation.cs \
	Game/AI/Goals/UseStairs.cs \
	Game/AI/Goals/WalkOnSameFloor.cs \
	Game/AI/Goals/WalkToDesk.cs \
	Game/AI/Goals/WaitUntilTimeToArrive.cs \
	Game/AI/Goals/WorkerThink.cs \
	Game/AI/Memory.cs \
	Game/AI/Mind.cs \
	Game/Basics/Enumerations.cs \
	Game/Basics/Extensions.cs \
	Game/Basics/RandomNumberGenerator.cs \
	Game/Basics/Vector2.cs \
	Game/Data/Data.cs \
	Game/Persistence/GameLoader.cs \
	Game/Persistence/GameLoadException.cs \
	Game/Persistence/GameSaver.cs \
	Game/Persistence/LoadObjectStore.cs \
	Game/Persistence/PersistentObject.cs \
	Game/Persistence/SaveObjectStore.cs \
	Game/Transportation/Edge.cs \
	Game/Transportation/Node.cs \
	Game/Transportation/Transportations.cs \
	Game/Accountant.cs \
	Game/Bathroom.cs \
	Game/Building.cs \
	Game/Cat.cs \
	Game/Computer.cs \
	Game/Desk.cs \
	Game/Game.cs \
	Game/ITTech.cs \
	Game/Janitor.cs \
	Game/Lamp.cs \
	Game/Office.cs \
	Game/Person.cs \
	Game/Stairs.cs \
	Game/Worker.cs

SOURCES = \
	$(BUTTON_OFFICE_SOURCES) \
	$(COMMON_SOURCES) \
	$(GAME_SOURCES)

all: buttonoffice

buttonoffice: $(SOURCES)
	mcs $(filter %.cs, $^) -out:$@ -debug -d:DEBUG -reference:System.Drawing -reference:System.Windows.Forms

clean:
	$(RM) buttonoffice
	$(RM) buttonoffice.mdb
