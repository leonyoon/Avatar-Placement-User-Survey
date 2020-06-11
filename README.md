# Avatar Placement User Survey Result

screenshot
- 24 space pairs
- 36 questions per house pair

~/Houses/ : images of 8 houses

~/PairOfHouses/ : images of 24 house pairs

~/UserPlacements/Pair#/P#S% /: images of user placed avatars (# - pair number and % - scene number)

---------------------------------------------------------------------------------------------------------------------------------------

data
- 45d feature vectors
- feature_x_#.txt (# - pair number)

~/x/ : 1 feature vector from the placement of person X, repeated 110 times (rows) for each question (1 placement x 110 repeats x 36 questions = 3960 rows) to match 110 negative random samples.

~/pos/ : 10 feature vectors of Avatar X' placed by participants for each question repeated 11 times (10 answers x 11 repeats x 36 questions = 3960 rows).

~/neg/ : 110 feature vectors of randomly placed Avatar X' for each question (110 samples x 36 questions = 3960 rows).

