using UnityEngine.EventSystems;
using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour
{

	public Interactable focus;
	public LayerMask movementmask;

	Camera cam;
	PlayerMotor motor;

	// Start is called before the first frame update
	void Start()
	{
		cam = Camera.main;
		motor = GetComponent<PlayerMotor>();
	}

	// Update is called once per frame
	void Update()
	{
		//Stops from moving if mouse is hovering over a gameobject or ui.
		if (EventSystem.current.IsPointerOverGameObject())
			return;

		//Checks input: Right Mouse button
		if (Input.GetMouseButtonDown(1))
		{
			//Casts ray to camera
			Ray ray = cam.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;

			if (Physics.Raycast(ray, out hit, 100, movementmask))
			{
				//Debug.Log("We hit " + hit.collider.name + " " + hit.point);
				motor.MoveToPoint(hit.point);

				RemoveFocus();
			}
		}

		//Checks input: Left Mouse button
		if (Input.GetMouseButtonDown(0))
		{
			//Creates Ray
			Ray ray = cam.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;

			//Ray hits something?
			if (Physics.Raycast(ray, out hit, 100))
			{
				//Check if ray hits interactable
				Interactable interactable = hit.collider.GetComponent<Interactable>();
				//Checks if it's interactable
				if (interactable != null)
				{
					SetFocus(interactable);
				}
			}
		}
	}

	void SetFocus(Interactable newFocus)
	{
		if (newFocus != focus)
		{
			if (focus != null)
				focus.OnDefocused();

			focus = newFocus;
			motor.FollowTarget(newFocus);
		}


		newFocus.OnFocused(transform);

	}

	void RemoveFocus()
	{
		if (focus != null)
			focus.OnDefocused();

		focus = null;
		motor.StopFollowTarget();
	}
}