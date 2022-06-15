namespace ET.Client
{
    [Callback(CallbackType.Box2DBeginContact)]
    public class Box2DBeginContactClientHandler: IAction<Body2dComponent,Body2dComponent>
    {
        public void Handle(Body2dComponent a, Body2dComponent b)
        {
            Log.Debug("Box2DBeginContactHandler client:"+a.Id);
        }
    }
    [Callback(CallbackType.Box2DEndContact)]
    public class Box2DEndContactClientHandler: IAction<Body2dComponent,Body2dComponent>
    {
        public void Handle(Body2dComponent a, Body2dComponent b)
        {
            Log.Debug("Box2DBeginContactHandler client:"+a.Id);
        }
    }
}