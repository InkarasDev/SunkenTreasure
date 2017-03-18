<?php 
$servername = 'mysql.hostinger.lt';
$username = 'u377991660_tba';
$password = 'MZqhY5SmFCkc';
$database = 'u377991660_board';

$conn = new mysqli($servername, $username, $password, $database);

$playerPoints = $_POST['playerpoints'];
$playerName = substr($_POST['playername'], 0, 49);

$query = $conn->prepare("INSERT INTO leaderboard (points, name) VALUES (?, ?)");
$query->bind_param('ds', $playerPoints, $playerName);
$query->execute();
$query->close();
$conn->close();