using System.Collections.Generic;
using System.Threading;
using UnityEngine;

namespace ET.Server
{
	public class ServerEntitySyncComponentAwakeSystem : AwakeSystem<ServerEntitySyncComponent>
	{
		public override void Awake(ServerEntitySyncComponent self)
		{
			self.Awake();
		}
	}

	public class ServerEntitySyncComponentUpdateSystem : UpdateSystem<ServerEntitySyncComponent>
	{
		public override void Update(ServerEntitySyncComponent self)
		{
			self.Update();
		}
	}

	public static class ServerEntitySyncComponentSystem
	{
		public static void Awake(this ServerEntitySyncComponent self)
		{
			self.Interval = 1000 / self.Fps;
		}

		public static void Update(this ServerEntitySyncComponent self)
		{
			if (TimeHelper.ServerNow() - self.Timer > self.Interval)
			{
				self.Timer = TimeHelper.ServerNow();
				var transform = self.GetParent<Unit2D>();
				var lp = transform.LastPosition;
				var p = transform.Position;
				if (Vector3.Distance(lp, p) < 0.01f)
					return;
				transform.LastPosition = p;
				var msg = new B2C_OnEntityChanged();
				msg.Id = self.Id;
				msg.X = (int)(p.x * 100);
				msg.Y = (int)(p.y * 100);
				MessageHelper.BroadcastToAllNotSelf(self.Domain, self.Id, msg);
			}
		}
	}
}