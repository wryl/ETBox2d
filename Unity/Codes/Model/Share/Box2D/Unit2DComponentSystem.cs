namespace ET
{
	[ObjectSystem]
	public class Unit2DComponentAwakeSystem : AwakeSystem<Unit2DComponent>
	{
		public override void Awake(Unit2DComponent self)
		{
		}
	}
	
	[ObjectSystem]
	public class Unit2DComponentDestroySystem : DestroySystem<Unit2DComponent>
	{
		public override void Destroy(Unit2DComponent self)
		{
		}
	}
	
	public static class Unit2DComponentSystem
	{
		public static void Add(this Unit2DComponent self, Unit2D unit)
		{
		}

		public static Unit2D Get(this Unit2DComponent self, long id)
		{
			Unit2D unit = self.GetChild<Unit2D>(id);
			return unit;
		}

		public static void Remove(this Unit2DComponent self, long id)
		{
			Unit2D unit = self.GetChild<Unit2D>(id);
			unit?.Dispose();
		}
	}
}