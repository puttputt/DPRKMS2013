/*
This camera smoothes out rotation around the y-axis and height.
Horizontal Distance to the target is always fixed.

There are many different ways to smooth the rotation but doing it this way gives you a lot of control over how the camera behaves.

For every of those smoothed values we calculate the wanted value and the current value.
Then we smooth it using the Lerp function.
Then we apply the smoothed values to the transform's position.
*/

// The target we are following
var target : Transform;
// The distance in the x-z plane to the target
var launchDistance = 14.0;
// the height we want the camera to be above the target
var launchHeight = 7.0;
//the height on the target that we want to look at
var launchTargetHeight = 7.0;
var launchFOV = 60;

var flightDistance = 7;
var flightHeight = 17;
var flightTargetHeight = 9;
var flightFOV = 85.0f;


var heightDamping = 100.0;
var rotationDamping = 3.0;

var cameraPanRange = 100;
var cameraPanHeight = 115;
var cameraBreachHeight = 195;
var cameraFOVRange = 100;

var cameraFOVChange = true;
var cameraPAN = true;
// Place the script in the Camera-Control group in the component menu
@script AddComponentMenu("Camera-Control/Smooth Follow")


function LateUpdate () {
    // Early out if we don't have a target
    if (!target)
        return;
    var distance : float;
    var height : float;
    var targetHeight : float;
    var fov = launchFOV;
    if(cameraPAN){
		if(this.transform.position.y > cameraPanHeight){
			var cameraPanDelta = (this.transform.position.y - cameraPanHeight)/cameraPanRange;
	        //Debug.Log("cam y " + cameraPanDelta);
			if(cameraPanDelta > 1.0f)
			{
	            height = flightHeight;
	            distance = flightDistance;
	            targetHeight = flightTargetHeight;
			}
	        else
	        {
	            distance = launchDistance + ((flightDistance-launchDistance)*cameraPanDelta);
	            height = launchHeight + ((flightHeight-launchHeight)*cameraPanDelta);
	            targetHeight = launchTargetHeight + ((flightTargetHeight-launchTargetHeight)*cameraPanDelta);
	        }
		}
		else{
	        distance = launchDistance;
	        height = launchHeight;
	        targetHeight=launchTargetHeight;
	    }
	}
	else{
		distance = launchDistance;
		height = launchHeight;
		targetHeight=launchTargetHeight;
	}
	if(cameraFOVChange){
		if(this.transform.position.y > cameraBreachHeight)
		{
			var cameraFOVDelta = (this.transform.position.y - cameraBreachHeight)/cameraFOVRange;
			if(cameraFOVDelta < 1){
			    Debug.Log("cam" + cameraFOVDelta);
				fov = launchFOV + ((flightFOV-launchFOV)*cameraFOVDelta);
			}
			else{
				fov = flightFOV;
			}
		}
    }
    this.camera.fieldOfView = fov;
    // Calculate the current rotation angles
    wantedRotationAngle = target.eulerAngles.y;
    wantedHeight = target.position.y + height;

    currentRotationAngle = transform.eulerAngles.y;
    currentHeight = transform.position.y;

    // Damp the rotation around the y-axis
    currentRotationAngle = Mathf.LerpAngle (currentRotationAngle, wantedRotationAngle, rotationDamping * Time.deltaTime);

    // Damp the height
    currentHeight = Mathf.Lerp (currentHeight, wantedHeight, heightDamping * Time.deltaTime);

    // Convert the angle into a rotation
    currentRotation = Quaternion.Euler (0, currentRotationAngle, 0);

    // Set the position of the camera on the x-z plane to:
    // distance meters behind the target
    transform.position = target.position;
    transform.position -= currentRotation * Vector3.forward * distance;

    // Set the height of the camera
    transform.position.y = currentHeight;
    var target2 = target.position + Vector3(0, targetHeight, 0);

    // Always look at the target
    transform.LookAt (target2);
}