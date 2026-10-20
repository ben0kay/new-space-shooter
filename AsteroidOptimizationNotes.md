# Asteroid Streaming, Rendering & Large-Sector Optimization Notes

> **Status:** Design findings and recommendations — **not implemented** in this new repository.
>
> **Purpose:** Support very large, visually convincing space sectors with potentially thousands or tens of thousands of asteroids, without requiring all of them to be full simulated objects.
>
> **Reference prototype:** [ben0kay/godot-space-shooter-new](https://github.com/ben0kay/godot-space-shooter-new). Preserve that repo; migrate or redesign systems one feature at a time.

## 1. Main Principle: A Big Sector Is Not Automatically Expensive

Placing a station 20,000 units away does not itself create a major rendering or CPU cost. **What exists, what is visible, and what is updating** determine the workload.

The significant costs are typically:

- The number of independent scene nodes and physics objects.
- Draw calls, materials, triangles, shadows and overdraw.
- Collision shapes and physics processing.
- Scripts, movement, target searches, AI and proximity checks.
- Procedural mesh generation and setup when loading a region.
- Memory consumed by all loaded meshes, textures and nodes.
- The number of objects tested and updated as the player travels.

A sector could have 20,000 *logical asteroid records* without 20,000 active `Asteroid` nodes. The objective is **vast represented space, limited active simulation**.

**Performance target, not a guarantee:** Experiment with populations of 5,000–20,000 logical asteroids, then measure frame times, loading stalls and memory use on actual target hardware. Do not treat any asteroid count as a guaranteed 60 FPS.

## 2. Existing Work in the Old Space Shooter

The previous project has several useful pieces already:

| Existing file | What it currently demonstrates |
| --- | --- |
| [ASTEROIDS/Generation/AsteroidBackdrop.cs](https://github.com/ben0kay/godot-space-shooter-new/blob/main/ASTEROIDS/Generation/AsteroidBackdrop.cs) | Decorative distant rocks using shared low-detail mesh variants, spatially divided `MultiMeshInstance3D` batches, no collisions or per-asteroid updates. Has a configured maximum count of 20,000, **not a verified benchmark**. |
| [SCRIPTS/Optimization/DistanceDetailManager.cs](https://github.com/ben0kay/godot-space-shooter-new/blob/main/SCRIPTS/Optimization/DistanceDetailManager.cs) | Staggered distance/frustum checks and near/reduced/distant states for registered objects. |
| [SCRIPTS/Optimization/DistanceDetailConfig.cs](https://github.com/ben0kay/godot-space-shooter-new/blob/main/SCRIPTS/Optimization/DistanceDetailConfig.cs) | Example detail thresholds and check intervals; these are prototype defaults, not universal recommendations. |
| [ASTEROIDS/Asteroid.cs](https://github.com/ben0kay/godot-space-shooter-new/blob/main/ASTEROIDS/Asteroid.cs) | Procedural asteroid meshes, seeded appearance, rotation, collisions, health, damage effects; distant state can disable rotation, shadows and cosmetic effects. |
| [ASTEROIDS/Generation/AsteroidFieldGenerator.cs](https://github.com/ben0kay/godot-space-shooter-new/blob/main/ASTEROIDS/Generation/AsteroidFieldGenerator.cs) | Seeded field placement and persistent IDs, but instantiates each interactive asteroid and checks new placements against previous ones. |
| [MINING/Deposits/AsteroidDepositGenerator.cs](https://github.com/ben0kay/godot-space-shooter-new/blob/main/MINING/Deposits/AsteroidDepositGenerator.cs) | Generates resource deposits on asteroid surfaces. |

**Important limitation of the old architecture:** Decorative backdrop rocks and mineable/damageable rocks are separate populations. The interactive generator creates full nodes, meshes, collision bodies and possible deposits upfront. The distance manager reduces some ongoing work, but does **not** unload those nodes or replace their meshes with true cheaper LODs. Distant cosmetic simplification is not the same as streaming.

## 3. Proposed New Architecture: Asteroid Data vs. Asteroid Presentation

Represent every persistent asteroid first as lightweight data:

- Stable identity: sector key, field key and asteroid ID.
- Position (ideally sector-local or chunk-local), orientation and approximate radius.
- Appearance/shape seed and type or material category.
- Resource identity and seed/data for deposits.
- Changes to preserve: mined quantities, damage/destruction, special loot, etc.

Use the data to choose an appropriate **presentation** near the player:

| State | Representation | Simulation |
| --- | --- | --- |
| **Interactive / near** | Full detailed mesh and a Godot gameplay object | Collision, mining, damage, special effects; rotation only if warranted |
| **Visible / mid-range** | Simplified mesh in spatially grouped `MultiMeshInstance3D` | Normally no physics object, deposits, damage script or individual tick |
| **Distant** | Very low-poly instances, silhouettes, impostors, or none (depending on apparent size) | No individual physics or per-frame updates |
| **Unloaded** | Logical state/seed only | Nothing rendered or simulated; preserve important changes |

An asteroid must be **the same asteroid** when approaching or leaving: no random rerolls, duplicate visuals, regenerated ore, or reappearing destroyed rocks. Stable IDs and deterministic seeds are foundational.

For near/mid/far switching, use distance **and projected/on-screen size**. A huge landmark asteroid remains useful farther away than a tiny pebble.

## 4. Spatial Streaming / Chunks

Divide large sectors into spatial cells or chunks. Each chunk owns a small collection of asteroid records, decorative batches and activated gameplay objects.

- Load or generate logical content using deterministic sector/chunk seeds.
- Keep nearby chunks active and progressively simplify or unload distant chunks.
- Base tests on the camera/player position, relevant asteroid size and gameplay needs.
- Use **hysteresis** or slightly different enter/exit distances so objects do not flicker between states as the ship moves.
- Budget creation and destruction over multiple frames to avoid spikes when flying or boosting.
- Prioritize objects in front of the ship and large visible landmarks when streaming.
- Avoid looping over every asteroid on every frame.
- A spatial hash/grid, octree or similar index can answer *which records are nearby* efficiently; start with the simplest structure that profiles well.

A far asteroid can remain visible as a lightweight instance **without its gameplay node being loaded**.

## 5. Rendering Strategies

### MultiMesh and Batching

Use shared meshes and materials for many similarly shaped rocks. Individual instance transforms can vary position, scale and orientation.

**Caveat:** Godot culls a `MultiMesh` as a batch, not each individual asteroid. Do not place an entire huge sector inside one `MultiMesh`. Partition batches by location/chunk (and, when helpful, LOD/material), so off-screen chunks can be culled.

### LOD Meshes

Maintain a small set of reusable mesh detail levels:

- Near: detailed, irregular procedural surface.
- Medium: notably lower polygon count.
- Far: simple low-poly silhouette.
- Extremely far: vanish when too small, or use an appropriate impostor/visual field.

Minimize visible popping through sensible thresholds, visual matching and optional fades. Different shape variants and seeded scale/rotation help avoid obvious repetition.

### Shadows, Materials and Effects

- Cast detailed shadows only where visually useful, usually near the ship.
- Avoid expensive per-asteroid unique shader materials.
- Skip cosmetic crack/damage shaders, collision effects and debris for distant or invisible asteroids.
- Use shared materials, limit transparency/overdraw, and measure lighting/shadow cost.
- Per-asteroid rotation can stop at distance. Static rocks are usually acceptable there.
- If needed, explore shader-driven *visual-only* motion for far instances, but do not allow the visible orientation to diverge noticeably from a nearby activated collider.

### Collision and Physics

Only activate collision and mining/damage behavior when an asteroid needs gameplay interaction. **Do not rely exclusively on visual LOD to save physics cost.**

Near-activation ranges should account for ship speed and projectile range to avoid ships passing through rocks before collision data loads. Prewarm ahead during boost/high-speed travel.

## 6. Generation and Placement Optimization

The old field generator tests each new asteroid against all previously placed asteroids. This can become expensive with thousands of rocks.

Recommended replacement:

1. Partition candidate placements into a spatial hash/grid.
2. For a new asteroid, inspect only nearby occupied cells.
3. Test actual radius/clearance overlap against those neighbors.
4. Bound placement attempts and report impossible configurations.
5. Reuse deterministic generation seeds so regeneration yields the same layout.

This primarily reduces **world generation/setup** cost, not necessarily steady-state FPS.

Generate expensive asteroid meshes, convex collision shapes and detailed exposed-surface resource deposits **when needed**, rather than constructing them for every distant rock at sector entry. Consider caching shared shapes and any required resource metadata.

## 7. Coordinate Precision: The Large-World Caveat, in Plain English

**Distance itself is not the problem. Extremely large coordinate *numbers* can eventually become a problem.**

For example:

- At position `(0, 0, 0)`, 32-bit floats represent very small offsets accurately.
- At approximately `X = 100,000` units, adjacent representable 32-bit values are spaced about **0.0078125 units** apart.
- At approximately `X = 1,000,000` units, the spacing is about **0.0625 units**.

These spacings are not constant across all coordinate values; they increase in steps as magnitude grows.

If the ship, camera, projectiles or nearby rocks are using extremely large global coordinates, small movements may eventually appear jittery or produce less accurate physics.

**This is separate from asteroid rendering performance.** A sector that is 20,000 units wide is not automatically a precision problem, and thousands of asteroids do not cause precision loss merely because they exist.

### Possible Solutions Later

**Option A — Sector-local coordinates (sensible initial design):**
- Store a sector's entities relative to its own origin, rather than assigning every object one enormous universal coordinate.
- Travel between sectors can re-center gameplay near the origin.
- Keep any overall galaxy/sector positioning separate from active 3D coordinates.

**Option B — Floating origin (only if necessary):**
- If an enormous continuous region takes the player very far from local origin, move nearby scene objects and the ship together so the player is near `(0,0,0)` again.
- Preserve the real logical world location in separate world/chunk coordinates.
- Handle physics, trails, interpolation, effects and camera carefully when shifting; it should not feel like teleportation.

**Option C — Double-precision engine build:**
- Godot can be built with large-world/double-precision support, but it is a special engine build and introduces compatibility/maintenance considerations. Not the default assumption.

**Recommendation:** Do **not** implement floating origin at the start. Prefer sector-local/chunk-local positioning and only add origin shifting when testing proves it is needed.

## 8. AI and Distant Entities

The same separation should apply to ships, drones and stations:

- Distant stations: scene or simplified render, no expensive per-frame scripts.
- Neutral traffic far away: aggregate or simulated at low frequency if continuity matters; activate full ship AI only when relevant.
- Hostile encounters: context-driven spawn/activation and bounded simulation budgets.
- Distant projectiles and effects: retire or simplify according to combat relevance, not solely render distance.
- Important discoveries and destroyed structures: preserve their states persistently.

Never simulate the entire galaxy at full detail every frame.

## 9. Practical Migration Path

Do **not** transfer the entire old asteroid system unchanged or attempt all optimizations at once.

1. **Prove the visuals:** Migrate a ship, camera, environment and a small asteroid test field.
2. **Build asteroid records:** Stable IDs, sizes, positions and procedural seeds independent of Godot nodes.
3. **Implement chunked mid/far rendering:** Shared mesh variants, `MultiMesh`, spatial chunk bounds and basic visibility checks.
4. **Add near-asteroid activation:** Promote selected records into interactive asteroid scenes with collision/mining/damage.
5. **Preserve state across transitions:** Verify mined/depleted/destroyed asteroids remain consistent after unloading/reloading.
6. **Optimize placement:** Introduce spatial hashing and generation budgets when measurements justify it.
7. **Stress-test:** Compare 1,000, 5,000, 10,000 and 20,000 logical asteroids. Record frame rate, CPU/GPU frame time, draw calls, physics objects, memory, load spikes and traversal smoothness.

Keep the existing asteroid visual/damage code as reference. **Profile before adding more complex optimization machinery.**

## 10. Decisions to Make Later

- Desired size and shape of a typical sector?
- Which asteroids should be mineable versus purely decorative?
- How far away should large asteroid silhouettes remain visible?
- What maximum player flight/boost speed must streaming support?
- Should resource deposits be fixed per asteroid or generated from stable seeds at activation?
- How much distant NPC/ship activity should remain logically simulated?
- What persistent state must survive sector unloading?
- What is the target frame-time budget on the developer's actual machine?

---

**Core design rule:** *The universe can contain thousands of asteroids; only a small local subset should need to behave like complete asteroids at any moment.*
