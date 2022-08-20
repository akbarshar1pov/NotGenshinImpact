using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharacterAttribute : MonoBehaviour {

	public float HP;
	public float MaxHP;
	public int MP;
	public float attack;
	public float defence;
	public float attackDistance;
	public float skillDistance;
	public GameObject damageTextObject;
	public GameObject damageEffect;
	public int damageTextDuring = 3;
	private GameObject effectContainer;
	private List<GameObject> damageTexts = new List<GameObject>( );
	private bool isDeath;
	public Slider healthSlider;

	public static int score = 0;
	public GameObject anim;
	private bool doorIsOpen = false;

	public bool IsDeath
	{
		get { return isDeath; }
	}

	void Start( ) {
		isDeath = false;
		effectContainer = GameObject.FindGameObjectWithTag("EffectContainer");
	}

	void Update( ) {
		updateDamageText( );
		healthSlider.value = HP;
		if(score == 5 && !doorIsOpen)
		{
			GameObject go = GameObject.Find("hinges");
			Animation anim = go.GetComponent<Animation>();
			anim.Play("Door");
			doorIsOpen = true;
		}
	}

	void updateDamageText( ) {

		damageTexts.RemoveAll(item => item == null);
		foreach ( var text in damageTexts ) {
			text.transform.Translate(new Vector3(0, 0.5f * Time.deltaTime, 0));
		}

	}

	public void TakeDamage( string str, bool isCritical = false ) {

		GameObject text = Instantiate(damageTextObject, this.transform.position + new Vector3(0, 1, 0), Quaternion.identity) as GameObject;
		text.GetComponent<TextMesh>( ).text = str;

		text.transform.LookAt(GameObject.FindGameObjectWithTag("MainCamera").gameObject.transform);
		text.transform.Rotate(new Vector3(0, 1, 0), 180);

		if ( isCritical ) {
			text.GetComponent<TextMesh>( ).color = Color.red;
		}
		
		damageTexts.Add(text);
		Destroy(text, 2f); 

		GameObject effect = Instantiate(damageEffect) as GameObject;
		effect.transform.position = this.gameObject.transform.position;
		effect.transform.position += new Vector3(0, 1, 0);
		Destroy(effect, 2f);

		this.HP -= int.Parse(str);
		if ( this.HP < 0 )
		{
			isDeath = true;
			score++;
		}

	}
}
