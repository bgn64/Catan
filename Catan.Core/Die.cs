using System;

namespace Catan.Core;

public class Die
{
	Random _random;

	public Die()
	{
		_random = new Random();
		Value = 1;
	}

	public Die(Die die)
	{
		_random = new Random();
		Value = die.Value;
	}

	public int Value { get; private set; }

	public void Roll()
	{
		Value = _random.Next(1, 7);	
	}
}
