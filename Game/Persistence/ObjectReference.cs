using System;

namespace ButtonOffice.Persistence
{
    internal struct ObjectReference
    {
        public readonly UInt32 Identifier;
        
        public ObjectReference(UInt32 Identifier)
        {
            this.Identifier = Identifier;
        }
    }
}
