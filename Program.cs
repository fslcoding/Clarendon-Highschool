using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;

namespace ConsoleApplication1
{
    class Program
    {


        static string optionEntered;
        static int maxMenu = 0;
        static string path = "students.csv";
        static string[] users = new string[File.ReadAllLines(path).Length];
        static int noStudents = users.Length;
        static int numberOfBitsOfInfo = 10;
        static string[,] students = new string[noStudents, numberOfBitsOfInfo];
        private static int validNumber;

        static void Main(string[] args)
        {
            mainMenu();
            readInUsers();
        }
        static bool validOption(string optionEntered, int maxValue)
        {
            bool isValid = false;
            if (int.TryParse(optionEntered, out validNumber) && (validNumber <= maxValue))
                isValid = true;
            else
                isValid = false;
            return isValid;
        }
        static void readInUsers()
        {
            try
            {
                users = File.ReadAllLines(path);
            }
            catch (FileNotFoundException fnfex)
            {
                Console.WriteLine("File not found. Type any key to continue" + fnfex.ToString());
            }
            string[] tempStudent = new string[numberOfBitsOfInfo];
            for (int i = 0; i < noStudents; i++)
            {
                tempStudent = users[i].Split(',');
                for (int j = 0; j < numberOfBitsOfInfo; j++)
                {
                    students[i, j] = tempStudent[j];
                }
            }
        }
        static void writeToFile(string[,] newStudents)
        {
            try
            {



                using (StreamWriter sw = new StreamWriter(path))
                {
                    for (int i = 0; i < newStudents.GetLength(0); i++)
                    {
                        for (int j = 0; j < newStudents.GetLength(1); j++)
                        {
                            sw.Write(newStudents[i, j] + ",");
                        }
                        sw.WriteLine();
                    }
                }
            }



            catch (Exception ex)
            {
                Console.WriteLine("The file did not write correctly" + ex.ToString());
                mainMenu();
            }
            Console.WriteLine("Students details were succesfully updated. Press any key to continue");
            Console.ReadKey();
        }


        static void appTitle(string menuName)
        {
            Console.Clear();
            Console.WriteLine("*****Clarendon High*****");
            Console.WriteLine("*******SKI TRIP*********");
            Console.WriteLine("************************");
            Console.WriteLine("You are here: " + menuName);
        }

        static void mainMenu()
        {
            appTitle("Main Menu");
            readInUsers();
            Console.WriteLine("1. Add Student Details");
            Console.WriteLine("2. Ski Quiz");
            Console.WriteLine("3. Slope times");
            Console.WriteLine("4. Groups");
            maxMenu = 4;
            try
            {
                do
                {
                    Console.WriteLine("Enter menu option");
                    optionEntered = Console.ReadLine();
                }
                while (!validOption(optionEntered, maxMenu));
            }
            catch (Exception ex)
            {
                Console.WriteLine("The following issue ocurred " + ex.ToString());
                mainMenu();
            }

            switch (validNumber)
            {
                case 1:
                    addStud();
                    break;
                case 2:
                    SkiQuiz();
                    break;
                case 3:
                    slopeTimes();
                    break;
                case 4:
                    Groups();
                    break;
            }
        }

        private static void Groups()
        {
            throw new NotImplementedException();
        }

        private static void slopeTimes()
        {
            throw new NotImplementedException();
        }

