namespace ET
{
	// 分发数值监听
	[Event(SceneType.Map)]  // 服务端Map需要分发
	[Event(SceneType.Box2dWorld)]  // 服务端Map需要分发
	[Event(SceneType.Current)] // 客户端CurrentScene也要分发
	public class NumericChangeEvent_NotifyWatcher: AEvent<Unit2D, EventType.NumbericChange>
	{
		protected override async ETTask Run(Unit2D unit, EventType.NumbericChange args)
		{
			NumericWatcherComponent.Instance.Run(unit, args);
			await ETTask.CompletedTask;
		}
	}
}
