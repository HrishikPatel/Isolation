using System;
using static System.Console;

namespace Bme121
{
    static class Program
    {
       static bool[ , ] board; 
       static string[ ] letters = { "a","b","c","d","e","f","g","h","i","j","k","l",
									"m","n","o","p","q","r","s","t","u","v","w","x","y","z"};
       static string name;
       static string name2;
       static int rows;
       static int columns;
       static int rowOfA;
       static int rowOfAcopy;
       static int columnOfA;
       static int columnOfAcopy;
       static int rowOfB;
       static int rowOfBcopy; 
       static int columnOfB;
       static int columnOfBcopy;
       static string move;
       static bool moveOfA = true;
        
        static void Main( )
        {
			Initialization();
			bool flag = false;
			while(!flag)
			{
				DrawGameBoard();
				turns();
			}
        }
        static void Initialization()
        {
			            //Asking for names
			Write("Enter your name [default Player A]: ");
			name = ReadLine();
			if(name.Length == 0) name = "Player A";
			WriteLine( "name : {0}", name);
			Write("Enter your name [default Player B]: ");
			name2 = ReadLine();
			if(name2.Length == 0) name2 = "Player B";
			WriteLine( "name : {0}", name2);
                
            //Getting the board size
            Write("Enter a number of rows from 4-26 : ");
            string check = ReadLine();
            if(check.Length == 0) check = "6";
            rows = int.Parse(check);
            while(rows < 4 | rows > 26) 
            {
				Write("please enter a number of rows from 4-26 : ");
				check = ReadLine();
			}
			Write("Enter a number of columns from 4-26 : ");
			check = ReadLine();
			if(check.Length == 0) check = "8";
			columns = int.Parse(check);
			
			// checking if input is valid or not
			while(columns < 4 | columns > 26) 
			{
				Write("please enter a valid number of columns : ");
				check = ReadLine();
			}
			board = new bool[ rows, columns ]; // false if tile removed
			for (int r = 0; r < board.GetLength(0); r++)
			{
				for(int c = 0; c < board.GetLength(1); c++)
				{
					board[ r, c] = true;
				}
			}
			//Choosing starting point of Player A
			Write("{0}, choose your starting row number:", name);
			check = ReadLine();
			if(check.Length == 0) check = "2";
			rowOfA = int.Parse(check);
			Write("{0}, choose your starting column number:", name);
			check = ReadLine();
			if(check.Length == 0) check = "1";
			columnOfA = int.Parse(check);
			WriteLine( "{0}'s starting point is ({1}{2})", name, letters[ rowOfA-1 ], letters[columnOfA-1] );
			rowOfAcopy = rowOfA;
			columnOfAcopy = columnOfA;
			
			//Choosing starting point of player B
			Write("{0}, choose your starting row number:", name2);
			check = ReadLine();
			if(check.Length == 0) check = "3";
			rowOfB = int.Parse(check);
			Write("{0}, choose your starting column number:", name2);
			check = ReadLine();
			if(check.Length == 0) check = "7";
			columnOfB = int.Parse(check);
			WriteLine( "{0}'s starting point is ({1}{2})", name2, letters[ rowOfB-1 ], letters[columnOfB-1] );
			rowOfBcopy = rowOfB;
			columnOfBcopy = columnOfB;
		}
        static void DrawGameBoard()
        {
            const string h  = "\u2500"; // horizontal line
            const string v  = "\u2502"; // vertical line
            const string tl = "\u250c"; // top left corner
            const string tr = "\u2510"; // top right corner
            const string bl = "\u2514"; // bottom left corner
            const string br = "\u2518"; // bottom right corner
            const string vr = "\u251c"; // vertical join from right
            const string vl = "\u2524"; // vertical join from left
            const string hb = "\u252c"; // horizontal join from below
            const string ha = "\u2534"; // horizontal join from above
            const string hv = "\u253c"; // horizontal vertical cross
            //const string sp = " ";      // space
            const string pa = "A";      // pawn A
            const string pb = "B";      // pawn B
            const string bb = "\u25a0"; // small block
            const string fb = "\u2588"; // full block
            const string lh = "\u258c"; // left half block
            const string rh = "\u2590"; // right half block
            
                

			// Draw the board Columns
             for(int c = 0; c < board.GetLength( 1 ); c++)
             {
				 Write("");
				if(c== 0)Write( "     {0}  ", letters[ c ] ); 
				else Write(" {0}  ", letters[c]);
			 }   
			 WriteLine();
            // Draw the top board boundary.
            Write( "   " );
            for( int c = 0; c < board.GetLength( 1 ); c ++ )
            {
                if( c == 0 ) Write( tl );
                Write( "{0}{0}{0}", h );
                if( c == board.GetLength( 1 ) - 1 ) Write( "{0}", tr ); 
                else                                Write( "{0}", hb );
            }
            WriteLine( );
            
            // Draw the board rows.
            for( int r = 0; r < board.GetLength( 0 ); r ++ )
            {
                Write( " {0} ", letters[ r ] );
                
                // Draw the row contents.
                for( int c = 0; c < board.GetLength( 1 ); c ++ )
                {
                    if( c == 0 ) Write( v);
                    if( board[ r, c ])
                    {
						if(r == (rowOfA-1) && c == (columnOfA-1)  )
						{
							Write($" {pa} {v}");								
						}
						else 
						{
							if(r == (rowOfB-1) && c == (columnOfB-1)) Write($" {pb} {v}");
							else 
							{
								if(r == (rowOfAcopy-1) && c == (columnOfAcopy-1))
								{
									Write($" {bb} {v}");
								}
								else
								{
									if(r == (rowOfBcopy-1) && c == (columnOfBcopy-1))
									{
										Write($" {bb} {v}");
									}
									else
									{
										Write( "{0}{1}{2}{3}", rh, fb, lh, v ); //Drawing the boxes
									}
								}
							}
						}
					}
                    else               
                    {
						Write( "   {0}", v);	
					}
                }
                WriteLine( );
                
                // Draw the boundary after the row.
                if( r != board.GetLength( 0 ) - 1 )
                { 
                    Write( "   " );
                    for( int c = 0; c < board.GetLength( 1 ); c ++ )
                    {
                        if( c == 0 ) Write( vr );
                        Write( "{0}{0}{0}", h );
                        if( c == board.GetLength( 1 ) - 1 ) Write( "{0}", vl ); 
                        else                                Write( "{0}", hv );
                    }
                    WriteLine( );
                }
                else
                {
                    Write( "   " );
                    for( int c = 0; c < board.GetLength( 1 ); c ++ )
                    {
                        if( c == 0 ) Write( bl );
                        Write( "{0}{0}{0}", h );
                        if( c == board.GetLength( 1 ) - 1 ) Write( "{0}", br ); 
                        else                                Write( "{0}", ha );
                    }
                    WriteLine( );
                }
			}
        }
        static void turns()
        {
				// Move of player A
				if(moveOfA)
				{
					Write( "{0}, make your move[abcd]", name);
					move = ReadLine( );
					while(move.Length < 4 | move.Length > 4)
					{
						WriteLine("invalid input"); 
						Write( "{0}, make your move[abcd]", name);
						move = ReadLine();
					}
					string s1 = move.Substring( 0, 1 );
					string s2 = move.Substring( 1, 1 );
					if(Array.IndexOf( letters, s1 ) == (rowOfB-1) && Array.IndexOf( letters, s2 ) == (columnOfB-1)) 
					{
						WriteLine("Cannot occupy the same tile as {0}", name2);
						return;
					}
					if(Array.IndexOf( letters, s1 ) > rows |  Array.IndexOf( letters, s2 ) > columns) //Make sure the move is within the board 
					{
						WriteLine("Cannot make a move outside of the board dimensions");
						return;
					}
					if(Array.IndexOf( letters, s1 ) < (rowOfA-2)  ||  Array.IndexOf( letters, s2 ) < (columnOfA-2)) //Making sure the move is only to the adjacent squares in any direction
					{
						
							WriteLine("illegal move");
							return;
					}
					if(Array.IndexOf( letters, s1 ) > rowOfA |  Array.IndexOf( letters, s2 ) > columnOfA)
					{
							WriteLine("illegal move");
							return;	
					}
					string s3 = move.Substring( 2, 1);
					string s4 = move.Substring( 3, 1 );
					if(Array.IndexOf( letters, s3 ) == (rowOfB-1) && Array.IndexOf( letters, s4 ) == (columnOfB-1)) 
					{
						WriteLine("Cannot remove the tile occupied by {0}", name2);
						return;
					}
					if(s1 == s3 && s2 == s4)
					{
						WriteLine("Cannot remove the tile you're moving to");
						return;
					}
					if(!board[Array.IndexOf(letters, s3), Array.IndexOf(letters, s4)])
					{
						WriteLine("Cannot remove the tile that's already removed");
						return;
					}
					if(Array.IndexOf( letters, s3 ) > rows |  Array.IndexOf( letters, s4 ) > columns) 
					{
						WriteLine("Cannot remove a tile outside of the board dimensions");
						return;
					}
					if(Array.IndexOf( letters, s3) == (rowOfBcopy-1) && Array.IndexOf(letters, s4) == (columnOfBcopy-1) )
					{
						WriteLine("Cannot remove starting squares");
						return;
					}
					if(Array.IndexOf( letters, s3) == (rowOfAcopy-1) && Array.IndexOf(letters, s4) == (columnOfAcopy-1) )
					{
						WriteLine("Cannot remove starting squares");
						return;
					}
					board[Array.IndexOf(letters, s3), Array.IndexOf(letters, s4)] = false;
					rowOfA = Array.IndexOf( letters, s1 )+1; // updating positions of token A
					columnOfA = Array.IndexOf( letters, s2 )+1;
					moveOfA = false;
				}
				// Move of Player B
				else
				{
					Write( "{0}, make your move[abcd]", name2);
					move = ReadLine( );
					while(move.Length < 4 | move.Length > 4) //making sure string length is 4 digits only
					{
						WriteLine("invalid input"); 
						Write( "{0}, make your move[abcd]", name2);
						move = ReadLine();
					}
					string s1 = move.Substring( 0, 1 );
					string s2 = move.Substring( 1, 1 );
					if(Array.IndexOf( letters, s1 ) == (rowOfA-1) && Array.IndexOf( letters, s2 ) == (columnOfA-1)) 
					{
						WriteLine("Cannot occupy the same tile as {0}", name);
						return;
					}
					if(Array.IndexOf( letters, s1 ) > rows ||  Array.IndexOf( letters, s2 ) > columns) 
					{
						WriteLine("Cannot make a move outside of the board dimensions");
						return;
					}
					if(Array.IndexOf( letters, s1 ) < (rowOfB-2)  ||  Array.IndexOf( letters, s2 ) < (columnOfB-2)) //Making sure the move is only to the adjacent square
					{
						
							WriteLine("illegal move");
							return;
					}
					if(Array.IndexOf( letters, s1 ) > rowOfB ||  Array.IndexOf( letters, s2 ) > columnOfB)
					{
							WriteLine("illegal move");
							return;	
					}
					string s3 = move.Substring( 2, 1);
					string s4 = move.Substring( 3, 1 );
					if(Array.IndexOf( letters, s3 ) == (rowOfA-1) && Array.IndexOf( letters, s4 ) == (columnOfA-1)) 
					{
						WriteLine("Cannot remove the tile occupied by {0}", name);
						return;
					}
					if(s1 == s3 && s2 == s4)
					{
						WriteLine("Cannot remove the tile you're moving to");
						return;
					}
					if(!board[Array.IndexOf(letters, s3), Array.IndexOf(letters, s4)])
					{
						WriteLine("Cannot remove the tile that's already removed");
						return;
					}
					if(Array.IndexOf( letters, s3 ) > rows ||  Array.IndexOf( letters, s4 ) > columns) 
					{
						WriteLine("Cannot remove a tile outside of the board dimensions");
						return;
					}
					if((Array.IndexOf( letters, s3) == (rowOfBcopy-1) && Array.IndexOf(letters, s4) == (columnOfBcopy-1)))
					{
						WriteLine("Cannot remove starting squares");
						return;
					}
					if(Array.IndexOf( letters, s3) == (rowOfAcopy-1) && Array.IndexOf(letters, s4) == (columnOfAcopy-1) )
					{
						WriteLine("Cannot remove starting squares");
						return;
					}
					board[Array.IndexOf(letters, s3), Array.IndexOf(letters, s4)] = false;
					rowOfB = Array.IndexOf( letters, s1 )+1; // updating positions of token B
					columnOfB = Array.IndexOf( letters, s2 )+1;
					moveOfA = true;
				}
		}
    }
}

