namespace Catan.Core;

public abstract class ParentPhase<TSubphase> : Phase where TSubphase : Phase
{
	private TSubphase? _currentPhase;
	public TSubphase? CurrentPhase
	{
		get => _currentPhase;
		protected set
		{
			_currentPhase = value;
			if (_currentPhase != null)
			{
				_currentPhase.OnPhaseStart();
			}
		}
	}

	internal override IEnumerable<Command> GetValidCommands()
	{
		return _currentPhase?.GetValidCommands() ?? new List<Command>();
	}
}
