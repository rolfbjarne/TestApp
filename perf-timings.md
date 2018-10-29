Configuration: release with llvm enabled

| Scenario          | initial | array fix | dict fix | array fix + memcpy fix | array + binary search fix |
| -                 | -       | -         | -        | -                      | -                         |
| Release/link all  | 470ms   | 229ms     | 224ms    | 220ms                  | 216                       |
| Release/dont link | 754ms   | 448ms     | 377ms    | 451ms                  | 373                       |

Numbers
=======

Test case: https://github.com/rolfbjarne/TestApp/commit/004283d7b628a29fcf711d98d8842bfd4ef4393b

iPad Air 2
----------

| Configuration       | Before | After fix 1 | After fix 2  | Improvement from fix 1 to fix 2 | Improvement from before to fix 2   |
| ------------------- | ------ | ----------: | -----------: | ------------------------------: | ---------------------------------: |
| Release (link all)  | 477 ms |      481 ms |       224 ms |                    257 ms (53%) |                       253 ms (53%) |
| Release (dont link) | 738 ms |      656 ms |       377 ms |                    279 ms (43%) |                       459 ms (62%) |

iPhone X
--------

| Configuration       | Before | After fix 1 | After fix 2  | Improvement from fix 1 to fix 2 | Improvement from before to fix 2   |
| ------------------- | ------ | ----------: | -----------: | ------------------------------: | ---------------------------------: |
| Release (link all)  |  98 ms |       99 ms |        42 ms |                     57 ms (58%) |                        56 ms (57%) |
| Release (dont link) | 197 ms |      153 ms |        91 ms |                     62 ms (41%) |                       106 ms (54%) |

When linking all assemblies, the type map has 24 entries, and when not linking
at all it has 2993 entries.

This is part 2 of multiple fixes for #4936.


Numbers
=======

Test case: https://github.com/rolfbjarne/TestApp/commit/004283d7b628a29fcf711d98d8842bfd4ef4393b

Fix 1 refers to PR #5009.
Fix 2 refers to PR #?
Fix 3 is this fix.

iPad Air 2
----------

| Configuration       | Before | After fix 1 | After fix 2  | After fix 3  | Improvement from fix 2 to fix 3 | Cumulative improvement |
| ------------------- | ------ | ----------: | -----------: | -----------: | ------------------------------: | ---------------------: |
| Release (link all)  | 477 ms |      481 ms |       224 ms |       172 ms |                     52 ms (23%) |           305 ms (64%) |
| Release (dont link) | 738 ms |      656 ms |       377 ms |       201 ms |                    176 ms (47%) |           537 ms (73%) |

iPhone X
--------

| Configuration       | Before | After fix 1 | After fix 2  | After fix 3  | Improvement from fix 2 to fix 3 | Cumulative improvement |
| ------------------- | ------ | ----------: | -----------: | -----------: | ------------------------------: | ---------------------: |
| Release (link all)  |  98 ms |       99 ms |        42 ms |        31 ms |                     11 ms (26%) |            67 ms (68%) |
| Release (dont link) | 197 ms |      153 ms |        91 ms |        43 ms |                     48 ms (53%) |           154 ms (78%) |

When linking all assemblies, the type map has 24 entries, and when not linking
at all it has 2993 entries.

This is part 3 of multiple fixes for #4936.


Numbers
=======

Test case: https://github.com/rolfbjarne/TestApp/commit/004283d7b628a29fcf711d98d8842bfd4ef4393b

Fix 1 refers to PR #5009.
Fix 2 refers to PR #?
Fix 3 is this fix.

iPad Air 2
----------

| Configuration       | Before | After fix 1 | After fix 2  | After fix 3  | After fix 4  | Improvement from fix 3 to fix 4 | Cumulative improvement |
| ------------------- | ------ | ----------: | -----------: | -----------: | -----------: | ------------------------------: | ---------------------: |
| Release (link all)  | 477 ms |      481 ms |       224 ms |       172 ms |       148 ms |                     24 ms (14%) |           329 ms (69%) |
| Release (dont link) | 738 ms |      656 ms |       377 ms |       201 ms |       146 ms |                     55 ms (27%) |           592 ms (80%) |

iPhone X
--------

