/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using System.Threading.Tasks;

namespace ET
{
    public class FUI_AttackTextButtonAwakeSystem : AwakeSystem<FUI_AttackTextButton, GObject>
    {
        public override void Awake(FUI_AttackTextButton self, GObject go)
        {
            self.Awake(go);
        }
    }
        
    public sealed class FUI_AttackTextButton : FUI
    {	
        public const string UIPackageName = "Battle";
        public const string UIResName = "AttackTextButton";
        
        /// <summary>
        /// {uiResName}的组件类型(GComponent、GButton、GProcessBar等)，它们都是GObject的子类。
        /// </summary>
        public GButton self;
            
    	public Controller m_button;
    	public GGraph m_n0;
    	public GGraph m_n1;
    	public GGraph m_n2;
    	public GTextField m_title;
    	public const string URL = "ui://2k4bu9x0t32m4";

       
        private static GObject CreateGObject()
        {
            return UIPackage.CreateObject(UIPackageName, UIResName);
        }
    
        private static void CreateGObjectAsync(UIPackage.CreateObjectCallback result)
        {
            UIPackage.CreateObjectAsync(UIPackageName, UIResName, result);
        }
        
       
        public static FUI_AttackTextButton CreateInstance(Entity parent)
        {			
            return parent.AddChild<FUI_AttackTextButton, GObject>(CreateGObject());
        }
        
       
        public static ETTask<FUI_AttackTextButton> CreateInstanceAsync(Entity parent)
        {
            ETTask<FUI_AttackTextButton> tcs = ETTask<FUI_AttackTextButton>.Create(true);
    
            CreateGObjectAsync((go) =>
            {
                tcs.SetResult(parent.AddChild<FUI_AttackTextButton, GObject>(go));
            });
    
            return tcs;
        }
        
       
        /// <summary>
        /// 仅用于go已经实例化情况下的创建（例如另一个组件引用了此组件）
        /// </summary>
        /// <param name="domain"></param>
        /// <param name="go"></param>
        /// <returns></returns>
        public static FUI_AttackTextButton Create(Entity parent, GObject go)
        {
            return parent.AddChild<FUI_AttackTextButton, GObject>(go);
        }
            
       
        /// <summary>
        /// 通过此方法获取的FUI，在Dispose时不会释放GObject，需要自行管理（一般在配合FGUI的Pool机制时使用）。
        /// </summary>
        public static FUI_AttackTextButton GetFormPool(Entity domain, GObject go)
        {
            var fui = go.Get<FUI_AttackTextButton>();
        
            if(fui == null)
            {
                fui = Create(domain, go);
            }
        
            fui.isFromFGUIPool = true;
        
            return fui;
        }
            
        public void Awake(GObject go)
        {
            if(go == null)
            {
                return;
            }
            
            GObject = go;	
            
            if (string.IsNullOrWhiteSpace(Name))
            {
                Name = Id.ToString();
            }
            
            self = (GButton)go;
            
            self.Add(this);
            
            var com = go.asCom;
                
            if(com != null)
            {	
                
    			m_button = com.GetControllerAt(0);
    			m_n0 = (GGraph)com.GetChildAt(0);
    			m_n1 = (GGraph)com.GetChildAt(1);
    			m_n2 = (GGraph)com.GetChildAt(2);
    			m_title = (GTextField)com.GetChildAt(3);
    		}
    	}
           
        public override void Dispose()
        {
            if(IsDisposed)
            {
                return;
            }
            
            base.Dispose();
            
            self.Remove();
            self = null;
            
    		m_button = null;
    		m_n0 = null;
    		m_n1 = null;
    		m_n2 = null;
    		m_title = null;
    	}
    }
}