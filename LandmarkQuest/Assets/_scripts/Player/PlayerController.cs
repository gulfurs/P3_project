using UnityEngine.EventSystems;
using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour
{

	public Interactable focus;

	Camera cam;
	PlayerMotor motor;

	private ParticleSystem interactParticles;

	// Start is called before the first frame update
	void Start()
	{
		cam = Camera.main;
		motor = GetComponent<PlayerMotor>();
		interactParticles = GetComponentInChildren<ParticleSystem>();
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

			if (Physics.Raycast(ray, out hit, 10000))
			{
				PlayParticles(hit.point, null);
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
			if (Physics.Raycast(ray, out hit, 10000))
			{
				
				//Check if ray hits interactable
				Interactable interactable = hit.collider.GetComponent<Interactable>();
				//Checks if it's interactable
				if (interactable != null)
				{
					// Check if the interactable has the ObjectiveInteraction component
					if (interactable.GetComponent<ObjectiveInteraction>() != null)
					{
						PlayParticles(hit.point, "Objective");
					}
					else
					{
						PlayParticles(hit.point, "Interactable");
					}

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

	void PlayParticles(Vector3 position, string interactType)
	{
		ParticleSystem particle = Instantiate(interactParticles, position, Quaternion.identity);

		ParticleSystem.MainModule mainModule = particle.main;

		switch (interactType)
		{
			case "Objective":
				mainModule.startColor = Color.blue;
				break;

			case "Interactable":
				mainModule.startColor = Color.yellow;
				break;

			default:
				break;
		}

		Destroy(particle.gameObject, particle.main.duration);
	}


}