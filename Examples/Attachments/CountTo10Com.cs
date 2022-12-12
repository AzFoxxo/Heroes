using Heroes;
using Heroes.Attachables;

namespace App.Attachments;

public class CountTo10Com : Attachable
{
    private int _count = 0;

    // Initialisation code
    public override void Initialisation()
    {
        // Print beginning messages
        GayPrint("Initialisation");
        GayPrint("Getting ready to count to 10");
    }

    // Update code
    public override void Update()
    {
        // Increment the count
        _count++;

        // Print the value in gay text
        GayPrint(_count.ToString());

        // Check if the count is 10
        if (_count == 10)
        {
            // Print the end message
            GayPrint("Counted to 10");

            // Delete the attachable
            PrintLine((object)$"Deleting attachable {Parent}");
            Parent!.DeleteAttachable(this);
        }
    }
        
    // Destroy code
    public override void Destroy()
    {
        // Print ending messages
        GayPrint("Destroy");

        // End the application
        Application.Quit();
    }
}