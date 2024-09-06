# GROUP-Integrated-Gameplay-Systems-Prototype
In GROUPS of 2 (max 3), select a game concept with as many "relatively complex" gameplay systems as team members (2 people = 2 systems).
Each system should support gameplay, e.g.: climbing / locomotion, ability systems, gunplay (aim-assist, generation, damage, etc.), artificial intelligence, level generation, character customization, etc. and contain at least 2 design patterns (excl. singletons).
Develop a working prototype of this game concept, that includes game startup, playable gameplay-loop, and a gameplay end-state (game-over, level complete, etc.) 
Document design decisions, individually motivating selection of patterns & integration with other team-member's systems.

RESTRICTION: The entire code-structure may only inherit a single MonoBehaviour (1 per group, not 1 per member). This will force you to implement ways to integrate each gameplay system, and make explicit your approach to object communication.

TIP: try to come up with a "fun" twist on existing gameplay concepts to make it more fun to work on the assignment!

TIP: come up with various "degrees of complexity" for your gameplay system, and start working from the simplest design. Refactor & extend to the more complex designs when time allows. The simplest version should still meet requirements, and complex versions should not change how integration with other systems is performed (this is mostly to save you refactor time! come up with a good interface!)

Deliverables

PDF Documentation, including:
Game Concept description (group)
Gameplay System description (individual)
Selection & Motivation for code structure's design (individual)
Code Conventions (if different from course)
Github link to shared code repository (entire project)
(permanent) Link to Playable Build
UML Class & Activity Diagrams for:
Gameplay Systems (individual and/or group, make sure we can read it)
Game Flow (group)
