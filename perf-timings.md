Configuration: release with llvm enabled

| Scenario          | initial | array fix | dict fix | array fix + memcpy fix | array + binary search fix |
| -                 | -       | -         | -        | -                      | -                         |
| Release/link all  | 470ms   | 229ms     | 224ms    | 220ms                  | 216                       |
| Release/dont link | 754ms   | 448ms     | 377ms    | 451ms                  | 373                       |


baseline
linkall:  Did 10000 iterations in 477ms (average: 477.07ms of 100 iterations, diff since last: 0.00)
dontlink: Did 10000 iterations in 738ms (average: 738.28ms of 100 iterations, diff since last: 0.00)

binary search:
linkall:  Did 10000 iterations in 479ms (average: 481.20ms of 100 iterations, diff since last: -0.02)
dontlink: Did 10000 iterations in 655ms (average: 656.14ms of 100 iterations, diff since last: -0.01)

array fix:
linkall:  Did 10000 iterations in 223ms (average: 223.54ms of 100 iterations, diff since last: -0.01)
dontlink: Did 10000 iterations in 376ms (average: 376.59ms of 100 iterations, diff since last: -0.01)


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
