using ButtonOffice.Persistence;
using System;

namespace ButtonOffice
{
    public class Vector2 : IPersistable
    {
        public Double X;
        public Double Y;
        
        public Vector2()
        {
            this.X = 0.0;
            this.Y = 0.0;
        }
        
        public Vector2(Double X, Double Y)
        {
            this.X = X;
            this.Y = Y;
        }
        
        public Double GetDistanceSquared(Vector2 Other)
        {
            var DeltaX = Other.X - X;
            var DeltaY = Other.Y - Y;
            
            return DeltaX * DeltaX + DeltaY * DeltaY;
        }
        
        public void Save(SaveObjectStore ObjectStore)
        {
            ObjectStore.Save("x", X);
            ObjectStore.Save("y", Y);
        }
        
        public void Load(LoadObjectStore ObjectStore)
        {
            X = ObjectStore.LoadDoubleProperty("x");
            X = ObjectStore.LoadDoubleProperty("y");
        }
        
        public static Vector2 operator+(Vector2 One, Vector2 Two)
        {
            return new Vector2(One.X + Two.X, One.Y + Two.Y);
        }
    }
}
