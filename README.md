Placement Retargeting of Virtual Avatars to Dissimilar Indoor Environments
---------------------------------------------------------------------------------------------------------------------------------------

<p align="center"><img src="TVCG_thumbnail_small.png" align="center" style="width: 25%; height: 25%"/> <br></p>

> **Placement Retargeting of Virtual Avatars to Dissimilar Indoor Environments**<br>

> IEEE TVCG, presented in IEEE ISMAR 2020<br>
> Paper: https://arxiv.org/abs/2012.11878<br>
> Project: http://motionlab.kaist.ac.kr/?page_id=6060<br>
> Video(method): https://www.youtube.com/watch?v=QIZJvcvW1qg&t=2s<br>
> Video(demo): https://www.youtube.com/watch?v=wwsWBeflVcU<br>

<br><br>


Data Visualization Application For Windows
---------------------------------------------------------------------------------------------------------------------------------------
https://drive.google.com/file/d/13PcQHmVxsHDgv_bbqmzMKffixaZOvAJL/view?usp=sharing

<p align="center"><img src="Visualizer_small.png" align="center" style="width: 25%; height: 25%"/> <br></p>
<br><br>

screenshot of user survey results
---------------------------------------------------------------------------------------------------------------------------------------
- 24 house pairs
- 36 questions per house pair

~/Houses/ : images of 8 houses

~/PairOfHouses/ : images of 24 house pairs

~/UserPlacements/Pair#/P#S% /: images of user placed avatars (# - pair number and % - scene number)
<br><br>


data
---------------------------------------------------------------------------------------------------------------------------------------
- 45d feature vectors
- feature_x_#.txt (# - pair number)

~/x/ : 1 feature vector from the placement of person X, repeated 110 times (rows) for each question (1 placement x 110 repeats x 36 questions = 3960 rows) to match 110 negative random samples.

~/pos/ : 10 feature vectors of Avatar X' placed by participants for each question repeated 11 times (10 answers x 11 repeats x 36 questions = 3960 rows).

~/neg/ : 110 feature vectors of randomly placed Avatar X' for each question (110 samples x 36 questions = 3960 rows).

