class Scripture
{
    public Scripture(string lineVerse ,string refName)
    {
        this.reference = new Reference(refName);
        string[] strWords = Verse.parseString(lineVerse);
        Verse v1 = new Verse(strWords);
        verses.Add(v1);
    }
    List<Verse> verses = new List<Verse>();
    Reference reference;
    bool doForceQuit = false;
    bool allHidden = false;
    // GETTER
    public Reference getRef()
    {
        return this.reference;
    }
    public bool getForceQuit()
    {
        return this.doForceQuit;
    }
    // SETTER
    public void forceQuit()
    {
        this.doForceQuit = true;
    }
    // METHODS
    public bool checkHidden()
    {
        return Verse.checkAllHidden(verses[0]) ? true : false;
    }
    public void printVerses()
    {
        foreach (Verse v in this.verses)
        {
            v.printVerse();
        }
    }
    public void randomizeScripture()
    {
        foreach (Verse v in this.verses)
        {
            v.hideRandom();
        }
    }
}