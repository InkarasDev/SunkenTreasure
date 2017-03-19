<?php 
$servername = 'localhost';
$username = 'id1116309_gediminas';
$password = '';
$database = 'id1116309_unity';

$conn = new mysqli($servername, $username, $password, $database);

$playerPoints = $_POST['playerpoints'];
// we have database limit on name for 50 symbols. 
$playerName = substr($_POST['playername'], 0, 49);
// we are going to use these '_' and ' ' symbols to parse records in unity so, just remove them.
$symbolsToReplace = ['_', ' ']; 
$playerName = str_replace($symbolsToReplace, '', $playerName);

// prepared statement to avoid sql injection.
$query = $conn->prepare("INSERT INTO leaderboard (points, name) VALUES (?, ?)");
$query->bind_param('ds', $playerPoints, $playerName);
$query->execute();
$query->close();
$conn->close();