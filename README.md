Cloning Zen Match
https://play.google.com/store/apps/details?id=com.exoticmatch.game

--UI--
- Level indicator on top center of the screen
- Setting - pause button on top right of the screen
- Playable area
- Matching array area
- Group of 3 help buttons:
+ Remove all unmatched elements from matching array
+ Undo last move
+ Shuffle all tiles on the playable area

--Level--
- Each level contains at least 1 tiles layout.

--Playable area--
- Different tiles with images.
- Tiles can be stacked on other tiles.
- Only the top tile can be selected and move to the matching array, other tiles beneath the top tile will be disabled.
- Order of tile layer can be adjusted by using sorting layer

--Matching array area--
- Consists of 7 elements max.
- Any 3 matching elements will be removed from the matching array and playable area.
- If the new element has instances in the matching array, put it at the existed instance's position +1

--Win condition--
- Tiles in playable area = 0 & number of elements in matching array = 0.
- Player lost if matching array is full (out of space)

--Processing logic--
- If there's nothing in matchingArray, return the first position.
- If the matchingArray isn't empty, find the last index of duplicated elements in the array, then return that index + 1; move any tiles from that index 1 index further
- If the matchingArray is full or out of space, notify user that they failed that level.
- If matchingArray has 3 of the same element, remove them from matchingArray and destroy those gameObjects, play matched particle system at the same time.