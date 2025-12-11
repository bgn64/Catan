namespace Catan.Core;

public abstract class Command
{
	internal event EventHandler? CommandComplete;

	protected virtual void OnCommandComplete(EventArgs e)
	{
		CommandComplete?.Invoke(this, e);
	}

	public abstract bool CanExecute(Game game);

	protected abstract void ExecuteCore(Game game);

	public void Execute(Game game)
	{
		if(!CanExecute(game))
		{
			return;
		}

		ExecuteCore(game);
		OnCommandComplete(EventArgs.Empty);
	}

	public abstract void Accept(ICommandVisitor visitor);
}
