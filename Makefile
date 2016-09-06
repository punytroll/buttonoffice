default: all

BUTTON_OFFICE_SOURCES = \
	ButtonOffice/Application.cs \
	ButtonOffice/DrawingBoard.cs \
	ButtonOffice/EntityPrototype.cs \
	ButtonOffice/FloatingText.cs \
	ButtonOffice/MainWindow.cs

COMMON_SOURCES = \
	Common/Extensions.cs \
	Common/Pair.cs

GAME_SOURCES = \
	Game/Data/Data.cs \
	Game/Persistence/GameLoader.cs \
	Game/Persistence/GameSaver.cs \
	Game/Persistence/PersistentObject.cs \
	Game/Accountant.cs \
	Game/AssertMessages.cs \
	Game/Bathroom.cs \
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
	Game/Person.cs \
	Game/RandomNumberGenerator.cs \
	Game/Worker.cs

SOURCES = \
	$(BUTTON_OFFICE_SOURCES) \
	$(COMMON_SOURCES) \
	$(GAME_SOURCES)

all: buttonoffice.mono

buttonoffice.mono: $(SOURCES)
	dmcs $^ -out:$@ -debug -d:DEBUG -reference:System.Drawing,System.Windows.Forms
