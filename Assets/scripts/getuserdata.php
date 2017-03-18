<?php 
$servername = 'localhost';
$username = 'id1116309_gediminas';
$password = '987654321';
$database = 'id1116309_unity';

$conn = new mysqli($servername, $username, $password, $database);

$sql = "SELECT points, name FROM leaderboard ORDER BY points DESC LIMIT 10";
$result = $conn->query($sql);
$toReturn = '';
while ($row = $result->fetch_assoc()) {
        $toReturn .= $row['points'] . ' ' . $row['name'] . '_';
    }

echo substr($toReturn, 0, -1);