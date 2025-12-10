namespace Catan.Core;

public class InitialRoll : IEquatable<InitialRoll>, IComparable<InitialRoll>
{
	List<Die> _rolls;

	public InitialRoll()
	{
		_rolls = new List<Die>();
	}

	public IEnumerable<Die> Rolls => _rolls;

	public void AddRoll(Die roll)
	{
		_rolls.Add(roll);
	}

	public bool Equals(InitialRoll? other)
	{
		if (other == null)
		{
			return false;
		}

		List<Die> otherRolls = other.Rolls.ToList();

		if (_rolls.Count != otherRolls.Count)
		{
			return false;
		}

		for (int i = 0; i < _rolls.Count; i++)
		{
			if (_rolls[i].Value != otherRolls[i].Value)
			{
				return false;
			}
		}

		return true;
	}

	public override bool Equals(object? obj) => Equals(obj as InitialRoll);

	public override int GetHashCode()
	{
		var hash = new HashCode();

		for (int i = 0; i < _rolls.Count; i++)
		{
			hash.Add(_rolls[i].Value);
		}

		return hash.ToHashCode();
	}

	public static bool operator ==(InitialRoll? initialRoll1, InitialRoll? initialRoll2)
	{
		if (initialRoll1 is null)
		{
			return initialRoll2 is null;
		}

		return initialRoll1.Equals(initialRoll2);
	}

	public static bool operator !=(InitialRoll? initialRoll1, InitialRoll? initialRoll2)
	{
		if (initialRoll1 is null)
		{
			return initialRoll2 is not null;
		}

		return !initialRoll1.Equals(initialRoll2);
	}

	public int CompareTo(InitialRoll? other)
	{
		if(other == null)
		{
			return 1;
		}

		List<Die> otherRolls = other.Rolls.ToList(); 
		int i = 0;

		while (i < _rolls.Count && i < otherRolls.Count)
		{
			Die thisRoll = _rolls[i];
			Die otherRoll = otherRolls[i];

			if (_rolls[i].Value > otherRolls[i].Value)
			{
				return 1;
			}
			else if (otherRolls[i].Value > _rolls[i].Value)
			{
				return -1;
			}

			i++;
		}

		if (_rolls.Count > otherRolls.Count)
		{
			return 1;
		}
		else if (otherRolls.Count > _rolls.Count)
		{
			return -1;
		}

		return 0;
	}

	public static bool operator >  (InitialRoll operand1, InitialRoll operand2)
	{
		return operand1.CompareTo(operand2) > 0;
	}
	
	public static bool operator <  (InitialRoll operand1, InitialRoll operand2)
	{
		return operand1.CompareTo(operand2) < 0;
	}

	public static bool operator >=  (InitialRoll operand1, InitialRoll operand2)
	{
		return operand1.CompareTo(operand2) >= 0;
	}
	
	public static bool operator <=  (InitialRoll operand1, InitialRoll operand2)
	{
		return operand1.CompareTo(operand2) <= 0;
	}
}
