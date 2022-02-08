# ConqueringOfMarsApp

Nasa rovers are exploring a plataeu which is on Mars. Nasa sends instructions to control rovers. This instructions are related to make the rover spin or move forward one grid point. There are three instructions. These are L(Left), R(Right) and M(Move).  

Rover has always facing a compass point and coordinates(x,y).
- Left or Right instruction just changes rover's facing compass point.
- Move instruction just changes rover's coordinates. 

### After Move instrunctions;
- If its facing compass point is North, it is going to North and value on the y-axis increase. Value on the X-axis is not affected.
- If its facing compass point is South, it is going to South and value on the y-axis decrease. Value on the X-axis is not affected.
- If its facing compass point is East, it is going to East and value on the x-axis increase. Value on the Y-axis is not affected.
- If its facing compass point is West, it is going to West and value on the x-axis decrease. Value on the Y-axis is not affected.

Plataeu has a boundary for discovery. Rovers can't get out from boundary. PlataueBoundaryChecker checks the boundary after Move instruction. If a rover reach to boundary, row speaker warns Nasa and it does not move. This boundary is initialized in the appsetting.json. 

RoverSpeaker can give some messages for its position,boundary,moving or own identity. 

### Rovers
There are two rovers. 
- One of them is Red Kit which is starting from(1.2) and facing North at first. It is exploring west side of the Mars.  
- The other one is Sunrise that is starting from(3.3) and facing East. Its mission is exploring east side of the Mars.

After L,M,L,M,L,M,L,M,M for Red Kit and M,M,R,M,M,R,M,R,R,M instructions for Sunrise, output is shown below.

## Output 
<img width="254" alt="output of conquering of Mars" src="https://user-images.githubusercontent.com/8994712/153032314-1195cbcc-d696-4413-9d36-ff12514606ca.png">

## Code Coverage Results
<img width="722" alt="code coverage results" src="https://user-images.githubusercontent.com/8994712/153032814-ab860dec-4e9f-4fe4-8281-7e3b532ea348.png">




