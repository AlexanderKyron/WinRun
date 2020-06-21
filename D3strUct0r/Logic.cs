using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;

namespace WinRun
{
    public class Logic
    {
        // Struct that defines the coordinates of a rectangular shape.
        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int Left;        // x position of upper-left corner
            public int Top;         // y position of upper-left corner
            public int Right;       // x position of lower-right corner
            public int Bottom;      // y position of lower-right corner
        }

        //List of processes running on the PC
        Process[] processes = Process.GetProcesses(".");

        //Get methods from user32.dll for:
        //  1. Setting window positions
        //  2. Getting a given window's RECT
        //  3. Getting the current Foreground window

        [DllImport("user32.dll", EntryPoint = "SetWindowPos")]
        public static extern IntPtr SetWindowPos(IntPtr hWnd, 
            int hWndInsertAfter, int x, int Y, int cx, int cy, int wFlags);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool GetWindowRect(HandleRef hWnd, out RECT lpRect);

        [DllImport("user32.dll", EntryPoint = "GetForegroundWindow")]
        public static extern IntPtr GetForegroundWindow();

        Rectangle rectangle = new Rectangle();
        Rectangle winRec;

        //Singleton logic!
        public static Logic instance;
        public static Logic getSingleton()
        {
            if(instance == null)
            {
                instance = new Logic();
            }
            return instance;
        }

        //Moves the foreground window by the mouse velocity
        public void Move(int[] vel)
        {
            //Useful values
            const short SWP_NOSIZE = 1;
            const short SWP_NOZORDER = 0X4;
            
                //Get the pointer/windowhandle to the foreground window
                IntPtr handle = GetForegroundWindow();
                //If there is an active window,
                if (handle != IntPtr.Zero)
                {
                    //Do a collision check between the mouse and the handle
                    if (CollisionCheck(handle))
                        {
                            //Get the window position
                            Point currentWindowPos = winRec.Location;
                            //Move the window away from the mouse at speed
                            SetWindowPos(handle, 0, currentWindowPos.X +
                                vel[0],currentWindowPos.Y + vel[1], 0, 0, 
                                SWP_NOZORDER | SWP_NOSIZE);
                        }
                }
        }

        //Check for collision/overlap between the mouse & a given window
        public bool CollisionCheck(IntPtr handle)
        {
            //Hold the window rectangle points in a variable called rct
            RECT rct;
            if(!GetWindowRect(new HandleRef(this, handle), out rct)) {
                return false;
            }

            //Use a rectangle object to store the coordinates (Left, Top)
            //and the size (right-left, bottom-top), giving us a more useful
            //representation that we can use.
            rectangle.X = rct.Left;
            rectangle.Y = rct.Top;
            rectangle.Width = rct.Right - rct.Left;
            rectangle.Height = rct.Bottom - rct.Top;
            winRec = rectangle;

            //Get mousepointer position
            Point win32Mouse = MouseHelper.GetPosition();

            //Check collisions along horizontal axis, if the mouse X coordinate
            //is not contained within the bounds of the left or right coordinates
            //of the window, return false.
            if (win32Mouse.X <= rectangle.X || 
                win32Mouse.X >= (rectangle.X + rectangle.Width))
                return false;

            //Check collisions along vertical axis, if the mouse Y coordinate
            //is not contained within the bounds of the top or bottom coordinates
            //of the window, return false.
            if (win32Mouse.Y <= rectangle.Location.Y || 
                win32Mouse.Y >= (rectangle.Location.Y + rectangle.Height))
                return false;

            //If it has not been ruled out, then return true
            return true;
        }

        //Gets the velocity of the mouse
        public static int[] getMouseVel(ref Point m1)
        {
            //Create an array for velocity. 
            //  vel[0] stores the change in position along the X axis
            //  vel[1] stores the change in position along the Y axis
            int[] vel = new int[2];
            //Get the position
            Point m2 = MouseHelper.GetPosition();
            //Subtract the point passed (taken each tick) from the point just taken
            vel[0] = (m2.X - m1.X) * 2;
            vel[1] = (m2.Y - m1.Y) * 2;
            //make it so that the old point is now the most recent point
            m1 = m2;
            //Write the velocity to the console
            Console.WriteLine("" + vel[0] + ", " + vel[1]);
            //return the mouse velocity
            return vel;
        }

       
    }
    
}
