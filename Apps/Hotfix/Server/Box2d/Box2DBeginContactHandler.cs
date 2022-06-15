namespace ET.Server
{
    [Callback(CallbackType.Box2DBeginContact)]
    public class Box2DBeginContactHandler: IAction<Body2dComponent,Body2dComponent>
    {
        public void Handle(Body2dComponent a, Body2dComponent b)
        {
            Log.Debug("Box2DBeginContactHandler Server:"+a.Id);
        }
    }
    [Callback(CallbackType.Box2DEndContact)]
    public class Box2DEndContactHandler: IAction<Body2dComponent,Body2dComponent>
    {
        public void Handle(Body2dComponent a, Body2dComponent b)
        {
            Log.Debug("Box2DBeginContactHandler Server:"+a.Id);
        }
    }
}