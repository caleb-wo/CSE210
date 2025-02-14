class Book
{
    List<Scripture> scriptures = new List<Scripture>();

    //SETTERS and GETTERS
    public void addScripture(Scripture s){
        scriptures.Add(s);
    }
    public Scripture getScripture(String verseRef){
        return scriptures.Find(s => s.getRef().getRef() == verseRef);
    }
}