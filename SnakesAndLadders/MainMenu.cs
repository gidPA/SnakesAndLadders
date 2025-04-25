static class MainMenu{
    static void DisplayMenu(){
        Console.WriteLine
        (
            "Select an action: " +
            "[1] Start a new game" +
            "[2] Load previous game" +
            "[Q] Quit"
        );
    }

    static void NewGame(){
        Console.WriteLine("Please select a board: ");

        for(int i = 0; i < 3; i++){
            Console.WriteLine("Map " + i);
        }
    }
}