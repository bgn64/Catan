namespace Catan.Core;

public abstract class Phase
{
	internal virtual void OnPhaseStart()
	{
	}

	internal event EventHandler? PhaseComplete;

	protected void OnPhaseComplete(EventArgs e)
	{
		PhaseComplete?.Invoke(this, e);
	}
}
