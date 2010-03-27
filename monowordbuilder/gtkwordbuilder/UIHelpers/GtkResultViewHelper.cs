//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.4200
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using Whee.WordBuilder.Model;
using Whee.WordBuilder.UIHelpers;
using Gtk;

namespace Whee.WordBuilder.UIHelpers
{
	public class GtkResultViewHelper : IResultViewHelper
	{
		public GtkResultViewHelper(TreeView resultsTreeView)
		{
			m_ResultsTreeView = resultsTreeView;
			m_ResultsTreeView.Selection.Changed += HandleM_ResultsTreeViewSelectionChanged;
			m_ResultsTreeView.Selection.Mode = SelectionMode.Multiple;
			
			TreeViewColumn wordCol = new TreeViewColumn();
			wordCol.Title = "Word";
			
			CellRendererText wordColRenderer = new CellRendererText();
			wordCol.PackStart(wordColRenderer, true);
		
			wordCol.AddAttribute(wordColRenderer, "text", 1);
		
			m_ResultsTreeView.AppendColumn(wordCol);
		
			m_Results = new ListStore(typeof(Context), typeof(string));
			m_ResultsTreeView.Model = m_Results;
		}

		void HandleM_ResultsTreeViewSelectionChanged(object sender, EventArgs e)
		{
			if (SelectionChanged != null)
			{
				SelectionChanged(this, EventArgs.Empty);
			}			
		}

		private TreeView m_ResultsTreeView;
		private ListStore m_Results;
		
		#region IResultViewHelper implementation
		public void Clear ()
		{
			m_Results.Clear();
		}
		
		
		public void AddItem (Context context)
		{
			m_Results.AppendValues(context, context.ToString());			
		}
				
		public System.Collections.Generic.List<Context> GetSelectedItems ()
		{
			List<Context> result = new List<Context>();
			TreePath[] selected = m_ResultsTreeView.Selection.GetSelectedRows();
			TreeIter iter;
			
			foreach (TreePath sel in selected)
			{
				m_ResultsTreeView.Model.GetIter(out iter, sel);
				Context context = (Context)m_ResultsTreeView.Model.GetValue(iter, 0);
				
				result.Add(context);
			}
			
			return result;
		}
		
		public System.Collections.Generic.List<Context> GetAllItems ()
		{
			List<Context> result = new List<Context>();
			
			TreeIter iter;
			for (m_Results.GetIterFirst(out iter); m_Results.IterNext(ref iter);)
			{
				Context context = (Context)m_ResultsTreeView.Model.GetValue(iter, 0);
				
				result.Add(context);
			}
			
			return result;
		}

		public event EventHandler<EventArgs> SelectionChanged;		
		#endregion
	}
}
