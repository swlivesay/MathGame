/*
    ------ Requirements-----
    1. You need to create a Math game containing the 4 basic operations
    2. The divisions should result on INTEGERS ONLY and dividends should go from 0 to 100. 
        Example: Your app shouldn't present the division 7/2 to the user, since it doesn't result in an integer.
    3. Users should be presented with a menu to choose an operation
    4. You should record previous games in a List and there should be an option in the menu for the user to visualize a history of previous games.
    5. You don't need to record results on a database. Once the program is closed the results will be deleted.

*/

using System.Collections;

Random rand = new Random();
string[,] gameHistory = new string[10, 3];

bool playGame = true;
string? userSelection = "exit";
string equation;
string? readResult;
int gameSelection;
int myAnswer;
int firstNumber;
int secondNumber;
int roundsPlayed = 0;
int maxRounds = 10;
int correctAnswerCount = 0;

Console.Clear();
Console.WriteLine("Welcome to the Math Game!\n");

// game loop starts here
// 1 = addition, 2 = subtraction, 3 = multiplication, 4 = division, 5 = random operator, 6 = history

do
{
    
    DisplayGameMenu(roundsPlayed + 1, maxRounds);
    
    // user selects which math operation to play, or enters exit to end game or print history.
    userSelection = SelectGameOption();
    if (userSelection == "5")
        userSelection = RandomOperator();

    if (userSelection == "exit")
    {
        playGame = false;
    }
    else if (userSelection == "6")
    {
        PrintHistory(gameHistory);
    }
    else
    {
        // if game selection is a math operator execute the below 
        int.TryParse(userSelection, out gameSelection);
        if (gameSelection == 4) // if division change scope of random to be 0-100 inclusive
            GetNumbers(0, 101);       
        else    
            GetNumbers();
        AskMathQuestion(firstNumber, secondNumber,  gameSelection);
        bool isCorrect = CheckAnswer(myAnswer, gameSelection);
        StoreHistory(roundsPlayed, equation, myAnswer, isCorrect);
        roundsPlayed++; 
    }

    // count rounds played and end after the 10th round
    if (roundsPlayed == maxRounds)
        playGame = false;
          
} while (playGame == true);

EndGame();


void EndGame()// provide end of game summary to include how many rounds played, number of correct answers and a score
{

    Console.Clear();
    Console.WriteLine($"Game Over!\n\nYou played {roundsPlayed} rounds and answered {correctAnswerCount} questions correctly.");
    Console.WriteLine($"Your score is {(correctAnswerCount / (float)roundsPlayed):P2}\n");
    PrintHistory(gameHistory);

}
// displays game mode options and prompts player for input
void DisplayGameMenu(int roundsPlayed, int maxRounds)
{
    Console.WriteLine($"Round {roundsPlayed} of {maxRounds}.\nSelect one of the following options:");
    Console.WriteLine("1 Addition\n2 Subtraction\n3 Multiplication\n4 Division\n5 Random\n6 Print History\nType 'Exit' to quit\n");
    Console.Write("Choice: ");
}

// reads user input and verifies it is a valid choice. returns selected mode
string SelectGameOption()
{   
    bool valid = false;
    do
    {
        string? readSelection;
        string[] validSelection = {"1", "2", "3", "4", "5", "6", "exit"};
        readSelection = Console.ReadLine().ToLower();
        if (readSelection != null)
        {
            if (validSelection.Contains(readSelection) == true)
            {
                userSelection = readSelection; 
                valid = true;
            }  
            else
            {
                Console.Clear();
                Console.WriteLine("You entered an invalid game mode.");
                DisplayGameMenu(roundsPlayed + 1, maxRounds);
            }
        }
    } while (valid == false);
    Console.Clear();
    return userSelection;         
    
}

//generate numbers for math game
void GetNumbers(int start = 1, int end = 13)
{
    Random rand = new Random();
    firstNumber = rand.Next(start, end);
    secondNumber = rand.Next(start, end);

}

