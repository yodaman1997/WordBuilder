//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.4200
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using NUnit.Framework;
using System;
using NUnit.Mocks;
using System.Collections.Generic;

using Whee.WordBuilder.Helpers;
using Whee.WordBuilder.Model;
using Whee.WordBuilder.Exporters;

namespace test
{


	[TestFixture()]
	public class CsvExporterTest
	{

		private DynamicMock m_FileSystem;

		[Test()]
		public void TestExport()
		{
			m_FileSystem = new DynamicMock(typeof(IFileSystem));

			List<Context> selected = new List<Context>();
			
			Context result = new Context();
			result.Tokens.Add("a");
			result.Tokens.Add("b");
			result.Tokens.Add("c");
			
			Context branch = result.Branch("b1");
			branch.Tokens.Add("d");
			
			selected.Add(result);
			
			IExporter exporter = new CsvExporter();
			
			m_FileSystem.Expect("WriteAllText", @"c:\abc.csv", string.Format(".Word.;b1{0}abc;abcd{0}", Environment.NewLine));
			
			exporter.Export(selected, @"c:\abc.csv", (IFileSystem)m_FileSystem.MockInstance);
			
			m_FileSystem.Verify();
		}
	}
}