        static void addStud()
        {
            string firstName = "";
            string surname = "";
            string gender = "";
            string house = "";
            string username = "";
            int age = 0;
            int quizScore = 0;

            string[] tempStudent = new string[numberOfBitsOfInfo];
            appTitle("ADD STUDENT DETAILS");

            try
            {
                Console.WriteLine("Please enter student first name");
                firstName = Console.ReadLine();

                Console.WriteLine("Please enter student surname");
                surname = Console.ReadLine();
                do
                {
                    Console.WriteLine("Please enter gender (\"m\" or \"f\"");
                    gender = Console.ReadLine().ToLower();
                }
                while (((gender != "m") && (gender != "f")));
                do
                {
                    Console.WriteLine("Please enter age");
                }
                while (!((int.TryParse(Console.ReadLine(), out age) && (age >= 11 && age <= 18))));


            }
            catch (Exception ex)
            {
                Console.WriteLine("The following error occurred " + ex.ToString());
                addStud();
            }

            username = generateUsername(firstName, surname);
            tempStudent[0] = username;
            tempStudent[1] = firstName;
            tempStudent[2] = surname;
            tempStudent[3] = gender;
            tempStudent[4] = age.ToString();
            tempStudent[5] = quizScore.ToString();



            string[,] newStudents = new string[students.GetLength(0) + 1, numberOfBitsOfInfo];

            for (int i = 0; i < students.GetLength(0); i++)
                for (int j = 0; j < students.GetLength(1); j++)
                {
                    newStudents[i, j] = students[i, j];
                }

            for (int k = 0; k < numberOfBitsOfInfo; k++)
            {
                newStudents[newStudents.GetLength(0) - 1, k] = tempStudent[k];
            }
            writeToFile(newStudents);
            Console.WriteLine("New student details have been written to file. Press any key to return to the main menu");
            Console.ReadKey();
            mainMenu();

        }


        static string generateUsername(string firstName, string surname)
        {
            string username;
            Random rand = new Random();
            int randomNumber = rand.Next(100, 999);
            username = firstName.Substring(0, 1) + surname + randomNumber.ToString();
            return username;
        }

        static void quizMenu()
        {
            appTitle("GEOGRAPHY QUIZ MENU");
            Console.WriteLine("1. Start Quiz");
            Console.WriteLine("2. Leaderboard");
            Console.WriteLine("3. Back to Main Menu");

            try
            {
                do
                {
                    Console.WriteLine("Enter menu option");
                    optionEntered = Console.ReadLine();
                }
                while ((!validOption(optionEntered, maxMenu)));
            }
            catch (Exception ex)
            {
                Console.WriteLine("The following issue ocurred " + ex.ToString());
                mainMenu();
            }

            switch (validNumber)
            {
                case 1:
                    geogQuiz();
                    break;
                case 2:
                    leaderboard();
                    break;
                case 3:
                    mainMenu();
                    break;
                default:
                    Console.WriteLine("You chose an invalid option. Please try again");
                    Thread.Sleep(3000);
                    quizMenu();
                    break;
            }
        }


