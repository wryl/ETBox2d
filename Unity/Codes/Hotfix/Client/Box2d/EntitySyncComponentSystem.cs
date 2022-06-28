

using System.Numerics;

namespace ET.Client
{
	public class EntitySyncComponentAwakeSystem : AwakeSystem<EntitySyncComponent>
	{
		public override void Awake(EntitySyncComponent self)
		{
			self.Awake();
		}
	}

	public class EntitySyncComponentUpdateSystem : UpdateSystem<EntitySyncComponent>
	{
		public override void Update(EntitySyncComponent self)
		{
			self.Update();
		}
	}

	public static class EntitySyncComponentSystem
	{
		public static void Awake(this EntitySyncComponent self)
		{
			self.Interval = 1000 / self.Fps;
		}

		public static void Update(this EntitySyncComponent self)
		{
			if (TimeHelper.ServerNow() - self.Timer > self.Interval)
			{
				self.Timer = TimeHelper.ServerNow();
				var unit = self.GetParent<Unit2D>();
				var lp = unit.LastPosition;
				var p = unit.Position;
				if (Vector3.Distance(lp, p) < 0.01f)
					return;
				unit.LastPosition = p;
				var msg = new C2B_OnSelfEntityChanged();
				msg.Id = self.Id;
				msg.X = (int)(p.X * 100);
				msg.Y = (int)(p.Y * 100);
				msg.AngleY = (int) unit.Angle * 100;
				unit.ClientScene().GetComponent<SessionComponent>().Session.Send(msg);
			}
		}
	}
}