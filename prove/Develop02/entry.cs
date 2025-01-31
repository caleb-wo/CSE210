class Entry
{
    private String date;
    private String prompt;
    private String text;

    //SETTERS
    public void setDate(String date)
    {
        this.date = date;
    }
    public void setPrompt(String question)
    {
        this.prompt = question;
    }
    public void setText(String entry)
    {
        this.text = entry;
    }
    //GETTERS
    public String getDate()
    {
        return this.date;
    }
    public String getPrompt()
    {
        return this.prompt;
    }
    public String getText()
    {
        return this.text;
    }
}