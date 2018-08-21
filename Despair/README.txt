** DESPAIR              **
** BY: RICHIE TARKOWSKI **
** CREATED: 2/19/18     **
** COMPLETED: 5/3/18	**
** CIT 195              **

--OVERVIEW: 

	Player is trapped in an abandoned school and has two goals:
		1) Exit safely
		2) Collect evidence for his/her report

	In order to exit, the game will require the player to collect all evidence.

	The Player can die at each door in the game in one of two ways:
		1) If an enemy is behind the door and the player attempts to enter
		2) The player keeps the door open too long

	There are 12 total locations, 10 objects, 6 NPCs in the game

	Player has a level and XP. Level increases by 1 as XP increases by 10.
	Player earns XP from traveling, picking up objects, talking to NPCs

	Locations are restricted based on the players current location or what the have in their inventory.

--TIMER CODE:

	I used this code as my old jumpscare event, but the console got stuck on the Console.ReadKey() when the event was raised

    //Create Timer
    System.Timers.Timer jumpScare = new System.Timers.Timer();

    //Trigger event when timer expires
    jumpScare.Elapsed += new ElapsedEventHandler(OnTimedEvent);

    //Timer length (MS)
    jumpScare.Interval = 2000;

    //No reset
    jumpScare.AutoReset = false;

    //Enable timer event trigger
    jumpScare.Enabled = true;

    //Start Timer
    jumpScare.Start();