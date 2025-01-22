/*
    ------ Requirements-----
    1. You need to create a Math game containing the 4 basic operations
    2. The divisions should result on INTEGERS ONLY and dividends should go from 0 to 100. 
        Example: Your app shouldn't present the division 7/2 to the user, since it doesn't result in an integer.
    3. Users should be presented with a menu to choose an operation
    4. You should record previous games in a List and there should be an option in the menu for the user to visualize a history of previous games.
    5. You don't need to record results on a database. Once the program is closed the results will be deleted.

*/

Random rand = new Random();
string[] gameHistory = new string[10];
bool playGame = true;
string? userSelection = "exit";
string equation;
string? readResult;
int gameSelection;
int myAnswer;
int firstNumber;
int secondNumber;

Console.Clear();
Console.WriteLine("Welcome to the Math Game!\n");

// game loop starts here
// 1 = addition, 2 = subtraction, 3 = multiplication, 4 = division, 5 = random operator, 6 = history
// TODO: add Random and History choices for player
do
{
    DisplayGameMenu();
    
    // user selects which math operation to play, or enters exit to end game.
    userSelection = SelectGameOption();
    if (userSelection == "exit")
    {
        playGame = false;
    }
    else
    {
         
        int.TryParse(userSelection, out gameSelection);
        if (gameSelection == 4) // if division change scope of random to be 0-100 inclusive
            GetNumbers(0, 101);
        else        
            GetNumbers();
        AskMathQuestion(firstNumber, secondNumber,  gameSelection);
        bool isCorrect = CheckAnswer(myAnswer, gameSelection); // TODO does this bool need to be assigned?
    }
     
} while (playGame == true);

void DisplayGameMenu()
{
    Console.WriteLine("Select one of the following options:");
    Console.WriteLine("1 Addition\n2 Subtraction\n3 Multiplication\n4 Division\nType 'Exit' to quit\n");
    Console.Write("Choice: ");
}

string SelectGameOption()
{
    string? readSelection;
    readSelection = Console.ReadLine();

    if (readSelection != null)
    {
    Console.Clear();
        switch (readSelection)
        {
            case "1":
            case "2":
            case "3":
            case "4":
                return readSelection; 
            default:
                userSelection = "exit";
                break;
        }
    }
    return userSelection;
}

void GetNumbers(int start = 1, int end = 13)
{
    Random rand = new Random();
    firstNumber = rand.Next(start, end);
    secondNumber = rand.Next(start, end);

}

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
            {   // TODO: catch divide by zero here
                // ensure the quotient results in an integer and has no remainder
                
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
    
    // TODO what error handling needs to happen here?
    readResult = Console.ReadLine();
    myAnswer = 0;
    int.TryParse(readResult, out myAnswer); // TODO: throw exception if input is not a number
    return myAnswer;
}



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
            correctAnswer = firstNumber - secondNumber;
            if (myAnswer == correctAnswer)
                answerIsCorrect = true;
            break; 
        case 3:
            correctAnswer = firstNumber * secondNumber;
            if (myAnswer == correctAnswer)
                answerIsCorrect = true;
            break;
        case 4:
            correctAnswer = firstNumber / secondNumber;
            if (myAnswer == correctAnswer)
                answerIsCorrect = true;
            break;
    }
    equation = $"{firstNumber.ToString()} {myOperator.ToString()} {secondNumber.ToString()} = ";

    if (answerIsCorrect == false)
        Console.WriteLine($"Incorrect, the correct answer is {correctAnswer}\n");   
    else
        Console.WriteLine("Correct!\n");

    Console.Write("Press any key to continue...");
    Console.ReadKey();
    Console.Clear();
    return answerIsCorrect;

}

// void StoreHistory(string equation, int myAnswer, int gameSelection)
// {
    
   
//}