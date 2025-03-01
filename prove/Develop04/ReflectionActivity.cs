public class ReflectionActivity : RestActivity
{
    Random random = new Random();
    List<Prompt> questions = new List<Prompt>
    {
         new Prompt("Think of a time when you stood up for someone else.")
        ,new Prompt("Think of a time when you did something really difficult.")
        ,new Prompt("Think of a time when you helped someone in need.")
        ,new Prompt("Think of a time when you did something truly selfless.")
    };

    private List<Prompt> reflectQuestions = new List<Prompt>
    {
         new Prompt("Why was this experience meaningful to you?")
        ,new Prompt("Have you ever done anything like this before?")
        ,new Prompt("How did you get started?")
        ,new Prompt("How did you feel when it was complete?")
        ,new Prompt("What made this time different than other times when you were not as successful?")
        ,new Prompt("What is your favorite thing about this experience?")
        ,new Prompt("What could you learn from this experience that applies to other situations?")
        ,new Prompt("What did you learn about yourself through this experience?")
        ,new Prompt("How can you keep this experience in mind in the future?")
    };
    public ReflectionActivity(int duration)
        : base("Reflection Activity", 
            "This activity will help you relax by walking your through breathing in and out slowly. Clear your mind and focus on your breathing.",
            duration) {}

    public string getQuestion(int idx)
    {
        questions[idx].Use();
        return questions[idx].ToString();
    }
    
     public string getReflectQuestion(int idx)
     {
         if (reflectQuestions[idx].IsUsed())
         {
             return getReflectQuestion(random.Next(reflectQuestions.Count));
         }
         else
         {
             reflectQuestions[idx].Use();
             return reflectQuestions[idx].ToString();
         }
     }   
     
      public bool allUsed()
      {
          return reflectQuestions.All(rQ => rQ.IsUsed());
      }

      public void doActivity(int time)
      {
          timer.Restart();
          Console.WriteLine("==========WELCOME TO YOUR REFLECTION ACTIVITY==========");
          RestActivity.timer.Start(); 

          while (RestActivity.timer.Elapsed.TotalSeconds < time)
          {
              Console.WriteLine("Please think deeply on the following prompt:");
              Console.WriteLine(getQuestion(random.Next(questions.Count)));
              Console.WriteLine("Please take time to reflect...");
              freeze(time / 3); 

              Console.WriteLine("With that in mind, please reflect on the following...");
        
              while (!allUsed() && RestActivity.timer.Elapsed.TotalSeconds < time)
              {
                  Console.WriteLine(getReflectQuestion(random.Next(reflectQuestions.Count)));
                  freeze(time / 6);

                  if (RestActivity.timer.Elapsed.TotalSeconds >= time)
                      break;
              }
          }
          Console.WriteLine("Great job!");
      }

}