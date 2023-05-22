using System;
using System.Collections.Generic;
using Oscar;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class OscarVision : MonoBehaviour
{
	#region Variables

	public float sightRefreshTime = 2f;
	
	public List<DynamicObject> aiInSight = new List<DynamicObject>();	
	
	public List<DynamicObject> playerInSight = new List<DynamicObject>();
	
	public List<DynamicObject> allInSight = new List<DynamicObject>();

	public delegate void OnObjectSeen(GameObject thing);

	public event OnObjectSeen objectSeenEvent;

	#endregion

	void Start()
	{
		allInSight  = new List<DynamicObject>();
		aiInSight = new List<DynamicObject>();
		playerInSight = new List<DynamicObject>();

		StartCoroutine(CheckStillVisible());
	}

	#region OnTriggerEnter

	private void OnTriggerEnter(Collider other)
	{
		//everything vision for anyone's use :D
		if (other != null)
		{
			if (other.GetComponent<DynamicObject>() != null)
			{
				DynamicObject dynamicObj = other.GetComponent<DynamicObject>();
				
				if (!allInSight.Contains(dynamicObj))
				{
					allInSight.Add(dynamicObj);
					
					allInSight.Sort(Comparison);
				}
			}
		}
	}

	int Comparison(DynamicObject x, DynamicObject y)
	{
		if (Vector3.Distance(transform.position, x.transform.position) < Vector3.Distance(transform.position, y.transform.position))
		{
			return -1;
		}
		else
		{
			return 1;
		}
	}

	#endregion

	#region OnTriggerStay

	private IEnumerator CheckStillVisible()
	{
		while (true)
		{			
			// CLEAR ALL OTHERS
			aiInSight.Clear();
			playerInSight.Clear();

			foreach (DynamicObject dynamicObj in allInSight)
			{
				//everything vision for anyone's use :D
				if (dynamicObj != null)
				{
					//LINECAST HERE. If false, continue (next in list)
					// Perform linecast
					bool hit = Physics.Linecast(transform.position, dynamicObj.transform.position);

					if (hit)
					{
						//are they a Bee, use this:
						if (!aiInSight.Contains(dynamicObj))
						{
							if (dynamicObj.isAI == true)
		                    {
                     			//Add to list here
		                        aiInSight.Add(dynamicObj);
		                    }
						}
						if (!playerInSight.Contains(dynamicObj))
						{
							//are they a Civ, use this:
							if (dynamicObj.isPlayer == true)
							{
								//Add to list here
								playerInSight.Add(dynamicObj);
							}
						}
						// Invoke objectSeenEvent
						objectSeenEvent?.Invoke(dynamicObj.gameObject);
					}
				}
			}

			yield return new WaitForSeconds(sightRefreshTime);
		}
	}
	
	#endregion
	

	#region OnTriggerExit

	private void OnTriggerExit(Collider other)
	{
		if (other.GetComponent<DynamicObject>() != null)
		{
			DynamicObject dynamicObj = other.GetComponent<DynamicObject>();

			allInSight.Remove(dynamicObj);
			
			// print(dynamicObj.description);
			
		}
	}

	#endregion
}