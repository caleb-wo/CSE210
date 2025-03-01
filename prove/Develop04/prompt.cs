public class Prompt
{
    private string prompt;
    private bool isUsed;

    public Prompt(string prompt)
    {
        this.prompt = prompt;
        this.isUsed = false;
    }

    public string ToString()
    {
        return prompt;
    }

    public void Use()
    {
        this.isUsed = true;
    }

    public void Unuse()
    {
        this.isUsed = false;
    }

    public bool IsUsed()
    {
        return this.isUsed;
    }
}