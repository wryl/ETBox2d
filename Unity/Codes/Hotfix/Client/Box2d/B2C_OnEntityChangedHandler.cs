
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
				return;
			}
			Vector3 pos = new Vector3(message.X/100f, message.Y/100f, 0);
			unit.GetComponent<PositionFollowComponent>().CalcLerp(unit.Position,pos);
			unit.GetComponent<StateMachine2D>().ChangeState((CharacterMovementStates) message.CharacterStates,false);
			await ETTask.CompletedTask;
		}
	}
}
