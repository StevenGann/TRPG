using System.Collections.Generic;
using System.Linq;

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
            Verbs.Add("take");
            Verbs.Add("drop");
            Verbs.Add("examine");

            Adjectives = new List<string>();
        }

        public Command Parse(string _input)
        {
            Command result = new Command();
            result.Text = _input.ToLower();
            char[] delimiterChars = { ' ', ',', '.', ':', '\t' };
            List<string> Words = result.Text.Split(delimiterChars).ToList<string>();

            foreach (string word in Words)
            {
                if (Verbs.Contains(word))
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