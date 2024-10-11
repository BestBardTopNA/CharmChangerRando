# CharmChangerRando

A mod that let's you randomize the settings of the Charm Changer mod. 

For use with Randomizer 4 and Charm Changer to add an extra level of randomization to the game. 

The randomized values are pulled from a kinda jank gaussian distributaion, resulting in an (roughly) even chance for any value to be higher or lower than the vanilla, and for it to tend to be closer to the vanilla values than extreme values.  

If you want to check how the charms have been randomized in your run, simply head into the charm changer settings and you can see the new values there.

Settings can be found in the connections menu for rando4

Note that some charms can have their stats changed in such a way that makes them unable to do certain things rando4 may assume you have the ability to do in logic: IE it may assume with mark of pride + longnail you can kill a baldur, but in reality the values are randomized so low you cannot. In these very rare cases, either use a debug mod or else adjust the charm changer mod settings to rectify the situation.

Settings:

Randomization shrinking:
    A value from 1 to 5, this sets how much variance there should be. Higher value results in less variance, lower value gives higher variance. 

Exclude Regular Stats:
    Charm Changer has values that can change some non-charm behavior, such as base walking/running speed and focus speed. Turn this setting off if you want the base values randomized as well.

Unimplimaneted: No Stat Decrease
    Currently does nothing. When implemented it will allow you to ensure that charms that improve default behavior (such as sprintmaster increaseing running speed) will still be guarenteed to improve them post-randomization. Currently Sprintmaster's running speed can be lower than your normal speed, making it walkmaster.