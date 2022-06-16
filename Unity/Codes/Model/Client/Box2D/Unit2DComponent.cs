namespace ET
{
	[ChildType(typeof(Unit2D))]
	[ComponentOf(typeof(Scene))]
	public class Unit2DComponent: Entity, IAwake, IDestroy
	{
	}
}