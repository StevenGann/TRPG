using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRPG
{
    public class Parser
    {
        public List<string> Verbs;
        public List<string> Adjectives;

        public Parser()
        {
            //Load dictionaries from file. Or hardcoded will do for now.
            Verbs = new List<string>();
            Verbs.Add("eat");
            Verbs.Add("toss");
            Verbs.Add("kill");
            Verbs.Add("read");
            Verbs.Add("loot");
            Verbs.Add("throw");

            Adjectives = new List<string>();
            Adjectives.Add("dangerous");
            Adjectives.Add("quickly");
            Adjectives.Add("hard");
            Adjectives.Add("slow");
        }

        public Command Parse(string _input)
        {
            Command result = new Command();
            result.Text = _input.ToLower();
            char[] delimiterChars = { ' ', ',', '.', ':', '\t' };
            List<string> Words = result.Text.Split(delimiterChars).ToList<string>();

            foreach (string word in Words)
            {
                if(Verbs.Contains(word))
                {
                    result.Tokens.Add(new Token(word, 1));
                }
                else if (Adjectives.Contains(word))
                {
                    result.Tokens.Add(new Token(word, 2));
                }
                else
                {
                    result.Tokens.Add(new Token(word, 0));
                }
            }

            return result;
        }
    }
}
