using System.Drawing;
using System.Runtime.InteropServices;

namespace D3strUct0r
{
    //Unnecessary extra class w00t w00t
    static class MouseHelper
    {
        //Import GetCursorPos method from user32.dll
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool GetCursorPos(ref Point pt);

        //Gets mouse position
        public static Point GetPosition()
        {
            Point w32Mouse = new Point();
            GetCursorPos(ref w32Mouse);
            return w32Mouse;
        }
        
    }
    
}
