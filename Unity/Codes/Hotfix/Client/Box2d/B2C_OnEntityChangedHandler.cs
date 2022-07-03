
using System.Numerics;

namespace ET.Client
{
	[MessageHandler(SceneType.Client)]
	public class B2C_OnEntityChangedHandler : AMHandler<B2C_OnEntityChanged>
	{
		protected override async ETTask Run(Session session, B2C_OnEntityChanged message)
		{
			Unit2D unit = session.DomainScene().CurrentScene().GetComponent<Unit2DComponent>().Get(message.Id);
			if (unit == null)
			{
				Log.Debug("not found unit");
				return;
			}
			unit.GetComponent<CmdCollectorComponent>()?.AddCmd(message.CharacterCMD);
			Vector2 pos = new Vector2(message.X/1000f, message.Y/1000f);
			unit.GetComponent<PositionFollowComponent>().CalcLerp(pos);
			await ETTask.CompletedTask;
		}
	}
}
