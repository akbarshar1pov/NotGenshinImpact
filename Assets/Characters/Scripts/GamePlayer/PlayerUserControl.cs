using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;


[RequireComponent(typeof(PlayerCharacter))]
public class PlayerUserControl : MonoBehaviour {
	private PlayerCharacter m_Character; 		// Ссылка на PlayerCharacter в объекте
	private Transform m_Cam;                  	// Ссылка на основную камеру в сценах трансформируется
	private Vector3 m_CamForward;            	// Текущее прямое направление камеры
	private Vector3 m_Move;
	private bool m_Jump;                     	// Желаемое направление движения относительно мира, рассчитанное на основе camForward и пользовательского ввода.

	private static KeyCode[ ] AttackKeys = {
			KeyCode.I,		// Для удара
			KeyCode.J,		// Для джеба
			KeyCode.K,			// Для подъема
			KeyCode.L,			// За нарушение
			KeyCode.E,			// Для навыка 1
			KeyCode.Q,			// Для навыка 2
			KeyCode.U,			// Для навыка 3
			KeyCode.O,			// Для навыка 4
		};

	private static string[ ] Attacks = {
			"Jab",
			"Kick",
			"Rise",
			"Offence",
			"Skill1",
			"Skill2",
			"Skill3",
			"Skill4",
		};

	private void Start( ) {
		// получаем трансформацию основной камеры
		if ( Camera.main != null ) {
			m_Cam = Camera.main.transform;
		} else {
			Debug.LogWarning(
				"Warning: no main camera found. Third person character needs a Camera tagged \"MainCamera\", for camera-relative controls.", gameObject);
			// в этом случае мы используем относительные элементы управления, что, вероятно, не то, что хочет пользователь, но эй, мы их предупредили!
		}

		// получаем символ от третьего лица (он никогда не должен быть нулевым из-за требуемого компонента)
		m_Character = GetComponent<PlayerCharacter>( );
	}


	public static string[] getAttackStrings( ) {
		return Attacks;
	}

	private void Update( ) {
		if ( !m_Jump ) {
			m_Jump = CrossPlatformInputManager.GetButtonDown("Jump");
		}
	}


	// Фиксированное обновление вызывается синхронно с физикой
	private void FixedUpdate( ) {
		// read inputs
		float h = CrossPlatformInputManager.GetAxis("Horizontal");
		float v = CrossPlatformInputManager.GetAxis("Vertical");
		bool crouch = Input.GetKey(KeyCode.C);
		bool dodge = Input.GetKeyDown(KeyCode.F);

		for ( int i = 0; i < AttackKeys.Length; ++i ) {
			if ( Input.GetKeyDown(AttackKeys[i]) ) {
				m_Character.Attack(Attacks[i]);
				//m_Character.UpdateAnimator(Attacks[i]);
				//Input.ResetInputAxes( );
			}
		}

		if ( dodge ) {
			m_Character.UpdateAnimator("Dodge");
			//Input.ResetInputAxes( );
		}

		// рассчитываем направление движения для перехода к персонажу
		if ( m_Cam != null ) {
			// вычисляем относительное направление движения камеры:
			m_CamForward = Vector3.Scale(m_Cam.forward, new Vector3(1, 0, 1)).normalized;
			m_Move = v * m_CamForward + h * m_Cam.right;
		} else {
			// мы используем направления относительно мира в случае отсутствия основной камеры
			m_Move = v * Vector3.forward + h * Vector3.right;
		}
#if !MOBILE_INPUT
		// множитель скорости ходьбы
		if ( !Input.GetKey(KeyCode.LeftShift) ) m_Move *= 0.7f;
#endif
		// передаем все параметры скрипту управления персонажем
		// [Этот класс может ТОЛЬКО вызывать эту функцию для управления персонажем
		m_Character.Move(m_Move, crouch, m_Jump);
		m_Jump = false;
		//Debug.Log(m_Character.isAttacking( ));
	}
}