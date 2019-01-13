using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace TryNativeWindow
{
    public class MyNativeWindow : NativeWindow
    {
        private const int WM_GETDLGCODE = 0x087;
        private const int WM_KEYDOWN = 0x0100;
        private const int WM_KEYUP = 0x0101;
        private const int WM_CHAR = 0x0102;
        private const int DLGC_WANTALLCHAR = 0x04;
        private const int DLGC_WANTCHARS = 0x80;
        private IntPtr hwnd;
        private string chars = "";

        public MyNativeWindow(IntPtr hwnd) :
            base()
        {
            this.hwnd = hwnd;
        }

        public void Attach()
        {
            AssignHandle(hwnd);
        }

        public void Detach()
        {
            ReleaseHandle();
        }

        public string Chars
        {
            get
            {
                string result = chars;
                chars = "";
                return result;
            }
        }

        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case WM_GETDLGCODE:
                    {
                        m.Result = new IntPtr(DLGC_WANTALLCHAR);
                        return;
                    }
                case WM_KEYDOWN:
                    {
                        //Debug.Print("keydown: {0}", m.WParam.ToInt32());
                    }
                    break;
                case WM_KEYUP:
                    {
                        //Debug.Print("keyup: {0}", m.WParam.ToInt32());
                    }
                    break;
                case WM_CHAR:
                    {
                        char c = (char)m.WParam.ToInt32();
                        if (char.IsLetterOrDigit(c))
                        {
                            chars += c;
                        }
                        else if (char.IsControl(c) && c == '\b')
                        {
                            chars += c;
                        }
                        //Debug.Print("char: {0}", chars);
                    }
                    break;
            }
            base.WndProc(ref m);
        }
    }
}
