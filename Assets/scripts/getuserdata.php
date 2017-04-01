<?php 
$servername = 'no';
$username = 'no';
$password = 'no';
$database = 'no';

$conn = new mysqli($servername, $username, $password, $database);

$sql = "SELECT points, name FROM leaderboard ORDER BY points DESC LIMIT 10";
$result = $conn->query($sql);
$toReturn = '';
while ($row = $result->fetch_assoc()) {
        $toReturn .= $row['points'] . ' ' . $row['name'] . '_';
    }

echo substr($toReturn, 0, -1);