# Soldier
Test project to have soldiers running around and shooting each other.
Goals:
- [x] Learn about animation blendtrees
- [ ] Learn about performance with multiple models
- [ ] Learn about 3D position calculation
- [ ] Play around with pathfinding and line of sight

## Animation blendtrees
Try out the sample code on [unitydocs](https://docs.unity3d.com/Manual/nav-CouplingAnimationAndNavigation.html)

## State machine
Modi:
- Scouting
  - Move .5 speed
  - Scan 45 degrees
  - LookAtWeight body .5
- Firing
  - Do not move
  - Keep scanning the target
  - LookAtWeight body 1