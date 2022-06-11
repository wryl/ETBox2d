using System.Collections.Generic;
using System.Threading;
using UnityEngine;

namespace ET.Server
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
				var transform = self.GetParent<Unit2D>();
				var lp = transform.LastPosition;
				var p = transform.Position;
				//Log.Debug(self.Id.ToString()+p.ToString());
				if (Vector3.Distance(lp, p) < 0.1f)
					return;
				transform.LastPosition = p;
				var msg = new B2C_OnEntityChanged();
				msg.Id = self.Id;
				msg.X = (int)(p.x * 100);
				msg.Y = (int)(p.y * 100);
				MessageHelper.BroadcastToAll(self.Domain, msg);
			}
		}
	}
}