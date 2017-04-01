<?php 
$servername = 'no';
$username = 'no';
$password = 'no';
$database = 'no';

$conn = new mysqli($servername, $username, $password, $database);

$playerPoints = $_POST['playerpoints'];
// just to make display look proper
$playerName = substr($_POST['playername'], 0, 20);
// we are going to use these '_' and ' ' symbols to parse records in unity so, just remove them.
$symbolsToReplace = ['_', ' ']; 
$playerName = str_replace($symbolsToReplace, '', $playerName);
// check if player record exists and points should be updated rather than inserted
$stmt = $conn->stmt_init();
$stmt->prepare("SELECT points FROM leaderboard WHERE name = ?");
$stmt->bind_param('s', $playerName);
$stmt->execute();
$result = $stmt->get_result()->fetch_assoc();

$points = 0;
if (!empty($result)) {
	$points = (int) $result['points'];
}

if ($playerPoints > $points) {
	$query = $conn->prepare(
		"UPDATE leaderboard
		SET points = ?
		WHERE name = ?"
	);
	$query->bind_param('ds', $playerPoints, $playerName);
	$query->execute();

} else {
	$query = $conn->prepare("INSERT INTO leaderboard (points, name) VALUES (?, ?)");
	$query->bind_param('ds', $playerPoints, $playerName);
	$query->execute();
}

$query = $conn->prepare("INSERT INTO leaderboard (points, name) VALUES (?, ?)");
$query->bind_param('ds', $playerPoints, $playerName);
$query->execute();
$query->close();
$conn->close();