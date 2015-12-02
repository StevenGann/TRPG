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
            Verbs.Add("attack");

            //Eventually, Adjectives will be assembled by scanning all loaded Items.
            Adjectives = new List<string>();
            Adjectives.Add("sharp");
            Adjectives.Add("dull");
            Adjectives.Add("deadly");
            Adjectives.Add("blunt");
            Adjectives.Add("rusty");
            Adjectives.Add("glowing");
            Adjectives.Add("shiny");
            Adjectives.Add("polished");
            Adjectives.Add("dirty");
            Adjectives.Add("extreme");
            Adjectives.Add("small");
            Adjectives.Add("large");
            Adjectives.Add("tiny");
            Adjectives.Add("giant");
            Adjectives.Add("slimy");
            Adjectives.Add("greasy");
            Adjectives.Add("smelly");
        }

        public Command Parse(string _input)
        {
            Command result = new Command();
            result.Text = _input.ToLower();
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