        static void SkiQuiz()
        {
            char correctAnswer, answerChosen;
            int quizScore = 0;
            appTitle("SKI QUIZ");
            string user = "";
            bool invalid = true;
            int previousBestScore = 0;
            int failCounter = 0;
            do
            {
              string valid="false";
          
                if (failCounter > 0)
                {
                    Console.WriteLine("Username does not exist. Please reenter username");
                }
                else
                {
                    Console.WriteLine("Please enter username of person taking the quiz");
                }
                user = Console.ReadLine().ToLower();
                for (int i = 0; i < students.GetLength(0); i++)
                {
                    if (user == students[i, 0])
                    {
                        invalid = false;
                        previousBestScore = Convert.ToInt16(students[i, 9]);
                    }


                    else
                    {
                        invalid = false;
                    }
                }

                if (invalid == false)
                {
                    Console.WriteLine("please try again");
                    Console.ReadKey();
                    geogQuiz();
                }

                Console.WriteLine("Welcome " + user + ", press any key to start quiz. Your previous best score was  " + previousBestScore + ".");
                Console.ReadKey();
                Thread.Sleep(2000);
                appTitle("QUIZ: QUESTION 1");
                Console.WriteLine("1. What is the capital of France");
                Console.WriteLine("a.Paris\nb.Marseille\nc.Nantes\nd.Dieppe");

                try
                {
                    answerChosen = Convert.ToChar(Console.ReadLine().ToLower());
                    correctAnswer = 'a';
                    quizScore += checkQuestion(answerChosen, correctAnswer);

                    appTitle("QUIZ: QUESTION 2");
                    Console.WriteLine("2. In which US state can the city of Miami be found?");
                    Console.WriteLine("a.New York\nb.California\nc.Colorado\nd.Florida");
                    answerChosen = Convert.ToChar(Console.ReadLine().ToLower());
                    correctAnswer = 'd';
                    quizScore += checkQuestion(answerChosen, correctAnswer);

                    appTitle("QUIZ: QUESTION 3");
                    Console.WriteLine("3. On which continent can the Andes be found on");
                    Console.WriteLine("a.North America\nb.South America\nc.Europe\nd.Africa");
                    answerChosen = Convert.ToChar(Console.ReadLine().ToLower());
                    correctAnswer = 'b';
                    quizScore += checkQuestion(answerChosen, correctAnswer);

                    appTitle("QUIZ: QUESTION 4");
                    Console.WriteLine("4. On which continent can Mt Kilmanjiro be found");
                    Console.WriteLine("a.North America\nb.South America\nc.Europe\nd.Africa");
                    answerChosen = Convert.ToChar(Console.ReadLine().ToLower());
                    correctAnswer = 'd';
                    quizScore += checkQuestion(answerChosen, correctAnswer);

                    appTitle("QUIZ: QUESTION 5");
                    Console.WriteLine("5. Which is the largest mountain in the world");
                    Console.WriteLine("a.Everest\nb.K2\nc.Mount St Helens\nd.black");
                    answerChosen = Convert.ToChar(Console.ReadLine().ToLower());
                    correctAnswer = 'a';
                    quizScore += checkQuestion(answerChosen, correctAnswer);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("The following error occurred " + ex.ToString() + ". Press any key to continue");
                    Console.ReadKey();
                    geogQuiz();
                }

                appTitle("END OF QUIZ");

                for (int i = 0; i < students.GetLength(0); i++)
                    if (user == students[i, 0])
                    {
                        if (quizScore > (Convert.ToInt16(students[i, 9])))
                        {
                            students[i, 9] = quizScore.ToString();
                            writeToFile(students);
                            Console.WriteLine("Congratulations, you scored " + quizScore + ". This is your best ever score. Press any key to return to Main Menu");
                            Console.ReadKey();
                            mainMenu();
                        }

                        else
                        {
                            Console.WriteLine("You scored " + quizScore + " but failed to beat your previous best. Press any key to return to Main Menu");
                            Console.ReadKey();
                            mainMenu();
                        }
                    }
            }


        while (valid=="false");
        }

           int checkQuestion(char answerChosen,  string correctAnswer)
            {
                int userScore = 0;

                if (correctAnswer == answerChosen)
                {
                    userScore = userScore + 1;
                    Console.WriteLine("Well done. Correct answer");
                }
                else
                    Console.WriteLine("Hard luck. Wrong answer. The correct answer was " + correctAnswer);
                return userScore;

            }


