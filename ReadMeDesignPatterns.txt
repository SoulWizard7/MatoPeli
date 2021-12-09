Niklas Högnabba

Design patterns for game development - Course - FG21

Matopeli - The word for Snake game in Finnish. Game was originally made for the Data Structures and Algorithms course.
A basic Snake game that contains: basic pickups that adds lenght to the snake, a teleport mechanic, and a skill tree where
you can choose your music.

Design Patterns:
-Singleton 
	-Static UnityEvents in: ScoreHandler.cs, SkillTree.cs, TickManager.cs, WormController.cs
	-Used for easy access and Events have various listeners in different scripts, TickManagers tick Event being used the most. This flows over to next pattern below.

-Observer
	-Unity Events are type of observer pattern. For example in WormController.cs, I have a listener that will use AddBodyPart to increase the length of the worm. It uses a linked list to find the last node on the worm and add a new link there. The event comes from BasePickup which invokes the event when the worm head collides with the pickup.

-Single source of truth
	-UpdateAllSkillUI function in SkillTree.cs
	-Uses event to tell skilltree to update, the single truth of the skill values lies in SkillTree

-Façade
	-Not implemented in code, so this becomes more a question of interpretation perhaps. But in the project I have a GameManager GameObject where I have collected all the scripts that have game setting one might want to change. Perhaps a real façade
would be an actual script that controlls all the other scripts and their variables and references. So this manager gameobject could be considered a façade. These scripts include StartSetup.cs, PickupSpawner.cs, TickManager.cs, ScoreHandler.cs and GameMenus.cs. I am trying to implement these kinds of game managers to help the designers to not be confused with having setting for the games spread around in different places.

-Dependency Injections
	-UpdateScore function in ScoreHandler.cs
	-UpdateScore gets called through event invokes, and takes a value referance. You only get 1 point in this version of the game, but in ScoreHandler the function updates in the Start function with a value of 0. This is used to initialize the score board when you start the game. After it is used used to pass through the score value and the method then updates the current score and skill points.



Comments for the person going through this assignment:
I had never studied design patterns at all before this course, so I had some trouble wrapping my head around many things. In the end I think I got some idea of what a design pattern is, and a bit on the way to understand some of the patterns as well. But the more advanced patterns late in the course I barely scrached the surface on. Many patterns are ways of programming that are quite natural to do, you dont even know you are already doing it. Hopefully these examples are satisfactory to get a passing grade. I have also worked with some state machines as well while trying to do enemy AI in other projects, but did not include them in the list above. Maybe quickly check these examples:
https://gitlab.com/SoulWizard7/gamecore-3-hippie-slayer/-/tree/master/Assets/Scripts/StateMachine
https://github.com/SoulWizard7/CoolStealthGame/ //currently in progres as of 9th Dec, 2021

