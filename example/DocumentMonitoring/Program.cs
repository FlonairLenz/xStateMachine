using System;

namespace DocumentMonitoring
{
    class Program
    {
        static void Main(string[] args)
        {
            var document = new Document(true);
            Console.WriteLine($"Current document state: { document.State.ToString() }");
            document.Publish();
            Console.WriteLine($"Current document state: { document.State.ToString() }");
            document.Change("Change document text");
            Console.WriteLine($"Current document state: { document.State.ToString() }");
            document.Publish();
            Console.WriteLine($"Current document state: { document.State.ToString() }");
            document.Remove();
            Console.WriteLine($"Current document state: { document.State.ToString() }");
            try
            {
                document.Delete();
                Console.WriteLine($"Current document state: { document.State.ToString() }");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}