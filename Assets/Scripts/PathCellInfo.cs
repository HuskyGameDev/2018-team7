
public struct PathCellInfo
{
	public bool passable, trigger;
	public int cost;

	public PathCellInfo(bool passable, bool trigger, int cost)
	{
		this.passable = passable;
		this.trigger = trigger;
		this.cost = cost;
	}

	public override string ToString()
	{
		return "Passable: " + passable + ", trigger: " + trigger + ", cost: " + cost;
	}
}
