# Game Vision — New Space Shooter

> **Status:** Early creative direction / pre-production. This document describes the game we want to build, not a list of features already implemented.
>
> **Genre:** 3D spacecraft-based survival, exploration, progression and combat sandbox.
>
> **Engine:** Godot 4.x / C#.
>
> **Previous prototype for reference:** [godot-space-shooter-new](https://github.com/ben0kay/godot-space-shooter-new). The new project is a fresh foundation, not a mandate to migrate every old system unchanged.

## 1. High-Level Vision

Build an atmospheric, visually satisfying **space exploration and survival game where the player's spacecraft is their primary character, home, tool and means of progression**.

The world should invite the player to fly out simply to see what is over the next asteroid ridge, behind a station, or beyond a distant signal. Exploration should regularly reveal something worth investigating: valuable minerals, dangerous territory, unusual formations, abandoned structures, wrecks, other ships, hidden opportunities or unexpected encounters.

Mining, salvaging, upgrading, travelling, fighting and returning to safety should form a connected experience. Combat matters, but the game should **not** feel like an endless arena shooter or a sequence of forced enemy waves.

The previous space shooter already demonstrated that relatively simple 3D ships, procedural asteroids, convincing lighting, thrusters, particles and space environments can create strong visuals. This project should build on that strength rather than becoming dependent on expensive character art or complex humanoid animation.

**The central feeling:** A small spacecraft exploring an enormous, beautiful, dangerous universe — gradually becoming more capable, better equipped and able to venture farther into the unknown.

## 2. The Player's Role

The player begins with a modest spacecraft and limited capabilities. They are not immediately commanding a powerful fleet or operating an industrial empire.

The ship is more than a weapon platform:

- **Transport:** A satisfying vehicle to pilot through large 3D environments.
- **Lifeline:** Fuel, hull integrity, protection and equipment condition matter.
- **Tool:** Mining beams, scanners, salvage equipment and utility systems make exploration productive.
- **Combat platform:** Weapons, shields, countermeasures and handling determine how a threat is approached.
- **Progression platform:** Upgrades and new equipment change what the player can accomplish.

The core game remains **ship-focused**. Cockpit presentation and atmospheric ship interiors may enhance immersion, but walking characters, humanoid combat and extensive character animation are **not prerequisites** for the main gameplay.

An overarching story or mystery could give longer-term purpose, but player-directed exploration and progression should remain enjoyable without constant quest markers.

## 3. Core Design Pillars

### A. Exploration That Rewards Curiosity

Space should feel vast without feeling empty or interchangeable. Different regions need recognizable identities, landmarks, resource opportunities and dangers.

The player should be rewarded for scanning, following signals, navigating asteroid formations and investigating unfamiliar structures.

### B. Survival Without Excessive Chores

The ship should require preparation and occasional difficult decisions: do I have enough fuel, cargo capacity, protection or ammunition to take this risk?

Survival mechanics should create tension and meaningful planning, **not repetitive maintenance every few minutes**. Early gameplay should be approachable, and improved equipment or station services should make routine tasks easier over time.

### C. Meaningful Resource and Equipment Progression

Mining and salvage should have clear purposes: repairing damage, buying or manufacturing equipment, improving the ship, unlocking new capabilities or preparing for more dangerous regions.

A better mining tool should open up new opportunities. Better thrusters should change how the ship feels to pilot. New defences should make previously dangerous journeys more manageable.

### D. Dynamic, Varied Encounters

Hostile ships are only one kind of encounter. The universe should also contain neutral traffic, automated drones, patrols, abandoned assets, damaged vessels, environmental hazards and rare discoveries.

The player should have meaningful options: fight, flee, evade, investigate, salvage, trade or ignore.

### E. Strong Visual Atmosphere

Beautiful lighting, depth, scale, motion and responsive effects are central to the game's identity. Even a quiet flight through an asteroid field should feel good.

The art direction should favour assets and systems that a solo developer can realistically expand: rigid spacecraft, rocks, drones, stations, machinery, shaders and procedural environments.

### F. A World That Remembers

Exploration and player actions should have continuity where it matters. A discovered location should remain discovered; depleted or destroyed objects should not arbitrarily return simply because the player travelled away; significant player changes should persist.

Persistence should be deliberate and performance-conscious rather than saving every inconsequential particle or decorative object.

## 4. The Main Gameplay Loop

A typical session could look like this:

1. **Prepare at a safe location.** Review ship condition, fuel, equipment, cargo and known destinations.
2. **Choose a goal.** Follow an unexplored signal, mine needed resources, investigate a wreck, explore a new region or pursue a contract.
3. **Travel and explore.** Pilot through an atmospheric 3D environment, navigate obstacles and discover places of interest.
4. **Interact with the world.** Scan, mine, collect salvage, investigate structures or encounter other ships.
5. **Make decisions under pressure.** Manage threats, available cargo space, damage, energy and the risk of pushing farther.
6. **Return or press onward.** Seek a station, use field repairs, change destination or pursue a more valuable opportunity.
7. **Progress.** Repair, trade, process resources where appropriate, fit equipment or unlock access to something new.

The loop should support shorter trips as well as longer expeditions. **Exploration, mining and combat should reinforce each other instead of existing as disconnected minigames.**

## 5. The Universe and Exploration

### An Interconnected Space Sandbox

The intended result is a world that feels open and explorable, with multiple regions and meaningful travel between them.

The previous game uses connected, separately loaded sectors. That is a useful starting reference, but the new architecture may evolve. The choice between large streamed spaces, discrete sectors, or a hybrid should be made based on gameplay quality, performance and development practicality.

The player should have freedom **within** a region rather than following a narrow level corridor.

### Regions With Distinct Identities

Potential environments include:

- **Mining belts:** Dense or scattered asteroids, mineral deposits, commercial activity and hazards.
- **Quiet outer sectors:** Lonely, sparsely populated regions with unusual discoveries and valuable salvage.
- **Industrial space:** Stations, platforms, refineries, cargo traffic and controlled territory.
- **Dangerous frontiers:** Hostile patrols, abandoned installations, unstable debris fields or automated defences.
- **Distinctive natural environments:** Nebulae, dust, dramatic stellar lighting, unusually shaped asteroid clusters and other memorable sights.

These are examples, not a finalized map or a requirement to implement every region immediately.

### Authored Discoveries + Procedural Detail

Use a controlled blend:

- **Authored:** Important stations, memorable locations, major routes, unique encounters and selected landmarks.
- **Procedural/data-driven:** Asteroid populations, resource distributions, environmental dressing, some points of interest and suitable encounters.

The goal is variety and replayability without losing recognizable places. A region should feel designed rather than like an endless random scatter of rocks.

Exploration should eventually benefit from useful navigation and discovery tools: region maps, discovered-location markers, scanners, signals, known hazards and travel information.

## 6. Mining, Salvaging and Resources

Mining should be a satisfying physical interaction with asteroids rather than a menu action. Ship equipment, deposit characteristics, risk and location should influence what can be harvested.

Salvaging should provide a complementary path to progression. Wrecks, abandoned equipment, destroyed ships and derelict structures may yield different rewards from natural resources.

Possible progression layers:

- Raw materials gathered from rocks and debris.
- Recovered components or manufactured intermediate items.
- Equipment, ammunition, replacement parts, ship modules and upgrades.

Not every resource needs a complicated refining chain. Production depth should be added only when it creates meaningful choices or a sense of technological advancement.

**Avoid filler resources that exist solely to inflate recipe counts.** Materials should have recurring uses, clear identities and believable relationships to the equipment they help create.

## 7. Ship Progression and Customization

Upgrading the spacecraft should be one of the game's most satisfying rewards.

Possible categories include:

- **Flight and handling:** Thrusters, acceleration, maneuverability, boosting and travel capability.
- **Power and endurance:** Fuel, energy generation/storage and subsystem efficiency.
- **Protection:** Shields, armour, hull durability and countermeasures.
- **Combat:** Weapon types, hardpoints, projectile behaviour, tracking and specialized tools.
- **Exploration:** Sensors, scanners, navigation, detection range and hazard resistance.
- **Utility:** Mining equipment, salvage tools, cargo space and repair capability.

Upgrades should affect the actual experience of flying and interacting with the world, not merely increase numbers.

Different ship classes or additional hulls may be introduced later if they create genuinely different playstyles. A fully modular ship-building system is a possible direction, **not an immediate requirement**.

## 8. Combat and Encounters

Combat should remain enjoyable, responsive and visually impressive — but be one part of a larger experience.

### What Combat Should Feel Like

- Distinct weapons and recognizable projectile, beam and explosion effects.
- Ships with readable silhouettes, roles, behaviours and strengths.
- Shields, armour, hull damage and subsystem effects where they improve decisions.
- Movement and terrain matter: asteroid cover, open space, escape routes and positioning.
- Threats vary from minor opportunistic encounters to dangerous organized groups or rare large ships.

### A Living Universe, Not Constant Waves

Potential encounters include:

- Small pirate groups or opportunistic raiders.
- Patrols and escorts belonging to different factions.
- Automated security drones around restricted locations.
- Neutral miners, traders and utility craft going about their work.
- Distress signals, ambushes and damaged or abandoned ships.
- Contested resource sites and occasional larger engagements.

Encounters should account for region danger, player activity and context. Safe areas should feel relatively safe; dangerous regions should justify preparation.

The design should avoid both extremes: **minutes of featureless inactivity** and **relentless enemy spawning that prevents exploration**.

Enemy behaviours should be varied and data-driven. Not every ship should automatically attack the player on sight.

### Factions

The old prototype already explored different faction identities. The new game can build on that idea with distinct territory, attitudes, ship designs, equipment and encounter patterns.

Reputation, relationships, trade access and territorial control are potential future systems, but their exact depth remains undecided.

## 9. Survival, Risk and Recovery

Possible survival pressures include fuel consumption, ship damage, ammunition availability, energy use, cargo limitations and environmental hazards.

The important question is always: **what interesting decision does this mechanic create?**

For example:

- Low fuel may make a distant rich field risky to investigate.
- Damaged scanners may make navigation or discovery less reliable.
- An overloaded cargo hold may force the player to prioritize valuable salvage.
- A stronger shield may permit exploration of a previously dangerous area.

Failure and recovery need thoughtful design. Returning to a station should feel like relief and progress, not punishment. The exact death, rescue, insurance, repair and cargo-loss rules are intentionally undecided.

## 10. Stations, Infrastructure and Activities

Stations and installations should make the universe feel inhabited while providing useful services.

Possible roles:

- Refuel, repair, rearm and recover.
- Store, trade or process collected resources.
- Purchase, install or craft equipment.
- Obtain information, contracts or travel access.
- Serve as safe bases for longer expeditions.

Building a personal outpost, developing stations, managing autonomous drones or establishing mining infrastructure could become future progression features. These ideas should be evaluated against the ship-focused core loop rather than assumed from the outset.

## 11. Visual Identity and Presentation

The existing space shooter is the visual benchmark, not something to discard.

Preserve and improve the qualities that already work:

- Convincing 3D spacecraft with responsive banking, thrusters and trails.
- Procedural asteroids with believable shapes, scale and rotation.
- Atmospheric stars, nebulae, dust, sunlight, shadows and subtle space haze.
- Clear weapon effects, shield impacts, debris and satisfying destruction.
- The sense of depth and distance when approaching large formations or stations.
- Comfortable third-person ship controls, with cockpit view as an immersive option.

Favor clean, readable presentation and restrained effects over clutter. The world should be beautiful even when nothing is being shot.

Performance is part of visual quality: large regions need distance-based detail, sensible update schedules, pooled transient effects, efficient asteroid populations and streaming or unloading as needed.

## 12. Progression Fantasy

A broad progression arc might be:

1. **Surviving:** A limited ship, small jobs, nearby mining and cautious journeys.
2. **Establishing:** Reliable supplies, repaired equipment, improved cargo and basic combat capability.
3. **Expanding:** Better tools, access to richer regions, more ambitious salvage and tougher threats.
4. **Venturing Beyond:** Specialized ship systems, rare discoveries, dangerous expeditions and high-value opportunities.
5. **Mastery:** A capable vessel and enough freedom to choose ambitious long-term goals.

These are overlapping experiences, not mandatory levels or a fixed campaign.

## 13. Scope Boundaries

The following principles should guide decisions and prevent the project from growing out of control:

- **Not just an arcade shooter.** Fighting is important, but the game must work as an exploration/progression sandbox.
- **Not a humanoid survival game in disguise.** Walking and character animation should not block the primary gameplay.
- **Not a compulsory factory simulator.** Industrial chains and automated logistics are optional, not the main focus.
- **Not an empty infinite universe.** Prioritize interesting navigable regions over raw map size.
- **Not a constant maintenance treadmill.** Survival pressure should support decisions and adventure.
- **Not a full feature checklist on day one.** Begin with a small enjoyable loop, then expand deliberately.

New ideas should earn their place by improving exploration, survival decisions, progression, combat, atmosphere or the player's freedom.

## 14. What Carries Over From the Previous Prototype

The old [godot-space-shooter-new repository](https://github.com/ben0kay/godot-space-shooter-new) is an **implementation reference and source of candidate reusable assets**, not the blueprint for the new code structure.

Notable existing foundations observed in the old project include:

- A 3D player spacecraft, ship controls, boosting and cockpit/chase cameras.
- A player ship model, thruster effects and flight visual feedback.
- Procedural asteroid fields, asteroid damage visuals and resource deposits.
- Mining tools, item definitions and cargo foundations.
- Weapons, beams, projectiles, shields, enemy ships and factions.
- Space lighting, sky shaders, atmospheric effects and station visuals.
- Connected sector travel and a starting HQ/mining-belt setup.
- Early ship-resource, subsystem-condition and progression foundations.

**These features exist in the old prototype; they are not automatically implemented in this new repository.** Some may be reused mostly intact, some refactored, and others replaced after assessing the new design.

### Development Approach

Build the new project incrementally. Establish a clean, modular Godot/C# architecture and migrate one coherent, testable feature at a time. Keep the previous repository unchanged.

Follow this repository's [CodePreferences.md](CodePreferences.md): understandable scripts, clear folders, regioned/commented code, reusable helpers, configuration where helpful, performance awareness and meaningful documentation. Avoid overengineering before a system needs it.

## 15. The First Playable Goal

**A small but convincing space-survival expedition**, not a giant universe.

A worthwhile first vertical slice:

1. Launch or depart from a small safe station in a flyable 3D spacecraft.
2. Enter an asteroid field with appealing lighting, motion and varied rock formations.
3. Locate and mine a useful deposit; collect cargo.
4. Encounter a modest, context-appropriate threat or discovery.
5. Decide whether to continue exploring or return with the collected resources.
6. Return to safety and turn those resources into one meaningful improvement.

This slice should already communicate the future game's promise: **exploration → discovery → risk → reward → better capabilities → deeper exploration**.

Polish the feel of flight and the moment-to-moment visuals early. A small environment that is genuinely enjoyable to explore is a better foundation than ten unfinished systems.

## 16. Important Decisions Still Open

Do not treat these as resolved until discussed and tested:

- How large should local playable regions be, and should travel use distinct sectors, seamless streaming or a hybrid?
- What should the initial ship's survival pressures be, and how punishing should failure feel?
- How much crafting/refining is enjoyable before it becomes busywork?
- How prominent should trading, contracts, factions and an economy become?
- Should players eventually own multiple ships, build outposts, or command drones?
- How persistent and reactive should individual regions be?
- What is the best long-term balance of peaceful exploration and dangerous encounters?
- Is there an overarching story or mystery, or primarily emergent sandbox progression?

These choices should evolve through prototypes rather than be locked in by the first document.

---

**North Star:** *The player should want to undock, fly into the unknown and see what they discover — and feel that every successful journey makes the next one more exciting.*
