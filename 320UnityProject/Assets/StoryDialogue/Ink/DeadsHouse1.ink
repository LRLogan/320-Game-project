0

+ [0] -> 0first_look
+ [1] -> 1search
+ [2] -> 2houses

=== 0first_look ===
YOU>>So this must be the victim...

VIGILANT FROG>>No weapons or causes of death in sight, nor any leads to look into.

YOU>>Huh?

STERN FROG>>We've already surveyed the scene, and there's no more work to be done here.
You might as well just go home now.

YOU>>Wait—
Left before I could even say anything, huh...
Well, it's not like this case is gonna solve itself on its own.
Guess I'd better look around for some clues...

-> END

=== 1search ===
+ [0] -> 1letter

=== 1letter ===
YOU>>A letter...?
Let's see...

>>. . .

YOU>>So the autopsy's gone missing, huh?
Surely it can't be too far from here, though, right?
It's gotta still be somewhere in the town...
Oh, but it looks like there's another letter...!
Let's see what it says...

>>“Dear Pelo,
“Thank you for taking the time to reach out to me.
“The situation you wrote about seems of the utmost importance.
“I will make my way to your town as quickly as possible.
“If there is anything else you would like to ask or express, please do not hesitate.
“- Buff Frog”

YOU>>..."Pelo", huh?
I guess that's the name of the victim here.
And who's this "Buff Frog" figure, I wonder...?
Well, guess I'd better find that autopsy to start with.

-> END

=== 2houses ===
+ [0] -> 2houses

=== 2report ===
YOU>>Okay, let's see what we have here...
A bloodstained shirt...
That one belonged to "Bale", right?
A sticky note that points to an empty poison bottle...
I found that one in a notebook belonging to "Kayla", didn't I?
Oh, and also that shopping list that had a mask and knife in it!
So strange... If I remember correctly, that was "Petro" who bought that, wasn't it?
Well, I guess I'll just lay out these pieces of evidence and...
...wait a second.
Am I misremembering, or was the body in this room last time I was here?
But it's gone now...?
My mind isn't playing tricks on me, is it...?
I'd better double-check with Toadshoe!
Let's see if this walkie-talkie works...

>><i>*bzzt*</i>

YOU>>Detective Toadshoe! Can you hear me?

TOADSHOE>>Yes, I can.
What's the matter?

YOU>>Pelo—the victim, his body <i>was</i> in his house when I first got here, right...?

TOADSHOE>>I believe so, yes.
Why?

YOU>>Well, uh...
I was investigating some of the other residents' houses, but when I got back the body was missing!

TOADSHOE>>What about the other officers that were with you?

YOU>>What officers?

TOADSHOE>>The ones that gave you the debrief when you first got there...?

YOU>>When I first got there...?
...
Oh! I know which ones you're talking about!
They told me they couldn't find any leads and left!

TOADSHOE>>...
......
...............

YOU>>Uhh, are you still there...?

TOADSHOE>>They...did <i>what</i>?
Okay, well I'm going to have a word with them later.
But regardless, if you don't have backup, we're gonna need to get you out of there, quick.

YOU>>But...the case...

TOADSHOE>>We need to make sure you're safe before the murderer finds you.

YOU>>Wait, didn't you say there <i>wasn't</i> a murderer...?

TOADSHOE>>So who is it? What's the name of the frog we have to arrest?

-> report_choices

=== report_choices ===
+ [Bale] -> bale
* [Erica] -> erica
+ [Kayla] -> kayla
+ [Petro] -> petro
+ [Buff Frog] -> buff_frog
* [Toadshoe] -> toadshoe

=== bale ===
YOU>>It's Bale. It's gotta be.

-> END

=== erica ===
YOU>>I think it might be Erica.

TOADSHOE>>"Erica", you say?
...
Have you seen this "Erica"?

YOU>>No, but I found a letter from her in another frog's house.

TOADSHOE>>Well, I'm sorry, but...
We can't arrest her.

YOU>>What?! Why not?

TOADSHOE>>We know which Erica you're talking about.
She's dead.

YOU>>She's...<i>WHAT</i>...?!?!

TOADSHOE>>Died in a horrible accident as she was returning home from a trip...
According to the report, she was stabbed by...
...a tree.

YOU>>A...<i>tree</i>, you say...?

