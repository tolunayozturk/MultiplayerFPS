using UnityEngine;
using UnityEngine.Networking;

public class PlayerHealth : NetworkBehaviour 
{
	[SerializeField] int maxHealth = 3;

	Player player;

	[SyncVar (hook = "OnHealthChanged")] int health; // when client recieves the health change, update the canvas

	void Awake()
	{
		player = GetComponent<Player> ();
	}

	[ServerCallback]
	void OnEnable()
	{
		health = maxHealth;
	}

	[Server]
	public bool TakeDamage()
	{
		bool died = false;

		if (health <= 0)
			return died;

		health--;
		died = health <= 0;

		RpcTakeDamage (died);

		return died;
	}

	[ClientRpc]
	void RpcTakeDamage(bool died)
	{
		if (isLocalPlayer) {
			PlayerCanvas.canvas.FlashDamageEffect ();
		}
		if (died)
			player.Die ();
	}

	void OnHealthChanged(int value)
	{
		health = value;
		if (isLocalPlayer) {
			PlayerCanvas.canvas.SetHealth (value);
		}
	}
}