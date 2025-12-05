+ [0] -> 0search
+ [1] -> 1investigate

=== 0search ===
+ [0] -> 0autopsy

=== 0autopsy ===
YOU>>Oh, the autopsy!
I wonder what it was doing in this trash can?
Well, let's read what it says...

>>. . .

YOU>>He died from an "unknown poison"...?
What could <i>that</i> possibly mean...?
I guess I might as well look around and see if I can get any leads...

-> END

=== 1investigate ===
+ [0] -> 1investigate

=== 1finish ===
YOU>>Those are all the houses in Lilypad Landing, right?
Guess I'd better head back to Pelo's house to deliberate, then.

-> END

=== locked ===
0

+ [0] -> check_house
+ [1] -> find_autopsy
+ [2] -> houses_done

=== check_house ===
YOU>>Hmm, maybe I should check the victim's house first.
It was in the east by the lake, right?

-> 0search

=== find_autopsy ===
YOU>>I should probably go find that autopsy first before someone else does...
It's gotta be outside somewhere, right?

-> 0search

=== footsteps ===
YOU>>Weird...
I could've sworn I heard footsteps, but...
...there's no one out here...?
Well if that shopping list is something to go off of, I'd better be careful of any knife-wielding frogs...

-> 1investigate

=== poison ===
YOU>>Okay, so I've found a rock...
Southwest of the bridge...
Surely <i>this</i> isn't "Hylox Hill", is it?
It's clearly a <i>rock</i>, not a <i>hill</i>...
Oh, but there's something right here!

>>. . .

YOU>>...!
Is this...?!
It's a bottle of...
...poison?!
Well, it seems to be empty, but still...
I wonder what <i>that's</i> about...

-> 1investigate

=== red_tree ===
YOU>>A red tree!
But why is it...bloody?
Wait, there's something in the branches!
Is this...a shirt?
That's a weird design... It's almost like it's...
...!
Surely, it's not actually...!
Are those...<i>real</i> bloodstains?!
Could this have something to do with the murder...?!
I'd better find more about this suspect...
Let's see... Their house was the big one in the north, right?

-> 1investigate

=== houses_done ===
YOU>>It's getting late, so I probably shouldn't be staying out here too much longer.
I've gotta head back to Pelo's house and look at what I've gathered.

+ [0] -> 0search