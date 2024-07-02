This felt way too different of a task (a given matrix becomes a random matrix of random dimensions), so I created a seperate folder.
I think if I combined it with the main task, both would be kind of "out of focus" or the code would be way too complicated for anyone other than me.
Notable changes: out.txt becomes unreadable for such large matrix dimensions, so I changed what data gets written and moved anything unnecessary to results.txt
(After checking that the code works for a random matrix A and debugging).
As expected, the elapsed time shows a power-law dependency to the matrix dimensions (y=a(x^b)) since in the log-log plot the results are spread out approximately in a straight line.
Since the contents of the matrix A are not important, i useed a seeded RNG to make everything more consistent during building and testing.
I set a soft-unofficial max value of around ~10 seconds of elapsed time for each loop iteration, that's why I stopped at n=160. For example, for n=200 I got a time as high as 28s.