namespace Catan.Core;

public class Player
{
	public Player()
	{
		Id = Guid.NewGuid();
	}

	public Guid Id { get; }
}
