# AI-Rocketship-Fly

![](AILearnsToFlyClipGIF.gif.gif)

A handful of rocket ships are spawned on a launch platform, each have different mass and size, this of course affects the fuel consumption and speed. The smaller the rocket, the less fuel is burned but the engine is small so it goes slower. The larger the rocket, the more fuel is burned and the engine is much larger so the rocket goes quicker. The aim of this simulation is to figure out the optimal mass and size of a rocket given a certain inputted fuel amount. The score of each rocket is based on the distance/height. Once the generation is done flying, the top 50% will survive while the others are 'killed' off. The top 50% will then reproduce using a simple neural network and the future generations will carry on the traits such as the size and mass.