// based on game mode selected, ask user a math question. Read user input and result in myAnswer
int AskMathQuestion(int x, int y, int gameSelection)
{

    switch (gameSelection)
    {
        case 1:
            Console.Write($"{x} + {y} = ");
            break;
        case 2:
            Console.Write($"{x} - {y} = ");
            break;
        case 3:
            Console.Write($"{x} x {y} = ");
            break;
        case 4:
            bool notValidExpression = true; // division problem must result in whole number with no remainder
            do
            {   
                // ensure the quotient results in an integer and has no remainder. ensure no divide by zero
                
                if (y > 0 && x % y == 0)
                {
                    Console.WriteLine($"{x} / {y} = ");
                    notValidExpression = false;
                }
                // if operation requirements from above comment are not met, get two new numbers and assign to x & y as well as 
                // firstNumber and secondNumber
                else 
                {
                    GetNumbers(0, 101);
                    x = firstNumber;
                    y = secondNumber;               
                }
            } while (notValidExpression);
            break;
    }
    
    
    readResult = Console.ReadLine();
    int.TryParse(readResult, out myAnswer); 
    return myAnswer;
}


// take myAnswer from AskMathQuestion and check for correct answer. use gameSelection to specify which operation to perform
// returns boolean
bool CheckAnswer(int myAnswer, int gameSelection)
{
    bool answerIsCorrect = false;
    int correctAnswer = -1;
    char myOperator = '?';

    switch (gameSelection)
    {
        case 1:
            myOperator = '+';
            correctAnswer = firstNumber + secondNumber;
            if (myAnswer == correctAnswer)
                answerIsCorrect = true;
            break;
        case 2:
            myOperator = '-';
            correctAnswer = firstNumber - secondNumber;
            if (myAnswer == correctAnswer)
                answerIsCorrect = true;
            break; 
        case 3:
            myOperator = 'x';
            correctAnswer = firstNumber * secondNumber;
            if (myAnswer == correctAnswer)
                answerIsCorrect = true;
            break;
        case 4:
            myOperator = '/';
            correctAnswer = firstNumber / secondNumber;
            if (myAnswer == correctAnswer)
                answerIsCorrect = true;
            break;
    }
    equation = $"{firstNumber.ToString()} {myOperator.ToString()} {secondNumber.ToString()} = ";

    if (answerIsCorrect == false)
        Console.WriteLine($"Incorrect, the correct answer is {correctAnswer}\n");   
    else
    {
        Console.WriteLine("Correct!\n");
        correctAnswerCount++;
    }

    WaitAndClearScreen();
    return answerIsCorrect;

}

// waits for user to hit any key and clears the screen for next round or display of info. 
void WaitAndClearScreen()
{
    Console.Write("Press any key to continue...");
    Console.ReadKey();
    Console.Clear();
}

// store game info for each round into an array
void StoreHistory(int roundsPlayed, string equation, int myAnswer, bool isCorrect)
{
    int round = roundsPlayed;
    gameHistory[round, 0] = equation;
    gameHistory[round, 1] = myAnswer.ToString();
    string correct = (isCorrect)? "Correct": "Incorrect";
    gameHistory[round, 2] = correct;
}

// using gameHistory Array extract game info and display it
void PrintHistory(string[,] gameHistory)
{
    Console.Write($"Round  |  ");
    Console.Write($"Equation  |  ");
    Console.Write($"You Answered  |  ");
    Console.Write($"  Result\n");

   
    for (int i = 0; i < gameHistory.GetLength(0); i++)
    {
        Console.Write(i+1 + "\t");
        Console.Write($"  {gameHistory[i,0]}\t");
        Console.Write($"    {gameHistory[i,1]}\t\t");
        Console.Write($" {gameHistory[i,2]}\n");
    }
    Console.WriteLine("");
    WaitAndClearScreen();
}

// when Random game mode is selected (5), randomly select a number to mimic game selection of game mode operator (+, -, *, /)
string RandomOperator()
{
    Random rand = new Random();
    return rand.Next(1, 5).ToString();
}