| Configuration       | Before | After fix 1 | After fix 2  | After fix 3  | After fix 4  | Improvement from fix 3 to fix 4 | Cumulative improvement |
| ------------------- | ------ | ----------: | -----------: | -----------: | -----------: | ------------------------------: | ---------------------: |
| Release (link all)  |  98 ms |       99 ms |        42 ms |        31 ms |        29 ms |                      2 ms ( 6%) |            69 ms (70%) |
| Release (dont link) | 197 ms |      153 ms |        91 ms |        43 ms |        28 ms |                     15 ms (35%) |           169 ms (86%) |

When linking all assemblies, the type map has 24 entries, and when not linking
at all it has 2993 entries.

This is part 4 (the last) of multiple fixes for #4936.


# Part 5 (instantiator)

Numbers
=======

Test case: https://github.com/rolfbjarne/TestApp/commit/004283d7b628a29fcf711d98d8842bfd4ef4393b

Fix 1 refers to PR #5009.
Fix 2 refers to PR #5013.
Fix 3 refers to PR #?
Fix 4 refers to PR #?
Fix 5 is this fix.

iPad Air 2
----------

| Configuration       | Before | After fix 1 | After fix 2  | After fix 3  | After fix 4  | After fix 5  | Improvement from fix 4 to fix 5 | Cumulative improvement |
| ------------------- | ------ | ----------: | -----------: | -----------: | -----------: | -----------: | ------------------------------: | ---------------------: |
| Release (link all)  | 477 ms |      481 ms |       224 ms |       172 ms |       148 ms |        86 ms |                     62 ms (42%) |           391 ms (82%) |
| Release (dont link) | 738 ms |      656 ms |       377 ms |       201 ms |       146 ms |        88 ms |                     58 ms (40%) |           650 ms (88%) |

iPhone X
--------

| Configuration       | Before | After fix 1 | After fix 2  | After fix 3  | After fix 4  | After fix 5  | Improvement from fix 4 to fix 5 | Cumulative improvement |
| ------------------- | ------ | ----------: | -----------: | -----------: | -----------: | -----------: | ------------------------------: | ---------------------: |
| Release (link all)  |  98 ms |       99 ms |        42 ms |        31 ms |        29 ms |        18 ms |                     11 ms (38%) |            80 ms (82%) |
| Release (dont link) | 197 ms |      153 ms |        91 ms |        43 ms |        28 ms |        17 ms |                     11 ms (39%) |           180 ms (91%) |

When linking all assemblies, the type map has 24 entries, and when not linking
at all it has 2993 entries.






## baseline

### iPad Air 2
  linkall:  Did 10000 iterations in 477ms (average: 477.07ms of 100 iterations, diff since last: 0.00)
  dontlink: Did 10000 iterations in 738ms (average: 738.28ms of 100 iterations, diff since last: 0.00)

### iPhone X

  linkall:  Did 10000 iterations in 99ms (average: 99.43ms of 100 iterations, diff since last: 0.00)
  dontlink: Did 10000 iterations in 197ms (average: 196.66ms of 100 iterations, diff since last: 0.00)



## binary search

### iPad Air 2

  linkall:  Did 10000 iterations in 479ms (average: 481.20ms of 100 iterations, diff since last: -0.02)
  dontlink: Did 10000 iterations in 655ms (average: 656.14ms of 100 iterations, diff since last: -0.01)

### iPhone X

  linkall:  Did 10000 iterations in  98ms (average:  97.86ms of 100 iterations, diff since last: 0.00)
  dontlink: Did 10000 iterations in 153ms (average: 153.18ms of 100 iterations, diff since last: 0.00)



## array fix

### iPad Air 2

  linkall:  Did 10000 iterations in 223ms (average: 223.54ms of 100 iterations, diff since last: -0.01)
  dontlink: Did 10000 iterations in 376ms (average: 376.59ms of 100 iterations, diff since last: -0.01)

### iPhone X

  linkall:  Did 10000 iterations in 42ms (average: 41.74ms of 100 iterations, diff since last: 0.00)
  dontlink: Did 10000 iterations in 91ms (average: 91.44ms of 100 iterations, diff since last: 0.00)



## intptr ctor cache

