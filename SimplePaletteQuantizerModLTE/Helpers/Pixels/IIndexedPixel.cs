using System;

namespace Erwine.Leonard.T.Toolkit.SimplePaletteQuantizer.Helpers.Pixels
{
    public interface IIndexedPixel
    {
        // index methods
        Byte GetIndex(Int32 offset);
        void SetIndex(Int32 offset, Byte value);
    }
}
