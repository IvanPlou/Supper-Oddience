Alex Gulewich
Jan, 27, 2024
Sound Collision Affector

The sound collision affector was made to affect pitch and volume according to the impact,
It accepts a Collision Affector Settings scriptable object to assign it's attributes.

I created the script to be able to be more or less self sufficent as far as gameobject audio goes,
Just to speed things along in production however it is automatically overridden from the presence of another audio source

A T T R I B U T E S

Clip: The audio Clip You want played on collision

Trigger Before Collision: How far from the actual collider until the noise should be activated.
NOTE: GREAT if the timing is a bit off, BUT if the audio is playing when there was never a collision check this

Audio Deadzone: The minimum speed the attached object has to be going to activate the sound

Create Audio Source: Check this off if you want the script to create a audio source if one isn't already present