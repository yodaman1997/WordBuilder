using System;
public class WeightedCommand
{
	private CommandBase _Command;
	public CommandBase Command {
		get { return _Command; }
		set { _Command = value; }
    }
    
	private double _Weight;
	public double Weight {
		get { return _Weight; }
		set { _Weight = value; }
    }
}
