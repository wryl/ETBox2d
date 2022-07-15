/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;
using System.Threading.Tasks;

namespace ET
{
    public class FUI_BattleComponentAwakeSystem : AwakeSystem<FUI_BattleComponent, GObject>
    {
        public override void Awake(FUI_BattleComponent self, GObject go)
        {
            self.Awake(go);
        }
    }
        
    public sealed class FUI_BattleComponent : FUI
    {	
        public const string UIPackageName = "Battle";
        public const string UIResName = "BattleComponent";
        
        /// <summary>
        /// {uiResName}的组件类型(GComponent、GButton、GProcessBar等)，它们都是GObject的子类。
        /// </summary>
        public GComponent self;
            
    	public GTextField m_n0;
    	public GList m_n2;
    	public FUI_AttackButton m_n3;
    	public const string URL = "ui://2k4bu9x0tz250";

       
        private static GObject CreateGObject()
        {
            return UIPackage.CreateObject(UIPackageName, UIResName);
        }
    
        private static void CreateGObjectAsync(UIPackage.CreateObjectCallback result)
        {
            UIPackage.CreateObjectAsync(UIPackageName, UIResName, result);
        }
        
       
        public static FUI_BattleComponent CreateInstance(Entity parent)
        {			
            return parent.AddChild<FUI_BattleComponent, GObject>(CreateGObject());
        }
        
       
        public static ETTask<FUI_BattleComponent> CreateInstanceAsync(Entity parent)
        {
            ETTask<FUI_BattleComponent> tcs = ETTask<FUI_BattleComponent>.Create(true);
    
            CreateGObjectAsync((go) =>
            {
                tcs.SetResult(parent.AddChild<FUI_BattleComponent, GObject>(go));
            });
    
            return tcs;
        }
        
       
        /// <summary>
        /// 仅用于go已经实例化情况下的创建（例如另一个组件引用了此组件）
        /// </summary>
        /// <param name="domain"></param>
        /// <param name="go"></param>
        /// <returns></returns>
        public static FUI_BattleComponent Create(Entity parent, GObject go)
        {
            return parent.AddChild<FUI_BattleComponent, GObject>(go);
        }
            
       
        /// <summary>
        /// 通过此方法获取的FUI，在Dispose时不会释放GObject，需要自行管理（一般在配合FGUI的Pool机制时使用）。
        /// </summary>
        public static FUI_BattleComponent GetFormPool(Entity domain, GObject go)
        {
            var fui = go.Get<FUI_BattleComponent>();
        
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
            
            self = (GComponent)go;
            
            self.Add(this);
            
            var com = go.asCom;
                
            if(com != null)
            {	
                
    			m_n0 = (GTextField)com.GetChildAt(0);
    			m_n2 = (GList)com.GetChildAt(1);
    			m_n3 = FUI_AttackButton.Create(this, com.GetChildAt(2));
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
            
    		m_n0 = null;
    		m_n2 = null;
    		m_n3.Dispose();
    		m_n3 = null;
    	}
    }
}