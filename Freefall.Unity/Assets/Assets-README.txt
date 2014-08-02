PIXEL SNAPPING:
- When importing a texture, either as a sprite or in any other context, always set the filter mode to "Point". This eliminates the fuzzy edges between pixels.
- Always import sprites with a "Pixels to Units" value of 1. This means that 1 pixel in the Sprite will be equivalent to 1 unit in the Unity editor.

SCENES:
- CAMERA: Use the ScaledCamera prefab for the main scene camera.

REARRANGING FOLDERS/ASSETS:
- ALWAYS do this from within Unity's Project tab: it manages all of the .meta files and internal references correctly.