TOADSHOE>>Yes...
So it can't have been her, because this was a few days before Pelo's death.
He was the one that sent the report, after all.

YOU>>...!

TOADSHOE>>So you have to pick someone else.
Who's the frog we've got to arrest?

-> report_choices

=== kayla ===
YOU>>I'm pretty sure it's Kayla.

TOADSHOE>>Kayla?

YOU>>Her house is in the southeast, by Pelo's.
When I was looking around inside, I came across this sticky note.

TOADSHOE>>"Hylox Hill"..."on top"...?

YOU>>I went to the top of Hylox Hill, and there was an empty bottle of poison.
And Pelo's autopsy said that he died from <i>an unknown poison</i>.

TOADSHOE>>So you think this "Kayla" is the one responsible for Pelo's death?

YOU>>She has to be. Why else would she have had that note in there?
She was probably keeping track of where the bottle was so that she could retrieve it later.

TOADSHOE>>Hmm, if you say so...
For the safety of the Forest, right?

-> END

=== petro ===
YOU>>It has to be Petro.

TOADSHOE>>Petro?

YOU>>He lives in a little house in the northeast.
It was kinda bizarre being in there...
For some reason, there was this big crate I had to push to open a door...

TOADSHOE>>Your point...?

YOU>>Right. Yeah, so I managed to find a recent shopping order on his laptop.
There were all sorts of weird things on it, but two items in particular stood out to me:
A ski mask, and a knife.

TOADSHOE>>Interesting...
You think the ski mask could have been to cover his face?

YOU>>It's gotta be. Why else would he have bought it <i>with</i> the knife?
And right before the day of the murder?

TOADSHOE>>Good point...
So he's the one you want to arrest?

YOU>>Yep. No doubt about it.

TOADSHOE>>Alright, then.
For the safety of the Forest, right?

-> END

=== buff_frog ===
YOU>>Buff Frog, maybe...?

TOADSHOE>>Buff Frog...?
You're not being serious, right?

YOU>>...

TOADSHOE>>...
You're actually serious?
Buff Frog's been such a huge help to FROG.
He always reports incidents to us, and he does such a good job cleaning up the scene afterward.
Why would he...?

YOU>>"Cleaning up the scene", you say?

TOADSHOE>>Surely...
Surely not...

YOU>>Each of the residents of this town seem to have some connection to him.
Pelo received a letter from him...
Erica gave Bale a Buff Frog charm for his birthday...

TOADSHOE>>...

YOU>>Is something wrong?

TOADSHOE>>No, it's nothing... Continue.

YOU>>Oh, uh...
Well, Kayla had a drawing of Buff Frog in her notebook...
And I saw something about Buff Frog sticker sheets on Petro's laptop screen.

TOADSHOE>>Huh, interesting...
You see, Buff Frog's something of a celebrity here in the Forest...
...but I wouldn't have expected such a small town like Lilypad Landing to know about him.

YOU>>Really?

TOADSHOE>>I mean, it's only got four houses, right?
How do you think word would have spread in a place like that?

YOU>>Now that I think about it, you have a point...

TOADSHOE>>So you think it's Buff Frog, huh?

YOU>>Yes.

TOADSHOE>>Alright, then. I'll send out the order.
For the safety of the Forest, right?

-> END

=== toadshoe ===
YOU>>Toadshoe...

TOADSHOE>>Yes...?

YOU>>No, I'm saying...
The criminal...is Toadshoe.

TOADSHOE>>...

YOU>>It's you, isn't it?
All of a sudden out of nowhere, you started pushing me to give a name.
Right before I could find anything incriminating about you.
You wanted to cover up your crime so bad...
...you hired a rookie detective for the case.
Because you thought I'd never figure it out.
But I did it anyway.
So? What do you have to say?

TOADSHOE>>...
......
I'm sorry...
...but...
...you're wrong.

YOU>>I know you're lying.

TOADSHOE>>Even if I were, I'm your boss, and you know you can't arrest me.
The law of the Forest declares it as such.
Regardless, I commend you for your bravery, even if it ended up being misguided.

YOU>>What...?
But...!

TOADSHOE>>Also, there is a possibility that you lose your job if you let a murderer run loose by not arresting them.
So you have to choose someone.
Which frog are we arresting?

-> report_choices