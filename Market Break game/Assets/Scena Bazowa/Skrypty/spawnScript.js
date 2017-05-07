// Variable to store the enemy prefab
public var enemy : GameObject;
public var enemy2 : GameObject;
public var enemy3 : GameObject;

// Variable to know how fast we should create new enemies
public var spawnTime : float = 2;
public var spawnTime2 : float = 2;
public var spawnTime3 : float = 2;
public var ust : Ustawienia;

function Awake() {
    // Call the 'addEnemy' function in 0 second
    // Then every 'spawnTime' seconds
    InvokeRepeating("addEnemy", 0, spawnTime);
    InvokeRepeating("addEnemy2", 2, spawnTime2);
    InvokeRepeating("addEnemy3", 5, spawnTime3);
}


// New function to spawn an enemy
function addEnemy() {
if(!ust.spawnerPause)
{
    // Get the renderer component of the spawn object
    var rd = GetComponent.<Renderer>();

    // Position of the left edge of the spawn object
    // It's: (position of the center) minus (half the width)
    var x1 = transform.position.x - rd.bounds.size.x/2;
    
    // Same for the right edge
    var x2 = transform.position.x + rd.bounds.size.x/2;

    // Randomly pick a point within the spawn object
    var spawnPoint = Vector2(Random.Range(x1, x2), transform.position.y-2.2);

    // Create an enemy at the 'spawnPoint' position
    Instantiate(enemy, spawnPoint, Quaternion.identity);
}
} 

function addEnemy2() {
if(!ust.spawnerPause)
{
    // Get the renderer component of the spawn object
    var rd = GetComponent.<Renderer>();

    // Position of the left edge of the spawn object
    // It's: (position of the center) minus (half the width)
    var x1 = transform.position.x - rd.bounds.size.x/2;

    // Same for the right edge
    var x2 = transform.position.x + rd.bounds.size.x/2;

    // Randomly pick a point within the spawn object
    var spawnPoint = Vector2(Random.Range(x1, x2), transform.position.y-2.2);

    // Create an enemy at the 'spawnPoint' position
    Instantiate(enemy2, spawnPoint, Quaternion.identity);
}
} 

function addEnemy3() {
if(!ust.spawnerPause)
{
    // Get the renderer component of the spawn object
    var rd = GetComponent.<Renderer>();

    // Position of the left edge of the spawn object
    // It's: (position of the center) minus (half the width)
    var x1 = transform.position.x - rd.bounds.size.x/2;

    // Same for the right edge
    var x2 = transform.position.x + rd.bounds.size.x/2;

    // Randomly pick a point within the spawn object
    var spawnPoint = Vector2(Random.Range(x1, x2), transform.position.y-2.2);

    // Create an enemy at the 'spawnPoint' position
    Instantiate(enemy3, spawnPoint, Quaternion.identity);
}
} 