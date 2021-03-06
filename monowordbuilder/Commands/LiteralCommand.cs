using System.Collections.Generic;

[Highlight(RegEx = "^\\s*literal ", Color = "Green")]
public class LiteralCommand : CommandBase
{

	private string _Literal;
	public string Literal {
		get { return _Literal; }
		set { _Literal = value; }
	}

	public override void Execute(Context context)
	{
		context.Tokens.Add(Literal);
	}

	public override void LoadCommand(Project project, System.IO.TextReader reader, string line, ref int lineNumber)
	{
		base.LoadCommand(project, reader, line, ref lineNumber);
		List<string> parts = ProjectSerializer.ReadTokens(line);

		if (parts.Count != 2) {
			project.Warnings.Add(string.Format("Line {0}: The Literal command requires 1 argument.", lineNumber));
		}
		else {
			_Literal = parts[1];
		}
	}

	public override void WriteCommand(System.IO.TextWriter writer)
	{
		writer.WriteLine("Literal {0}", ProjectSerializer.SecureString(_Literal));
	}


	public override void CheckSanity(Project project)
	{
	}
}