### iPad Air 2

  linkall:  Did 10000 iterations in 171ms (average: 172.33ms of 100 iterations, diff since last: -0.01)
  dontlink: Did 10000 iterations in 200ms (average: 200.61ms of 100 iterations, diff since last: -0.01)

### iPhone X

  linkall:  Did 10000 iterations in 30ms (average: 30.70ms of 100 iterations, diff since last: -0.01)
  dontlink: Did 10000 iterations in 43ms (average: 43.32ms of 100 iterations, diff since last: 0.00)


## is_user_type fix

### iPad Air 2

  linkall:  Did 10000 iterations in 147ms (average: 147.87ms of 100 iterations, diff since last: -0.01)
  dontlink: Did 10000 iterations in 145ms (average: 146.17ms of 100 iterations, diff since last: -0.01)

### iPhone X


  linkall:  Did 10000 iterations in 29ms (average: 28.65ms of 100 iterations, diff since last: 0.00)
  dontlink: Did 10000 iterations in 28ms (average: 28.28ms of 100 iterations, diff since last: 0.00)



## instantiator

### iPad Air 2

  linkall:  Did 10000 iterations in 92ms (average: 85.94ms of 100 iterations, diff since last: 0.06)
  dontlink: Did 10000 iterations in 91ms (average: 87.77ms of 100 iterations, diff since last: 0.03)

### iPhone X

  linkall:  Did 10000 iterations in 18ms (average: 17.73ms of 100 iterations, diff since last: 0.00)
  dontlink: Did 10000 iterations in 17ms (average: 17.42ms of 100 iterations, diff since last: 0.00)

link all:

    release initial:
    Oct 17 12:06:09 Rolfs-iPad-Air-iOS-82 helloworld[1132] <Warning>: Did 10000 iterations in 470ms (average: 470.48ms of 100 iterations, diff since last: 0.00)
    
    array fix:
    Oct 17 11:59:36 Rolfs-iPad-Air-iOS-82 helloworld[1128] <Warning>: Did 10000 iterations in 230ms (average: 229.19ms of 100 iterations, diff since last: 0.01)
    
    dict fix:
    Oct 17 12:15:11 Rolfs-iPad-Air-iOS-82 helloworld[1136] <Warning>: Did 10000 iterations in 222ms (average: 223.68ms of 100 iterations, diff since last: -0.02)
    
    array + memcpy fix:
    Oct 17 12:24:12 Rolfs-iPad-Air-iOS-82 helloworld[1139] <Warning>: Did 10000 iterations in 218ms (average: 219.74ms of 100 iterations, diff since last: -0.02)

    array binary search fix:
    Oct 17 14:00:36 Rolfs-iPad-Air-iOS-82 helloworld[1198] <Warning>: Did 10000 iterations in 215ms (average: 216.20ms of 100 iterations, diff since last: -0.01)


dont link (2993 entries in array):
	initial:
	Oct 17 13:14:44 Rolfs-iPad-Air-iOS-82 helloworld[1162] <Warning>: Did 10000 iterations in 753ms (average: 754.47ms of 100 iterations, diff since last: -0.01)

	array fix:
	Oct 17 12:42:41 Rolfs-iPad-Air-iOS-82 helloworld[1147] <Warning>: Did 10000 iterations in 448ms (average: 447.56ms of 100 iterations, diff since last: 0.00)

	dict fix:
	Oct 17 13:03:37 Rolfs-iPad-Air-iOS-82 helloworld[1158] <Warning>: Did 10000 iterations in 376ms (average: 376.94ms of 100 iterations, diff since last: -0.01)

	array + memcpy fix:
	Oct 17 12:53:24 Rolfs-iPad-Air-iOS-82 helloworld[1153] <Warning>: Did 10000 iterations in 450ms (average: 451.48ms of 100 iterations, diff since last: -0.01)

	binary search:
	Oct 17 13:36:50 Rolfs-iPad-Air-iOS-82 helloworld[1169] <Warning>: Did 10000 iterations in 661ms (average: 657.66ms of 100 iterations, diff since last: 0.03)

	array + binary search
	Oct 17 14:07:25 Rolfs-iPad-Air-iOS-82 helloworld[1204] <Warning>: Did 10000 iterations in 376ms (average: 373.96ms of 100 iterations, diff since last: 0.02)
