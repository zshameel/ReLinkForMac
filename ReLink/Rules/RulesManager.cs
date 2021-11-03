using System;
using System.Collections.Generic;

namespace ReLink {

    public class RulesManager {
        internal List<Rule> Rules { get; private set; }

        public RulesManager() {
            InitRules();
        }

        private void InitRules() {
            Rules = new List<Rule>();
            if (BrowserSettings.Rules != null) {
                foreach (Rule rule in BrowserSettings.Rules) {
                    if (rule != null) {
                        Rules.Add(rule);
                    }
                }
            }

            Rules.Sort();
        }

    }

}
