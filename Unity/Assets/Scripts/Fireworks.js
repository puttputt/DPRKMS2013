#pragma strict
private var x:float = 0.0;
private var y:float = 0.0;
private var z:float = 0.0;

private var state:boolean = false;
private var TIME:float = 0.0;
private var radius:float = 0.0;

private var oldPosition : Vector3 = Vector3.zero;
private var newPosition : Vector3 = Vector3.zero;

// coefficients  
var a:float = -10.0;
var b:float = 35.0;  
var c:float = 0.0;

// multiplies the localPosition in the X and Z axis
var XZ_multiplier:float = 3.0;

// increases radius
var add_value:float = 0.05;

// angle around the local Y axis
var angle:float = 0;

// duration to explosion of fireworks
var duration_to_explosion:float = 1.5;

var particleArray:ParticleSystem[];
var trail_renderer:TrailRenderer;
var point_light:Light;

var enabled_light:boolean = true;

function Start () {
// Enables firework rocket
EnableRocket();
}

function Update () {

	if(state == true) {
	
	TIME += Time.fixedDeltaTime;
	radius = radius + add_value; 
	
	// Position
	x = radius * Mathf.Cos(0.0174532925*angle);
	y = a*radius*radius + b*radius + c;
	z = radius * Mathf.Sin(0.0174532925*angle);	
	transform.localPosition = Vector3(x*XZ_multiplier, y, z*XZ_multiplier);
	
	// Rotation
	oldPosition = newPosition;
	newPosition = transform.localPosition;
	var relativePos = newPosition - oldPosition;
	transform.localRotation = Quaternion.LookRotation(relativePos);
	
		// Enables explosion and disables firework rocket
		if(TIME > duration_to_explosion) {
		particleArray[1].transform.localPosition = particleArray[0].transform.localPosition;
	    DisableRocket(); 
		particleArray[1].Play();
		point_light.enabled = enabled_light;     
		}
	}
	
	// After explosion
	else {
		if(enabled_light == true)
		point_light.intensity = point_light.intensity - 0.5;
	
		// Destroys Firework gameObject	
		if(!particleArray[1].IsAlive())
		Destroy(transform.parent.gameObject); 	
	}

}

function EnableRocket() {
state = true;
particleArray[0].Play(false);
trail_renderer.time = 0.2;
}

function DisableRocket() {
state = false;
particleArray[0].enableEmission = false;
trail_renderer.time = 0;  
}

