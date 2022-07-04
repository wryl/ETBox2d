/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using FairyGUI.Utils;

namespace Package1
{
    public partial class Component1 : GComponent
    {
        public GTextField n0;
        public Button1 n1;
        public const string URL = "ui://2k4bu9x0tz250";

        public static Component1 CreateInstance()
        {
            return (Component1)UIPackage.CreateObject("Package1", "Component1");
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            n0 = (GTextField)GetChildAt(0);
            n1 = (Button1)GetChildAt(1);
        }
    }
}