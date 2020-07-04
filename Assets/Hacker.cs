using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hacker : MonoBehaviour {
    // Game configuration data
    string[] level1Passwords = { "books", "aisle", "shelf", "password", "font", "borrow" };
    string[] level2Passwords = { "prisoner", "handcuffs", "holster", "uniform", "arrest" };
    string[] level3Passwords = { "astronauts", "environment", "exploration", "starfield", "telescope" };

    //Game state
    int level;
    int count;
    string password;
    enum Screen { MainMenu, Password, Win };
    Screen currentScreen;

    // Use this for initialization
    void Start() {
        ShowMainMenu();
    }

    //This'll display the main menu on the terminal in-game whenever called
    void ShowMainMenu() {
        currentScreen = Screen.MainMenu;
        count = 0;

        Terminal.ClearScreen();
        Terminal.WriteLine("What would you like to hack into?");
        Terminal.WriteLine("");
        Terminal.WriteLine("Press 1 for the local library.");
        Terminal.WriteLine("Press 2 for the police station.");
        Terminal.WriteLine("Press 3 for NASA.");
        Terminal.WriteLine("");
        Terminal.WriteLine("Enter your selection:");
    }

    //Respond to user input
    void OnUserInput(string input) {
        //Main game function input
        if (input == "menu" || input == "Menu")
        {
            ShowMainMenu();
        }
        else if (input == "quit" || input == "exit" || input == "close") {
            Terminal.WriteLine("If on the web, just close the tab.");
            Application.Quit();
        }
        else if (currentScreen == Screen.MainMenu)
        {
            RunMainMenu(input);
        }
        else if (currentScreen == Screen.Password)
        {
            RunPasswordCheck(input);
        }
    }

    void RunMainMenu(string input) {
        bool ifValidLvlNum = (input == "1" || input == "2" || input == "3");

        if (ifValidLvlNum == true)
        {
            level = int.Parse(input);
            StartGame();
        } //Below are easter eggs
        else if (input == "007")
        {
            Terminal.WriteLine("Choose an assignment, Mr. Bond.");
        }
        else if (input == "76")
        {
            Terminal.WriteLine("It's reclamation day! Get out there and reclaim America!");
        }
        else if (input == "Vault-Tech")
        {
            Terminal.WriteLine("Thank you for using, Vault-Tech!");
        }
        else
        {//Invalid input handling
            count++;
            Terminal.WriteLine("Please select a valid level.");
            if(count >= 5)
            {
                ShowMainMenu();
            }
        }
    }

    void RunPasswordCheck(string input) {
        if (input == password)
        {
            DisplayWinScreen();
        }
        else
        {
            StartGame();
        }
    }

    //Function that starts the game
    void StartGame() {
        currentScreen = Screen.Password;
        Terminal.ClearScreen();
        SigninPrompt();
        Terminal.WriteLine("Enter your password, hint: " + password.Anagram());
    }

    void SigninPrompt(){
        switch (level)
        {
            case 1:
                Terminal.WriteLine("Username: Ca*****");
                password = level1Passwords[Random.Range(0, level1Passwords.Length)];
                break;
            case 2:
                Terminal.WriteLine("Username: Bl*******");
                password = level2Passwords[Random.Range(0, level2Passwords.Length)];
                break;
            case 3:
                Terminal.WriteLine("Username: Br**********");
                password = level3Passwords[Random.Range(0, level3Passwords.Length)];
                break;
            default:
                Debug.LogError("Invalid level number");
                ShowMainMenu();
                Terminal.WriteLine("Terminal has crashed, successfully reloaded.");
                break;
        }
    }

    void DisplayWinScreen() {
        currentScreen = Screen.Win;
        Terminal.ClearScreen();
        ShowLevelReward();
    }

    void ShowLevelReward() {
        switch (level)
        {
            case 1:
                Terminal.WriteLine(@"
Hello, Head Librarian Caswell.
Welcome to the County Library Database!
     ______________ 
    /            //  1. Member Mgmt
   /            //   2. Staff Mgmt
  /            //    3. Book Registry
 /            //     4. Check-in/Out
(____________(/      5. Sign-out
"
);
                break;
            case 2:
                Terminal.WriteLine(@"
Hello, Chief Blackwell.
Welcome to the PD Database!
 __
/0 \__________   1. Booking
\__/-='- =-  '   2. Officer Mgmt
                 3. Evidence
                 4. History
                 5. Sign-out
"
);
                break;
            case 3:
                Terminal.WriteLine(@"
Hello, Administrator Brindenstine.
 _ __   __ _ ___  ___ _
| '_ \ / _' / __|/  _' |
| | | | |_| \__ \  |_| |
|_| |_|\__,_|___)\___,_|
Welcome to the NASA Database stored in Houston, Texas.
"
);
                break;
        }
        Terminal.WriteLine("*^*Type menu to exit connection*/*");
    }
}
