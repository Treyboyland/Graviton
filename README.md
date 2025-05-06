# Graviton

I developed Graviton over the course of a few months in 2019. The University of Nebraska's Creative Production Lab (UNO CPL) was looking for local-made games to put onto their gaming cabinet. Building for the cabinet required a number of features, including:

- 5:4 Aspect Ratio
- Idle screens that activated after a certain amount of inactivity
- Support for the cabinet's specific joystick configuration

(Full requirements can be found here: https://www.unomaha.edu/criss-library/creative-production-lab/game-development.php)

I decided to create a version of Snake because A) Snake itself is relatively mechanically simple, and B) I wasn't that experienced making hobbyist games over longer periods. Instead of getting longer, the player cube would speed up and higher scoring pellets would spawn in. The goal was then to gain the highest score as opposed to growing the largest snake.

The level editor was something I added for potential replayability, since users would have been stuck with the few levels I shipped otherwise. The user is able to place boxes within the defined game area and toggle if certain walls will reduce the player score and break the score multiplier. There are also a couple of quality of life features, including an undo command and a mirroring command.

# Post-Mortem
Originally this game was written in Unity 2016, and then upgraded to Unity 6. In that time I decided to use some of the new Unity features, including the Input System. I believe this is a general improvement to the game's architecture, but it also means that it technically cannot run on the older UNO CPL Game Cabinet anymore. I decided to update Canvas scaling a bit as well. 

The music was relively basic, and I would have liked find a more involved track with a longer loop.

There is some general inefficiency in the loading of a lot of levels, as well as the spawn checking. If I had more time I would look into a way to improve the speed of both of those, either through multithreading or Unity's Job system.

Additional features that I would add, if I decided to take more time: moving walls (either scaling up or rotating), 1-hit KO walls, more "juice" (e.g. a trail for the player at high combos, bounce to the combo counter, either some screen shake or a slight pause when the player is hurt by a wall), more balancing with the player's speed at higher combo levels. 


