namespace Catan.Core;

public interface ICommand
{
	public bool CanExecute(Game game);

	public void Execute(Game game);
}