            static void leaderboard()
            {
                appTitle("GEOGRAPHY QUIZ LEADERBOARD");
                string[,] quizScore = new string[noStudents, 3];

                for (int i = 0; i < students.GetLength(0); i++)
                {
                    quizScore[i, 0] = students[i, 1];
                    quizScore[i, 1] = students[i, 2];
                    quizScore[i, 2] = students[i, 11];
                }
                string temp, temp1, temp2;
                for (int j = 0; j <= quizScore.GetLength(0) - 1; j++)
                {
                    for (int i = 0; i < quizScore.GetLength(0) - 1; i++)
                    {

                        if (Convert.ToInt16((quizScore[i, 2])) < Convert.ToInt16((quizScore[i + 1, 2])))
                        {
                            temp = quizScore[i + 1, 0];
                            temp1 = quizScore[i + 1, 1];
                            temp2 = quizScore[i + 1, 2];

                            quizScore[i + 1, 0] = quizScore[i, 0];
                            quizScore[i + 1, 1] = quizScore[i, 1];
                            quizScore[i + 1, 2] = quizScore[i, 2];

                            quizScore[i, 0] = temp;
                            quizScore[i, 1] = temp1;
                            quizScore[i, 2] = temp2;
                        }
                    }
                }
                int rank = 1;
                Console.WriteLine("Rank\tName\t\tScore");
                for (int a = 0; a < quizScore.GetLength(0); a++)
                {
                    Console.Write(rank + "\t");
                    for (int b = 0; b < quizScore.GetLength(1); b++)
                    {
                        Console.Write((quizScore[a, b].ToString()) + "\t");
                    }
                    Console.WriteLine();
                    rank++;
                }
                Console.WriteLine("Press any key to return to the Quiz menu");
                Console.ReadKey();
                quizMenu();
            }

            static void financeMenu()
            {
                appTitle("SLOPE TIMES MENU");
                Console.WriteLine("1. Add Time");
                Console.WriteLine("2. ");
                Console.WriteLine("3. Return to Main Menu");
                maxMenu = 4;
                do
                {
                    Console.WriteLine("Enter menu option");
                    optionEntered = Console.ReadLine();
                }
                while (!validOption(optionEntered, maxMenu));

                switch (validNumber)
                {
                    case 1:
                        addTime();
                        break;
                    case 2:
                        bestTime();
                        break;
                    case 3:
                        mainMenu();
                        break;
                }
            }


            static void addTime()
            {
                appTitle("ADD TIME");
                bool invalidUser = true;
                int usernameRowID = 0;
                int time;
                string currentValidUser = "";

                do
                {

                    try
                    {
                        Console.WriteLine("Please enter your username");
                        string user = Console.ReadLine();
                        readInUsers();


                        for (int i = 0; i < students.GetLength(0); i++)
                            if (students[i, 0] == user)
                            {
                                invalidUser = false;
                                currentValidUser = students[i, 0];
                                usernameRowID = i;
                            }
                    }

                    catch (Exception ex)
                    {
                        Console.WriteLine("The following issue occurred " + ex.ToString() + ". \nPress any key to reload the add time menu");
                        Console.ReadKey();
                        addTime();
                    }
                }
                while (invalidUser);

                Console.WriteLine("You are about to add deposits for " + currentValidUser + ". Please enter deposit amount");
                for (int j = 1; j <= 3; j++)
                {
                    do
                    {

                        Console.WriteLine("Enter deposit amount " + j + ":");
                    }
                    while (!int.TryParse(Console.ReadLine(), out time));
                    students[usernameRowID, j + 5] = time.ToString();
                }

                writeToFile(students);
                Console.WriteLine("Ski times successfully updated. Press any key to return to ski menu");
                Console.ReadKey();
                financeMenu();
            }


