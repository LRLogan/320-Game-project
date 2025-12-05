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

YOU>>She's...WHAT...?!?!

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

-> END

=== petro ===
YOU>>It has to be Petro.

-> END

=== buff_frog ===
YOU>>Buff Frog, maybe...?

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