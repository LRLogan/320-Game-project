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

=== locked ===
0

+ [0] -> check_house
+ [1] -> find_autopsy

=== check_house ===
YOU>>Hmm, maybe I should check the victim's house first.
It was in the northeast, right?

-> 0search

=== find_autopsy ===
YOU>>I should probably go find that autopsy first before someone else does...
It's gotta be outside somewhere, right?

-> 0search

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