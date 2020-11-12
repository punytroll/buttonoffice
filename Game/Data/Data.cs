using System;
using System.Drawing;

namespace ButtonOffice
{
    public class Data
    {
        public static readonly Color AccountantBackgroundColor = Color.FromArgb(255, 220, 220, 120);
        public static readonly UInt64 AccountantBonusPromille = 50ul;
        public static readonly Color AccountantBorderColor = Color.FromArgb(255, 0, 0, 0);
        public static readonly UInt64 AccountantHireCost = 45000ul;
        public static readonly UInt32 AccountantStartMinute = 500u;
        public static readonly UInt64 AccountantWage = 1500ul;
        public static readonly UInt64 AccountantWorkMinutes = 600ul;
        public static readonly Double AccountantWorkSpeed = 0.15;
        public static readonly Color BackgroundColor = Color.FromArgb(255, 135, 205, 235);
        public static readonly Color BathroomBackgroundColor = Color.FromArgb(255, 210, 240, 255);
        public static readonly Single BathroomBlockHeight = 1.0f;
        public static readonly Single BathroomBlockWidth = 10.0f;
        public static readonly Color BathroomBorderColor = Color.FromArgb(255, 0, 0, 0);
        public static readonly UInt64 BathroomBuildCost = 250000ul;
        public static readonly Double BlockHeight = 50.0;
        public static readonly Double BlockWidth = 20.0;
        public static readonly Color BuildingBackgroundColor = Color.FromArgb(255, 220, 220, 230);
        public static readonly Color BuildingBorderColor = Color.FromArgb(255, 0, 0, 0);
        public static readonly Color CatBackgroundColor = Color.FromArgb(255, 0, 0, 0);
        public static readonly Color CatBorderColor = Color.FromArgb(255, 0, 0, 0);
        public static readonly Single CatHeight = 0.3f;
        public static readonly Double CatWalkSpeed = 0.5;
        public static readonly Single CatWidth = 1.0f;
        public static readonly Color ComputerBackgroundColor = Color.FromArgb(255, 245, 245, 220);
        public static readonly Color ComputerBorderColor = Color.FromArgb(255, 0, 0, 0);
        public static readonly Single ComputerHeight = 0.2f;
        public static readonly Single ComputerWidth = 1.2f;
        public static readonly Color DeskBackgroundColor = Color.FromArgb(255, 127, 48, 14);
        public static readonly Single DeskFourX = 11.825f;
        public static readonly Single DeskHeight = 0.3f;
        public static readonly Single DeskOneX = 0.575f;
        public static readonly Single DeskThreeX = 8.075f;
        public static readonly Single DeskTwoX = 4.325f;
        public static readonly Single DeskWidth = 2.6f;
        public static readonly Color EarnMoneyFloatingTextColor = Color.FromArgb(220, 20, 220, 20);
        public static readonly Double FloatingTextSpeed = 30.0;
        public static readonly Single GameMinutesPerSecond = 5;
        public static readonly Color GroundColor = Color.FromArgb(255, 64, 52, 18);
        public static readonly Color ITTechBackgroundColor = Color.FromArgb(255, 255, 120, 255);
        public static readonly Color ITTechBorderColor = Color.FromArgb(255, 0, 0, 0);
        public static readonly UInt64 ITTechHireCost = 60000ul;
        public static readonly Double ITTechRepairSpeed = 0.2;
        public static readonly Double ITTechRepairComputerSpeed = 0.06;
        public static readonly Double ITTechRepairLampSpeed = 0.1;
        public static readonly UInt32 ITTechStartMinute = 540u;
        public static readonly UInt64 ITTechWage = 2000ul;
        public static readonly UInt32 ITTechWorkMinutes = 540u;
        public static readonly Color JanitorBackgroundColor = Color.FromArgb(255, 120, 120, 255);
        public static readonly Color JanitorBorderColor = Color.FromArgb(255, 0, 0, 0);
        public static readonly Double JanitorCleanAmount = 2.0;
        public static readonly Double JanitorCleanSpeed = 0.5;
        public static readonly UInt64 JanitorHireCost = 30000ul;
        public static readonly UInt32 JanitorStartMinute = 1200u;
        public static readonly UInt64 JanitorWage = 1000ul;
        public static readonly UInt32 JanitorWorkMinutes = 600u;
        public static readonly Single LampHeight = 0.1f;
        public static readonly Single LampOneX = 1.0f;
        public static readonly Single LampThreeX = 11.0f;
        public static readonly Single LampTwoX = 6.0f;
        public static readonly Single LampWidth = 3.0f;
        public static readonly Double MeanMinutesToBrokenComputer = 500.0f;
        public static readonly Double MeanMinutesToBrokenLamp = 1500.0f;
        public static readonly UInt64 NewGameCents = 5000000ul;
        public static readonly Int32 NewGameHighestFloor = 136;
        public static readonly Int32 NewGameLowestFloor = 0;
        public static readonly Double NewGameMinutes = 480.0;
        public static readonly Int32 NewGameLeftBorder = -66;
        public static readonly Int32 NewGameRightBorder = 66;
        public static readonly Color OfficeBackgroundColor = Color.FromArgb(255, 255, 255, 255);
        public static readonly Single OfficeBlockHeight = 1.0f;
        public static readonly Single OfficeBlockWidth = 15.0f;
        public static readonly Color OfficeBorderColor = Color.FromArgb(255, 0, 0, 0);
        public static readonly UInt64 OfficeBuildCost = 250000ul;
        public static readonly Double PersonHeightMean = 0.75;
        public static readonly Double PersonHeightSpread = 0.3;
        public static readonly Single PersonSpeed = 2.0f;
        public static readonly Single PersonTagHeight = 0.15f;
        public static readonly Single PersonTagWidth = 1.0f;
        public static readonly Double PersonWidthMean = 1.6;
        public static readonly Double PersonWidthSpread = 0.5;
        public static readonly String SaveGameFileVersion = "15";
        public static readonly Color SpendMoneyFloatingTextColor = Color.FromArgb(220, 220, 20, 20);
        public static readonly Color StairsBackgroundColor = Color.FromArgb(180, 192, 160, 160);
        public static readonly Single StairsBlockHeight = 2.0f;
        public static readonly Single StairsBlockWidth = 5.0f;
        public static readonly Color StairsBorderColor = Color.FromArgb(180, 0, 0, 0);
        public static readonly UInt64 StairsBuildCost = 60000ul;
        public static readonly UInt64 StairsAddFloorCost = 40000ul;
        public static readonly UInt64 StairsRemoveFloorCost = 5000ul;
        public static readonly Double StairsSpeed = 0.25;
        public static readonly Double StairsWeightDownwards = 3.0;
        public static readonly Double StairsWeightUpwards = 4.0;
        public static readonly Color WorkerBackgroundColor = Color.FromArgb(255, 120, 120, 120);
        public static readonly Color WorkerBorderColor = Color.FromArgb(255, 0, 0, 0);
        public static readonly UInt64 WorkerHireCost = 30000ul;
        public static readonly UInt32 WorkerStartMinute = 540u;
        public static readonly UInt64 WorkerWage = 1000ul;
        public static readonly UInt32 WorkerWorkMinutes = 540u;
        public static readonly Double WorkerWorkSpeed = 0.22;
    }
}
