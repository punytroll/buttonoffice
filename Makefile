default: all

BUTTON_OFFICE_SOURCES = \
	ButtonOffice/Application.cs \
	ButtonOffice/DrawingBoard.cs \
	ButtonOffice/EntityPrototype.cs \
	ButtonOffice/FloatingText.cs \
	ButtonOffice/MainWindow.cs \
	ButtonOffice/MainWindow.designer.cs

BUTTON_OFFICE_RESOURCES = \
	ButtonOffice/MainWindow.resources

COMMON_SOURCES = \
	Common/Extensions.cs \
	Common/Pair.cs \
	Common/ReferencePriorityQueueByList.cs

GAME_SOURCES = \
	Game/Data/Data.cs \
	Game/Goals/FindPathToLocation.cs \
	Game/Goals/UseStairs.cs \
	Game/Goals/WalkOnSameFloor.cs \
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
	Game/AssertMessages.cs \
	Game/Bathroom.cs \
	Game/Building.cs \
	Game/Cat.cs \
	Game/Computer.cs \
	Game/Desk.cs \
	Game/Enumerations.cs \
	Game/Game.cs \
	Game/Goal.cs \
	Game/Goals.cs \
	Game/ITTech.cs \
	Game/Janitor.cs \
	Game/Lamp.cs \
	Game/Mind.cs \
	Game/Office.cs \
	Game/PathEdge.cs \
	Game/Person.cs \
	Game/RandomNumberGenerator.cs \
	Game/Stairs.cs \
	Game/Vector2.cs \
	Game/Worker.cs

SOURCES = \
	$(BUTTON_OFFICE_SOURCES) \
	$(COMMON_SOURCES) \
	$(GAME_SOURCES)

RESOURCES = \
	$(BUTTON_OFFICE_RESOURCES)

all: buttonoffice.mono

buttonoffice.mono: $(SOURCES) $(RESOURCES)
	mcs $(filter %.cs, $^) -out:$@ -debug -d:DEBUG -reference:System.Drawing -reference:System.Windows.Forms $(foreach resources, $(filter %.resources, $^), -resource:$(resources))

%.resources: %.resx
	resgen $<
