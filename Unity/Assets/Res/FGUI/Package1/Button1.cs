/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Package1
{
    public partial class Button1 : GButton
    {
        public Controller button;
        public GGraph n0;
        public GGraph n1;
        public GGraph n2;
        public const string URL = "ui://2k4bu9x0bmwt2";

        public static Button1 CreateInstance()
        {
            return (Button1)UIPackage.CreateObject("Package1", "Button1");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            button = GetControllerAt(0);
            n0 = (GGraph)GetChildAt(0);
            n1 = (GGraph)GetChildAt(1);
            n2 = (GGraph)GetChildAt(2);
        }
    }
}