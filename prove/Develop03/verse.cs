using System.Formats.Tar;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;

class Verse {
    public Verse(string[] w)
    {
        foreach (string word in w)
        {
            Word newWord = new Word(word);
            this.verse.Add(newWord);
        }
    }
    private List<Word> verse = new List<Word>();
    private bool allHidden = false;
    private int randomCallCount = 0;
    // SETTER
    public void setHidden(bool tf)
    {
       this.allHidden = tf; 
    }
    // GETTER
    public List<Word> getVerse()
    {
        return this.verse;
    }
    // METHODS
    public void addRandCall()
    {
        this.randomCallCount++;
    }
    public void printVerse()
    {
        StringBuilder sb = new StringBuilder();
        int count = 1;
        foreach (Word w in verse)
        {
            if (sb.Length > 0)
            {
                sb.Append(" ");
                if (!w.getHidden())
                {
                    sb.Append(w.getWord());
                } else
                {
                    sb.Append(w.getHiddenWord());
                }
            } else
            {
                if (!w.getHidden())
                {
                    sb.Append(w.getWord());
                } else
                {
                    sb.Append(w.getHiddenWord());
                }
            }
            if (count % 10 == 0)
            {
                sb.AppendLine();
            }
            count++;
        }
        Console.WriteLine(sb.ToString());
    }
    public void hideRandom()
        {
            Random rand = new Random();
            addRandCall();
            foreach (Word w in getVerse())
            {
                int numCap = -1000;
                if (randomCallCount > 8 && randomCallCount <= 10)
                {
                    numCap = 2;
                } else if (randomCallCount > 5 && randomCallCount <= 8)
                {
                    numCap = 3;
                } else if (randomCallCount > 3 && randomCallCount <= 5)
                {
                    numCap = 4;
                } else if (randomCallCount <= 3)
                {
                    numCap = 5;
                } 
                int randNum = rand.Next(1 ,numCap);
                int keyNum = rand.Next(1 ,numCap);

                if (randNum == keyNum)
                {
                    w.hideWord();
                }
            }
        }
    // STATIC methods
    public static bool checkAllHidden(Verse v)
    {
        foreach(Word w in v.getVerse())
        {
            if (!w.getHidden())
            {
                return false;
            }
        }
        v.setHidden(true);
        return true;    
    }
    public static string[] parseString(string s)
    {
        return s.Split(" ");
    }
}