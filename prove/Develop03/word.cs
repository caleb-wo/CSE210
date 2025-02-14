class Word 
{
    private string word;
    private string hiddenWord;
    private bool isHidden;
    // CONSTRUCTORS
    public Word(string w) 
    {
        this.word = w;
        this.hiddenWord = new string('_' ,w.Length);
        this.isHidden = false;

    }
    // GETTERS
    public string getWord()
    {
        return this.word;
    }
    public string getHiddenWord()
    {
        return this.hiddenWord;
    }
    public bool getHidden() 
    {
        return this.isHidden;
    }
    // METHODS
    public void hideWord()
    {
        this.isHidden = true;
    }
}