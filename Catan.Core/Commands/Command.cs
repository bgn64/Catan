namespace Catan.Core;

public abstract class Command
{
	internal event EventHandler? CommandComplete;

	protected virtual void OnCommandComplete(EventArgs e)
	{
		CommandComplete?.Invoke(this, e);
	}

	public abstract bool CanExecute(Game game);

	protected abstract bool TryExecuteCore(Game game);

	public bool TryExecute(Game game)
	{
		if (!TryExecuteCore(game))
		{
			return false;
		}


		OnCommandComplete(EventArgs.Empty);

		return true;
	}

	public abstract void Accept(ICommandVisitor visitor);
}
