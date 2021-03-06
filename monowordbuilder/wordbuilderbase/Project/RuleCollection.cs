using System;
using System.Collections.Generic;

using Whee.WordBuilder.Helpers;

namespace Whee.WordBuilder.Model
{
	public class RuleCollection : System.Collections.ObjectModel.Collection<Rule>
	{
		public RuleCollection(IRandom random)
		{
			m_Random = random;
		}
		
		private IRandom m_Random;
	    
		public Rule GetRuleByName(string name)
		{
	        List<Rule> rules = new List<Rule>();
	        double total = 0;
	        
	        foreach (Rule r in this) {
	            if (r.Name == name) {
	                rules.Add(r);
	                total += r.Probability;
	            }
	        }
	        
	     	if (rules.Count == 0) {
	            return null;
	        }
	        
	        double selected = m_Random.NextDouble() * total;
	        
	        foreach (Rule r in rules) {
	            selected -= r.Probability;
	            if (selected <= 0) {
					return r;
	            }
	        }
	        
	        return rules.Count > 0 ? rules[rules.Count - 1] : null;
	    }
	}
}