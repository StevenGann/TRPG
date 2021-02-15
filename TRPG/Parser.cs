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
            Verbs = new List<string>
            {
                "take",
                "drop",
                "examine",
                "attack"
            };

            //Eventually, Adjectives will be assembled by scanning all loaded Items.
            Adjectives = new List<string>
            {
                "sharp",
                "dull",
                "deadly",
                "blunt",
                "rusty",
                "glowing",
                "shiny",
                "polished",
                "dirty",
                "extreme",
                "small",
                "large",
                "tiny",
                "giant",
                "slimy",
                "greasy",
                "smelly"
            };
        }

        public Command Parse(string _input)
        {
            Command result = new Command
            {
                Text = _input.ToLower()
            };
            char[] delimiterChars = { ' ', ',', '.', ':', '\t' };
            List<string> Words = result.Text.Split(delimiterChars).ToList<string>();
            List<string> adjectiveStack = new List<string>();
            foreach (string word in Words)
            {
                if (Verbs.Contains(word))
                {
                    Token newToken = new Token(word, 1);
                    if (adjectiveStack.Count > 0)
                    {
                        newToken.Adjectives.AddRange(adjectiveStack);
                    }
                    result.Tokens.Add(newToken);
                    adjectiveStack = new List<string>();
                }
                else if (Adjectives.Contains(word))
                {
                    adjectiveStack.Add(word);
                }
                else
                {
                    Token newToken = new Token(word, 0);
                    if (adjectiveStack.Count > 0)
                    {
                        newToken.Adjectives.AddRange(adjectiveStack);
                    }
                    result.Tokens.Add(newToken);
                    adjectiveStack = new List<string>();
                }
            }

            return result;
        }
    }
}