<?php 
$servername = 'mysql.hostinger.lt';
$username = 'u377991660_tba';
$password = 'MZqhY5SmFCkc';
$database = 'u377991660_board';

$conn = new mysqli($servername, $username, $password, $database);

$sql = "SELECT points, name FROM leaderboard ORDER BY points DESC LIMIT 10";
$result = $conn->query($sql);
$resultsArray = [];
while ($row = $result->fetch_assoc()) {
        echo $row['points'] . '_' . $row['name'] . ' ';
    }

