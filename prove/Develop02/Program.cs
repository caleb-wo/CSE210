using System;
using System.IO;
using System.Collections.Generic;

class Program
{
    public static void Main(String[] args)
    {
        //INITIALIZE new manager and begin managing user.
        Manager mainManager = new Manager();
        mainManager.manageUser();    
    }
}