            static void bestTime()
            {
                appTitle("best time");
                double BestTime;
                string[,] studentTimes = new string[students.GetLength(0), 3];
                for (int i = 0; i < students.GetLength(0); i++)
                {
                    BestTime = 0;
                    for (int j = 6; j <= 8; j++)
                    {
                        BestTime += Convert.ToDouble(students[i, j]);
                    }
                    studentTimes[i, 0] = students[i, 1];
                    studentTimes[i, 1] = students[i, 2];
                    studentTimes[i, 2] = BestTime.ToString();
                }

                string temp, temp1, temp2;
                for (int j = 0; j <= studentTimes.GetLength(0) - 1; j++)
                {
                    for (int i = 0; i < studentTimes.GetLength(0) - 1; i++)
                    {

                        if (double.Parse(studentTimes[i, 2]) > double.Parse(studentTimes[i + 1, 2]))
                        {
                            temp = studentTimes[i + 1, 0];
                            temp1 = studentTimes[i + 1, 1];
                            temp2 = studentTimes[i + 1, 2];

                            studentTimes[i + 1, 0] = studentTimes[i, 0];
                            studentTimes[i + 1, 1] = studentTimes[i, 1];
                            studentTimes[i + 1, 2] = studentTimes[i, 2];

                            studentTimes[i, 0] = temp;
                            studentTimes[i, 1] = temp1;
                            studentTimes[i, 2] = temp2;
                        }
                    }
                }
                Console.WriteLine("Name\t\tBest Times");
                for (int a = 0; a < studentTimes.GetLength(0); a++)
                {
                    Console.WriteLine(studentTimes[a, 0].ToString() + "\t" + studentTimes[a, 1].ToString() + "\t\t£" + studentTimes[a, 2].ToString() + "\t£" + (100 - (Convert.ToDouble(studentTimes[a, 2]))));
                }
                Console.WriteLine();

                Console.WriteLine("Press any key to return to the Slope Times menu");
                Console.ReadKey();
                financeMenu();
            }




            static void reports()
            {
                appTitle("REPORTS");
                Console.WriteLine("1. Students by age group ");
                Console.WriteLine("2. Students by gender");
                Console.WriteLine("3. List of students");
                Console.WriteLine("4. Return to main menu ");
                maxMenu = 4;
                do
                {
                    Console.WriteLine("Enter menu option");
                    optionEntered = Console.ReadLine();
                }
                while (!validOption(optionEntered, maxMenu));

                switch (validNumber)
                {
                    case 1:
                        ageGroup();
                        break;
                    case 2:
                        gender();
                        break;
                    case 3:
                        studentList();
                        break;
                    case 4:
                        mainMenu();
                        break;
                }
            }

            static void ageGroup()
            {
                appTitle("STUDENTS BY AGE GROUP\n");
                Console.WriteLine("\t\tAGE 11");
                for (int i = 0; i < students.GetLength(0); i++)
                {
                    if ((Int16.Parse(students[i, 4]) >= 11) && (Int16.Parse(students[i, 4]) <= 11))
                    {
                        Console.WriteLine(students[i, 1] + "\t" + students[i, 2]);
                    }
                }
                Console.WriteLine("\n\t\tAGE 12");
                for (int i = 0; i < students.GetLength(0); i++)
                {
                    if ((Int16.Parse(students[i, 4]) >= 12) && (Int16.Parse(students[i, 4]) <= 12))
                    {
                        Console.WriteLine(students[i, 1] + "\t" + students[i, 2]);
                    }
                }
                Console.WriteLine("\nPress any key to return to the reports menu");
                Console.ReadKey();
                reports();
            }


            static void gender()
            {
                appTitle("STUDENTS BY GENDER\n");
                Console.WriteLine("\t\tMALE");
                for (int i = 0; i < students.GetLength(0); i++)
                {
                    if (students[i, 3] == "m")
                    {
                        Console.WriteLine(students[i, 1] + "\t" + students[i, 2]);
                    }
                }
                Console.WriteLine("\n\t\tFEMALE");
                for (int i = 0; i < students.GetLength(0); i++)
                {
                    if (students[i, 3] == "f")
                    {
                        Console.WriteLine(students[i, 1] + "\t" + students[i, 2]);
                    }
                }
                Console.WriteLine("\nPress any key to return to the reports menu");
                Console.ReadKey();
                reports();
            }


            static void studentList()
            {
                appTitle("STUDENTS LIST\n");
                Console.WriteLine("Name\t\tGender\tAge");
                for (int i = 0; i < students.GetLength(0); i++)
                {
                    for (int j = 1; j < 5; j++)
                    {
                        Console.Write(students[i, j] + "\t");
                    }
                    Console.WriteLine();
                }
                Console.WriteLine("\nPress any key to return to the reports menu");
                Console.ReadKey();
                reports();
            }
        }
    }

