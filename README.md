# Game.TicTacToe
TicTacToe game problem

Tic-Tac-Toe
Tic tac toe console game in C#

What?
Tic-tac-toe (also known as noughts and crosses or Xs and Os) is a game for two players, X and O, 
who take turns marking the spaces in a 3×3 grid. The player who succeeds in placing three of their marks in a horizontal, vertical 
or diagonal row wins the game. This is a simple console game with either two players for playing or user playing with computer

To run the application you need to install .NET Core runtime on your system.

To run the application first traverse to the folder that has .csproj available. In this casse it would be ..\Game.TicTacToe\Game.TicTacToe
Now run the following command : dotnet run

To run the tests traverse to the following location : ..\Game.TicTacToe\Game.TicTacToe.Tests
Now run the following command : dotnet test /p:CollectCoverage=true /p:CoverletOutput=TestResults/ /p:CoverletOutputFormat=lcov 
